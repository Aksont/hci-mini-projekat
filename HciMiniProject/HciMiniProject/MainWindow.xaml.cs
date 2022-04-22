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
            double max = values.Max();
            double min = values.Min();




            var lineSeries1 = new LineSeries
            {
                Title = name,
                Fill = Brushes.Transparent,
                //Stroke = { (value, index) => (value > 3.0 ? Brushes.HotPink : Brushes.YellowGreen) },
                /*Configuration = new CartesianMapper<Point>()
                    .X(point => point.X)
                    .Y(point => point.Y)
                    .Stroke(point => point.X >= 10 ? Brushes.Red : Brushes.LightGreen),*/
                Values = values,
                Stroke = Brushes.Yellow,
                /*DataLabels = {
                    foreach (double value in values) {
                        if (value == max || value == min) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                },*/
                  //  Stroke = 
                    /*Stroke = from value in values if(value==max)Brushes.Yellow,
                    Stroke = from value in values where value == max select Brushes.Red : Brushes.Yellow,
                    Stroke = (deals.Count() == 0 ? "Prospect" : "Client")*/
                //Fill = Brushes.Transparent,
               // ScalesYAt = 0
            };
            //lineSeries1.DataLabels = (value, index) => ((value >= max) || (value <= min) ? true : false);



           // MyChart.line

            SeriesCollection.Clear();
            /*var lineSeries1 = new LineSeries
            {
                Title = "Mixed Color Series",
                Configuration = new CartesianMapper<Point>()
                  .X(point => point.X)
                  .Y(point => point.Y)
                  .Stroke(point => point.X > 50 ? Brushes.Red : Brushes.Yellow)
                  .Fill(point => point.X > 50 ? Brushes.Red : Brushes.Yellow),
                Values = values
            };*/


            SeriesCollection.Add(lineSeries1);

            var dapperMapper = new CartesianMapper<double>()
            //the data point will be displayed at the position of its index on the X axis
            .X((value, index) => index)
            //the data point will have a Y value of its value (your double) aka the column height
            .Y((value) => value)
            //pass any Func to determine the fill color according to value and index
            //in this case, all columns over 3 height will be pink
            //in your case, you want this to depend on the index
            .Stroke((value, index) => ((value >= max) || (value <= min) ? Brushes.Red : Brushes.Yellow));


            LiveCharts.Charting.For<double>(dapperMapper, SeriesOrientation.Horizontal);

            Formatter = value => value.ToString("N");

            //MyChart.AxisX.Clear();
            //MyChart.AxisY.Clear();
            //MyChart.AxisX.Add(new Axis());
            //MyChart.AxisY.Add(new Axis());

            BarLabels = labels.ToArray();

           // MyChart.Update();

            DataContext = this;
        }


        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }
        public Func<double, string> Formatter { get; set; }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PlotGraph("TREASURY_YIELD", "monthly", "", "10year");
            PlotGraph("RealGDP", "annual", "billions of dollars", "");
        }
    }
}
