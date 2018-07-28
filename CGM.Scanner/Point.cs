using System.Runtime.InteropServices;

namespace CGM.Scanner
{
  [StructLayout(LayoutKind.Sequential)]
  public struct Point
  {
    public decimal X;
    public decimal Y;
    public Point(decimal X, decimal Y)
    {
      this = new Point();
      this.X = X;
      this.Y = Y;
    }

    public bool IsEmpty()
    {
      return (this.X == null && this.Y == null);
    }
  }



}
