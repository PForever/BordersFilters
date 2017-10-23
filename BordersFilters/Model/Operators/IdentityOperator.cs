using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;

namespace Model.Operators {
	class IdentityOperator : IOperator {
		static byte[,] matrix = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };

		public string GetName() {
			return "Тождественный оператор";
		}

		// Служит для тестов, не должен изменить ничего в картинке.
		public byte[,] Transform(byte[,] src) {
			// byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			var operatorsApplyer = new OperatorsHelper.OperatorsApplyer(src, matrix);
			operatorsApplyer.Apply();
			return operatorsApplyer.GetResult();
		}

		public byte[,] Transform(Color[,] src) {
			return Transform(BasicFunctions.GetGrayArray(src));
		}
	}
}
