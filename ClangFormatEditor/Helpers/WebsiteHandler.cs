using System;
using System.Diagnostics;
using System.Windows;

namespace ClangFormatEditor.Helpers
{
  public class WebsiteHandler
  {
    public static void OpenUri(string uri)
    {
      try
      {
        var processStartInfo = new ProcessStartInfo(uri)
        {
          UseShellExecute = true,
        };
        Process.Start(processStartInfo);
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Editor Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
