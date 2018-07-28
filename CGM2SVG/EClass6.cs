using CGM.Scanner;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CGM2SVG
{
  internal sealed class EClass6
  {
    // Methods
    public static bool class7(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 1:
          {
            int num = mycontext.Reader.ReadE();
            string str = mycontext.Reader.ReadS();
            myxml.WriteStartElement("qsvg:string");
            myxml.WriteAttributeString("identifier", num.ToString());
            myxml.WriteAttributeString("value", str);
            myxml.WriteEndElement();
            break;
          }
        case 2:
          {
            int num2 = mycontext.Reader.ReadI();
            byte[] buffer = mycontext.Reader.ReadD();
            myxml.WriteStartElement("qsvg:data");
            myxml.WriteAttributeString("identifier", num2.ToString());
            myxml.WriteAttributeString("value", Convert.ToBase64String(buffer));
            myxml.WriteEndElement();
            break;
          }
        default:
          return false;
      }
      return true;
    }

    public static bool class9(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      if (element.ElementId == 1)
      {
        string str = mycontext.Reader.ReadS();
        SDR sdr = mycontext.Reader.ReadSDR();
        myxml.WriteStartElement("qsvg:sdrData");
        myxml.WriteAttributeString("type", str);
        WriteSDR(myxml, sdr);
        myxml.WriteEndElement();
        return true;
      }
      return false;
    }

    private static void WriteSDR(XmlWriter myxml, SDR sdr)
    {
      foreach (SDR.Member member in sdr.Members)
      {
        myxml.WriteStartElement("qsvg:sdrMember");
        myxml.WriteAttributeString("type", Enum.GetName(typeof(SDR.DataTypeIndex), member.DataType));
        if (member.DataType == SDR.DataTypeIndex.SDR)
        {
          object[] items = member.Items;
          for (int i = 0; i < items.Length; i++)
          {
            SDR sdr2 = (SDR)items[i];
            WriteSDR(myxml, sdr2);
          }
        }
        else
        {
          object[] objArray = member.Items;
          for (int j = 0; j < objArray.Length; j++)
          {
            object objectValue = RuntimeHelpers.GetObjectValue(objArray[j]);
            myxml.WriteStartElement("qsvg:sdrItem");
            if (objectValue != null)
            {
              myxml.WriteAttributeString("value", objectValue.ToString());
            }
            myxml.WriteEndElement();
          }
        }
        myxml.WriteEndElement();
      }
    }
  }



}
