using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MLS.Infrastructure.Data.Interfaces;
using MLS.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MLS.Infrastructure.Data
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            DbSet<MLS.Infrastructure.Data.Entities.Account> _dbSet = _context.Set<Entities.Account>();

            return Mapper.Map< IEnumerable < Account >>(_dbSet.ToList());
        }
    }
}
