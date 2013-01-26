using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace SVD
{
    class Compression
    {
        
        //Creating Matrix and SVD Objects
        Matrix matrix, u, v, d;
        SingularValueDecomposition svd;
        //Creating SVD constants
        double SV1, SVK1;

        public Compression(Matrix m)
        {
            matrix = m;

            //Performing SVD calculations and saving components
            svd = new SingularValueDecomposition(m);

            u = svd.LeftSingularVectors;
            v = svd.RightSingularVectors;
            d = svd.S;
        }

        //Public getters
        public Matrix getU()
        {
            return u;
        }

        public Matrix getV()
        {
            return v;
        }

        public Matrix getD()
        {
            return d;
        }

        public double getSV1()
        {
            return SV1;
        }

        public double getSVK1()
        {
            return SVK1;
        }

        public Matrix compress(int k)
        {
            //Setting up compression matrix
            int rows = matrix.RowCount;
            int cols = matrix.ColumnCount;
            Matrix VK = new Matrix(cols, k);
            Matrix UK = new Matrix(rows, k);
            double SK;
            Matrix Ak = new Matrix(rows, cols);

            //Begin compressing matrix according to algorithm
            Console.WriteLine("Compressing matrix... K = " + k);
            for (int i = 0; i < k; i++)
            {
                //Making V, U, and S for compressed matrix
                VK = v.GetColumnVector(i).ToColumnMatrix();
                UK = u.GetColumnVector(i).ToColumnMatrix();
                SK = d[i, i];

                //Performing operations on V, U, S
                VK.Transpose();
                UK = UK.Multiply(VK);
                UK.Multiply(SK);
                Ak.Add(UK);
            }

            //Returning data
            SV1 = d[0, 0];

            SVK1 = d[k + 1, k + 1];

            Console.WriteLine("SV1: " + d[0, 0] + " SVK1: " + d[k + 1, k + 1]);

            return Ak;
        }



    }
}
