using System;
using System.Linq;
using WebApi.DBOperations;

namespace  WebApi.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        public BooksViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext  dbContext)
        {
            _dbContext = dbContext;
        }
        public BooksViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book == null)
                throw new InvalidOperationException("Böyle bir kitap mevcut değil.");
            book.Title = Model.Title;
            book.GenreId = Convert.ToInt32(Model.Genre);
            book.PageCount = Model.PageCount;
            book.PublishDate = Convert.ToDateTime(Model.PublishDate);
            return Model;
         }
        public class BooksViewModel
        {
            public string Title { get; set; }

            public int PageCount { get; set; }

            public string PublishDate{ get; set; }

            public string Genre { get; set; }
        }

    }
}
