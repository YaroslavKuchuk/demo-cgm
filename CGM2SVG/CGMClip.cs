using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMClip
  {
    // Fields
    public string ClipID;
    public static int ClipNum = 0;
    public Point ClipPoint1;
    public Point ClipPoint2;

    // Methods
    public static string GenerateID()
    {
      ClipNum++;
      return ("Clip" + ClipNum.ToString());
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      this.ClipID = GenerateID();
      doc.WriteStartElement("clipPath");
      doc.WriteAttributeString("id", this.ClipID);
      this.ClipPoint1 = context.fxy(this.ClipPoint1);
      this.ClipPoint2 = context.fxy(this.ClipPoint2);
      if (decimal.Compare(this.ClipPoint2.Y, this.ClipPoint1.Y) < 0)
      {
        int num = Convert.ToInt32(this.ClipPoint1.Y);
        this.ClipPoint1.Y = this.ClipPoint2.Y;
        this.ClipPoint2.Y = new decimal(num);
      }
      doc.WriteStartElement("rect");
      doc.WriteAttributeString("x", this.ClipPoint1.X.ToString());
      doc.WriteAttributeString("y", this.ClipPoint1.Y.ToString());
      doc.WriteAttributeString("width",  Math.Abs(decimal.Subtract(this.ClipPoint2.X, this.ClipPoint1.X)).ToString());
      doc.WriteAttributeString("height", Math.Abs(decimal.Subtract(this.ClipPoint2.Y, this.ClipPoint1.Y)).ToString());
      doc.WriteEndElement();
      doc.WriteEndElement();
    }
  }



}
