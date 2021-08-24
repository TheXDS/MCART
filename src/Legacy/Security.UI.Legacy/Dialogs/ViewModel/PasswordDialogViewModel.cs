/*
PasswordDialogViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using System.Security;
using System.Threading.Tasks;
using TheXDS.MCART.Security.Password;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    /// <summary>
    /// ViewModel básico para un cuadro de diálogo para inicio de sesión y
    /// administración de contraseñas y credenciales.
    /// </summary>
    public abstract class PasswordDialogViewModelBase : ViewModelBase
    {
        private SecureString? _confirm;
        private IPasswordEvaluator? _evaluator;
        private string? _generatedPassword;
        private IPasswordGenerator? _generator;
        private string? _hint;
        private int? _maxTries;
        private PasswordDialogMode _mode;
        private SecureString _password = null!;
        private PwEvalResult _result;
        private string? _title;
        private int _triesCount;
        private string? _user;
        private LoginValidator? _validator;

        /// <summary>
        /// Ejecuta una acción de evaluación de la contraseña.
        /// </summary>
        public void OnEvaluate()
        {
            if (!Mode.HasFlag(PasswordDialogMode.PwQuality) || Password is null) return;
            Result = Password.Length == 0 ? PwEvalResult.Empty : Evaluator?.Evaluate(Password) ?? PwEvalResult.Null;
        }

        /// <summary>
        /// Ejecuta una acción de generación de contraseñas.
        /// </summary>
        public void OnGenerate()
        {
            if (!Mode.HasFlag(PasswordDialogMode.Generator)) return;
            Password = Generator!.Generate();
            Confirm = Password;
            GeneratedPassword = Password.Read();
            OnPropertyChanged(nameof(GeneratedPassword));
        }

        /// <summary>
        /// Indica si el cuadro de confirmación está visible.
        /// </summary>
        public bool IsConfirmVisible => Mode.HasFlag(PasswordDialogMode.Confirm);

        /// <summary>
        /// Indica si el generador de contraseñas está visible.
        /// </summary>
        public bool IsGeneratorVisible => Mode.HasFlag(PasswordDialogMode.Generator);

        /// <summary>
        /// Indica si el cuadro de pista para contraseña está visible.
        /// </summary>
        public bool IsHintVisible => Mode.HasFlag(PasswordDialogMode.Hint);

        /// <summary>
        /// Indica si la contraseña ha sido evaluada como inválida.
        /// </summary>
        public bool IsInvalid => Result.Critical;

        /// <summary>
        /// Indica si el indicador de calidad de contraseña está visible.
        /// </summary>
        public bool IsQualityVisible => Mode.HasFlag(PasswordDialogMode.PwQuality);

        /// <summary>
        /// Indica si el cuadro de usuario está visible.
        /// </summary>
        public bool IsUserVisible => Mode.HasFlag(PasswordDialogMode.User);

        /// <summary>
        /// Obtiene mensajes adicionales sobre la evaluación de una
        /// contraseña.
        /// </summary>
        public string? MorInfo => Result.Details;

        /// <summary>
        /// Obtiene un porcentaje evaluado de calidad de contraseña.
        /// </summary>
        public float PasswordQuality => Result.Result * 100;

        /// <summary>
        /// Obtiene o establece la contraseña introducida en el cuadro de
        /// confirmación.
        /// </summary>
        public SecureString? Confirm
        {
            get => _confirm;
            set => Change(ref _confirm, value);
        }

        /// <summary>
        /// Obtiene o establece un <see cref="IPasswordEvaluator"/> a
        /// utilizar para comprobar la calidad de una contraseña.
        /// </summary>
        public IPasswordEvaluator? Evaluator
        {
            get => _evaluator;
            set
            {
                if (Change(ref _evaluator, value)) OnEvaluate();
            }
        }

        /// <summary>
        /// Obtiene una contraseña generada por este ViewModel.
        /// </summary>
        public string? GeneratedPassword
        {
            get => _generatedPassword;
            private set => Change(ref _generatedPassword, value);
        }

        /// <summary>
        /// Obtiene o establece un <see cref="IPasswordGenerator"/> a 
        /// utilizar para generar contraseñas.
        /// </summary>
        public IPasswordGenerator? Generator
        {
            get => _generator;
            set
            {
                if (Change(ref _generator, value)) OnGenerate();
            }
        }

        /// <summary>
        /// Obtiene o establece un indicio de contraseña.
        /// </summary>
        public string? Hint
        {
            get => _hint;
            set => Change(ref _hint, value);
        }

        /// <summary>
        /// Número máximo de intentos permitidos. Si se establece en
        /// <see langword="null"/>, no se establecerá un límite de
        /// intentos.
        /// </summary>
        public int? MaxTries
        {
            get => _maxTries;
            set => Change(ref _maxTries, value);
        }

        /// <summary>
        /// Obtiene o establece el modo de funcionamiento de este ViewModel.
        /// </summary>
        public PasswordDialogMode Mode
        {
            get => _mode;
            set
            {
                if (!Change(ref _mode, value)) return;
                OnPropertyChanged(nameof(IsUserVisible));
                OnPropertyChanged(nameof(IsConfirmVisible));
                OnPropertyChanged(nameof(IsHintVisible));
                OnPropertyChanged(nameof(IsQualityVisible));
                OnPropertyChanged(nameof(IsGeneratorVisible));
            }
        }

        /// <summary>
        /// Obtiene o establece la contraseña.
        /// </summary>
        public SecureString Password
        {
            get => _password;
            set
            {
                if (Change(ref _password, value)) OnEvaluate();
            }
        }

        /// <summary>
        /// Obtiene el resultado de la evaluación de la contraseña.
        /// </summary>
        public PwEvalResult Result
        {
            get => _result;
            private set
            {
                if (!Change(ref _result, value)) return;
                OnPropertyChanged(nameof(PasswordQuality));
                OnPropertyChanged(nameof(MorInfo));
                OnPropertyChanged(nameof(IsInvalid));
            }
        }

        /// <summary>
        /// Obtiene o establce el título de este ViewModel.
        /// </summary>
        public string? Title
        {
            get => _title;
            set => Change(ref _title, value);
        }

        /// <summary>
        /// Obtiene la cantidad de intentos de inicio de sesión actualmente
        /// realizados.
        /// </summary>
        public int TriesCount
        {
            get => _triesCount;
            private set => Change(ref _triesCount, value);
        }

        /// <summary>
        /// Obtiene o establece el nombre de usuario.
        /// </summary>
        public string? User
        {
            get => _user;
            set => Change(ref _user, value);
        }

        /// <summary>
        /// Obtiene o establece un validador de inicio de sesión a utilizar
        /// al presionar el botón de continuar.
        /// </summary>
        public LoginValidator? Validator
        {
            get => _validator;
            set => Change(ref _validator, value);
        }

        /// <summary>
        /// Realiza una validación de inicio de sesión de forma asíncrona.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si la validación se ha realizado con
        /// éxito, <see langword="false"/> si la validación ha fallado, o
        /// <see langword="null"/> si no se ha establecido una función de
        /// validación.
        /// </returns>
        public async Task<bool?> ValidateAsync()
        {
            if (Password is null || Password.Length == 0) return false;
            if (TriesCount > MaxTries) return false;
            if (TriesCount++ > MaxTries) return false;
            if (Validator is null) return null;
            IsBusy = true;
            var t = await Validator(new Credential(User, Password));
            IsBusy = false;
            return t;
        }
    }
}