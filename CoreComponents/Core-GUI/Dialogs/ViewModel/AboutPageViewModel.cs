/*
AboutPage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class AboutPageViewModel : NotifyPropertyChanged
    {
        private IExposeInfo _element;

        public IExposeInfo Element
        {
            get => _element;
            set
            {
                if (Equals(value, _element)) return;
                _element = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Copyright));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(Version));
                OnPropertyChanged(nameof(HasLicense));
                OnPropertyChanged(nameof(IsMcart));
            }
        }
        public string Name => Element?.Name;
        public string Copyright => Element?.Copyright;
        public string Description => Element?.Description;
        public string Author => Element?.Author;
        public string License => Element?.License;
        public Version Version => Element?.Version;
        public bool HasLicense => Element?.HasLicense ?? false;
        public UIElement Icon => Element?.Icon;

        public bool IsMcart => (Element as AssemblyDataExposer)?.Assembly == RTInfo.RTAssembly;
    }
}