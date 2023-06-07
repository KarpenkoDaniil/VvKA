using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGU_Lab_4
{
    public class OnCPU
    {
        public double[][] Matrix;
        public double num;

        public OnCPU(double[][] matrix)
        {
            this.Matrix = matrix;
        }

        public OnCPU(double[][] matrix, double num)
        {
            this.Matrix = matrix;
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix[i].Length; j++)
                {
                    Matrix[i][j] = num * Matrix[i][j];
                }
            }

        }

        public static OnCPU operator *(OnCPU Matrix, OnCPU Matrix_2)
        {
            double Sum = 0;
            int Cheker = 0;
            double[][] NewArray = new double[Matrix.Matrix.Length][];
            for (int i = 0; i < Matrix.Matrix.Length; i++)
            {
                NewArray[i] = new double[Matrix_2.Matrix[0].Length];
            }

            for (int i = 0; i < Matrix.Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix_2.Matrix[0].Length; j++)
                {
                    for (int k = 0; k < Matrix_2.Matrix.Length; k++)
                    {
                        Sum = Sum + (Matrix.Matrix[i][k] * Matrix_2.Matrix[k][Cheker]);
                    }
                    Cheker++;
                    NewArray[i][j] = Sum;
                    Sum = 0;
                }
                Cheker = 0;
            }
            return new OnCPU(NewArray);
        }
    }
}
