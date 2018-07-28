using System.Drawing;

namespace CGM2SVG
{
  public class TranslateOptions
  {
    // Fields
    public bool EnlargeInBox;
    public bool FitInBox;
    public bool SingleBackground;
    public float SizeScale;
    public string SizeUnits;
    public bool SpecifySize;
    public bool StrictMode;
    public RectangleF ViewBox;

    // Methods
    public TranslateOptions()
    {
      this.FitInBox = true;
      this.EnlargeInBox = true;
      this.SpecifySize = false;
      this.SizeUnits = "px";
      this.SizeScale = 1f;
      this.StrictMode = false;
      this.SingleBackground = true;
      this.ViewBox = RectangleF.Empty;
      this.FitInBox = false;
      this.EnlargeInBox = false;
    }

    public TranslateOptions(RectangleF r)
    {
      this.FitInBox = true;
      this.EnlargeInBox = true;
      this.SpecifySize = false;
      this.SizeUnits = "px";
      this.SizeScale = 1f;
      this.StrictMode = false;
      this.SingleBackground = true;
      this.ViewBox = r;
    }

    public TranslateOptions(PointF TopLeft, PointF BottomRight)
    {
      this.FitInBox = true;
      this.EnlargeInBox = true;
      this.SpecifySize = false;
      this.SizeUnits = "px";
      this.SizeScale = 1f;
      this.StrictMode = false;
      this.SingleBackground = true;
      RectangleF ef = new RectangleF(TopLeft.X, TopLeft.Y, BottomRight.X - TopLeft.Y, BottomRight.Y - TopLeft.Y);
      this.ViewBox = ef;
    }
  }



}
