using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class PrewittOperator : IOperator {
	    public PrewittOperator() => Name = OperatorsEnum.PrewittOperator;

	    public OperatorsEnum Name { get; }

		static readonly int[,] 
			_operX = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } },
			_operY = _operX.Transponse();

		public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			return dst.ForEach((i, j) => dst[i, j] = src.Process(i, j, _operX, _operY));
		}
	}
}
