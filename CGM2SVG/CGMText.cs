using CGM.Scanner;
using System;
using System.Collections;
using System.Drawing;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMText
  {
    // Fields
    public ArrayList appendcontext = new ArrayList();
    public ArrayList appendtext = new ArrayList();
    public CGM.Scanner.Point Position;
    public string Text;
    public SizeF TextSize;

    // Methods
    public double ActualRatio(string mytext, string myfont, double myheight)
    {
      SizeF ef = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(this.Text, new Font(myfont, (float)myheight, FontStyle.Regular, GraphicsUnit.Pixel));
      return (double)(ef.Width / ef.Height);
    }

    public double RequestedRatio()
    {
      return (double)(this.TextSize.Width / this.TextSize.Height);
    }

    public double ScaleRatio(string text, SVGContext mycontext, double height)
    {
      return (this.RequestedRatio() / this.ActualRatio(text, mycontext.Font == null ? "Tahoma" : mycontext.Font.ToString(), height));
    }

    public override string ToString()
    {
      return string.Format("Text: '{0}', Position: {1}", this.Text, this.Position.ToString());
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext mycontext)
    {
      this.Position = mycontext.fxy(this.Position);
      doc.WriteStartElement("text");
      doc.WriteAttributeString("x", this.Position.X.ToString());
      doc.WriteAttributeString("y", (Convert.ToDouble(this.Position.Y) + mycontext.compAliVert()).ToString());
      doc.WriteAttributeString("fill", mycontext.Text);
      mycontext.PrintTextRotation(doc, this.Position);
      mycontext.PrintTextHeight(doc, this.TextSize);
      mycontext.PrintTextAlign(doc);
      if (!String.IsNullOrEmpty(mycontext.Font)) //if (StringType.StrCmp(mycontext.Font, "", false) != 0)
      {
        doc.WriteAttributeString("font-family", mycontext.Font);
      }
      if (mycontext.isClip & !String.IsNullOrEmpty(mycontext.CurrClipID)) //if (mycontext.isClip & (StringType.StrCmp(mycontext.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + mycontext.CurrClipID + ")");
      }
      mycontext.printTextAngle(doc, this.Position);
      if (mycontext.TextSpacing != 0.0)
      {
        doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      }
      int num = this.Text.Length;
      this.Text = "";
      string[] strArray = new string[] { "d", "e", "m", "o" };
      int index = 0;
      while (this.Text.Length <= num)
      {
        this.Text = this.Text + strArray[index];
        if (strArray[index] == "o") //if (StringType.StrCmp(strArray[index], "o", false) == 0)
        {
          index = 0;
        }
        else
        {
          index++;
        }
      }
      doc.WriteString(this.Text);
      doc.WriteEndElement();
    }

    public void UpdateSVGwContext(XmlTextWriter doc, SVGContext mycontext)
    {
      this.Position = mycontext.fxy(this.Position);
      doc.WriteStartElement("text");
      doc.WriteAttributeString("x", this.Position.X.ToString());
      doc.WriteAttributeString("y", (Convert.ToDouble(this.Position.Y) + mycontext.compAliVert()).ToString());
      //doc.WriteAttributeString("fill", (LateBinding.LateGet(this.appendcontext.get_Item(0), null, "Text", new object[0], null, null)).ToString());
      mycontext.PrintTextRotation(doc, this.Position);
      mycontext.PrintTextHeight(doc, this.TextSize);
      mycontext.PrintTextAlign(doc);
      //doc.WriteAttributeString("font-family", (LateBinding.LateGet(this.appendcontext.get_Item(0), null, "Font", new object[0], null, null)).ToString());
      //if (BooleanType.FromObject(ObjectType.BitAndObj(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "isClip", new object[0], null, null), ObjectType.ObjTst(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "CurrClipID", new object[0], null, null), "", false) != 0)))
      //{
      //  doc.WriteAttributeString("clip-path", StringType.FromObject(ObjectType.StrCatObj(ObjectType.StrCatObj("url(#", LateBinding.LateGet(this.appendcontext.get_Item(0), null, "CurrClipID", new object[0], null, null)), ")")));
      //}
      if (mycontext.TextSpacing != 0.0)
      {
        doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      }
      mycontext.printTextAngle(doc, this.Position);
      int num2 = this.Text.Length;
      this.Text = "";
      string[] strArray = new string[] { "d", "e", "m", "o" };
      int index = 0;
      while (this.Text.Length <= num2)
      {
        this.Text = this.Text + strArray[index];
        if (strArray[index] == "o")//if (StringType.StrCmp(strArray[index], "o", false) == 0)
        {
          index = 0;
        }
        else
        {
          index++;
        }
      }
      doc.WriteString(this.Text);
      //for (int i = 1; i < this.appendtext.Count; i++)
      //{
      //  doc.WriteStartElement("tspan");
      //  doc.WriteAttributeString("fill", (LateBinding.LateGet(this.appendcontext.get_Item(i), null, "Text", new object[0], null, null)).ToString());
      //  object[] args = new object[] { doc, this.Position };
      //  bool[] copyBack = new bool[] { true, true };
      //  LateBinding.LateCall(this.appendcontext.get_Item(i), null, "PrintTextRotation", args, null, copyBack);
      //  if (copyBack[1])
      //  {
      //    this.Position = args[1];
      //  }
      //  if (copyBack[0])
      //  {
      //    doc = args[0];
      //  }
      //  object[] objArray2 = new object[] { doc };
      //  copyBack = new bool[] { true };
      //  LateBinding.LateCall(this.appendcontext.get_Item(i), null, "PrintTextAlign", objArray2, null, copyBack);
      //  if (copyBack[0])
      //  {
      //    doc = objArray2[0];
      //  }
      //  doc.WriteAttributeString("font-family", mycontext.Font);
      //  mycontext.PrintTextHeight(doc, this.TextSize);
      //  if (BooleanType.FromObject(ObjectType.BitAndObj(LateBinding.LateGet(this.appendcontext.get_Item(i), null, "isClip", new object[0], null, null), ObjectType.ObjTst(LateBinding.LateGet(this.appendcontext.get_Item(i), null, "CurrClipID", new object[0], null, null), "", false) != 0)))
      //  {
      //    doc.WriteAttributeString("clip-path", (ObjectType.StrCatObj(ObjectType.StrCatObj("url(#", LateBinding.LateGet(this.appendcontext.get_Item(i), null, "CurrClipID", new object[0], null, null)), ")")).ToString());
      //  }
      //  if (mycontext.TextSpacing != 0.0)
      //  {
      //    doc.WriteAttributeString("letter-spacing", StringType.FromDouble(mycontext.TextSpacing * 4.8));
      //  }
      //  num2 = StringType.FromObject(this.appendtext.get_Item(i - 1)).Length;
      //  string str2 = "";
      //  string[] strArray3 = new string[] { "d", "e", "m", "o" };
      //  index = 0;
      //  while (str2.Length <= num2)
      //  {
      //    str2 = str2 + strArray3[index];
      //    if (strArray3[index] == "o") //if (StringType.StrCmp(strArray3[index], "o", false) == 0)
      //    {
      //      index = 0;
      //    }
      //    else
      //    {
      //      index++;
      //    }
      //  }
      //  doc.WriteString(str2);
      //  doc.WriteEndElement();
      //}
      doc.WriteStartElement("tspan");
      doc.WriteAttributeString("fill", mycontext.Text);
      mycontext.PrintTextRotation(doc, this.Position);
      mycontext.PrintTextAlign(doc);
      mycontext.PrintTextHeight(doc, this.TextSize);
      doc.WriteAttributeString("font-family", mycontext.Font);
      if (mycontext.isClip & !String.IsNullOrEmpty(mycontext.CurrClipID)) //if (mycontext.isClip & (StringType.StrCmp(mycontext.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + mycontext.CurrClipID + ")");
      }
      if (mycontext.TextSpacing != 0.0)
      {
        doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      }
      //num2 = StringType.FromObject(this.appendtext.get_Item(this.appendtext.Count - 1)).Length;
      //string text = "";
      //string[] strArray2 = new string[] { "d", "e", "m", "o" };
      //index = 0;
      //while (text.Length <= num2)
      //{
      //  text = text + strArray2[index];
      //  if (strArray2[index] == "o") //if (StringType.StrCmp(strArray2[index], "o", false) == 0)
      //  {
      //    index = 0;
      //  }
      //  else
      //  {
      //    index++;
      //  }
      //}
      //doc.WriteString(text);
      doc.WriteEndElement();
      doc.WriteEndElement();
    }
  }

}
