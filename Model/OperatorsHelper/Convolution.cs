using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
        public static int Process(this byte[,] pix, int i, int j, int[,] oper)
        {
            int result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result;
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
<<<<<<< HEAD
			//return Math.Abs(result).ToByte();
			return result.ToByte();
		}
        private static byte GetPoint(this byte[,] arr, int i, int j)
=======
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
        public static int Process(this int[,] pix, int i, int j, int[,] oper)
>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
        {
            int result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2));
            return result;
        }
        /// <summary>
        /// Применяет формулу свёртки.
        /// </summary>
        /// <param name="pix">Массив участка изображения.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="oper">Оператор свёртки.</param>
        /// <returns></returns>
        public static int Process(this int[,] pix, int i, int j, double[,] oper)
        {
            int result = 0;
            int size = oper.GetLength(0);
            oper.ForEach((k, l) => result += (int)(oper[k, l] * pix.GetPoint(i + k - size / 2, j + l - size / 2)));
            return result;
        }
        private static T GetPoint<T>(this T[,] arr, int i, int j)
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
        /// <summary>
        /// Применяет формулу свёртки для двух операторов, вычисляя приближённое значение производной и возвращая угол градиента.
        /// </summary>
        /// <param name="pix">Массив участка изображения.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="operX">Оператор свёртки по X.</param>
        /// <param name="operY">Оператор свёртки по Y.</param>
        /// <param name="grad">Угол градиента.</param>
        public static int Process(this int[,] pix, int i, int j, int[,] operX, int[,] operY, out int grad)
        {
            double gx = pix.Process(i, j, operX);
            double gy = pix.Process(i, j, operY);
            byte g = Math.Sqrt(gx * gx + gy * gy).ToByte(); //(Math.Abs(gx) + Math.Abs(gy)).ToByte();
            grad = g != 0 ? (gy < Double.Epsilon? 90 : (int)Math.Round((90 - Math.Atan(gy / gx) / Math.PI * 180) / 45) * 45) : -1; /*TODO можно оптимизировать через Enum*/
            return g;
        }

        /// <summary>
        /// Подавление немаксимумов.
        /// </summary>
        /// <param name="src">Исходная матрица.</param>
        /// <param name="grads">Матрица градиентов.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <param name="borderMin">Нижнаяя граница.</param>
        /// <param name="borderMax">Верхняя граница.</param>
        /// <param name="value">Промежуточное значение.</param>
        /// <param name="midPoints"></param>
        /// <returns></returns>
        public static int NonMaximum(this int[,] src, int[,] grads, int i, int j, int borderMin, int borderMax, int value, ConcurrentBag<Point> midPoints)
        {
            switch (grads[i, j])
            {
                case 180:
                case 0:
                    if (/*grads.GetPoint(i, j - 1).Module < grads[i, j].Module && grads.GetPoint(i, j + 1).Module < grads[i, j].Module
                        && */src.GetPoint(i, j - 1) <= src[i, j] && src.GetPoint(i, j + 1) <= src[i, j])
                    {
                        if (src[i, j] <= borderMin) return byte.MinValue;
                        if (src[i, j] >= borderMax) return byte.MaxValue;
                        midPoints.Add(new Point(i,j));
                        return value;
                    }
                    else return byte.MinValue;
                case 45:
                    if(/*grads.GetPoint(i-1, j-1).Module < grads[i, j].Module && grads.GetPoint(i+1, j+1).Module < grads[i, j].Module
                       &&*/ src.GetPoint(i-1, j - 1) <= src[i, j] && src.GetPoint(i+1, j + 1) <= src[i, j])
                    {
                        if (src[i, j] <= borderMin) return byte.MinValue;
                        if (src[i, j] >= borderMax) return byte.MaxValue;
                        midPoints.Add(new Point(i, j));
                        return value;
                    }
                    else return byte.MinValue;
                case 90:
                    if(/*grads.GetPoint(i-1, j).Module < grads[i, j].Module && grads.GetPoint(i+1, j).Module < grads[i, j].Module
                       &&*/ src.GetPoint(i-1, j) <= src[i, j] && src.GetPoint(i + 1, j) <= src[i, j])
                    {
                        if (src[i, j] <= borderMin) return byte.MinValue;
                        if (src[i, j] >= borderMax) return byte.MaxValue;
                        midPoints.Add(new Point(i, j));
                        return value;
                    }
                    else return byte.MinValue;
                case 135:
                    if(/*grads.GetPoint(i-1, j+1).Module < grads[i, j].Module && grads.GetPoint(i+1, j-1).Module < grads[i, j].Module
                       &&*/ src.GetPoint(i-1, j + 1) <= src[i, j] && src.GetPoint(i+1, j - 1) <= src[i, j])
                    {
                        if (src[i, j] <= borderMin) return byte.MinValue;
                        if (src[i, j] >= borderMax) return byte.MaxValue;
                        midPoints.Add(new Point(i, j));
                        return value;
                    }
                    else return byte.MinValue;
                default:
                    return byte.MinValue;
            }
        }

        private const int Size = 1;
        /// <summary>
        /// Проверка соприкосновения промежуточного элемента с границей.
        /// </summary>
        /// <param name="src">Исходаня матрица.</param>
        /// <param name="grads">Матрица градиентов.</param>
        /// <param name="i">Первая координата центрального элемента.</param>
        /// <param name="j">Вторая координата центрального элемента.</param>
        /// <returns></returns>
        public static bool UncertaintyTracing(this int[,] src, int[,] grads, int i, int j)
        {
            src[i, j] = byte.MinValue;
            for (int k = -Size; k <= Size; k++)
            {
                for (int l = -Size; l <= Size; l++)
                {
                    int x = i + k, y = j + l;
                    if(k == 0 && l == 0) continue;
                    if (grads.GetPoint(x, y) != grads[i, j]) continue;
                    var temp = src.GetPoint(x, y);
                    if (temp >= byte.MaxValue || temp != byte.MinValue && Chack(src, x, y) && src.UncertaintyTracing(grads, x, y))
                    {
                        src[i, j] = byte.MaxValue;
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool Chack(int[,] src, int x, int y)
        {
            return x > 0 && y > 0 && x < src.GetLength(0) && y < src.GetLength(1);
        }
    }
}
