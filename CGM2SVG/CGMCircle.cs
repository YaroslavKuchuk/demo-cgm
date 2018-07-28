using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMCircle
  {
    // Fields
    public Point center;
    public int radius;

    // Methods
    public string GetPath(SVGContext context)
    {
      this.center = context.fxy(this.center);
      double num = context.fscale((double)this.radius);
      string str = " M" + (Convert.ToDouble(this.center.X) - num).ToString() + "," + this.center.Y.ToString();
      str = ((str + " A" + this.radius.ToString() + "," + this.radius.ToString()) + " 0 1 0 ") + (Convert.ToDouble(this.center.X) + num).ToString() + "," + this.center.Y.ToString();
      str = str + " M" + (Convert.ToDouble(this.center.X) + num).ToString() + "," + this.center.Y.ToString();
      str = (((str + " A" + this.radius.ToString() + "," + this.radius.ToString()) + " 0 0 0 ") + (Convert.ToDouble(this.center.X) - num).ToString() + "," + this.center.Y.ToString()) + "Z";
      context.newregion = true;
      return str;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      this.center = context.fxy(this.center);
      doc.WriteStartElement("circle");
      doc.WriteAttributeString("cx", this.center.X.ToString());
      doc.WriteAttributeString("cy", this.center.Y.ToString());
      doc.WriteAttributeString("r", context.fscale((double)this.radius).ToString());
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
