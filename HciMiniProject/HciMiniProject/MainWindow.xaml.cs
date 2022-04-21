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
using HciMiniProject.API;
using LiveCharts;
using LiveCharts.Wpf;

namespace HciMiniProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            //PlotGraph("RealGDP", "annual", "billions of dollars", "");

            PlotGraph("TREASURY_YIELD", "monthly", "", "10year");
        }

        private List<DataDateValue> getData(string name, string interval, string unit, string maturity)
        {
            if (name == "RealGDP")
            {
                return API.API.GetRealGDPData(interval, unit);
            } else
            {
                Console.WriteLine(name);
                return API.API.GetTreasuryYieldData(interval, maturity);
            }
        }

        private void PlotGraph(string name, string interval, string unit, string maturity)
        {
            List<DataDateValue> data = getData(name, interval, unit, maturity);
            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(dataDateValue.date);
            }
            var lineSeries1 = new LineSeries
            {
                Title = name,
                Values = values,
                DataLabels = true,
                Stroke = Brushes.Yellow,
                Fill = Brushes.Transparent,
                ScalesYAt = 0
            };

            BarLabels = labels.ToArray();
            //SeriesCollection.Clear();
            SeriesCollection.Clear();
            SeriesCollection.Add(lineSeries1);

            Formatter = value => value.ToString("N");

            MyChart.AxisX.Clear();
            MyChart.AxisY.Clear();
            MyChart.AxisX.Add(new Axis());
            MyChart.AxisY.Add(new Axis());
            MyChart.Update();

            DataContext = this;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PlotGraph("TREASURY_YIELD", "monthly", "", "10year");
            PlotGraph("RealGDP", "annual", "billions of dollars", "");
        }
    }
}
