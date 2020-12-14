using System;
using System.Threading;

namespace ClangFormatDetector
{
  public class InputDelayer
  {
    #region Members

    public event EventHandler Idled = delegate { };
    public int WaitingMilliSeconds { get; set; }

    private readonly Timer waitingTimer;

    #endregion

    #region Methods

    public InputDelayer(int waitingMilliSeconds = 600)
    {
      WaitingMilliSeconds = waitingMilliSeconds;
      waitingTimer = new Timer(p =>
      {
        Idled(this, EventArgs.Empty);
      });
    }

    public void TextChanged()
    {
      waitingTimer.Change(WaitingMilliSeconds, Timeout.Infinite);
    }

    #endregion
  }
}
