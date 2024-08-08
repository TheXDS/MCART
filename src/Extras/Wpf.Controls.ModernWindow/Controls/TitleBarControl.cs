using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Helpers.DependencyObjectHelpers;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Implements a window's title bar, with customizable title bar content.
/// </summary>
public class TitleBarControl : ContentControl
{
    private static readonly DependencyPropertyKey CloseWindowCommandPropertyKey;
    private static readonly DependencyPropertyKey HideWindowCommandPropertyKey;
    private static readonly DependencyPropertyKey ShowWindowCommandPropertyKey;

    /// <summary>
    /// Defines the <see cref="CloseWindowCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CloseWindowCommandProperty;

    /// <summary>
    /// Defines the <see cref="HideWindowCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty HideWindowCommandProperty;

    /// <summary>
    /// Defines the <see cref="ShowWindowCommand"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowWindowCommandProperty;

    /// <summary>
    /// Defines the <see cref="WindowReference"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty WindowReferenceProperty;

    static TitleBarControl()
    {
        var t = typeof(TitleBarControl);
        WindowReferenceProperty = NewDp<IWpfWindow, TitleBarControl>(nameof(WindowReference));
        (HideWindowCommandPropertyKey, HideWindowCommandProperty) = NewDpRo<ICommand, TitleBarControl>(nameof(HideWindowCommand));
        (ShowWindowCommandPropertyKey, ShowWindowCommandProperty) = NewDpRo<ICommand, TitleBarControl>(nameof(ShowWindowCommand));
        (CloseWindowCommandPropertyKey, CloseWindowCommandProperty) = NewDpRo<ICommand, TitleBarControl>(nameof(CloseWindowCommand));
        DefaultStyleKeyProperty.OverrideMetadata(t, new FrameworkPropertyMetadata(t));
        HeightProperty.OverrideMetadata(t, new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        VerticalAlignmentProperty.OverrideMetadata(t, new FrameworkPropertyMetadata(VerticalAlignment.Top, FrameworkPropertyMetadataOptions.AffectsArrange));
        HorizontalAlignmentProperty.OverrideMetadata(t, new FrameworkPropertyMetadata(HorizontalAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsArrange));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleBarControl"/> class.
    /// </summary>
    public TitleBarControl()
    {
        SetValue(CloseWindowCommandPropertyKey, new SimpleCommand(OnCloseWindow));
        SetValue(ShowWindowCommandPropertyKey, new SimpleCommand(OnShowWindow));
        SetValue(HideWindowCommandPropertyKey, new SimpleCommand(OnHideWindow));
    }

    /// <summary>
    /// Gets a reference to the window being controlled by this instance.
    /// </summary>
    public IWpfWindow? WindowReference
    {
        get => (IWpfWindow?)GetValue(WindowReferenceProperty);
        set => SetValue(WindowReferenceProperty, value);
    }

    /// <summary>
    /// Gets a command that hides the active window.
    /// </summary>
    public ICommand HideWindowCommand => (ICommand)GetValue(HideWindowCommandProperty)!;

    /// <summary>
    /// Gets a command that shows the active window.
    /// </summary>
    public ICommand ShowWindowCommand => (ICommand)GetValue(ShowWindowCommandProperty)!;

    /// <summary>
    /// Gets a command that closes the active window.
    /// </summary>
    public ICommand CloseWindowCommand => (ICommand)GetValue(CloseWindowCommandProperty)!;

    private void OnHideWindow()
    {
        WindowReference?.Minimize();
    }

    private void OnShowWindow()
    {
        WindowReference?.ToggleMaximize();
    }

    private void OnCloseWindow()
    {
        WindowReference?.Close();
    }
}

