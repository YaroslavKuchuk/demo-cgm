using System.Xml;

namespace CGM2SVG
{
  internal class CGMBackGround
  {
    // Methods
    public void WriteBackground(XmlTextWriter doc, SVGContext mycontext)
    {
      if (!mycontext.options.SingleBackground || !mycontext.backgroundProduced)
      {
        doc.WriteStartElement("rect");
        doc.WriteAttributeString("width", mycontext.rw.ToString());
        doc.WriteAttributeString("height", mycontext.rh.ToString());
        doc.WriteAttributeString("fill", mycontext.Back.ToString());
        doc.WriteAttributeString("qsvg:element", "background");
        doc.WriteEndElement();
        mycontext.backgroundProduced = true;
      }
    }
  }
}
