using ClangFormatDetector.Enums;
using ClangFormatDetector.Helpers;
using ClangFormatDetector.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace ClangFormatDetector
{
  public class StyleFormatter
  {
    #region Members

    //TODO fi path
    private readonly string projectPath = "";

    #endregion


    #region Methods

    public string FormatText(string textToFormat, List<IFormatOption> formatStyleOptions, FormatStyle selectedStyle)
    {
      CreatePaths(out string filePath, out string formatFilePath, out string folderPath);

      FileSystem.WriteContentToFile(formatFilePath, FormatOptionFile.CreateOutput(formatStyleOptions, selectedStyle).ToString());
      FileSystem.WriteContentToFile(filePath, textToFormat);

      var content = FormatFileOutsideProject(projectPath, filePath);

      FileSystem.DeleteDirectory(folderPath);

      return content;
    }

    private void CreatePaths(out string filePath, out string formatFilePath, out string folderPath)
    {
      //TODO everything is in the project folder
      string parentFolder = Path.Combine(projectPath, "Format");
      FileSystem.CreateDirectory(parentFolder);

      folderPath = Path.Combine(parentFolder, Guid.NewGuid().ToString());
      FileSystem.CreateDirectory(folderPath);

      filePath = Path.Combine(folderPath.ToString(), "FormatTemp.cpp");
      formatFilePath = Path.Combine(projectPath, folderPath.ToString(), ".clang-format");
    }

    private string FormatFileOutsideProject(string directoryPath, string filePath)
    {
      // TODO fix path
      string vsixPath = Path.GetDirectoryName("");
      string output = string.Empty;

      if (string.IsNullOrWhiteSpace(vsixPath) || string.IsNullOrWhiteSpace(directoryPath)
        || string.IsNullOrWhiteSpace(filePath))
      {
        return string.Empty;
      }

      try
      {
        //TODO fix path
        var process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.FileName = "PATH HERE";
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
