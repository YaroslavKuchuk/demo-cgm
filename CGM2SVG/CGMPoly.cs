using CGM.Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMPoly
  {
    // Fields
    public Point[] points;

    // Methods
    public string GetPath(SVGContext context)
    {
      string[] strArray = new string[points.Length]; //Array.CreateInstance(typeof(string), this.points.Length);
      int num2 = strArray.Length - 1;
      for (int i = 0; i <= num2; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      string str2 = string.Join(" L", strArray);
      return ("M" + str2 + " Z");
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      string[] strArray = new string[(this.points.Length - 1) + 1];
      int num2 = strArray.Length - 1;
      for (int i = 0; i <= num2; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      if (context.interiorstyle == 3)
      {
        context.getHatch(doc);
      }
      doc.WriteStartElement("polygon");
      doc.WriteAttributeString("points", string.Join(" ", strArray));
      doc.WriteAttributeString("fill", context.fill);
      doc.WriteAttributeString("fill-rule", "evenodd");
      context.PrintEdge(doc);
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID)) //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }

    public void UpdateSVGfFigure(XmlTextWriter doc, SVGContext context)
    {
      string[] strArray = new string[points.Length]; //Array.CreateInstance(typeof(string), this.points.Length);
      int num2 = strArray.Length - 1;
      for (int i = 0; i <= num2; i++)
      {
        strArray[i] = context.fx(Convert.ToDouble(this.points[i].X)).ToString() + "," + context.fy(Convert.ToDouble(this.points[i].Y)).ToString();
      }
      if (context.interiorstyle == 3)
      {
        context.getHatch(doc);
      }
      doc.WriteStartElement("polygon");
      doc.WriteAttributeString("points", string.Join(" ", strArray));
      doc.WriteAttributeString("fill", context.fill);
      if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID)) //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }



}
