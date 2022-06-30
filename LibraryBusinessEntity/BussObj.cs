using System.Collections.Generic;
using System.Linq;
using Library.BusinessEntity.Helpers;
using Library.Web.ModelBinders;
using Library.Web.Models;

namespace Library.BusinessEntity
{
    public class BookObj
    {
        private static Books _books { 
            get; 
            set; 
        }
        /// <summary>
        /// Returns the List of books 
        /// </summary>
        public static Books BooksList
        {
            get
            {
                if (_books != null)
                    return _books;//Retrieve books data if available without reading from the file
                                  //var booksList = FileReader.JsonFileRead<List<BookData>>(HttpContext.Current.Server.MapPath(Constants.BooksResourceJsonFile));
                var booksList = FileReader.JsonFileRead<List<BookData>>(Constants.BooksResourceJsonFile);
                _books = new Books { BooksList = booksList.OrderBy(x => x.BookTitle).ToList() };
                return _books;
            }
        }
        /// <summary>
        /// Returns the Book object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BookData GetBookById(int id)
        {
            return BooksList.BooksList.FirstOrDefault(b => b.BookId == id);
        }

        /// <summary>
        /// Returns a list of top 10 most common words used in the book
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        public static IEnumerable<WordCount> GetCommonWordsInTheBook(string bookTitle)
        {
            //var words = FileReader.TextFileRead(HttpContext.Current.Server.MapPath($"~/resources/{bookTitle}.txt"));
            var words = FileReader.TextFileRead($"{Constants.BooksResourcePath}{bookTitle}.txt");
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
        /// Returns a list of words used in the book that starts with the str passed
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<WordCount> GetWordCountInTheBook(string bookTitle, string str)
        {
            //var words = FileReader.TextFileRead(HttpContext.Current.Server.MapPath($"~/resources/{bookTitle}.txt"));
            var words = FileReader.TextFileRead($"{Constants.BooksResourcePath}{bookTitle}.txt");
            var orderedWords = words.Split(Constants.SplitDeliminator)
                                  .Where(x => x.ToLower().StartsWith(str.ToLower()))
                                  .Select(x => x.ToLower())
                                  .GroupBy(x => x.ToLower())
                                  .Select(x => new WordCount
                                  {
                                      Word = x.Key.FirstCharToUpper(),
                                      Count = x.Count()
                                  })
                                  .OrderByDescending(x => x.Count);
            return orderedWords;
        }
    }
}