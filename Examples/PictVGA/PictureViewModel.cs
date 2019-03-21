using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Samples.PictVGA
{
    class PictureViewModel : ViewModel.WpfWindowViewModel
    {
        private string _inputPicture;
        private IEffectProcessor _selectedFilter;
        private BitmapImage _sourceImage;

        public string InputPicture
        {
            get => _inputPicture;
            set
            {
                if (Change(ref _inputPicture, value))
                {
                    SourceImage = new BitmapImage(new Uri(InputPicture));
                }
            }
        }

        public ICommand BrowseCommand { get; }
        public BitmapImage SourceImage
        {
            get => _sourceImage;
            private set => Change(ref _sourceImage, value);
        }
        public BitmapSource DestImage
        {
            get
            {
                if (SourceImage is null) return null;
                return SelectedFilter?.Process(SourceImage);
            }
        }

        public IEffectProcessor SelectedFilter
        {
            get => _selectedFilter;
            set => Change(ref _selectedFilter, value);
        }

        public IEnumerable<IEffectProcessor> AvailableFilters { get; private set; }

        public PictureViewModel()
        {
            RegisterPropertyChangeBroadcast(nameof(SourceImage), nameof(DestImage));
            RegisterPropertyChangeBroadcast(nameof(SelectedFilter), nameof(DestImage));

            AvailableFilters = Objects.FindAllObjects<IEffectProcessor>();
            WindowSize = new Types.Size(800, 450);
        }
    }
    public interface IEffectProcessor : INameable
    {
        BitmapSource Process(BitmapImage input);
    }
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