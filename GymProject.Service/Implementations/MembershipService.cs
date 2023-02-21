using GymProject.DAL.Interfaces;
using GymProject.Domain.Entity;
using GymProject.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymProject.Domain.ViewModels.Membership;
using Microsoft.EntityFrameworkCore;
using GymProject.Domain.Enum;

namespace GymProject.Service.Interfaces
{
    public class MembershipService : IMembershipService
    {
        public readonly IBaseRepository<Membership> _baseRepository;
        public MembershipService(IBaseRepository<Membership> _membershipRepository)
        {
            _baseRepository = _membershipRepository;
        }
        public IBaseResponse<List<Membership>> GetMemberships()
        {
            
            try
            {
                var membership = _baseRepository.GetAll().ToList();
                if(!membership.Any())
                {

                    return new BaseResponse<List<Membership>>()
                    {
                        Description = "Not founded",
                        StatusCode = Domain.Enum.StatusCode.OK
                    };
                }

                return new BaseResponse<List<Membership>>()
                {
                    Data = membership,
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Membership>>()
                {
                    Description = $"[GetMembership] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
        }

        public async Task<IBaseResponse<MembershipViewModels>> GetMembership(int id)
        {
            try
            {
                var membership = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (membership == null)
                {
                    return new BaseResponse<MembershipViewModels>()
                    {
                        Description = "Karnet nie znalezony",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new MembershipViewModels()
                {
                    Description = membership.Description,
                    Name = membership.Name,
                    Price = membership.Price,
                    Duration = membership.Duration,
                    Image = membership.Avatar
                };

                return new BaseResponse<MembershipViewModels>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MembershipViewModels>()
                {
                    Description = $"[GetMembership] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Membership>> Create(MembershipViewModels model, byte[] imageData)
        {
            try
            {
                var membership = new Membership()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Avatar = imageData,
                    Duration = model.Duration
                };
                await _baseRepository.Create(membership);

                return new BaseResponse<Membership>()
                {
                    StatusCode = Domain.Enum.StatusCode.OK,
                    Data = membership
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Membership>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteMembership(int id)
        {
            try
            {
                var membership = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (membership == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = Domain.Enum.StatusCode.UserNotFound,
                        Data = false
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteMembership] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<MembershipViewModels>> CreateMembership(MembershipViewModels membershipViewModel)
        {
            var baseResponse = new BaseResponse<MembershipViewModels>();
            try
            {
                var membership = new Membership()
                {
                    Description = membershipViewModel.Description,
                    Name = membershipViewModel.Name,
                    Price = membershipViewModel.Price
                };

                await _baseRepository.Create(membership);
            }
            catch (Exception ex)
            {
                return new BaseResponse<MembershipViewModels>()
                {
                    Description = $"[Create] :  { ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Membership>> Edit(int id, MembershipViewModels model)
        {
            try
            {
                var member = await _baseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (member == null)
                {
                    return new BaseResponse<Membership>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                member.Description = model.Description;
                member.Price = model.Price;
                member.Name = model.Name;
                member.Duration = model.Duration;

                await _baseRepository.Update(member);


                return new BaseResponse<Membership>()
                {
                    Data = member,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Membership>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
