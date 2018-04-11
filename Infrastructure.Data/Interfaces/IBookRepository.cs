using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Infrastructure.Data.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(string Id);
        Task<IEnumerable<Book>> GetBoosksByCategory(int categoryId, int PageSize, int Page);
        void UpdateBook(string Id, Book book);

        int GetBooksCount(int categoryId);
    }
}
