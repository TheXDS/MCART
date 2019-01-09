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
using TheXDS.MCART.Component;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;

// ReSharper disable MemberCanBePrivate.Global

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class AboutPageViewModel : NotifyPropertyChanged, INameable, IDescriptible
    {
        private IExposeInfo _element;
        private bool _showAboutMcart = true;
        private bool _showPluginInfo;

        public string Author => Element?.Author;
        public bool ClsCompliant => Element?.ClsCompliant ?? false;
        public string Copyright => Element?.Copyright;
        public string Description => Element?.Description;
        public IExposeInfo Element
        {
            get => _element;
            set
            {
                if (Equals(value, _element)) return;
                _element = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(ClsCompliant));
                OnPropertyChanged(nameof(Copyright));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(HasLicense));
                OnPropertyChanged(nameof(Icon));
                OnPropertyChanged(nameof(IsMcart));
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(ShowAboutMcart));
                OnPropertyChanged(nameof(ShowPluginInfo));
                OnPropertyChanged(nameof(Version));
            }
        }
        public bool HasLicense => Element?.HasLicense ?? false;
        public UIElement Icon => Element?.Icon;
        public bool IsMcart => (Element as AssemblyDataExposer)?.Assembly == RTInfo.RTAssembly;
        public string License => Element?.License;
        public string Name => Element?.Name;
        public bool ShowAboutMcart
        {
            get => _showAboutMcart && !IsMcart;
            set
            {
                if (value == _showAboutMcart) return;
                _showAboutMcart = value;
                OnPropertyChanged();
            }
        }
        public bool ShowPluginInfo
        {
            get => _showPluginInfo;
            set
            {
                if (value == _showPluginInfo) return;
                _showPluginInfo = value;
                OnPropertyChanged();
            }
        }
        public Version Version => Element?.Version;
    }
}