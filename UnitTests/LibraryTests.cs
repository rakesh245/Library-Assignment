using System.Linq;
using System.Threading.Tasks;
using Library.BusinessEntity;
using Library.Web.Models;
using NUnit.Framework;

namespace Library.UnitTests
{
    [TestFixture]
    public class LibraryTests
    {
        public LibraryTests()
        {
        }
        [Test]
        public void GetBooksTest()
        {
            var books = BooksList.GetBooksListObj().GetBooksList;
            Assert.IsNotNull(books);
        }
        [Test]
        public async Task GetBookByIdTestAsync()
        {
            var bookInstance = BooksList.GetBooksListObj();
            var book = await Task.Run(() => bookInstance.GetBookById(2));
            Assert.IsNotNull(book);
            Assert.AreEqual("Moby Dick", book.BookTitle);
            var words = bookInstance.GetCommonWordsInTheBook(book.BookTitle);
            Assert.AreEqual(751, words.FirstOrDefault(x => x.Word == "There").Count);
            Assert.AreEqual(308, words.FirstOrDefault(x => x.Word == "Chapter").Count);
        }

        [Test]
        public async Task GetKeywordCountFromTheBookTestAsync()
        {
            var bookInstance = BooksList.GetBooksListObj();
            var book = await Task.Run(() => bookInstance.GetBookById(2));
            Assert.IsNotNull(book);
            Assert.AreEqual("Moby Dick", book.BookTitle);
            var words = bookInstance.GetKeyWordCountInTheBook(book.BookTitle,"ther");
            WordCount result = null;
            result = words.FirstOrDefault(x => x.Word == "There");
            Assert.AreEqual(751, result.Count);
            result = words.FirstOrDefault(x => x.Word == "Therein");
            Assert.AreEqual(6, result.Count);
        }
    }
}