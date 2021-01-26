using ClangFormatEditor.Interfaces;
using System.Collections.Generic;

namespace ClangFormatEditor
{
  public class FormatOptionsHandler
  {
    public static void DisableAllOptions(List<IFormatOption> FormatOptions)
    {
      foreach (var item in FormatOptions)
      {
        item.IsEnabled = false;
      }
    }
  }
}
