using System.Text;

namespace amazon_clone.Utility.Helpers
{
    public static class StringHelper
    {
        public static string ToBase64String(this string originalString)
        {
            var textBytes = Encoding.UTF8.GetBytes(originalString);
            var encodedString = Convert.ToBase64String(textBytes);
            return encodedString;
        }

        public static string GetOriginalTextFromBase64String(this string encodedText)
        {
            var encodedTextBytes = Convert.FromBase64String(encodedText);
            var originalText = Encoding.UTF8.GetString(encodedTextBytes);
            return originalText;
        }
    }
}
