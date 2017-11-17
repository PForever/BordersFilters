using System;
using Model.Abstract;
using Model.OperatorsHelper;

/// <summary>
/// Оператор осуществляет преобразование изображения оператором "Лапласиан Гауссиана", LoG. 
/// В качестве входных параметров принимает размер матрицы и сигму.
/// В случае, если параметры некорректны, используются параметры по умолчанию: 3 и 1.4 соответственно
/// </summary>
namespace Model.Operators {
	public class LaplasGaussOperator : IOperator {
		public LaplasGaussOperator() => Name = OperatorsEnum.LaplasGaussOperator;

		public OperatorsEnum Name { get; }

		bool inverted = true;
		double[,] _oper;

		private readonly double[,] _LoG_5x5 = new double[,]{
			{0, 0, -1, 0, 0},
			{0, -1, -2, -1, 0},
			{-1, -2, 16, -2, -1},
			{0, -1, -1, -1, 0},
			{0, 0, -1, 0, 0}
		};

		public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			//double[,] oper = GetGaussianLaplasian(MatrixSize, Sigma);
			double[,] oper = _LoG_5x5;

			if (inverted) oper = oper.Divide(-1);

			return dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, oper).Binary());
		}

		public double[,] GetGaussianLaplasian(int MatrixSize, double Sigma) {
			if (MatrixSize < 3 || MatrixSize % 2 == 0) {
				MatrixSize = 3;
			}
			Sigma = Math.Abs(Sigma - 1) < float.Epsilon ? 1.4 : Sigma;

			double[,] oper = new double[MatrixSize, MatrixSize];

			int mid = MatrixSize / 2;

			for (int i = 0; i <= mid; i++) {
				for (int j = 0; j <= mid; j++) {
					oper.SymmetricalSetter(mid - i, mid - j, LoG(i, j, Sigma));
				}
			}

			oper[mid, mid] = LoG(0, 0, Sigma);
			
			return oper;
		}

		public static double LoG(int x, int y, double Sigma) {

			double tmp2 = (x * x + y * y - 2 * Sigma * Sigma) / (Sigma * Sigma * Sigma * Sigma);
			double tmp3 = - (x * x + y * y) / (2.0 * Sigma * Sigma);

			return (tmp3 * Math.Exp(tmp2));
		}
	}
}
