using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;
using System.Drawing;

namespace SVD
{
    class ImgMatrix
    {
        Matrix aMatrix, rMatrix, bMatrix, gMatrix;
        Bitmap img;

        public ImgMatrix(Bitmap img)
        {
            this.img = img;
            aMatrix = new Matrix(img.Width, img.Height);
            rMatrix = new Matrix(img.Width, img.Height);
            bMatrix = new Matrix(img.Width, img.Height);
            gMatrix = new Matrix(img.Width, img.Height);
            computeMatrix();
        }

        public ImgMatrix(Matrix AM, Matrix RM, Matrix BM, Matrix GM, int height, int width)
        {
            img = new Bitmap(AM.RowCount, AM.ColumnCount);
            Color currColor;


            Console.WriteLine("Computing image from compressed matrix...");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (AM[i, j] > 255)
                        AM[i, j] = 255;
                    if (RM[i, j] > 255)
                        RM[i, j] = 255;
                    if (GM[i, j] > 255)
                        GM[i, j] = 255;
                    if (BM[i, j] > 255)
                        BM[i, j] = 255;

                    if (AM[i, j] < 0)
                        AM[i, j] = 0;
                    if (RM[i, j] < 0)
                        RM[i, j] = 0;
                    if (GM[i, j] < 0)
                        GM[i, j] = 0;
                    if (BM[i, j] < 0)
                        BM[i, j] = 0;

                    
                    byte a = (byte)AM[i, j];
                    byte r = (byte)RM[i, j];
                    byte g = (byte)BM[i, j];
                    byte b = (byte)GM[i, j];
                    currColor = Color.FromArgb(a, r, g, b);
                    img.SetPixel(i, j, currColor);
                }
            }
        }

        private void computeMatrix()
        {
            int height = img.Height;
            int width = img.Width;

            Color currColor;

            Console.WriteLine("Computing Matrix from image...");

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    currColor = img.GetPixel(i,j);
                    int alpha = currColor.A;
                    int red = currColor.R;
                    int blue = currColor.B;
                    int green = currColor.G;
                    aMatrix[i, j] = alpha;
                    rMatrix[i, j] = red;
                    bMatrix[i, j] = blue;
                    gMatrix[i, j] = green;
                }
            }
        }

        public Bitmap getImage()
        {
            return img;
        }

        public Matrix getAMatrix()
        {
            return aMatrix;
        }

        public Matrix getRMatrix()
        {
            return rMatrix;
        }

        public Matrix getGMatrix()
        {
            return gMatrix;
        }

        public Matrix getBMatrix()
        {
            return bMatrix;
        }
    }
}
