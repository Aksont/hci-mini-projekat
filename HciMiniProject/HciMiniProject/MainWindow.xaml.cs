using HciMiniProject.API;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HciMiniProject
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            SeriesCollectionBar = new SeriesCollection();

            string name = "TREASURY_YIELD";
            List<DataDateValue> data = getData(name, "monthly", "", "10year");


            //line plot visualise
            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(parseDate(dataDateValue.date));
            }

            PlotLineGraph(name, values, labels);



            //bar plot visualise
            List<DataDateValue> dataBarPlot = scaleDataBarPlot(data);

            ChartValues<double> valuesBar = new ChartValues<double>();
            List<string> labelsBar = new List<string>();

            foreach (DataDateValue elem in dataBarPlot)
            {
                valuesBar.Add(elem.value);
                labelsBar.Add(elem.date);
            }

            PlotBarGraph(name, valuesBar, labelsBar);

        }


        private List<DataDateValue> scaleDataBarPlot(List<DataDateValue> data)
        {
            if (data.Count <= 30)
            {
                return data;
            }
            double sizeOfRange = data.Count / 30;
            List<DataDateValue> values = new List<DataDateValue>();
            for (int i = 0; i < 30; i++)
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
            }
            else
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
                PointGeometrySize = 5,
                Configuration = new CartesianMapper<double>()
                .Stroke(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.Yellow)
                .Y(value => value)
            };

            SeriesCollection.Clear();
            SeriesCollection.Add(lineSeries1);
            Formatter = value => value.ToString("N");
            LineLabels = labels.ToArray();
            DataContext = this;

        }

        private string parseDate(string date)
        {
            string[] parameters = date.Split('-');
            return parameters[2] + "." + parameters[1] + "." + parameters[0] + ".";
        }

        private void PlotBarGraph(string name, ChartValues<double> values, List<string> labels)
        {
            double max = values.Max();
            double min = values.Min();
            var columnSeries = new ColumnSeries()
            {
                Title = name,
                Values = values,
                Configuration = new CartesianMapper<double>()
                .Stroke(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.DeepSkyBlue)
                .Fill(value => (value == max) ? Brushes.Red : (value == min) ? Brushes.Red : Brushes.DeepSkyBlue)
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
            PlotBarGraph(name, values, labels);
            PlotLineGraph(name, values, labels);
        }

        private void Table_View_Click(object sender, RoutedEventArgs e)
        {
            TableWindow tableWindow = new TableWindow();
            tableWindow.Show();
        }

    }
}
