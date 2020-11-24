using System;
using System.Runtime.Serialization;

namespace ClangFormatDetector.Enums
{
  [Serializable]
  public enum ToggleValues
  {
    [EnumMember(Value = "true")]
    True,
    [EnumMember(Value = "false")]
    False
  }

  public enum FormatStyle
  {
    Custom,
    LLVM,
    Google,
    Chromium,
    Microsoft,
    Mozilla,
    WebKit,
  }
}
