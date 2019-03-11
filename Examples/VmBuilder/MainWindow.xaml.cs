/*
MainWindow.cs

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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
            InitializeComponent();
            _db.Database.CreateIfNotExists();
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
            try
            {
                Entity.Elements.Clear();
            }
            catch
            {
                /* nada */
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
}