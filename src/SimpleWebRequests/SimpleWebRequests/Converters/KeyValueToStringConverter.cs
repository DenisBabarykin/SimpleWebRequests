using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebRequests.Converters
{
    /// <summary>Class for converting key-value parameters to "x-www-form-urlencoded" request string</summary>
    public class KeyValueToStringConverter
    {
        /// <summary>Converts key-value parameters to "x-www-form-urlencoded" request string</summary>
        /// <param name="pairs">Parameters as key-value pairs</param>
        /// <returns>"x-www-form-urlencoded" request string</returns>
        public static string Convert(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            if (pairs == null)
                throw new ArgumentNullException("pairs", "Collection of key-value pairs for converting to string must not be null.");

            string result = "";
            foreach (var pair in pairs)
                result += string.Format("{0}={1}&", pair.Key, pair.Value);
            if (result != "")
                result = result.Substring(0, result.Length - 1); // Removing last "&"
            return result;
        }
    }
}
