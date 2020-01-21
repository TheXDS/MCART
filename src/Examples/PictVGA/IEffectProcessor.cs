using System.Windows.Media.Imaging;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Samples.PictVGA
{
    public interface IEffectProcessor : INameable
    {
        BitmapSource Process(BitmapImage input);
    }
}