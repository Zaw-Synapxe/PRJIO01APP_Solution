using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.API.Models;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell" },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" }
        };

        // GET: api/books
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return books;
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            Book book = books.Find(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/books
        [HttpPost]
        public ActionResult<Book> PostBook(Book newBook)
        {
            books.Add(newBook);
            return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            Book currentBook = books.Find(b => b.Id == id);

            if (currentBook == null)
            {
                return NotFound();
            }

            currentBook.Title = updatedBook.Title;
            currentBook.Author = updatedBook.Author;

            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            Book book = books.Find(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);

            return NoContent();
        }



        /*
        
        This controller defines several actions:

        GET /api/books: Gets a list of all books.
        GET /api/books/{id}: Gets a single book by ID.
        POST /api/books: Adds a new book to the list.
        PUT /api/books/{id}: Updates an existing book.
        DELETE /api/books/{id}: Deletes a book from the list.

        */
    }
}
