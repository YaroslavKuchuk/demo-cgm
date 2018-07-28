using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMPolySet
  {
    // Fields
    public int[] flags;
    public string path = "";
    public Point[] points;
    public string strokepath = "";

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
      bool flag = true;
      string str = "";
      string sLeft = "M";
      string str5 = "M";
      int num3 = this.points.Length - 1;
      for (int i = 0; i <= num3; i++)
      {
        if (flag)
        {
          str5 = sLeft;
          if (sLeft == "L" && !flag)
          {
            str5 = "M";
          }
          this.strokepath = this.strokepath + string.Format("{0}{1}{2},{3}", new object[] { str, str5, context.fx(Convert.ToDouble(this.points[i].X)), context.fy(Convert.ToDouble(this.points[i].Y)) });
        }
        this.path = this.path + string.Format("{0}{1}{2},{3}", new object[] { str, sLeft, context.fx(Convert.ToDouble(this.points[i].X)), context.fy(Convert.ToDouble(this.points[i].Y)) });
        switch (this.flags[i])
        {
          case 0:
            str = "";
            sLeft = "L";
            flag = false;
            break;

          case 1:
            str = "";
            sLeft = "L";
            flag = true;
            break;

          case 2:
            str = "Z";
            sLeft = "M";
            flag = false;
            break;

          case 3:
            str = "Z";
            sLeft = "M";
            flag = true;
            break;
        }
      }
      this.path = this.path + "Z";
      if (context.interiorstyle == 3)
      {
        context.getHatch(doc);
      }
      doc.WriteStartElement("g");
          if (context.isClip & !String.IsNullOrEmpty(context.CurrClipID)) //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
          {
            doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
          }
          doc.WriteStartElement("path");
              doc.WriteAttributeString("d", this.path);
              doc.WriteAttributeString("fill", context.fill);
              doc.WriteAttributeString("fill-rule", "evenodd");
              doc.WriteAttributeString("stroke", "none");
          doc.WriteEndElement();
        doc.WriteStartElement("path");
          doc.WriteAttributeString("d", this.strokepath);
          doc.WriteAttributeString("fill", "none");
          context.PrintEdge(doc);
        doc.WriteEndElement();
      doc.WriteEndElement();
    }
  }
}
