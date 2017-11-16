//DON'T TOUCH!!!!!
using Model;
namespace Model.Abstract {
	interface IOperator {
        /// <summary>
        /// Действие оператора
        /// </summary>
        /// <param name="src">Пиксельная матрица</param>
        /// <param name="reapplyСount">Число итераций</param>
        /// <returns></returns>
        OperatorsEnum Name { get; }
        byte[,] Transform(byte[,] src, int MatrixSize, double Sigma);
	}
}
