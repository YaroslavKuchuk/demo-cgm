using CGM.Scanner;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CGM2SVG
{
  internal class CGM4Text
  {
    // Fields
    public ArrayList appendcontext = new ArrayList();
    public ArrayList appendtext = new ArrayList();
    public Point Position;
    public string Text;

    // Methods
    public void UpdateSVG(XmlTextWriter doc, SVGContext mycontext)
    {
      this.Position = mycontext.fxy(this.Position);
      doc.WriteStartElement("text");
      doc.WriteAttributeString("x", this.Position.X.ToString());
      doc.WriteAttributeString("y", (Convert.ToDouble(this.Position.Y) + mycontext.compAliVert()).ToString());
      doc.WriteAttributeString("fill", mycontext.Text);
      mycontext.PrintTextRotation(doc, this.Position);
      doc.WriteAttributeString("font-size", (mycontext.fscale(mycontext.FontSize)).ToString());
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
      //string[] strArray = new string[] { "d", "e", "m", "o" };
      //int index = 0;
      //while (this.Text.Length <= num)
      //{
      //  this.Text = this.Text + strArray[index];
      //  if (strArray[index] == "o")
      //  {
      //    index = 0;
      //  }
      //  else
      //  {
      //    index++;
      //  }
      //}
      doc.WriteString(this.Text);
      doc.WriteEndElement();
    }

    public void UpdateSVGwContext(XmlTextWriter doc, SVGContext mycontext)
    {
      this.Position = mycontext.fxy(this.Position);
      doc.WriteStartElement("text");
      doc.WriteAttributeString("x", this.Position.X.ToString());
      doc.WriteAttributeString("y", (Convert.ToDouble(this.Position.Y) + mycontext.compAliVert()).ToString());
     // doc.WriteAttributeString("fill", StringType.FromObject(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "Text", new object[0], null, null)));
      object[] args = new object[] { doc, this.Position };
      bool[] copyBack = new bool[] { true, true };
      //LateBinding.LateCall(this.appendcontext.get_Item(0), null, "PrintTextRotation", args, null, copyBack);
      //if (copyBack[1])
      //{
      //  this.Position = args[1];
      //}
      //if (copyBack[0])
      //{
      //  doc = args[0];
      //}
      args = new object[1];
      //object o = this.appendcontext.get_Item(0);
      //args[0] = RuntimeHelpers.GetObjectValue(LateBinding.LateGet(o, null, "FontSize", new object[0], null, null));
      object[] objArray3 = args;
      copyBack = new bool[] { true };
      if (copyBack[0])
      {
      //  LateBinding.LateSetComplex(o, null, "FontSize", new object[] { RuntimeHelpers.GetObjectValue(objArray3[0]) }, null, true, true);
      }
      //doc.WriteAttributeString("font-size", (LateBinding.LateGet(this.appendcontext.get_Item(0), null, "fscale", objArray3, null, copyBack)).ToString());
      args = new object[] { doc };
      copyBack = new bool[] { true };
      //LateBinding.LateCall(this.appendcontext.get_Item(0), null, "PrintTextAlign", args, null, copyBack);
      if (copyBack[0])
      {
       // doc = args[0];
      }
      //doc.WriteAttributeString("font-family", StringType.FromObject(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "Font", new object[0], null, null)));
     // if (BooleanType.FromObject(ObjectType.BitAndObj(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "isClip", new object[0], null, null), ObjectType.ObjTst(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "CurrClipID", new object[0], null, null), "", false) != 0)))
     // {
     //   doc.WriteAttributeString("clip-path", StringType.FromObject(ObjectType.StrCatObj(ObjectType.StrCatObj("url(#", LateBinding.LateGet(this.appendcontext.get_Item(0), null, "CurrClipID", new object[0], null, null)), ")")));
     // }
      mycontext.printTextAngle(doc, this.Position);
      if (mycontext.TextSpacing != 0.0)
      {
        doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      }
      int num2 = this.Text.Length;
      this.Text = "";
      //string[] strArray = new string[] { "d", "e", "m", "o" };
      //int index = 0;
      //while (this.Text.Length <= num2)
      //{
      //  this.Text = this.Text + strArray[index];
      //  if (strArray[index] == "o")
      //  //if (StringType.StrCmp(strArray[index], "o", false) == 0)
      //  {
      //    index = 0;
      //  }
      //  else
      //  {
      //    index++;
      //  }
      //}
      doc.WriteString(this.Text);
      //for (int i = 1; i < this.appendtext.Count; i++)
      //{
      //  doc.WriteStartElement("tspan");
      //  doc.WriteAttributeString("fill", (LateBinding.LateGet(this.appendcontext.get_Item(i), null, "Text", new object[0], null, null)).ToString());
      //  args = new object[] { doc, this.Position };
      //  copyBack = new bool[] { true, true };
      //  LateBinding.LateCall(this.appendcontext.get_Item(i), null, "PrintTextRotation", args, null, copyBack);
      //  if (copyBack[1])
      //  {
      //    this.Position = args[1];
      //  }
      //  if (copyBack[0])
      //  {
      //    doc = args[0];
      //  }
      //  object[] objArray = new object[1];
      //  o = this.appendcontext.get_Item(i);
      //  objArray[0] = RuntimeHelpers.GetObjectValue(LateBinding.LateGet(o, null, "FontSize", new object[0], null, null));
      //  args = objArray;
      //  copyBack = new bool[] { true };
      //  if (copyBack[0])
      //  {
      //    LateBinding.LateSetComplex(o, null, "FontSize", new object[] { RuntimeHelpers.GetObjectValue(args[0]) }, null, true, true);
      //  }
      //  doc.WriteAttributeString("font-size",(LateBinding.LateGet(this.appendcontext.get_Item(i), null, "fscale", args, null, copyBack)).ToString());
      //  args = new object[] { doc };
      //  copyBack = new bool[] { true };
      //  LateBinding.LateCall(this.appendcontext.get_Item(i), null, "PrintTextAlign", args, null, copyBack);
      //  if (copyBack[0])
      //  {
      //    doc = args[0];
      //  }
      //  doc.WriteAttributeString("font-family", mycontext.Font);
      //  if (BooleanType.FromObject(ObjectType.BitAndObj(LateBinding.LateGet(this.appendcontext.get_Item(i), null, "isClip", new object[0], null, null), ObjectType.ObjTst(LateBinding.LateGet(this.appendcontext.get_Item(i), null, "CurrClipID", new object[0], null, null), "", false) != 0)))
      //  {
      //    doc.WriteAttributeString("clip-path", (ObjectType.StrCatObj(ObjectType.StrCatObj("url(#", LateBinding.LateGet(this.appendcontext.get_Item(i), null, "CurrClipID", new object[0], null, null)), ")")).ToString());
      //  }
      //  if (mycontext.TextSpacing != 0.0)
      //  {
      //    doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      //  }
      //  num2 = StringType.FromObject(this.appendtext.get_Item(i - 1)).Length;
      //  string str2 = "";
      //  string[] strArray3 = new string[] { "d", "e", "m", "o" };
      //  index = 0;
      //  while (str2.Length <= num2)
      //  {
      //    str2 = str2 + strArray3[index];
      //    if (strArray3[index] == "o")
      //    {
      //      index = 0;
      //    }
      //    else
        //  {
        //    index++;
        //  }
        //}
      //  doc.WriteString(str2);
      //  doc.WriteEndElement();
      //}
      doc.WriteStartElement("tspan");
      doc.WriteAttributeString("fill", mycontext.Text);
      mycontext.PrintTextRotation(doc, this.Position);
      doc.WriteAttributeString("font-size", (mycontext.fscale(mycontext.FontSize)).ToString());
      mycontext.PrintTextAlign(doc);
      doc.WriteAttributeString("font-family", mycontext.Font);
      //if (BooleanType.FromObject(ObjectType.BitAndObj(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "isClip", new object[0], null, null), ObjectType.ObjTst(LateBinding.LateGet(this.appendcontext.get_Item(0), null, "CurrClipID", new object[0], null, null), "", false) != 0)))
      //{
      //  doc.WriteAttributeString("clip-path", "url(#" + mycontext.CurrClipID + ")");
      //}
      //if (mycontext.TextSpacing != 0.0)
      //{
      //  doc.WriteAttributeString("letter-spacing", (mycontext.TextSpacing * 4.8).ToString());
      //}
      //num2 = StringType.FromObject(this.appendtext.get_Item(this.appendtext.Count - 1)).Length;
      string text = "";
      //string[] strArray2 = new string[] { "d", "e", "m", "o" };
      //index = 0;
      //while (text.Length <= num2)
      //{
      //  text = text + strArray2[index];
      //  if (StringType.StrCmp(strArray2[index], "o", false) == 0)
      //  {
      //    index = 0;
      //  }
      //  else
      //  {
      //    index++;
      //  }
      //}
      doc.WriteString(text);
      doc.WriteEndElement();
      doc.WriteEndElement();
    }
  }



}
