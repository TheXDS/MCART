/*
PasswordDialogViewModel.cs

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

using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <summary>
    /// ViewModel para la gestión de contraseñas y credenciales.
    /// </summary>
    public class PasswordDialogViewModel : PasswordDialogViewModelBase
    {
        /// <summary>
        /// Obtiene un comando que permite evaluar la calidad de la contraseña.
        /// </summary>
        public ObservingCommand EvaluateCommand { get; }
        /// <summary>
        /// Obtiene un comando que permite generar una contraseña.
        /// </summary>
        public ObservingCommand GenerateCommand { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PasswordDialogViewModel"/>.
        /// </summary>
        public PasswordDialogViewModel()
        {
            EvaluateCommand = new ObservingCommand(this, OnEvaluate)
                .SetCanExecute(CanEvaluate)
                .RegisterObservedProperty(nameof(IsGeneratorVisible), nameof(Generator));
            GenerateCommand = new ObservingCommand(this, OnGenerate)
                .SetCanExecute(CanGenerate)
                .RegisterObservedProperty(nameof(IsQualityVisible), nameof(Evaluator));
        }

        private bool CanGenerate(object? arg)
        {
            return IsGeneratorVisible && Generator is not null;
        }
        private bool CanEvaluate()
        {
            return IsQualityVisible && Evaluator is not null;
        }
    }
}