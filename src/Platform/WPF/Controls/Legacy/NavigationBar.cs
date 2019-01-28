/*
NavigationBar.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using TheXDS.MCART.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Converters;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using TheXDS.MCART.Resources;
using static TheXDS.MCART.WpfUi;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Controls
{
    /// <inheritdoc />
    /// <remarks>
    /// Este control es especialmente útil para controlar un objeto 
    /// <see cref="T:System.Windows.Data.CollectionView" /> provisto por las conexiones de bases de 
    /// datos, en efecto, cumpliendo las funciones de un controlador en el
    /// paradigma Model-View-Controller (MVC).
    /// </remarks>
    [Obsolete(InternalStrings.LegacyComponent)]
    public class NavigationBar : UserControl
    {
        #region ValueConverters privados para controles.
        class Editvalconv : IValueConverter
        {
            NavigationBarEditMode f;
            internal Editvalconv(NavigationBarEditMode flg) { f = flg; }
            object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (((NavigationBarEditMode)value & f) != 0) ? Visibility.Visible : Visibility.Collapsed;
            }
            object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (Visibility)value == Visibility.Visible ? NavigationBarEditMode.All : NavigationBarEditMode.ReadOnly;
            }
        }
        class Editvalconv2 : IValueConverter
        {
            NavigationBarEditMode f;
            internal Editvalconv2(NavigationBarEditMode flg) { f = flg; }
            object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return ((NavigationBarEditMode)value & f) != 0;
            }
            object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (bool)value ? NavigationBarEditMode.All : NavigationBarEditMode.ReadOnly;
            }
        }
        #endregion

        #region Controles
        /// <summary>
        /// Anchura predeterminada de los botones.
        /// </summary>
        const double btnW = 24;
        /// <summary>
        /// Altura predeterminada de los botones.
        /// </summary>
        const double btnH = 20;
        /// <summary>
        /// <see cref="Thickness"/> predeterminado para algunos controles.
        /// </summary>
        static Thickness thk1 = new Thickness(5, 5, 5, 0);
        DockPanel pnlNav = new DockPanel()
        {
            VerticalAlignment = VerticalAlignment.Center
        };
        TextBlock lblInfo = new TextBlock()
        {
            Text = St.Of,
            Margin = new Thickness(5, 0, 5, 0)
        };
        Button btnFirst = new Button()
        {
            Width = btnW,
            Content = (char)9198
        };
        Button btnPrev = new Button()
        {
            Width = btnW,
            Content = (char)9664
        };
        Button btnNext = new Button()
        {
            Width = btnW,
            Content = (char)9654
        };
        Button btnLast = new Button()
        {
            Width = btnW,
            Content = (char)9197
        };

        // TODO: Reemplazar por un posible nuevo control compatible con marca de agua.
        TextBox txtSearch = new TextBox()
        {
            Width = 100,

            // Este control es transparente
            // para poder mostrar el texto en
            // marca de agua.            
            Background = null
        };
        Button btnClseSearch = new Button()
        {
            Width = btnW,
            Content = "X"
        };
        Button btnNew = new Button()
        {
            Height = 20,
            Content = St.BtnNew,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = thk1
        };
        Button btnEdit = new Button()
        {
            Height = 20,
            Content = St.BtnEdit,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = thk1
        };
        Button btnDel = new Button()
        {
            Height = 20,
            Content = St.BtnDel,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = thk1
        };
        Button btnSave = new Button()
        {
            Height = 20,
            Content = St.BtnSave,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = thk1,
            Visibility = Visibility.Collapsed
        };
        Button btnCncl = new Button()
        {
            Height = 20,
            Content = St.Cancel,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = thk1,
            Visibility = Visibility.Collapsed
        };
        TextBox txtPos = new TextBox()
        {
            Width = 40
        };
        TextBlock lblTot = new TextBlock();
        #endregion

        #region Miembros privados
        /// <summary>
        /// Lista a ser controlada de manera opcional.
        /// </summary>
        BindingListCollectionView view = null;
        /// <summary>
        /// Lista de filtros de búsqueda.
        /// </summary>
        List<string> flts = new List<string>();
        /// <summary>
        /// Lista de controles con Binding de datos a controlar.
        /// </summary>
        List<UIElement> ctrls = new List<UIElement>();
        bool wasNewPressed;
        #endregion

        #region Propiedades de dependencia
        static Type T = typeof(NavigationBar);
        /// <summary>
        /// Clave de propiedad de dependencia <see cref="HasItemsProperty"/>.
        /// </summary>
        protected static DependencyPropertyKey HasItemsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(HasItems), typeof(bool), T, new PropertyMetadata(true));
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura <see cref="HasItems"/>.
        /// </summary>
        public static DependencyProperty HasItemsProperty = HasItemsPropertyKey.DependencyProperty;
        /// <summary>
        /// Clave de propiedad de dependencia <see cref="IsEditingProperty"/>.
        /// </summary>
        public static DependencyPropertyKey IsEditingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsEditing), typeof(bool), T, new PropertyMetadata(false));
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura <see cref="IsEditing"/>.
        /// </summary>
        public static DependencyProperty IsEditingProperty = IsEditingPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="ButtonWidth"/>.
        /// </summary>
        public static DependencyProperty ButtonWidthProperty = DependencyProperty.Register(nameof(ButtonWidth), typeof(double), T, new PropertyMetadata(Convert.ToDouble(80)));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Mode"/>.
        /// </summary>
        public static DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(NavigationBarEditMode), T, new PropertyMetadata(NavigationBarEditMode.ReadOnly), (a) => typeof(NavigationBarEditMode).IsEnumDefined(a));
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
        /// Identifica la propiedad de dependencia <see cref="Max"/>.
        /// </summary>
        public static DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(int), T, new PropertyMetadata(0, UpdtLayout), a => (int)a >= 0);
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Position"/>.
        /// </summary>
        public static DependencyProperty PositionProperty = DependencyProperty.Register(nameof(Position), typeof(int), T, new PropertyMetadata(1, UpdtLayout), a => (int)a >= 1);
        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene un valor que indica si la lista de la barra de navegación está vacía
        /// </summary>
        public bool HasItems => (bool)GetValue(HasItemsProperty);
        /// <summary>
        /// Obtiene un valor que indica si actualmente el control se encuentra en modo de edición
        /// </summary>        
        public bool IsEditing => (bool)GetValue(IsEditingProperty);
        /// <summary>
        /// Obtiene o establece el ancho de los botones de edición de este control
        /// </summary>        
        public double ButtonWidth
        {
            get => (double)GetValue(ButtonWidthProperty); set => SetValue(ButtonWidthProperty, value);
        }
        /// <summary>
        /// Obtiene o establece los modos de edición disponibles para este control
        /// </summary>        
        public NavigationBarEditMode Mode
        {
            get => (NavigationBarEditMode)GetValue(ModeProperty); set => SetValue(ModeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece si se mostrará el cuadro de búsqueda
        /// </summary>
        /// <returns><see langword="true"/> si el cuadro de búsqueda es visible; de lo contrario, <c>False</c></returns>
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
        /// <summary>
        /// Obtiene o establece el valor máximo de la barra de navegación
        /// </summary>
        /// <returns>El valor máximo de elementos a los que este nevegador debe permitir acceder</returns>
        public int Max
        {
            get => (int)GetValue(MaxProperty); set => SetValue(MaxProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el valor actual de la barra de navegación
        /// </summary>
        /// <returns>El elemento actual al que el usuario accedió por medio de la barra</returns>
        public int Position
        {
            get => (int)GetValue(PositionProperty); set => SetValue(PositionProperty, value);
        }
        /// <summary>
        /// Obtiene el listado de campos para realizar búsquedas
        /// </summary>
        public List<string> Filters => flts;
        /// <summary>
        /// Determina si este control administra un <see cref="CollectionView"/>
        /// </summary>
        /// <returns><see langword="true"/> si este control actualmente administra un <see cref="CollectionView"/>, <see langword="false"/> en caso contrario.</returns>
        public bool HasViewAttached => view != null;
        /// <summary>
        /// Devuelve el <see cref="CollectionView"/> actualmente administrado por este control
        /// </summary>
        /// <returns>El <see cref="CollectionView"/> actualmente administrado por este control en caso de haberse establecido; de lo contrario, <c>Nothing</c></returns>
        public CollectionView AttachedView => view;
        /// <summary>
        /// Determina si este control administra el estado de otros controles
        /// </summary>
        /// <returns><see langword="true"/> si este control administra el estado de otros controles; de lo contario, <c>False</c>.</returns>
        public bool HasAttachedControls => ctrls.Any();
        /// <summary>
        /// Devuelve un <see cref="ReadOnlyCollection{T}"/> de los controles administrados por este control
        /// </summary>
        /// <returns>Un <see cref="ReadOnlyCollection{T}"/> de los controles administrados por este control</returns>
        public ReadOnlyCollection<UIElement> AttachedControls => ctrls.AsReadOnly();
        #endregion

        #region Eventos
        /// <summary>
        /// Se produce cuando se ha conectado a un <see cref="CollectionView"/>.
        /// </summary>
        public event EventHandler<ValueEventArgs<CollectionView>> AttachedToView;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al primer elemento.
        /// </summary>
        public event EventHandler<DependencyPropertyChangingEventArgs> MovingToFirst;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al elemento anterior.
        /// </summary>
        public event EventHandler<DependencyPropertyChangingEventArgs> MovingToPrev;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al elemento siguiente.
        /// </summary>
        public event EventHandler<DependencyPropertyChangingEventArgs> MovingToNext;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al último elemento.
        /// </summary>
        public event EventHandler<DependencyPropertyChangingEventArgs> MovingToLast;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación a un elemento en particular.
        /// </summary>
        public event EventHandler<DependencyPropertyChangingEventArgs> MovingToPosition;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al primer elemento.
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> MovedToFirst;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al elemento anterior.
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> MovedToPrev;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al elemento siguiente.
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> MovedToNext;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación al último elemento.
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> MovedToLast;
        /// <summary>
        /// Se produce cuando se ha solicitado la navegación a un elemento en particular.
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> MovedToPosition;
        /// <summary>
        /// Se produce cuando se ha introducido texto en el cuadro de búsqueda.
        /// </summary>
        public event EventHandler<ValueEventArgs<string>> SearchEntered;
        /// <summary>
        /// Se produce cuando se ha cerrado la búsqueda.
        /// </summary>
        public event EventHandler SearchClosed;
        /// <summary>
        /// Se produce cuando se ha solicitado la creación de un elemento nuevo.
        /// </summary>
        public event EventHandler<CancelEventArgs> CreatingNew;
        /// <summary>
        /// Se produce cuando se ha solicitado la edición del elemento actual.
        /// </summary>
        public event EventHandler<CancelEventArgs> Editing;
        /// <summary>
        /// Se produce cuando se ha solicitado la eliminación del elemento actual.
        /// </summary>
        public event EventHandler<CancelEventArgs> Deleting;
        /// <summary>
        /// Se produce cuando se ha presionado el botón Guardar al editar o crear un nuevo elemento.
        /// </summary>
        public event EventHandler<ItemCreatingEventArgs<object>> Saving;
        /// <summary>
        /// Se produce cuando se ha presionado el botón Cancelar al editar o crear un nuevo elemento.
        /// </summary>
        public event EventHandler<CancelEventArgs> Cancelling;
        /// <summary>
        /// Se produce cuando se ha creado un elemento nuevo.
        /// </summary>
        public event EventHandler NewCreated;
        /// <summary>
        /// Se produce cuando se ha entrado en modo de edición.
        /// </summary>
        public event EventHandler EditEntered;
        /// <summary>
        /// Se produce cuando se ha eliminado un elemento.
        /// </summary>
        public event EventHandler ItemDeleted;
        /// <summary>
        /// Se produce cuando se ha guardado un elemento
        /// </summary>
        public event EventHandler<ItemCreatedEventArgs<object>> ItemSaved;
        /// <summary>
        /// Se produce cuando se ha cancelado la creación/edición de un elemento
        /// </summary>
        public event EventHandler Cancelled;
        #endregion

        #region Métodos privados
        static void UpdtLayout(DependencyObject dd, DependencyPropertyChangedEventArgs e)
        {
            var d = (NavigationBar)dd;
            var max = d.Max;
            var pos = d.Position;

            if (e.Property.Is(MaxProperty))
            {
                // Si el máximo de elementos es superior a la posición actual...
                if ((int)e.NewValue < pos) d.Position = 1;
            }
            if (e.Property.Is(PositionProperty))
            {
                // Si la posición se intenta establecer en un valor superior al máximo...
                if ((int)e.NewValue > max) throw new ArgumentOutOfRangeException(nameof(Position));
            }
            DisableControls(d.btnFirst, d.btnPrev, d.btnNext, d.btnLast, d.txtPos);
            d.btnEdit.Visibility = max > 0 ? Visibility.Visible : Visibility.Collapsed;
            if (max > 1) d.txtPos.IsEnabled = true;
            if (pos > 1) EnableControls(d.btnFirst, d.btnPrev);
            if (pos < max) EnableControls(d.btnNext, d.btnLast);
        }
        void GotoNormalMode()
        {
            ShowControls(lblTot, txtPos);
            if (Max > 0) btnEdit.Visibility = Visibility.Visible;
            SetValue(IsEditingProperty, false);
            lblInfo.Text = St.Of;
            ctrls.DisableControls();
            wasNewPressed = false;
        }
        void GotoNavigationBarEditMode()
        {
            btnEdit.Visibility = Visibility.Collapsed;
            SetValue(IsEditingProperty, true);
            lblInfo.Text = St.Of;
            ctrls.EnableControls();
        }
        string GenFilters(string s)
        {
            var x = new StringBuilder();
            foreach (var j in flts)
            {
                if (!x.ToString().IsEmpty()) x.Append(" OR ");
                x.Append($"{j} LIKE '%{s}%'");
            }
            return x.ToString();
        }
        #endregion

        #region Botones
        void CnclSrch(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
        }
        void BtnCncl_Click(object sender, RoutedEventArgs e)
        {
            var ev = new CancelEventArgs();
            Cancelling?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                if ((bool)view?.IsAddingNew) view.CancelNew();
                else if ((bool)view?.IsEditingItem) view.CancelEdit();
                GotoNormalMode();
                Cancelled?.Invoke(this, EventArgs.Empty);
            }
        }
        void First(object sender, RoutedEventArgs e)
        {
            var ev = new DependencyPropertyChangingEventArgs(PositionProperty, Position, 1);
            MovingToFirst?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                Position = 1;
                view?.MoveCurrentToFirst();
                MovedToFirst?.Invoke(this, ev);
            }
        }
        void Prev(object sender, RoutedEventArgs e)
        {
            var cp = Position;
            var ev = new DependencyPropertyChangingEventArgs(PositionProperty, cp, cp - 1);
            MovingToPrev?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                Position = cp - 1;
                view?.MoveCurrentToPrevious();
                MovedToPrev?.Invoke(this, ev);
            }
        }
        void Nxt(object sender, RoutedEventArgs e)
        {
            var cp = Position;
            var ev = new DependencyPropertyChangingEventArgs(PositionProperty, cp, cp + 1);
            MovingToNext?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                Position = cp + 1;
                view?.MoveCurrentToNext();
                MovedToNext?.Invoke(this, ev);
            }
        }
        void Last(object sender, RoutedEventArgs e)
        {
            var cp = Max;
            var ev = new DependencyPropertyChangingEventArgs(PositionProperty, Position, cp);
            MovingToLast?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                Position = cp < 1 ? 1 : cp;
                view?.MoveCurrentToLast();
                MovedToLast?.Invoke(this, ev);
            }
        }
        void TxtPos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out var v)) return;
            e.Handled = true;
            if (v.IsBetween(1, Max) && char.IsDigit(e.Text.Last()) && e.Text.Last() != ' ')
            {
                var ev = new DependencyPropertyChangingEventArgs(PositionProperty, Position, v);
                MovingToPosition?.Invoke(this, ev);
                if (!ev.Cancel)
                {
                    Position = v;
                    view?.MoveCurrentToPosition(v);
                    MovedToPosition?.Invoke(this, ev);
                }
                txtPos.SelectAll();
            }
        }
        void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.IsEmpty())
            {
                if (view != null)
                {
                    if (view.CanCustomFilter) view.CustomFilter = string.Empty;
                    view.Refresh();
                    view.MoveCurrentToFirst();
                    Position = 1;
                    Max = view.Count;
                }
                SearchClosed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (view != null && flts.Count > 0)
                {
                    if (view.CanCustomFilter) view.CustomFilter = GenFilters(txtSearch.Text);
                    view.Refresh();
                    view.MoveCurrentToFirst();
                    Position = 1;
                    Max = view.Count;
                }
                SearchEntered?.Invoke(this, new ValueEventArgs<string>(txtSearch.Text));
            }
        }
        void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            var ev = new CancelEventArgs();
            CreatingNew?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                wasNewPressed = true;
                view?.AddNew();
                CollapseControls(txtPos, lblTot);
                lblInfo.Text = St.NewReg;
                GotoNavigationBarEditMode();
                NewCreated?.Invoke(this, EventArgs.Empty);
            }
        }
        void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ev = new CancelEventArgs();
            Editing?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                view?.EditItem(view.CurrentItem);
                GotoNavigationBarEditMode();
                EditEntered?.Invoke(this, EventArgs.Empty);
            }
        }
        void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var ev = new CancelEventArgs();
            Deleting?.Invoke(this, ev);
            if (view != null && !ev.Cancel)
            {
                view.Remove(view.CurrentItem);
                if (Position >= Max && Max > 1)
                {
                    Position = Max - 1;
                    view.MoveCurrentToLast();
                }
                else if (Position < view.Count)
                    view.MoveCurrentToPosition(Position);
                Max -= 1;
            }
            ItemDeleted?.Invoke(this, EventArgs.Empty);
        }
        void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var ev = new ItemCreatingEventArgs<object>(view.CurrentAddItem, wasNewPressed);
            Saving?.Invoke(this, ev);
            if (!ev.Cancel)
            {
                if (view != null)
                {
                    if (view.IsAddingNew)
                    {
                        var i = view.CurrentAddItem;
                        view.CommitNew();
                        view.MoveCurrentTo(i);
                        Max += 1;
                        Position = view.IndexOf(i) + 1;
                    }
                    else if (view.IsEditingItem)
                    {
                        view.CommitEdit();
                    }
                }
                GotoNormalMode();
                ItemSaved?.Invoke(this, ev);
            }
        }
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Conecta un <see cref="BindingListCollectionView"/> para ser
        /// controlado automáticamente por este control.
        /// </summary>
        /// <param name="cv">
        /// <see cref="BindingListCollectionView"/> a controlar.
        /// </param>
        /// <param name="searchFields">
        /// Si <see cref="HasSearch"/> es <see langword="true"/>, establece los campos
        /// necesarios para realizar búsquedas.
        /// </param>
        public void AttachView(BindingListCollectionView cv, string[] searchFields = null)
        {
            if (!(view is null)) flts.Clear();
            if (HasSearch && !(searchFields is null)) flts.AddRange(searchFields);
            view = cv ?? throw new ArgumentNullException(nameof(cv));
            Max = view.Count;
            view.MoveCurrentToFirst();
            Position = 1;
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
            Max = 0;
            Position = 1;
            txtSearch.Clear();
        }
        /// <summary>
        /// Agrega una colección de controles para que su estado sea
        /// administrado automáticamente por este control.
        /// </summary>
        /// <param name="C">Colección de controles a administrar.</param>
        public void AttachControls(params UIElement[] C)
        {
            if (C is null || !C.Any()) throw new ArgumentNullException();
            if (btnCncl.IsVisible) throw new InvalidOperationException();
            ctrls.Clear();
            ctrls.AddRange(C);
            ctrls.DisableControls();
        }
        /// <summary>
        /// Libera a los controles administrados por este control
        /// </summary>
        public void DetachControls()
        {
            if (btnCncl.IsVisible) throw new InvalidOperationException();
            ctrls.EnableControls();
            ctrls.Clear();
        }
        /// <summary>
        /// Inicializa una nueva instancia de este control
        /// </summary>
        public NavigationBar()
        {
            MinWidth = 96;
            var roth = new StackPanel { MinWidth = 96 };
            var grdedit = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Center };
            var b = new WrapPanel();
            SetBinding(TextBox.TextProperty, new Binding(nameof(txtSearch.Text)) { Source = txtSearch });
            SetBinding(HasItemsProperty, new Binding(nameof(Max))
            {
                Source = this,
                Converter = new NumberToBooleanConverter()
            });
            var c = new Grid
            {
                Margin = new Thickness(5, 0, 0, 0),
                Height = 20
            };
            c.SetBinding(VisibilityProperty, new Binding(nameof(HasSearch))
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            });
            var d = new TextBlock
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
                Converter = new StringVisibilityConverter()
            });
            btnClseSearch.SetBinding(VisibilityProperty, new Binding(nameof(Search))
            {
                Source = this,
                Converter = new StringVisibilityConverter(true)
            });
            txtPos.SetBinding(TextBox.TextProperty, new Binding(nameof(Position))
            {
                Source = this,
                Converter = new ToStringConverter()
            });
            lblTot.SetBinding(TextBlock.TextProperty, new Binding(nameof(Max))
            {
                Source = this,
                Converter = new ToStringConverter()
            });
            btnNew.SetBinding(IsEnabledProperty, new Binding(nameof(Mode))
            {
                Source = this,
                Converter = new Editvalconv2(NavigationBarEditMode.Newable)
            });
            btnEdit.SetBinding(IsEnabledProperty, new Binding(nameof(Mode))
            {
                Source = this,
                Converter = new Editvalconv2(NavigationBarEditMode.Editable)
            });
            btnDel.SetBinding(IsEnabledProperty, new Binding(nameof(Mode))
            {
                Source = this,
                Converter = new Editvalconv2(NavigationBarEditMode.Deletable)
            });
            btnNew.SetBinding(WidthProperty, new Binding(nameof(ButtonWidth)) { Source = this });
            btnEdit.SetBinding(WidthProperty, new Binding(nameof(ButtonWidth)) { Source = this });
            btnDel.SetBinding(WidthProperty, new Binding(nameof(ButtonWidth)) { Source = this });
            btnSave.SetBinding(WidthProperty, new Binding(nameof(ButtonWidth)) { Source = this });
            btnCncl.SetBinding(WidthProperty, new Binding(nameof(ButtonWidth)) { Source = this });
            btnNew.SetBinding(VisibilityProperty, new Binding(nameof(IsEditing))
            {
                Source = this,
                Converter = new BooleanToInvVisibilityConverter()
            });
            btnDel.SetBinding(VisibilityProperty, new Binding(nameof(btnEdit.Visibility)) { Source = btnEdit });
            btnSave.SetBinding(VisibilityProperty, new Binding(nameof(IsEditing))
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            });
            btnCncl.SetBinding(VisibilityProperty, new Binding(nameof(IsEditing))
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            });
            pnlNav.SetBinding(IsEnabledProperty, new Binding(nameof(IsEditing))
            {
                Source = this,
                Converter = new BooleanConverter<bool>(false, true)
            });
            grdedit.SetBinding(VisibilityProperty, new Binding(nameof(Mode))
            {
                Source = this,
                Converter = new Editvalconv(NavigationBarEditMode.All)
            });
            DockPanel.SetDock(b, Dock.Left);
            b.Children.Add(btnFirst);
            b.Children.Add(btnPrev);
            pnlNav.Children.Add(b);
            b = new WrapPanel { Height = 20 };
            DockPanel.SetDock(b, Dock.Right);
            b.Children.Add(btnNext);
            b.Children.Add(btnLast);
            var a = new Rectangle();
            a.SetBinding(Shape.FillProperty, new Binding(nameof(Background)) { Source = txtPos });
            c.Children.Add(a);
            c.Children.Add(d);
            c.Children.Add(txtSearch);
            b.Children.Add(c);
            b.Children.Add(btnClseSearch);
            pnlNav.Children.Add(b);
            b = new WrapPanel
            {
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            b.SetBinding(VisibilityProperty, new Binding(nameof(HasItems))
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            });
            b.Children.Add(txtPos);
            b.Children.Add(lblInfo);
            b.Children.Add(lblTot);
            pnlNav.Children.Add(b);
            var _with1 = grdedit.Children;
            _with1.Add(btnNew);
            _with1.Add(btnEdit);
            _with1.Add(btnDel);
            _with1.Add(btnSave);
            _with1.Add(btnCncl);
            roth.Children.Add(pnlNav);
            roth.Children.Add(grdedit);
            Content = roth;
            UpdtLayout(this, new DependencyPropertyChangedEventArgs());
            btnClseSearch.Click += CnclSrch;
            btnCncl.Click += BtnCncl_Click;
            btnFirst.Click += First;
            btnPrev.Click += Prev;
            btnLast.Click += Last;
            txtPos.PreviewTextInput += TxtPos_PreviewTextInput;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDel.Click += BtnDel_Click;
            btnSave.Click += BtnSave_Click;
        }
        #endregion
    }
}