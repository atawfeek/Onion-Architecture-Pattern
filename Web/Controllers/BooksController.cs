using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLS.Domain;
using MLS.Services.Interfaces;

namespace MLS.Web.Controllers
{
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly IBookService _iBookService;
        private readonly ILogger<BooksController> _log;

        public BooksController(IBookService iBookService, ILogger<BooksController> log)
        {
            _iBookService = iBookService;
            _log = log;
        }

        //// GET: api/Books
        //[EnableCors("ebookPolicy")]
        //[HttpGet]
        //[ProducesResponseType(typeof(Models.ModelCollection<Book>), 200)]
        //public async Task<IActionResult> GetBooks()
        //{
        //    try
        //    {
        //        var _books = await _iBookService.GetBooks();
        //        return Ok(new Models.ModelCollection<Book>() { Items = _books });
        //    }
        //    catch(Exception ex)
        //    {
        //        _iLogger.CreateLogger<MLS.Domain.User>().LogError(ex.Message);
        //        return StatusCode(500);
        //    }
        //}

        [EnableCors("ebookPolicy")]
        [HttpGet("featured")]
        [ProducesResponseType(typeof(ModelCollection<Book>), 200)]
        public async Task<IActionResult> GetFeatured()
        {
            try
            {
                var _books = await _iBookService.GetBooks();
                return Ok(new ModelCollection<Book>() { Items = _books });
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [EnableCors("ebookPolicy")]
        [HttpGet("recently-added")]
        [ProducesResponseType(typeof(ModelCollection<Book>), 200)]
        public async Task<IActionResult> GetRecentlyAdded()
        {
            try
            {
                var _books = await _iBookService.GetBooks();
                return Ok(new ModelCollection<Book>() { Items = _books });
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return StatusCode(500);
            }
        }


        // GET: api/Books/5
        [EnableCors("ebookPolicy")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        public async Task<IActionResult> GetBook([FromRoute] string id)
        {
            try
            {
                var _book = await _iBookService.GetBookById(id);
                return Ok(_book);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        // POST: api/Books?id=5
        [EnableCors("ebookPolicy")]
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult UpdateBook([FromRoute] string id, [FromBody] Book book)
        {
            try
            {
                _iBookService.UpdateBook(id, book);
                return Ok();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                _log.LogError(ex.Message);
                return StatusCode(500, "The update was not successful due to conflicting changes and move on. details: " + ex.Message);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

    }
}