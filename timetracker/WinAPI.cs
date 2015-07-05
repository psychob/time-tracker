namespace timetracker
{
 public class WinAPI
 {
  public const int WM_QUERYENDSESSION = 0x0011;
  public const int WM_ENDSESSION = 0x0016;

  [System.Runtime.InteropServices.DllImport("kernel32.dll")]
  public static extern ulong GetTickCount64();
 }
}