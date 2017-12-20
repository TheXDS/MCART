//
//  PasswordDialog.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MCART.Controls;
using MCART.Security.Password;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using static MCART.Types.Color;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Esta clase contiene una ventana de Windows Presentation Framework que
    /// permite al usuario validar contraseñas, así como también establecerlas
    /// y medir el nivel de seguridad de la contraseña escogida.
    /// </summary>
    public partial class PasswordDialog : Window
    {
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
        #region Eventos de controles
        void Btngo_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsr.IsVisible && TxtUsr.Text == string.Empty)
            {
                if (!TxtUsr.IsWarned())
                    TxtUsr.Warn(St.MandatoryField);
                TxtUsr.Focus();
                return;
            }
            if (loginVal?.Invoke(TxtUsr.Text, Txtpw.SecurePassword) ?? true)
            {
                retVal = new PwDialogResult(TxtUsr.Text, Txtpw.SecurePassword, null, MessageBoxResult.OK, PwEvalResult.Null);
                Close();
            }
            else
            {
                if (Txtpw.IsWarned())Txtpw.ClearWarn();
                Txtpw.Warn(St.IncorrectPWD);
                Txtpw.SelectAll();
                Txtpw.Focus();
            }
        }
        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsr.IsVisible && TxtUsr.Text == string.Empty)
            {
                if (!TxtUsr.IsWarned())
                    TxtUsr.Warn(St.MandatoryField);
                TxtUsr.Focus();
                return;
            }
            if (Txtpw.Password != Txtcnf.Password)
            {
                MessageBox.Show(St.PwNotMatch, St.Err);
                Txtcnf.Password = string.Empty;
                Txtpw.Focus();
                Txtpw.SelectAll();
                return;
            }
            else if (SecurIndicator.IsVisible && Ringsec.Value < 50)
            {
                MessageBox.Show(St.PwNtbmc, St.Err);
                Txtcnf.Password = string.Empty;
                Txtpw.Focus();
                Txtpw.SelectAll();
                return;
            }
            retVal = new PwDialogResult(TxtUsr.Text, Txtpw.SecurePassword, Txthint.Text, MessageBoxResult.OK, pwEvalResult);
            Close();
        }
        void Txtpw_PasswordChanged(object sender, RoutedEventArgs e)
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
        #region Métodos públicos
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        public PasswordDialog()
        {
            Thickness tck = new Thickness(10);
            Thickness tck2 = new Thickness(10, 10, 10, 0);
            TextBlock a;
            StackPanel StckRoot = new StackPanel();
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
            Txtpw.PasswordChanged += Txtpw_PasswordChanged;
            Txtpw.PasswordChanged += WarnClearing;
            Txtpw.GotFocus += TxtFocus;
            Txtpw.LostFocus += Txtcnf_check;
            Txtcnf.PasswordChanged += WarnClearing;
            Txtcnf.GotFocus += TxtFocus;
            Txtcnf.LostFocus += Txtcnf_check;
            BtnConfirm.Click += BtnConfirm_Click;
        }
        /// <summary>
        /// Obtiene la información de inicio de sesión.
        /// </summary>
        /// <param name="defaultUsr">
        /// Parámetro opcional. Nombre de usuario a mostrar de manera 
        /// predeterminada en el cuadro.
        /// </param>
        /// <param name="storedPw">
        /// Parámetro opcional. Proporciona una contraseña predeterminada para
        /// esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Parámetro opcional. Delegado de validación de la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult Login(string defaultUsr, string storedPw, LoginValidator loginValidator = null)
        {
            loginVal = loginValidator;
            TxtUsr.Visibility = Visibility.Visible;
            TxtUsr.Text = defaultUsr;
            Txtpw.Password = storedPw;
            Btngo.Visibility = Visibility.Visible;
            MoreCtrls.Visibility = Visibility.Collapsed;
            if (defaultUsr.IsEmpty()) TxtUsr.Focus(); else Txtpw.Focus();
            ShowDialog();
            return retVal;
        }
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="storedPw">
        /// Parámetro opcional. Proporciona una contraseña predeterminada para
        /// esta ventana.
        /// </param>
        /// <param name="loginValidator">
        /// Parámetro opcional. Delegado de validación de la contraseña.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult GetPassword(string storedPw, LoginValidator loginValidator = null)
        {
            TxtUsr.Visibility = Visibility.Collapsed;
            TxtUsr.Text = string.Empty;
            Txtpw.Password = storedPw;
            Btngo.Visibility = Visibility.Visible;
            MoreCtrls.Visibility = Visibility.Collapsed;
            Txtpw.Focus();
            ShowDialog();
            return retVal;
        }
        /// <summary>
        /// Permite al usuario escoger una contraseña.
        /// </summary>
        /// <param name="Mode">
        /// Parámetro opcional. Establece las opciones disponibles para esta 
        /// ventana. De forma predeterminada, únicamente se mostrará un cuadro
        /// para confirmar la contraseña.
        /// </param>
        /// <param name="PwEvaluatorObj">
        /// Parámetro opcional. Objeto evaluador de contraseñas a utilizar. Si
        /// se omite, se utilizará un evaluador con un conjunto de reglas 
        /// predeterminado. Se ignora si <paramref name="Mode"/> no incluye la 
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
        public PwDialogResult ChoosePassword(PwMode Mode = PwMode.JustConfirm, PwEvaluator PwEvaluatorObj = null, int passValue = 50)
        {
            Title = St.SetX(St.Pwd.ToLower());
            TxtUsr.Visibility = (Mode & PwMode.Usr) == PwMode.Usr ? Visibility.Visible : Visibility.Collapsed;
            Btngo.Visibility = Visibility.Collapsed;
            MoreCtrls.Visibility = Visibility.Visible;
            HintBlock.Visibility = (Mode & PwMode.Hint) == PwMode.Hint ? Visibility.Visible : Visibility.Collapsed;
            if ((Mode & PwMode.Secur) == PwMode.Secur)
            {
                SecurIndicator.Visibility = Visibility.Visible;
                if (PwEvaluatorObj == null)
                {
                    pwEvaluator = new PwEvaluator(RuleSets.CommonComplexityRuleSet());
#if SaferPasswords
                    pwEvaluator.Rules.Add(RuleSets.PwLatinEvalRule());
                    pwEvaluator.Rules.Add(RuleSets.PwOtherSymbsEvalRule());
                    pwEvaluator.Rules.Add(RuleSets.PwOtherUTFEvalRule());
#endif
                }
                else pwEvaluator = PwEvaluatorObj;
            }
            else SecurIndicator.Visibility = Visibility.Collapsed;
            ShowDialog();
            return retVal;
        }
        #endregion
    }
}