using System.Windows;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Defines a window that automatically applies acrylic background effects
/// and allows for title bar customization.
/// </summary>
public abstract class AcrylicWindow : ModernWindow
{
    static AcrylicWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AcrylicWindow), new FrameworkPropertyMetadata(typeof(AcrylicWindow)));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcrylicWindow"/>
    /// class.
    /// </summary>
    public AcrylicWindow()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? _, RoutedEventArgs __)
    {
        if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17134))
        {
            this.EnableMicaIfSupported();
        }
        else
        {
            this.EnableBlur();
        }
    }

    /// <summary>
    /// Destroys this instance of the <see cref="AcrylicWindow"/> class.
    /// </summary>
    ~AcrylicWindow()
    {
        Loaded -= OnLoaded;
    }
}
