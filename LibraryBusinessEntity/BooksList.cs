using System.Collections.Generic;
using System.Linq;
using Library.Helpers;
using Library.Web.ModelBinders;
using Library.Web.Models;
using Library.DataEntity;


namespace Library.BusinessEntity
{
    public class BooksList
    {
        private static readonly BooksList Instance = new BooksList();//Restrict from creating new instance of the class
        private readonly Books _books = new Books();

        private BooksList()
        {
            var bookData = new BooksData();
            var booksList = bookData.GetBooks();
            _books.BooksList = booksList.OrderBy(x => x.BookTitle).ToList();
        }

        /// <summary>
        /// Return the instance of the class
        /// </summary>
        /// <returns></returns>
        public static BooksList GetBooksListObj()
        {
            return Instance;
        }

        /// <summary>
        /// Returns Books object with the collections of all the books
        /// </summary>
        public Books GetBooksList => _books;

        /// <summary>
        /// Returns the Book object by the bookId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetBookById(int id)
        {
            return _books.BooksList.FirstOrDefault(b => b.BookId == id);
        }

        /// <summary>
        /// Returns the list of the top 10 most common words used in the book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> GetCommonWordsInTheBook(int id)
        {
            var bookTitle = _books.BooksList.First(b => b.BookId == id).BookTitle;
            var bookData = new BooksData();
            var words = bookData.GetBookContent(bookTitle);
            var orderedWords = words.Split(Constants.SplitDeliminator)
                .Where(x => x.Length > 4)
                .Select(x => x.ToLower())
                .GroupBy(x => x.ToLower())
                .Select(x => new WordCount
                {
                    Word = x.Key.FirstCharToUpper(),
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(Constants.TopWordCount);
            return orderedWords;
        }

        /// <summary>
        /// Returns the list the top 10 most common words used in the book
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> GetCommonWordsInTheBook(string bookTitle)
        {
            var bookData = new BooksData();
            var words = bookData.GetBookContent(bookTitle); 
            var orderedWords = words.Split(Constants.SplitDeliminator)
                .Where(x => x.Length > 4)
                .Select(x => x.ToLower())
                .GroupBy(x => x.ToLower())
                .Select(x => new WordCount
                {
                    Word = x.Key.FirstCharToUpper(),
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(Constants.TopWordCount);
            return orderedWords;
        }

        /// <summary>
        /// Returns a list of words used in the book that starts with the string passed
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public IEnumerable<WordCount> GetKeyWordCountInTheBook(string bookTitle, string str)
        {
            var bookData = new BooksData();
            var words = bookData.GetBookContent(bookTitle);
            var orderedWords = words.Split(Constants.SplitDeliminator)
                .Where(x => x.ToLower().StartsWith(str.ToLower()))
                .Select(x => x.ToLower())
                .GroupBy(x => x.ToLower())
                .Select(x => new WordCount
                {
                    Word = x.Key.FirstCharToUpper(),
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(10);
            return orderedWords;
        }
    }
}