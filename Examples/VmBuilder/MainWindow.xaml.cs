using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheXDS.MCART.ViewModel;

namespace VmBuilder
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var Vm = ViewModelBuilder<Test>.New<TestViewModel>();
            DataContext = Vm;
            ((INotifyPropertyChanged)DataContext).PropertyChanged += (sender, e) =>
            {
                MessageBox.Show(e.PropertyName);
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).Id=10;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).Elements.Add(((dynamic)DataContext).Id.ToString());
        }
    }

    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<string> Elements { get; } = new List<string>();
    }

    public class TestViewModel : ViewModel<Test>
    {
        public TestViewModel()
        {
            RegisterPropertyChangeBroadcast("Id","Name","LastName");
        }
    }



}
