using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Scanner
{
  public class CGMScanner
  {

    private static byte[] InreaseArray(byte[] a1, int delta)
    {
      int length = a1.Length + delta;
      byte[] buffer = new byte[length]; //Array.CreateInstance(typeof(byte), length);
      a1.CopyTo(buffer, 0);
      return buffer;
    }

    public static void ReadFile(Stream r, IElementHandler h)
    {
      ArrayList list = new ArrayList();
      int num = (int)r.Length;
      do
      {
        int num5;
        int num6 = (int)r.Position;
        int num2 = ReadWordFromFile(r);
        if ((num2 & 0x1f) == 0x1f)
        {
          num5 = ReadWordFromFile(r);
        }
        else
        {
          num5 = num2 & 0x1f;
        }
        int num4 = (num2 & 0xfe0) / 0x20;
        int num3 = (num2 & 0xf000) / 0x1000;
        byte[] buffer = new byte[0];
        while ((num5 & 0x8000) != 0)
        {
          num5 &= 0x7fff;
          buffer = InreaseArray(buffer, num5);
          r.Read(buffer, buffer.Length - num5, num5);
          if ((num5 & 1) != 0)
          {
            r.ReadByte();
          }
          num5 = ReadWordFromFile(r);
        }
        buffer = InreaseArray(buffer, num5);
        r.Read(buffer, buffer.Length - num5, num5);
        if ((num5 & 1) != 0)
        {
          r.ReadByte();
        }
        CGMElement el = new CGMElement
        {
          ElementId = num4,
          ElementClass = num3,
          Offset = num6,
          Data = buffer
        };
        h.Element(el);
      }
      while (r.Position < num);
    }

    private static int ReadWordFromFile(Stream r)
    {
      return ((r.ReadByte() * 0x100) + r.ReadByte());
    }
  }



}
