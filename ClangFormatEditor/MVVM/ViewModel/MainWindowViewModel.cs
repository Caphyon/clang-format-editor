using ClangFormatEditor.Helpers;
using ClangFormatEditor.MVVM.Views;
using System.Windows;
using System.Windows.Input;

namespace ClangFormatEditor.MVVM.ViewModel
{
  public class MainWindowViewModel
  {
    #region Members

    private ICommand openFileSelector;
    private ICommand openConfigurator;
    private ICommand openGitHub;
    private static DetectorView detector;
    private static ConfiguratorView configurator;
    private static FileSelectorView fileSelector;

    #endregion

    #region Properties

    public bool CanExecute { get; set; } = true;

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

    public ICommand OpenGitHubCommand
    {
      get => openGitHub ??= new RelayCommand(() => WebsiteHandler.OpenUri("https://github.com/Caphyon/clang-format-editor/issues"), () => CanExecute);
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
