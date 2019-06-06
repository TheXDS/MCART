/*
MainWindow.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace VmBuilder
{
    public partial class MainWindow : Window
    {
        private TestViewModel Vm => DataContext as TestViewModel;
        private readonly TestContext _db = new TestContext();
        public MainWindow()
        {
            //InitializeComponent();
            //_db.Database.CreateIfNotExists();
            DataContext = ViewModelFactory.BuildViewModel(typeof(TestViewModel),typeof(Test)).New();
            lst1.ItemsSource = _db.Tests.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Vm.Entity.Id == 0) _db.Tests.Add(Vm.Entity);
            _db.SaveChanges();
            lst1.ItemsSource = null;
            lst1.ItemsSource = _db.Tests.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => Vm.Entity = new Test();

        private void Lst1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Vm.Entity = lst1.SelectedItem as Test;
        }
    }

    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Number> Elements { get; } = new Collection<Number>();
    }
    public class Number
    {
        [Key]
        public int Id { get; set; }
        public string Phone { get; set; }
    }

    public abstract class TestViewModel : NotifyPropertyChanged, IDynamicViewModel<Test>
    {
        public void Testtest()
        {
            var o = new object();
            lock (Entity ?? o)
            {
                Entity.Elements.Clear();
                Entity.Elements.Clear();
            }
        }
        private string _newPhone;

        object IDynamicViewModel.Entity
        {
            get => Entity;
            set => Entity = value as Test;
        }
        public TestViewModel()
        {
            RegisterPropertyChangeBroadcast(nameof(Test.Elements), nameof(RealCount));
            RegisterPropertyChangeBroadcast(nameof(Test.Id), nameof(IdLength));

            AddId = new SimpleCommand(OnAdd);
            RemId = new SimpleCommand(OnRemove);
        }
        private void OnAdd()
        {
            Entity.Elements?.Add(new Number { Phone = NewPhone });
            NewPhone = null;
            Notify(nameof(Entity.Elements));
        }
        private void OnRemove()
        {
            Entity.Elements.RemoveAll(p => p.Phone == NewPhone);
            Notify(nameof(Entity.Elements));
        }        

        public int RealCount => Entity.Elements.Count;
        public int IdLength => Entity.Id.ToString().Length;
        public ICommand AddId { get; }
        public ICommand RemId { get; }
        public string NewPhone
        {
            get => _newPhone;
            set => Change(ref _newPhone, value);
        }
        public abstract Test Entity { get; set; }
        public abstract void Edit(Test entity);
    }
    public class TestContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
    }


    public class TestViewModel_8e106e3fc1a64e369f50b019eef60abf : TestViewModel, IDynamicViewModel<Test>
    {
        public TestViewModel_8e106e3fc1a64e369f50b019eef60abf()
        {
            _elements = new ObservableCollectionWrap<Number>();
        }

        public override Test Entity
        {
            get
            {
                return _entity;
            }
            set
            {
                if (Change(ref _entity, value))
                {
                    _elements.Substitute(Entity.Elements);
                    Refresh();
                }
            }
        }

        public override void Edit(Test A_1)
        {
            Email = A_1.Email;
            _elements.Replace(A_1.Elements);
        }

        public override void Refresh()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("Name");
            OnPropertyChanged("LastName");
            OnPropertyChanged("Email");
            OnPropertyChanged("Elements");
        }

        public string Email
        {
            get
            {
                return Entity?.Email;
            }
            set
            {   
                if (Entity != null && !Equals(Entity.Email, value))
                {
                    Entity.Email = value;
                    base.OnPropertyChanged("Email");
                }
            }
        }

        public ICollection<Number> Elements => _elements;

        private Test _entity;
        private readonly ObservableCollectionWrap<Number> _elements;
    }
}