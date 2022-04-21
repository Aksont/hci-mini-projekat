using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Wpf;

namespace HciMiniProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlotGraph();
        }

        private void PlotGraph()
        {
            SeriesCollection = new SeriesCollection();
            var lineSeries1 = new LineSeries
            {
                Title = "S1",
                Values = new ChartValues<double>() { 2.3, 2.0, 3.1, 1.3, 0.5, 3.8, 7.3, 2.4, 1.2, 0.1 },
                DataLabels = true,
                Stroke = Brushes.Green,
                Fill = Brushes.Transparent,
                ScalesYAt = 0
            };
            SeriesCollection.Add(lineSeries1);

           // MyChart.AxisY.Add(new Axis());
            //MyChart.AxisY.Add(new Axis());

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
