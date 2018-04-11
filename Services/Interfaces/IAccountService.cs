using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccounts();
    }
}
