using System;
using System.Collections.Generic;
using Library.Helpers;
using Library.Web.Models;

namespace Library.DataEntity
{
    public class BooksData
    {
        private List<Book> _books;
        private string _bookContent;
        public BooksData()
        {
            
        }
        /// <summary>
        /// Returns the list of books collection
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public List<Book> GetBooks()
        {
            try
            {
                _books = FileReader.JsonFileRead<List<Book>>(Constants.BooksResourceJsonFile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to read books json file for the books list: {ex.Message}");
            }
            return _books;
        }
        /// <summary>
        /// Reads through the book and returns text as string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public string GetBookContent(string fileName)
        {
            try
            {
                //_bookContent = FileReader.TextFileRead(HttpContext.Current.Server.MapPath($"~/resources/{bookTitle}.txt"));
                _bookContent = FileReader.TextFileRead($"{Constants.BooksResourcePath}{fileName}.txt");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to read book data: {ex.Message}");
            }
            return _bookContent;
        }
    }
}