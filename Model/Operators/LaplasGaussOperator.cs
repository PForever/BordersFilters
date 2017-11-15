using System;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	public class LaplasGaussOperator : IOperator {
		bool inverted = true;
		double[,] _oper;

		public LaplasGaussOperator() => Name = OperatorsEnum.LaplasGaussOperator;

		public OperatorsEnum Name { get; }

		public byte[,] Transform(byte[,] src) {
			return Transform(src, 3);
		}

		// ОСТОРОЖНО!!!! 
		// АБСОЛЮТНО
		// НЕ ТЕЩЕННЫЙ
		// КОД!!!!!

		public byte[,] Transform(byte[,] src, int matrix_size) {
			if (matrix_size < 3 || matrix_size % 2 == 0) return null;
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			int[,] oper = GetGaussianLaplasian(matrix_size, 2);
			if (inverted) oper = oper.Divide(-1);

			return dst.ParallelForEach((i, j) => dst[i, j] = Math.Abs(src.Process(i, j, oper)).ToByte());

		}

		public int[,] GetGaussianLaplasian(int matrix_size, double sigma) {
			if (matrix_size < 3 || matrix_size % 2 == 0) return null;
			_oper = new double[matrix_size, matrix_size];
			double[,] oper = new double[matrix_size, matrix_size];

			int mid = matrix_size / 2;
			int sum = 0;

			for (int i = 0; i < matrix_size; i++) {
				for (int j = 0; j < matrix_size; j++) {
					oper.SymmetricalSetter(mid - i, mid - j, LoG(i, j));
				}
			}

			int[,] int_oper = new int[matrix_size, matrix_size];

			double det = Math.Abs(oper.Determinant());
			_oper.ParallelForEach((i, j) => int_oper[i, j] = (int) (_oper[i, j] / det));
			
			return int_oper;
		}

		public static double LoG(int x, int y) {
			int sigma = 2;

			double tmp1 = -1 / (Math.Pow(sigma, 4) * Math.PI);
			double tmp2 = (x * x + y * y) / (2.0 * sigma * sigma);
			double tmp3 = 1 - tmp2;

			return tmp1 * ((int)tmp3) * Math.Exp(-tmp2);
		}
	}
}