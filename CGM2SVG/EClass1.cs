using CGM.Scanner;
using System;
using System.Collections;
using System.Xml;

namespace CGM2SVG
{

  internal sealed class EClass1
  {
    // Methods
    public static bool class1(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 1:
          mycontext.version = mycontext.Reader.ReadI();
          myxml.WriteComment("Version: " + mycontext.version.ToString().Trim());
          break;

        case 2:
          myxml.WriteComment("MetaFile Description: " + mycontext.Reader.ReadS().Trim());
          break;

        case 9:
          mycontext.MaxColourIndex = Convert.ToInt32(mycontext.Reader.ReadCI());
          break;

        case 10:
          mycontext.mincolor = mycontext.Reader.ReadCD();
          mycontext.maxcolor = mycontext.Reader.ReadCD();
          break;

        case 12:
          break;

        case 13:
          {
            ArrayList list = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list.Add(mycontext.Reader.ReadS());
            }
            mycontext.Fonts = (string[])list.ToArray(typeof(string));
            mycontext.Font = mycontext.Fonts[0];
            break;
          }
        case 0x12:
          mycontext.SegPriExtMin = mycontext.Reader.ReadI();
          mycontext.segPriExtMax = mycontext.Reader.ReadI();
          break;

        default:
          return false;
      }
      return true;
    }
  }



}
