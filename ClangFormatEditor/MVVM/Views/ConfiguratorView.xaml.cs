using System;
using System.Windows;
using System.Windows.Controls;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for FormatFileCreationView.xaml
  /// </summary>
  public partial class ConfiguratorView : Window
  {
    private readonly ConfiguratorViewModel configuratorViewModel;

    public ConfiguratorView()
    {
      InitializeComponent();
      configuratorViewModel = new ConfiguratorViewModel(this);
      DataContext = configuratorViewModel;
    }

    private void RunFormat_TextBoxChanged(object sender, TextChangedEventArgs e)
    {
      configuratorViewModel.RunFormat();
    }

    private void RunFormat_DropDownClosed(object sender, EventArgs e)
    {
      configuratorViewModel.RunFormat();
    }

    private void OpenMultipleInput(object sender, RoutedEventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      configuratorViewModel.OpenMultipleInput(FormatOptions.Items.IndexOf(element));
    }

    private void CodeEditor_PreviewDragOver(object sender, DragEventArgs e)
    {
      configuratorViewModel.PreviewDragOver(e);
    }

    private void CodeEditor_PreviewDrop(object sender, DragEventArgs e)
    {
      configuratorViewModel.PreviewDrop(e);
    }
  }
}
