using System;
using System.Collections;

namespace Model.OperatorsHelper
{
    public static class Convolution
    {
        public static byte Process(this byte[,] pix, int i, int j, int[,] oper)
        {
            int result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result.ToByte();
        }

        public static byte Process(this byte[,] pix, int i, int j, double[,] oper)
        {
            double result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result.ToByte();
        }

        private static double _epsilon = Math.Pow(10, -8);
        public static byte Process(this byte[,] pix, int i, int j, int[,] operX, int[,] operY)
        {
            double Gx = pix.Process(i, j, operX);
            double Gy = pix.Process(i, j, operY);

            return Math.Sqrt(Gx * Gx + Gy * Gy).ToByte();
        }
        public static byte GetPoint(this byte[,] arr, int i, int j)
        {
            return arr[arr.GetUpperBound(0) - i < 0 ? arr.GetUpperBound(0) : (i > 0 ? i : 0),
                arr.GetUpperBound(1) - j < 0 ? arr.GetUpperBound(1) : (j > 0 ? j : 0)];
        }
    }
}
