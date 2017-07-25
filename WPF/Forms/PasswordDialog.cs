//
//  PasswordDialog.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using St = MCART.Resources.Strings;
using static MCART.Types.Color;
namespace MCART.Forms
{
    /// <summary>
    /// Esta clase contiene una ventana de Windows Presentation Framework que
    /// permite al usuario validar contraseñas, así como también establecerlas
    /// y medir el nivel de seguridad de la contraseña escogida.
    /// </summary>
    public class PasswordDialog : Window
    {
        #region Controles
        private Button Btngo = new Button
        {
            Width = 23,
            IsDefault = true,
            Content = "➜"
        };
        private TextBox TxtUsr = new TextBox
        {
            Height = 23,
            Margin = new Thickness(10, 10, 10, 0)
        };
        private PasswordBox Txtpw = new PasswordBox
        {
            Height = 23,
            PasswordChar = '●'
        };
        private StackPanel MoreCtrls = new StackPanel();
        private PasswordBox Txtcnf = new PasswordBox
        {
            Margin = new Thickness(10, 0, 10, 0),
            Height = 23,
            PasswordChar = '●'
        };
        private StackPanel HintBlock = new StackPanel();
        private TextBox Txthint = new TextBox
        {
            Height = 23,
            Margin = new Thickness(10, 0, 10, 0)
        };
        private StackPanel SecurIndicator = new StackPanel();
        private ProgressRing Ringsec = new ProgressRing
        {
            Margin = new Thickness(10),
            TextFormat = "{0:f0}%"
        };
        private TextBlock Securdetails = new TextBlock
        {
            Margin = new Thickness(0, 10, 10, 10),
            VerticalAlignment = VerticalAlignment.Center,
            TextWrapping = TextWrapping.Wrap
        };
        private Button BtnConfirm = new Button
        {
            Width = 75,
            Content = St.OK,
            Margin = new Thickness(10)
        };
        #endregion
        #region Miembros privados
        private PwDialogResult r;
        private PwEvaluator pwe;
        private PwEvalResult Pwr;
        #endregion
        #region Eventos de controles
        private void Btngo_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsr.IsVisible && TxtUsr.Text == string.Empty)
            {
                if (!TxtUsr.IsWarned())
                    TxtUsr.Warn(St.MandatoryField);
                TxtUsr.Focus();
                return;
            }
            r = new PwDialogResult(TxtUsr.Text, Txtpw.Password, null, MessageBoxResult.OK, PwEvalResult.Null);
            Close();
        }
        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
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
            r = new PwDialogResult(TxtUsr.Text, Txtpw.Password, Txthint.Text, MessageBoxResult.OK, Pwr);
            Close();
        }
        private void Txtpw_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (SecurIndicator.IsVisible)
            {
                if (Txtcnf.IsWarned())
                    Txtcnf.ClearWarn();
                Pwr = pwe.Evaluate(Txtpw.Password);
                Securdetails.Text = Pwr.Details;
                Ringsec.Value = Pwr.Result * 100;
                if (!Pwr.Critical)
                {
                    Ringsec.Fill = new SolidColorBrush(BlendHealth(Pwr.Result));
                }
                else
                {
                    Ringsec.Fill = SystemColors.HighlightBrush;
                }
            }
        }
        private void Txtcnf_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((Control)sender).IsWarned()) ((Control)sender).ClearWarn();
        }
        private void Txtcnf_check(object sender, RoutedEventArgs e)
        {
            if (Txtcnf.Password != string.Empty && Txtcnf.Password != Txtpw.Password)
                Txtcnf.Warn(St.PwNotMatch);
        }
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        public PasswordDialog()
        {
            TextBlock a = default(TextBlock);
            StackPanel StckRoot = new StackPanel();
            DockPanel c = new DockPanel { Margin = new Thickness(10) };
            SizeToContent = SizeToContent.Height;
            ResizeMode = ResizeMode.NoResize;
            Width = 250;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            a = new TextBlock
            {
                Text = St.Usr,
                Margin = new Thickness(10, 10, 10, 0)
            };
            a.SetBinding(VisibilityProperty, new Binding(nameof(TxtUsr.Visibility)) { Source = TxtUsr });
            StckRoot.Children.Add(a);
            StckRoot.Children.Add(TxtUsr);
            a = new TextBlock
            {
                Text = St.Pwd,
                Margin = new Thickness(10, 10, 10, 0)
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
                Margin = new Thickness(10)
            });
            MoreCtrls.Children.Add(Txtcnf);
            HintBlock.Children.Add(new TextBlock
            {
                Text = St.PwdHint,
                Margin = new Thickness(10)
            });
            HintBlock.Children.Add(Txthint);
            MoreCtrls.Children.Add(HintBlock);
            SecurIndicator.Children.Add(new TextBlock
            {
                Text = St.Security,
                Margin = new Thickness(10, 10, 10, 0)
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
            TxtUsr.TextChanged += Txtcnf_PasswordChanged;
            Txtpw.PasswordChanged += Txtpw_PasswordChanged;
            Txtpw.LostFocus += Txtcnf_check;
            Txtcnf.PasswordChanged += Txtcnf_PasswordChanged;
            Txtcnf.LostFocus += Txtcnf_check;
            BtnConfirm.Click += BtnConfirm_Click;
        }
        /// <summary>
        /// Obtiene una contraseña.
        /// </summary>
        /// <param name="StoredPw">
        /// Parámetro opcional. Proporciona una contraseña predeterminada para
        /// esta ventana.
        /// </param>
        /// <param name="DefaultUsr">
        /// Parámetro opcional. Nombre de usuario a mostrar de manera 
        /// predeterminada en el cuadro.
        /// </param>
        /// <param name="ShowUsrBox">
        /// Parámetro opcional. Indica si se mostrará o no el cuadro de 
        /// usuario.
        /// </param>
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de esta función.
        /// </returns>
        public PwDialogResult GetPassword(string DefaultUsr = null, string StoredPw = null, bool ShowUsrBox = false)
        {
            Title = St.Pwd;
            TxtUsr.Visibility = ShowUsrBox ? Visibility.Visible : Visibility.Collapsed;
            TxtUsr.Text = DefaultUsr;
            Txtpw.Password = StoredPw;
            Btngo.Visibility = Visibility.Visible;
            MoreCtrls.Visibility = Visibility.Collapsed;
            ShowDialog();
            return r ?? PwDialogResult.Null;
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
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public PwDialogResult ChoosePassword(PwMode Mode = PwMode.JustConfirm, PwEvaluator PwEvaluatorObj = null)
        {
            Title = string.Format(St.SetX, St.Pwd.ToLower());
            TxtUsr.Visibility = (Mode & PwMode.Usr) == PwMode.Usr ? Visibility.Visible : Visibility.Collapsed;
            Btngo.Visibility = Visibility.Collapsed;
            MoreCtrls.Visibility = Visibility.Visible;
            HintBlock.Visibility = (Mode & PwMode.Hint) == PwMode.Hint ? Visibility.Visible : Visibility.Collapsed;
            if ((Mode & PwMode.Secur) == PwMode.Secur)
            {
                SecurIndicator.Visibility = Visibility.Visible;
                if (PwEvaluatorObj == null)
                {
                    pwe = new PwEvaluator(RuleSets.CommonComplexityRuleSet());
                    pwe.Rules.Add(RuleSets.PwLatinEvalRule());
                    pwe.Rules.Add(RuleSets.PwOtherSymbsEvalRule());
                    pwe.Rules.Add(RuleSets.PwOtherUTFEvalRule());
                }
                else pwe = PwEvaluatorObj;
            }
            else SecurIndicator.Visibility = Visibility.Collapsed;
            ShowDialog();
            return r ?? PwDialogResult.Null;
        } 
        #endregion
    }
}