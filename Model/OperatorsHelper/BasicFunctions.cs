using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper
{
    public static class BasicFunctions
    {
        /// <summary>
        /// Получает двумерный массив байтов со значениями яркости соответствующего пикселя.
        /// </summary>
        public static byte[,] GetGrayArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = (byte)(src[i, j].GetBrightness() * 255));
            return dst;
        }
        /// <summary>
        /// Получает двумерный массив байтов со значениями красного цвета соответствующего пикселя.
        /// </summary>
        public static byte[,] GetRedArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].R);
            return dst;
        }
        /// <summary>
        /// Получает двумерный массив байтов со значениями зелёного цвета соответствующего пикселя.
        /// </summary>
        public static byte[,] GetGreenArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].G);
            return dst;
        }
        /// <summary>
        /// Получает двумерный массив байтов со значениями синего цвета соответствующего пикселя.
        /// </summary>
        public static byte[,] GetBlueArray(this Color[,] src)
        {
            byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i, j) => dst[i, j] = src[i, j].B);
            return dst;
        }

        /// <summary>
        /// Составляет двумерный массив Color из переданного массива байтов.
        /// </summary>
        public static Color[,] GetColorArray(this byte[,] src)
        {
            Color[,] dst = new Color[src.GetLength(0), src.GetLength(1)];
            dst.ForEach((i,j) => dst[i, j] = Color.FromArgb(src[i, j], src[i, j], src[i, j]));
            return dst;
        }
        /// <summary>
        /// Находит максимальный элемент в матрице.
        /// </summary>
        public static double Max(this double[,] src)
        {
            double max = default(double);
            foreach (var current in src)
            {
                if (current > max) max = current;
            }
            return max;
        }
		/// <summary>
		/// Составляет двумерный массив Color из массивов цветов.
		/// </summary>
		/// <param name="dst">Изменяемый массив.</param>
		/// <param name="srcR">Массив яркости красных цветов.</param>
		/// <param name="srcG">Массив яркости зелёных цветов.</param>
		/// <param name="srcB">Массив яркости синих цветов.</param>
		/// <returns></returns>
		public static Color[,] GetColorArray(this Color[,] dst, byte[,] srcR, byte[,] srcG, byte[,] srcB) {
			if (srcR == null || srcG == null || srcB == null) return null;

            dst.ForEach((i, j) => dst[i, j] = Color.FromArgb(srcR[i, j], srcG[i, j], srcB[i, j]));
            return dst;
        }

        /// <summary>
        /// Выполняет задоное действие проходя по всей размерности массива.
        /// Метод допускает изменение состояния элементов массива.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива.</typeparam>
        /// <param name="src">Исходный массив.</param>
        /// <param name="action">Выполняемое действие.
        /// Первый аргумент -- счётчик нулевого измерения масиива.
        /// Второй аргумент -- счётчик первого измерения массива.</param>
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

        /// <summary>
        /// Выполняет задоное действие проходя по всей размерности массива.
        /// Метод допускает изменение состояния элементов массива.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива.</typeparam>
        /// <param name="src">Исходный массив.</param>
        /// <param name="action">Выполняемое действие.
        /// Первый аргумент -- счётчик нулевого измерения масиива.
        /// Второй аргумент -- счётчик первого измерения массива.</param>
        public static void ForEach<T>(this ConcurrentBag<T> src, Action<T> action)
        {
            foreach (var element in src) action(element);
        }
        /// <summary>
        /// Выполняет задоное действие проходя по всей размерности массива в параллельном режиме.
        /// Метод допускает изменение состояния элементов массива.
        /// </summary>
        /// <typeparam name="T">Тип элементов массива.</typeparam>
        /// <param name="src">Исходный массив.</param>
        /// <param name="action">Выполняемое действие.
        /// Первый аргумент -- счётчик нулевого измерения масиива.
        /// Второй аргумент -- счётчик первого измерения массива.</param>
        public static T[,] ParallelForEach<T>(this T[,] src, Action<int, int> action)
        {
            int width = src.GetLength(0);
            int hight = src.GetLength(1);
            Parallel.For(0, width, i => Parallel.For(0, hight, j => action(i, j)));

            return src;
        }
        /// <summary>
        /// Конвернирует int в byte, сохраняя максимальность или минимальность значения при отбрасывании битов.
        /// </summary>
        public static byte ToByte(this int value) => (byte) (value > 255 ? 255 : (value < 0 ? 0 : value));
        /// <summary>
        /// Конвернирует float в byte, сохраняя максимальность или минимальность значения при отбрасывании битов.
        /// </summary>
        public static byte ToByte(this float value) => (byte) (value > 255 ? 255 : (value < 0 ? 0 : value));
        /// <summary>
        /// Конвернирует double в byte, сохраняя максимальность или минимальность значения при отбрасывании битов.
        /// </summary>
        public static byte ToByte(this double value) => (byte)(value > 255 ? 255 : (value < 0 ? 0 : value));
        /// <summary>
        /// Конвернирует int[,] в byte[,], сохраняя максимальность или минимальность значения при отбрасывании битов.
        /// </summary>
        public static byte[,] ToByte(this int[,] intArray)
        {
            var byteArray = new byte[intArray.GetLength(0), intArray.GetLength(1)];
            byteArray.ForEach((i, j) => byteArray[i, j] = intArray[i, j].ToByte());
            return byteArray;
        }
        /// <summary>
        /// Конвернирует byte[,] в int[,] , сохраняя максимальность или минимальность значения при отбрасывании битов.
        /// </summary>
        public static int[,] ToInt(this byte[,] byteArray)
        {
            var intArray = new int[byteArray.GetLength(0), byteArray.GetLength(1)];
            intArray.ForEach((i, j) => intArray[i, j] = byteArray[i, j]);
            return intArray;
        }
        /// <summary>
        /// Проводит процедуру бинаризации изображения.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>Массив пикселей.</returns>
        public static byte[,] Binary(this byte[,] arr) => arr.ForEach((i, j) => { if (arr[i, j] >= Thresholad) arr[i, j] = byte.MaxValue; });

        /// <summary>
        /// Проводит процедуру бинаризации изображения.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>Массив пикселей.</returns>
        public static byte[,] Binary(this int[,] arr)
        {
            var byteArr = new byte[arr.GetLength(0), arr.GetLength(1)];
            arr.ForEach((i, j) => byteArr[i,j] = arr[i, j] <= Thresholad ? byte.MinValue : byte.MaxValue);
            return byteArr;
        }

        /// <summary>
        /// Проводит процедуру бинаризации.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Пикселей.</returns>
        public static byte Binary(this byte value) => value <= Thresholad ? byte.MinValue : byte.MaxValue;
        /// <summary>
        /// Проводит процедуру бинаризации.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Пикселей.</returns>
        public static byte Binary(this int value) => value <= Thresholad ? byte.MinValue : byte.MaxValue;

        private const byte Thresholad = (byte) (0.20 * byte.MaxValue);
    }
}
