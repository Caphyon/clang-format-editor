using ClangFormatEditor.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ClangFormatEditor.Update
{
  public class Updater
  {
    #region Public Methods

    public static void UpdateEditor()
    {
      if (UpdaterExecutableFound() == false) return;
      StartUpdaterProcess(GetArguments(UpdaterConstants.CheckUpdateParamaters), CheckForUpdateProcessExited);
    }

    #endregion

    #region Private Methods

    private static void StartUpdaterProcess(string arguments, EventHandler ClosedEvent)
    {
      try
      {
        var process = new Process();
        process.StartInfo.FileName = UpdaterConstants.Cmd;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = true;
        process.Exited += ClosedEvent;
        process.Start();
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, UpdaterConstants.ClangFormatUpdater, MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private static void CheckForUpdateProcessExited(object sender, EventArgs e)
    {
      if (sender is Process process)
      {
        switch (process.ExitCode)
        {
          case UpdaterConstants.NoUpdateReturnCode:
            process.Close();
            return;
          case UpdaterConstants.UpdateFoundReturnCode:
            StartUpdaterProcess(GetArguments(UpdaterConstants.StartUpdateParameters), StartUpdateProcessExited);
            break;
          default:
            process.Close();
            return;
        }
      }
    }

    private static void StartUpdateProcessExited(object sender, EventArgs e)
    {
      if (sender is Process process)
      {
        process.Close();
        UpdateClangFormatExe();
      }
    }

    private static bool UpdaterExecutableFound()
    {
      return FileSystem.DoesFileExist(UpdaterConstants.Path, UpdaterConstants.Executable);
    }

    private static string GetArguments(string updateParameters)
    {
      var path = Path.Combine(UpdaterConstants.Path, UpdaterConstants.Executable);
      return string.Concat(UpdaterConstants.CommandParamater, "\"", path, "\"", updateParameters);
    }

    private static void UpdateClangFormatExe()
    {
      try
      {
        if (FileSystem.DoesFileExist(ProjectSetup.AppDataDirectory, AppConstants.ClangFormatExe)) return;
        File.Delete(Path.Combine(ProjectSetup.AppDataDirectory, AppConstants.ClangFormatExe));
        File.Copy(AppConstants.ClangFormatExe, Path.Combine(ProjectSetup.AppDataDirectory, AppConstants.ClangFormatExe));
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Setup Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    #endregion
  }
}
