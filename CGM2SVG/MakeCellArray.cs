using CGM.Scanner;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

namespace CGM2SVG
{
  internal class MakeCellArray
  {
    // Fields
    private Color[,] cellarray;
    public int cellmode;
    public byte[] data;
    private SVGContext mycontext;
    private int mylcp;
    public int nx;
    public int ny;
    public CGM.Scanner.Point p;
    public CGM.Scanner.Point q;
    public CGM.Scanner.Point r;

    // Methods
    public MakeCellArray(SVGContext context)
    {
      this.mycontext = context;
    }

    private long GetBits(byte[] b, int offset, int len)
    {
      long num4 = 0L;
      int num = offset / 8;
      int num3 = ((offset + len) - 1) / 8;
      int num6 = num3;
      for (int i = num; i <= num6; i++)
      {
        num4 = (num4 << 8) | b[i];
      }
      if (((offset & 7) <= 0) && ((len & 7) <= 0))
      {
        return num4;
      }
      num4 = num4 >> (((((num3 - num) + 1) * 8) - len) - (offset & 7));
      return (num4 & ((((long)1L) << len) - 1L));
    }

    private void GetColor(byte[] data, ref int curBit, int colorLen, ref Color color)
    {
      if (this.mycontext.Reader.context.isDirectColor && (colorLen >= 8))
      {
        ArrayList list = new ArrayList();
        list.Add(this.GetBits(data, curBit, colorLen));
        curBit += colorLen;
        list.Add(this.GetBits(data, curBit, colorLen));
        curBit += colorLen;
        list.Add(this.GetBits(data, curBit, colorLen));
        curBit += colorLen;
        if (this.mycontext.Reader.context.Is4ColorModel)
        {
          list.Add(this.GetBits(data, curBit, colorLen));
          curBit += colorLen;
        }
        color = this.mycontext.convertColor(list.ToArray(typeof(long)));
      }
      else
      {
        long num = this.GetBits(data, curBit, colorLen);
        curBit += colorLen;
        color = this.mycontext.colortable[(int)num];
      }
    }

    private void GetColorList()
    {
      int curBit = 0;
      int lCP = this.LCP;
      if (lCP == 0)
      {
        if (this.mycontext.Reader.context.isDirectColor)
        {
          lCP = this.mycontext.Reader.context.ColorPrecision;
        }
        else
        {
          lCP = this.mycontext.Reader.context.ColorIndexPrecision;
        }
      }
      if (this.cellmode > 0)
      {
        int num12 = this.ny - 1;
        for (int i = 0; i <= num12; i++)
        {
          int num11 = this.nx - 1;
          for (int j = 0; j <= num11; j++)
          {
            Color color = new Color();
            this.GetColor(this.data, ref curBit, lCP, ref color);
            this.cellarray[j, i] = color;
          }
          curBit = (curBit + 15) & -16;
        }
      }
      else
      {
        int num10 = this.ny - 1;
        for (int k = 0; k <= num10; k++)
        {
          int num6 = 0;
          do
          {
            Color color2 = new Color();
            int num7 = (int)this.GetBits(this.data, curBit, this.mycontext.Reader.context.IntegerPrecision);
            curBit += this.mycontext.Reader.context.IntegerPrecision;
            this.GetColor(this.data, ref curBit, lCP, ref color2);
            int num9 = num7 - 1;
            for (int m = 0; m <= num9; m++)
            {
              this.cellarray[num6, k] = color2;
              num6++;
            }
          }
          while (num6 < this.nx);
          curBit = (curBit + 15) & -16;
        }
      }
    }

    public void MakeArray()
    {
      this.cellarray = new Color[(this.nx - 1) + 1, (this.ny - 1) + 1];
      this.GetColorList();
      if (decimal.Compare(this.p.X, this.q.X) < 0)
      {
        int num8 = this.ny - 1;
        for (int i = 0; i <= num8; i++)
        {
          int num2 = 0;
          for (int j = this.nx - 1; num2 < j; j--)
          {
            Color color = this.cellarray[num2, i];
            this.cellarray[num2, i] = this.cellarray[j, i];
            this.cellarray[j, i] = color;
            num2++;
          }
        }
      }
      if (decimal.Compare(this.p.Y, this.q.Y) < 0)
      {
        int num7 = this.nx - 1;
        for (int k = 0; k <= num7; k++)
        {
          int num5 = 0;
          for (int m = this.ny - 1; num5 < m; m--)
          {
            Color color2 = this.cellarray[k, num5];
            this.cellarray[k, num5] = this.cellarray[k, m];
            this.cellarray[k, m] = color2;
            num5++;
          }
        }
      }
    }

    public void updatesvg(XmlTextWriter doc)
    {
      Bitmap bitmap = new Bitmap(this.nx, this.ny, PixelFormat.Format24bppRgb);
      int num4 = this.nx - 1;
      for (int i = 0; i <= num4; i++)
      {
        int num3 = this.ny - 1;
        for (int j = 0; j <= num3; j++)
        {
          bitmap.SetPixel((this.nx - 1) - i, j, this.cellarray[i, j]);
        }
      }
      MemoryStream stream = new MemoryStream();
      PNGMaker.Convert((Image)bitmap, (Stream)stream);
      stream.Close();
      doc.WriteStartElement("image");
      doc.WriteAttributeString("x", (this.mycontext.fx(Convert.ToDouble(decimal.Compare(this.p.X, this.r.X) < 0 ? this.p.X : this.r.X))).ToString());
      doc.WriteAttributeString("y", (this.mycontext.fy(Convert.ToDouble(decimal.Compare(this.p.Y, this.q.Y) > 0 ? this.p.Y : this.q.Y))).ToString());
      double introduced6 = Convert.ToDouble(this.p.X);
      double introduced7 = Convert.ToDouble(this.r.X);
      doc.WriteAttributeString("width", (this.mycontext.fscale(Math2.Distance(introduced6, Convert.ToDouble(this.p.Y), introduced7, Convert.ToDouble(this.r.Y)))).ToString());
      double introduced8 = Convert.ToDouble(this.r.X);
      double introduced9 = Convert.ToDouble(this.q.X);
      doc.WriteAttributeString("height", (this.mycontext.fscale(Math2.Distance(introduced8, Convert.ToDouble(this.r.Y), introduced9, Convert.ToDouble(this.q.Y)))).ToString());
      doc.WriteAttributeString("href", "http://www.w3.org/1999/xlink", "data:;base64," + Convert.ToBase64String(stream.ToArray()));
      if (this.mycontext.isClip & !String.IsNullOrEmpty(mycontext.CurrClipID)) //if (this.mycontext.isClip & (StringType.StrCmp(this.mycontext.CurrClipID, "", false) != 0))
      {
        doc.WriteAttributeString("clip-path", "url(#" + this.mycontext.CurrClipID + ")");
      }
      doc.WriteEndElement();
    }

    // Properties
    public int LCP {
      get {
        return this.mylcp;
      }
      set {
        this.mylcp = value;
      }
    }
  }



}
