using ClangFormatDetector.Enums;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClangFormatDetector.Interfaces
{
  public interface IFormatEditor
  {
    IEnumerable<ToggleValues> BooleanComboboxValues { get; }
    ICommand CreateFormatFileCommand { get; }
    ICommand ResetCommand { get; }
    List<IFormatOption> FormatOptions { get; set; }
    IFormatOption SelectedOption { get; set; }
    FormatStyle SelectedStyle { get; set; }

    void OpenMultipleInput(int index);
  }
}