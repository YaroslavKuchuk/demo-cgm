using System;
using System.Runtime.InteropServices;

namespace CGM2SVG
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CircleInfo
  {
    public PointDbl Center;
    public double R;
  }


  internal sealed class CGM2SVGMath
  {
    // Methods
    public static CircleInfo FindCircleBy3Points(PointDbl[] p)
    {
      CircleInfo info = new CircleInfo();//TODO
      if (Math2.IsNul(Math2.CompareAngles(p[0].X - p[1].X, p[0].Y - p[1].Y, p[2].X - p[1].X, p[2].Y - p[1].Y)))
      {
        info.R = -1.0;
        return info;
      }
      double[] numArray = Math2.SolveSystem2(new double[,] { { 2.0 * (p[0].X - p[2].X), 2.0 * (p[0].Y - p[2].Y) }, { 2.0 * (p[1].X - p[2].X), 2.0 * (p[1].Y - p[2].Y) } }, new double[] { (((p[0].X * p[0].X) - (p[2].X * p[2].X)) + (p[0].Y * p[0].Y)) - (p[2].Y * p[2].Y), (((p[1].X * p[1].X) - (p[2].X * p[2].X)) + (p[1].Y * p[1].Y)) - (p[2].Y * p[2].Y) });
      info.Center.X = numArray[0];
      info.Center.Y = numArray[1];
      info.R = Math2.Distance(p[0].X - numArray[0], p[0].Y - numArray[1]);
      return info;
    }

    public static double[,] FindEllipseTransformToHorizontal(PointDbl p, PointDbl p1, PointDbl p2)
    {
      double num = Math2.Distance(p1.X - p.X, p1.Y - p.Y);
      double num2 = Math2.Distance(p2.X - p.X, p2.Y - p.Y);
      return new double[,] { { ((p2.X - p.X) / num2), ((p1.X - p.X) / num), p.X }, { ((p2.Y - p.Y) / num2), ((p1.Y - p.Y) / num), p.Y }, { 0.0, 0.0, 1.0 } };
    }

    public static SVGEllipseArc FindSVGArc(PointDbl p, PointDbl p1, PointDbl p2, PointDbl Δstart, PointDbl Δend)
    {
      SVGEllipseArc arc2 = new SVGEllipseArc();
      arc2.RadiusX = Math2.Distance(p.X, p.Y, p2.X, p2.Y);
      arc2.RadiusY = Math2.Distance(p.X, p.Y, p1.X, p1.Y);
      double[,] a = FindEllipseTransformToHorizontal(p, p1, p2);
      double[,] numArray2 = SpecialInvertMatrix(a);
      PointDbl dbl3 = new PointDbl(Δstart.X + p.X, Δstart.Y + p.Y);
      PointDbl dbl2 = TransformPoint(numArray2, dbl3);
      double introduced10 = Math2.Sqr(dbl2.X / arc2.RadiusX);
      double num = Math.Sqrt(introduced10 + Math2.Sqr(dbl2.Y / arc2.RadiusY));
      dbl2.X /= num;
      dbl2.Y /= num;
      dbl3 = new PointDbl(Δend.X + p.X, Δend.Y + p.Y);
      PointDbl dbl = TransformPoint(numArray2, dbl3);
      double introduced11 = Math2.Sqr(dbl.X / arc2.RadiusX);
      double num2 = Math.Sqrt(introduced11 + Math2.Sqr(dbl.Y / arc2.RadiusY));
      dbl.X /= num2;
      dbl.Y /= num2;
      if (Math2.Is90DegIntersection(p1.X - p.X, p1.Y - p.Y, p2.X - p.X, p2.Y - p.Y))
      {
        arc2.AngleX = Math.Atan2(a[1, 0], a[0, 0]);
        arc2.StartP = TransformPoint(a, dbl2);
        arc2.EndP = TransformPoint(a, dbl);
        arc2.CenterP = p;
        arc2.SweepFlag = Math2.CompareAngles(p1.X - p.X, p1.Y - p.Y, p2.X - p.X, p2.Y - p.Y) < 0.0;
        arc2.LargeArc = Math2.CompareAngles(dbl2.X, dbl2.Y, dbl.X, dbl.Y) < 0.0;
        return arc2;
      }
      arc2.Transform = a;
      arc2.AngleX = 0.0;
      arc2.StartP = dbl2;
      arc2.EndP = dbl;
      dbl3 = new PointDbl(0.0, 0.0);
      arc2.CenterP = dbl3;
      arc2.SweepFlag = false;
      arc2.LargeArc = Math2.CompareAngles(dbl2.X, dbl2.Y, dbl.X, dbl.Y) < 0.0;
      return arc2;
    }

    private static double[,] SpecialInvertMatrix(double[,] A)
    {
      double num = (A[0, 0] * A[1, 1]) - (A[0, 1] * A[1, 0]);
      return new double[,] { { (A[1, 1] / num), (-A[0, 1] / num), (((A[0, 1] * A[1, 2]) - (A[1, 1] * A[0, 2])) / num) }, { (-A[1, 0] / num), (A[0, 0] / num), (((-A[0, 0] * A[1, 2]) + (A[1, 0] * A[0, 2])) / num) }, { 0.0, 0.0, 1.0 } };
    }

    private static PointDbl TransformPoint(double[,] A, PointDbl p)
    {
      PointDbl dbl;
      double[] numArray = Math2.MultiplyMatrix(A, new double[] { p.X, p.Y, 1.0 });
      dbl.X = numArray[0];
      dbl.Y = numArray[1];
      return dbl;
    }
  }



}
