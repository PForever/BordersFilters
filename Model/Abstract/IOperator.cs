//DON'T TOUCH!!!!!
namespace Model.Abstract {
	interface IOperator {
        /// <summary>
        /// Действие оператора
        /// </summary>
        /// <param name="src">Пиксельная матрица</param>
        /// <param name="reapplyСount">Число итераций</param>
        /// <returns></returns>
		byte[,] Transform(byte[,] src, int reapplyСount);
	}
}
