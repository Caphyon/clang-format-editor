using ClangFormatEditor.Enums;
using ClangFormatEditor.Extensions;
using ClangFormatEditor.Interfaces;
using ClangFormatEditor.MVVM.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClangFormatEditor
{
  public class FormatOptionsProvider
  {
    #region Members

    //TODO double instance of LlvmData
    public static FormatOptionsLlvmData CustomOptionsData;
    public static FormatOptionsLlvmData LlvmOptionsData;
    public static FormatOptionsGoogleData GoogleOptionsData;
    public static FormatOptionsChromiumData ChromiumOptionsData;
    public static FormatOptionsMozillaData MozillaOptionsData;
    public static FormatOptionsWebKitData WebkitOptionsData;
    public static FormatOptionsMicrosoftData MicrosoftOptionsData;

    #endregion

    #region Constructor

    static FormatOptionsProvider()
    {
      InitializeFormatData();
    }

    #endregion

    #region Methods
    public static void ResetOptions()
    {
      InitializeFormatData();
    }

    public static List<IFormatOption> GetDefaultOptionsForStyle(FormatStyle style)
    {
      switch (style)
      {
        case FormatStyle.LLVM:
          return FormatOptionsAllData.FormatOptions.Values.ToList();
        case FormatStyle.Google:
          return new FormatOptionsGoogleData().FormatOptions;
        case FormatStyle.Chromium:
          return new FormatOptionsChromiumData().FormatOptions;
        case FormatStyle.Microsoft:
          return new FormatOptionsMicrosoftData().FormatOptions;
        case FormatStyle.Mozilla:
          return new FormatOptionsMozillaData().FormatOptions;
        case FormatStyle.WebKit:
          return new FormatOptionsWebKitData().FormatOptions;
        default:
          break;
      }
      return new FormatOptionsLlvmData().FormatOptions;
    }

    public static List<IFormatOption> CloneDetectedOptions(List<IFormatOption> formatOptions)
    {
      var clonedOptions = new List<IFormatOption>();
      foreach (var option in formatOptions)
      {
        switch (option)
        {
          case FormatOptionToggleModel toggleModel:
            clonedOptions.Add(toggleModel.Clone());
            break;
          case FormatOptionInputModel inputModel:
            clonedOptions.Add(inputModel.Clone());
            break;
          case FormatOptionMultipleToggleModel multipleToggleModel:
            clonedOptions.Add(multipleToggleModel.Clone());
            break;
          case FormatOptionMultipleInputModel multipleInputModel:
            clonedOptions.Add(multipleInputModel.Clone());
            break;
          default:
            break;
        }
      }
      return clonedOptions;
    }

    private static void InitializeFormatData()
    {
      //TODO use FormatOptionsAllData for all option types 
      CustomOptionsData = new FormatOptionsLlvmData
      {
        FormatOptions = FormatOptionsAllData.GetFormatOptionsValues()
      };
      LlvmOptionsData = new FormatOptionsLlvmData();
      GoogleOptionsData = new FormatOptionsGoogleData();
      ChromiumOptionsData = new FormatOptionsChromiumData();
      MozillaOptionsData = new FormatOptionsMozillaData();
      WebkitOptionsData = new FormatOptionsWebKitData();
      MicrosoftOptionsData = new FormatOptionsMicrosoftData();
    }

    #endregion
  }
}
