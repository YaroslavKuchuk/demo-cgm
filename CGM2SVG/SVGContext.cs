using CGM.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
  internal class SVGContext : ICloneable
  {
    // Fields
    public Color _backColor;
    public CGMText AppendTxt;
    public CGM4Text AppendTxt4;
    public bool Appfour;
    public int[] ASF;
    public object AuxColorC;
    public bool backgroundProduced;
    public double CharacterHeight;
    public Color[] colortable;
    public bool connectingedge;
    public string CurrClipID;
    public CGMFigure CurrFigure;
    public edgebundlestruct[] edgebundle;
    public int edgebundleindex;
    public int edgecap;
    public object EdgeColorC;
    public int edgejoin;
    public int edgetype_;
    public int edgevis;
    public double edgewidth_;
    public int edgewidthmode;
    public bool figline;
    public fillbundlestruct[] fillbundle;
    public int fillbundleindex;
    public object FillColorC;
    public int fillrep;
    public bool Final;
    public bool fingureOn;
    public string FontIndex;
    public string[] Fonts;
    public double FontSize;
    public int HatchIndex_;
    public bool[] Hatchwritten;
    public int interiorstyle_;
    public bool isClip;
    public linebundlestruct[] linebundle;
    public int linebundleindex;
    public int linecap;
    public object LineColorC;
    public int linejoin;
    public int linetype_;
    public double linewidth_;
    public int linewidthmode;
    public uint[] maxcolor;
    public int MaxColourIndex;
    public double MetreLimit;
    public uint[] mincolor;
    public bool newregion;
    public double oh;
    public TranslateOptions options;
    public double ow;
    public CGMTiles prevtiles;
    public string qsvgns;
    public CGMBinaryReader Reader;
    public int RestrictedTextType;
    public int rh;
    public CGM.Scanner.Point rtl;
    public int rw;
    public double scale;
    public int segPriExtMax;
    public int SegPriExtMin;
    public int startcolorindex;
    public int TextAlignment;
    public int TextAlignmentVert;
    public double TextAngle;
    public textbundlestruct[] textbundle;
    public int textbundleindex;
    public object TextColorC;
    public double TextHContinuous;
    public int TextPath;
    public double TextRotateAngle;
    public double Textspacing_;
    public double TextVContinuous;
    public bool transparent;
    public CGM.Scanner.Point vdclb;
    public CGM.Scanner.Point vdcrt;
    public int version;

    // Methods
    public SVGContext()
    {
      this.qsvgns = "http://www.docsoft.com/qsvg/";
      this.backgroundProduced = false;
      this.linebundle = new linebundlestruct[0];
      this.textbundle = new textbundlestruct[0];
      this.edgebundle = new edgebundlestruct[0];
      this.fillbundle = new fillbundlestruct[0];
      this.textbundleindex = 0;
      this.linebundleindex = 0;
      this.edgebundleindex = 0;
      this.fillbundleindex = 0;
      this.maxcolor = new uint[] { Convert.ToUInt32(0xff), Convert.ToUInt32(0xff), Convert.ToUInt32(0xff), Convert.ToUInt32(0xff) };
      this.mincolor = new uint[] { Convert.ToUInt32(0), Convert.ToUInt32(0), Convert.ToUInt32(0), Convert.ToUInt32(0) };
      this.ASF = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0
    };
      this.MaxColourIndex = 0x3f;
      this.connectingedge = false;
      this.newregion = false;
      this.figline = false;
      this.edgevis = 1;
      this.startcolorindex = 0;
      this.edgewidthmode = 1;
      this.linewidthmode = 1;
      this.MetreLimit = 32767.0;
      this.Textspacing_ = 0.0;
      this.TextAngle = 1.0;
      this.TextRotateAngle = 0.0;
      this.interiorstyle_ = 0;
      this.transparent = true;
      this.edgewidth_ = 0.0;
      this.linewidth_ = 1.0;
      this.FillColorC = 1;
      this.LineColorC = 1;
      this.EdgeColorC = 1;
      this.TextColorC = 1;
      this.AuxColorC = 1;
      this.linecap = 1;
      this.edgecap = 1;
      this.linejoin = 1;
      this.edgejoin = 1;
      this.linetype_ = 1;
      this.edgetype_ = 1;
      this.CurrClipID = "";
      this.isClip = true;
      this.RestrictedTextType = 1;
      this.Final = true;
      this.HatchIndex_ = 1;
      this.Hatchwritten = new bool[] { false, false, false, false, false, false };
      this.fingureOn = false;
      this.colortable = new Color[] { Color.White, Color.Black };
    }


    public object Clone()
    {
      return (SVGContext)this.MemberwiseClone();
    }

    public double compAliVert()
    {
      double num2 = default(double);
      double num = this.fscale(this.FontSize);
      switch (this.TextAlignmentVert)
      {
        case 0:
          return 0.0;

        case 1:
          return (num * 0.9);

        case 2:
          return (num * 0.7);

        case 3:
          return (num * 0.5);

        case 4:
          return 0.0;

        case 5:
          return -(num * 0.2);

        case 6:
          return ((this.TextVContinuous - 0.28) * num);
      }
      return num2;
    }

    private Color convertCMYKColor(double c, double m, double y, double k)
    {
      c = this.NormColorComponent(0, c / 100.0);
      m = this.NormColorComponent(0, m / 100.0);
      y = this.NormColorComponent(0, y / 100.0);
      k = this.NormColorComponent(0, k / 100.0);
      double num3 = -(((c * (1.0 - k)) + k) - 1.0);
      double num2 = -(((m * (1.0 - k)) + k) - 1.0);
      double num = -(((y * (1.0 - k)) + k) - 1.0);
      return Color.FromArgb(0xff, (int)Math.Round(num3 * 255.0), (int)Math.Round(num2 * 255.0), (int)Math.Round(num * 255.0));
    }

    public Color convertColor(object obj)
    {
      return this.colortable[0];
      //if (!Information.IsArray(RuntimeHelpers.GetObjectValue(obj)))
      //{
      //  return this.colortable[Convert.ToInt32(RuntimeHelpers.GetObjectValue(obj))];
      //}
      //if (((Array)obj).get_Length() > 3)
      //{
      //  return this.convertCMYKColor((double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 0 }, null))), (double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 1 }, null))), (double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 2 }, null))), (double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 3 }, null))));
      //}
      //return this.convertRGBColor((double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 0 }, null))), (double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 1 }, null))), (double)Convert.ToInt32(RuntimeHelpers.GetObjectValue(LateBinding.LateIndexGet(obj, new object[] { 2 }, null))));

    }

    public Color convertRGBColor(double r, double g, double b)
    {
      r = this.NormColorComponent(0, r);
      g = this.NormColorComponent(0, g);
      b = this.NormColorComponent(0, b);
      return Color.FromArgb(0xff, (int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
    }

    public double divide(int n1, int n2)
    {
      if (n2 != 0)
      {
        return (((double)n1) / ((double)n2));
      }
      return 1.0;
    }

    public ArrayList ExportColorTable()
    {
      return new ArrayList(this.colortable);
    }


    public double fscale(double scaler)
    {
      double falsePart = scaler / this.scale;
      return falsePart;
    }

    public double fscaleFloat(double scaler)
    {
      return (scaler / this.scale);
    }

    public double fx(double XC)
    {
      int inversion = this.GetInversion();
      if ((inversion == 3) | (inversion == 4))
      {
        XC -= Convert.ToDouble(this.vdclb.X);
        XC = -XC;
      }
      else
      {
        XC -= Convert.ToDouble(this.vdclb.X);
      }
      XC = this.fscale(XC);
      XC += Convert.ToDouble(this.rtl.X);
      XC += (this.rw - (this.ow / this.scale)) / 2.0;
      return XC;
    }

    public CGM.Scanner.Point fxy(CGM.Scanner.Point xyc)
    {
      xyc.X = new decimal(this.fx(Convert.ToDouble(xyc.X)));
      xyc.Y = new decimal(this.fy(Convert.ToDouble(xyc.Y)));
      return xyc;
    }

    public SizeF fxy(SizeF xyc)
    {
      xyc.Width = (float)this.fscale((double)xyc.Width);
      xyc.Height = (float)this.fscale((double)xyc.Height);
      return xyc;
    }

    public double fy(double YC)
    {
      int inversion = this.GetInversion();
      if ((inversion == 1) | (inversion == 3))
      {
        YC -= Convert.ToDouble(this.vdcrt.Y);
        YC = -YC;
        YC = this.fscale(YC);
        YC += Convert.ToDouble(this.rtl.Y);
        return YC;
      }
      YC -= Convert.ToDouble(this.vdclb.Y);
      YC = this.fscale(YC);
      YC += Convert.ToDouble(decimal.Add(this.rtl.Y, new decimal(this.rh)));
      return YC;
    }

    public object getHatch(XmlTextWriter doc)
    {
      object obj2 = default(object);
      if (!this.Hatchwritten[this.HatchIndex - 1])
      {
        Color fillColor;
        if (this.FillColor.IsEmpty)
        {
          fillColor = this.colortable[1];
        }
        else
        {
          fillColor = this.FillColor;
        }
        doc.WriteStartElement("pattern");
        doc.WriteAttributeString("id", this.HatchIndex.ToString());
        doc.WriteAttributeString("patternUnits", "userSpaceOnUse");
        doc.WriteAttributeString("x", "0");
        doc.WriteAttributeString("y", "0");
        double num2 = ((double)this.rw) / 100.0;
        doc.WriteAttributeString("width", num2.ToString());
        num2 = ((double)this.rh) / 100.0;
        doc.WriteAttributeString("height", num2.ToString());
        num2 = ((double)this.rw) / 100.0;
        double num = ((double)this.rh) / 100.0;
        doc.WriteAttributeString("viewbox", "0 0 " + num2.ToString() + " " + num.ToString());
        if (this.HatchIndex == 1)
        {
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 200.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          doc.WriteAttributeString("y1", "0");
          num = ((double)this.rh) / 100.0;
          doc.WriteAttributeString("x2", num.ToString());
          doc.WriteAttributeString("y2", "0");
          doc.WriteEndElement();
        }
        else if (this.HatchIndex == 2)
        {
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 200.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          doc.WriteAttributeString("y1", "0");
          doc.WriteAttributeString("x2", "0");
          num = ((double)this.rh) / 100.0;
          doc.WriteAttributeString("y2", num.ToString());
          doc.WriteEndElement();
        }
        else if (this.HatchIndex == 3)
        {
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 2000.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          num = (((double)this.rh) / 100.0) + 1.0;
          doc.WriteAttributeString("y1", num.ToString());
          num = (((double)this.rw) / 100.0) + 1.0;
          doc.WriteAttributeString("x2", num.ToString());
          doc.WriteAttributeString("y2", "0");
          doc.WriteEndElement();
        }
        else if (this.HatchIndex == 4)
        {
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 2000.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          doc.WriteAttributeString("y1", "0");
          num = (((double)this.rw) / 100.0) + 1.0;
          doc.WriteAttributeString("x2", num.ToString());
          num = (((double)this.rh) / 100.0) + 1.0;
          doc.WriteAttributeString("y2", num.ToString());
          doc.WriteEndElement();
        }
        else if (this.HatchIndex == 5)
        {
          doc.WriteStartElement("rect");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 2000.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x", "0");
          doc.WriteAttributeString("y", "0");
          num = ((double)this.rw) / 100.0;
          doc.WriteAttributeString("width", num.ToString());
          num = ((double)this.rh) / 100.0;
          doc.WriteAttributeString("height", num.ToString());
          doc.WriteEndElement();
        }
        else if (this.HatchIndex == 6)
        {
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 3000.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          doc.WriteAttributeString("y1", "0");
          num = (((double)this.rw) / 100.0) + 1.0;
          doc.WriteAttributeString("x2", num.ToString());
          num = (((double)this.rh) / 100.0) + 1.0;
          doc.WriteAttributeString("y2", num.ToString());
          doc.WriteEndElement();
          doc.WriteStartElement("line");
          doc.WriteAttributeString("fill", "none");
          doc.WriteAttributeString("stroke", ColorTranslator.ToHtml(fillColor));
          num = ((double)this.rw) / 3000.0;
          doc.WriteAttributeString("stroke-width", num.ToString());
          doc.WriteAttributeString("x1", "0");
          num = (((double)this.rh) / 100.0) + 1.0;
          doc.WriteAttributeString("y1", num.ToString());
          doc.WriteAttributeString("x2", ((((double)this.rw) / 100.0) + 1.0).ToString());
          doc.WriteAttributeString("y2", "0");
          doc.WriteEndElement();
        }
        doc.WriteEndElement();
        this.Hatchwritten[this.HatchIndex - 1] = true;
      }
      return obj2;
    }

    public int GetInversion()
    {
      int num = default(int);
      if (decimal.Compare(this.vdclb.X, this.vdcrt.X) < 0)
      {
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) < 0)
        {
          return 1;
        }
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) > 0)
        {
          return 2;
        }
      }
      if (decimal.Compare(this.vdclb.X, this.vdcrt.X) > 0)
      {
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) < 0)
        {
          return 3;
        }
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) > 0)
        {
          return 4;
        }
      }
      return num;
    }

    public PointF GetViewboxWH()
    {
      PointF tf = default(PointF);
      if (decimal.Compare(this.vdclb.X, this.vdcrt.X) < 0)
      {
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) < 0)
        {
          float x = Convert.ToSingle(decimal.Subtract(this.vdcrt.X, this.vdclb.X));
          return new PointF(x, Convert.ToSingle(decimal.Subtract(this.vdcrt.Y, this.vdclb.Y)));
        }
        if (decimal.Compare(this.vdclb.Y, this.vdcrt.Y) > 0)
        {
          float introduced3 = Convert.ToSingle(decimal.Subtract(this.vdcrt.X, this.vdclb.X));
          return new PointF(introduced3, Convert.ToSingle(decimal.Subtract(this.vdcrt.Y, this.vdclb.Y)));
        }
      }
      return tf;
    }

    private int NormColorComponent(int i, double val)
    {
      int num2 = Convert.ToInt32(this.mincolor[i]);
      int num = Convert.ToInt32(this.maxcolor[i]);
      return (int)Math.Round(Math.Floor(((val - num2) / ((double)((num - num2) + 1f))) * 256.0));
    }

    public void PrintEdge(XmlWriter xw)
    {
      double num = this.fscale(this.edgewidth);
      if (num == 0.0)
      {
        num = 1.0;
      }
      if (this.edgevis == 1)
      {
        xw.WriteAttributeString("stroke", this.Edge?.ToString());
        xw.WriteAttributeString("stroke-width", num.ToString());
      }
      else
      {
        xw.WriteAttributeString("stroke-width", "0");
        xw.WriteAttributeString("stroke", this.Edge?.ToString());
      }
      if (this.GetedgeJoin != "unspecified")
      {
        xw.WriteAttributeString("stroke-linejoin", this.GetedgeJoin);
      }
      if (this.GetedgeCap != "unspecified")
      {
        xw.WriteAttributeString("stroke-linecap", this.GetedgeCap);
      }
      if (this.MetreLimit < 32000.0)
      {
        xw.WriteAttributeString("stroke-miterlimit", this.MetreLimit.ToString());
      }
      this.PrintStrokeDashArray(xw, (int)Math.Round(this.edgetype));
    }

    public void PrintEdgeArc(XmlWriter xw)
    {
      double num = this.fscale(this.edgewidth);
      if (num == 0.0)
      {
        num = 1.0;
      }
      if (this.edgevis == 1)
      {
        xw.WriteAttributeString("stroke", this.Edge.ToString());
        xw.WriteAttributeString("stroke-width", num.ToString());
      }
      else
      {
        xw.WriteAttributeString("stroke-width", "0");
        xw.WriteAttributeString("stroke", this.Edge.ToString());
      }
      xw.WriteAttributeString("stroke-linejoin", "round");
      if (this.GetedgeCap != "unspecified")
      {
        xw.WriteAttributeString("stroke-linecap", this.GetedgeCap);
      }
      if (this.MetreLimit < 32000.0)
      {
        xw.WriteAttributeString("stroke-miterlimit", this.MetreLimit.ToString());
      }
      this.PrintStrokeDashArray(xw, (int)Math.Round(this.edgetype));
    }

    public void PrintLine(XmlWriter xw)
    {
      double num = this.fscale(this.Linewidth);
      if (num == 0.0)
      {
        num = 1.0;
      }
      xw.WriteAttributeString("stroke", this.Line?.ToString());
      xw.WriteAttributeString("stroke-width", num.ToString());
      if (this.GetLineJoin != "unspecified")
      {
        xw.WriteAttributeString("stroke-linejoin", this.GetLineJoin);
      }
      if (this.GetLineCap != "unspecified")
      {
        xw.WriteAttributeString("stroke-linecap", this.GetLineCap);
      }
      if (this.MetreLimit < 32000.0)
      {
        xw.WriteAttributeString("stroke-miterlimit", this.MetreLimit.ToString());
      }
      this.PrintStrokeDashArray(xw, (int)Math.Round(this.Linetype));
    }

    private void PrintStrokeDashArray(XmlWriter xw, int type)
    {
      string str = (((double)(this.rh * 15)) / 1000.0).ToString();
      string str2 = (((double)(this.rh * 5)) / 1000.0).ToString();
      switch (type)
      {
        case 2:
          xw.WriteAttributeString("stroke-dasharray", str + "," + str2);
          break;

        case 3:
          xw.WriteAttributeString("stroke-dasharray", str2 + "," + str2);
          break;

        case 4:
          xw.WriteAttributeString("stroke-dasharray", str + "," + str2 + "," + str2 + "," + str2);
          break;

        case 5:
          xw.WriteAttributeString("stroke-dasharray", str + "," + str2 + "," + str2 + "," + str2 + "," + str2 + "," + str2);
          break;

        default:
          return;
      }
    }

    public void PrintTextAlign(XmlWriter xw)
    {
      if (this.TextPath == 0)
      {
        switch (this.TextAlignment)
        {
          case 1:
            xw.WriteAttributeString("text-anchor", "start");
            break;

          case 2:
            xw.WriteAttributeString("text-anchor", "middle");
            break;

          case 3:
            xw.WriteAttributeString("text-anchor", "end");
            break;
        }
      }
    }

    public object printTextAngle(XmlWriter doc, CGM.Scanner.Point Position)
    {
      object obj2 = default(object);
      string[] strArray2;
      double num6;
      if ((this.TextAngle != 1.0) & (this.TextRotateAngle == 0.0))
      {
        double num2 = Math.Floor(Math.Tan(this.TextAngle) * 100.0) * 0.001;
        double num = num2 * Convert.ToDouble(Position.Y);
        strArray2 = new string[5];
        strArray2[0] = "matrix(1 0 ";
        num6 = -num2;
        strArray2[1] = num6.ToString();
        strArray2[2] = " 1 ";
        strArray2[3] = num.ToString();
        strArray2[4] = " 0)";
        doc.WriteAttributeString("transform", string.Concat(strArray2));
        return obj2;
      }
      if ((this.TextAngle == 1.0) & (this.TextRotateAngle != 0.0))
      {
        strArray2 = new string[7];
        strArray2[0] = "rotate(";
        num6 = -this.TextRotateAngle;
        strArray2[1] = num6.ToString();
        strArray2[2] = " ";
        strArray2[3] = Position.X.ToString();
        strArray2[4] = ", ";
        strArray2[5] = Position.Y.ToString();
        strArray2[6] = ")";
        doc.WriteAttributeString("transform", string.Concat(strArray2));
        return obj2;
      }
      if ((this.TextAngle != 1.0) & (this.TextRotateAngle != 0.0))
      {
        double num4 = Math.Floor(Math.Tan(this.TextAngle) * 100.0) * 0.001;
        double num3 = num4 * Convert.ToDouble(Position.Y);
        string[] strArray = new string[6];
        strArray2 = new string[] { "rotate(", this.TextRotateAngle.ToString(), " ", Position.X.ToString(), ", ", Position.Y.ToString() };
        strArray[0] = string.Concat(strArray2);
        strArray[1] = ") matrix(1 0 ";
        strArray[2] = num4.ToString();
        strArray[3] = " 1 ";
        strArray[4] = num3.ToString();
        strArray[5] = " 0)";
        doc.WriteAttributeString("transform", string.Concat(strArray));
      }
      return obj2;
    }

    public void PrintTextHeight(XmlWriter xw, SizeF mysize)
    {
      double maxValue = double.MaxValue;
      switch (this.TextPath)
      {
        case 0:
        case 1:
          maxValue = mysize.Height;
          break;

        case 2:
        case 3:
          maxValue = mysize.Width;
          break;
      }
      if (maxValue == 0.0)
      {
        maxValue = this.FontSize;
      }
      else
      {
        maxValue = Math.Min(Math.Abs(maxValue), this.FontSize);
      }
      xw.WriteAttributeString("font-size", this.fscale(maxValue).ToString() + "px");
    }

    public void PrintTextRotation(XmlWriter xw, CGM.Scanner.Point loc)
    {
      switch (this.TextPath)
      {
        case 1:
          xw.WriteAttributeString("writing-mode", "rl-tb");
          xw.WriteAttributeString("unicode-bidi", "bidi-override");
          xw.WriteAttributeString("direction", "rtl");
          break;

        case 2:
          xw.WriteAttributeString("transform", string.Format("rotate(-90 {0} {1})", loc.X, loc.Y));
          xw.WriteAttributeString("glyph-orientation-horizontal", "90");
          break;

        case 3:
          xw.WriteAttributeString("writing-mode", "tb");
          xw.WriteAttributeString("glyph-orientation-vertical", "0");
          break;
      }
    }

    // Properties
    public object Back { get; }
    public Color BackColor { get; set; }
    private bool ColorSelectionMode { get; }
    public object Edge { get; }
    public Color EdgeColor { get; }
    public double edgetype { get; set; }
    public double edgewidth { get; set; }
    public string fill { get; }
    public Color FillColor { get; }
    public string Font { get; set; }
    public string GetedgeCap { get; }
    public string GetedgeJoin { get; }
    public string GetLineCap { get; }
    public string GetLineJoin { get; }
    public int HatchIndex { get; set; }
    public int interiorstyle { get; set; }
    public object Line { get; }
    public Color LineColor { get; }
    public double Linetype { get; set; }
    public double Linewidth { get; set; }
    public string Text { get; }
    public Color TextColor { get; }
    public double TextSpacing { get; set; }
    public bool vdcextents { get; }

    // Nested Types
    [StructLayout(LayoutKind.Sequential)]
    public struct edgebundlestruct
    {
      public int type;
      public double width;
      public Color color;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct fillbundlestruct
    {
      public int style;
      public Color color;
      public int hatch;
      public int pattern;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct linebundlestruct
    {
      public int type;
      public double width;
      public Color color;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct textbundlestruct
    {
      public string fontindex;
      public int precision;
      public double spacing;
      public double expansion;
      public Color color;
    }
  }



}
