//
//  PasswordDialog.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using TheXDS.MCART.Controls;
using TheXDS.MCART.Security.Password;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static TheXDS.MCART.Types.Color;
using St = TheXDS.MCART.Resources.Strings;
using System;

namespace TheXDS.MCART.Dialogs
{
    /// <summary>
    /// Esta clase contiene una ventana de Windows Presentation Framework que
    /// permite al usuario validar contraseñas, así como también establecerlas
    /// y medir el nivel de seguridad de la contraseña escogida.
    /// </summary>
    public sealed partial class PasswordDialog : Window
    {
        StackPanel StckRoot = new StackPanel();
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

        #region Controles
        Button Btngo = new Button
        {
            Width = 23,
            IsDefault = true,
            Content = "➜"
        };
        TextBox TxtUsr = new TextBox
        {
            Height = 23,
            TabIndex = 1,
            Margin = new Thickness(10, 10, 10, 0)
        };
        PasswordBox Txtpw = new PasswordBox
        {
            Height = 23,
            TabIndex = 2,
            PasswordChar = '●'
        };
        StackPanel MoreCtrls = new StackPanel();
        PasswordBox Txtcnf = new PasswordBox
        {
            Margin = new Thickness(10, 0, 10, 0),
            Height = 23,
            PasswordChar = '●'
        };
        StackPanel HintBlock = new StackPanel();
        TextBox Txthint = new TextBox
        {
            Height = 23,
            Margin = new Thickness(10, 0, 10, 0)
        };
        StackPanel SecurIndicator = new StackPanel();
        ProgressRing Ringsec = new ProgressRing
        {
            Margin = new Thickness(10),
            Height = 64,
            TextSize = 24,
            Thickness = 8,
            TextFormat = "{0:f0}%"
        };
        TextBlock Securdetails = new TextBlock
        {
            Margin = new Thickness(0, 10, 10, 10),
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.Wrap
        };
        Button BtnConfirm = new Button
        {
            Width = 75,
            Content = St.OK,
            Margin = new Thickness(10)
        };
        #endregion

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        private PasswordDialog()
        {
            Thickness tck = new Thickness(10);
            Thickness tck2 = new Thickness(10, 10, 10, 0);
            TextBlock a;
            DockPanel c = new DockPanel { Margin = tck };
            SizeToContent = SizeToContent.Height;
            ResizeMode = ResizeMode.NoResize;
            Width = 250;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowStyle = WindowStyle.ToolWindow;
            a = new TextBlock
            {
                Text = St.Usr,
                Margin = tck2
            };
            a.SetBinding(VisibilityProperty, new Binding(nameof(TxtUsr.Visibility)) { Source = TxtUsr });
            StckRoot.Children.Add(a);
            StckRoot.Children.Add(TxtUsr);
            a = new TextBlock
            {
                Text = St.Pwd,
                Margin = tck2
            };
            a.SetBinding(VisibilityProperty, new Binding(nameof(MoreCtrls.Visibility)) { Source = MoreCtrls });
            StckRoot.Children.Add(a);
            DockPanel.SetDock(Btngo, Dock.Right);
            c.Children.Add(Btngo);
            c.Children.Add(Txtpw);
            StckRoot.Children.Add(c);
            MoreCtrls.Children.Add(new TextBlock
            {
                Text = St.PwdConfirm,
                Margin = tck
            });
            MoreCtrls.Children.Add(Txtcnf);
            HintBlock.Children.Add(new TextBlock
            {
                Text = St.PwdHint,
                Margin = tck
            });
            HintBlock.Children.Add(Txthint);
            MoreCtrls.Children.Add(HintBlock);
            SecurIndicator.Children.Add(new TextBlock
            {
                Text = St.Security,
                Margin = tck2
            });
            c = new DockPanel();
            c.Children.Add(Ringsec);
            c.Children.Add(Securdetails);
            SecurIndicator.Children.Add(c);
            MoreCtrls.Children.Add(SecurIndicator);
            MoreCtrls.Children.Add(BtnConfirm);
            StckRoot.Children.Add(MoreCtrls);
            Content = StckRoot;
            Btngo.Click += Btngo_Click;
            TxtUsr.TextChanged += WarnClearing;
            TxtUsr.GotFocus += TxtFocus;
            Txtpw.PasswordChanged += PasswordChanged;
            Txtpw.PasswordChanged += WarnClearing;
            Txtpw.GotFocus += TxtFocus;
            Txtpw.LostFocus += Txtcnf_check;
            Txtcnf.PasswordChanged += WarnClearing;
            Txtcnf.GotFocus += TxtFocus;
            Txtcnf.LostFocus += Txtcnf_check;
            BtnConfirm.Click += BtnConfirm_Click;
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
        #region Métodos públicos
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="knownPassword">
        /// Proporciona una contraseña predeterminada para esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de la contraseña. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="maxTries">
        /// Cantidad máxima de intentos. El inicio de sesión se cancela si el
        /// usuario excede esta cantidad de intentos inválidos.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si la contraseña es válida,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? GetPassword(string knownPassword, LoginValidator loginValidator, int maxTries)
        {
            using (var dialog = new PasswordDialog())
            {
                dialog.loginVal = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
                if (maxTries < 1) throw new ArgumentOutOfRangeException(nameof(maxTries));
                dialog.maxTries = maxTries;
                dialog.TxtUsr.Visibility = Visibility.Collapsed;
                dialog.Btngo.Visibility = Visibility.Visible;
                dialog.MoreCtrls.Visibility = Visibility.Collapsed;
                dialog.TxtUsr.Text = string.Empty;
                dialog.Txtpw.Password = knownPassword;
                dialog.Txtpw.Focus();
                dialog.ShowDialog();
                return dialog.DialogResult;
            }
        }
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="knownUser">
        /// Nombre de usuario a mostrar de manera predeterminada en el cuadro.
        /// </param>
        /// <param name="knownPassword">
        /// Proporciona una contraseña predeterminada para esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Delegado de validación de inicio de sesión. Debe tratarse de un
        /// <see cref="System.Threading.Tasks.Task{TResult}"/> que devuelve un
        /// <see cref="bool"/>, <see langword="true"/> si el inicio de sesión
        /// fue exitoso, <see langword="false"/> en caso contrario.
        /// </param>
        /// <param name="maxTries">
        /// Cantidad máxima de intentos. El inicio de sesión se cancela si el
        /// usuario excede esta cantidad de intentos inválidos.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el inicio de sesión fue exitoso,
        /// <see langword="false"/> en caso contrario o al exceder la cantidad
        /// máxima de intentos, o <see langword="null"/> si el inicio de sesión
        /// fue cancelado.
        /// </returns>
        public static bool? Login(string knownUser, string knownPassword, LoginValidator loginValidator, int maxTries)
        {
            using (var dialog = new PasswordDialog())
            {
                dialog.loginVal = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
                if (maxTries < 1) throw new ArgumentOutOfRangeException(nameof(maxTries));
                dialog.maxTries = maxTries;
                dialog.TxtUsr.Visibility = Visibility.Visible;
                dialog.Btngo.Visibility = Visibility.Visible;
                dialog.MoreCtrls.Visibility = Visibility.Collapsed;
                dialog.TxtUsr.Text = knownUser;
                dialog.Txtpw.Password = knownPassword;
                if (knownUser.IsEmpty()) dialog.TxtUsr.Focus(); else dialog.Txtpw.Focus();
                var r = dialog.ShowDialog();
                /* -= NOTA =-
                 * No tengo idea por qué los desarrolladores de WPF decidieron
                 * que cuando ShowDialog() retorna, si el valor de DialogResult
                 * es null, automáticamente se establece en false. Hay que
                 * realizar un workaround para poder devolver null.
                 */
                return dialog.forceNull ? null : r;
            }
        }
        /// <summary>
        /// Permite al usuario escoger una contraseña.
        /// </summary>
        /// <param name="mode">
        /// Parámetro opcional. Establece las opciones disponibles para esta 
        /// ventana. De forma predeterminada, únicamente se mostrará un cuadro
        /// para confirmar la contraseña.
        /// </param>
        /// <param name="pwEvaluator">
        /// Parámetro opcional. Objeto evaluador de contraseñas a utilizar. Si
        /// se omite, se utilizará un evaluador con un conjunto de reglas 
        /// predeterminado. Se ignora si <paramref name="mode"/> no incluye la 
        /// bandera <see cref="PwMode.Secur"/>.
        /// </param>
        /// <param name="passValue">
        /// Parámetro opcional. Determina la puntuación mínima requerida para 
        /// aceptar una contraseña. De forma predeterminada, se establece en
        /// un puntaje de al menos 50%.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public static PwDialogResult ChoosePassword(PwMode mode, PwEvaluator pwEvaluator, int passValue)
        {
            using (var dialog = new PasswordDialog())
            {
                dialog.Title = St.SetX(St.Pwd.ToLower());
                dialog.TxtUsr.Visibility = (mode & PwMode.Usr) == PwMode.Usr ? Visibility.Visible : Visibility.Collapsed;
                dialog.Btngo.Visibility = Visibility.Collapsed;
                dialog.MoreCtrls.Visibility = Visibility.Visible;
                dialog.HintBlock.Visibility = (mode & PwMode.Hint) == PwMode.Hint ? Visibility.Visible : Visibility.Collapsed;
                if ((mode & PwMode.Secur) == PwMode.Secur)
                {
                    dialog.SecurIndicator.Visibility = Visibility.Visible;
                    if (pwEvaluator == null)
                    {
                        dialog.pwEvaluator = new PwEvaluator(RuleSets.CommonComplexityRuleSet());
#if SaferPasswords
                        dialog.pwEvaluator.Rules.Add(RuleSets.PwLatinEvalRule());
                        dialog.pwEvaluator.Rules.Add(RuleSets.PwOtherSymbsEvalRule());
                        dialog.pwEvaluator.Rules.Add(RuleSets.PwOtherUTFEvalRule());
#endif
                    }
                    else dialog.pwEvaluator = pwEvaluator;
                }
                else dialog.SecurIndicator.Visibility = Visibility.Collapsed;
                dialog.passVal = passValue;
                dialog.ShowDialog();
                return dialog.retVal;
            }
        }
        #endregion
        #region Botones
        async void Btngo_Click(object sender, RoutedEventArgs e)
        {
            if (++tries > maxTries)
            {
                DialogResult = false;
                Close();
            }
            if (!Prechecks()) return;
            StckRoot.IsEnabled = false;
            if (await loginVal(TxtUsr.Text, Txtpw.SecurePassword))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                StckRoot.IsEnabled = true;
                Txtpw.Warn(St.InvalidPassword);
                Txtpw.Focus();
            }
        }
        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!Prechecks()) return;
            retVal = new PwDialogResult(TxtUsr.Text, Txtpw.SecurePassword, Txthint.Text, MessageBoxResult.OK, pwEvalResult);
            Close();
        }
        #endregion
        #region Eventos de controles
        void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SecurIndicator.IsVisible)
            {
                if (Txtcnf.IsWarned()) Txtcnf.ClearWarn();
                pwEvalResult = pwEvaluator.Evaluate(Txtpw.SecurePassword);
                Securdetails.Text = pwEvalResult.Details;
                Ringsec.Value = pwEvalResult.Result * 100;
                if (!pwEvalResult.Critical)
                    Ringsec.Fill = new SolidColorBrush(BlendHealth(pwEvalResult.Result));
                else
                    Ringsec.Fill = SystemColors.HighlightBrush;
            }
        }
        void WarnClearing(object sender, RoutedEventArgs e)
        {
            if (((Control)sender).IsWarned()) ((Control)sender).ClearWarn();
        }
        void Txtcnf_check(object sender, RoutedEventArgs e)
        {
            if (Txtcnf.Password != string.Empty && Txtcnf.Password != Txtpw.Password)
                Txtcnf.Warn(St.PwNotMatch);
        }
        void TxtFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
            (sender as PasswordBox)?.SelectAll();
        }
        #endregion
        bool Prechecks()
        {
            /* -= Orden de verificaciones=-
             * Las advertencias de procesan es este orden:
             * 1) Orden descendente de controles.
             * 2) problemas menores primero, mayores al final.
             */

            bool retval = true;
            if (TxtUsr.IsVisible && TxtUsr.Text == string.Empty)
            {
                TxtUsr.Warn(St.MandatoryField);
                if (retval) TxtUsr.Focus();
                retval = false;
            }
            if (SecurIndicator.IsVisible && Ringsec.Value < passVal)
            {
                Txtcnf.Warn(St.PwNtbmc);
                Txtcnf.Password = string.Empty;
                if (retval) Txtpw.Focus();
                retval = false;
            }
            if (Txtpw.Password == string.Empty)
            {
                Txtpw.Warn(St.MandatoryField);
                if (retval) Txtpw.Focus();
                retval = false;
            }
            if (Txtcnf.IsVisible && Txtpw.Password != Txtcnf.Password)
            {
                Txtcnf.Warn(St.PwNotMatch);
                Txtcnf.Password = string.Empty;
                if (retval) Txtcnf.Focus();
                retval = false;
            }
            return retval;
        }
    }
}