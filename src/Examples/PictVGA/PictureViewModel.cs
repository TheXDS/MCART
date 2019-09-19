using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Dialogs;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Samples.PictVGA
{
    class PictureViewModel : WpfWindowViewModel
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
        public ICommand AboutCommand { get; }
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

            AboutCommand = new SimpleCommand(OnAbout);

            AvailableFilters = Objects.FindAllObjects<IEffectProcessor>();
            WindowSize = new Types.Size(800, 450);
        }

        private void OnAbout()
        {
            AboutBox.ShowDialog(Application.Current);
        }
    }
}