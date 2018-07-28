using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMArc
  {
    // Fields
    public Point cd1;
    public Point cd2;
    public Point center;
    public int closingType = -1;
    public bool collinearCoords = false;
    public Point v1;
    public Point v2;

    // Methods
    private Point context_fxy2(Point p, SVGContext context)
    {
      int inversion = context.GetInversion();
      if ((inversion == 3) | (inversion == 4))
      {
        p.X = decimal.Negate(p.X);
      }
      if ((inversion == 1) | (inversion == 3))
      {
        p.Y = decimal.Negate(p.Y);
      }
      return p;
    }

    public string GetPath(SVGContext context)
    {
      PointDbl p = new PointDbl(context.fxy(this.center));
      PointDbl dbl4 = new PointDbl(context.fxy(this.cd1));
      PointDbl dbl3 = new PointDbl(context.fxy(this.cd2));
      PointDbl dbl2 = new PointDbl(this.context_fxy2(this.v1, context));
      PointDbl dbl = new PointDbl(this.context_fxy2(this.v2, context));
      SVGEllipseArc arc = CGM2SVGMath.FindSVGArc(p, dbl4, dbl3, dbl2, dbl);
      string dAttribute = arc.dAttribute;
      string transformAttribute = arc.transformAttribute;
      if ((arc.StartP.X == arc.EndP.X) & (arc.StartP.Y == arc.EndP.Y))
      {
        SVGEllipseArc arc2 = arc;
        arc2.EndP.Y++;
        dAttribute = arc.dAttribute;
      }
      switch (this.closingType)
      {
        case 0:
          return (dAttribute + string.Format(" L{0},{1} L{2},{3} Z", new object[] { arc.CenterP.X, arc.CenterP.Y, arc.StartP.X, arc.StartP.Y }));

        case 1:
          return (dAttribute + string.Format(" L{0},{1} Z", arc.StartP.X, arc.StartP.Y));
      }
      return dAttribute;
    }

    public void SetCircle3Points(Point[] p)
    {
      PointDbl[] dblArray = new PointDbl[3];
      PointDbl dbl3 = new PointDbl(p[0]);
      dblArray[0] = dbl3;
      PointDbl dbl2 = new PointDbl(p[1]);
      dblArray[1] = dbl2;
      PointDbl dbl = new PointDbl(p[2]);
      dblArray[2] = dbl;
      CircleInfo info = CGM2SVGMath.FindCircleBy3Points(dblArray);
      if (info.R >= 0.0)
      {
        this.center.X = new decimal(info.Center.X);
        this.center.Y = new decimal(info.Center.Y);
        this.SetCircleDiameters(info.R, Math2.CompareAngles(Convert.ToDouble(decimal.Subtract(p[2].X, p[0].X)), Convert.ToDouble(decimal.Subtract(p[2].Y, p[0].Y)), Convert.ToDouble(decimal.Subtract(p[1].X, p[0].X)), Convert.ToDouble(decimal.Subtract(p[1].Y, p[0].Y))) < 0.0);
        this.v1.X = decimal.Subtract(p[0].X, this.center.X);
        this.v1.Y = decimal.Subtract(p[0].Y, this.center.Y);
        this.v2.X = decimal.Subtract(p[2].X, this.center.X);
        this.v2.Y = decimal.Subtract(p[2].Y, this.center.Y);
      }
      else
      {
        this.collinearCoords = true;
        this.center = p[1];
        this.SetCircleDiameters(Math2.Distance(Convert.ToDouble(decimal.Subtract(p[0].X, this.center.X)), Convert.ToDouble(decimal.Subtract(p[2].X, this.center.X))), false);
        this.v1.X = decimal.Subtract(p[0].X, this.center.X);
        this.v1.Y = decimal.Subtract(p[0].Y, this.center.Y);
        this.v2.X = decimal.Subtract(p[2].X, this.center.X);
        this.v2.Y = decimal.Subtract(p[2].Y, this.center.Y);
      }
    }

    public void SetCircleDiameters(double r, bool backward)
    {
      this.cd1.X = this.center.X;
      this.cd1.Y = new decimal(Convert.ToDouble(this.center.Y) - r);
      if (backward)
      {
        this.cd2.X = new decimal(Convert.ToDouble(this.center.X) - r);
      }
      else
      {
        this.cd2.X = new decimal(Convert.ToDouble(this.center.X) + r);
      }
      this.cd2.Y = this.center.Y;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      PointDbl dbl6 = new PointDbl(context.fxy(this.center));
      PointDbl p = dbl6;
      dbl6 = new PointDbl(context.fxy(this.cd1));
      PointDbl dbl2 = dbl6;
      dbl6 = new PointDbl(context.fxy(this.cd2));
      PointDbl dbl3 = dbl6;
      dbl6 = new PointDbl(this.context_fxy2(this.v1, context));
      PointDbl dbl4 = dbl6;
      dbl6 = new PointDbl(this.context_fxy2(this.v2, context));
      PointDbl dbl5 = dbl6;
      SVGEllipseArc arc = CGM2SVGMath.FindSVGArc(p, dbl2, dbl3, dbl4, dbl5);
      string dAttribute = arc.dAttribute;
      string transformAttribute = arc.transformAttribute;
      switch (this.closingType)
      {
        case 0:
          dAttribute = dAttribute + string.Format(" L{0},{1} L{2},{3} Z", new object[] { arc.CenterP.X, arc.CenterP.Y, arc.StartP.X, arc.StartP.Y });
          break;

        case 1:
          dAttribute = dAttribute + string.Format(" L{0},{1} Z", arc.StartP.X, arc.StartP.Y);
          break;
      }
      doc.WriteStartElement("path");
      doc.WriteAttributeString("d", dAttribute);
      if (!String.IsNullOrEmpty(transformAttribute)) //if (StringType.StrCmp(transformAttribute, "", false) != 0)
      {
        doc.WriteAttributeString("transform", transformAttribute);
      }
      if (this.closingType >= 0)
      {
        context.PrintEdgeArc(doc);
        doc.WriteAttributeString("fill", context.fill);
      }
      else
      {
        context.PrintLine(doc);
        doc.WriteAttributeString("fill", "none");
      }
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID)) //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }



}
