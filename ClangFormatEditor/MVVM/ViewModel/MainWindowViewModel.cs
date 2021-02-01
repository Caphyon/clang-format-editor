using ClangFormatEditor.MVVM.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace ClangFormatEditor.MVVM.ViewModel
{
  public class MainWindowViewModel
  {
    #region Members

    private ICommand openFileSelector;
    private ICommand openConfigurator;
    private static DetectorView detector;
    private static ConfiguratorView configurator;
    private static FileSelectorView fileSelector;

    #endregion

    #region Properties

    public bool CanExecute
    { get; set; } = true;

    #endregion


    #region Commands

    public ICommand OpenFileSelectorCommand
    {
      get => openFileSelector ??= new RelayCommand(() => OpenDetector(), () => CanExecute);
    }

    public ICommand OpenConfiguratorCommand
    {
      get => openConfigurator ??= new RelayCommand(() => OpenConfigurator(), () => CanExecute);
    }

    #endregion

    #region Methods

    private static void OpenDetector()
    {
      if (detector == null && fileSelector == null)
      {
        detector = new DetectorView();
        detector.Closed += DetectorClosed;

        fileSelector = new FileSelectorView(detector);
        fileSelector.Closed += SelectorClosed;
        fileSelector.Show();
      }
      else
      {
        if (fileSelector != null && fileSelector.IsActive == false)
        {
          fileSelector.Activate();
        }
        else
        {
          if (detector.WindowState == WindowState.Minimized)
          {
            detector.WindowState = WindowState.Normal;
          }
          detector.Activate();
        }
      }
    }

    private static void OpenConfigurator()
    {
      if (configurator == null)
      {
        configurator = new ConfiguratorView();
        configurator.Closed += ConfiguratorClosed;
        configurator.Show();
      }
      else
      {
        if (configurator.WindowState == WindowState.Minimized)
        {
          configurator.WindowState = WindowState.Normal;
        }
        configurator.Activate();
      }
    }

    private static void DetectorClosed(object sender, EventArgs e)
    {
      detector = null;
    }

    private static void SelectorClosed(object sender, EventArgs e)
    {
      if (detector.IsLoaded == false)
      {
        detector = null;
      }
      fileSelector = null;
    }

    private static void ConfiguratorClosed(object sender, EventArgs e)
    {
      configurator = null;
    }

    #endregion
  }
}
