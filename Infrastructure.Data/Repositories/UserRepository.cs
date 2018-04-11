using MLS.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MLS.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MLS.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                var users = _context.AppUsers
                    .Include(s => s.Account).AsNoTracking()
                    .ToList();

                return Mapper.Map< IEnumerable<User> >(users);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
