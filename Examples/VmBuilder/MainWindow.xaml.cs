/*
TypeFactory.cs

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
            DataContext = TypeFactory.NewViewModel<TestViewModel, ITest>();
            lst1.ItemsSource = _db.Tests.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Vm.Self.Id == 0) _db.Tests.Add(Vm.Entity as Test);
            _db.SaveChanges();
            lst1.ItemsSource = null;
            lst1.ItemsSource = _db.Tests.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => Vm.Entity = new Test();

        private void Lst1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) => Vm.Entity = lst1.SelectedItem as ITest;
    }

    public class TestContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
    }
    public interface ITest
    {
        [Key]
        int Id { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        ICollection<Number> Elements { get; }
    }
    public interface INumber
    {
        [Key]
        int Id { get; set; }
        string Phone { get; set; }
    }
    public class Number : INumber
    {
        public int Id { get; set; }
        public string Phone { get; set; }
    }
    public class Test : ITest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Number> Elements { get; } = new Collection<Number>();
    }

    public abstract class TestViewModel : NotifyPropertyChanged, IGeneratedViewModel<ITest>
    {
        private string _newPhone;

        public TestViewModel()
        {
            RegisterPropertyChangeBroadcast(nameof(ITest.Elements), nameof(RealCount));
            RegisterPropertyChangeBroadcast(nameof(ITest.Id), nameof(IdLength));

            var t = false;
            t = true;

            AddId = new SimpleCommand(() =>
            {
                Self.Elements?.Add(new Number { Phone = NewPhone });
                NewPhone = null;
            });
            RemId = new SimpleCommand(() => Self.Elements.RemoveAll(p=>p.Phone==NewPhone));
        }
        public int RealCount => Self.Elements.Count;
        public int IdLength => Self.Id.ToString().Length;
        public ICommand AddId { get; }
        public ICommand RemId { get; }
        public string NewPhone
        {
            get => _newPhone;
            set => Change(ref _newPhone, value);
        }
        public abstract ITest Self { get; }
        public abstract ITest Entity { get; set; }
        public abstract void Edit(ITest entity);
    }
}