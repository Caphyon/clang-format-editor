using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ClangFormatEditor
{
  public sealed class ProjectSetup
  {
    #region Members

    public static readonly string AppDataDirectory;
    public static ProjectSetup Instance
    {
      get
      {
        return instance;
      }
    }

    private static readonly ProjectSetup instance = new();

    #endregion


    #region Constructor

    static ProjectSetup()
    {
      AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppConstants.ClangFormatEditor);
      SetupFormatDirectory();
    }

    private ProjectSetup()
    {

    }

    #endregion

    #region Methods

    private static void SetupFormatDirectory()
    {
      try
      {
        if (Directory.Exists(AppDataDirectory) == false)
        {
          Directory.CreateDirectory(AppDataDirectory);
          File.Copy(AppConstants.ClangFormatExe, Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe), true);
        }
        else
        {
          UpdateClangFormatExe();
        }
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Setup Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private static void UpdateClangFormatExe()
    {
      var installedFormatExe = Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe);
      if (File.Exists(installedFormatExe) == false || File.Exists(AppConstants.ClangFormatExe) == false) return;

      var existingFullVersion = FileVersionInfo.GetVersionInfo(installedFormatExe).FileVersion;
      var newFullVersion = FileVersionInfo.GetVersionInfo(AppConstants.ClangFormatExe).FileVersion;
      var existingVersion = new Version(existingFullVersion.Substring(0, existingFullVersion.IndexOf(' ')));
      var newVersionInfo = new Version(newFullVersion.Substring(0, existingFullVersion.IndexOf(' ')));

      if (existingVersion < newVersionInfo)
      {
        File.Copy(AppConstants.ClangFormatExe, Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe), true);
      }
    }

    #endregion
  }
}
