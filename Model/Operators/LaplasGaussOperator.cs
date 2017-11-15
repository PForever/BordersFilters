using System;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	public class LaplasGaussOperator : IOperator {
		bool inverted = true;

		public LaplasGaussOperator() => Name = OperatorsEnum.LaplasOperator;

		public OperatorsEnum Name { get; }

		public byte[,] Transform(byte[,] src) {
			return Transform(src, 3);
		}

		public byte[,] Transform(byte[,] src, int matrix_size) {
			if (matrix_size < 3 || matrix_size % 2 == 0) return null;
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			int[,] oper;

			oper = GetLaplasianGaussian(matrix_size);
			if (inverted) oper = oper.Divide(-1);

			return dst.ParallelForEach((i, j) => dst[i, j] = Math.Abs(src.Process(i, j, oper)).ToByte());
		}

		private int[,] GetLaplasianGaussian(int matrix_size) {
			int[,] oper = new int[matrix_size, matrix_size];
			int tmp = -1;
			int mid = matrix_size / 2;
			int sum = 0;


			for (int x = 0; x < mid; x++) {
				oper.SymmetricalSetter(x, mid, tmp);
				tmp *= 2;
				for (int y = 1; y < x; y++) {
					int tmp2 = oper[mid, x - y];
					sum += tmp2 * 4;
					oper.SymmetricalSetter(mid - y, x, tmp2);
				}
			}
			oper[mid, mid] = -sum;

			return oper;
		}
	}
}
