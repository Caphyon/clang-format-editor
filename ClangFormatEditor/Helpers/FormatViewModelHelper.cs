using ClangFormatEditor.Enums;
using ClangFormatEditor.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClangFormatEditor.Helpers
{
  public class FormatViewModelHelper
  {
    #region Methods

    public static async Task<(bool, string)> CheckOptionsValidityAsync(string input, List<IFormatOption> formatStyleOptions, FormatStyle formatStyle)
    {
      string output = string.Empty;
      await Task.Run(() =>
      {
        var formatter = new StyleFormatter();
        output = formatter.FormatText(input, formatStyleOptions, formatStyle);
      });

      bool errorDetected = false;
      if (output.Contains("YAML") || output.Contains("Error") || output.Contains("do(es)"))
      {
        errorDetected = true;
      }
      return (errorDetected, output);
    }

    public static async Task<string> GetLineNumbersAsync(int numberOfLines)
    {
      var sb = new StringBuilder();
      await Task.Run(() =>
      {
        for (int i = 1; i <= numberOfLines; i++)
        {
          sb.AppendLine(i.ToString());
        }
      });
      return sb.ToString();
    }

    #endregion



  }
}
