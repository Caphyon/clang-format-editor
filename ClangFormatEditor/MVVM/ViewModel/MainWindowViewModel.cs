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
    private static MainWindow mainWindow;

    #endregion

    #region Properties

    public bool CanExecute
    { get; set; } = true;

    #endregion

    #region Constructor
    public MainWindowViewModel(MainWindow window)
    {
      mainWindow = window;
    }

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
      if ((detector == null || detector.IsLoaded == false) &&
          (fileSelector == null || fileSelector.IsLoaded == false))
      {
        detector = new DetectorView();

        fileSelector = new FileSelectorView(detector);
        fileSelector.Show();
      }
      else
      {
        if (fileSelector.IsActive == false && fileSelector.IsLoaded)
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
      if (configurator == null || configurator.IsLoaded == false)
      {
        configurator = new ConfiguratorView();
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

    #endregion
  }
}
