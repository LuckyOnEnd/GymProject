using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.Profile;
using GymProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);
        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}
