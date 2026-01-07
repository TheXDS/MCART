/*
EntityViewModelTests.cs

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

using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Mvvm.Tests.Types.Base;

public class FormViewModelBaseTests
{
    private class TestFormViewModel : FormViewModelBase
    {
        private string? _StringProperty;

        public string? StringProperty
        {
            get => _StringProperty;
            set => Change(ref _StringProperty, value);
        }

        public SimpleCommand TestCommand { get; }

        private void OnTestCommand()
        {
        }

        public TestFormViewModel()
        {
            TestCommand = new SimpleCommand(OnTestCommand);
            RegisterValidation(() => StringProperty)
                .AddRule(p => !p.IsEmpty(), "Test error");

            ValidationAffects(TestCommand);
        }
    }

    [TestCase(null, true)]
    [TestCase("", true)]
    [TestCase("Test", false)]
    public void HasErrors_test(string? value, bool expected)
    {
        var vm = new TestFormViewModel
        {
            StringProperty = value
        };
        vm.CheckErrors();
        Assert.That(vm.HasErrors, Is.EqualTo(expected));
    }

    [Test]
    public void Validation_errors_disable_SimpleCommands_test()
    {
        var vm = new TestFormViewModel();
        vm.CheckErrors();
        Assert.That(vm.TestCommand.CanExecute(null), Is.False);
    }
}
