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
using System.Linq;
using static MCART.Resources.RTInfo;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Diálogo que permite mostrar información acerca de los 
    /// <see cref="Plugin"/> cargables por MCART.
    /// </summary>
    public class PluginBrowser : Dialog
    {
        #region Construcción de ventana
        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="PluginBrowser"/>.
        /// </summary>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="PluginBrowser"/>.
        /// </returns>
        public static PluginBrowser Create()
        {
            var t = typeof(PluginBrowser);
            Builder builder = new Builder(RTAssembly, $"{t.FullName}.glade", null);
            return new PluginBrowser(builder, builder.GetObject(t.Name).Handle);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="PluginBrowser"/>.
        /// </summary>
        /// <param name="builder">
        /// <see cref="Builder"/> utilizado para construir la interfaz.
        /// </param>
        /// <param name="handle">
        /// Handle obtenido de la ventana creada por <paramref name="builder"/>.
        /// </param>
        public PluginBrowser(Builder builder, IntPtr handle) : base(handle)
        {
            builder.Autoconnect(this);
            trvInterfaces.AppendColumn("Interfaz", new CellRendererText(), "text", 0);
            trvInterfaces.Model = lstIfaces;
            trvPlugins.AppendColumn("Ensamblado", new CellRendererText(), "text", 0);
            trvPlugins.Model = trPlugins;
        }
        #endregion

        #region Widgets
#pragma warning disable CS0649
#pragma warning disable CS0169
        [Builder.Object] Entry txtName;
        [Builder.Object] Entry txtVer;
        [Builder.Object] CheckButton chkBeta;
        [Builder.Object] CheckButton chkUnstable;
        [Builder.Object] Entry txtDesc;
        [Builder.Object] Entry txtCopyright;
        [Builder.Object] TextView txtLicense;
        [Builder.Object] CheckButton chkMinVer;
        [Builder.Object] CheckButton chkTgtVer;
        [Builder.Object] Entry txtMinVer;
        [Builder.Object] Entry txtTgtVer;
        [Builder.Object] Label lblVeredict;
        [Builder.Object] MenuBar mnuInteractions;
        [Builder.Object] TreeView trvInterfaces;
        [Builder.Object] TreeView trvPlugins;
#pragma warning restore CS0649
#pragma warning restore CS0169
        #endregion

        #region Campos privados
        ListStore lstIfaces = new ListStore(typeof(string));
        TreeStore trPlugins = new TreeStore(typeof(string));
        List<IEnumerable<IPlugin>> lstPlugins = new List<IEnumerable<IPlugin>>();
        #endregion

        #region Métodos privados
        void ClearDetails()
        {
            txtName.Clear();
            txtVer.Clear();
            chkBeta.Active = false;
            chkUnstable.Active = false;
            txtDesc.Clear();
            txtCopyright.Clear();
            txtLicense.Buffer.Clear();
            lstIfaces.Clear();
            chkMinVer.Active = false;
            chkMinVer.Inconsistent = false;
            chkTgtVer.Active = false;
            chkTgtVer.Inconsistent = false;
            txtMinVer.Clear();
            txtMinVer.ClearTooltip();
            txtTgtVer.Clear();
            txtTgtVer.ClearTooltip();
            lblVeredict.Text = St.PluginInfo1;
            mnuInteractions.ClearContents();
        }
        void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVer.Text = P.Version.ToString();
            chkBeta.Active = P.IsBeta;
            chkUnstable.Active = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyright.Text = P.Copyright;
            txtLicense.Buffer.Text = P.License;
            foreach (Type T in P.Interfaces) lstIfaces.AppendValues(T.Name);
            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                chkMinVer.Active = mv <= RTVersion;
                if (!chkMinVer.Active) txtMinVer.TooltipText = St.UnsupportedVer;
            }
            else
            {
                chkMinVer.Inconsistent = true;
                txtMinVer.TooltipText = St.NoData;
            }
            if (P.TargetRTVersion(out Version tv))
            {
                txtTgtVer.Text = tv.ToString();
                chkTgtVer.Active = tv >= RTVersion;
                if (!chkTgtVer.Active) txtTgtVer.TooltipText = St.UnsupportedVer;
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
                mnuInteractions.Append(P.UIMenu);
        }
        #endregion

        #region Controladores de eventos
        /// <summary>
        /// Cierra este <see cref="PluginBrowser"/> 
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        void BtnClose_Click(object sender, EventArgs e) => Destroy();
        void TrvPlugins_Shown(object sender, EventArgs e)
        {
            if (!trvPlugins.Visible) return;
            trPlugins.Clear();
            foreach (var j in (new PluginLoader(new RelaxedPluginChecker())).PluginTree())
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
            if (tp?.Indices.Length == 2)
                ShwDetails(lstPlugins[tp.Indices[0]].ElementAt(tp.Indices[1]));
        }
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Marca un <see cref="Widget"/> para ser mostrado. 
        /// </summary>
        public new void Show()
        {
            TrvPlugins_Shown(this, EventArgs.Empty);
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
        #endregion
    }
}