using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;

namespace  WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery bookquery = new GetBooksQuery(_context);
            var result = bookquery.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(GetBookByIdQuery id)
        {
            GetBookByIdQuery bookidquery = new GetBookByIdQuery(_context);
            var result = bookidquery.Handle();
            return Ok(result);
            
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{

        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return book;
        //} 

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            
return Ok();

        } 
        //Put
        [HttpPut("id")]
       public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;//Kitabın türü değiştirilmişse yeni kitabın türünü kullan,değiştirilmemişse mevcut kitabın türünü kullan.
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book == null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }

}