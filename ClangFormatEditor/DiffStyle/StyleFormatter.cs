using ClangFormatEditor.Enums;
using ClangFormatEditor.Helpers;
using ClangFormatEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace ClangFormatEditor
{
  public class StyleFormatter
  {
    #region Methods

    public string FormatText(string textToFormat, List<IFormatOption> formatStyleOptions, FormatStyle selectedStyle)
    {
      CreatePaths(out string filePath, out string formatFilePath, out string folderPath);

      FileSystem.WriteContentToFile(formatFilePath, FormatOptionFile.CreateOutput(formatStyleOptions, selectedStyle).ToString());
      FileSystem.WriteContentToFile(filePath, textToFormat);

      var content = FormatFile(ProjectSetup.AppDataDirectory, filePath);

      FileSystem.DeleteDirectory(folderPath);

      return content;
    }

    private void CreatePaths(out string filePath, out string formatFilePath, out string folderPath)
    {
      string parentFolder = Path.Combine(ProjectSetup.AppDataDirectory, AppConstants.FormatDirectory);
      FileSystem.CreateDirectory(parentFolder);

      folderPath = Path.Combine(parentFolder, Guid.NewGuid().ToString());
      FileSystem.CreateDirectory(folderPath);

      filePath = Path.Combine(folderPath.ToString(), AppConstants.FormatTemp);
      formatFilePath = Path.Combine(ProjectSetup.AppDataDirectory, folderPath.ToString(), AppConstants.ClangFormat);
    }

    private static string FormatFile(string directoryPath, string filePath)
    {
      string clangFormatExe = Path.Combine(ProjectSetup.AppDataDirectory, AppConstants.ClangFormatExe);
      string output = string.Empty;

      if (string.IsNullOrWhiteSpace(clangFormatExe) || string.IsNullOrWhiteSpace(directoryPath)
        || string.IsNullOrWhiteSpace(filePath))
      {
        return string.Empty;
      }

      try
      {
        var process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = clangFormatExe;
        process.StartInfo.WorkingDirectory = directoryPath;
        process.StartInfo.Arguments = $"-style=file \"{Path.GetFullPath(filePath)}\"";

        process.Start();
        output = process.StandardOutput.ReadToEnd();
        if (string.IsNullOrWhiteSpace(output))
        {
          output = process.StandardError.ReadToEnd();
        }
        process.WaitForExit();
        process.Close();
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message, "Clang-Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return output;
    }
    #endregion
  }
}
