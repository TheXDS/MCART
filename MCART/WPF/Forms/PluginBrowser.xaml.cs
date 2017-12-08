//
//  PluginBrowser.cs
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

using MCART.PluginSupport;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using static MCART.Resources.RTInfo;
using St = MCART.Resources.Strings;

namespace MCART.Forms
{
    /// <summary>
    /// Diálogo que permite mostrar información acerca de los 
    /// <see cref="Plugin"/> cargables por MCART.
    /// </summary>
    public partial class PluginBrowser : Window
    {
        void ClrDetails()
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
            lblVeredict.Text = St.PluginInfo1;
            tabInteractions.IsEnabled = false;
            tabInteractions.Content = null;
        }
        void ShwDetails(IPlugin P)
        {
            txtName.Text = P.Name;
            txtVersion.Text = P.Version.ToString();
            chkBeta.IsChecked = P.IsBeta;
            chkUnstable.IsChecked = P.IsUnstable;
            txtDesc.Text = P.Description;
            txtCopyrgt.Text = P.Copyright;
            txtLicense.Text = P.License;
            foreach (Type T in P.Interfaces)
                lstInterf.Items.Add(new ListViewItem() { Content = T.Name });
            bool? mvf = null, tvf = null;
            if (P.MinRTVersion(out Version mv))
            {
                txtMinVer.Text = mv.ToString();
                if (mv > RTVersion)
                {
                    mvf = false;
                    txtMinVer.Warn(St.UnsupportedVer);
                }
                else mvf = true;
            }
            else
            {
                txtMinVer.Text = St.Unk;
                txtMinVer.Warn(St.NoData);
            }
            if (P.TargetRTVersion(out Version tv))
            {
                txtTgtVer.Text = tv.ToString();
                if (tv < RTVersion)
                {
                    tvf = false;
                    txtTgtVer.Warn(St.UnsupportedVer);
                }
                else tvf = true;
            }
            else
            {
                txtTgtVer.Text = St.Unk;
                txtTgtVer.Warn(St.NoData);
            }
            if (mvf == true && tvf == true)
                lblVeredict.Text = St.PluginInfo2;
            else if (!mvf.HasValue || !tvf.HasValue)
                lblVeredict.Text = St.PluginInfo4;
            else lblVeredict.Text = St.PluginInfo3;
            if (P.HasInteractions)
            {
                var pnl = P.UIPanel<Button, WrapPanel>();
                var thk = new Thickness(5);
                foreach (var j in pnl.Children.OfType<FrameworkElement>()) j.Margin = thk;
                pnl.Orientation = Orientation.Vertical;
                tabInteractions.Content = pnl;
            }
            else
            {
                tabInteractions.IsEnabled = false;
                tabInteractions.Content = St.FeatNotAvailable;
            }
        }
        void BtnClose_Click(object sender, RoutedEventArgs e) => Close();
        void TrvAsm_Loaded(object sender, RoutedEventArgs e)
        {
            if (trvAsm.Visibility == Visibility.Collapsed) return;
            foreach (var j in Plugin.PluginTree<IPlugin>(true))
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
        void Itm_Selected(object sender, RoutedEventArgs e)
        {
            ClrDetails();
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
        /// Inicializa una nueva instancia de la clase
        /// <see cref="PluginBrowser"/>.
        /// </summary>
        public PluginBrowser() => InitializeComponent();
    }
}