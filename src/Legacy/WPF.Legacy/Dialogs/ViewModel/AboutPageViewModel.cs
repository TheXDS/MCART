/*
AboutPageViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class AboutPageViewModel : AboutPageViewModelBase<IExposeExtendedGuiInfo<UIElement?>>
    {
        public UIElement? Icon => IsMcart ? WpfIcons.MCART : Element?.Icon;

        public AboutPageViewModel() : base()
        {
            RegisterPropertyChangeTrigger(nameof(Icon), nameof(Element));
        }

        protected override void OnLicense()
        {
            if (License is { LicenseUri: Uri uri }) Process.Start(uri.ToString());
            else if (License is { LicenseContent: var content })
            {
                Window? w = new()
                {
                    SizeToContent = SizeToContent.Width,
                    MaxWidth = 640,
                    MaxHeight = 480,
                    Content = new ScrollViewer
                    {
                        Content = new TextBox
                        {
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            FontFamily = new FontFamily("Consolas"),
                            Text = content,
                            AcceptsReturn = true,
                            IsReadOnly = true,
                            TextWrapping = TextWrapping.WrapWithOverflow
                        }
                    }
                };
                w.ShowDialog();
            }
        }

        protected override void OnAboutMcart()
        {
            WpfRtInfo.Show();
        }

        protected override void On3rdPartyLicenses()
        {

        }
    }
}