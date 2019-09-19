using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Samples.PictVGA
{
    public class NoiseProcessor : IEffectProcessor
    {
        public string Name => "Agrega ruido a una imagen";

        public BitmapSource Process(BitmapImage input)
        {
            var wb = new WriteableBitmap(input);
            try
            {
                // Reserve the back buffer for updates.
                wb.Lock();
                var r = new Random();
                for(int j = 0; j < 32768; j++)
                {

                    int column = r.Next((int)wb.Width);
                    int row = r.Next((int)wb.Height);
                    unsafe
                    {
                        // Get a pointer to the back buffer.
                        int pBackBuffer = (int)wb.BackBuffer;

                        // Find the address of the pixel to draw.
                        pBackBuffer += row * wb.BackBufferStride;
                        pBackBuffer += column * 4;

                        // Compute the pixel's color.
                        int color_data = 255 << 16; // R
                        color_data |= 128 << 8;   // G
                        color_data |= 255 << 0;   // B

                        // Assign the color data to the pixel.
                        *((int*)pBackBuffer) = color_data;
                    }

                    // Specify the area of the bitmap that changed.
                    wb.AddDirtyRect(new Int32Rect(column, row, 1, 1));
                }
            }
            finally
            {
                wb.Unlock();
            }
            return wb;
        }
    }
}