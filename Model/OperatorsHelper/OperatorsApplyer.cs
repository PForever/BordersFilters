using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.OperatorsHelper {
	class OperatorsApplyer {
		byte[,] _src, _result;
	    readonly int[,] _operX;
	    readonly int[,] _operY;
	    readonly int _matrixSize;

	    readonly int _forvSize;
	    readonly int _backSize;
	    readonly int _mCenter;

		public OperatorsApplyer(byte[,] img, int[,] matrix) {
			_src = img;
			_result = new byte[_src.GetLength(0), _src.GetLength(1)];
			_operX = matrix;
			_matrixSize = matrix.GetLength(0);

			_forvSize = _matrixSize / 2;
			_backSize = -(_matrixSize % 2 == 1 ? _forvSize : _forvSize - 1);
			_mCenter = _matrixSize / 2;
		}

		public OperatorsApplyer(byte[,] img, int[,] operX, int[,] operY) {
			_src = img;
			_result = new byte[_src.GetLength(0), _src.GetLength(1)];
			this._operX = operX;
			this._operY = operY;
			_matrixSize = operX.GetLength(0);

			_forvSize = _matrixSize / 2;
			_backSize = -(_matrixSize % 2 == 1 ? _forvSize : _forvSize - 1);
			_mCenter = _matrixSize / 2;
		}

		public void Apply() {
			int width = _src.GetUpperBound(0), height = _src.GetUpperBound(1);
			
			for (int i = - _backSize; i < width - _forvSize; i++) {
				for (int j = - _backSize; j < height - _forvSize; j++) {
					Apply(i, j);
				}
			}
		}

		public void Reapply() {
			_src = _result;
			_result = new byte[_src.GetLength(0), _src.GetLength(1)];
			Apply();
		}

		private void Apply(int a, int b) {
			int resX = 0, resY = 0;
			if (_operY == null)
				for (int i = _backSize; i <= _forvSize; i++)
					for (int j = _backSize; j <= _forvSize; j++) {
						byte tmp = _src[a + i, b + j];
						resX += _operX[_mCenter + i, _mCenter + j] * tmp;
					}
			else
				for (int i = _backSize; i <= _forvSize; i++)
					for (int j = _backSize; j <= _forvSize; j++) {
						byte tmp = _src[a + i, b + j];
						resX += _operX[_mCenter + i, _mCenter + j] * tmp;
						resY += (int) (_operY[_mCenter + i, _mCenter + j] * tmp);
					}
			
			int res = resY == 0 ? resX : resX + resY;
			_result[a, b] = (byte) ( res > 255 ? 255 : res );
		}

		public byte[,] GetResult() {
			//return (byte[,])img.Clone();
			return _result;
		}
	}
}
