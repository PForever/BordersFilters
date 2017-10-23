using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper {
	class OperatorsApplyer {
		byte[,] img;
		byte[,] matrix;
		int matrix_size;

		int forv_size;
		int back_size;
		int m_center;

		public OperatorsApplyer(byte[,] img, byte[,] matrix) {
			this.img = img;
			this.matrix = matrix;
			matrix_size = matrix.GetLength(0);

			forv_size = matrix_size / 2;
			back_size = -(matrix_size % 2 == 1 ? forv_size : forv_size - 1);
			m_center = matrix_size / 2;
		}

		public void Apply() {
			int width = img.GetUpperBound(0), height = img.GetUpperBound(1);

			for (int i = - back_size; i < width - forv_size; i++) {
				for (int j = - back_size; j < height - forv_size; j++) {
					Apply(i, j);
				}
			}
		}

		private void Apply(int a, int b) {
			int res = 0;
			for (int i = back_size; i < forv_size; i++) {
				for (int j = back_size; j < forv_size; j++) {
					res += matrix[m_center + i, m_center + j] * img[a + i, b + j];
				}
			}
			img[a, b] = (byte) ( res > 255 ? 255 : res );
		}

		public byte[,] GetResult() {
			//return (byte[,])img.Clone();
			return img;
		}
	}
}
