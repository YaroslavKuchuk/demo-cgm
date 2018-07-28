using CGM.Scanner;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml;

namespace CGM2SVG
{
  internal sealed class EClass4
  {
    // Methods
    public static bool class4(CGMElement element, SVGContext mycontext, XmlTextWriter myxml)
    {
      CGM.Scanner.Point[] pointArray;
      switch (element.ElementId)
      {
        case 1:
          {
            ArrayList list = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list.Add(mycontext.Reader.ReadP());
            }
            CGMPolyLine line = new CGMPolyLine
            {
              points = (CGM.Scanner.Point[])list.ToArray(typeof(CGM.Scanner.Point))
            };
            if (mycontext.CurrFigure == null)
            {
              line.UpdateSVG(myxml, mycontext);
            }
            else
            {
              mycontext.figline = true;
              mycontext.CurrFigure.AddPath(line.GetPath(mycontext), mycontext);
            }
            break;
          }
        case 2:
          {
            ArrayList list2 = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list2.Add(mycontext.Reader.ReadP());
            }
            CGMDisjoint disjoint = new CGMDisjoint
            {
              points = (CGM.Scanner.Point[])list2.ToArray(typeof(CGM.Scanner.Point))
            };
            if (mycontext.CurrFigure == null)
            {
              disjoint.UpdateSVG(myxml, mycontext);
            }
            else
            {
              mycontext.figline = true;
              mycontext.CurrFigure.AddPath(disjoint.GetPath(mycontext), mycontext);
            }
            break;
          }
        case 4:
          {
            CGM4Text text = new CGM4Text
            {
              Position = mycontext.Reader.ReadP()
            };
            int num = mycontext.Reader.ReadE();
            text.Text = mycontext.Reader.ReadS();
            if (num != 0)
            {
              mycontext.Final = true;
              text.UpdateSVG(myxml, mycontext);
            }
            else
            {
              mycontext.Final = false;
              SVGContext context = (SVGContext)mycontext.Clone();
              text.appendcontext.Add(context);
              mycontext.AppendTxt4 = text;
              mycontext.Appfour = true;
            }
            break;
          }
        case 5:
          {
            CGMText text2 = new CGMText();
            SizeF ef = new SizeF((float)mycontext.Reader.ReadVDC(), (float)mycontext.Reader.ReadVDC());
            text2.TextSize = ef;
            text2.Position = mycontext.Reader.ReadP();
            int num2 = mycontext.Reader.ReadE();
            text2.Text = mycontext.Reader.ReadS();
            if (num2 != 1)
            {
              mycontext.Final = false;
              SVGContext context2 = (SVGContext)mycontext.Clone();
              text2.appendcontext.Add(context2);
              mycontext.AppendTxt = text2;
              mycontext.Appfour = false;
            }
            else
            {
              text2.UpdateSVG(myxml, mycontext);
            }
            break;
          }
        case 6:
          if (mycontext.Final)
          {
            mycontext.Final = false;
          }
          else
          {
            int num3 = mycontext.Reader.ReadE();
            string str = mycontext.Reader.ReadS();
            if (num3 != 1)
            {
              if (mycontext.Appfour)
              {
                CGM4Text text5 = mycontext.AppendTxt4;
                text5.appendtext.Add(str);
                SVGContext context3 = (SVGContext)mycontext.Clone();
                text5.appendcontext.Add(context3);
                mycontext.Appfour = true;
              }
              else
              {
                CGMText appendTxt = mycontext.AppendTxt;
                appendTxt.appendtext.Add(str);
                SVGContext context4 = (SVGContext)mycontext.Clone();
                appendTxt.appendcontext.Add(context4);
                mycontext.Appfour = false;
              }
            }
            else if (!mycontext.Appfour)
            {
              CGMText text4 = mycontext.AppendTxt;
              text4.appendtext.Add(str);
              text4.UpdateSVGwContext(myxml, mycontext);
              mycontext.Final = true;
              mycontext.AppendTxt = null;
            }
            else
            {
              CGM4Text text3 = mycontext.AppendTxt4;
              text3.appendtext.Add(str);
              text3.UpdateSVGwContext(myxml, mycontext);
              mycontext.Final = true;
              mycontext.AppendTxt4 = null;
            }
          }
          break;

