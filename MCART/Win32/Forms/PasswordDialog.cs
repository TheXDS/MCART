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

using MCART.Security.Password;
using System;
using System.Drawing;
using System.Windows.Forms;
using static MCART.Types.Color;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Esta clase contiene una ventana de Windows Forms que permite al usuario
    /// validar contraseñas, así como también establecerlas y medir el nivel de
    /// seguridad de la contraseña escogida.
    /// </summary>
    public partial class PasswordDialog : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PasswordDialog"/>.
        /// </summary>
        public PasswordDialog()
        {
            InitializeComponent();
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
            Text = St.Pwd;
            txtUser.Visible = ShowUsrBox;
            txtUser.Text = DefaultUsr;
            txtPw.Text = StoredPw;
            btnGo.Visible=true;
            //MoreCtrls.Visibility = Visibility.Collapsed;
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
        /// <returns>
        /// Un <see cref="PwDialogResult"/> con el resultado de este diálogo.
        /// </returns>
        public PwDialogResult ChoosePassword(PwMode Mode = PwMode.JustConfirm, PwEvaluator PwEvaluatorObj = null)
        {
            Text = St.SetX(St.Pwd.ToLower());
            txtUser.Visible = (Mode & PwMode.Usr) == PwMode.Usr;
            btnGo.Visible = false;

            //MoreCtrls.Visibility = Visibility.Visible;
            //HintBlock.Visibility = (Mode & PwMode.Hint) == PwMode.Hint ? Visibility.Visible : Visibility.Collapsed;
            if ((Mode & PwMode.Secur) == PwMode.Secur)
            {
                prScore.Visible = true;
                if (PwEvaluatorObj.IsNull())
                {
                    pwEvaluator = new PwEvaluator(RuleSets.CommonComplexityRuleSet());
                    pwEvaluator.Rules.Add(RuleSets.PwLatinEvalRule());
                    pwEvaluator.Rules.Add(RuleSets.PwOtherSymbsEvalRule());
                    pwEvaluator.Rules.Add(RuleSets.PwOtherUTFEvalRule());
                }
                else pwEvaluator = PwEvaluatorObj;
            }
            else prScore.Visible = false;
            ShowDialog();
            return retVal;
        }








        #region Eventos de controles
        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (txtUser.Visible && txtUser.Text.IsEmpty())
            {
                if (!txtUser.IsWarned())
                    txtUser.Warn(St.MandatoryField);
                txtUser.Focus();
                return;
            }
            retVal = new PwDialogResult(txtUser.Text, txtPw.Text, null, DialogResult.OK, PwEvalResult.Null);
            Close();
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtUser.Visible && txtUser.Text.IsEmpty())
            {
                if (!txtUser.IsWarned()) txtUser.Warn(St.MandatoryField);
                txtUser.Focus();
                return;
            }
            if (txtPw.Text != txtConfirm.Text)
            {
                MessageBox.Show(St.PwNotMatch, St.Err);
                txtConfirm.Clear();
                txtPw.Focus();
                txtPw.SelectAll();
                return;
            }
            else if (prScore.Visible && prScore.Value < 50)
            {
                MessageBox.Show(St.PwNtbmc, St.Err);
                txtConfirm.Clear();
                txtPw.Focus();
                txtPw.SelectAll();
                return;
            }
            retVal = new PwDialogResult(txtUser.Text, txtPw.Text, txtHint.Text, DialogResult.OK, pwEvalResult);
            Close();

        }
        private void TxtPw_TextChanged(object sender, EventArgs e)
        {
            if (prScore.Visible)
            {
                if (txtConfirm.IsWarned())
                    txtConfirm.ClearWarn();
                pwEvalResult = pwEvaluator.Evaluate(txtPw.Text);
                lblMorInfo.Text = pwEvalResult.Details;
                prScore.Value = pwEvalResult.Result * 100;
                if (!pwEvalResult.Critical)
                    prScore.Fill = BlendHealth(pwEvalResult.Result);
                else
                    prScore.Fill = SystemColors.Highlight;
            }
        }
        private void WarnCheck(object sender, EventArgs e)
        {
            if (((Control)sender).IsWarned()) ((Control)sender).ClearWarn();
        }
        private void CheckPw(object sender, EventArgs e)
        {
            if (!txtConfirm.Text.IsEmpty() && txtConfirm.Text != txtPw.Text)
                txtConfirm.Warn(St.PwNotMatch);
        }
        #endregion
    }
}