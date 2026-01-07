/*
ValidationSource_Tests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class ValidationSource_Tests
{
    [ExcludeFromCodeCoverage]
    private class TestViewModel : NotifyPropertyChanged, IValidatingViewModel
    {
        private string? _testString;
        private readonly ValidationSource<TestViewModel> _errorSource;
        public string? TestString
        {
            get => _testString;
            set => Change(ref _testString, value);
        }

        public string? AnotherProperty => _testString;
        
        public ValidationSource ErrorSource => _errorSource;

        public ValidationSource<TestViewModel> TestSource => _errorSource;

        public TestViewModel()
        {
            _errorSource = new ValidationSource<TestViewModel>(this);
        }
    }

    [TestCase("", true)]
    [TestCase(null, true)]
    [TestCase("Test", false)]
    public void Simple_validation_test(string? testString, bool hasErrors)
    {
        var vm = new TestViewModel();
        vm.TestSource.RegisterValidation(p => p.TestString)
            .AddRule(p => !p.IsEmpty(), "EmptyTest");
        vm.TestString = testString;
        Assert.That(vm.ErrorSource.CheckErrors(), Is.Not.EqualTo(hasErrors));
        Assert.That(vm.ErrorSource.HasErrors, Is.EqualTo(hasErrors));
        Assert.That(vm.ErrorSource.PassesValidation, Is.Not.EqualTo(hasErrors));
    }

    [TestCase("", true)]
    [TestCase(null, true)]
    [TestCase("Test1234", true)]
    [TestCase("Test", true)]
    [TestCase("Test  McTest", true)]
    [TestCase("Test McTest", false)]
    public void Complex_validation_test(string? testString, bool hasErrors)
    {
        var vm = new TestViewModel();
        vm.TestSource.RegisterValidation(p => p.TestString)
            .NotEmpty("EmptyTest")
            .AddRule(p => !p.ContainsNumbers(), "NoDigits")
            .AddRule(p => p.Split().Length.IsBetween(2, 4), "NotFullName")
            .AddRule(p => p.Split().All(q => !q.IsEmpty()), "PartEmpty");
        vm.TestString = testString;
        Assert.That(vm.ErrorSource.CheckErrors(), Is.Not.EqualTo(hasErrors));
        Assert.That(vm.ErrorSource.HasErrors, Is.EqualTo(hasErrors));
        Assert.That(vm.ErrorSource.PassesValidation, Is.Not.EqualTo(hasErrors));
    }

    [TestCase(null, "EmptyTest")]
    [TestCase("", "EmptyTest")]
    [TestCase("Test1234", "NoDigits;NotFullName")]
    [TestCase("Test", "NotFullName")]
    [TestCase("Test  McTest", "PartEmpty")]
    public void GetErrors_with_property_selector_test(string? testString, string errors)
    { 
        var vm = new TestViewModel();
        vm.TestSource.RegisterValidation(p => p.TestString)
            .NotEmpty("EmptyTest")
            .AddRule(p => !p.ContainsNumbers(), "NoDigits")
            .AddRule(p => p.Split().Length.IsBetween(2, 4), "NotFullName")
            .AddRule(p => p.Split().All(q => !q.IsEmpty()), "PartEmpty");
        
        vm.TestString = testString;
        vm.ErrorSource.CheckErrors();
        Assert.That(vm.TestSource.GetErrors(p => p.TestString), Is.EquivalentTo(errors.Split(';')));
    }

    [TestCase(null, "EmptyTest")]
    [TestCase("", "EmptyTest")]
    [TestCase("Test1234", "NoDigits;NotFullName")]
    [TestCase("Test", "NotFullName")]
    [TestCase("Test  McTest", "PartEmpty")]
    public void GetErrors_with_property_name_test(string? testString, string errors)
    { 
        var vm = new TestViewModel();
        vm.TestSource.RegisterValidation(p => p.TestString)
            .NotEmpty("EmptyTest")
            .AddRule(p => !p.ContainsNumbers(), "NoDigits")
            .AddRule(p => p.Split().Length.IsBetween(2, 4), "NotFullName")
            .AddRule(p => p.Split().All(q => !q.IsEmpty()), "PartEmpty");
        
        vm.TestString = testString;
        vm.ErrorSource.CheckErrors();
        Assert.That(vm.TestSource.GetErrors(nameof(TestViewModel.TestString)), Is.EquivalentTo(errors.Split(';')));
    }
    
    [Test]
    public void GetErrors_without_args_test()
    { 
        var vm = new TestViewModel();
        vm.TestSource.RegisterValidation(p => p.TestString)
            .NotEmpty("EmptyTest");
        vm.TestSource.RegisterValidation(p => p.AnotherProperty)
            .AddRule(_ => false, "AnotherTest");
        vm.ErrorSource.CheckErrors();
        Assert.That(vm.TestSource.GetErrors(), Is.EquivalentTo(new[] { "EmptyTest", "AnotherTest" }));
    }
    
    [Test]
    public void Indexer_test()
    {
        var vm = new TestViewModel();
        Assert.That(vm.ErrorSource.CheckErrors(), Is.True);
        Assert.That(vm.ErrorSource[nameof(TestViewModel.TestString)], Is.EquivalentTo(Array.Empty<string>()));
        vm.TestSource.RegisterValidation(p => p.TestString).NotEmpty("EmptyTest");
        Assert.That(vm.ErrorSource.CheckErrors(), Is.False);
        Assert.That(vm.ErrorSource[nameof(TestViewModel.TestString)], Is.EquivalentTo(new[] { "EmptyTest" }));
    }
}
