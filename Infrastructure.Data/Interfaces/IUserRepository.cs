using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLS.Infrastructure.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }
}
