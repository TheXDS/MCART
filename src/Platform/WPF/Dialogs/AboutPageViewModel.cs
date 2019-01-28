/*
AboutPageViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.ViewModel;

// ReSharper disable MemberCanBePrivate.Global

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class AboutPageViewModel : AboutPageViewModelBase<ApplicationInfo>, INameable, IDescriptible
    {
        public UIElement Icon => Element?.Icon;

        protected override void OnElementChanged()
        {
            OnPropertyChanged(nameof(Icon));
            IsMcart = Element?.Assembly == RTInfo.RTAssembly;
            AboutMcartCommand.SetCanExecute(IsMcart);
            PluginInfoCommand.SetCanExecute(ShowPluginInfo);
            LicenseCommand.SetCanExecute(HasLicense);
        }

        public SimpleCommand AboutMcartCommand { get; }
        public SimpleCommand PluginInfoCommand { get; }
        public SimpleCommand LicenseCommand { get; }

        public AboutPageViewModel()
        {
            AboutMcartCommand = new SimpleCommand(OnAboutMcart, false);
            PluginInfoCommand = new SimpleCommand(OnPluginInfo, false);
            LicenseCommand = new SimpleCommand(OnLicense, false);
        }

        private void OnLicense()
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
                        Text = License,
                        AcceptsReturn = true,
                        IsReadOnly = true,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    }
                }
            };
            w.ShowDialog();
        }

        private void OnPluginInfo()
        {
            throw new NotImplementedException();
//            new PluginBrowser().ShowDialog();

        }

        private void OnAboutMcart()
        {
            throw new NotImplementedException();
//            RTInfo.Show();

        }
    }
}