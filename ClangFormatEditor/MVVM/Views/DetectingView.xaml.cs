using System.Windows;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for DetectingView.xaml
  /// </summary>
  public partial class DetectingView : Window
  {
    public DetectingView(string title, string description = "", string descriptionExtra = "")
    {
      InitializeComponent();
      WindowTitle.Text = title;
      Description.Text = description;
      DescriptionExtra.Text = descriptionExtra;
    }
  }
}
