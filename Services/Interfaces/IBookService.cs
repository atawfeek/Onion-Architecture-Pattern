using MLS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLS.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(string Id);
        void UpdateBook(string Id, Book book);

        Task<ModelCollection<Book>> GetBooksByCategory(int Id, int Page);
    }
}
