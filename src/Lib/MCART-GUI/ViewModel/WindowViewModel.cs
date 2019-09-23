/*
WindowViewModel.cs

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

using TheXDS.MCART.Types;

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    ///     ViewModel con propiedades básicas de gestión de la ventana.
    /// </summary>
    public abstract class WindowViewModel : ViewModelBase
    {
        private string _title;
        private Size _size;

        /// <summary>
        ///     Obtiene o establece el título de la ventana.
        /// </summary>
        public string Title
        {
            get => _title;
            set => Change(ref _title, value);
        }

        /// <summary>
        ///     Obtiene o establece la altura deseada de la ventana.
        /// </summary>
        public double WindowHeight
        {
            get => _size.Height;
            set
            {
                if (_size.Height == value) return;
                _size.Height = value;
                Notify(nameof(WindowHeight), nameof(WindowSize));
            }
        }

        /// <summary>
        ///     Obtiene o establece el ancho deseado de la ventana.
        /// </summary>
        public double WindowWidth
        {
            get => _size.Width;
            set
            {
                if (_size.Width == value) return;
                _size.Width = value;
                Notify(nameof(WindowWidth), nameof(WindowSize));
            }
        }

        /// <summary>
        ///     Obtiene o establece el tamaño deseado de la ventana.
        /// </summary>
        public Size WindowSize
        {
            get => _size;
            set
            {
                if (Change(ref _size, value))
                {
                    Notify(nameof(WindowHeight), nameof(WindowWidth));
                }
            }
        }
    }
}