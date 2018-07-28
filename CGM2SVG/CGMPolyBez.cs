using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMPolyBez
  {
    // Fields
    public bool Continue;
    public Point[] Points;

    // Methods
    public string GetPath(SVGContext context)
    {
      string str2 = String.Empty;
      string[] strArray = new string[Points.Length]; //Array.CreateInstance(typeof(string), this.Points.Length);
      int num6 = strArray.Length - 1;
      for (int i = 0; i <= num6; i++)
      {
        strArray[i] = string.Format("{0:f2},{1:f2}", context.fx(Convert.ToDouble(this.Points[i].X)), context.fy(Convert.ToDouble(this.Points[i].Y)));
      }
      if (!this.Continue)
      {
        int num5 = strArray.Length - 1;
        for (int k = 0; k <= num5; k += 4)
        {
          str2 = str2 + string.Format("L{0} C{1} {2} {3}", new object[] { strArray[k], strArray[k + 1], strArray[k + 2], strArray[k + 3] });
        }
        return str2;
      }
      int num4 = strArray.Length - 3;
      for (int j = 1; j <= num4; j += 3)
      {
        str2 = str2 + string.Format("L{0} C{1} {2} {3}", new object[] { strArray[j - 1], strArray[j], strArray[j + 1], strArray[j + 2] });
      }
      return str2;
    }

    public string GetPath2(SVGContext context)
    {
      string str2 = String.Empty;
      string[] strArray = new string[Points.Length]; //Array.CreateInstance(typeof(string), this.Points.Length);
      int num6 = strArray.Length - 1;
      for (int i = 0; i <= num6; i++)
      {
        strArray[i] = string.Format("{0:f2},{1:f2}", context.fx(Convert.ToDouble(this.Points[i].X)), context.fy(Convert.ToDouble(this.Points[i].Y)));
      }
      if (!this.Continue)
      {
        int num5 = strArray.Length - 1;
        for (int k = 0; k <= num5; k += 4)
        {
          str2 = str2 + string.Format("M{0} C{1} {2} {3} ", new object[] { strArray[k], strArray[k + 1], strArray[k + 2], strArray[k + 3] });
        }
        return str2;
      }
      int num4 = strArray.Length - 3;
      for (int j = 1; j <= num4; j += 3)
      {
        str2 = str2 + string.Format("M{0} C{1} {2} {3} ", new object[] { strArray[j - 1], strArray[j], strArray[j + 1], strArray[j + 2] });
      }
      return str2;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      doc.WriteStartElement("path");
      doc.WriteAttributeString("d", this.GetPath2(context));
      doc.WriteAttributeString("fill", "none");
      context.PrintLine(doc);
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID)) //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }



}
