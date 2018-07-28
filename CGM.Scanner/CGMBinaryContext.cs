namespace CGM.Scanner
{
  public class CGMBinaryContext
  {
    // Fields
    public int CharSetType = 0;
    public int ColorIndexPrecision = 8;
    public ColorModelEnum ColorModel = ColorModelEnum.RGB;
    public int ColorPrecision = 8;
    public int IndexPrecision = 0x10;
    public int IntegerPrecision = 0x10;
    public bool isDirectColor = false;
    public bool isRealVC = false;
    public bool isRealVDC = false;
    public int NamePrecision = 0x10;
    public RealTypeEnum RealType = RealTypeEnum.Fixed32;
    public int VDCIntegerPrecision = 0x10;
    public RealTypeEnum VDCRealType = RealTypeEnum.Fixed32;

    // Properties
    public bool Is4ColorModel {
      get {
        return (this.ColorModel == ColorModelEnum.CMYK);
      }
    }

    // Nested Types
    public enum ColorModelEnum
    {
      CIELAB = 2,
      CIELUV = 3,
      CMYK = 4,
      RGB = 1,
      RGB_rel = 5
    }

    public enum RealTypeEnum
    {
      Float32,
      Float64,
      Fixed32,
      Fixed64
    }
  }



}
