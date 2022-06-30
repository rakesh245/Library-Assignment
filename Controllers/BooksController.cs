using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Library.BusinessEntity;

namespace Library.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var list = BooksList.GetBooksListObj().GetBooksList;
                return Json(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
		[HttpGet()]
        public async Task<IHttpActionResult> SearchById(int id)
        {
            try
            {
                var bookInstance = BooksList.GetBooksListObj();
                var book = await Task.Run(() => bookInstance.GetBookById(id));
                if (book == null)//Check if the ID passed has a book association
                {
                    return BadRequest("Book not found");
                }
                //Get common words from the book
                return Json(bookInstance.GetCommonWordsInTheBook(book.BookTitle));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("{id}/query={query}")]
        [HttpGet()]
        public IHttpActionResult GetWordCountFromTheBook(int id, string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            {
                return BadRequest("Invalid word entered.");
            }
            try
            {
                var bookInstance = BooksList.GetBooksListObj();
                var book = bookInstance.GetBookById(id);
                if (book == null)
                {
                    return BadRequest("Book not found");
                }
                //Get all words starts with the string
                return Json(bookInstance.GetKeyWordCountInTheBook(book.BookTitle, query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*
		The following GET methods are expected on the api/books controller:

			1. GET api/books
				Returns a list of Ids & Titles for all the books in the Resources folder
				Titles should be the name of the filename, minus the extension.
				The Id should be unique.

			2. GET api/books/{Id}
				Returns a list of the most common 10 words (min 5 letters) and how many times they occur in the specified book.
				When parsing, whitespace, linefeeds and punctiation should be ignored, and words matched case-insensitively (e.g. "the" and "The" and "THE" will be returned as "The"=3)
				Words should be returned in capital case (e.g. Word), and the list should be sorted in decreasing incidence.

			3. GET api/books/{Id}?query={query}
				Returns a list of all words which start with the specified string (min 3 letters) and the number of times they occur within the specified book.
				Case matching should be insensitive.

		The logic for these calls should largely be encapsulated in other classes. This should make it easier to Unit Test those classes
		to validate the expected behaviour.
		*/
    }
}
