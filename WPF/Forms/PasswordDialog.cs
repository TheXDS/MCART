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
using MCART.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using St = MCART.Resources.Strings;
namespace MCART.Forms
{
    /// <summary>
    /// Esta clase contiene una ventana de Windows Presentation Framework
    /// que permite al usuario validar contraseñas, así como también
    /// establecerlas y medir el nivel de seguridad de la contraseña escogida.
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

        #region Miembros públicos
        /// <summary>
        /// Configura el cuadro de contraseña al utilizar <see cref="ChoosePassword(PwMode, PwEvaluator)"/>
        /// </summary>
        public enum PwMode : byte
        {
            /// <summary>
            /// Únicamente confirmar
            /// </summary>
            JustConfirm,
            /// <summary>
            /// Mostrar cuadro de indicio
            /// </summary>
            Hint,
            /// <summary>
            /// Mostrar indicador de seguridad
            /// </summary>
            Secur,
            /// <summary>
            /// Mostrar cuadro de indicio e indicador de seguridad
            /// </summary>
            Both,
            /// <summary>
            /// Mostrar cuadro de usuario
            /// </summary>
            Usr,
            /// <summary>
            /// Mostrar cuadro de usuario y cuadro de indicio
            /// </summary>
            UsrHint,
            /// <summary>
            /// Mostrar cuadro de usuario e indicador de seguridad
            /// </summary>
            UsrSecur,
            /// <summary>
            /// Mostrar cuadro de usuario, cuadro de indicio e indicador de seguridad
            /// </summary>
            UsrBoth
        }
        /// <summary>
        /// Representa el resultado de un cuadro de diálogo <see cref="PasswordDialog"/>
        /// </summary>
        public class PwDialogResult
        {
            private string u;
            private string p;
            private MessageBoxResult r;
            private string h;
            private PwEvalResult e;
            /// <summary>
            /// Obtiene el usuario introducido en el <see cref="PasswordDialog"/> 
            /// </summary>
            /// <returns>Si se muestra este diálogo con <see cref="PwMode.Usr"/>, se devuelve el usuario introducido en el <see cref="PasswordDialog"/>; de lo contrario se devuelve <see cref="string.Empty"/></returns>
            public string Usr
            {
                get { return u; }
            }
            /// <summary>
            /// Obtiene la contraseña que el usuario ha introducido
            /// </summary>
            /// <returns>Una <see cref="string"/> con la contraseña que el usuario ha introducido</returns>
            public string Pwd
            {
                get { return p; }
            }
            /// <summary>
            /// Obtiene el indicio de contraseña introducido por el usuario
            /// </summary>
            /// <returns><see cref="string.Empty"/> si el cuadro se inicia con <see cref="GetPassword(string, string, Boolean)"/>.
            /// Si se inicia con <see cref="ChoosePassword(PwMode, PwEvaluator)"/>, se devuelve un
            /// <see cref="string"/> con el indicio de contraseña que el usuario ha introducido</returns>
            public string Hint
            {
                get { return h; }
            }
            /// <summary>
            /// Obtiene el resultado del cuadro de diálogo
            /// </summary>
            /// <returns>un <see cref="MessageBoxResult"/> que indica la acción realizada por el usuario</returns>
            public MessageBoxResult Result
            {
                get { return r; }
            }
            /// <summary>
            /// Obtiene el resultado de la evaluación de la contraseña
            /// </summary>
            /// <returns>Si se muestra este diálogo con <see cref="PwMode.Secur"/>, se devuelve el resultado de la evaluación de las reglas especificadas; de lo contrario se devuelve <c>Nothing</c></returns>
            public PwEvalResult Evaluation
            {
                get { return e; }
            }
            /// <summary>
            /// Crea una nueva instancia de la clase <see cref="PwDialogResult"/>
            /// </summary>
            /// <param name="us">Usuario del cuadro de diálogo</param>
            /// <param name="pw">Contraseña introducida por el usuario</param>
            /// <param name="hn">Indicio de contraseña introducida por el usuario</param>
            /// <param name="re">Resultado del cuadro de diálogo</param>
            /// <param name="ev">Resultado de la evaluación</param>
            internal PwDialogResult(string us, string pw, string hn, MessageBoxResult re, PwEvalResult ev)
            {
                u = us;
                p = pw;
                h = hn;
                r = re;
                e = ev;
            }
        }
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
                    Ringsec.Fill = new SolidColorBrush(UITools.BlendHealthColor(Pwr.Result));
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
            return r ?? new PwDialogResult(null, null, null, MessageBoxResult.Cancel, PwEvalResult.Null);
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
            return r ?? new PwDialogResult(null, null, null, MessageBoxResult.Cancel, PwEvalResult.Null);
        } 
        #endregion
    }
}