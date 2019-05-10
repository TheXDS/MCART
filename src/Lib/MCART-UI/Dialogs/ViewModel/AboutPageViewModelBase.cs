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

#nullable enable

using System;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <summary>
    ///     ViewModel que describe el comportamiento de una ventana que muestra
    ///     información sobre un <see cref="IExposeInfo"/>.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de elemento para el cual mostrar detalles.
    /// </typeparam>
    public abstract class AboutPageViewModelBase<T> : ViewModelBase, INameable, IDescriptible where T : IExposeInfo
    {

#pragma warning disable CS8625
        private T _element = default;
#pragma warning restore CS8625

        private bool _showAboutMcart = true;
        private bool _showPluginInfo;
        private bool _isMcart;

        /// <summary>
        ///     Obtiene o establece un valor que indica si el
        ///     <see cref="IExposeInfo"/> presentado es MCART.
        /// </summary>
        public bool IsMcart
        {
            get => _isMcart;
            protected set => Change(ref _isMcart, value);
        }

        /// <summary>
        ///     Obtiene al autor del <see cref="IExposeInfo"/> para el cual se
        ///     presentan los detalles.
        /// </summary>
        public string? Author => Element?.Author;

        /// <summary>
        ///     Obtiene un valor que determina si el <see cref="IExposeInfo"/>
        ///     cumple con las especificaciones del CLS.
        /// </summary>
        public bool ClsCompliant => Element?.ClsCompliant ?? false;

        /// <summary>
        ///     Obtiene la información de Copyright del
        ///     <see cref="IExposeInfo"/> para el cual se presentan los 
        ///     detalles.
        /// </summary>
        public string? Copyright => Element?.Copyright;

        /// <summary>
        ///     Obtiene la descripción del <see cref="IExposeInfo"/> para el
        ///     cual se presentan los detalles.
        /// </summary>
        public string Description => Element?.Description;

        /// <summary>
        ///     Obtiene o establece el <see cref="IExposeInfo"/> para el cual
        ///     se presentan los detalles.
        /// </summary>
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

        /// <summary>
        ///     Obtiene un valor que determina si el <see cref="IExposeInfo"/>
        ///     contiene una licencia.
        /// </summary>
        public bool HasLicense => Element?.HasLicense ?? false;

        /// <summary>
        ///     Ejecuta una acción especial al cambiar el
        ///     <see cref="IExposeInfo"/> para el cual se presentan los
        ///     detalles.
        /// </summary>
        protected abstract void OnElementChanged();

        /// <summary>
        ///     Obtiene la licencia del <see cref="IExposeInfo"/> para el cual
        ///     se presentan los detalles.
        /// </summary>
        public string? License => Element?.License;

        /// <summary>
        ///     Obtiene el nombrer del <see cref="IExposeInfo"/> para el cual
        ///     se presentan los detalles.
        /// </summary>
        public string Name => Element?.Name;

        /// <summary>
        ///     Obtiene o establece un valor que indica si debe mostrarse un
        ///     vínculo a una nueva ventana para ver detalles sobre MCART.
        /// </summary>
        public bool ShowAboutMcart
        {
            get => _showAboutMcart && !IsMcart;
            set => Change(ref _showAboutMcart, value);
        }

        /// <summary>
        ///     Obtiene o establece un valor que indica si debe mostrarse un
        ///     vínculo al explorador de plugins de MCART.
        /// </summary>
        public bool ShowPluginInfo
        {
            get => _showPluginInfo;
            set => Change(ref _showPluginInfo, value);
        }

        /// <summary>
        ///     Obtiene la versión del <see cref="IExposeInfo"/> para el cual
        ///     se presentan los detalles.
        /// </summary>
        public Version? Version => Element?.Version;
    }
}