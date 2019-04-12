/*
AboutPageViewModelBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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
using TheXDS.MCART.Component;
using TheXDS.MCART.Types;
using TheXDS.MCART.ViewModel;

// ReSharper disable MemberCanBePrivate.Global

namespace TheXDS.MCART.Dialogs.ViewModel
{
    public abstract class AboutPageViewModelBase<T> : ViewModelBase, INameable, IDescriptible where T : IExposeInfo
    {
        private T _element;
        private bool _showAboutMcart = true;
        private bool _showPluginInfo;
        private bool _isMcart;

        public bool IsMcart
        {
            get => _isMcart;
            protected set => Change(ref _isMcart, value);
        }

        public string Author => Element?.Author;
        public bool ClsCompliant => Element?.ClsCompliant ?? false;
        public string Copyright => Element?.Copyright;
        public string Description => Element?.Description;
        public T Element
        {
            get => _element;
            set
            {
                if (!Change(ref _element, value)) return;
                Notify(new[]{
                    nameof(Author),
                    nameof(ClsCompliant),
                    nameof(Copyright),
                    nameof(Description),
                    nameof(HasLicense),
                    nameof(IsMcart),
                    nameof(License),
                    nameof(Name),
                    nameof(ShowAboutMcart),
                    nameof(ShowPluginInfo),
                    nameof(Version)
                });
                OnElementChanged();
            }
        }
        public bool HasLicense => Element?.HasLicense ?? false;

        protected abstract void OnElementChanged();

        public string License => Element?.License;
        public string Name => Element?.Name;

        public bool ShowAboutMcart
        {
            get => _showAboutMcart && !IsMcart;
            set => Change(ref _showAboutMcart, value);
        }
        public bool ShowPluginInfo
        {
            get => _showPluginInfo;
            set => Change(ref _showPluginInfo, value);
        }
        public Version Version => Element?.Version;
    }
}