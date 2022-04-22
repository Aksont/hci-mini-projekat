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
using LiveCharts.Configurations;
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
            SeriesCollection = new SeriesCollection();
            SeriesCollectionBar = new SeriesCollection();

            // zakucano
            string name = "TREASURY_YIELD";
            List<DataDateValue> data = getData(name, "monthly", "", "10year");

            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(parseDate(dataDateValue.date));
            }

            PlotLineGraph(name, values, labels);


            List<DataDateValue> dataBarPlot = scaleDataBarPlot(data);

            ChartValues<double> valuesBar = new ChartValues<double>();
            List<string> labelsBar = new List<string>();

            foreach (DataDateValue elem in dataBarPlot)
            {
                valuesBar.Add(elem.value);
                labelsBar.Add(elem.date);
            }

            PlotColumnGraph(name, valuesBar, labelsBar);

        }


        private List<DataDateValue> scaleDataBarPlot(List<DataDateValue> data)
        {
            if (data.Count <= 30)
            {
                return data;
            }
            double sizeOfRange = data.Count / 30;
            List<DataDateValue> values = new List<DataDateValue>();
            for (int i=0; i<30; i++)
            {
                double sum = 0;
                int startOfRange = Convert.ToInt32(sizeOfRange * i);
                int endOfRange = Convert.ToInt32(sizeOfRange * (i + 1));
                for (int j = startOfRange; j < endOfRange; j++)
                {
                    sum += data[j].value;
                }
                values.Add(new DataDateValue(parseDate(data[startOfRange].date) + "-" + parseDate(data[endOfRange].date), sum / (endOfRange - startOfRange + 1)));
            }
            return values;
        }


        private List<DataDateValue> getData(string name, string interval, string unit, string maturity)
        {
            if (name == "RealGDP")
            {
                YAxisName = "Real GDP value";
                return API.API.GetRealGDPData(interval, unit);
            } else
            {
                YAxisName = "Tresury yield value";
                return API.API.GetTreasuryYieldData(interval, maturity);
            }
        }

        private void PlotLineGraph(string name, ChartValues<double> values, List<string> labels)
        {
            double max = values.Max();
            double min = values.Min();

            var lineSeries1 = new LineSeries()
            {
                Title = name,
                Fill = Brushes.Transparent,
                Values = values,
                Configuration = new CartesianMapper<double>()
                .Stroke(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.Yellow)
                .Y(value => value)

            };

            SeriesCollection.Clear();


            SeriesCollection.Add(lineSeries1);

            /*var dapperMapper = new CartesianMapper<double>()
            .X((value, index) => index)
            .Y((value) => value)
            .Stroke((value, index) => ((value >= max) || (value <= min) ? Brushes.Red : Brushes.LightBlue));

            LiveCharts.Charting.For<double>(dapperMapper, SeriesOrientation.Horizontal);*/

            Formatter = value => value.ToString("N");

            LineLabels = labels.ToArray();
            DataContext = this;

        }

        private string parseDate(string date)
        {
            string[] parameters = date.Split('-');
            return parameters[2] + "." + parameters[1] + "." + parameters[0] + ".";
        }

        private void PlotColumnGraph(string name, ChartValues<double> values, List<string> labels)
        {
            double max = values.Max();
            double min = values.Min();
            var columnSeries = new ColumnSeries()
            {
                Title = name,
                Values = values,
                Configuration = new CartesianMapper<double>()
                .Stroke(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.Yellow)
                .Fill(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.Yellow)
                .Y(value => value)
            };
            SeriesCollectionBar.Clear();
            SeriesCollectionBar.Add(columnSeries);
            SeriesCollectionBar.Add(columnSeries);
            FormatterBar = value => value.ToString("N");

            BarLabels = labels.ToArray();
            RotateTransform = new RotateTransform(13);

            DataContext = this;

        }

        


        public SeriesCollection SeriesCollection { get; set; }
        public string[] LineLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollectionBar { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> FormatterBar { get; set; }
        public RotateTransform RotateTransform { get; set; }
        public string YAxisName { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = "TREASURY_YIELD";
            List<DataDateValue> data = getData(name, "monthly", "", "10year");

            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(dataDateValue.date);
            }
          /*  List<DataDateValue> data = getData("RealGDP", "monthly", "", "10year");
            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(dataDateValue.date);
            }*/
            PlotColumnGraph(name, values, labels);
            PlotLineGraph(name, values, labels);
            //PlotGraph("TREASURY_YIELD", "monthly", "", "10year");
            // PlotLineGraph("RealGDP", "annual", "billions of dollars", "");
        }
    }
}
