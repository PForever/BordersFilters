﻿using System;
using Model.Abstract;
using Model.OperatorsHelper;

namespace Model.Operators
{
    class GaussOperator : IOperator { 

        public GaussOperator() => Name = OperatorsEnum.GaussOperator;

        public OperatorsEnum Name { get; }
   
        private double[,] _oper;

        public byte[,] Transform(byte[,] src, int MatrixSize, double Sigma)
        {
			_oper = GetGauss(MatrixSize, Sigma);

            byte[,] dst = new byte[src.GetLength(0),src.GetLength(1)];
            src.ParallelForEach((i, j) => dst[i, j] = src.Process(i, j, _oper));

            return dst;
        }

<<<<<<< HEAD
		public static double[,] GetGauss(int MatrixSize, double Sigma) {
=======
		public double[,] GetGauss(int MatrixSize, double Sigma) {
>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
			double[,] gauss = new double[MatrixSize, MatrixSize];
            int halfLenght = MatrixSize / 2;

			gauss.ParallelForEach((i, j) => gauss[i, j] = Gauss(Sigma, i - halfLenght, j - halfLenght));

            double det = Math.Abs(gauss.Determinant());
            gauss.ParallelForEach((i, j) => gauss[i, j] = gauss[i, j]/det);
			return gauss;
		}

<<<<<<< HEAD
        private static double Gauss(double sigma, int i, int j) => (1 / (2 * Math.PI * Math.Pow(sigma, 2))) *
=======
        private double Gauss(double sigma, int i, int j) => (1 / (2 * Math.PI * Math.Pow(sigma, 2))) *
>>>>>>> 9cf16ae5b64198bc132399b52cc5d9bd9bf4c869
                                                               Math.Exp(-(Math.Pow(i, 2) + Math.Pow(j, 2)) /
                                                                        (2 * Math.Pow(sigma, 2)));
    }
}
