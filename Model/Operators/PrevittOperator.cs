using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Model.Abstract;

namespace Model.Operators {
	class PrevittOperator : IOperator {
		static int[,] 
			oper_x = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } },
			oper_y = BasicFunctions.Transponse(oper_x);

		public string GetName() {
			return "Оператор Превитта";
		}

		//
		public byte[,] Transform(byte[,] src) {
			var operatorsApplyer = new OperatorsHelper.OperatorsApplyer(src, oper_x, oper_y);
			operatorsApplyer.Apply();
			return operatorsApplyer.GetResult();
		}

		public byte[,] Transform(byte[,] src, int reapply_count) {
			for (int i = 0; i < reapply_count; i++) {
				src = Transform(src);
			}
			return src;
		}
	}
}
