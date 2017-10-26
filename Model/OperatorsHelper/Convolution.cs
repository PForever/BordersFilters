using System;
using System.Collections;

namespace Model.OperatorsHelper
{
    public static class Convolution
    {
        /// <summary>
        /// Применяет формулу свёртки.
        /// </summary>
        /// <param name="pix">Массив участка изображения.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="oper">Оператор свёртки.</param>
        /// <returns></returns>
        public static byte Process(this byte[,] pix, int i, int j, int[,] oper)
        {
            int result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result.ToByte();
        }
        /// <summary>
        /// Применяет формулу свёртки.
        /// </summary>
        /// <param name="pix">Массив участка изображения.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="oper">Оператор свёртки.</param>
        /// <returns></returns>
        public static byte Process(this byte[,] pix, int i, int j, double[,] oper)
        {
            double result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result.ToByte();
        }
        private static byte GetPoint(this byte[,] arr, int i, int j)
        {
            return arr[arr.GetUpperBound(0) - i < 0 ? arr.GetUpperBound(0) : (i > 0 ? i : 0),
                arr.GetUpperBound(1) - j < 0 ? arr.GetUpperBound(1) : (j > 0 ? j : 0)];
        }

        /// <summary>
        /// Применяет формулу свёртки для двух операторов, вычисляя приближённое значение производной.
        /// </summary>
        /// <param name="pix">Массив участка изображения.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="operX">Оператор свёртки по X.</param>
        /// <param name="operY">Оператор свёртки по Y.</param>
        public static byte Process(this byte[,] pix, int i, int j, int[,] operX, int[,] operY)
        {
            double gx = pix.Process(i, j, operX);
            double gy = pix.Process(i, j, operY);

            return Math.Sqrt(gx * gx + gy * gy).ToByte(); //(Math.Abs(gx) + Math.Abs(gy)).ToByte();
        }

    }
}
