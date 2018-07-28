using CGM.Scanner;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CGM2SVG
{
  internal sealed class EClass5
  {
    // Methods
    public static bool class5(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 1:
          mycontext.linebundleindex = mycontext.Reader.ReadIX();
          break;

        case 2:
          mycontext.Linetype = mycontext.Reader.ReadIX();
          break;

        case 3:
          if (mycontext.linewidthmode != 0)
          {
            mycontext.Linewidth = mycontext.Reader.ReadR();
          }
          else
          {
            mycontext.Linewidth = Convert.ToDouble(mycontext.Reader.ReadVDC());
          }
          break;

        case 4:
          mycontext.LineColorC = RuntimeHelpers.GetObjectValue(mycontext.Reader.ReadCO());
          break;

        case 9:
          mycontext.textbundleindex = mycontext.Reader.ReadIX();
          break;

        case 10:
          {
            int index = mycontext.Reader.ReadIX() - 1;
            if (mycontext.Fonts == null || (index >= mycontext.Fonts.Length))
            {
              mycontext.Font = "Arial";
            }
            else
            {
              mycontext.Font = mycontext.Fonts[index];
            }
            break;
          }
        case 13:
          mycontext.TextSpacing = mycontext.Reader.ReadR();
          break;

        case 14:
          mycontext.TextColorC = RuntimeHelpers.GetObjectValue(mycontext.Reader.ReadCO());
          break;

        case 15:
          mycontext.FontSize = Convert.ToDouble(mycontext.Reader.ReadVDC());
          break;

        case 0x10:
          {
            double num3 = 0;
            double num6 = 0;
            double num8 = 0;
            double num10 = 0;
            double num12 = 0;
            double num13 = 0;
            int num7 = Convert.ToInt32(mycontext.Reader.ReadVDC());
            int num11 = Convert.ToInt32(mycontext.Reader.ReadVDC());
            int num5 = Convert.ToInt32(mycontext.Reader.ReadVDC());
            int num9 = Convert.ToInt32(mycontext.Reader.ReadVDC());
            double num4 = Math.Sqrt((double)((num7 * num7) + (num11 * num11)));
            double num2 = Math.Sqrt((double)((num5 * num5) + (num9 * num9)));
            if (num4 != 0.0)
            {
              num8 = ((double)num7) / num4;
              num12 = ((double)num11) / num4;
            }
            if (num2 != 0.0)
            {
              num6 = ((double)num5) / num2;
              num10 = ((double)num9) / num2;
            }
            if (mycontext.GetInversion() == 3)
            {
              num3 = -1.0;
              num13 = 0.0;
            }
            else if (mycontext.GetInversion() == 4)
            {
              num3 = -1.0;
              num13 = 0.0;
              num10 = -num10;
            }
            else
            {
              num3 = 1.0;
              num13 = 0.0;
            }
            if ((num6 != 0.0) | (num10 != 0.0))
            {
              mycontext.TextRotateAngle = (Math.Acos(((num3 * num6) + (num13 * num10)) / (Math.Sqrt(1.0) * Math.Sqrt(Math.Pow(num10, 2.0) + Math.Pow(num6, 2.0)))) * 180.0) / 3.1415926535897931;
              if (num10 < 0.0)
              {
                mycontext.TextRotateAngle = -mycontext.TextRotateAngle;
              }
            }
            else
            {
              mycontext.TextRotateAngle = 0.0;
            }
            if ((((Math.Acos(((num8 * num6) + (num12 * num10)) / (Math.Sqrt(1.0) * Math.Sqrt(Math.Pow(num10, 2.0) + Math.Pow(num6, 2.0)))) * 180.0) / 3.1415926535897931) < 89.0) | (((Math.Acos(((num8 * num6) + (num12 * num10)) / (Math.Sqrt(1.0) * Math.Sqrt(Math.Pow(num10, 2.0) + Math.Pow(num6, 2.0)))) * 180.0) / 3.1415926535897931) > 91.0))
            {
              mycontext.TextAngle = Math.Acos(((num8 * num6) + (num12 * num10)) / (Math.Sqrt(1.0) * Math.Sqrt(Math.Pow(num10, 2.0) + Math.Pow(num6, 2.0))));
            }
            break;
          }
        case 0x11:
          mycontext.TextPath = mycontext.Reader.ReadE();
          break;

        case 0x12:
          mycontext.TextAlignment = mycontext.Reader.ReadE();
          mycontext.TextAlignmentVert = mycontext.Reader.ReadE();
          mycontext.TextHContinuous = mycontext.Reader.ReadR();
          mycontext.TextVContinuous = mycontext.Reader.ReadR();
          break;

        case 0x15:
          mycontext.fillbundleindex = mycontext.Reader.ReadIX();
          break;

        case 0x16:
          mycontext.interiorstyle = mycontext.Reader.ReadE();
          if (mycontext.interiorstyle == 3)
          {
            mycontext.getHatch(myxml);
          }
          break;

        case 0x17:
          mycontext.FillColorC = RuntimeHelpers.GetObjectValue(mycontext.Reader.ReadCO());
          break;

        case 0x18:
          mycontext.HatchIndex = mycontext.Reader.ReadIX();
          break;

        case 0x1a:
          mycontext.edgebundleindex = mycontext.Reader.ReadIX();
          break;

        case 0x1b:
          mycontext.edgetype = mycontext.Reader.ReadIX();
          break;

        case 0x1c:
          if (mycontext.edgewidthmode != 0)
          {
            mycontext.edgewidth = mycontext.Reader.ReadR();
          }
          else
          {
            mycontext.edgewidth = Convert.ToDouble(mycontext.Reader.ReadVDC());
          }
          break;

        case 0x1d:
          mycontext.EdgeColorC = RuntimeHelpers.GetObjectValue(mycontext.Reader.ReadCO());
          break;

        case 30:
          mycontext.edgevis = mycontext.Reader.ReadE();
          break;

        case 0x22:
          {
            mycontext.startcolorindex = Convert.ToInt32(mycontext.Reader.ReadCI());
            ArrayList list = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list.Add(mycontext.convertColor(mycontext.Reader.ReadCD()));
            }
            int num14 = list.Count;
            if (mycontext.colortable.Length < (mycontext.startcolorindex + num14))
            {
              mycontext.colortable = mycontext.colortable.Concat(new Color[((mycontext.startcolorindex + num14) - 1) + 1]).ToArray();//mycontext.colortable = Utils.CopyArray((Array)mycontext.colortable, new Color[((mycontext.startcolorindex + num14) - 1) + 1]);
            }
            Array.Copy(list.ToArray(typeof(Color)), 0, mycontext.colortable, mycontext.startcolorindex, list.Count);
            if (mycontext.startcolorindex == 0)
            {
              mycontext.BackColor = Color.Empty;
            }
            break;
          }
        case 0x23:
          while (!mycontext.Reader.EOF)
          {
            int num15 = mycontext.Reader.ReadE();
            mycontext.ASF[num15] = mycontext.Reader.ReadE();
          }
          break;

        case 0x25:
          mycontext.linecap = mycontext.Reader.ReadIX();
          break;

        case 0x26:
          mycontext.linejoin = mycontext.Reader.ReadIX();
          break;

        case 0x2c:
          mycontext.edgecap = mycontext.Reader.ReadIX();
          break;

        case 0x2d:
          mycontext.edgejoin = mycontext.Reader.ReadIX();
          break;

        default:
          return false;
      }
      return true;
    }
  }



}
