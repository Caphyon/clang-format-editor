using ClangFormatEditor.MVVM.Models;
using System.Collections.Generic;
using System.Windows;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for InputFormatStylesView.xaml
  /// </summary>
  public partial class ToggleMultipleDataView : Window
  {
    public ToggleMultipleDataView(List<ToggleModel> input)
    {
      //TODO handle owner
      InitializeComponent();
      DataContext = new ToggleMultipleDataViewModel(input);
      //Owner = SettingsProvider.FormatEditorView;
    }
  }
}
