using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using NUnit.Framework;

namespace TheXDS.MCART.UI.Tests.ViewModel
{
    public class ObservingCommandTests
    {
        private class TestNpcClass : NotifyPropertyChanged
        {
            private string? _TestString;

            public string? TestString
            {
                get => _TestString;
                set => Change(ref _TestString, value);
            }
        }

        [Test]
        public void PropertyChange_Fires_Notification_Test()
        {
            TestNpcClass? i = new();
            ObservingCommand? obs = new ObservingCommand(i, NoAction)
                .SetCanExecute((a, b) => !i.TestString.IsEmpty())
                .RegisterObservedProperty(nameof(TestNpcClass.TestString));

            Assert.False(obs.CanExecute(null));
            i.TestString = "Test";
            Assert.True(obs.CanExecute(null));
        }
        
        [ExcludeFromCodeCoverage]
        private static void NoAction() { }
    }
}
