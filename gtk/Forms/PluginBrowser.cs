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

using Gtk;
using MCART.PluginSupport;
using System;
using System.Collections.Generic;
using static MCART.Resources.RTInfo;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Diálogo que permite mostrar información acerca de los 
    /// <see cref="Plugin"/> cargables por MCART.
    /// </summary>
    public partial class PluginBrowser : Dialog
    {
        ListStore lstIfaces = new ListStore(typeof(string));
        TreeStore trPlugins = new TreeStore(typeof(string));
        List<List<IPlugin>> lstPlugins = new List<List<IPlugin>>();
        void ClearDetails()
        {
            txtName.Text.Clear();
            txtVer.Text.Clear();
            chkIsBeta.Active = false;
            chkIsUnafe.Active = false;
            txtDesc.Text.Clear();
            txtCopyright.Text.Clear();
            txtLicense.Buffer.Clear();
            lstIfaces.Clear();
            chkMinVer.Active = false;
            chkMinVer.Inconsistent = false;
            chkTgtVer.Active = false;
            chkTgtVer.Inconsistent = false;
            txtMinVer.Text.Clear();
            txtMinVer.TooltipText.Clear();
            txtTgtVer.Text.Clear();
            txtTgtVer.TooltipText.Clear();
            lblVeredict.Text = St.PluginInfo1;
            mnuInteractions.ClearContents();
        }
        void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVer.Text = P.Version.ToString();
            chkIsBeta.Active = P.IsBeta;
            chkIsUnafe.Active = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyright.Text = P.Copyright;
            txtLicense.Buffer.Text = P.License;
            foreach (Type T in P.Interfaces) lstIfaces.AppendValues(T.Name);
            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                chkMinVer.Active = mv > RTVersion;
                if (chkMinVer.Active) txtMinVer.TooltipText = St.UnsupportedVer;
            }
            else
            {
                chkMinVer.Inconsistent = true;
                txtMinVer.TooltipText = St.NoData;
            }
            if (P.TargetRTVersion(out Version tv))
            {
                txtTgtVer.Text = mv.ToString();
                chkTgtVer.Active = tv < RTVersion;
                if (chkTgtVer.Active) txtTgtVer.TooltipText = St.UnsupportedVer;
            }
            else
            {
                chkTgtVer.Inconsistent = true;
                txtTgtVer.TooltipText = St.NoData;
            }
            if (chkMinVer.Active && chkTgtVer.Active)
                lblVeredict.Text = St.PluginInfo2;
            else if (chkMinVer.Inconsistent || chkTgtVer.Inconsistent)
                lblVeredict.Text = St.PluginInfo4;
            else lblVeredict.Text = St.PluginInfo3;
            if (P.HasInteractions)
            {
                MenuItem r = new MenuItem(P.Name);
                Menu roth = new Menu();
                r.Submenu = roth;
                foreach (InteractionItem j in P.PluginInteractions)
                {
                    ImageMenuItem k = j;
                    k.Activated += j.Action;
                    roth.Append(k);
                }
                mnuInteractions.Append(r);
            }
        }
        void BtnClose_Click(object sender, EventArgs e) => Destroy();
        void TrvPlugins_Shown()
        {
            if (!trvPlugins.Visible) return;
            trPlugins.Clear();
            foreach (var j in Plugin.PluginTree<IPlugin>(true))
            {
                TreeIter plg = trPlugins.AppendValues(j.Key);
                foreach (var k in j.Value) trPlugins.AppendValues(plg, k.Name);
                lstPlugins.Add(j.Value);
            }
        }
        void OnTrvPluginsCursorChanged(object sender, EventArgs e)
        {
            trvPlugins.GetCursor(out TreePath tp, out TreeViewColumn tvc);
            ClearDetails();
            if (tp.Indices.Length == 2)
                ShwDetails(lstPlugins[tp.Indices[0]][tp.Indices[1]]);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PluginBrowser"/>.
        /// </summary>
		public PluginBrowser()
        {
            Build();
            trvInterfaces.AppendColumn("Interfaz", new CellRendererText(), "text", 0);
            trvInterfaces.Model = lstIfaces;
            trvPlugins.AppendColumn("Plugin", new CellRendererText(), "text", 0);
            trvPlugins.Model = trPlugins;
        }
        /// <summary>
        /// Marca un <see cref="Widget"/> para ser mostrado. 
        /// </summary>
        public new void Show()
        {
            TrvPlugins_Shown();
            base.Show();
        }
        /// <summary>
        /// Muestra información acerca de un <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="IPlugin"/> del cual mostrar la información.
        /// </param>
        public void DetailsOf(IPlugin p)
        {
            trvPlugins.Visible = false;
            trvPlugins.Sensitive = false;
            ShwDetails(p);
            Show();
        }
    }
}