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
}
