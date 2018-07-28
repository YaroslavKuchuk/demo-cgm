using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Scanner
{
  public class CGMBinaryReader : IDisposable
  {
    public readonly Stream BaseStream;
    public readonly CGMBinaryContext context;

    public CGMBinaryReader(Stream stream) : this(stream, new CGMBinaryContext())
    {
    }

    public CGMBinaryReader(Stream stream, CGMBinaryContext context)
    {
      this.BaseStream = stream;
      this.context = context;
    }

    public void Dispose()
    {
      this.BaseStream.Close();
    }

    public byte[] ReadBS(int n)
    {
      if (n < 0)
      {
        n = (int)this.BaseStream.Length - (int)this.BaseStream.Position;
      }
      byte[] buffer = new byte[n + 1];
      this.BaseStream.Read(buffer, 0, n);
      return buffer;
    }

    public uint ReadCCO()
    {
      return this.ReadUI_n(this.context.ColorPrecision);
    }

    public uint[] ReadCD()
    {
      if (this.context.Is4ColorModel)
      {
        return new uint[] { this.ReadCCO(), this.ReadCCO(), this.ReadCCO(), this.ReadCCO() };
      }
      return new uint[] { this.ReadCCO(), this.ReadCCO(), this.ReadCCO() };
    }

    public char ReadChar()
    {
      byte[] buffer = new byte[] { (byte)this.BaseStream.ReadByte() };
      return this.CurrentEnc.GetChars(buffer)[0];
    }

    public char[] ReadChars(int n)
    {
      byte[] buffer = new byte[(n - 1) + 1];
      this.BaseStream.Read(buffer, 0, n);
      return this.CurrentEnc.GetChars(buffer);
    }

    public uint ReadCI()
    {
      return this.ReadUI_n(this.context.ColorIndexPrecision);
    }

    public object ReadCO()
    {
      if (this.context.isDirectColor)
      {
        return this.ReadCD();
      }
      return this.ReadCI();
    }

    public byte[] ReadD()
    {
      byte[] buffer = new byte[0];
      int num = this.BaseStream.ReadByte();
      if (num < 0xff)
      {
        buffer = new byte[(num - 1) + 1];
        this.BaseStream.Read(buffer, 0, buffer.Length);
        return buffer;
      }
      int num2 = 0;
      bool flag = false;

       

      do
      {
        flag = false;
        num = Convert.ToInt32(this.ReadUI_n(0x10));
        if ((num & 0x8000) != 0)
        {
          num &= 0x7fff;
          flag = true;
        }

        var newBuffer = new byte[((num2 + num) - 1) + 1];
        newBuffer.CopyTo(buffer, newBuffer.Length + buffer.Length);
        //buffer = Utils.CopyArray((Array)buffer, new byte[((num2 + num) - 1) + 1]);

        this.BaseStream.Read(buffer, num2, num);
        num2 += num;
      }
      while (flag);
      return buffer;
    }

    public int ReadE()
    {
      return this.ReadSI_n(0x10);
    }

    public double ReadFP32()
    {
      double num;
      byte[] buffer = new byte[4];
      this.BaseStream.Read(buffer, 0, 4);
      Array.Reverse(buffer);
      MemoryStream stream = new MemoryStream(buffer);
      BinaryReader reader = new BinaryReader(stream);
      try
      {
        num = reader.ReadSingle();
      }
      finally
      {
        reader.Close();
        stream.Close();
      }
      return num;
    }

    public double ReadFP64()
    {
      double num;
      byte[] buffer = new byte[8];
      this.BaseStream.Read(buffer, 0, 8);
      Array.Reverse(buffer);
      MemoryStream stream = new MemoryStream(buffer);
      BinaryReader reader = new BinaryReader(stream);
      try
      {
        num = reader.ReadDouble();
      }
      finally
      {
        reader.Close();
        stream.Close();
      }
      return num;
    }

    public double ReadFX32()
    {
      int num3 = this.ReadSI_n(0x10);
      uint num = this.ReadUI_n(0x10);
      return (num3 + (((double)Convert.ToInt32(num)) / 65536.0));
    }

    public double ReadFX64()
    {
      int num3 = this.ReadSI_n(0x20);
      uint num = this.ReadUI_n(0x20);
      return (num3 + (((double)Convert.ToInt64(num)) / 4294967296));
    }

    public int ReadI()
    {
      return this.ReadSI_n(this.context.IntegerPrecision);
    }

    public int ReadIX()
    {
      return this.ReadSI_n(this.context.IndexPrecision);
    }

    public int ReadN()
    {
      return this.ReadSI_n(this.context.NamePrecision);
    }

    public Point ReadP()
    {
      return new Point(Convert.ToDecimal(this.ReadVDC()), Convert.ToDecimal(this.ReadVDC()));
    }

    public double ReadR()
    {
      double num = 0;//Double.NaN; //TODO
      switch (this.context.RealType)
      {
        case CGMBinaryContext.RealTypeEnum.Float32:
          return this.ReadFP32();

        case CGMBinaryContext.RealTypeEnum.Float64:
          return this.ReadFP64();

        case CGMBinaryContext.RealTypeEnum.Fixed32:
          return this.ReadFX32();

        case CGMBinaryContext.RealTypeEnum.Fixed64:
          return this.ReadFX64();
      }
      return num;
    }

    public string ReadS()
    {
      StringBuilder builder = new StringBuilder();
      int n = this.BaseStream.ReadByte();
      if (n < 0xff)
      {
        builder.Append(this.ReadChars(n));
      }
      else
      {
        bool flag = false;
        do
        {
          flag = false;
          n = Convert.ToInt32(this.ReadUI_n(0x10));
          if ((n & 0x8000) != 0)
          {
            n &= 0x7fff;
            flag = true;
          }
          builder.Append(this.ReadChars(n));
        }
        while (flag);
      }
      return builder.ToString();
    }

    public SDR ReadSDR()
    {
      int num2 = this.BaseStream.ReadByte();
      if (num2 == 0xff)
      {
        num2 = Convert.ToInt32(this.ReadUI_n(0x10));
        if ((num2 & 0x8000) != 0)
        {
          throw new NotImplementedException("Splited SDR not supported");
        }
      }
      long num = this.BaseStream.Position + num2;
      ArrayList list = new ArrayList();
      while (this.BaseStream.Position < num)
      {
        SDR.Member member = new SDR.Member
        {
          DataType = (SDR.DataTypeIndex)Enum.ToObject(typeof(SDR.DataTypeIndex), this.ReadI()) //TODO: to metod
          //DataType = this.ReadI()
        };
        int num3 = this.ReadI();
        ArrayList list2 = new ArrayList();
        int num5 = num3 - 1;
        for (int i = 0; i <= num5; i++)
        {
          list2.Add(RuntimeHelpers.GetObjectValue(this.ReadSDRItem(member.DataType)));
        }
        member.Items = list2.ToArray();
        list.Add(member);
      }
      return new SDR { Members = (SDR.Member[])list.ToArray(typeof(SDR.Member)) };
    }

    private object ReadSDRItem(SDR.DataTypeIndex t)
    {
      object obj2 = new object();//TODO
      switch (t)
      {
        case SDR.DataTypeIndex.CI:
          return this.ReadCI();

        case SDR.DataTypeIndex.CD:
          return this.ReadCD();

        case SDR.DataTypeIndex.N:
          return this.ReadN();

        case SDR.DataTypeIndex.E:
          return this.ReadE();

        case SDR.DataTypeIndex.I:
          return this.ReadI();

        case SDR.DataTypeIndex.reserved:
        case SDR.DataTypeIndex.BS:
        case SDR.DataTypeIndex.CL:
          return obj2;

        case SDR.DataTypeIndex.IF8:
          return this.ReadSI_n(8);

        case SDR.DataTypeIndex.IF16:
          return this.ReadSI_n(0x10);

        case SDR.DataTypeIndex.IF32:
          return this.ReadSI_n(0x20);

        case SDR.DataTypeIndex.IX:
          return this.ReadIX();

        case SDR.DataTypeIndex.R:
          return this.ReadR();

        case SDR.DataTypeIndex.S:
        case SDR.DataTypeIndex.SF:
          return this.ReadS();

        case SDR.DataTypeIndex.VC:
          return this.ReadVC();

        case SDR.DataTypeIndex.VDC:
          return this.ReadVDC();

        case SDR.DataTypeIndex.CCO:
          return this.ReadCCO();

        case SDR.DataTypeIndex.UI8:
          return this.ReadUI_n(8);

        case SDR.DataTypeIndex.UI32:
          return this.ReadUI_n(0x20);

        case SDR.DataTypeIndex.UI16:
          return this.ReadUI_n(0x10);
      }
      return obj2;
    }

    public int ReadSI_n(int precision)
    {
      int num2 = precision / 8;
      byte[] buffer = new byte[4];
      int index = 4 - num2;
      this.BaseStream.Read(buffer, index, num2);
      if ((num2 < 4) && ((buffer[index] & 0x80) != 0))
      {
        int num5 = index - 1;
        for (int i = 0; i <= num5; i++)
        {
          buffer[i] = 0xff;
        }
      }
      return Convert.ToInt32((((buffer[0] << 0x18) | (buffer[1] << 0x10)) | (buffer[2] << 8)) | buffer[3]);
    }

    public object ReadSS(bool absolute)
    {
      if (absolute)
      {
        return this.ReadVDC();
      }
      return this.ReadR();
    }

    public uint ReadUI_n(int precision)
    {
      int num2 = precision / 8;
      byte[] buffer = new byte[4];
      int num = 4 - num2;
      this.BaseStream.Read(buffer, num, num2);
      return Convert.ToUInt32((((buffer[0] << 0x18) | (buffer[1] << 0x10)) | (buffer[2] << 8)) | buffer[3]);
    }

    public byte ReadUI8()
    {
      return (byte)this.BaseStream.ReadByte();
    }

    public uint ReadUInt32()
    {
      return this.ReadUI_n(0x20);
    }

    public object ReadVC()
    {
      if (this.context.isRealVC)
      {
        return this.ReadR();
      }
      return this.ReadI();
    }

    public object ReadVDC()
    {
      object obj2 = new object();//TODO
      if (!this.context.isRealVDC)
      {
        return this.ReadSI_n(this.context.VDCIntegerPrecision);
      }
      switch (this.context.VDCRealType)
      {
        case CGMBinaryContext.RealTypeEnum.Float32:
          return this.ReadFP32();

        case CGMBinaryContext.RealTypeEnum.Float64:
          return this.ReadFP64();

        case CGMBinaryContext.RealTypeEnum.Fixed32:
          return this.ReadFX32();

        case CGMBinaryContext.RealTypeEnum.Fixed64:
          return this.ReadFX64();
      }
      return obj2;
    }

    public Point ReadVP()
    {
      return new Point(Convert.ToDecimal(this.ReadVC()), Convert.ToDecimal(this.ReadVC()));
    }

    // Properties
    private Encoding CurrentEnc {
      get {
        if (this.context.CharSetType == 4)
        {
          return Encoding.UTF8;
        }
        return Enc8bit.getInstance();
      }
    }

    public bool EOF {
      get {
        return (this.BaseStream.Position >= this.BaseStream.Length);
      }
    }
  }



}
