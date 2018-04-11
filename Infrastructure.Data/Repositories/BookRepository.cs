using MLS.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MLS.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MLS.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext _context;
        public int TotalPages { get; private set; }

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book GetById(string Id)
        {
            try
            {
                var book = _context.Books
                    .Where(x => x.Id == Convert.ToInt32(Id))
                    .Include(b => b.Authors)
                    .ThenInclude(x => x.Author)
                    .Include(b => b.Categories)
                    .ThenInclude(c => c.Category)//.AsNoTracking()
                    .FirstOrDefault();

                return Mapper.Map<Book>(book);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBook(string Id, Book book)
        {
            var originalBook = _context.Books
                    .Where(x => x.Id == Convert.ToInt32(Id))
                    .Include(b => b.Authors)
                    .ThenInclude(x => x.Author)
                    .Include(b => b.Categories)
                    .ThenInclude(c => c.Category)
                    .FirstOrDefault();
            var bookDB = Mapper.Map<Entities.Book>(book);

            //to do : remove existing many-to-many relashionship with author
          
            //then add the new ones
            foreach (var currentAuthor in book.Authors)
            {
                var exists = originalBook.Authors.Where(x => x.BookId == originalBook.Id && x.AuthorId == currentAuthor.Id).FirstOrDefault();
                if (exists == null)
                {
                    originalBook.Authors.Add(new Entities.BookAuthor()
                    {
                        Book = originalBook,
                        Author = _context.Authors.Where(x => x.Id == currentAuthor.Id).FirstOrDefault()
                    });
                }
            }
            
            _context.Entry(originalBook).CurrentValues.SetValues(bookDB);

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Book> GetAll()
        {
            try
            {
                var books = _context.Books
                    .Include(b => b.Authors)
                    .ThenInclude(x => x.Author)
                    .Include(b => b.Categories)
                    .ThenInclude(c => c.Category)//.AsNoTracking()
                    .ToList();

                return Mapper.Map< IEnumerable<Book> >(books);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Book>> GetBoosksByCategory(int categoryId, int PageSize, int Page)
        {
            try
            {
                var _books = _context.Books
                    .Include(b => b.Authors)
                        .ThenInclude(x => x.Author)
                    .Include(b => b.Categories)
                        .ThenInclude(c => c.Category)
                    .Where(x => x.Categories.Any(cat => cat.CategoryId == categoryId))
                    .Skip((Page - 1) * PageSize).Take(PageSize + 1) //Trick: get additional item to detect if this page has more items.
                                    .AsNoTracking()
                                    .ToListAsync();
                
                return await Mapper.Map<Task<IEnumerable<Book>>>(_books);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetBooksCount(int categoryId)
        {
            try
            {
                return _context.Books
                    .Include(b => b.Authors)
                        .ThenInclude(x => x.Author)
                    .Include(b => b.Categories)
                        .ThenInclude(c => c.Category)
                    .Where(x => x.Categories.Any(cat => cat.CategoryId == categoryId))
                    .AsNoTracking()
                    .Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
