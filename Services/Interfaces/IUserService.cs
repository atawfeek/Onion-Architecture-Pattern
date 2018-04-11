using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
