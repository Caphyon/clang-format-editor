using ClangFormatEditor.MVVM.Views;
using ClangFormatEditor.Update;
using System.Linq;
using System.Windows;

namespace ClangFormatEditor
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void AppStartup(object sender, StartupEventArgs e)
    {
      var mainWindow = new MainWindow();
      mainWindow.Show();
      Update(e);
    }

    private static void Update(StartupEventArgs e)
    {
      if (e.Args.Length > 0 && e.Args.First() == UpdaterConstants.VisualStudio) return;
      Updater.UpdateEditor();
    }
  }
}
