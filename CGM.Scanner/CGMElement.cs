namespace CGM.Scanner
{
  public class CGMElement
  {
    public byte[] Data;
    public int ElementClass;
    public int ElementId;
    public int Offset;

    public override string ToString()
    {
      return string.Format("offset: {0}, class: {1}, id: {2}, datalen: {3}", new object[] { this.Offset, this.ElementClass, this.ElementId, this.Data.Length });
    }
  }

}
