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
using MCART.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Lógica de interacción para PluginBrowser.xaml
    /// </summary>
    public partial class PluginBrowser : Window
    {
        private void ClrDetails()
        {
            txtName.Clear();
            txtVersion.Clear();
            chkBeta.IsChecked = false;
            chkUnstable.IsChecked = false;
            txtDesc.Clear();
            txtCopyrgt.Clear();
            txtLicense.Clear();
            lstInterf.Items.Clear();
            txtMinVer.Clear();
            txtMinVer.ClearWarn();
            txtTgtVer.Clear();
            txtTgtVer.ClearWarn();
            tabInteractions.IsEnabled = false;
            tabInteractions.Content = null;
        }
        private void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVersion.Text = P.Version.ToString();
            chkBeta.IsChecked = P.IsBeta;
            chkUnstable.IsChecked = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyrgt.Text=P.Copyright;
            txtLicense.Text=P.License;
            lstInterf.Items.Clear();
            foreach (Type T in P.Interfaces)
                lstInterf.Items.Add(new ListViewItem() { Content = T.Name });
            txtMinVer.ClearWarn();
            txtTgtVer.ClearWarn();
            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                if (mv > MCART.Resources.RTInfo.RTVersion)
                    txtMinVer.Warn(St.UnsupportedVer);
            }
            else txtMinVer.Warn(St.NoData);            

            if (P.MinRTVersion(out Version tv))
            {
                txtTgtVer.Text = tv.ToString();
                if (tv < MCART.Resources.RTInfo.RTVersion)
                    txtTgtVer.Warn(St.UnsupportedVer);
            }
            else txtTgtVer.Warn(St.NoData);
            if (P.HasInteractions)
            {
                MenuItem roth = new MenuItem()
                {
                    Header = P.Name,
                    VerticalAlignment=VerticalAlignment.Center,
                    HorizontalAlignment=HorizontalAlignment.Center
                };
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
        private void BtnClose_Click(object sender, RoutedEventArgs e) { Close(); }
        private void TrvAsm_Loaded(object sender, RoutedEventArgs e)
        {
            if (trvAsm.Visibility == Visibility.Collapsed) return;
            foreach (var j in Plugin.Tree<IPlugin>())
            {
                TreeViewItem roth = new TreeViewItem() { Header = j.Key };
                foreach (var k in j.Value)
                {
                    TreeViewItem itm = new TreeViewItem() { Header = k.Name, Tag = k };
                    itm.Selected += Itm_Selected;
                    roth.Items.Add(itm);
                }
                trvAsm.Items.Add(roth);
            }
        }
        private void Itm_Selected(object sender, RoutedEventArgs e)
        {
            ShwDetails((IPlugin)((TreeViewItem)sender).Tag);
        }

        /// <summary>
        /// Muestra información acerca de un <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="IPlugin"/> del cual mostrar la información.
        /// </param>
        public void DetailsOf(IPlugin p)
        {
            trvAsm.Visibility = Visibility.Collapsed;
            ShwDetails(p);
            ShowDialog();            
        }
        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="PluginBrowser"/>.
        /// </summary>
        public PluginBrowser()
        {
            InitializeComponent();
        }
    }
}