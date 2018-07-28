using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CGM2SVG
{
  public class PNGMaker
  {
    // Methods
    public static void Convert(Image imgtiff, Stream sout)
    {
      imgtiff.Save(sout, ImageFormat.Png);
      imgtiff.Dispose();
    }

    public static void Convert(Stream sin, Stream sout)
    {
      Image image = Image.FromStream(sin);
      image.Save(sout, ImageFormat.Png);
      image.Dispose();
    }

    public static void Convert(string source, string dest)
    {
      Image image = Image.FromFile(source);
      image.Save(dest, ImageFormat.Png);
      image.Dispose();
    }
  }

}
