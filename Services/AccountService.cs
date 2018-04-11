using MLS.Infrastructure.Data.Interfaces;
using MLS.Domain;
using MLS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Account>> GetAccounts()
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}
