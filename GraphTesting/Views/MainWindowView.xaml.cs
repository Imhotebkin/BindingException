using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using GraphTesting.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.NewLine;
using Microsoft.Research.DynamicDataDisplay.Markers2;
using Microsoft.Research.DynamicDataDisplay.ViewportConstraints;

namespace GraphTesting.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {

        public MainWindowView()
        {           
            InitializeComponent();
            Loaded += new RoutedEventHandler(Window_Loaded);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Chart.StrokeThickness = 3;
            Chart.SetBinding(LineChart.ItemsSourceProperty, new Binding("ExampleCollection")); //doesn't work
            Listbox.SetBinding(ListBox.ItemsSourceProperty, new Binding("ExampleCollection")); //works
        }
// Looking at the code of the base class for the LineChart (https://d3future.codeplex.com/SourceControl/latest#Main/src/DynamicDataDisplay.Markers2/PointChartBase.cs)
// the ItemsSourceProperty is just a regular Dependency property and should work like everything else
// Even though I turned all exceptions to break I still can't manage to catch it. Probably because it happens inside a dll.
// When I made the question I saw the exception when I was looking at the autos in the LineChasrt.base.base.ItemsSource. In the value was the exception. 
// I saw it when I breaked in the Window_Loaded. But for some reason I can not reproduce that now:/
    }
}
