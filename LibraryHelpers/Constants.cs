namespace Library.Helpers
{
    public static class Constants
    {
        public static char[] SplitDeliminator
        {
            get
            {
                return new[] { ' ', ',', ':', ';', '?', '!', '\t', '\"', '\r', '.', '\n', '-', '_', '(', ')', '"', '\'', '*' };
            }
        }

        public static string BooksResourceJsonFile =>
            $"{System.Configuration.ConfigurationManager.AppSettings["JsonDataFileFolderPath"]}books-data.json";

        //return "~/json/books-data.json";
        public static string BooksResourcePath => System.Configuration.ConfigurationManager.AppSettings["BooksResourceFolderPath"];

        //return "~/json/books-data.json";
        public const int TopWordCount = 10;
    }
}