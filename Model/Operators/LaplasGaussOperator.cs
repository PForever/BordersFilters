using System;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	public class LaplasGaussOperator : IOperator {
		public LaplasGaussOperator() => Name = OperatorsEnum.LaplasGaussOperator;

		public OperatorsEnum Name { get; }
		
<<<<<<< HEAD
=======
		// ОСТОРОЖНО!!!! 
		// АБСОЛЮТНО
		// НЕ ТЕЩЕННЫЙ
		// КОД!!!!!

>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
		bool inverted = true;
		double[,] _oper;

		public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
<<<<<<< HEAD
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			double[,] oper = GetGaussianLaplasian(MatrixSize, Sigma);
			if (inverted) oper = oper.Divide(-1);

			dst = dst.ParallelForEach((i, j) => dst[i, j] = ((int) src.Process(i, j, oper)).ToByte());
			return dst;
		}
		
		public double[,] GetGaussianLaplasian(int MatrixSize, double Sigma) {
			if (MatrixSize < 3 || MatrixSize % 2 == 0) {
				MatrixSize = 3;
			}
			Sigma = Sigma == 1 ? 1.4 : Sigma;

			double[,] oper = new double[MatrixSize, MatrixSize];

			int mid = MatrixSize / 2;

			for (int i = 0; i <= mid; i++) {
				for (int j = 0; j <= mid; j++) {
					oper.SymmetricalSetter(mid - i, mid - j, LoG(i, j, Sigma));
				}
			}

			oper[mid, mid] = LoG(0, 0, Sigma);
			
			//double det = Math.Abs(oper.Determinant());
=======
			if (MatrixSize < 3 || MatrixSize % 2 == 0) return null;
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			int[,] oper = GetGaussianLaplasian(MatrixSize, Sigma);
			if (inverted) oper = oper.Divide(-1);

			dst = dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, oper).ToByte());
			return dst;
		}
		
		public int[,] GetGaussianLaplasian(int MatrixSize, double Sigma) {
			if (MatrixSize < 3 || MatrixSize % 2 == 0) return null;
			_oper = new double[MatrixSize, MatrixSize];
			double[,] oper = new double[MatrixSize, MatrixSize];

			int mid = MatrixSize / 2;
			int sum = 0;

			for (int i = 0; i < mid; i++) {
				for (int j = 0; j < mid; j++) {
					oper.SymmetricalSetter(mid - i, mid - j, LoG(i, j));
				}
			}

			int[,] int_oper = new int[MatrixSize, MatrixSize];

			double det = Math.Abs(oper.Determinant());
			_oper.ParallelForEach((i, j) => int_oper[i, j] = (int) (_oper[i, j] / det));
>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
			
			//oper.Divide(det);
			return oper;
		}

		public static double LoG(int x, int y, double Sigma) {

			double tmp2 = (x * x + y * y - 2 * Sigma * Sigma) / (Sigma * Sigma * Sigma * Sigma);
			double tmp3 = - (x * x + y * y) / (2.0 * Sigma * Sigma);

			return (tmp3 * Math.Exp(tmp2));
		}
	}
}
