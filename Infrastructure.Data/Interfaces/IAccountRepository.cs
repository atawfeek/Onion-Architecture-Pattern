using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLS.Infrastructure.Data.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
    }
}
