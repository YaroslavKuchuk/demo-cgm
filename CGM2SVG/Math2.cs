using System;
using System.Diagnostics;

namespace CGM2SVG
{
  internal sealed class Math2
  {
    // Fields
    public static double Accuracy = 1E-14;

    // Methods
    public static double Angle(double Δx1, double Δy1, double Δx2, double Δy2)
    {
      double num2 = Math.Atan2(Δy2, Δx2) - Math.Atan2(Δy1, Δx1);
      if (num2 < 0.0)
      {
        num2 += 6.2831853071795862;
      }
      return num2;
    }

    [Conditional("DEBUG")]
    public static void CheckNaN(params object[] obj)
    {
      object[] objArray = obj;
      for (int i = 0; i < objArray.Length; i++)
      {
        if (double.IsNaN(Convert.ToDouble(objArray[i])))
        {
          throw new ArgumentOutOfRangeException("NaN found");
        }
      }
    }

    public static double CompareAngles(double Δx1, double Δy1, double Δx2, double Δy2)
    {
      return ((Δy1 * Δx2) - (Δx1 * Δy2));
    }

    public static double Distance(double Δx, double Δy)
    {
      return Math.Sqrt((Δx * Δx) + (Δy * Δy));
    }

    public static double Distance(double x1, double y1, double x2, double y2)
    {
      return Distance(x1 - x2, y1 - y2);
    }

    public static double[,] InvertMatrix(double[,] A)
    {
      ValidateMatrix(A);
      switch (A.GetLength(0))
      {
        case 1:
          return new double[,] { { (1.0 / A[0, 0]) } };

        case 2:
          {
            double num = Convert.ToDouble(Δ(A));
            return new double[,] { { (A[1, 1] / num), (-A[0, 1] / num) }, { (-A[1, 0] / num), (A[0, 0] / num) } };
          }
      }
      throw new NotImplementedException();
    }

    public static bool Is90DegIntersection(double Δx1, double Δy1, double Δx2, double Δy2)
    {
      return IsNul((Δx1 * Δx2) + (Δy1 * Δy2));
    }

    public static bool IsNul(double x)
    {
      return ((-Accuracy < x) && (x < Accuracy));
    }

    public static Array MultiplyMatrix(Array A, Array B)
    {
      if (((A.Rank != 2) | (B.Rank != 2)) || (A.GetLength(1) != B.GetLength(0)))
      {
        throw new Exception("Invalid or incompatible matrices");
      }
      int length = A.GetLength(0);
      int num = A.GetLength(1);
      int num2 = B.GetLength(1);
      Array o = Array.CreateInstance(typeof(double), length, num2);
      int num10 = length - 1;
      //for (int i = 0; i <= num10; i++)
      //{
      //  int num9 = num2 - 1;
      //  for (int j = 0; j <= num9; j++)
      //  {
      //    double num6 = 0.0;
      //    int num8 = num - 1;
      //    for (int k = 0; k <= num8; k++)
      //    {
      //      num6 = Convert.ToDouble(ObjectType.AddObj(num6, ObjectType.MulObj(LateBinding.LateIndexGet(A, new object[] { i, k }, null), LateBinding.LateIndexGet(B, new object[] { k, j }, null))));
      //    }
      //    LateBinding.LateIndexSet(o, new object[] { i, j, num6 }, null);
      //  }
      //}
      return o;
    }

    public static double[] MultiplyMatrix(Array A, double[] B)
    {
      if ((A.Rank != 2) || (A.GetLength(1) != B.Length))
      {
        throw new Exception("Invalid or incompatible matrices");
      }
      double[] numArray = new double[A.GetUpperBound(0) + 1];
      //int num5 = numArray.Length - 1;
      //for (int i = 0; i <= num5; i++)
      //{
      //  double num2 = 0.0;
      //  int num4 = B.Length - 1;
      //  for (int j = 0; j <= num4; j++)
      //  {
      //    num2 = Convert.ToDouble(ObjectType.AddObj(num2, ObjectType.MulObj(LateBinding.LateIndexGet(A, new object[] { i, j }, null), B[j])));
      //  }
      //  numArray[i] = num2;
      //}
      return numArray;
    }

    public static double[] SolveSystem2(double[,] A, double[] B)
    {
      if (((A.GetLength(0) != 2) | (A.GetLength(1) != 2)) | (B.Length != 2))
      {
        throw new Exception("Size is not 2");
      }
      double num = Convert.ToDouble(Δ(A));
      double num2 = Convert.ToDouble(Δ(new double[,] { { B[0], A[0, 1] }, { B[1], A[1, 1] } }));
      double num3 = Convert.ToDouble(Δ(new double[,] { { A[0, 0], B[0] }, { A[1, 0], B[1] } }));
      return new double[] { (num2 / num), (num3 / num) };
    }

    public static double Sqr(double x)
    {
      return (x * x);
    }

    private static void ValidateMatrix(double[,] A)
    {
      int length = A.GetLength(0);
      if (length != A.GetLength(1))
      {
        throw new Exception("Matrix must be squared");
      }
      if (length < 1)
      {
        throw new Exception("Matrix Size > 0");
      }
    }

    public static object Δ(double[,] A)
    {
      ValidateMatrix(A);
      switch (A.GetLength(0))
      {
        case 1:
          return A[0, 0];

        case 2:
          return ((A[0, 0] * A[1, 1]) - (A[0, 1] * A[1, 0]));

        case 3:
          {
            double num = Convert.ToDouble(Δ(new double[,] { { A[1, 1], A[1, 2] }, { A[2, 1], A[2, 2] } }));
            double num2 = Convert.ToDouble(Δ(new double[,] { { A[1, 0], A[1, 2] }, { A[2, 0], A[2, 2] } }));
            double num3 = Convert.ToDouble(Δ(new double[,] { { A[1, 0], A[1, 1] }, { A[2, 0], A[2, 1] } }));
            return ((num - num2) + num3);
          }
      }
      throw new NotImplementedException();
    }
  }



}
