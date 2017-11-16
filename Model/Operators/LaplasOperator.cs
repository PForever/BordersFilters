using System;
using Model.Abstract;
using Model.OperatorsHelper;
/// <summary>
/// Оператор осуществляет преобразование изображения оператором Лапласа. 
/// В качестве входных параметров принимает размер матрицы, сигму игнорирует.
/// В случае, если параметры некорректны, используются параметры по умолчанию: 3
/// </summary>
namespace Model.Operators {
	public class LaplasOperator : IOperator {
		public LaplasOperator() => Name = OperatorsEnum.LaplasOperator;

		public OperatorsEnum Name { get; }

		bool inverted = true;

		public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			if (MatrixSize < 3 || MatrixSize % 2 == 0) MatrixSize = 3;
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];

			int[,] oper = GetLaplasian(MatrixSize);
			if (inverted) oper = oper.Divide(-1);

			return dst.ForEach((i, j) => dst[i, j] = Math.Abs(src.Process(i, j, oper)).ToByte());
		}

		private int[,] GetLaplasian(int matrix_size) {
			int[,] oper = new int[matrix_size, matrix_size];
			int tmp = -1;
			int mid = matrix_size / 2;
			int sum = 0;

			for (int i = 0; i < mid; i++)
			{
				oper.SymmetricalSetter(i, mid, tmp);
				sum += tmp * 4;
				tmp *= 2;
				for (int j = 1; j < i; j++)
				{
					int tmp2 = oper[mid, i - j];
					sum += tmp2 * 4;
					oper.SymmetricalSetter(mid - j, i, tmp2);
				}
			}
			oper[mid, mid] = - sum;

			return oper;
		}
	}
}
