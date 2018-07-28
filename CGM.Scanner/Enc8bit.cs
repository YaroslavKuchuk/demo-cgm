using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Scanner
{
  internal class Enc8bit : Encoding
  {

    private static readonly Lazy<Enc8bit> lazy = new Lazy<Enc8bit>(() => new Enc8bit());

    public static Enc8bit getInstance()
    {
      return lazy.Value;
    }

    public override int GetByteCount(char[] chars, int index, int count)
    {
      throw new NotImplementedException();
    }

    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
      throw new NotImplementedException();
    }

    public override int GetCharCount(byte[] bytes, int index, int count)
    {
      return count;
    }

    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
      int num = 0; //TODO
      int num3 = byteCount - 1;
      for (int i = 0; i <= num3; i++)
      {
        chars[i + charIndex] = (char)(bytes[byteIndex + i]);//Strings.ChrW(bytes[byteIndex + i]);
      }
      return num;
    }

    public override int GetMaxByteCount(int charCount)
    {
      throw new NotImplementedException();
    }

    public override int GetMaxCharCount(int byteCount)
    {
      throw new NotImplementedException();
    }
  }



}
