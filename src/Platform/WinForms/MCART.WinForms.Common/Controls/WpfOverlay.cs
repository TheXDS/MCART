// WpfOverlay.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace TheXDS.MCART.Controls;

internal partial class WpfOverlay : Window
{
    static WpfOverlay()
    {
        AllowsTransparencyProperty.OverrideMetadata(typeof(WpfOverlay), new FrameworkPropertyMetadata(true));
        BackgroundProperty.OverrideMetadata(typeof(WpfOverlay), new FrameworkPropertyMetadata(null));
        ShowInTaskbarProperty.OverrideMetadata(typeof(WpfOverlay), new FrameworkPropertyMetadata(false));
        WindowStyleProperty.OverrideMetadata(typeof(WpfOverlay), new FrameworkPropertyMetadata(WindowStyle.None));
    }

    private readonly bool _designMode;
    private Form? _target;
    private bool _extendToCaption;
    private double horizontalOffset;
    private double verticalOffset;
    private double verticalSizeAdjust;

    public bool ExtendToCaption
    {
        get => _extendToCaption;
        set
        {
            if (_extendToCaption == value) return;
            _extendToCaption = value;
            UpdateOffsets();
            UpdateSize();
        }
    }

    public Form? Target
    {
        get => _target;
        set
        {
            if (_target == value) return;
            if (_target is not null)
            {
                _target.Move -= Target_OnUpdateOverlay;
                _target.SizeChanged -= Target_OnUpdateOverlay;
                _target.DpiChanged -= Target_OnDpiChanged;
                _target.FormClosed -= Target_FormClosed;
                new WindowInteropHelper(this).Owner = IntPtr.Zero;
                if (!_designMode) Hide();
            }
            if (value is not null)
            {
                value.Move += Target_OnUpdateOverlay;
                value.SizeChanged += Target_OnUpdateOverlay;
                value.DpiChanged += Target_OnDpiChanged;
                value.FormClosed += Target_FormClosed;
                new WindowInteropHelper(this).Owner = value.Handle;
                if (!_designMode) Show();
            }
            _target = value;
            UpdateSize();
        }
    }

    public WpfOverlay(bool designMode)
    {
        AllowsTransparency = true;
        Background = null;
        ShowInTaskbar = false;
        WindowStyle = WindowStyle.None;
        _designMode = designMode;
        UpdateOffsets();
    }

    private void Target_FormClosed(object? sender, FormClosedEventArgs e)
    {
        Close();
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);
        UpdateSize();
    }

    private void Target_OnDpiChanged(object? sender, System.Windows.Forms.DpiChangedEventArgs e)
    {
        UpdateOffsets();
        UpdateSize();
    }

    private void Target_OnUpdateOverlay(object? sender, EventArgs e)
    {
        UpdateSize();
    }

    private void UpdateOffsets()
    {
        var verticalAdjust = SystemInformation.CaptionHeight + (SystemInformation.VerticalResizeBorderThickness * 2);
        horizontalOffset = SystemInformation.HorizontalResizeBorderThickness * 2;
        verticalOffset = _extendToCaption ? 0 : verticalAdjust;
        verticalSizeAdjust = _extendToCaption ? verticalAdjust : 0;
    }

    private void UpdateSize()
    {
        if (_target is null) return;
        var location = _target.Location;
        var size = _target.ClientSize;
        var dpi = VisualTreeHelper.GetDpi(this);
        var xScaleFactor = 1.0 / dpi.DpiScaleX;
        var yScaleFactor = 1.0 / dpi.DpiScaleY;
        Left = location.X * xScaleFactor + horizontalOffset;
        Top = location.Y * yScaleFactor + verticalOffset;
        Width = size.Width * xScaleFactor;
        Height = size.Height * yScaleFactor + verticalSizeAdjust;
    }
}
