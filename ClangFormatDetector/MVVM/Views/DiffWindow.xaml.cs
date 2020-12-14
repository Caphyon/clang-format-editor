using ClangFormatDetector.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClangFormatDetector.MVVM.Views
{
  /// <summary>
  /// Interaction logic for DiffWindow.xaml
  /// </summary>
  public partial class DiffWindow : Window
  {
    #region Members

    private readonly DiffViewModel diffViewModel;
    private readonly InputDelayer inputDelayer;
    private int elementIndex;
    private bool resetOptionCalled;

    #endregion

    #region Constructor
    public DiffWindow()
    {
      InitializeComponent();
      diffViewModel = new DiffViewModel(this);
      DataContext = diffViewModel;

      inputDelayer = new InputDelayer();
      inputDelayer.Idled += InputIdled;
      elementIndex = 0;
      resetOptionCalled = false;
    }

    #endregion

    #region Public Methods

    public async Task ShowDiffAsync(List<string> filesPath, Window detectingWindowOwner)
    {
      await diffViewModel.DiffDocumentsAsync(filesPath, detectingWindowOwner);
    }

    #endregion

    #region Private Methods

    private void Diff_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
      if (e.VerticalChange == 0 && e.HorizontalChange == 0) return;
      if (sender == DiffInput)
      {
        DiffOutput.ScrollToVerticalOffset(e.VerticalOffset);
        DiffOutput.ScrollToHorizontalOffset(e.HorizontalOffset);
      }
      else
      {
        DiffInput.ScrollToVerticalOffset(e.VerticalOffset);
        DiffInput.ScrollToHorizontalOffset(e.HorizontalOffset);
      }
    }

    private void InputIdled(object sender, EventArgs e)
    {
      if (elementIndex < 0 || elementIndex >= FormatOptions.Items.Count) return;
      Dispatcher.Invoke(() =>
      {
        diffViewModel.OptionChanged(elementIndex);
      });
    }

    private void OptionInputChanged(object sender, TextChangedEventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      elementIndex = FormatOptions.Items.IndexOf(element);

      if (resetOptionCalled == false)
      {
        inputDelayer.TextChanged();
      }
      else
      {
        resetOptionCalled = false;
      }
    }

    private void OptionDropDownClosed(object sender, EventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      elementIndex = FormatOptions.Items.IndexOf(element);
      diffViewModel.OptionChanged(elementIndex);
    }

    private void OpenMultipleInput(object sender, RoutedEventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      elementIndex = FormatOptions.Items.IndexOf(element);
      diffViewModel.OpenMultipleInput(elementIndex);
    }

    private void ResetOption(object sender, RoutedEventArgs e)
    {
      var element = (sender as FrameworkElement).DataContext;
      if (element == null) return;
      resetOptionCalled = true;

      elementIndex = FormatOptions.Items.IndexOf(element);
      diffViewModel.ResetOption(elementIndex);
    }

    #endregion
  }
}
