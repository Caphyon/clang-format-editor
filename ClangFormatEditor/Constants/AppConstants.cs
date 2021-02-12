namespace ClangFormatEditor
{
  public static class AppConstants
  {
    public const string CodeFileExtensions = "Code files (*.c;*.cpp;*.cxx;*.cc;*.cs;*.tli;*.tlh;*.h;*.hh;*.hpp;*.hxx;*.java;*.js)|*.c;*.cpp;*.cxx;*.cc;*.cs;*.tli;*.tlh;*.h;*.hh;*.hpp;*.hxx;*.java;*.js";
    public const string ClangFormatExtension = "Configuration files (.clang-format)|*.clang-format";
    public const string BoldFontWeight = "Bold";
    public const string NormalFontWeight = "Normal";
    public const string DiffPerfectMatchFound = "// Clang Power Tools has found a perfect diff match for this file.\r\n//\r\n// Generate your Clang-Format file and start using it in your projects.";

    public const string DetectingTitle = "Detecting Clang-Format Style";
    public const string DetectingDescription = "Process will take some time depending on file size (300 KB ~2 minutes)";
    public const string DetectingDescriptionExtra = "Varied code samples may result in detecting more format options";

    public const string UpdateTitle = "Updating Format Preview";
    public const string UpdateDescription = "The modified Format Options are being applied to the Format Preview";

    public const string ResetTitle = "Reseting Format Options";
    public const string ResetDescription = "Format Options are being reset to their initial detected values";

    public const string FontFamily = "Consolas";
    public const int FontSize = 12;
    public const string ClangFormatExe = "clang-format.exe";
    public const string ClangFormat = ".clang-format";
    public const string FormatTemp = "FormatTemp.cpp";
    public const string FormatDirectory = "Format";

    public const string ClangFormatEditor = "ClangFormatEditor";
  }
}
