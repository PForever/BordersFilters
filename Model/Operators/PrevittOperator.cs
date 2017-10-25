using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class PrevittOperator : IOperator {
		static readonly int[,] 
			_operX = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } },
			_operY = BasicMatrixFunctions.Transponse(_operX);

		public string GetName() {
			return "Оператор Превитта";
		}

		public byte[,] Transform(byte[,] src) {
			byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			//var operatorsApplyer = new OperatorsApplyer(src, OperX, OperY);
			//operatorsApplyer.Apply();
			//return operatorsApplyer.GetResult();
			return dst.ForEach((i, j) => dst[i, j] = src.Process(i, j, _operX, _operY));
		}

		public byte[,] Transform(byte[,] src, int reapplyCount) {
			for (int i = 0; i < reapplyCount; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
