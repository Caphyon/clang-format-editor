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
      CheckForUpdateProcess();
    }

    #endregion

    #region Private Methods

    private static void CheckForUpdateProcess()
    {
      try
      {
        var process = new Process();
        process.StartInfo.FileName = UpdaterConstants.Cmd;
        process.StartInfo.Arguments = GetJustCheckArguments();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = true;
        process.Exited += CheckForUpdateProcessExited;
        process.Start();
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Updater", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private static void CheckForUpdateProcessExited(object sender, EventArgs e)
    {
      if (sender is Process process)
      {
        switch (process.ExitCode)
        {
          case UpdaterConstants.NoUpdateReturnCode:
            return;
          case UpdaterConstants.UpdateFoundReturnCode:
            StartUpdateProcess();
            break;
          default:
            return;
        }
        process.Close();
      }
    }

    private static void StartUpdateProcess()
    {
      try
      {
        var process = new Process();
        process.StartInfo.FileName = UpdaterConstants.Cmd;
        process.StartInfo.Arguments = GetStartUpdateArguments();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.EnableRaisingEvents = true;
        process.Exited += StartUpdateProcessExited;
        process.Start();
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Updater", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private static void StartUpdateProcessExited(object sender, EventArgs e)
    {
      if (sender is Process process)
      {
        process.Close();
      }
    }

    private static bool UpdaterExecutableFound()
    {
      return FileSystem.DoesFileExist(UpdaterConstants.Path, UpdaterConstants.Executable);
    }

    private static string GetJustCheckArguments()
    {
      var path = Path.Combine(UpdaterConstants.Path, UpdaterConstants.Executable);
      return string.Concat(UpdaterConstants.CommandParamater, "\"", path, "\"", UpdaterConstants.CheckUpdateParamaters);
    }

    private static string GetStartUpdateArguments()
    {
      var path = Path.Combine(UpdaterConstants.Path, UpdaterConstants.Executable);
      return string.Concat(UpdaterConstants.CommandParamater, "\"", path, "\"", UpdaterConstants.StartUpdateParameters);
    }

    #endregion
  }
}
