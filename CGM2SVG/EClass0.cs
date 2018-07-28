using CGM.Scanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
  internal sealed class EClass0
  {
    // Methods
    public static bool class0(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 1:
          //myxml.WriteComment("Begin MetaFile: " + mycontext.Reader.ReadS().Trim());
          //foreach (Assembly assembly in AppDomain.get_CurrentDomain().GetAssemblies())
          //{
          //  if (string.Compare(Path.GetExtension(assembly.get_Location()), ".exe", true) == 0)
          //  {
          //    object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
          //    for (int i = 0; i < customAttributes.Length; i++)
          //    {
          //      AssemblyCompanyAttribute attribute = customAttributes[i];
          //      if (attribute.get_Company().StartsWith("DocSoft"))
          //      {
          //        break;
          //      }
          //    }
          //  }
          //}
          //throw new Exception("Invalid Use.");

        case 3:
          {
            string str2 = mycontext.Reader.ReadS();
            myxml.WriteStartElement("g");
            myxml.WriteAttributeString("qsvg:picture", str2.Trim());
            break;
          }
        case 4:
          if (!mycontext.BackColor.IsEmpty)
          {
            new CGMBackGround().WriteBackground(myxml, mycontext);
          }
          break;

        case 5:
          myxml.WriteEndElement();
          break;

        case 8:
          mycontext.CurrFigure = new CGMFigure();
          mycontext.fingureOn = true;
          break;

        case 9:
          if (mycontext.CurrFigure != null)
          {
            if (Dynamic.Instance.Linked)
            {
              Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
            }
            mycontext.CurrFigure.UpdateSVG(myxml, mycontext);
            if (Dynamic.Instance.Linked)
            {
              Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
            }
            mycontext.CurrFigure = null;
          }
          mycontext.fingureOn = false;
          mycontext.connectingedge = false;
          break;

        case 0x13:
          {
            mycontext.prevtiles = new CGMTiles();
            mycontext.prevtiles.Position = mycontext.Reader.ReadP();
            mycontext.Reader.ReadE();
            mycontext.Reader.ReadE();
            Size size = new Size(mycontext.Reader.ReadI(), mycontext.Reader.ReadI());
            mycontext.prevtiles.NumberTiles = size;
            size = new Size(mycontext.Reader.ReadI(), mycontext.Reader.ReadI());
            mycontext.prevtiles.TileSize = size;
            SizeF ef = new SizeF((float)mycontext.Reader.ReadR(), (float)mycontext.Reader.ReadR());
            mycontext.prevtiles.PointSize = ef;
            CGM.Scanner.Point point = new CGM.Scanner.Point(mycontext.Reader.ReadI(), mycontext.Reader.ReadI());
            mycontext.prevtiles.Offset = point;
            size = new Size(mycontext.Reader.ReadI(), mycontext.Reader.ReadI());
            mycontext.prevtiles.ImageSize = size;
            break;
          }
        case 20:
          mycontext.prevtiles.UpdateSVG(myxml, mycontext);
          break;

        case 0x15:
          {
            string s = mycontext.Reader.ReadS();
            string str4 = mycontext.Reader.ReadS();
            mycontext.Reader.ReadE();
            myxml.WriteStartElement("g");
            myxml.WriteAttributeString("id", MakeId(s));
            myxml.WriteAttributeString("qsvg:aps", s);
            myxml.WriteAttributeString("qsvg:type", str4);
            break;
          }
        case 0x16:
          myxml.WriteComment("aps body");
          break;

        case 0x17:
          myxml.WriteEndElement();
          break;

        default:
          return false;
      }
      return true;
    }

    private static string MakeId(string s)
    {
      if (String.IsNullOrEmpty(s))//if (StringType.StrCmp(s, "", false) == 0)
      {
        return string.Empty;
      }
      StringBuilder builder = new StringBuilder();
      CharEnumerator enumerator = s.GetEnumerator();
      while (enumerator.MoveNext())
      {
        char ch = enumerator.Current;
        if ((char.IsLetterOrDigit(ch) || (ch == '.')) || (ch == '_'))
        {
          builder.Append(ch);
        }
        else
        {
          builder.Append("_x").Append(Convert.ToInt32(ch).ToString("X"));
        }
      }
      //if (!char.IsLetter(builder.get_Chars(0)) && (builder.get_Chars(0) != '_'))
      //{
      //  builder.Insert(0, '_');
      //}
      return builder.ToString();
    }
  }



}
