using ClangFormatEditor.MVVM.ViewModel;
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
      DataContext = new MainWindowViewModel(this);
    }
  }
}
