namespace RepositoryLayer.Interfaces.BookInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.BookModels;

    public interface IBookRL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);

        public List<BookResponseModel> GetAllBooks();

        public BookResponseModel UpdateBooks(int BookId, BookPostModel bookPostModel);

        public BookResponseModel GetBookById(int BookId);

        public bool DeleteBook(int BookId);
    }
}
