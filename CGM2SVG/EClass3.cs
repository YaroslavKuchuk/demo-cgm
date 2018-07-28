using CGM.Scanner;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CGM2SVG
{
  internal sealed class EClass3
  {

    public static bool class3(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 3:
          mycontext.AuxColorC = RuntimeHelpers.GetObjectValue(mycontext.Reader.ReadCO());
          break;

        case 4:
          mycontext.transparent = mycontext.Reader.ReadE() > 0;
          break;

        case 5:
          {
            CGMClip clip = new CGMClip
            {
              ClipPoint1 = mycontext.Reader.ReadP(),
              ClipPoint2 = mycontext.Reader.ReadP()
            };
            clip.UpdateSVG(myxml, mycontext);
            mycontext.CurrClipID = clip.ClipID;
            break;
          }
        case 6:
          {
            int num = mycontext.Reader.ReadE();
            mycontext.isClip = num == 0 ? false : true;
            break;
          }
        case 10:
          mycontext.newregion = true;
          break;

        case 0x13:
          mycontext.MetreLimit = mycontext.Reader.ReadR();
          break;

        default:
          return false;
      }
      return true;
    }
  }



}
