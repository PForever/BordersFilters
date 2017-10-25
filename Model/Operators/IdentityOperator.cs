using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class IdentityOperator : IOperator {
		static readonly int[,] Matrix = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };

		// Служит для тестов, не должен изменить ничего в картинке.
		private byte[,] Transform(byte[,] src) {
			// byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			var operatorsApplyer = new OperatorsApplyer(src, Matrix);
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
