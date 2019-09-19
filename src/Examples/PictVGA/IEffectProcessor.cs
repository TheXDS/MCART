using System.Windows.Media.Imaging;
using TheXDS.MCART.Types;

namespace TheXDS.MCART.Samples.PictVGA
{
    public interface IEffectProcessor : INameable
    {
        BitmapSource Process(BitmapImage input);
    }
}