/*
PasswordDialog.cs

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

using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using TheXDS.MCART.Dialogs.ViewModel;
using TheXDS.MCART.Security.Password;
using static TheXDS.MCART.Types.Extensions.SecureStringExtensions;

namespace TheXDS.MCART.Dialogs
{
    /// <summary>
    /// Lógica de interacción para PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        private PasswordDialogViewModel Vm => (PasswordDialogViewModel)DataContext;

        private PasswordDialog()
        {
            InitializeComponent();
            DataContext = new PasswordDialogViewModel();

            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName != nameof(PasswordDialogViewModel.GeneratedPassword)) return;
                TxtPassword.Password = Vm.GeneratedPassword;
                TxtConfirm.Password = Vm.GeneratedPassword;
            };
        }

        private static bool InternalGetUserData(PasswordDialogMode mode, string? knownUser, string? knownPassword,
            string? knownHint, IPasswordEvaluator? evaluator, LoginValidator? validator, int? maxTries,
            out UserData userData)
        {
            if (maxTries.HasValue)
            {
                if (validator is null) throw new ArgumentNullException(nameof(validator));
                if (maxTries.Value <= 0) throw new ArgumentOutOfRangeException(nameof(maxTries));
            }

            var d = new PasswordDialog();

            mode |= knownHint is null ? 0 : PasswordDialogMode.Hint;
            mode |= evaluator is null ? 0 : PasswordDialogMode.PwQuality;

            d.Vm.User = knownUser;
            d.TxtPassword.Password = knownPassword;
            d.TxtConfirm.Password = knownPassword;
            d.Vm.Hint = knownHint;
            d.Vm.Evaluator = evaluator;
            d.Vm.Validator = validator;
            d.Vm.MaxTries = maxTries;
            d.Vm.Mode = mode;

            var r = d.ShowDialog() ?? false;

            userData = r
                ? new UserData(d.Vm.User ?? string.Empty, d.Vm.Password, d.Vm.Hint, d.Vm.PasswordQuality)
                : UserData.Null;

            return r;
        }

        private async void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (Vm.Validator is not null)
            {
                if (Vm.TriesCount >= Vm.MaxTries)
                {
                    DialogResult = false;
                    Close();
                    return;
                }

                try
                {
                    if (!await Vm.ValidateAsync() ?? true) return;
                }
                catch
                {
#if PreferExceptions
                    throw;
#else
                    DialogResult = false;
                    Close();
                    return;
#endif
                }
            }

            DialogResult = true;
            Close();
        }

        private void TxtConfirm_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Vm.Confirm = ((PasswordBox)sender).SecurePassword;
        }

        private void TxtPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Vm.Password = ((PasswordBox)sender).SecurePassword;
        }

#region GetUserData

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(out UserData userData)
        {
            return GetUserData(null, null, null, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownPassword, out UserData userData)
        {
            return GetUserData(null, knownPassword, null, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownUser, string knownPassword, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, null, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownUser, string knownPassword, string knownHint, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, knownHint, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownUser, string knownPassword, IPasswordEvaluator evaluator,
            out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, null, evaluator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string? knownUser, string? knownPassword, string? knownHint,
            IPasswordEvaluator? evaluator, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, knownHint, evaluator, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownUser, string knownPassword, IPasswordEvaluator evaluator,
            LoginValidator validator, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, null, evaluator, validator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string? knownUser, string? knownPassword, string? knownHint,
            IPasswordEvaluator? evaluator, LoginValidator? validator, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, knownHint, evaluator, validator, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string knownUser, string knownPassword, LoginValidator validator, int? maxTries,
            out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, null, null, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string? knownUser, string? knownPassword, string? knownHint,
            LoginValidator? validator, int? maxTries, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, knownHint, null, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string? knownUser, string? knownPassword, IPasswordEvaluator? evaluator,
            LoginValidator? validator, int? maxTries, out UserData userData)
        {
            return GetUserData(knownUser, knownPassword, null, evaluator, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownUser">Nombre de usuario conocido.</param>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <param name="generator">
        /// Si se establece en <see langword="true"/>, se incluye la
        /// funcionalidad del generador de contraseñas.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(string? knownUser, string? knownPassword, string? knownHint,
            IPasswordEvaluator? evaluator, LoginValidator? validator, int? maxTries, out UserData userData, bool generator = false)
        {
            return InternalGetUserData(PasswordDialogMode.User | PasswordDialogMode.Confirm | (generator ? PasswordDialogMode.Generator : 0), knownUser, knownPassword, knownHint, evaluator,
                validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, out UserData userData)
        {
            return GetUserData(credential, null, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, string knownHint, out UserData userData)
        {
            return GetUserData(credential, knownHint, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, IPasswordEvaluator evaluator, out UserData userData)
        {
            return GetUserData(credential, null, evaluator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, string? knownHint, IPasswordEvaluator? evaluator,
            out UserData userData)
        {
            return GetUserData(credential, knownHint, evaluator, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, IPasswordEvaluator evaluator, LoginValidator validator,
            out UserData userData)
        {
            return GetUserData(credential, null, evaluator, validator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, string? knownHint, IPasswordEvaluator? evaluator,
            LoginValidator? validator, out UserData userData)
        {
            return GetUserData(credential, knownHint, evaluator, validator, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, LoginValidator validator, int? maxTries,
            out UserData userData)
        {
            return GetUserData(credential, null, null, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, string knownHint, LoginValidator validator,
            int? maxTries, out UserData userData)
        {
            return GetUserData(credential, knownHint, null, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, IPasswordEvaluator evaluator, LoginValidator validator,
            int? maxTries, out UserData userData)
        {
            return GetUserData(credential, null, evaluator, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="credential">Credenciales conocidas.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool GetUserData(ICredential credential, string? knownHint, IPasswordEvaluator? evaluator,
            LoginValidator? validator, int? maxTries, out UserData userData)
        {
            return InternalGetUserData(PasswordDialogMode.User | PasswordDialogMode.Confirm, credential.Username, credential.Password.Read(),
                knownHint, evaluator,
                validator, maxTries, out userData);
        }

#endregion

#region ConfirmPassword

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(out UserData userData)
        {
            return ConfirmPassword(null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, out UserData userData)
        {
            return ConfirmPassword(knownPassword, null, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, string? knownHint, out UserData userData)
        {
            return ConfirmPassword(knownPassword, knownHint, null, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, IPasswordEvaluator? evaluator, out UserData userData)
        {
            return ConfirmPassword(knownPassword, null, evaluator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, string? knownHint, IPasswordEvaluator? evaluator,
            out UserData userData)
        {
            return ConfirmPassword(knownPassword, knownHint, evaluator, null, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, IPasswordEvaluator? evaluator, LoginValidator validator,
            out UserData userData)
        {
            return ConfirmPassword(knownPassword, null, evaluator, validator, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, string? knownHint, IPasswordEvaluator? evaluator,
            LoginValidator? validator, out UserData userData)
        {
            return ConfirmPassword(knownPassword, knownHint, evaluator, validator, null, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, IPasswordEvaluator? evaluator, LoginValidator validator,
            int? maxTries, out UserData userData)
        {
            return ConfirmPassword(knownPassword, null, evaluator, validator, maxTries, out userData);
        }

        /// <summary>
        /// Obtiene la información de registro de un usuario.
        /// </summary>
        /// <param name="knownPassword">Contraseña conocida.</param>
        /// <param name="knownHint">Indicio de contraseña conocido.</param>
        /// <param name="evaluator">Evaluador de contraseña a utilizar.</param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos.</param>
        /// <param name="userData"></param>
        /// <returns>
        /// <see langword="true" /> si se ha obtenido la información del
        /// usuario correctamente, <see langword="false" /> en caso
        /// contrario.
        /// </returns>
        public static bool ConfirmPassword(string? knownPassword, string? knownHint, IPasswordEvaluator? evaluator,
            LoginValidator? validator, int? maxTries, out UserData userData)
        {
            return InternalGetUserData(PasswordDialogMode.Confirm, null, knownPassword, knownHint, evaluator, validator,
                maxTries, out userData);
        }

#endregion

#region Login

        /// <summary>
        /// Obtiene una credencial de inicio de sesión.
        /// </summary>
        /// <param name="loginData">
        /// Parámetro de salida. Incluye la credencial de inicio de sesión
        /// introducida en el cuadro de diálogo.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el usuario ha escrito sus
        /// credenciales y pulsado 'continuar',
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Login(out ICredential loginData)
        {
            return Login(null, out loginData);
        }

        /// <summary>
        /// Obtiene una credencial de inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Usuario conocido.
        /// </param>
        /// <param name="loginData">
        /// Parámetro de salida. Incluye la credencial de inicio de sesión
        /// introducida en el cuadro de diálogo.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el usuario ha escrito sus
        /// credenciales y pulsado 'continuar',
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Login(string? knownUser, out ICredential loginData)
        {
            var r = InternalGetUserData(PasswordDialogMode.User, knownUser, null, null, null, null, null, out var l);
            loginData = r ? Credential.From(l) : Credential.Null;
            return r;
        }

        /// <summary>
        /// Realiza un inicio de sesión.
        /// </summary>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si las credenciales son válidas,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Login(LoginValidator validator)
        {
            return Login(validator, null);
        }

        /// <summary>
        /// Realiza un inicio de sesión.
        /// </summary>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos válidos.</param>
        /// <returns>
        /// <see langword="true" /> si las credenciales son válidas,
        /// <see langword="false" /> en caso contrario o si se excede la
        /// cantidad máxima de intentos.
        /// </returns>
        public static bool Login(LoginValidator validator, int? maxTries)
        {
            return Login(null, validator, maxTries);
        }

        /// <summary>
        /// Realiza un inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Usuario conocido.
        /// </param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si las credenciales son válidas,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool Login(string knownUser, LoginValidator validator)
        {
            return Login(knownUser, validator, null);
        }

        /// <summary>
        /// Realiza un inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Usuario conocido.
        /// </param>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos válidos.</param>
        /// <returns>
        /// <see langword="true" /> si las credenciales son válidas,
        /// <see langword="false" /> en caso contrario o si se excede la
        /// cantidad máxima de intentos.
        /// </returns>
        public static bool Login(string? knownUser, LoginValidator validator, int? maxTries)
        {
            return InternalGetUserData(PasswordDialogMode.User, knownUser, null, null, null, validator, maxTries,
                out _);
        }

#endregion

#region CheckPassword

        /// <summary>
        /// Permite verificar una contraseña.
        /// </summary>
        /// <param name="password">
        /// Contraseña introducida por el usuario.
        /// </param>
        /// <returns>
        /// <see langword="true" /> si el usuario ha escrito su contraseña y
        /// pulsado 'continuar', <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool CheckPassword(out SecureString? password)
        {
            var r = InternalGetUserData(PasswordDialogMode.PasswordOnly, null, null, null, null, null, null, out var l);
            password = r ? l.Password : null;
            return r;
        }

        /// <summary>
        /// Permite verificar una contraseña.
        /// </summary>
        /// <param name="validator">Función de validación de contraseña.</param>
        /// <returns>
        /// <see langword="true" /> si la contraseña es válida,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        public static bool CheckPassword(LoginValidator validator)
        {
            return CheckPassword(validator, null);
        }

        /// <summary>
        /// Permite verificar una contraseña.
        /// </summary>
        /// <param name="validator">
        /// Función de validación de contraseña.
        /// </param>
        /// <param name="maxTries">Número máximo de intentos válidos.</param>
        /// <returns>
        /// <see langword="true" /> si la contraseña es válida,
        /// <see langword="false" /> en caso contrario o si se excede la
        /// cantidad máxima de intentos.
        /// </returns>
        public static bool CheckPassword(LoginValidator validator, int? maxTries)
        {
            return InternalGetUserData(PasswordDialogMode.User, null, null, null, null, validator, maxTries, out _);
        }

#endregion

        private void BtnReGen_OnClick(object sender, RoutedEventArgs e)
        {
            Vm.OnGenerate();
        }

        private void BtnToClipboard_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Vm.GeneratedPassword);
        }

        private void BtnOkGen_OnClick(object sender, RoutedEventArgs e)
        {
            BtnGenerate.IsChecked = false;
        }

        private void BtnCancelGen_OnClick(object sender, RoutedEventArgs e)
        {
            TxtPassword.Clear();
            TxtConfirm.Clear();
            BtnGenerate.IsChecked = false;
        }
    }
}