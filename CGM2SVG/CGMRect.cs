using CGM.Scanner;
using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMRect
  {
    // Fields
    public Point bottomright;
    public Point topleft;

    // Methods
    public string GetPath(SVGContext context)
    {
      this.topleft = context.fxy(this.topleft);
      this.bottomright = context.fxy(this.bottomright);
      if (decimal.Compare(this.bottomright.Y, this.topleft.Y) < 0)
      {
        int num = Convert.ToInt32(this.topleft.Y);
        this.topleft.Y = this.bottomright.Y;
        this.bottomright.Y = new decimal(num);
      }
      string str = " M" + this.topleft.X.ToString() + "," + this.topleft.Y.ToString();
      str = str + " L" + this.bottomright.X.ToString() + "," + this.topleft.Y.ToString();
      str = str + " L" + this.bottomright.X.ToString() + "," + this.bottomright.Y.ToString();
      str = (str + " L" + this.topleft.X.ToString() + "," + this.bottomright.Y.ToString()) + " Z ";
      context.newregion = true;
      return str;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      this.topleft = context.fxy(this.topleft);
      this.bottomright = context.fxy(this.bottomright);
      if (decimal.Compare(this.bottomright.Y, this.topleft.Y) < 0)
      {
        int num = Convert.ToInt32(this.topleft.Y);
        this.topleft.Y = this.bottomright.Y;
        this.bottomright.Y = new decimal(num);
      }
      doc.WriteStartElement("rect");
      doc.WriteAttributeString("x", this.topleft.X.ToString());
      doc.WriteAttributeString("y", this.topleft.Y.ToString());
      doc.WriteAttributeString("width", Math.Abs(decimal.Subtract(this.bottomright.X, this.topleft.X)).ToString());
      doc.WriteAttributeString("height", Math.Abs(decimal.Subtract(this.bottomright.Y, this.topleft.Y)).ToString());
      context.PrintEdge(doc);
      doc.WriteAttributeString("fill", context.fill);
      //if (context.isClip & (StringType.StrCmp(context.CurrClipID, "", false) != 0))
      if (context.isClip & context.CurrClipID != "")
      {
        doc.WriteAttributeString("clip-path", "url(#" + context.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }
  }

}
