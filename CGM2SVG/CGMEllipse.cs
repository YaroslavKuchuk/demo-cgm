using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMEllipse
  {
    // Fields
    public Point cd1;
    public Point cd2;
    public Point center;

    // Methods
    public string GetPath(SVGContext context)
    {
      this.center = context.fxy(this.center);
      this.cd1 = context.fxy(this.cd1);
      this.cd2 = context.fxy(this.cd2);
      double introduced6 = Convert.ToDouble(decimal.Subtract(this.cd1.X, this.center.X));
      double num2 = Math2.Distance(introduced6, Convert.ToDouble(decimal.Subtract(this.cd1.Y, this.center.Y)));
      double introduced7 = Convert.ToDouble(decimal.Subtract(this.cd2.X, this.center.X));
      double num3 = Math2.Distance(introduced7, Convert.ToDouble(decimal.Subtract(this.cd2.Y, this.center.Y)));
      decimal introduced8 = decimal.Subtract(this.cd1.Y, this.center.Y);
      double num = Math.Atan(Convert.ToDouble(decimal.Divide(introduced8, decimal.Subtract(this.cd1.X, this.center.X)))) * 57.295779513082323;
      string str = " M" + (Convert.ToDouble(this.center.X) - num2).ToString() + "," + this.center.Y.ToString();
      str = ((str + " A" + num2.ToString() + "," + num3.ToString()) + " 0 0 1 ") + (Convert.ToDouble(this.center.X) + num2).ToString() + "," + this.center.Y.ToString();
      str = str + " M" + (Convert.ToDouble(this.center.X) + num2).ToString() + "," + this.center.Y.ToString();
      str = (((str + " A" + num2.ToString() + "," + num3.ToString()) + " 0 0 1 ") + (Convert.ToDouble(this.center.X) - num2).ToString() + "," + this.center.Y.ToString()) + "Z";
      context.newregion = true;
      return str;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      this.center = context.fxy(this.center);
      this.cd1 = context.fxy(this.cd1);
      this.cd2 = context.fxy(this.cd2);
      double introduced4 = Convert.ToDouble(decimal.Subtract(this.cd1.X, this.center.X));
      double num2 = Math2.Distance(introduced4, Convert.ToDouble(decimal.Subtract(this.cd1.Y, this.center.Y)));
      double introduced5 = Convert.ToDouble(decimal.Subtract(this.cd2.X, this.center.X));
      double num3 = Math2.Distance(introduced5, Convert.ToDouble(decimal.Subtract(this.cd2.Y, this.center.Y)));
      decimal introduced6 = decimal.Subtract(this.cd1.Y, this.center.Y);
      double num = Math.Atan(Convert.ToDouble(decimal.Divide(introduced6, decimal.Subtract(this.cd1.X, this.center.X)))) * 57.295779513082323;
      doc.WriteStartElement("ellipse");
      doc.WriteAttributeString("cx", this.center.X.ToString());
      doc.WriteAttributeString("cy", this.center.Y.ToString());
      doc.WriteAttributeString("rx", num2.ToString());
      doc.WriteAttributeString("ry", num3.ToString());
      doc.WriteAttributeString("transform", "rotate(" + num.ToString() + " " + this.center.X.ToString() + " " + this.center.Y.ToString() + ")");
      context.PrintEdge(doc);
      doc.WriteAttributeString("fill", context.fill);
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID))//if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }



}
