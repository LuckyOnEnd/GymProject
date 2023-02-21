using GymProject.DAL.Interfaces;
using GymProject.Domain.Entity;
using GymProject.Domain.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.DAL.Repositories
{
    public class MembershipRepository  : IBaseRepository<Membership>
        //: IMembershipRepository
    {
        private readonly ApplicationDbContext _db;

        public MembershipRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Membership entity)
        {
            await _db.Memberships.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Membership entity)
        {
            _db.Memberships.Remove(entity);
            await _db.SaveChangesAsync();
        }
        public IQueryable<Membership> GetAll()
        {
            return _db.Memberships;
        }
        public async Task<Membership> Update(Membership entity)
        {
            _db.Memberships.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
