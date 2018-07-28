using System;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMFigure
  {
    // Fields
    public string MyFillPath = "";
    public string MyStrokePath = "";

    // Methods
    public void AddPath(string path, SVGContext mycontext)
    {
      if (mycontext.newregion | !String.IsNullOrEmpty(this.MyFillPath)) //if (mycontext.newregion | (StringType.StrCmp(this.MyFillPath, "", false) == 0))
      {
        if (mycontext.edgevis == 1)
        {
          this.MyStrokePath = this.MyStrokePath + path;
        }
        if (!String.IsNullOrEmpty(this.MyFillPath) && !this.MyFillPath.Trim().EndsWith("Z"))// if ((StringType.StrCmp(this.MyFillPath, "", false) != 0) && !this.MyFillPath.Trim().EndsWith("Z"))
        {
          this.MyFillPath = this.MyFillPath + "Z" + path;
        }
        else
        {
          this.MyFillPath = this.MyFillPath + path;
        }
      }
      else if (mycontext.connectingedge)
      {
        if (mycontext.edgevis == 1)
        {
          this.MyStrokePath = this.MyStrokePath + "L" + path.TrimStart(new char[0]).Substring(1);
        }
        this.MyFillPath = this.MyFillPath + "L" + path.TrimStart(new char[0]).Substring(1);
      }
      else if (!this.MyFillPath.TrimEnd(new char[0]).EndsWith("Z"))
      {
        this.MyFillPath = this.MyFillPath + "L" + path.TrimStart(new char[0]).Substring(1);
        this.MyStrokePath = this.MyStrokePath + "Z" + path;
      }
      else
      {
        this.MyFillPath = this.MyFillPath + path;
        this.MyStrokePath = this.MyStrokePath + path;
      }
      mycontext.newregion = false;
      mycontext.connectingedge = false;
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext context)
    {
      if (context.connectingedge)
      {
        this.MyStrokePath = this.MyStrokePath + " Z";
        context.connectingedge = false;
      }
      if (!this.MyFillPath.TrimEnd(new char[0]).EndsWith("Z"))
      {
        this.MyFillPath = this.MyFillPath + "Z";
      }
      if (!this.MyFillPath.TrimStart(new char[0]).StartsWith("M"))
      {
        this.MyFillPath = "M" + this.MyFillPath.TrimStart(new char[0]).Substring(1);
      }
      if (!String.IsNullOrEmpty(this.MyStrokePath) && !this.MyStrokePath.TrimStart(new char[0]).StartsWith("M"))//if ((StringType.StrCmp(this.MyStrokePath, "", false) != 0) && !this.MyStrokePath.TrimStart(new char[0]).StartsWith("M"))
      {
        this.MyStrokePath = "M" + this.MyStrokePath.TrimStart(new char[0]).Substring(1);
      }
      if (context.interiorstyle == 3)
      {
        context.getHatch(doc);
      }
      doc.WriteStartElement("g");
        doc.WriteStartElement("path");
            doc.WriteAttributeString("d", this.MyFillPath);
            doc.WriteAttributeString("fill", context.fill);
            doc.WriteAttributeString("fill-rule", "evenodd");
            doc.WriteAttributeString("stroke", "none");
        doc.WriteEndElement();
        doc.WriteStartElement("path");
            doc.WriteAttributeString("d", this.MyStrokePath);
            doc.WriteAttributeString("fill", "none");
            context.PrintEdgeArc(doc);
        doc.WriteEndElement();
      doc.WriteEndElement();
    }
  }



}
