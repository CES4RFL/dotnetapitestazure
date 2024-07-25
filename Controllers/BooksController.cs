using Microsoft.AspNetCore.Mvc;
using ApiBooks.Models;

namespace ApiBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2020 },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2021 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return _books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            book.Id = _books.Count + 1;
            _books.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _books.Remove(book);
            return NoContent();
        }
    }
}