        case 7:
          {
            ArrayList list3 = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list3.Add(mycontext.Reader.ReadP());
            }
            list3.Add(RuntimeHelpers.GetObjectValue(list3[0]));
            CGMPoly poly = new CGMPoly
            {
              points = (CGM.Scanner.Point[])list3.ToArray(typeof(CGM.Scanner.Point))
            };
            if (mycontext.CurrFigure == null)
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              poly.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            else
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(poly.GetPath(mycontext), mycontext);
            }
            break;
          }
        case 8:
          {
            ArrayList list4 = new ArrayList();
            ArrayList list5 = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list4.Add(mycontext.Reader.ReadP());
              list5.Add(mycontext.Reader.ReadE());
            }
            CGMPolySet set = new CGMPolySet
            {
              points = (CGM.Scanner.Point[])list4.ToArray(typeof(CGM.Scanner.Point)),
              flags = (int[])list5.ToArray(typeof(int))
            };
            if (Dynamic.Instance.Linked)
            {
              Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
            }
            set.UpdateSVG(myxml, mycontext);
            if (Dynamic.Instance.Linked)
            {
              Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
            }
            break;
          }
        case 9:
          {
            MakeCellArray array = new MakeCellArray(mycontext)
            {
              p = mycontext.Reader.ReadP(),
              q = mycontext.Reader.ReadP(),
              r = mycontext.Reader.ReadP(),
              nx = mycontext.Reader.ReadI(),
              ny = mycontext.Reader.ReadI(),
              LCP = mycontext.Reader.ReadI(),
              cellmode = mycontext.Reader.ReadE(),
              data = mycontext.Reader.ReadBS(-1)
            };
            array.MakeArray();
            array.updatesvg(myxml);
            break;
          }
        case 11:
          {
            CGMRect rect = new CGMRect
            {
              topleft = mycontext.Reader.ReadP(),
              bottomright = mycontext.Reader.ReadP()
            };
            if (mycontext.CurrFigure != null)
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(rect.GetPath(mycontext), mycontext);
            }
            else
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              rect.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            break;
          }
        case 12:
          {
            CGMCircle circle = new CGMCircle
            {
              center = mycontext.Reader.ReadP(),
              radius = Convert.ToInt32(mycontext.Reader.ReadVDC())
            };
            if (Dynamic.Instance.Linked)
            {
              Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
            }
            if (mycontext.CurrFigure != null)
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(circle.GetPath(mycontext), mycontext);
            }
            else
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              circle.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            break;
          }
        case 13:
          {
            CGMArc arc = new CGMArc();
            pointArray = new CGM.Scanner.Point[] { mycontext.Reader.ReadP(), mycontext.Reader.ReadP(), mycontext.Reader.ReadP() };
            arc.SetCircle3Points(pointArray);
            if (mycontext.CurrFigure != null)
            {
              mycontext.CurrFigure.AddPath(arc.GetPath(mycontext), mycontext);
            }
            else
            {
              arc.UpdateSVG(myxml, mycontext);
            }
            break;
          }
        case 14:
          {
            CGMArc arc2 = new CGMArc();
            pointArray = new CGM.Scanner.Point[] { mycontext.Reader.ReadP(), mycontext.Reader.ReadP(), mycontext.Reader.ReadP() };
            arc2.SetCircle3Points(pointArray);
            arc2.closingType = mycontext.Reader.ReadE();
            if (mycontext.CurrFigure != null)
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(arc2.GetPath(mycontext), mycontext);
            }
            else
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              arc2.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            break;
          }
        case 15:
          {
            CGMArc arc3 = new CGMArc
            {
              center = mycontext.Reader.ReadP(),
              v1 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    },
              v2 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    }
            };
            double r = Convert.ToDouble(mycontext.Reader.ReadVDC());
            arc3.SetCircleDiameters(r, false);
            if (mycontext.CurrFigure != null)
            {
              mycontext.CurrFigure.AddPath(arc3.GetPath(mycontext), mycontext);
            }
            else
            {
              arc3.UpdateSVG(myxml, mycontext);
            }
            break;
          }
        case 0x10:
          {
            CGMArc arc4 = new CGMArc
            {
              center = mycontext.Reader.ReadP(),
              v1 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    },
              v2 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    }
            };
            double num5 = Convert.ToDouble(mycontext.Reader.ReadVDC());
            arc4.SetCircleDiameters(num5, false);
            arc4.closingType = mycontext.Reader.ReadE();
            if (mycontext.CurrFigure != null)
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(arc4.GetPath(mycontext), mycontext);
            }
            else
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              arc4.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            break;
          }
        case 0x11:
          {
            CGMEllipse ellipse = new CGMEllipse
            {
              center = mycontext.Reader.ReadP(),
              cd1 = mycontext.Reader.ReadP(),
              cd2 = mycontext.Reader.ReadP()
            };
            if (mycontext.CurrFigure != null)
            {
              mycontext.newregion = true;
              mycontext.CurrFigure.AddPath(ellipse.GetPath(mycontext), mycontext);
            }
            else
            {
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateBeginSVGLink(ref myxml, mycontext);
              }
              ellipse.UpdateSVG(myxml, mycontext);
              if (Dynamic.Instance.Linked)
              {
                Dynamic.Instance.CreateEndSVGLink(ref myxml, mycontext);
              }
            }
            break;
          }
        case 0x12:
          new CGMArc
          {
            center = mycontext.Reader.ReadP(),
            cd1 = mycontext.Reader.ReadP(),
            cd2 = mycontext.Reader.ReadP(),
            v1 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    },
            v2 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    }
          }.UpdateSVG(myxml, mycontext);
          break;

        case 0x13:
          new CGMArc
          {
            center = mycontext.Reader.ReadP(),
            cd1 = mycontext.Reader.ReadP(),
            cd2 = mycontext.Reader.ReadP(),
            v1 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    },
            v2 = {
                        X = Convert.ToDecimal(mycontext.Reader.ReadVDC()),
                        Y = Convert.ToDecimal(mycontext.Reader.ReadVDC())
                    },
            closingType = mycontext.Reader.ReadE()
          }.UpdateSVG(myxml, mycontext);
          break;

        case 0x15:
          mycontext.connectingedge = true;
          break;

        case 0x1a:
          {
            CGMPolyBez bez = new CGMPolyBez
            {
              Continue = mycontext.Reader.ReadIX() == 1 ? false : true
            };
            ArrayList list6 = new ArrayList();
            while (!mycontext.Reader.EOF)
            {
              list6.Add(mycontext.Reader.ReadP());
            }
            bez.Points = (CGM.Scanner.Point[])list6.ToArray(typeof(CGM.Scanner.Point));
            if (mycontext.CurrFigure == null)
            {
              bez.UpdateSVG(myxml, mycontext);
            }
            else
            {
              mycontext.CurrFigure.AddPath(bez.GetPath(mycontext), mycontext);
            }
            break;
          }
        case 0x1c:
          {
            mycontext.Reader.ReadIX();
            mycontext.Reader.ReadI();
            mycontext.Reader.ReadCO();
            mycontext.Reader.ReadCO();
            mycontext.Reader.ReadSDR();
            byte[] buffer = mycontext.Reader.ReadBS(-1);
            mycontext.prevtiles.Tiles.Add(buffer);
            mycontext.prevtiles.isBitonal = true;
            break;
          }
        case 0x1d:
          {
            mycontext.Reader.ReadIX();
            mycontext.Reader.ReadI();
            mycontext.Reader.ReadI();
            mycontext.Reader.ReadSDR();
            byte[] buffer2 = mycontext.Reader.ReadBS(-1);
            mycontext.prevtiles.Tiles.Add(buffer2);
            mycontext.prevtiles.Tiles.Add(buffer2);
            mycontext.prevtiles.isBitonal = false;
            break;
          }
        default:
          return false;
      }
      return true;
    }
  }



}
