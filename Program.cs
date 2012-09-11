using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.Drawing;
using System.Windows.Forms;

namespace SVD
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MessageBox.Show("Welcome to Ethan Shernan's Image Compression Program :D!");

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files |*.jpg; *.png; *.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result.Equals(DialogResult.Cancel))
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Heeeeeere we go!!!!");
            Bitmap img = (Bitmap)Image.FromFile(openFileDialog1.FileName);

            int k = 0;
            int QV = (img.Height * img.Width) / (img.Height + img.Width);

            while (true)
            {
                Console.Write("Please enter a quality value (Rank K) between 1 and " + QV + " (ideal k: " + (QV - 1) + ") : ");

                try
                {
                    k = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error! Did not enter a number!");
                }

                if (k > QV || k <= 0)
                {
                    Console.Write("Error! ");
                }
                else
                    break;
            }

            ImgMatrix imageMatrix = new ImgMatrix(img);
            Compression compressA = new Compression(imageMatrix.getAMatrix());
            Compression compressR = new Compression(imageMatrix.getRMatrix());
            Compression compressG = new Compression(imageMatrix.getGMatrix());
            Compression compressB = new Compression(imageMatrix.getBMatrix());
            Console.WriteLine("Done");

            Matrix caMatrix = compressA.compress(k);
            Matrix crMatrix = compressR.compress(k);
            Matrix cgMatrix = compressG.compress(k);
            Matrix cbMatrix = compressB.compress(k);

            ImgMatrix cimageMatrix = new ImgMatrix(caMatrix, crMatrix, cgMatrix, cbMatrix, img.Width, img.Height);

            Bitmap cimg = cimageMatrix.getImage();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "ESS (.ess)|*.ess";
            DialogResult result2 = saveFileDialog1.ShowDialog();

            if(result2.Equals(DialogResult.Cancel))
            {
                Environment.Exit(0);
            }

            cimg.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            Console.WriteLine("Press any key to close :D See Readme for details on opening compressed file");
            Console.ReadKey(); //EXIT SEQUENCE
        }
    }
}
