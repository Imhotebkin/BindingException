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
        ObservableCollection<Point> data;
        private int counter;
        private bool plus;
        private double a;
        private double b;
        private int i;
        private Thread thread;

        public object obj; 

        public MainWindowView()
        {           
            InitializeComponent();
            obj = new object();
            
            //BindingOperations.EnableCollectionSynchronization(data, obj);
            Loaded += new RoutedEventHandler(Window1_Loaded);
            plotter.Viewport.FitToViewConstraints.Add(new FollowWidthConstraint(100));
            //dependentPlotter.Viewport.FitToViewConstraints.Add(new FollowWidthConstraint(100));
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Chart.StrokeThickness = 3;
            //data = new ObservableCollection<Point>();
            Chart.ItemsSource = data;
            thread = new Thread(new ThreadStart(gogogo));
            thread.IsBackground = true;
            thread.Start();

            //var rpms = Enumerable.Range(0, 9).Select(i => i * 1000.0);
            //var hps = new double[] { 0, 24, 52, 74, 98, 112, 124, 122, 116 };

            //var horsePowersDS = DataSource.Create(rpms, hps);
            //// shown at main ChartPlotter
            //var hpsLine = plotter.AddLineChart(horsePowersDS).
            //    WithStroke(Brushes.Red).
            //    WithStrokeThickness(2).
            //    WithDescription("HP per RPM");

            //var torque = new double[] { 0, 22, 45, 54, 58, 55, 50, 47, 45 };
            //var torqueDS = DataSource.Create(rpms, torque);

            //// shown at inner InjectedPlotter
            //var line = innerPlotter.AddLineChart(torqueDS).
            //    WithStroke(Brushes.Blue).
            //    WithStrokeThickness(2).
            //    WithDescription("Torque per RPM");

            //var values = new double[] { 10, 9, 7, 8, 5, 6, 4, 3, 2, 1 };
            //var valuesDS = DataSource.Create(rpms, values);

            //// shown at inner DependentPlotter
            //dependentPlotter.AddLineChart(valuesDS).
            //    WithStroke(Brushes.LawnGreen).
            //    WithStrokeThickness(2).
            //    WithDescription("Some fake values");
        }
        public void gogogo()
        {
            //Thread.Sleep(5000);
            //data = new ObservableCollection<Point>();
            //data.Add(new Point(1, 1));
            //data.Add(new Point(2, 2));
            //data.Add(new Point(3, 3));
            //data.Add(new Point(5, 5));
            counter = 0;
            while (true)
            {
                if (i > 50)
                    plus = false;
                if (i < -50)
                    plus = true;
                i = i + (plus ? 1 : -1);
                a += 0.3;
                b += i;
                //data.Add(new Point(a, -b));
                counter++;
                if (counter < 350)
                {
                    Thread.Sleep(30);
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Chart.StrokeThickness = 3;
            //ChartTwo.StrokeThickness = 3;
            //ChartThree.StrokeThickness = 3;
        }
    }
}
