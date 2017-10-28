using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class PrevittOperator : IOperator {
		static readonly int[,] 
			_operX = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } },
			_operY = _operX.Transponse();

		public byte[,] Transform(byte[,] src) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			return dst.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, _operX, _operY));
		}

		public byte[,] Transform(byte[,] src, int reapplyCount) {
			for (int i = 0; i < reapplyCount; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
