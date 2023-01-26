﻿/*
AboutView.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control que permite mostrar un cuadro de información de un ensamblado,
/// aplicación, programa u otro elemento similar.
/// </summary>
public class AboutView : Control
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(ImageSource), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Title"/>.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Version"/>.
    /// </summary>
    public static readonly DependencyProperty VersionProperty = DependencyProperty.Register(
        nameof(Version), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Description"/>.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Copyright"/>.
    /// </summary>
    public static readonly DependencyProperty CopyrightProperty = DependencyProperty.Register(
        nameof(Copyright), typeof(string), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="LicenseSource"/>.
    /// </summary>
    public static readonly DependencyProperty LicenseSourceProperty = DependencyProperty.Register(
        nameof(LicenseSource), typeof(object), typeof(AboutView), new PropertyMetadata(null));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="ThirdPartyLicenseSource"/>.
    /// </summary>
    public static readonly DependencyProperty ThirdPartyLicenseSourceProperty = DependencyProperty.Register(
        nameof(ThirdPartyLicenseSource), typeof(IEnumerable<object>), typeof(AboutView), new PropertyMetadata(null));

    static AboutView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AboutView), new FrameworkPropertyMetadata(typeof(AboutView)));
    }

    /// <summary>
    /// Obtiene o establece el ícono a mostrar en el cuadro de información.
    /// </summary>
    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el título del elemento del cual se muestra la
    /// información.
    /// </summary>
    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Obtiene o establece una cadena que representa la versión del elemento
    /// del cual se muestra la información.
    /// </summary>
    public string? Version
    {
        get => (string?)GetValue(VersionProperty);
        set => SetValue(VersionProperty, value);
    }

    /// <summary>
    /// Obtiene o establece la descripción del elemento del cual se muestra la
    /// información.
    /// </summary>
    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    /// <summary>
    /// Obtiene o establece la cadena de Copyright del elemento del cual se 
    /// muestra la información.
    /// </summary>
    public string? Copyright
    {
        get => (string?)GetValue(CopyrightProperty);
        set => SetValue(CopyrightProperty, value);
    }

    /// <summary>
    /// Obtiene o establece un objeto a utilizar como el origen de información
    /// de licencia a mostrar en este cuadro de información.
    /// </summary>
    public object? LicenseSource
    {
        get => (object?)GetValue(LicenseSourceProperty);
        set => SetValue(LicenseSourceProperty, value);
    }

    /// <summary>
    /// Obtiene o establece una colección de objetos a utilizar como origen de
    /// información de licencias de terceros a mostrar en este cuadro de
    /// información.
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
            Assembly a => a.GetAttr<LicenseAttributeBase>()?.GetLicense(a),
            IExposeAssembly e => e.Assembly.GetAttr<LicenseAttributeBase>()?.GetLicense(e),
            IExposeInfo i => i.License,
            string s => new TextLicense(s.Split('\n', 2)[0], s),
            not null => obj.GetAttr<LicenseAttributeBase>()?.GetLicense(obj),
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
