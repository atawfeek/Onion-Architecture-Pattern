using MLS.Infrastructure.Data.Interfaces;
using MLS.Domain;
using MLS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MLS.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        public static IConfiguration _configuration { get; set; }

        public BookService(IBookRepository repository, IConfiguration Configuration)
        {
            _repository = repository;
            _configuration = Configuration;
        }

        public Task<Book> GetBookById(string Id)
        {
            try
            {
                return Task.FromResult(_repository.GetById(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBook(string Id, Book book)
        {
            try
            {
                _repository.UpdateBook(Id, book);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<Book>> GetBooks()
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

        public async Task<ModelCollection<Book>> GetBooksByCategory(int Id, int Page)
        {
            try
            {
                //Page Size
                int PageSize = 5;
                bool hasMoreItems = false;
                bool success = Int32.TryParse(_configuration["PageSzie"], out PageSize);

                //get books from repository
                var books = await _repository.GetBoosksByCategory(Id, PageSize, Page);
                
                //remove the additional item if any
                if (((IList<Book>)books).Count > PageSize)
                {
                    ((IList<Book>)books).RemoveAt(((IList<Book>)books).Count - 1);
                    hasMoreItems = true;
                }

                return new ModelCollection<Book>()
                {
                    Page = Page,
                    Items = books,
                    HasMoreItems = hasMoreItems
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
