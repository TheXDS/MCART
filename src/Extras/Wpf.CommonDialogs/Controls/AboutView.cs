/*
AboutView.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Wpf.CommonDialogs.Controls;

/// <summary>
/// Control that allows displaying an information box for an assembly,
/// application, program, or other similar item.
/// </summary>
public class AboutView : Control
{
    /// <summary>
    /// Identifies the dependency property <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(ImageSource), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="Title"/>.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="Version"/>.
    /// </summary>
    public static readonly DependencyProperty VersionProperty = DependencyProperty.Register(
        nameof(Version), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="Description"/>.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="Copyright"/>.
    /// </summary>
    public static readonly DependencyProperty CopyrightProperty = DependencyProperty.Register(
        nameof(Copyright), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="LicenseSource"/>.
    /// </summary>
    public static readonly DependencyProperty LicenseSourceProperty = DependencyProperty.Register(
        nameof(LicenseSource), typeof(object), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property <see cref="ThirdPartyLicenseSource"/>.
    /// </summary>
    public static readonly DependencyProperty ThirdPartyLicenseSourceProperty = DependencyProperty.Register(
        nameof(ThirdPartyLicenseSource), typeof(IEnumerable<object>), typeof(AboutView), new PropertyMetadata(null));

    static AboutView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AboutView), new FrameworkPropertyMetadata(typeof(AboutView)));
    }

    /// <summary>
    /// Gets or sets the icon to be displayed in the information box.
    /// </summary>
    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Gets or sets the title of the item for which information is displayed.
    /// </summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets a string that represents the version of the item
    /// for which information is displayed.
    /// </summary>
    public string? Version
    {
        get => (string?)GetValue(VersionProperty);
        set => SetValue(VersionProperty, value);
    }

    /// <summary>
    /// Gets or sets the description of the item for which information is displayed.
    /// </summary>
    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Gets or sets the copyright string of the item for which information is 
    /// displayed.
    /// </summary>
    public string? Copyright
    {
        get => (string?)GetValue(CopyrightProperty);
        set => SetValue(CopyrightProperty, value);
    }

    /// <summary>
    /// Gets or sets an object to be used as the source of license information
    /// to be displayed in this information box.
    /// </summary>
    public object? LicenseSource
    {
        get => (object?)GetValue(LicenseSourceProperty);
        set => SetValue(LicenseSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets a collection of objects to be used as the source of
    /// third-party license information to be displayed in this information box.
    /// </summary>
    public IEnumerable<object>? ThirdPartyLicenseSource
    {
        get => (IEnumerable<object>?)GetValue(ThirdPartyLicenseSourceProperty);
        set => SetValue(ThirdPartyLicenseSourceProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        switch (GetTemplateChild("PART_3rdPartyCommand"))
        {
            case ButtonBase bb:
                bb.Command = new SimpleCommand(On3rdPartyLicense);
                break;
            case Hyperlink hy:
                hy.Click += (_, _) => On3rdPartyLicense();
                break;
        }
    }

    private void OnShowLicense()
    {
        License license = GetLicense(LicenseSource);
        Window? w = new()
        {
            Title = license.Name,
            SizeToContent = SizeToContent.Width,
            MaxWidth = 640,
            MaxHeight = 480,
            Content = BuildPresenter(license)
        };
        w.ShowDialog();
    }

    private void On3rdPartyLicense()
    {
        if (ThirdPartyLicenseSource is null) return;
        var t = new TabControl();
        foreach (var j in ThirdPartyLicenseSource
            .Select(p => GetLicense(p))
            .Select(p => new TabItem
            {
                Header = p.Name,
                Content = BuildPresenter(p)
            }))
        {
            t.Items.Add(j);
        }
        Window? w = new()
        {
            Title = "",
            SizeToContent = SizeToContent.Width,
            MaxWidth = 640,
            MaxHeight = 480,
            Content = t,
        };
        w.ShowDialog();
    }

    private static License GetLicense(object? obj)
    {
        return obj switch
        {
            LicenseAttributeBase a => a.GetLicense(obj),
            Assembly a => a.GetAttribute<LicenseAttributeBase>()?.GetLicense(a),
            IExposeAssembly e => e.Assembly.GetAttribute<LicenseAttributeBase>()?.GetLicense(e),
            IExposeInfo i => i.License,
            string s => new TextLicense(s.Split('\n', 2)[0], s),
            not null => obj.GetAttribute<LicenseAttributeBase>()?.GetLicense(obj),
            null => License.MissingLicense,
        } ?? License.Unspecified;
    }

    private static FrameworkElement BuildPresenter(License license)
    {
        return new ScrollViewer
        {
            Content = new TextBox
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                FontFamily = new FontFamily("Consolas"),
                Text = license.LicenseContent,
                AcceptsReturn = true,
                IsReadOnly = true,
                TextWrapping = TextWrapping.WrapWithOverflow
            }
        };
    }
}
