using GymProject.DAL.Interfaces;
using GymProject.Domain.Entity;
using GymProject.Domain.Enum;
using GymProject.Domain.Helpers;
using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.Account;
using GymProject.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Implementations
{
    [Authorize]
    public class AccountUserService : IAccountUserService
    {
        private readonly IBaseRepository<Profile> _proFileRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountUserService> _logger;

        public AccountUserService(IBaseRepository<User> userRepository,
            ILogger<AccountUserService> logger, IBaseRepository<Profile> proFileRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _proFileRepository = proFileRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => (x.Email == model.Email || x.Name == model.Name));
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с таким логином или почтой уже есть",
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Role = Role.User,
                    Confirmmail = false,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };

                var profile = new Profile()
                {
                    UserId = user.Id,
                };

                await _userRepository.Create(user);
                await _proFileRepository.Create(profile);
                var result = Authenticate(user);
                
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Nie znaleziono użytkownika"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Nieprawidzlowe login lub hasło"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Nie znaleziono użytkownika"
                    };
                }

                user.Password = HashPasswordHelper.HashPassword(model.NewPassword);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Hasło zmienione"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

       
        public async Task<bool> ChangeDateConfirmedEmailFromUser(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userName);

                user.Confirmmail = true;
                await _userRepository.Update(user);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                return false;
            }
        }
    }
}
