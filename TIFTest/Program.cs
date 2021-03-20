using LibTIFSteganography;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;

namespace TIFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Opening image...");
            Image sourceImage = Bitmap.FromFile("images/scenic_beach.tif");
            byte[] randomMessage = GenerateRandomBytes(sourceImage.Width * sourceImage.Height);
            Bitmap bm = new Bitmap(sourceImage);

            TIFWriter tif = new TIFWriter();
            Bitmap bm2 = tif.Encode(bm, randomMessage);
            bm2.Save("test.tif", ImageFormat.Tiff);

            byte[] b3 = tif.Decode(bm2, randomMessage.Length);

            if (b3.SequenceEqual(randomMessage));
                Console.WriteLine("Encoded information recovered from image");

            Console.WriteLine("File Written.  Press ENTER to exit.");
            Console.ReadLine();
        }

        public static byte[] GenerateRandomBytes(int size)
        {
            byte[] rngOutput = new byte[size];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(rngOutput);

            return rngOutput;
        }
    }
}
