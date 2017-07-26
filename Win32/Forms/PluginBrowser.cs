//
//  PluginBrowser.cs
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

using MCART.PluginSupport;
using System;
using System.Windows.Forms;
using static MCART.Resources.RTInfo;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Diálogo que permite mostrar información acerca de los 
    /// <see cref="Plugin"/> cargables por MCART.
    /// </summary>
    public partial class PluginBrowser : Form
    {
        void ClrDetails()
        {
            txtName.Clear();
            txtVersion.Clear();
            chkIsBeta.Checked = false;
            chkUnstable.Checked = false;
            txtDesc.Clear();
            txtCopyright.Clear();
            txtLicense.Clear();
            lstInterfaces.Items.Clear();
            txtMinVer.Clear();
            errProvider.SetError(txtMinVer, string.Empty);
            txtTgtVer.Clear();
            errProvider.SetError(txtTgtVer, string.Empty);
            lblVeredict.Text = St.PluginInfo1;
            mnuInteractions.Items.Clear();
        }
        void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVersion.Text = P.Version.ToString();
            chkIsBeta.Checked = P.IsBeta;
            chkUnstable.Checked = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyright.Text = P.Copyright;
            txtLicense.Text = P.License;
            foreach (Type T in P.Interfaces)
                lstInterfaces.Items.Add(T.Name);
            bool? mvf = null, tvf = null;
            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                if (mv > RTVersion)
                {
                    mvf = false;
                    errProvider.SetError(txtMinVer, St.UnsupportedVer);
                }
                else mvf = true;
            }
            else errProvider.SetError(txtMinVer, St.NoData);
            if (P.TargetRTVersion(out Version tv))
            {
                txtTgtVer.Text = tv.ToString();
                if (tv < RTVersion)
                {
                    tvf = false;
                    errProvider.SetError(txtTgtVer, St.UnsupportedVer);
                }
                else tvf = true;
            }
            else errProvider.SetError(txtTgtVer, St.NoData);
            if (mvf == true && tvf == true)
                lblVeredict.Text = St.PluginInfo2;
            else if (!mvf.HasValue || !tvf.HasValue)
                lblVeredict.Text = St.PluginInfo4;
            else lblVeredict.Text = St.PluginInfo3;
            if (P.HasInteractions)
            {
                ToolStripMenuItem roth = new ToolStripMenuItem(P.Name);
                mnuInteractions.Items.Add(roth);
                foreach (InteractionItem j in P.PluginInteractions)
                    roth.DropDownItems.Add(j);
            }
            else mnuInteractions.Items.Add(St.FeatNotAvailable);
        }
        void BtnClose_Click(object sender, EventArgs e) => Close();
        void TrvAsm_Layout(object sender, LayoutEventArgs e)
        {
            if (!trvAsm.Visible) return;
            foreach (var j in Plugin.PluginTree<IPlugin>(true))
            {
                TreeNode plg = trvAsm.Nodes.Add(j.Key);
                foreach (var k in j.Value) (plg.Nodes.Add(k.Name)).Tag = k;                
            }
        }
        void TrvAsm_Click(object sender, EventArgs e)
        {
            ClrDetails();
            if (trvAsm.SelectedNode.Tag is IPlugin)
                ShwDetails((IPlugin)trvAsm.SelectedNode.Tag);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginBrowser"/>.
        /// </summary>
        public PluginBrowser() => InitializeComponent();
        /// <summary>
        /// Muestra información acerca de un <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="IPlugin"/> del cual mostrar la información.
        /// </param>
        public void DetailsOf(IPlugin p)
        {
            trvAsm.Visible = false;
            ShwDetails(p);
            ShowDialog();
        }
    }
}