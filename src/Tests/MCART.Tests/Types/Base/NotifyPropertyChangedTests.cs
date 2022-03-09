/*
NotifyPropertyChangedTests.cs

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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TheXDS.MCART.Types.Base;
using NUnit.Framework;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Tests.Types.Base
{
    public class NotifyPropertyChangedTests : NotifyPropertyChanged
    {
        private int _value;
        private object? _obj;

        [ExcludeFromCodeCoverage]
        public int Value
        {
            get => _value;
            set => Change(ref _value, value);
        }
        
        [ExcludeFromCodeCoverage]
        public object? Obj
        {
            get => _obj;
            set => Change(ref _obj, value);
        }
        
        [ExcludeFromCodeCoverage]
        public int BrokenProperty
        {
            //get => _value;
            set => Change(ref _value, value, null!);
        }
        [ExcludeFromCodeCoverage]
        public int BrokenProperty2
        {
            //get => _value;
            set => Change(ref _value, value, string.Empty);
        }

        [ExcludeFromCodeCoverage]
        public object? SelfFalseTestingProperty
        {
            //get => _obj;
            set => Assert.False(Change(ref _obj, value));
        }

        [ExcludeFromCodeCoverage]
        public object? SelfTrueTestingProperty
        {
            //get => _obj;
            set => Assert.True(Change(ref _obj, value));
        }
        
        [ExcludeFromCodeCoverage]
        public int Prop1 { get; set; }
        [ExcludeFromCodeCoverage]
        public int Prop2 { get; set; }
        [ExcludeFromCodeCoverage]
        public int Prop3 { get; set; }

        [Test]
        public void RegisterPropertyChangeBroadcast_Contract_Test()
        {
            Assert.AreEqual("property", Assert.Throws<ArgumentNullException>(() =>
            {
                RegisterPropertyChangeBroadcast(null!, "Prop1");
            })?.ParamName);
            Assert.AreEqual("property", Assert.Throws<ArgumentException>(() =>
            {
                RegisterPropertyChangeBroadcast(string.Empty, "Prop1");
            })?.ParamName);
            Assert.AreEqual("property", Assert.Throws<ArgumentException>(() =>
            {
                RegisterPropertyChangeBroadcast(" ", "Prop1");
            })?.ParamName);
            Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentNullException>(() =>
            {
                RegisterPropertyChangeBroadcast("Prop1", null!);
            })?.ParamName);

            string[] arr = Array.Empty<string>();
            Assert.AreSame(arr, ((EmptyCollectionException?)Assert.Throws<InvalidOperationException>(() =>
            {
                RegisterPropertyChangeBroadcast("Prop1", arr);
            })?.InnerException)?.OffendingObject);

            Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(() =>
            {
                RegisterPropertyChangeBroadcast("Prop1", string.Empty);
            })?.ParamName);
            Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(() =>
            {
                RegisterPropertyChangeBroadcast("Prop1", (string)null!);
            })?.ParamName);
            Assert.AreEqual("affectedProperties", Assert.Throws<ArgumentException>(() =>
            {
                RegisterPropertyChangeBroadcast("Prop1", " ");
            })?.ParamName);
            
            RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop2));
            RegisterPropertyChangeBroadcast(nameof(Prop1), "Prop4");
            RegisterPropertyChangeBroadcast(nameof(Prop2), nameof(Prop3));
            Assert.Throws<InvalidOperationException>(() =>
                RegisterPropertyChangeBroadcast(nameof(Prop3), nameof(Prop1)));
        }

        [Test]
        public void ObserveTree_Test()
        {
            Assert.False(ObserveTree.ContainsKey(nameof(Prop1)));
            Assert.IsAssignableFrom<ReadOnlyDictionary<string, ICollection<string>>>(ObserveTree);
            RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop2));
            RegisterPropertyChangeBroadcast(nameof(Prop1), nameof(Prop3));
            Assert.True(ObserveTree.ContainsKey(nameof(Prop1)));
            Assert.AreEqual(new[]{nameof(Prop2), nameof(Prop3)}, ObserveTree[nameof(Prop1)].ToArray());
        }
        
        [Test]
        public void OnPropertyChangedTest()
        {
            bool risen = false;
            (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;

            void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                risen = true;
                evt = (sender, e);
            }

            PropertyChanged += TestPropertyChanged;
            Value = 1;
            PropertyChanged -= TestPropertyChanged;

            Assert.True(risen);
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(this, evt!.Value.Sender));
            Assert.AreEqual(nameof(Value), evt!.Value.Arguments.PropertyName);
            Assert.AreEqual(1, Value);
        }

        [Test]
        public void Property_Change_Forward_Test()
        {
            NotifyPropertyChangedTests source = new();
            NotifyPropertyChangedTests other = new();
            bool risen = false;
            (object? Sender, PropertyChangedEventArgs Arguments)? evt = null;
            void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                risen = true;
                evt = (sender, e);
            }
            source.ForwardChange(other);
            other.PropertyChanged += TestPropertyChanged;
            source.Value = 1;
            other.PropertyChanged -= TestPropertyChanged;
            Assert.True(risen);
            Assert.NotNull(evt);
            Assert.True(ReferenceEquals(other, evt!.Value.Sender));
            source.RemoveForwardChange(other);
        }

        [Test]
        public void NotifyRegistroir_Test()
        {
            bool risen = false;
            void TestPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(Obj)) risen = true;
            }
            RegisterPropertyChangeBroadcast(
                nameof(Value),
                nameof(Obj));
            PropertyChanged += TestPropertyChanged;
            Value = 2;
            UnregisterPropertyChangeBroadcast(nameof(Value));
            Assert.IsTrue(risen);
            PropertyChanged -= TestPropertyChanged;
        }

        [Test]
        public void Change_Contract_Test()
        {
            Assert.Throws<ArgumentNullException>(() => BrokenProperty = 1);
            Assert.Throws<ArgumentNullException>(() => BrokenProperty2 = 1);
            SelfFalseTestingProperty = null;
            SelfTrueTestingProperty = 33;
            SelfFalseTestingProperty = 33;
        }
    }
}
