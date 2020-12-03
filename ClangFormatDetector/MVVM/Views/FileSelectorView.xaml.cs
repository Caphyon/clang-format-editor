﻿using ClangFormatDetector.MVVM.ViewModels;
using System.Windows;

namespace ClangFormatDetector.MVVM.Views
{
  /// <summary>
  /// Interaction logic for FileSelectorView.xaml
  /// </summary>
  public partial class FileSelectorView : Window
  {
    private readonly FileSelectorViewModel fileSelectorViewModel;

    public FileSelectorView()
    {
      InitializeComponent();
      DataContext = fileSelectorViewModel;
      fileSelectorViewModel = new FileSelectorViewModel(this);
    }

    private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
    {
      var elementIndex = GetElementIndex(sender as FrameworkElement);
      ((FileSelectorViewModel)DataContext).RemoveFile(elementIndex);
    }

    private int GetElementIndex(FrameworkElement frameworkElement)
    {
      var element = frameworkElement.DataContext;
      return CollectionItems.Items.IndexOf(element);
    }

    private void FileSelectorWindow_Closed(object sender, System.EventArgs e)
    {
      fileSelectorViewModel.CloseWindow();
    }
  }
}