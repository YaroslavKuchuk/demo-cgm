using System;
using System.Diagnostics;
using System.IO;

namespace CGM2SVG
{
  public class TIFFMaker
  {
    // Fields
    public int ImageHeight;
    public int ImageWidth;
    public int ResolutionX = 300;
    public int ResolutionY = 300;
    public byte[] T6Data;

    // Methods
    public void ProduceBilevel(Stream s)
    {
      this.WriteBytes(s, new byte[] { 0x49, 0x49, 0x2a, 0 });
      this.WriteLong(s, this.T6Data.Length + 8);
      this.WriteBytes(s, this.T6Data);
      int i = 0x13;
      object obj2 = ((this.T6Data.Length + 8) + 2) + (i * 12);
      this.WriteInteger(s, i);
      this.WriteBytes(s, new byte[] { 0xfe, 0, 4, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0, 1, 4, 0, 1, 0, 0, 0 });
      this.WriteLong(s, this.ImageWidth);
      this.WriteBytes(s, new byte[] { 1, 1, 4, 0, 1, 0, 0, 0 });
      this.WriteLong(s, this.ImageHeight);
      this.WriteBytes(s, new byte[] { 2, 1, 3, 0, 1, 0, 0, 0, 1, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 3, 1, 3, 0, 1, 0, 0, 0, 4, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 6, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 10, 1, 3, 0, 1, 0, 0, 0, 1, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x11, 1, 4, 0, 1, 0, 0, 0, 8, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x15, 1, 3, 0, 1, 0, 0, 0, 1, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x16, 1, 4, 0, 1, 0, 0, 0 });
      this.WriteLong(s, this.ImageHeight);
      this.WriteBytes(s, new byte[] { 0x17, 1, 4, 0, 1, 0, 0, 0 });
      this.WriteLong(s, this.T6Data.Length);
      this.WriteBytes(s, new byte[] { 0x1a, 1, 5, 0, 1, 0, 0, 0 });
      this.WriteLong(s, Convert.ToInt32(obj2) + 4); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 4)));
      this.WriteBytes(s, new byte[] { 0x1b, 1, 5, 0, 1, 0, 0, 0 });
      this.WriteLong(s, Convert.ToInt32(obj2) + 12); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 12)));
      this.WriteBytes(s, new byte[] { 0x25, 1, 4, 0, 1, 0, 0, 0, 5, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 40, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x29, 1, 3, 0, 2, 0, 0, 0, 0, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x31, 1, 4, 0, 1, 0, 0, 0 });
      this.WriteLong(s, Convert.ToInt32(obj2) + 20); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 20)));
      this.WriteBytes(s, new byte[] { 0x47, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x48, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
      this.WriteBytes(s, new byte[] { 0x53, 0x66, 0x61, 120 });
      this.WriteLong(s, this.ResolutionX);
      this.WriteLong(s, 1);
      this.WriteLong(s, this.ResolutionY);
      this.WriteLong(s, 1);
      this.WriteBytes(s, new byte[] { 0x20, 0x20, 0x20, 0x20, 0 });
    }

    public void ProduceBilevel(string file)
    {
      FileStream stream = new FileStream(file, FileMode.Create);//FileStream stream = new FileStream(file, 2, 2);
      try
      {
        this.ProduceBilevel((Stream)stream);
      }
      finally
      {
        stream.Close();
      }
    }

    public void ProduceContonal(Stream s)
    {
      try
      {
        this.WriteBytes(s, new byte[] { 0x49, 0x49, 0x2a, 0 });
        this.WriteLong(s, this.T6Data.Length + 8);
        this.WriteBytes(s, this.T6Data);
        int i = 0x13;
        object obj2 = ((this.T6Data.Length + 8) + 2) + (i * 12);
        this.WriteBytes(s, new byte[] { 0, 8, 0, 8, 0, 8 });
        this.WriteInteger(s, i);
        this.WriteBytes(s, new byte[] { 0xfe, 0, 4, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageWidth);
        this.WriteBytes(s, new byte[] { 1, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageHeight);
        this.WriteBytes(s, new byte[] { 2, 1, 3, 0, 3, 0, 0, 0 });
        this.WriteLong(s, this.T6Data.Length);
        this.WriteBytes(s, new byte[] { 3, 1, 3, 0, 1, 0, 0, 0, 4, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 6, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x11, 1, 4, 0, 1, 0, 0, 0, 8, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x15, 1, 3, 0, 1, 0, 0, 0, 3, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x16, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageHeight);
        this.WriteBytes(s, new byte[] { 0x17, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.T6Data.Length);
        this.WriteBytes(s, new byte[] { 0x1a, 1, 5, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) + 4); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 4)));
        this.WriteBytes(s, new byte[] { 0x1b, 1, 5, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) + 12); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 12)));
        this.WriteBytes(s, new byte[] { 0x25, 1, 4, 0, 1, 0, 0, 0, 5, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 40, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x29, 1, 3, 0, 2, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x31, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) + 20); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 20)));
        this.WriteBytes(s, new byte[] { 0x47, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x48, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x53, 0x66, 0x61, 120 });
        this.WriteLong(s, this.ResolutionX);
        this.WriteLong(s, 1);
        this.WriteLong(s, this.ResolutionY);
        this.WriteLong(s, 1);
        this.WriteBytes(s, new byte[] { 0x20, 0x20, 0x20, 0x20, 0 });
      }
      catch (Exception exception1)
      {
       // ProjectData.SetProjectError(exception1);
        Trace.WriteLine("The error is " + exception1.Message);
      //  ProjectData.ClearProjectError();
      }
    }

    public void ProduceGrayScale(Stream s)
    {
      try
      {
        this.WriteBytes(s, new byte[] { 0x49, 0x49, 0x2a, 0 });
        this.WriteLong(s, this.T6Data.Length + 8);
        this.WriteBytes(s, this.T6Data);
        int i = 0x13;
        object obj2 = ((this.T6Data.Length + 8) + 2) + (i * 12);
        this.WriteBytes(s, new byte[] { 0, 8, 0, 8, 0, 8 });
        this.WriteInteger(s, i);
        this.WriteBytes(s, new byte[] { 0xfe, 0, 4, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageWidth);
        this.WriteBytes(s, new byte[] { 1, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageHeight);
        this.WriteBytes(s, new byte[] { 2, 1, 3, 0, 1, 0, 0, 0, 8, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 3, 1, 3, 0, 1, 0, 0, 0, 4, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 6, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x11, 1, 4, 0, 1, 0, 0, 0, 8, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x16, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.ImageHeight);
        this.WriteBytes(s, new byte[] { 0x17, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, this.T6Data.Length);
        this.WriteBytes(s, new byte[] { 0x1a, 1, 5, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) +  4);//this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 4)));
        this.WriteBytes(s, new byte[] { 0x1b, 1, 5, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) + 12);// this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 12)));
        this.WriteBytes(s, new byte[] { 0x25, 1, 4, 0, 1, 0, 0, 0, 5, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 40, 1, 3, 0, 1, 0, 0, 0, 2, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x29, 1, 3, 0, 2, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x31, 1, 4, 0, 1, 0, 0, 0 });
        this.WriteLong(s, Convert.ToInt32(obj2) + 20); //this.WriteLong(s, IntegerType.FromObject(ObjectType.AddObj(obj2, 20)));
        this.WriteBytes(s, new byte[] { 0x47, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x48, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
        this.WriteBytes(s, new byte[] { 0x53, 0x66, 0x61, 120 });
        this.WriteLong(s, this.ResolutionX);
        this.WriteLong(s, 1);
        this.WriteLong(s, this.ResolutionY);
        this.WriteLong(s, 1);
        this.WriteBytes(s, new byte[] { 0x20, 0x20, 0x20, 0x20, 0 });
      }
      catch (Exception exception1)
      {
      //  ProjectData.SetProjectError(exception1);
        Trace.WriteLine("The error is " + exception1.Message);
     //   ProjectData.ClearProjectError();
      }
    }

    private void WriteBytes(Stream s, byte[] b)
    {
      s.Write(b, 0, b.Length);
    }

    private void WriteInteger(Stream s, int i)
    {
      s.WriteByte((byte)(i & 0xff));
      s.WriteByte((byte)((i & 0xff00) / 0x100));
    }

    private void WriteLong(Stream s, int i)
    {
      s.WriteByte((byte)(i & 0xff));
      s.WriteByte((byte)((i & 0xff00) / 0x100));
      s.WriteByte((byte)((i & 0xff0000) / 0x10000));
      s.WriteByte((byte)((i & -16777216) / 0x1000000));
    }
  }



}
