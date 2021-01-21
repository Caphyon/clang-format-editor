using ClangFormatEditor.Enums;
using ClangFormatEditor.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClangFormatEditor
{
  public class ToggleMultipleDataViewModel
  {
    public List<ToggleModel> Input { get; set; } = new List<ToggleModel>();

    #region Properties

    public IEnumerable<ToggleValues> BooleanComboboxValues
    {
      get
      {
        return Enum.GetValues(typeof(ToggleValues)).Cast<ToggleValues>();
      }
    }

    #endregion


    #region Constructor

    public ToggleMultipleDataViewModel(List<ToggleModel> input)
    {
      Input = input;
    }

    // Empty constructor for 
    public ToggleMultipleDataViewModel()
    {

    }

    #endregion

  }
}
