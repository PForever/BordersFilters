using System;
using System.Drawing;

namespace Model.OperatorsHelper {
	/**
	* Класс содержит простые методы обработки изображения
	*/
	public static class BasicMatrixFunctions {


        /// <summary>
        /// Транспонирует переданную матрицу.
        /// </summary>
        public static int[,] Transponse(this int[,] src) {
			int[,] res = new int[src.GetLength(1), src.GetLength(0)];
			res.ForEach((i,j) => res[i, j] = src[j, i]);
			return res;
		}
        /// <summary>
        /// Отбрасывает все значения меньше заданного порога.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="threshold">Нижний порог.</param>
		public static byte[,] Threshold(this byte[,] src, byte threshold) {
			byte[,] result = new byte[src.GetLength(0), src.GetLength(1)];
		    result.ForEach((i, j) => result[i, j] = (byte) (src[i, j] < threshold ? 0 : 255));
			return result;
		}
        /// <summary>
        /// Делит все элементы матрицы на указаное число.
        /// </summary>
        /// <param name="src">Исходная матрица.</param>
        /// <param name="denominator">Делитель.</param>
	    public static double[,] Divide(this double[,] src, double denominator)
	    {
	        src.ForEach((i, j) => src[i, j] = src[i, j] / denominator);
	        return src;
	    }
	    /// <summary>
	    /// Делит все элементы матрицы на указаное число.
	    /// </summary>
	    /// <param name="src">Исходная матрица.</param>
	    /// <param name="denominator">Делитель.</param>
        public static int[,] Divide(this int[,] src, int denominator)
	    {
	        src.ForEach((i, j) => src[i, j] = src[i, j] / denominator);
	        return src;
	    }
	    /// <summary>
	    /// Делит все элементы матрицы на указаное число.
	    /// </summary>
	    /// <param name="src">Исходная матрица.</param>
	    /// <param name="denominator">Делитель.</param>
        public static byte[,] Divide(this byte[,] src, byte denominator)
	    {
	        src.ForEach((i, j) => src[i, j] = (src[i, j] / denominator).ToByte());
	        return src;
	    }
	    /// <summary>
	    /// Вычисляет определитель матрицы.
	    /// </summary>
        public static double Determinant(this double[,] matrix)
	    {
	        if (matrix.Length == 1) return matrix[0, 0];
            if (matrix.Length == 4)
	        {
	            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
	        }
	        double sign = 1, result = 0;
	        for (int i = 0; i < matrix.GetLength(1); i++)
	        {
	            double[,] minor = GetMinor(matrix, i);
	            result += sign * matrix[0, i] * Determinant(minor);
	            sign = -sign;
	        }
	        return result;
	    }

	    private static double[,] GetMinor(this double[,] matrix, int n)
	    {
	        double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result;
	    }
    }
}
