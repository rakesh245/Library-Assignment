using System.IO;
using Newtonsoft.Json;
namespace Library.Helpers
{
    public static class FileReader
    {
        /// <summary>
        /// Reads the Json file and convert the string to the generic object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns>T</returns>
        public static T JsonFileRead<T>(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(text);
            //using (var st = new StreamReader(filePath))
            //{
            //    return JsonConvert.DeserializeObject<T>(await st.ReadToEndAsync());
            //}
        }

        /// <summary>
        /// Reads the file and returns the string value of the text
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>string</returns>
        public static string TextFileRead(string filePath)
        {
            var text = File.ReadAllText(filePath);
            return text;
        }
    }
}