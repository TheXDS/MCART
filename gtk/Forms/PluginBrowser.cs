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
namespace MCART.Forms
{
    public partial class PluginBrowser : Dialog
    {
        ListStore LstIfaces = new ListStore(typeof(string), typeof(string));
        TreeStore TrPlugins;
        ListStore LstPlgins;
        void ClearDetails()
        {
            TxtName.Text.Clear();
            TxtVer.Text.Clear();
            TxtDesc.Text.Clear();
            TxtCopyright.Text.Clear();
            LstIfaces.Clear();
        }
        public PluginBrowser()
        {
            Build();
            TrvInterfaces.AppendColumn(
                new TreeViewColumn(
                    "Interfaz",
                    new CellRendererText(),
                    "text", 0));
            TrvInterfaces.AppendColumn(
                new TreeViewColumn(
                    "Descripción",
                    new CellRendererText(),
                    "text", 1));
            TrvInterfaces.Model = LstIfaces;
        }
        public new void Show()
        {
            TrPlugins = new TreeStore(typeof(string));
            LstPlgins = new ListStore(typeof(string));



            base.Show();
        }
        /// <summary>
        /// Muestra los detalles de el plugin especificado.
        /// </summary>
        /// <param name="plg">Plg.</param>
        public void DetailsOf(IPlugin plg)
        {
            TrvPlugins.Visible = false;
            TrvPlugins.Sensitive = false;
            //TODO: Showdetails
        }



        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Destroy();
        }

        protected void OnBtnTestClicked(object sender, EventArgs e)
        {
            //TODO: diálogo de pruebas de UI
        }


    }
}
