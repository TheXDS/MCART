//
//  SearchBox.cs
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

using MCART.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Converters;
using System.Windows.Data;
using St = MCART.Resources.Strings;

namespace MCART.Controls
{
    /// <summary>
    /// Cuadro de texto simple con marca de agua optimizado para búsquedas.
    /// </summary>
    public class SearchBox : UserControl
    {
        /// <summary>
        /// Se produce cuando se ha conectado a un <see cref="CollectionView"/>.
        /// </summary>
        public event AttachedToViewEventHandler AttachedToView;

        /// <summary>
        /// Controla el evento <see cref="AttachedToView"/>.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">argumentos del evento.</param>
        public delegate void AttachedToViewEventHandler(object sender, ValueEventArgs<CollectionView> e);

        /// <summary>
        /// Se produce cuando se ha introducido texto en el cuadro de búsqueda.
        /// </summary>
        public event SearchEnteredEventHandler SearchEntered;

        /// <summary>
        /// Controla el evento <see cref="SearchEntered"/>.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">argumentos del evento.</param>
        public delegate void SearchEnteredEventHandler(object sender, ValueEventArgs<string> e);

        /// <summary>
        /// Se produce cuando se ha cerrado la búsqueda.
        /// </summary>
        public event SearchClosedEventHandler SearchClosed;

        /// <summary>
        /// Controla el evento <see cref="SearchClosed"/>.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">argumentos del evento.</param>
        public delegate void SearchClosedEventHandler(object sender, EventArgs e);

        static Type T = typeof(SearchBox);

        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="HasSearch"/>.
        /// </summary>
        public static DependencyProperty HasSearchProperty = DependencyProperty.Register(nameof(HasSearch), typeof(bool), T, new PropertyMetadata(true));

        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Search"/>.
        /// </summary>
        public static DependencyProperty SearchProperty = DependencyProperty.Register(nameof(Search), typeof(string), T, new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="SearchWatermark"/>.
        /// </summary>
        public static DependencyProperty SearchWatermarkProperty = DependencyProperty.Register(nameof(SearchWatermark), typeof(string), T, new PropertyMetadata(St.Search));

        /// <summary>
        /// Obtiene o establece si se mostrará el cuadro de búsqueda
        /// </summary>
        /// <returns><c>True</c> si el cuadro de búsqueda es visible; de lo contrario, <c>False</c></returns>
        public bool HasSearch
        {
            get => (bool)GetValue(HasSearchProperty); set => SetValue(HasSearchProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el valor actual del cuadro de búsqueda.
        /// </summary>        
        public string Search
        {
            get => (string)GetValue(SearchProperty); set => txtSearch.Text = value;
        }

        /// <summary>
        /// Obtiene o establece la marca de agua a mostrar en el cuadro de búsqueda
        /// </summary>        
        public string SearchWatermark
        {
            get => (string)GetValue(SearchWatermarkProperty); set => SetValue(SearchWatermarkProperty, value);
        }

        BindingListCollectionView view = null;

        List<string> flts = new List<string>();

        TextBox txtSearch = new TextBox()
        {
            // Este control es transparente
            // para poder mostrar el texto en
            // marca de agua.            
            Background = null
        };

        Button btnClseSearch = new Button()
        {
            Width = 24,
            Content = "X"
        };

        /// <summary>
        /// Inicializa la clase <see cref="SearchBox"/>.
        /// </summary>
        static SearchBox()
        {
            BackgroundProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(SystemColors.WindowBrush));            
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SearchBox"/>.
        /// </summary>
        public SearchBox()
        {
            TextBlock d = new TextBlock
            {
                Margin = new Thickness(5, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontStyle = FontStyles.Italic,
                Foreground = SystemColors.GrayTextBrush
            };
            d.SetBinding(TextBlock.TextProperty, new Binding(nameof(SearchWatermark)) { Source = this });
            d.SetBinding(VisibilityProperty, new Binding(nameof(Search))
            {
                Source = this,
                Converter = new StringVisibilityConverter(Visibility.Visible, Visibility.Hidden)
            });
            btnClseSearch.SetBinding(VisibilityProperty, new Binding(nameof(Search))
            {
                Source = this,
                Converter = new StringVisibilityConverter(Visibility.Collapsed, Visibility.Visible)
            });
            SetBinding(SearchProperty, new Binding(nameof(txtSearch.Text)) { Source = txtSearch, Mode = BindingMode.TwoWay });
            DockPanel roth = new DockPanel();
            DockPanel.SetDock(btnClseSearch, Dock.Right);
            Grid box = new Grid();
            box.SetBinding(BackgroundProperty, new Binding(nameof(Background)) { Source = this });
            box.Children.Add(d);
            box.Children.Add(txtSearch);
            roth.Children.Add(btnClseSearch);
            roth.Children.Add(box);
            Content = roth;
            btnClseSearch.Click += CnclSrch;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        void CnclSrch(object sender, RoutedEventArgs e) => txtSearch.Clear();

        string GenFilters(string s)
        {
            StringBuilder x = new StringBuilder();
            foreach (string j in flts)
            {
                if (!x.ToString().IsEmpty()) x.Append(" OR ");
                x.Append($"{j} LIKE '%{s}%'");
            }
            return x.ToString();
        }

        void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.IsEmpty())
            {
                if (!view.IsNull())
                {
                    if (view.CanCustomFilter) view.CustomFilter = string.Empty;
                    view.Refresh();
                    view.MoveCurrentToFirst();
                }
                SearchClosed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (!view.IsNull() && flts.Count > 0)
                {
                    if (view.CanCustomFilter) view.CustomFilter = GenFilters(txtSearch.Text);
                    view.Refresh();
                    view.MoveCurrentToFirst();
                }
                SearchEntered?.Invoke(this, new ValueEventArgs<string>(txtSearch.Text));
            }
        }

        /// <summary>
        /// Conecta un <see cref="BindingListCollectionView"/> para ser
        /// controlado automáticamente por este control.
        /// </summary>
        /// <param name="cv">
        /// <see cref="BindingListCollectionView"/> a controlar.
        /// </param>
        /// <param name="searchFields">
        /// Si <see cref="HasSearch"/> es <c>true</c>, establece los campos
        /// necesarios para realizar búsquedas.
        /// </param>
        public void AttachView(BindingListCollectionView cv, string[] searchFields = null)
        {
            if (!view.IsNull()) flts.Clear();
            if (HasSearch && !searchFields.IsNull()) flts.AddRange(searchFields);
            view = cv ?? throw new ArgumentNullException(nameof(cv));
            view.MoveCurrentToFirst();
            txtSearch.Text = string.Empty;
            AttachedToView?.Invoke(this, new ValueEventArgs<CollectionView>(cv));
        }

        /// <summary>
        /// Libera la conexión de <see cref="BindingListCollectionView"/>
        /// </summary>
        public void DetachView()
        {
            view = null;
            flts.Clear();
            txtSearch.Clear();
        }
    }
}
