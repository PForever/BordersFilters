//DON'T TOUCH!!!!!
using Model;
namespace Model.Abstract {
	interface IOperator {
        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="src">���������� �������</param>
        /// <param name="reapply�ount">����� ��������</param>
        /// <returns></returns>
        OperatorsEnum Name { get; }
        byte[,] Transform(byte[,] src, int MatrixSize, double Sigma);
	}
}
