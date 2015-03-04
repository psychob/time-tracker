namespace timetracker
{
 public class WinAPI
 {
  [System.Runtime.InteropServices.DllImport("kernel32.dll")]
  public static extern ulong GetTickCount64();
 }
}