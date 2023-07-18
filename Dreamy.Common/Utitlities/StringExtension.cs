namespace Dreamy.Common.Utitlities
{
    public static class StringExtension
    {
        /// <summary>
        /// Remove all white and space of text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveAllWhiteSpace(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Replace(" ", String.Empty);
        }

        /// <summary>
        /// Trim and ToLower
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Standard(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            return text.Trim().ToLower();
        }
    }
}
