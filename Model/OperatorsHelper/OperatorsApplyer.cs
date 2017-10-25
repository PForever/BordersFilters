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
		
		public OperatorsApplyer(byte[,] img, int[,] operX, int[,] operY = null) {
			_src = img;
			_operX = operX;
			_operY = operY;
			_result = new byte[_src.GetLength(0), _src.GetLength(1)];
		}

		public void Apply() {
			int width = _src.GetUpperBound(0), height = _src.GetUpperBound(1);

			_result.ForEach((i, j) => _src.Process(i, j, _operX, _operY));
		}

		public void Reapply() {
			_src = _result;
			_result = new byte[_src.GetLength(0), _src.GetLength(1)];
			Apply();
		}

		public byte[,] GetResult() {
			//return (byte[,])img.Clone();
			return _result;
		}
	}
}
