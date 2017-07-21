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

using System;
using Gtk;
using MCART.PluginSupport;
using System.Linq;
using St = MCART.Resources.Strings;
using MCART.UI;
namespace MCART.Forms
{
    /// <summary>
    /// Diálogo que permite mostrar información acerca de los 
    /// <see cref="Plugin"/> cargables por MCART.
    /// </summary>
    public partial class PluginBrowser : Dialog
    {
        ListStore lstIfaces = new ListStore(typeof(string), typeof(string));
        TreeStore trPlugins;
        ListStore lstPlgins;
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
        private void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVer.Text = P.Version.ToString();
            chkIsBeta.Active = P.IsBeta;
            chkIsUnafe.Active = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyright.Text = P.Copyright;
            txtLicense.Buffer.Text = P.License;
            foreach (Type T in P.Interfaces) ;
            //lstIfaces.Items.Add(new ListViewItem() { Content = T.Name });

            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                chkMinVer.Active = mv > MCART.Resources.RTInfo.RTVersion;
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
                chkTgtVer.Active = tv < MCART.Resources.RTInfo.RTVersion;
                if (chkTgtVer.Active) txtTgtVer.TooltipText = St.UnsupportedVer;
            }
            else
            {
                chkTgtVer.Inconsistent = true;
                txtTgtVer.TooltipText = St.NoData;
            }

            if (P.HasInteractions)
            {
                foreach (InteractionItem j in P.PluginInteractions)
                {
                    MenuItem k = (MenuItem)j;
                    k.Click += j.RoutedAction;
                    roth.Items.Add(k);
                }
                tabInteractions.Content = roth;
            }
            else
            {
                tabInteractions.IsEnabled = false;
                tabInteractions.Content = St.FeatNotAvailable;
            }
        }

        public PluginBrowser()
        {
            Build();
            trvInterfaces.AppendColumn(
                new TreeViewColumn(
                    "Interfaz",
                    new CellRendererText(),
                    "text", 0));
            trvInterfaces.AppendColumn(
                new TreeViewColumn(
                    "Descripción",
                    new CellRendererText(),
                    "text", 1));
            trvInterfaces.Model = lstIfaces;
        }
        public new void Show()
        {
            trPlugins = new TreeStore(typeof(string));
            lstPlgins = new ListStore(typeof(string));



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
            //TODO: Showdetails
        }



        protected void BtnClose_Click(object sender, EventArgs e) => Destroy();

    }
}
