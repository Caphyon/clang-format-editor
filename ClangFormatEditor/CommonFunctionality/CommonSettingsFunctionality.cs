using ClangFormatEditor.Helpers;
using ClangFormatEditor.MVVM.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClangFormatEditor
{
  public abstract class CommonSettingsFunctionality
  {
    #region Methods

    protected static string OpenFile(string fileName, string defaultExt, string filter)
    {
      string path = string.Empty;
      var openFileDialog = new OpenFileDialog
      {
        FileName = fileName,
        DefaultExt = defaultExt,
        Filter = filter
      };

      bool? result = openFileDialog.ShowDialog();

      if (result == true)
      {
        string filename = openFileDialog.FileName;
        path = filename;
      }
      return path;
    }

    protected static string[] OpenFiles(string fileName, string defaultExt, string filter)
    {
      var openFileDialog = new OpenFileDialog
      {
        FileName = fileName,
        DefaultExt = defaultExt,
        Filter = filter,
        Multiselect = true
      };

      if (openFileDialog.ShowDialog() != true)
        return null;

      return openFileDialog.FileNames;
    }


    /// <summary>
    /// Browse for folder from where the files path acording to the given seach instruction will be collected 
    /// </summary>
    /// <param name="searchFilePattern">Search pattern to apply in the file search</param>
    /// <param name="searchOption">Information about how to search inside the selected folder</param>
    /// <returns>Array of files path</returns>
    protected static string[] BrowseForFolderFiles(string searchFilePattern, SearchOption searchOption)
    {
      using var folderBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
      System.Windows.Forms.DialogResult result = folderBrowseDialog.ShowDialog();

      if (result != System.Windows.Forms.DialogResult.OK || string.IsNullOrWhiteSpace(folderBrowseDialog.SelectedPath))
        return null;

      return Directory.GetFiles(folderBrowseDialog.SelectedPath, searchFilePattern, searchOption);
    }

    /// <summary>
    /// Browse for folder path 
    /// </summary>
    /// <param name="searchFilePattern">Search pattern to apply in the file search</param>
    /// <param name="searchOption">Information about how to search inside the selected folder</param>
    /// <returns>Array of files path</returns>
    protected static string BrowseForFolderFiles()
    {
      using var folderBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
      System.Windows.Forms.DialogResult result = folderBrowseDialog.ShowDialog();

      if (result != System.Windows.Forms.DialogResult.OK || string.IsNullOrWhiteSpace(folderBrowseDialog.SelectedPath))
        return null;

      return folderBrowseDialog.SelectedPath;
    }

    protected static string SaveFile(string fileName, string defaultExt, string filter)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      string path = string.Empty;

      // Set the default file extension
      saveFileDialog.FileName = fileName;
      saveFileDialog.DefaultExt = defaultExt;
      saveFileDialog.Filter = filter;

      //Display the dialog window
      bool? result = saveFileDialog.ShowDialog();

      if (result == true)
      {
        path = saveFileDialog.FileName;
      }

      return path;
    }

    protected static void WriteContentToFile(string path, string content)
    {
      FileSystem.WriteContentToFile(path, content);
    }

    protected string OpenContentDialog(string content)
    {
      var inputDataViewModel = new InputDataViewModel(content);
      inputDataViewModel.ShowViewDialog();
      string input = CreateInput(inputDataViewModel.Inputs.ToList());

      return input;
    }

    private string CreateInput(List<InputDataModel> models)
    {
      var sb = new StringBuilder();

      foreach (var item in models)
      {
        if (string.IsNullOrWhiteSpace(item.InputData) == false)
          sb.Append(item.InputData).Append(";");
      }

      if (sb.Length > 0)
        sb.Length--;

      return sb.ToString();
    }

    #endregion
  }
}
