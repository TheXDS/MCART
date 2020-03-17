/*
AboutPageViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;

#nullable enable

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class AboutPageViewModel : AboutPageViewModelBase<ApplicationInfo>
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
                var w = new Window
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