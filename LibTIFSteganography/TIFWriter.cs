using System;
using System.Drawing;

namespace LibTIFSteganography
{
    /*
     * TIF
     * 
     * Encodes/Decodes data into TIF images using the Alpha channel.  
     */

    public class TIFWriter
    {
        public Bitmap Encode(Bitmap sourceTIFImage, byte[] data)
        {
            if (data.Length > sourceTIFImage.Width * sourceTIFImage.Height)
                throw new Exception("Image to small.");

            Bitmap encodedTIFImage = new Bitmap(sourceTIFImage);
            int index = 0;

            for(int i = 0; i < encodedTIFImage.Height; i++)
            {
                for(int j = 0; j < encodedTIFImage.Width; j++)
                {
                    if (index < data.Length)
                    {
                        Color pixel = encodedTIFImage.GetPixel(j, i);
                        Color p = Color.FromArgb(data[index++], pixel.R, pixel.G, pixel.B);
                        encodedTIFImage.SetPixel(j, i, p);
                    }
                    else
                        break;
                }
            }

            return encodedTIFImage;
        }

        public byte[] Decode(Bitmap encodedTIFImage, int size)
        {
            if (size > encodedTIFImage.Width * encodedTIFImage.Height)
                throw new Exception("Image to small.");

            byte[] result = new byte[size];
            int index = 0;

            for (int i = 0; i < encodedTIFImage.Height; i++)
            {
                for (int j = 0; j < encodedTIFImage.Width; j++)
                {
                    if (index < size)
                    {
                        Color pixel = encodedTIFImage.GetPixel(j, i);
                        result[index++] = pixel.A;
                    }
                    else
                        break;
                }
            }

            return result;
        }
    }
}
