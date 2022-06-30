using Library.DataEntity;
using NUnit.Framework;

namespace Library.UnitTests
{
    [TestFixture]
    public class LibraryDataTests
    {
        public LibraryDataTests()
        {

        }
        [Test]
        public void ReadBooksJsonListTest()
        {
            var booksData = new BooksData();
            var result = booksData.GetBooks();
            Assert.IsNotNull(result);
        }
        [Test]
        public void ReadBookContentTest()
        {
            var booksData = new BooksData();
            var result = booksData.GetBookContent("A Tale Of Two Cities");
            Assert.IsNotNull(result);
        }
    }
}