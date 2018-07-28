using CGM.Scanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{

  internal sealed class EClass2
  {
    // Methods
    public static bool class2(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      switch (element.ElementId)
      {
        case 3:
          mycontext.linewidthmode = mycontext.Reader.ReadE();
          break;

        case 5:
          mycontext.edgewidthmode = mycontext.Reader.ReadE();
          break;

        case 6:
          {
            mycontext.vdclb = mycontext.Reader.ReadP();
            mycontext.vdcrt = mycontext.Reader.ReadP();
            mycontext.ow = Convert.ToDouble(Math.Abs(decimal.Subtract(mycontext.vdclb.X, mycontext.vdcrt.X)));
            mycontext.oh = Convert.ToDouble(Math.Abs(decimal.Subtract(mycontext.vdclb.Y, mycontext.vdcrt.Y)));
            CGM.Scanner.Point point = new CGM.Scanner.Point(new decimal(mycontext.options.ViewBox.Location.X), new decimal(mycontext.options.ViewBox.Location.Y));
            mycontext.rtl = point;
            mycontext.rw = Convert.ToInt32(Math.Round(mycontext.ow));
            mycontext.rh = Convert.ToInt32(Math.Round(mycontext.oh));
            mycontext.scale = 1.0;
            if (mycontext.options.FitInBox || mycontext.options.EnlargeInBox)
            {
              double num2 = mycontext.ow / ((double)mycontext.options.ViewBox.Width);
              double num = mycontext.oh / ((double)mycontext.options.ViewBox.Height);
              if (num2 < num)
              {
                if (((num > 1.0) && mycontext.options.FitInBox) || ((num < 1.0) && mycontext.options.EnlargeInBox))
                {
                  mycontext.scale = num;
                  mycontext.rw = Convert.ToInt32(Math.Round(mycontext.ow / num));
                  mycontext.rh = Convert.ToInt32(Math.Round((double)mycontext.options.ViewBox.Width));
                }
              }
              else if (((num2 > 1.0) && mycontext.options.FitInBox) || ((num2 < 1.0) && mycontext.options.EnlargeInBox))
              {
                mycontext.scale = num2;
                mycontext.rw = Convert.ToInt32(Math.Round((double)mycontext.options.ViewBox.Width));
                mycontext.rh = Convert.ToInt32(Math.Round(mycontext.oh / num2));
              }
            }
            break;
          }
        case 7:
          mycontext.BackColor = mycontext.convertColor(mycontext.Reader.ReadCD());
          break;

        case 11:
          {
            SVGContext.linebundlestruct linebundlestruct;
            int index = mycontext.Reader.ReadIX();
            linebundlestruct.type = mycontext.Reader.ReadIX();
            linebundlestruct.width = Convert.ToDouble(mycontext.Reader.ReadSS(true));
            linebundlestruct.color = (Color)mycontext.Reader.ReadCO();
            if ((mycontext.linebundle.Length - 1) < index)
            {
              mycontext.linebundle = mycontext.linebundle.Concat(new SVGContext.linebundlestruct[(index - 1) + 1]).ToArray();//mycontext.linebundle = Utils.CopyArray((Array)mycontext.linebundle, new SVGContext.linebundlestruct[(index - 1) + 1]);
            }
            mycontext.linebundle[index] = linebundlestruct;
            break;
          }
        case 13:
          {
            SVGContext.textbundlestruct textbundlestruct;
            int num4 = mycontext.Reader.ReadIX();
            textbundlestruct.fontindex = mycontext.Reader.ReadIX().ToString();
            textbundlestruct.precision = mycontext.Reader.ReadE();
            textbundlestruct.spacing = mycontext.Reader.ReadR();
            textbundlestruct.expansion = mycontext.Reader.ReadR();
            textbundlestruct.color = (Color)mycontext.Reader.ReadCO();
            if ((mycontext.textbundle.Length - 1) < num4)
            {
              //mycontext.textbundle = Utils.CopyArray((Array)mycontext.textbundle, new SVGContext.textbundlestruct[(num4 - 1) + 1]);
              mycontext.textbundle = mycontext.textbundle.Concat(new SVGContext.textbundlestruct[(num4 - 1) + 1]).ToArray();
            }
            mycontext.textbundle[num4] = textbundlestruct;
            break;
          }
        case 14:
          {
            SVGContext.fillbundlestruct fillbundlestruct;
            int num5 = mycontext.Reader.ReadIX();
            fillbundlestruct.style = mycontext.Reader.ReadE();
            fillbundlestruct.color = (Color)mycontext.Reader.ReadCO();
            fillbundlestruct.hatch = mycontext.Reader.ReadIX();
            fillbundlestruct.pattern = mycontext.Reader.ReadIX();
            if ((mycontext.fillbundle.Length - 1) < num5)
            {
              //mycontext.fillbundle = Utils.CopyArray((Array)mycontext.fillbundle, new SVGContext.fillbundlestruct[(num5 - 1) + 1]);
              mycontext.fillbundle = mycontext.fillbundle.Concat(new SVGContext.fillbundlestruct[(num5 - 1) + 1]).ToArray();
            }
            mycontext.fillbundle[num5] = fillbundlestruct;
            break;
          }
        case 15:
          {
            SVGContext.edgebundlestruct edgebundlestruct;
            int num6 = mycontext.Reader.ReadIX();
            edgebundlestruct.type = mycontext.Reader.ReadIX();
            edgebundlestruct.width = Convert.ToDouble(mycontext.Reader.ReadSS(true));
            edgebundlestruct.color = (Color)mycontext.Reader.ReadCO();
            if ((mycontext.edgebundle.Length - 1) < num6)
            {
              //mycontext.edgebundle = Utils.CopyArray((Array)mycontext.edgebundle, new SVGContext.edgebundlestruct[(num6 - 1) + 1]);
              mycontext.edgebundle = mycontext.edgebundle.Concat(new SVGContext.edgebundlestruct[(num6 - 1) + 1]).ToArray();
            }
            mycontext.edgebundle[num6] = edgebundlestruct;
            break;
          }
        default:
          return false;
      }
      return true;
    }
  }



}
