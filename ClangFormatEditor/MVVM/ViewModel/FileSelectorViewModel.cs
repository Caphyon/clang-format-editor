using ClangFormatEditor.Extensions;
using ClangFormatEditor.MVVM.Models;
using ClangFormatEditor.MVVM.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClangFormatEditor.MVVM.ViewModels
{
  public class FileSelectorViewModel : InputProvider
  {
    #region Members

    private readonly FileSelectorView selectorView;

    private ICommand browseCommand;
    private ICommand removeAllFilesCommand;
    private ICommand detectFormatStyleCommand;

    private long totalFilesSize = 0;
    private const long MAX_FILE_SIZE = 1000; //  1 MB
    private const int MAX_LENGTH_FILE_PATH = 90; // KB
    private const int MAX_SIZE_PER_FILE = 80; // KB

    private bool totalFileSizeFlag = false;
    private FileSizeWarningView warningWindow;

    private readonly DetectorView detectorView;

    #endregion


    #region Constructor

    public FileSelectorViewModel() { }

    public FileSelectorViewModel(FileSelectorView selector, DetectorView detector)
    {
      selectorView = selector;
      detectorView = detector;
      ChangeButtonsState(false);
    }

    #endregion


    #region Properties

    public ObservableCollection<SelectedFileModel> SelectedFiles { get; set; } = new ObservableCollection<SelectedFileModel>();

    public ICommand Browse
    {
      get => browseCommand ??= new RelayCommand(() => BrowseForFiles(), () => CanExecute);
    }
    public ICommand RemoveAllFilesCommand
    {
      get => removeAllFilesCommand ??= new RelayCommand(() => RemoveAllFiles(), () => CanExecute);
    }

    public ICommand DetectFormatStyleCommand
    {
      get => detectFormatStyleCommand ??= new RelayCommand(() => DetectFormatStyleAsync().SafeFireAndForget(), () => CanExecute);
    }

    public static bool CanExecute
    {
      get
      {
        return true;
      }
    }

    #endregion


    #region Methods

    private void BrowseForFiles()
    {
      string[] filePaths = OpenFiles(string.Empty, ".cpp", AppConstants.CodeFileExtensions);

      if (filePaths == null || filePaths.Length <= 0)
        return;

      for (int index = 0; index < filePaths.Length; ++index)
      {
        if (IsDuplicate(filePaths[index]))
          continue;

        string filePathToShow = CreateMiddleEllipsis(filePaths[index]);
        AddNewElement(filePaths[index], filePathToShow);
      }

      var sorted = SelectedFiles.OrderByDescending(file => file.FileSize).ToList();

      SelectedFiles.Clear();
      foreach (var file in sorted)
      {
        SelectedFiles.Add(file);
      }

      ChangeButtonsState(SelectedFiles.Count > 0);

      if (selectorView.WarningTextBox.Visibility != System.Windows.Visibility.Visible &&
        SelectedFiles.Any(file => file.FileSize > MAX_SIZE_PER_FILE))
      {
        selectorView.WarningTextBox.Visibility = System.Windows.Visibility.Visible;
      }

      if (!totalFileSizeFlag && totalFilesSize > MAX_FILE_SIZE)
      {
        totalFileSizeFlag = true;
        warningWindow = new FileSizeWarningView(selectorView);
        warningWindow.Closed += ChildWindow_Closed;
        warningWindow.Show();
      }
    }

    private void ChildWindow_Closed(object sender, System.EventArgs e)
    {
      if (warningWindow != null)
      {
        warningWindow.Closed -= ChildWindow_Closed;
        warningWindow = null;
      }
    }

    public void CloseWindow()
    {
      if (warningWindow != null)
      {
        warningWindow.Close();
      }
      if (detectorView.IsLoaded == false)
      {
        detectorView.Close();
      }
    }

    private bool IsDuplicate(string filePath) => SelectedFiles.FirstOrDefault(model => model.FilePath == filePath) != null;

    private void AddNewElement(string filePath, string filePathToShow)
    {
      var model = new SelectedFileModel(filePath, filePathToShow);
      totalFilesSize += model.FileSize;
      SelectedFiles.Add(model);
    }

    private static string CreateMiddleEllipsis(string filePath)
    {
      if (filePath.Length <= MAX_LENGTH_FILE_PATH)
        return filePath;

      while (filePath.Length > MAX_LENGTH_FILE_PATH)
        filePath = filePath.Remove(filePath.Length / 2, 1);

      var begin = filePath.Substring(0, filePath.Length / 2);
      begin = begin.Reverse().SubstringAfter("\\").Reverse();

      var end = filePath.Substring(filePath.Length / 2);
      end = end.SubstringAfter("\\");

      return $"{begin}\\...\\{end}";
    }

    private void RemoveAllFiles()
    {
      SelectedFiles.Clear();
      totalFileSizeFlag = false;
      totalFilesSize = 0;
      selectorView.WarningTextBox.Visibility = System.Windows.Visibility.Hidden;
      ChangeButtonsState(false);
    }

    public void RemoveFile(int index)
    {
      if (index < 0 || index >= SelectedFiles.Count)
        return;

      SelectedFileModel model = SelectedFiles[index];
      totalFilesSize -= model.FileSize;
      SelectedFiles.RemoveAt(index);

      ChangeButtonsState(SelectedFiles.Count != 0);

      if (!SelectedFiles.Any(file => file.FileSize > MAX_SIZE_PER_FILE))
        selectorView.WarningTextBox.Visibility = System.Windows.Visibility.Hidden;

      totalFileSizeFlag = totalFilesSize > MAX_FILE_SIZE;
    }

    private async Task DetectFormatStyleAsync()
    {
      var filesPath = SelectedFiles.Select(model => model.FilePath).ToList();

      selectorView.IsEnabled = false;
      await detectorView.ShowDiffAsync(filesPath, selectorView);
      selectorView.IsEnabled = true;
      if (detectorView.IsLoaded)
      {
        selectorView.Close();
      }
    }

    private void ChangeButtonsState(bool stateFlag)
    {
      selectorView.DetectFormatStyleButton.IsEnabled = stateFlag;
      selectorView.RemoveAllSection.IsEnabled = stateFlag;
      selectorView.RemoveAllTextBlock.Foreground = stateFlag ?
        System.Windows.Media.Brushes.Black : System.Windows.Media.Brushes.Gray;
    }

    #endregion

  }
}
