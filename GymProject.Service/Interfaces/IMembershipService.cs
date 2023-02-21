using GymProject.Domain.Entity;
using GymProject.Domain.Response;
using GymProject.Domain.ViewModels.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Service.Interfaces
{
    public interface IMembershipService
    {
        IBaseResponse<List<Membership>> GetMemberships();
        Task<IBaseResponse<MembershipViewModels>> GetMembership(int id);

        Task<IBaseResponse<Membership>> Create(MembershipViewModels membershipViewModels, byte[] imageData);
        Task<IBaseResponse<bool>> DeleteMembership(int id);
        Task<IBaseResponse<Membership>> Edit(int id, MembershipViewModels model);
    }
}
