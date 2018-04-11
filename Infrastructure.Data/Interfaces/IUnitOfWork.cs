using MLS.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLS.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        void Save();
    }
}
