using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators {
	class IdentityOperator : IOperator {
		static byte[,] matrix = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
		// Служит для тестов, не должен изменить ничего в картинке.
		public byte[,] Transform(byte[,] src) {
			// byte[,] dst = new byte[src.GetLength(0), src.GetLength(1)];
			var operatorsApplyer = new OperatorsApplyer(src, matrix);
			operatorsApplyer.Apply();
			return operatorsApplyer.GetResult();
		}
	}
}
