using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper {
	class OperatorsApplyer {
		byte[,] src, result;
		int[,] oper_x, oper_y;
		int matrix_size;

		int forv_size;
		int back_size;
		int m_center;

		public OperatorsApplyer(byte[,] img, int[,] matrix) {
			src = img;
			result = new byte[src.GetLength(0), src.GetLength(1)];
			oper_x = matrix;
			matrix_size = matrix.GetLength(0);

			forv_size = matrix_size / 2;
			back_size = -(matrix_size % 2 == 1 ? forv_size : forv_size - 1);
			m_center = matrix_size / 2;
		}

		public OperatorsApplyer(byte[,] img, int[,] oper_x, int[,] oper_y) {
			src = img;
			result = new byte[src.GetLength(0), src.GetLength(1)];
			this.oper_x = oper_x;
			this.oper_y = oper_y;
			matrix_size = oper_x.GetLength(0);

			forv_size = matrix_size / 2;
			back_size = -(matrix_size % 2 == 1 ? forv_size : forv_size - 1);
			m_center = matrix_size / 2;
		}

		public void Apply() {
			int width = src.GetUpperBound(0), height = src.GetUpperBound(1);
			
			for (int i = - back_size; i < width - forv_size; i++) {
				for (int j = - back_size; j < height - forv_size; j++) {
					Apply(i, j);
				}
			}
		}

		public void Reapply() {
			src = result;
			result = new byte[src.GetLength(0), src.GetLength(1)];
			Apply();
		}

		private void Apply(int a, int b) {
			int res_x = 0, res_y = 0;
			if (oper_y == null)
				for (int i = back_size; i <= forv_size; i++)
					for (int j = back_size; j <= forv_size; j++) {
						byte tmp = src[a + i, b + j];
						res_x += oper_x[m_center + i, m_center + j] * tmp;
					}
			else
				for (int i = back_size; i <= forv_size; i++)
					for (int j = back_size; j <= forv_size; j++) {
						byte tmp = src[a + i, b + j];
						res_x += oper_x[m_center + i, m_center + j] * tmp;
						res_y += oper_y == null ? 0 : oper_y[m_center + i, m_center + j] * tmp;
					}
			
			int res = res_y == 0 ? res_x : res_x + res_y;
			result[a, b] = (byte) ( res > 255 ? 255 : res );
		}

		public byte[,] GetResult() {
			//return (byte[,])img.Clone();
			return result;
		}
	}
}
