using System.Configuration;
using System.Windows;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for InputFormatStylesView.xaml
  /// </summary>
  public partial class InputMultipleDataView : Window
  {
    public InputMultipleDataView(string input)
    {
      //TODO handle owner
      InitializeComponent();
      DataContext = new InputMultipleDataViewModel(input);
      //Owner = SettingsProvider.FormatEditorView;
    }
  }
}
