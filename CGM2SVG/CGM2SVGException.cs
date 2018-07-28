using System;

namespace CGM2SVG
{
  public class CGM2SVGException : Exception
  {

    public long Offset;

    public CGM2SVGException(string msg, long offset, Exception innerException) : base(msg, innerException)
    {
      this.Offset = offset;
    }
  }


}
