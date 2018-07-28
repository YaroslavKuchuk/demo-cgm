using System;
using System.Runtime.InteropServices;

namespace CGM2SVG
{
  internal class Ellipse
  {
    // Fields
    private pointdbl A;
    private pointdbl B;
    private double deltatheta;
    public double fA;
    public double fS;
    private pointdbl Rx;
    private pointdbl Ry;
    private double theta1;
    private double theta2;
    public double weirdangle;
    private pointdbl X;
    public double x1;
    public double x2;
    public double y1;
    public double y2;

    // Methods
    public Ellipse()
    {
      this.x1 = 0.0;
      this.y1 = 0.0;
      this.x2 = 0.0;
      this.y2 = 0.0;
    }

    public Ellipse(double centerpointdblx, double centerpointdbly, double firstCDPx, double firstCDPy, double secondCDPx, double secondCDPy, double DX_start, double DY_start, double DX_end, double DY_end)
    {
      Ellipse.pointdbl pointdbl = new Ellipse.pointdbl();
      this.B = pointdbl;
      pointdbl = new Ellipse.pointdbl();
      this.Rx = pointdbl;
      pointdbl = new Ellipse.pointdbl();
      this.Ry = pointdbl;
      this.X = new Ellipse.pointdbl();
      this.A.X = DX_start - centerpointdblx;
      this.A.Y = DY_start - centerpointdbly;
      this.B.X = DX_end - centerpointdblx;
      this.B.Y = DY_end - centerpointdbly;
      this.Rx.X = firstCDPx - centerpointdblx;
      this.Rx.Y = firstCDPy - centerpointdbly;
      this.Ry.X = firstCDPx - centerpointdblx;
      this.Ry.Y = firstCDPy - centerpointdbly;
      this.X.X = 1.0;
      this.X.Y = 0.0;
      this.theta1 = Math.Acos(this.DotProduct(this.A, this.Rx) / (this.Magnitude(this.A) * this.Magnitude(this.Rx)));
      this.theta1 *= this.GetSign(this.A, this.Rx);
      if (this.theta1 < 0.0)
      {
        this.theta1 = (this.theta1 + 3.1415926535897931) + 3.1415926535897931;
      }
      this.theta2 = Math.Acos(this.DotProduct(this.B, this.Rx) / (this.Magnitude(this.B) * this.Magnitude(this.Rx)));
      this.theta2 *= this.GetSign(this.B, this.Rx);
      if (this.theta2 < 0.0)
      {
        this.theta2 = (this.theta2 + 3.1415926535897931) + 3.1415926535897931;
      }
      this.weirdangle = Math.Acos(this.DotProduct(this.Rx, this.X) / (this.Magnitude(this.Rx) * this.Magnitude(this.X)));
      this.weirdangle *= this.GetSign(this.Rx, this.X);
      if (this.weirdangle < 0.0)
      {
        this.weirdangle = (this.weirdangle + 3.1415926535897931) + 3.1415926535897931;
      }
      this.deltatheta = this.theta2 - this.theta1;
      this.x1 = (((Math.Cos(this.weirdangle) * this.Magnitude(this.Rx)) * Math.Cos(this.theta1)) + ((-Math.Sin(this.weirdangle) * this.Magnitude(this.Ry)) * Math.Sin(this.theta1))) + centerpointdblx;
      this.y1 = (((Math.Sin(this.weirdangle) * this.Magnitude(this.Rx)) * Math.Cos(this.theta1)) + ((Math.Cos(this.weirdangle) * this.Magnitude(this.Ry)) * Math.Sin(this.theta1))) + centerpointdbly;
      this.x2 = (((Math.Cos(this.weirdangle) * this.Magnitude(this.Rx)) * Math.Cos(this.theta2)) + ((-Math.Sin(this.weirdangle) * this.Magnitude(this.Ry)) * Math.Sin(this.theta2))) + centerpointdblx;
      this.y2 = (((Math.Sin(this.weirdangle) * this.Magnitude(this.Rx)) * Math.Cos(this.theta2)) + ((Math.Cos(this.weirdangle) * this.Magnitude(this.Ry)) * Math.Sin(this.theta2))) + centerpointdbly;
      if (Math.Abs(this.deltatheta) > 180.0)
      {
        this.fA = 1.0;
      }
      else
      {
        this.fA = 0.0;
      }
      if (this.deltatheta > 0.0)
      {
        this.fS = 1.0;
      }
      else
      {
        this.fS = 0.0;
      }
    }

    private double DotProduct(pointdbl vector1, pointdbl vector2)
    {
      return ((vector1.X * vector2.X) + (vector1.Y * vector2.Y));
    }

    private double GetSign(pointdbl vector1, pointdbl vector2)
    {
      if (((vector1.X * vector2.Y) - (vector1.Y * vector2.X)) >= 0.0)
      {
        return -1.0;
      }
      return 1.0;
    }

    private double Magnitude(pointdbl vector)
    {
      return Math.Sqrt(Math.Pow(vector.X, 2.0) + Math.Pow(vector.Y, 2.0));
    }

    // Nested Types
    [StructLayout(LayoutKind.Sequential)]
    public struct pointdbl
    {
      public double X;
      public double Y;
    }
  }



}
