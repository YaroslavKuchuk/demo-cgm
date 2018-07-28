using CGM.Scanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CGM2SVG
{
  [StructLayout(LayoutKind.Sequential)]
  public struct PointDbl
  {
    public double X;
    public double Y;
    public PointDbl(double X, double Y)
    {
      this = new PointDbl();
      this.X = X;
      this.Y = Y;
    }

    public PointDbl(CGM.Scanner.Point p)
    {
      this = new PointDbl();
      this.X = Convert.ToDouble(p.X);
      this.Y = Convert.ToDouble(p.Y);
    }

    public override string ToString()
    {
      return string.Format("({0},{1})", this.X, this.Y);
    }

    public override bool Equals(object obj)
    {
      if (obj is CGM.Scanner.Point)
      {
        PointF tf2 = (PointF)obj;
        if (Math2.IsNul(this.X - tf2.X))
        {
          PointF tf = (PointF)obj;
          if (Math2.IsNul(this.Y - tf.Y))
          {
            return true;
          }
        }
        return false;
      }
      if (!(obj is PointDbl))
      {
        throw new InvalidCastException();
      }
      PointDbl dbl2 = (PointDbl)obj;
      if (Math2.IsNul(this.X - dbl2.X))
      {
        PointDbl dbl = (PointDbl)obj;
        if (Math2.IsNul(this.Y - dbl.Y))
        {
          return true;
        }
      }
      return false;
    }
  }



}
