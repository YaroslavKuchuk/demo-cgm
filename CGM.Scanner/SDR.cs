namespace CGM.Scanner
{
  public class SDR
  {
    public Member[] Members;

    public enum DataTypeIndex
    {
      Unknown,
      SDR,
      CI,
      CD,
      N,
      E,
      I,
      reserved,
      IF8,
      IF16,
      IF32,
      IX,
      R,
      S,
      SF,
      VC,
      VDC,
      CCO,
      UI8,
      UI32,
      BS,
      CL,
      UI16
    }

    public class Member
    {
      public SDR.DataTypeIndex DataType;
      public object[] Items;
    }
  }



}
