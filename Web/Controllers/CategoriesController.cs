using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLS.Domain;
using MLS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MLS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IBookService _iBookService;
        private readonly ILoggerFactory _iLogger;

        public CategoriesController(IBookService iBookService, ILoggerFactory iLogger)
        {
            _iBookService = iBookService;
            _iLogger = iLogger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModelCollection<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok(new ModelCollection<Category>()
            {
                Items = new[] { new Category() { Name = "Category1" }, new Category() { Name = "Category2" } }
            }));
        }

        [HttpGet("{id}/subcategories")]
        [ProducesResponseType(typeof(ModelCollection<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            return await Task.FromResult(Ok(new ModelCollection<Category>()
            {
                Items = new[] { new Category() { Name = "Category3" }, new Category() { Name = "Category4" } }
            }));
        }

        // GET: api/Books
        [EnableCors("ebookPolicy")]
        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(ModelCollection<Book>), 200)]
        public async Task<IActionResult> GetBooksByCategory(int id, [FromQuery()] int page = 1)
        {
            try
            {
                var _booksCollection = await _iBookService.GetBooksByCategory(id, page);
                return Ok(_booksCollection);
            }
            catch (Exception ex)
            {
                _iLogger.CreateLogger<MLS.Domain.User>().LogError(ex.Message);
                return StatusCode(500);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            return await Task.FromResult(Ok(new Category()));
        }


    }
}