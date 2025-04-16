using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control that allows a video stream (usually from a web camera) to be
/// previewed by the user, as well as allowing for video stream configuration.
/// </summary>
/// <remarks>
/// The video stream will be opened in exclusive mode, so this control is not
/// suitable to be used by multiple preview panels, or concurrently with video
/// transmission.
/// </remarks>
[CLSCompliant(false)]
public partial class WebCamPane : Control
{
    static WebCamPane()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(WebCamPane), new FrameworkPropertyMetadata(typeof(WebCamPane)));
    }

    private static readonly DependencyPropertyKey CurrentFrameratePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(CurrentFramerate),
        typeof(double),
        typeof(WebCamPane),
        new PropertyMetadata(double.NaN));

    private static readonly DependencyPropertyKey FrameBufferPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(FrameBuffer),
        typeof(BitmapSource),
        typeof(WebCamPane),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceFrameBuffer));

    private static readonly DependencyPropertyKey IsBusyPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(IsBusy),
        typeof(bool),
        typeof(WebCamPane),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, null));

    /// <summary>
    /// Identifies the <see cref="OverlayContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty OverlayContentProperty = DependencyProperty.Register(
        nameof(OverlayContent),
        typeof(object),
        typeof(WebCamPane),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="DisabledContent"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DisabledContentProperty = DependencyProperty.Register(
        nameof(DisabledContent),
        typeof(object),
        typeof(WebCamPane),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifies the <see cref="IsActive"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
        nameof(IsActive),
        typeof(bool),
        typeof(WebCamPane),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnIsActiveChanged));

    /// <summary>
    /// Identifies the <see cref="SelectedVideoDevice"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SelectedVideoDeviceProperty = DependencyProperty.Register(
        nameof(SelectedVideoDevice),
        typeof(VideoCaptureDevice),
        typeof(WebCamPane),
        new PropertyMetadata(null, OnDeviceChanged));

    /// <summary>
    /// Identifies the <see cref="ResolutionProperty"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ResolutionProperty = DependencyProperty.Register(
        nameof(Resolution),
        typeof(VideoCapabilities),
        typeof(WebCamPane),
        new PropertyMetadata(null, OnReslutionChanged, CoerceResolution));

    /// <summary>
    /// Identifies the <see cref="CurrentFramerate"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CurrentFramerateProperty = CurrentFrameratePropertyKey.DependencyProperty;

    /// <summary>
    /// Identifies the <see cref="FrameBuffer"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty FrameBufferProperty = FrameBufferPropertyKey.DependencyProperty;

    /// <summary>
    /// Identifies the <see cref="IsBusy"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty IsBusyProperty = IsBusyPropertyKey.DependencyProperty;

    private static object? CoerceResolution(DependencyObject d, object baseValue)
    {
        var w = (WebCamPane)d;
        return baseValue is VideoCapabilities sz && w.SelectedVideoDevice?.VideoCapabilities is { } vc
            ? vc.Contains(sz) ? sz : vc.OrderByDescending(p => p.FrameSize).FirstOrDefault()
            : null;
    }

    private static void OnDeviceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WebCamPane p && p.IsActive)
        {
            p.IsActive = false;
        }
    }

    private static void OnReslutionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WebCamPane { SelectedVideoDevice: { } v })
        {
            v.VideoResolution = (VideoCapabilities)e.NewValue;
        }
    }

    private readonly object _syncLock = new();
    private DateTime? _fpscalc = null;

    /// <summary>
    /// Gets or sets the current resolution for the active video source.
    /// </summary>
    public VideoCapabilities Resolution
    {
        get => (VideoCapabilities)GetValue(ResolutionProperty);
        set => SetValue(ResolutionProperty, value);
    }

    /// <summary>
    /// Gets or sets the currently active video source.
    /// </summary>
    public VideoCaptureDevice? SelectedVideoDevice
    {
        get => (VideoCaptureDevice?)GetValue(SelectedVideoDeviceProperty);
        set => SetValue(SelectedVideoDeviceProperty, value);
    }

    /// <summary>
    /// Gets the current framerate of the camera.
    /// </summary>
    public double CurrentFramerate => (double)GetValue(CurrentFramerateProperty);

    /// <summary>
    /// Gets or sets the content to be overlaid on top of the captured
    /// video whenever the camera is active. This content will be overlaid
    /// only over the preview, not the actual framebuffer.
    /// </summary>
    public object? OverlayContent
    {
        get => GetValue(OverlayContentProperty);
        set => SetValue(OverlayContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content to be displayed whenever the camera
    /// preview is inactive.
    /// </summary>
    public object? DisabledContent
    {
        get => GetValue(DisabledContentProperty);
        set => SetValue(DisabledContentProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that turns on or off the video capture
    /// preview.
    /// </summary>
    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    /// <summary>
    /// Gets a <see cref="BitmapSource"/> with the curent framebuffer of
    /// the captured video.
    /// </summary>
    /// <value>
    /// A <see cref="BitmapSource"/> with the current framebuffer of the
    /// captured video, or <see langword="null"/> if the camera is not
    /// active.
    /// </value>
    public BitmapSource? FrameBuffer => (BitmapSource?)GetValue(FrameBufferProperty);

    /// <summary>
    /// Gets a value that indicates if this control is busy initializing
    /// the selected camera.
    /// </summary>
    public bool IsBusy => (bool)GetValue(IsBusyProperty);

    private static object? CoerceFrameBuffer(DependencyObject d, object baseValue)
    {
        if (d is not WebCamPane p) return null;
        return p.IsActive ? baseValue : null;
    }

    private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not WebCamPane p) return;
        if (e.NewValue is bool b && b)
        {
            p.OnStartVideo();
        }
        else
        {
            p.OnStopVideo();
        }
    }

    private void OnStartVideo()
    {
        if (SelectedVideoDevice?.VideoResolution is null)
        {
            IsActive = false;
            return;
        }
        SelectedVideoDevice.NewFrame += SelectedVideoDevice_NewFrame;
        SelectedVideoDevice.Start();
    }

    private async void OnStopVideo()
    {
        if (SelectedVideoDevice?.VideoResolution is null)
        {
            return;
        }
        SetValue(IsBusyPropertyKey, true);
        SelectedVideoDevice.SignalToStop();
        await Task.Run(SelectedVideoDevice.WaitForStop);
        SelectedVideoDevice.NewFrame -= SelectedVideoDevice_NewFrame;
        SetValue(CurrentFrameratePropertyKey, double.NaN);
        SetValue(FrameBufferPropertyKey, null);
        _fpscalc = null;
        SetValue(IsBusyPropertyKey, false);
    }

    private void SelectedVideoDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        lock (_syncLock)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    IntPtr hbitmap = eventArgs.Frame.GetHbitmap();
                    BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    bitmapSource.Freeze();
                    DeleteObject(hbitmap);
                    SetValue(CurrentFrameratePropertyKey, CalcFps());
                    SetValue(FrameBufferPropertyKey, bitmapSource);
                });
            }
            catch (TaskCanceledException)
            {
                (sender as VideoCaptureDevice)?.SignalToStop();
            }
            catch
            {
                Dispatcher.Invoke(() => IsActive = false);
            }
        }
    }

    private double CalcFps()
    {
        double fps;
        if (_fpscalc is not null){
            var ms = (DateTime.Now - _fpscalc.Value).TotalMilliseconds;
            fps = ms > 0 ? (1000.0 / ms).Clamp(0, 240) : double.PositiveInfinity;
        }
        else
        {
            fps = double.NaN;
        }
        _fpscalc = DateTime.Now;
        return fps;
    }

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool DeleteObject(IntPtr value);
}
