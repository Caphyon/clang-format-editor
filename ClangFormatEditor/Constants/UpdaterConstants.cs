namespace ClangFormatEditor.Update
{
  public static class UpdaterConstants
  {
    public const string Path = @"C:\Program Files (x86)\Caphyon\Clang Format Editor";
    public const string Executable = "Clang Format Editor Updater.exe";
    public const string CheckUpdateParamaters = "/justcheck";
    public const string StartUpdateParameters = "/checknow -minuseractions";
    public const string Cmd = "cmd.exe";
    public const string VisualStudio = "VisualStudio";
    public const string CommandParamater = "/C ";
    public const string ClangFormatUpdater = "Clang-Format Updater";
    public const int NoUpdateReturnCode = -536870895;
    public const int UpdateFoundReturnCode = 0;
  }
}
