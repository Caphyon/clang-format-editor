using ClangFormatEditor.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ClangFormatEditor.Update
{
  public class Updater
  {
    #region Members


    private int exitCode;

    #endregion


    #region Public Methods

    public void UpdateEditor()
    {
      if (UpdaterExecutableFound() == false) return;
      HandleUpdate();
    }

    private void HandleUpdate()
    {

    }

    #endregion

    #region Private Methods

    private static bool UpdaterExecutableFound()
    {
      return FileSystem.DoesFileExist(UpdaterConstants.Path, UpdaterConstants.Executable);
    }

    private static string GetInfoArguments()
    {
      var path = Path.Combine(UpdaterConstants.Path, UpdaterConstants.Executable);
      return string.Concat(UpdaterConstants.CommandParamater, "\"", path, "\"", " /justcheck");
    }

    private void UpdaterProcessExited(object sender, EventArgs e)
    {
      if (sender is Process)
      {
        var x = (Process)sender;
        // exitCode = sender.ExitCode;
        // sender.Close();
      }
    }

    private void LaunchUpdate()
    {

    }

    private void CallUpdaterProcess(string arguments)
    {
      try
      {
        var process = new Process();
        process.StartInfo.FileName = UpdaterConstants.Cmd;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.EnableRaisingEvents = true;
        process.Exited += UpdaterProcessExited;
        process.Start();
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Updater", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    #endregion

  }
}
