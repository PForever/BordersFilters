using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper
{
    public static class BasicFunctions
    {
        // Получает двумерный массив байтов со значениями яркости соответствующего пикселя
        public static byte[,] GetGrayArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = (byte)(src[i, j].GetBrightness() * 255));
            return dst;
        }
        public static byte[,] GetRedArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].R);
            return dst;
        }
        public static byte[,] GetGreenArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].G);
            return dst;
        }
        public static byte[,] GetBlueArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].B);
            return dst;
        }

        // Получает двумерный массив Color из переданного массива байтов
        public static Color[,] GetColorArray(this byte[,] src)
        {
            Color[,] dst = new Color[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i,j) => dst[i, j] = Color.FromArgb(src[i, j], src[i, j], src[i, j]));
            return dst;
        }

        public static double Max(this double[,] src)
        {
            double max = default(double);
            foreach (var current in src)
            {
                if (current > max) max = current;
            }
            return max;
        }

        public static Color[,] GetColorArray(this Color[,] dst, byte[,] srcR, byte[,] srcG, byte[,] srcB)
        {
            dst.ForEach((i, j) => dst[i, j] = Color.FromArgb(srcR[i, j], srcG[i, j], srcB[i, j]));
            return dst;
        }

        public static T[,] ForEach<T>(this T[,] src, Action<int, int> action)
        {
            int width = src.GetLength(0);
            int hight = src.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    action(i, j);
                }
            }
            return src;
        }

        public static T[,] ParallelForEach<T>(this T[,] src, Action<int, int> action)
        {
            int width = src.GetLength(0);
            int hight = src.GetLength(1);
            Parallel.For(0, width, i => Parallel.For(0, hight, j => action(i, j)));

            return src;
        }
    }
}
