using System.Windows.Media;
using Windows.UI.ViewManagement;
using MC = TheXDS.MCART.Types.Color;
using DC = System.Drawing.Color;

namespace TheXDS.MCART.Wpf.Win19041.Types.Extensions;

/// <summary>
/// Contiene extensiones para el tipo <see cref="UIColorType"/>.
/// </summary>
[CLSCompliant(false)]
public static class UIColorTypeExtensions
{
    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="Brush"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="Brush"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static Brush ToMediaBrush(this UIColorType color)
    {
        return new SolidColorBrush(color.ToMediaColor());
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="Color"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static Color ToMediaColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return Color.FromArgb(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="DC"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="DC"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static DC ToDrawingColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return DC.FromArgb(c.A, c.R, c.G, c.B);
    }

    /// <summary>
    /// Convierte un <see cref="UIColorType"/> en un <see cref="MC"/>.
    /// </summary>
    /// <param name="color">Color a convertir.</param>
    /// <returns>
    /// Un nuevo <see cref="MC"/> equivalente al <see cref="UIColorType"/>
    /// especificado.
    /// </returns>
    public static MC ToColor(this UIColorType color)
    {
        var c = new UISettings().GetColorValue(color);
        return new MC(c.A, c.R, c.G, c.B);
    }
}
