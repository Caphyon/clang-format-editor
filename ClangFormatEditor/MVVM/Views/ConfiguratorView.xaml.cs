﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace ClangFormatEditor.MVVM.Views
{
  /// <summary>
  /// Interaction logic for FormatFileCreationView.xaml
  /// </summary>
  public partial class ConfiguratorView : Window
  {
    private readonly ConfiguratorViewModel formatEditorViewModel;

    private const string inputWindowDefaulText = "// --- Clang Power Tools - Format Style Editor ---\r\n//\r\n" +
                                                 "// Add your code here\r\n//\r\n" +
                                                 "// Turn ON any format option or select a Style\r\n//\r\n" +
                                                 "// Format is run automatically\r\n//\r\n" +
                                                "// Check the OUTPUT to see your formatted code";
    private const string outputWindowDefaulText = "// Your formatted code is displayed here";

    public ConfiguratorView()
    {
      InitializeComponent();
      formatEditorViewModel = new ConfiguratorViewModel(this);
      DataContext = formatEditorViewModel;
      CodeEditor.Text = inputWindowDefaulText;
      CodeEditorReadOnly.Text = outputWindowDefaulText;
    }

    private void RunFormat_TextBoxChanged(object sender, TextChangedEventArgs e)
    {
      formatEditorViewModel.RunFormat();
    }

    private void RunFormat_Editor(object sender, EventArgs e)
    {
      if (formatEditorViewModel.IsAnyOptionEnabled() == false) return;
      formatEditorViewModel.RunFormat();
    }

    private void RunFormat_DropDownClosed(object sender, EventArgs e)
    {
      formatEditorViewModel.RunFormat();
    }

    private void OpenMultipleInput(object sender, RoutedEventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      formatEditorViewModel.OpenMultipleInput(FormatOptions.Items.IndexOf(element));
    }

    private void CodeEditor_PreviewDragOver(object sender, DragEventArgs e)
    {
      formatEditorViewModel.PreviewDragOver(e);
    }

    private void CodeEditor_PreviewDrop(object sender, DragEventArgs e)
    {
      formatEditorViewModel.PreviewDrop(e);
    }
  }
}
