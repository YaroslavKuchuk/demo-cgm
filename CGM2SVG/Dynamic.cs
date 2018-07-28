using System.Xml;

namespace CGM2SVG
{
  internal class Dynamic
  {
    // Fields
    private static Dynamic _inst = new Dynamic();
    private bool isHighlight;
    private bool isLinked;
    private string linkuri;

    // Methods
    private Dynamic()
    {
    }

    public void CreateBeginSVGHighlight(ref XmlTextWriter doc, SVGContext context)
    {
      if (this.isHighlight)
      {
        doc.WriteStartElement("script");
        doc.WriteAttributeString("type", "text/ecmascript");
      }
    }

    public void CreateBeginSVGLink(ref XmlTextWriter doc, SVGContext context)
    {
      doc.WriteStartElement("a");
      doc.WriteAttributeString("xlink:href", this.linkuri);
    }

    public void CreateEndSVGLink(ref XmlTextWriter doc, SVGContext context)
    {
      doc.WriteEndElement();
      this.isLinked = false;
    }

    // Properties
    public bool Highlight {
      get {
        return this.isHighlight;
      }
      set {
        this.isHighlight = value;
      }
    }

    public static Dynamic Instance {
      get {
        return _inst;
      }
    }

    public bool Linked {
      get {
        return this.isLinked;
      }
      set {
        this.isLinked = value;
      }
    }

    public string URI {
      get {
        return string.Empty;
      }
      set {
        this.linkuri = value;
      }
    }
  }



}
