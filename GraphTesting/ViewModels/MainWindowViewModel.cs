using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using Caliburn.Micro;
using Microsoft.Research.DynamicDataDisplay.Charts.NewLine;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace GraphTesting.ViewModels
{
    [Export(typeof(MainWindowViewModel))]
    public class MainWindowViewModel : PropertyChangedBase
    {
        public ObservableDataSource<Point> s { get; set; }
        public MTObservableCollection<Point> ExampleCollection { get; set; }
        public ObservableDataSource<Point> ss { get; set; }
        Thread thread;
        double a, b;
        int i;
        bool plus;
        Dispatcher disp;
        private int counter;

        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator ev)
        {
            Adding = true;

            a = 5;
            b = 50;

            disp = Dispatcher.CurrentDispatcher;

            ExampleCollection = new MTObservableCollection<Point>();
            ExampleCollection.Add(new Point(1, 1));
            ExampleCollection.Add(new Point(2, 2));
            ExampleCollection.Add(new Point(3, 3));
            ExampleCollection.Add(new Point(5, 5));

            ss = new ObservableDataSource<Point>();
            ss.AppendAsync(disp, new Point(1, 1));
            ss.AppendAsync(disp, new Point(2, 2));
            ss.AppendAsync(disp, new Point(3, 3));
            ss.AppendAsync(disp, new Point(5, 5));

            thread = new Thread(new ThreadStart(AddPoints));
            thread.IsBackground = true;
            thread.Start();
        }

        //The exception is not in the ViewModel code. I found it by looking at ItemsSource property of the chart from the UI thread autos

        void AddPoints()  // Adds new points to the collection
        {
            counter = 0;
            while (true)
            {
                if (Adding)
                {
                    if (i > 50)
                        plus = false;
                    if (i < -50)
                        plus = true;
                    i = i + (plus ? 1 : -1);
                    a+=0.3 ;
                    b += i;
                    ExampleCollection.Add(new Point(a, -b));
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
        }

        public bool Adding { get; set; }
    }
}
