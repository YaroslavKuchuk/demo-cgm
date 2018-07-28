using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CGM2SVG
{
  public class BitMapMaker
  {
    private int bheight;
    private Bitmap bmap;
    private int bwidth;
    private Stream pixelstream;
    private PixelFormat pixformat;
    private MemoryStream retstream = new MemoryStream();

    public BitMapMaker(Stream datastream, int width, int height, PixelFormat pixf)
    {
      this.pixelstream = datastream;
      this.bmap = new Bitmap(width, height, pixf);
      this.bwidth = width;
      this.bheight = height;
      this.pixformat = pixf;
    }

    public object CreateImage()
    {
      object obj2 = new object();
      int num10 = this.bheight - 1;
      for (int i = 0; i <= num10; i++)
      {
        int num9 = this.bwidth - 1;
        for (int j = 0; j <= num9; j++)
        {
          int num = 0;
          int num2 = 0;
          int num4 = 0;
          switch (this.pixformat)
          {
            case PixelFormat.Format24bppRgb:
              num4 = this.pixelstream.ReadByte();
              num2 = this.pixelstream.ReadByte();
              num = this.pixelstream.ReadByte();
              break;

            case PixelFormat.Format48bppRgb:
              num4 = this.pixelstream.ReadByte() << 8;
              num4 |= this.pixelstream.ReadByte();
              num2 = this.pixelstream.ReadByte() << 8;
              num2 |= this.pixelstream.ReadByte();
              num = this.pixelstream.ReadByte() << 8;
              num |= this.pixelstream.ReadByte();
              break;
          }
          try
          {
            Color color = Color.FromArgb(num2, num, num4);
            this.bmap.SetPixel(j, i, color);
          }
          catch (Exception exception1)
          {
            //ProjectData.SetProjectError(exception1); //TODO
            Exception exception = exception1;
            throw exception;
          }
        }
      }
      this.bmap.Save((Stream)this.retstream, ImageFormat.Bmp);
      return obj2;
    }

    public Stream GetFinalStream()
    {
      return this.retstream;
    }
  }



}
