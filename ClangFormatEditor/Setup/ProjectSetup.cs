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

    private static readonly ProjectSetup instance = new ProjectSetup();

    #endregion

    static ProjectSetup()
    {
      AppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppConstants.ClangFormatEditor);
      CreateDirectory();
    }

    private ProjectSetup()
    {

    }

    #region Constructor


    #endregion

    #region Methods

    private static void CreateDirectory()
    {
      try
      {
        if (FileSystem.DoesFileExist(AppDataDirectory, AppConstants.ClangFormatExe)) return;
        FileSystem.CreateDirectory(AppDataDirectory);
        File.Copy(AppConstants.ClangFormatExe, Path.Combine(AppDataDirectory, AppConstants.ClangFormatExe));
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Setup Failed", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    #endregion
  }
}
