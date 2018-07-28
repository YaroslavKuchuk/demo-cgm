using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMPolyLine
  {
    // Fields
    public Point[] points;

    // Methods
    public string GetPath(SVGContext context)
    {
      string[] strArray = new string[points.Length];//new Array.CreateInstance(typeof(string), this.points.Length);
      int num2 = strArray.Length - 1;
      for (int i = 0; i <= num2; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      string str2 = string.Join("L", strArray);
      return ("M" + str2);
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      string str = "none";
      if ((context.interiorstyle == 3) & context.fingureOn)
      {
        context.getHatch(doc);
        str = "url(#" + context.HatchIndex.ToString() + ")";
      }
      doc.WriteStartElement("path");
      doc.WriteAttributeString("d", this.GetPath(context));
      doc.WriteAttributeString("fill", str);
      context.PrintLine(doc);
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID))//if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }

}
