using CGM.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CGM2SVG
{
  internal class CGMTiles
  {
    // Fields
    public int cmprType = 2;
    public int colorPrecision;
    public Size ImageSize;
    public bool isBitonal = true;
    public Size NumberTiles;
    public CGM.Scanner.Point Offset;
    public SizeF PointSize;
    public CGM.Scanner.Point Position;
    public int rowpad;
    public int SDRLen;
    public ArrayList Tiles = new ArrayList();
    public Size TileSize;

    // Methods
    public override string ToString()
    {
      return ("Image: size " + this.ImageSize.ToString());
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext mycontext)
    {
      TIFFMaker maker = new TIFFMaker();
      int num8 = this.Tiles.Count - 1;
      for (int i = 0; i <= num8; i++)
      {
        MemoryStream stream;
        BitMapMaker maker2 = default(BitMapMaker);
        MemoryStream stream3;
        int colorPrecision;
        MemoryStream s = new MemoryStream();
        maker.T6Data = (byte[])this.Tiles[i];
        maker.ImageWidth = this.TileSize.Width;
        maker.ImageHeight = this.TileSize.Height;
        switch (this.cmprType)
        {
          case 2:
            if (!this.isBitonal)
            {
              break;
            }
            maker.ImageWidth = (this.TileSize.Width + 7) & -8;
            maker.ProduceBilevel((Stream)s);
            goto Label_00B6;

          case 5:
            stream3 = new MemoryStream((byte[])this.Tiles[i]);
            colorPrecision = this.colorPrecision;
            if (colorPrecision != 8)
            {
              goto Label_0116;
            }
            maker2 = new BitMapMaker(stream3, this.TileSize.Width, this.TileSize.Height, PixelFormat.Format24bppRgb);
            goto Label_0148;

          case 7:
            //s = new MemoryStream((byte[])this.Tiles[i], this.SDRLen, Information.UBound((byte[])this.Tiles[i], 1) - (this.SDRLen - 1));
           // s.set_Position(0L);
            goto Label_01BE;

          default:
            goto Label_01BE;
        }
        maker.ProduceContonal(s);
        Label_00B6:
       // s.set_Position(0L);
        goto Label_01BE;
        Label_0116:
        switch (colorPrecision)
        {
          case 0x10:
            maker2 = new BitMapMaker(stream3, this.TileSize.Width, this.TileSize.Height, PixelFormat.Format48bppRgb);
            break;
        }
        Label_0148:
        maker2.CreateImage();
        s = (MemoryStream)maker2.GetFinalStream();
       // s.set_Position(0L);
        Label_01BE:
        stream = new MemoryStream();
        PNGMaker.Convert((Stream)s, (Stream)stream);
        s.Close();
        stream.Close();
        int num2 = i % this.NumberTiles.Width;
        int num3 = i / this.NumberTiles.Width;
        double num4 = mycontext.fx(Convert.ToDouble(this.Position.X)) + (num2 * mycontext.fscale((double)(((float)this.TileSize.Width) / this.PointSize.Width)));
        double num5 = mycontext.fy(Convert.ToDouble(this.Position.Y)) + (num3 * mycontext.fscale((double)(((float)this.TileSize.Height) / this.PointSize.Height)));

        doc.WriteStartElement("image");
        doc.WriteAttributeString("x", Math.Floor((double)num4).ToString());
        doc.WriteAttributeString("y", Math.Floor((double)num5).ToString());
        doc.WriteAttributeString("width", (Math.Floor(mycontext.fscale((double)(((float)this.TileSize.Width) / this.PointSize.Width)))).ToString());
        doc.WriteAttributeString("height", (Math.Ceiling(mycontext.fscale((double)(((float)this.TileSize.Height) / this.PointSize.Height)))).ToString());
        doc.WriteAttributeString("href", "http://www.w3.org/1999/xlink", "data:;base64," + Convert.ToBase64String(stream.ToArray()));
        doc.WriteEndElement();
      }
    }
  }



}
