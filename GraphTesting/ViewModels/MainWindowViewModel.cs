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
        public ObservableCollection<Point> values { get; set; }
        //public EnumerableDataSource<Tuple<DateTime,int>> s { get; set; }
        public ObservableDataSource<Point> s { get; set; }
        public ObservableDataSource<Point> ss { get; set; }
        //public BindableCollection<Point> ss { get; set; }
        Thread thread;
        double a, b;
        int i;
        bool plus;
        Dispatcher disp;
        private int counter;
        private object obj;

        [ImportingConstructor]
        public MainWindowViewModel(IEventAggregator ev)
        {
            values = new ObservableCollection<Point>();
            values.Add(new Point(1,1));
            values.Add(new Point(2, 2));
            values.Add(new Point(3, 3));

            Adding = true;

            a = 5;
            b = 50;

            disp = Dispatcher.CurrentDispatcher;

            List<int> i = new List<int>();
            i.Add(1);
            i.Add(2);
            i.Add(3);

            s = new ObservableDataSource<Point>();

            ss = new ObservableDataSource<Point>();
            ss.AppendAsync(disp, new Point(1, 1));
            ss.AppendAsync(disp, new Point(2, 2));
            ss.AppendAsync(disp, new Point(3, 3));
            ss.AppendAsync(disp, new Point(5, 5));


            s.AppendAsync(disp, new Point(2, 1));
            s.AppendAsync(disp, new Point(3, 2));
            s.AppendAsync(disp, new Point(4, 3));
            s.AppendAsync(disp, new Point(6, 5));

            thread = new Thread(new ThreadStart(gogogo));
            thread.IsBackground = true;
            thread.Start();

            NotifyOfPropertyChange("s");
        }

        void gogogo()
        {
            //Thread.Sleep(5000);
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
                    s.AppendAsync(disp, new Point(a, b * 100));
                    ss.AppendAsync(disp, new Point(a, -b));
                    //ss.Add(new Point(a, -b));
                    counter++;
                    if (counter < 350)
                    {
                        Thread.Sleep(30);
                    }
                    else
                    {
                        Thread.Sleep(30);
                    }
                    NotifyOfPropertyChange("ss");
                }
            }
        }

        public bool Adding { get; set; }
    }
}
