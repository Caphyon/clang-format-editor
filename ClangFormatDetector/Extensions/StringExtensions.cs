using System.Globalization;

namespace ClangFormatDetector.Extensions
{
  public static class StringExtensions
  {
    public static string SubstringBefore(this string aText, string aSearchedSubstring)
    {
      if (string.IsNullOrEmpty(aSearchedSubstring))
        return aSearchedSubstring;

      CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
      int index = compareInfo.IndexOf(aText, aSearchedSubstring, CompareOptions.Ordinal);

      if (index < 0)
        return string.Empty; //No such substring

      return aText.Substring(0, index);
    }

    public static string SubstringAfter(this string aText, string aSearchedSubstring)
    {
      if (string.IsNullOrEmpty(aSearchedSubstring))
        return aText;

      CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
      int index = compareInfo.IndexOf(aText, aSearchedSubstring, CompareOptions.Ordinal);

      if (index < 0)
        return string.Empty; //No such substring

      return aText.Substring(index + aSearchedSubstring.Length);
    }
  }
}
