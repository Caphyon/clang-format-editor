using ClangFormatDetector.Interfaces;
using System.Collections.Generic;

namespace ClangFormatDetector
{
  public class FormatOptionsHandler
  {
    public void DisableAllOptions(List<IFormatOption> FormatOptions)
    {
      foreach (var item in FormatOptions)
      {
        item.IsEnabled = false;
      }
    }
  }
}
