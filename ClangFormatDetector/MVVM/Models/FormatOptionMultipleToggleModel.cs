using System.Collections.Generic;

namespace ClangFormatDetector.Models
{
  public class FormatOptionMultipleToggleModel : FormatOptionModel
  {
    #region Properties

    public List<ToggleModel> ToggleFlags { get; set; } = new List<ToggleModel>();

    #endregion

    #region Constructor

    public FormatOptionMultipleToggleModel()
    {
      HasMultipleToggle = true;
    }

    #endregion

  }
}
