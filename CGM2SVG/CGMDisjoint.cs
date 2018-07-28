using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMDisjoint
  {
    // Fields
    public Point[] points;

    // Methods
    public string GetPath(SVGContext context)
    {
      string str2 = String.Empty;
      string[] strArray = new string[points.Length]; //Array.CreateInstance(typeof(string), this.points.Length);
      int num4 = strArray.Length - 1;
      for (int i = 0; i <= num4; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      int num3 = strArray.Length;
      for (int j = 1; j <= num3; j += 2)
      {
        str2 = str2 + "M" + strArray[j - 1] + " L" + strArray[j] + " ";
      }
      return str2;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      string str = String.Empty;
      string[] strArray = new string[points.Length];//Array.CreateInstance(typeof(string), this.points.Length);
      int num4 = strArray.Length - 1;
      for (int i = 0; i <= num4; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      int num3 = strArray.Length;
      for (int j = 1; j <= num3; j += 2)
      {
        str = str + "M" + strArray[j - 1] + " L" + strArray[j] + " ";
      }
      doc.WriteStartElement("path");
      doc.WriteAttributeString("d", str);
      doc.WriteAttributeString("fill", "none");
      context.PrintLine(doc);
      doc.WriteEndElement();
    }
  }
}
