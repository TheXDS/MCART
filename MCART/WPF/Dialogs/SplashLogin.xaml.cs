/*
SplashLogin.xaml.cs

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
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Security.Password;
using St = TheXDS.MCART.Resources.Strings;
using St2 = TheXDS.MCART.Resources.InternalStrings;

namespace TheXDS.MCART.Dialogs
{
    /// <summary>
    /// Diálogo de inicio de sesión de diseño visualmente atractivo.
    /// </summary>
    /// <remarks>
    /// Este diálogo es una alternativa visualmente atractiva a 
    /// <see cref="PasswordDialog"/>. Para una mayor funcionalidad, utilice
    /// <see cref="PasswordDialog"/>
    /// </remarks>
    public partial class SplashLogin : Window
    {
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="BackgroundImage"/>.
        /// </summary>
        public static DependencyProperty BackgroundImageProperty = DependencyProperty.Register(
                nameof(BackgroundImage), typeof(BitmapSource), typeof(SplashLogin),
                new PropertyMetadata(null));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="CorpColor"/>.
        /// </summary>
        public static DependencyProperty CorpColorProperty = DependencyProperty.Register(
                nameof(CorpColor), typeof(Brush), typeof(SplashLogin),
                new PropertyMetadata(SystemColors.HighlightBrush));

        /* -= NOTA =-
         * No tengo idea por qué los desarrolladores de WPF decidieron que
         * cuando ShowDialog() retorna, si el valor de DialogResult es null,
         * automáticamente se establece en false. Hay que realizar un
         * workaround para poder devolver null.
         * 
         * El siguiente valor es una bandera para realizar el workaround para
         * poder devolver null al cancelar un inicio de sesión.
         */
        bool forceNull = false;
        private LoginValidator loginValidator;

        /// <summary>
        /// Obtiene o establece la imagen de fondo a utilizar por este
        /// <see cref="SplashLogin"/>.
        /// </summary>
        public BitmapSource BackgroundImage
        {
            get => (BitmapSource)GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el color corporativo a utilizar para el tema.
        /// </summary>
        public int CorpColor
        {
            get => (int)GetValue(CorpColorProperty);
            set => SetValue(CorpColorProperty, value);
        }
        /// <summary>
        /// Permite establecer un contenido personalizado en la barra lateral
        /// de este <see cref="SplashLogin"/>.
        /// </summary>
        public UIElementCollection SideBarContent => grdSide.Children;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="SplashLogin"/>.
        /// </summary>
        public SplashLogin()
        {
            InitializeComponent();
            btnGo.Click += BtnGo_Click;
            btnClose.Click += (sender, e) => Close();
            txtUsr.GotFocus += TxtFocus;
            txtUsr.TextChanged += WarnClear;
            txtPw.GotFocus += TxtFocus;
            txtPw.PasswordChanged += WarnClear;
            Closing += (sender, e) =>
            {
                /* -= NOTA =-
                 * No tengo idea por qué los desarrolladores de WPF decidieron
                 * que cuando ShowDialog() retorna, si el valor de DialogResult
                 * es null, automáticamente se establece en false. Hay que
                 * realizar un workaround para poder devolver null.
                 */
                forceNull = DialogResult is null;
            };
        }

        /// <summary>
        /// Realiza una acción de inicio de sesión.
        /// </summary>
        /// <param name="loginValidator">
        /// Callback de validación de inicio de sesión. Debe tratarse de una
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> asíncrona que devuelve
        /// un <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si se ha iniciado sesión correctamente, <see langword="false"/> en
        /// caso contrario.
        /// </returns>
        public bool? Login(LoginValidator loginValidator) => Login(string.Empty, string.Empty, loginValidator);
        /// <summary>
        /// Realiza una acción de inicio de sesión, estableciendo valores
        /// predeterminados para los cuadros de texto de entrada.
        /// </summary>
        /// <param name="defaultUser">Nombre conocido del usuario.</param>
        /// <param name="plainPassword">Contraseña conocida.</param>
        /// <param name="loginValidator">
        /// Callback de validación de inicio de sesión. Debe tratarse de una
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> asíncrona que devuelve
        /// un <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si se ha iniciado sesión correctamente, <see langword="false"/> en
        /// caso contrario.
        /// </returns>
        public bool? Login(string defaultUser, string plainPassword, LoginValidator loginValidator)
        {
            this.loginValidator = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
            txtUsr.Text = defaultUser;
            txtPw.Password = plainPassword;
            txtUsr.Focus();
            var r = base.ShowDialog();
            /* -= NOTA =-
             * No tengo idea por qué los desarrolladores de WPF decidieron que
             * cuando ShowDialog() retorna, si el valor de DialogResult es
             * null, automáticamente se establece en false. Hay que realizar un
             * workaround para poder devolver null.
             */
            return forceNull ? null : r;
        }

        private void WarnClear(object sender, RoutedEventArgs e)
        {
            txtPw.ClearWarn();
            loginWarning.Text = string.Empty;
        }
        private void TxtFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
            (sender as PasswordBox)?.SelectAll();
        }
        private async void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            contentRoot.IsEnabled = false;
            try
            {
                if (await loginValidator(txtUsr.Text, txtPw.SecurePassword))
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    txtPw.Warn(St.InvalidPassword);
                    loginWarning.Text = St.InvalidPassword;
                    txtPw.Focus();
                }
            }
            catch (Exception ex)
            {
                loginWarning.Text = ex.Message;
            }
            finally
            {
                contentRoot.IsEnabled = true;
            }
        }
        
        /// <summary>
        /// No utilizar.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// Este método siempre genera esta excepción.
        /// </exception>
        [Obsolete(St2.ObsoleteCheckDoc,true)] public new void Show() => throw new NotSupportedException(St2.ObsoleteCheckDoc);
        /// <summary>
        /// No utilizar.
        /// </summary>
        /// <returns>
        /// Este método siempre genera una <see cref="NotSupportedException"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Este método siempre genera esta excepción.
        /// </exception>
        [Obsolete(St2.ObsoleteCheckDoc, true)] public new bool? ShowDialog() => throw new NotSupportedException(St2.ObsoleteCheckDoc);
    }
}