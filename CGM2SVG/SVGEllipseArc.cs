using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM2SVG
{
  internal class SVGEllipseArc
  {
    // Fields
    public double AngleX;
    public PointDbl CenterP;
    public PointDbl EndP;
    public bool LargeArc;
    public double RadiusX;
    public double RadiusY;
    public PointDbl StartP;
    public bool SweepFlag;
    public double[,] Transform;

    // Properties
    public string dAttribute {
      get {
        if (Math2.IsNul(Math2.Distance(this.StartP.X, this.StartP.Y, this.EndP.X, this.EndP.Y)))
        {
          PointDbl dbl = new PointDbl((2.0 * this.StartP.X) - this.CenterP.X, (2.0 * this.StartP.Y) - this.CenterP.Y);
          return string.Format("M{0},{1} A{4},{5} {6} 1,{8} {9},{10} A{4},{5} {6} 0,{8} {2},{3}", new object[] { this.StartP.X, this.StartP.Y, this.EndP.X, this.EndP.Y, this.RadiusX, this.RadiusY, (this.AngleX / 3.1415926535897931) * 180.0, false , false , dbl.X, dbl.Y });
        }
        return string.Format("M{0},{1} A{4},{5} {6} {7},{8} {2},{3}", new object[] { this.StartP.X, this.StartP.Y, this.EndP.X, this.EndP.Y, this.RadiusX, this.RadiusY, (this.AngleX / 3.1415926535897931) * 180.0, false,  });
      }
    }

    public string transformAttribute {
      get {
        if (this.Transform == null)
        {
          return string.Empty;
        }
        return string.Format("matrix({0},{1},{2},{3},{4},{5})", new object[] { this.Transform[0, 0], this.Transform[1, 0], this.Transform[0, 1], this.Transform[1, 1], this.Transform[0, 2], this.Transform[1, 2] });
      }
    }
  }



}
