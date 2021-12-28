using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _db;
        private readonly IBookRepository _bookRepo;

        public BookController(BookDbContext db, IBookRepository bookRepo)
        {
            _db = db;
            _bookRepo = bookRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepo.GetBook(id);

            if (book == null)
            {
                return BadRequest("Can not find the book!");
            }

            return Ok(book);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetListBooks()
        {
            var list = await _bookRepo.GetListBooks();

            if (list == null)
            {
                return NoContent();
            }

            return Ok(list);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {

            var bookNew = await _bookRepo.Create(book);

            if (bookNew == null)
            {
                return BadRequest("The book can not be created!");
            }

            return Ok(bookNew);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            var bookUpdate = await _bookRepo.Update(book);

            if (bookUpdate == null)
            {
                return BadRequest("Can not update the book!");
            }

            return Ok(bookUpdate);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookRepo.Delete(id);

            if (!result)
            {
                return BadRequest("Can not delete the book!");
            }

            return Ok("Delete Success!");
        }

    }
}
