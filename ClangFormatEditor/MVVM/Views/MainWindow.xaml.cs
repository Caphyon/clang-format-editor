using System.Windows;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void OpenDetector(object sender, RoutedEventArgs e)
    {
      var selectorView = new FileSelectorView();
      selectorView.Show();
    }

    private void OpenConfigurator(object sender, RoutedEventArgs e)
    {
      var editorView = new FormatEditorView();
      editorView.Show();
    }
  }
}
