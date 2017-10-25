using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class PrevittOperator : IOperator {
		static readonly int[,] 
			OperX = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } },
			OperY = BasicMatrixFunctions.Transponse(OperX);

		public string GetName() {
			return "Оператор Превитта";
		}

		public byte[,] Transform(byte[,] src) {
			var operatorsApplyer = new OperatorsApplyer(src, OperX, OperY);
			operatorsApplyer.Apply();
			return operatorsApplyer.GetResult();
		}

		public byte[,] Transform(byte[,] src, int reapplyCount) {
			for (int i = 0; i < reapplyCount; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
