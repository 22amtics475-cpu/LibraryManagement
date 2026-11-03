using LibraryManagement.Models;
using LibraryManagement.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _booksService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _booksService.GetByIdAsync(id);

            if (book == null)
                return NotFound(new { message = "Book not found." });

            return Ok(book);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Keyword is required." });

            var books = await _booksService.SearchAsync(keyword);
            return Ok(books);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var books = await _booksService.GetLowStockAsync();
            return Ok(books);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = await _booksService.GetStatisticsAsync();
            return Ok(statistics);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Books book)
        {
            var createdBook = await _booksService.AddAsync(book);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdBook.Id },
                createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Books book)
        {
            var updated = await _booksService.UpdateAsync(id, book);

            if (!updated)
                return NotFound(new { message = "Book not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _booksService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Book not found." });

            return NoContent();
        }

        [HttpPost("{id}/issue")]
        public async Task<IActionResult> Issue(int id)
        {
            var issued = await _booksService.IssueAsync(id);

            if (!issued)
            {
                var book = await _booksService.GetByIdAsync(id);

                if (book == null)
                    return NotFound(new { message = "Book not found." });

                return Conflict(new { message = "No available copies to issue." });
            }

            return Ok(new { message = "Book issued successfully." });
        }

    }
}
