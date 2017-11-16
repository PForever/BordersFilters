using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class IdentityOperator : IOperator {
		static readonly int[,] Matrix = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
		
        public IdentityOperator() => Name = OperatorsEnum.IdentityOperator;

        public OperatorsEnum Name { get; }

	    public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
		
			src = dst.ForEach((i, j) => dst[i, j] = src.Process(i, j, Matrix).ToByte());

			return src;
		}
	}
}
