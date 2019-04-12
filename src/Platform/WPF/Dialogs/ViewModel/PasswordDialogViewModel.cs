/*
AboutPageViewModel.cs

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

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <inheritdoc />
    /// <summary>
    ///     ViewModel para la gestión de contraseñas y credenciales.
    /// </summary>
    public class PasswordDialogViewModel : PasswordDialogViewModelBase
    {
        /// <summary>
        ///     Obtiene un comando que permite evaluar la calidad de la contraseña.
        /// </summary>
        public ObservingCommand EvaluateCommand { get; }
        /// <summary>
        ///     Obtiene un comando que permite generar una contraseña.
        /// </summary>
        public ObservingCommand GenerateCommand { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="PasswordDialogViewModel"/>.
        /// </summary>
        public PasswordDialogViewModel()
        {
            EvaluateCommand = new ObservingCommand(this, OnEvaluate, CanEvaluate, nameof(IsGeneratorVisible), nameof(Generator));
            GenerateCommand = new ObservingCommand(this, OnGenerate, CanGenerate, nameof(IsQualityVisible), nameof(Evaluator));
        }

        private bool CanGenerate(object arg)
        {
            return IsGeneratorVisible && !(Generator is null);
        }
        private bool CanEvaluate()
        {
            return IsQualityVisible && !(Evaluator is null);
        }
    }
}