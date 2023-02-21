using GymProject.Domain.Entity;
using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<BaseResponse<UserViewModel>> GetUser(string login);


        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
