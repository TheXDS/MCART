/*
ViewModelFactoryTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

using NUnit.Framework;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Tests;

public class ViewModelFactoryTests
{
    [ExcludeFromCodeCoverage]
    public class TestModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    private static readonly TypeFactory _factory = new($"{typeof(ViewModelFactoryTests).FullName}._Generated");

    [Test]
    public void ViewModel_fabrication()
    {
        (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
        void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) => evt = (sender, e);

        var t = _factory.CreateViewModelClass<TestModel>();
        var instance = t.New();
        Assert.IsNotNull(instance);
        TestModel m = new();
        instance.PropertyChanged += OnPropertyChanged;

        instance.Entity = m;
        Assert.AreSame(m, instance.Entity);
        Assert.AreEqual(nameof(ViewModel<TestModel>.Entity), evt!.Value.Arguments.PropertyName);

        ((dynamic)instance).Name = "Test";
        Assert.AreEqual("Name", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", ((dynamic)instance).Name);
        Assert.AreEqual("Test", instance.Entity.Name);

        ((dynamic)instance).Description = "Test";
        Assert.AreEqual("Description", evt!.Value.Arguments.PropertyName);
        Assert.AreEqual("Test", ((dynamic)instance).Description);
        Assert.AreEqual("Test", instance.Entity.Description);

        instance.PropertyChanged -= OnPropertyChanged;
    }
}