using System.Drawing;
using System.Xml;

namespace CGM2SVG
{
  internal class BSpline
  {

    public PointF[] CtrlPts;
    public float[] Knots;
    public PointF[] nPts = new PointF[7];
    public int NumCtrlPts;
    public double ParamEnd;
    public double ParamStart;
    public int SPlineOrder;
    public double[] Weights;


    public object ConvertSplineToBezier()
    {
      return new object();
    }

    public object ConvertToBezier()
    {
      return new object();
    }

    public void UpdateSVG(XmlTextWriter doc, SVGContext mycontext)
    {
    }
  }


}
