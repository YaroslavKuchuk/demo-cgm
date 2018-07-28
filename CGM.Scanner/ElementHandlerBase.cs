using System;
using System.IO;

namespace CGM.Scanner
{
  public abstract class ElementHandlerBase : IElementHandler
  {

    private CGMBinaryContext context = new CGMBinaryContext();

    public void Element(CGMElement el)
    {
      if (el.ElementClass == 1)
      {
        using (CGMBinaryReader reader = this.GetReader(el))
        {
          this.ProcessClass1(el.ElementId, reader);
          goto Label_0079;
        }
      }
      if (el.ElementClass == 2)
      {
        using (CGMBinaryReader reader2 = this.GetReader(el))
        {
          this.ProcessClass2(el.ElementId, reader2);
        }
      }
      else if (el.ElementClass == 3)
      {
        using (CGMBinaryReader reader3 = this.GetReader(el))
        {
          this.ProcessClass3(el.ElementId, reader3);
        }
      }
      Label_0079:
      this.OnElement(el);
    }

    protected CGMBinaryReader GetReader(CGMElement el)
    {
      return new CGMBinaryReader(new MemoryStream(el.Data), this.context);
    }

    protected abstract void OnElement(CGMElement el);
    private void ProcessClass1(int id, CGMBinaryReader b)
    {
      switch (id)
      {
        case 3:
          if (b.ReadE() != 0)
          {
            b.context.isRealVDC = true;
            break;
          }
          b.context.isRealVDC = false;
          break;

        case 4:
          {
            int num2 = b.ReadI();
            b.context.IntegerPrecision = num2;
            break;
          }
        case 5:
          {
            int num3 = b.ReadE();
            int num4 = b.ReadI();
            int num5 = b.ReadI();
            if (num3 != 0)
            {
              if (num5 >= 0x20)
              {
                b.context.RealType = CGMBinaryContext.RealTypeEnum.Fixed64;
              }
              else
              {
                b.context.RealType = CGMBinaryContext.RealTypeEnum.Fixed32;
              }
              break;
            }
            if (num5 < 0x20)
            {
              b.context.RealType = CGMBinaryContext.RealTypeEnum.Float32;
              break;
            }
            b.context.RealType = CGMBinaryContext.RealTypeEnum.Float64;
            break;
          }
        case 6:
          {
            int num6 = b.ReadI();
            b.context.IndexPrecision = num6;
            break;
          }
        case 7:
          {
            int num7 = b.ReadI();
            b.context.ColorPrecision = num7;
            break;
          }
        case 8:
          {
            int num8 = b.ReadI();
            b.context.ColorIndexPrecision = num8;
            break;
          }
        case 14:
          {
            int num9 = b.ReadE();
            b.context.CharSetType = num9;
            break;
          }
        case 0x13:
          b.context.ColorModel = (CGMBinaryContext.ColorModelEnum)Enum.ToObject(typeof(CGMBinaryContext.ColorModelEnum), b.ReadIX());//TODO: to metod
          break;
      }
    }

    private void ProcessClass2(int id, CGMBinaryReader b)
    {
      if (id == 2)
      {
        int num = b.ReadE();
        b.context.isDirectColor = num > 0;
      }
    }

    private void ProcessClass3(int id, CGMBinaryReader b)
    {
      switch (id)
      {
        case 1:
          {
            int num = b.ReadI();
            b.context.VDCIntegerPrecision = num;
            break;
          }
        case 2:
          {
            int num2 = b.ReadE();
            int num3 = b.ReadI();
            int num4 = b.ReadI();
            if (num2 != 0)
            {
              if (num4 >= 0x20)
              {
                b.context.VDCRealType = CGMBinaryContext.RealTypeEnum.Fixed64;
              }
              else
              {
                b.context.VDCRealType = CGMBinaryContext.RealTypeEnum.Fixed32;
              }
              break;
            }
            if (num4 < 0x20)
            {
              b.context.VDCRealType = CGMBinaryContext.RealTypeEnum.Float32;
              break;
            }
            b.context.VDCRealType = CGMBinaryContext.RealTypeEnum.Float64;
            break;
          }
      }
    }

    protected void SetContext(CGMBinaryContext context)
    {
      this.context = context;
    }
  }



}
