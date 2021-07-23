using ClangFormatEditor.Helpers;
using System;
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
    }

    private ProjectSetup()
    {

    }

    #endregion

    #region Methods

    public static void CreateDirectory()
    {
      try
      {
        if (Directory.Exists(AppDataDirectory))
        {
          if (FileSystem.DoesFileExist(AppDataDirectory, AppConstants.ClangFormatExe))
          {
            File.Delete(Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe));
          }
        }
        else
        {
          FileSystem.CreateDirectory(AppDataDirectory);
        }
        File.Copy(AppConstants.ClangFormatExe, Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe));
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Setup Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    #endregion
  }
}
