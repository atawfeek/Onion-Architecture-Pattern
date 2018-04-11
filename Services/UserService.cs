using MLS.Infrastructure.Data.Interfaces;
using MLS.Domain;
using MLS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return Task.FromResult(_repository.GetAll());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
