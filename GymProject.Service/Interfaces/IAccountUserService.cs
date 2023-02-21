using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Interfaces
{
    public interface IAccountUserService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);

        Task<bool> ChangeDateConfirmedEmailFromUser(string userName);
    }
}
