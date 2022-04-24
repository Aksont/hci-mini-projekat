using HciMiniProject.API;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HciMiniProject
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> LineLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollectionBar { get; set; }
        public List<string> BarLabels { get; set; }
        public Func<double, string> FormatterBar { get; set; }
        public RotateTransform RotateTransform { get; set; }
        private string _YAxisName;
        public string YAxisName
        {
            get
            {
                return _YAxisName;
            }
            set
            {
                if (_YAxisName != value)
                {
                    _YAxisName = value;
                    OnPropertyChanged("YAxisName");
                }
            }
        }

        private string _XLabelsSplit;
        public string XLabelsSplit
        {
            get
            {
                return _XLabelsSplit;
            }
            set
            {
                if (_XLabelsSplit != value)
                {
                    _XLabelsSplit = value;
                    OnPropertyChanged("XLabelsSplit");
                }
            }
        }
        public List<DataDateValue> data;
        public double max;
        public double min;

        private TableWindow tableWindow;

        public string chosenRadioButtonOption = "RealGDP";    // GDP or Treasure
        public string chosenInterval = "annual";    // annual/quaterly OR daily/weekly/monthly
        public string chosenMaturity = "10year";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();

            FillComboboxes();

            SeriesCollection = new SeriesCollection();
            SeriesCollectionBar = new SeriesCollection();

            data = getData(chosenRadioButtonOption, chosenInterval, chosenMaturity);

            MakeLineGraph();
            MakeBarGraph();
            DataContext = this;
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
                values.Add(new DataDateValue(data[startOfRange].date, data[endOfRange].date, sum / (endOfRange - startOfRange + 1)));
            }
            return values;
        }


        private List<DataDateValue> getData(string name, string interval, string maturity)
        {
            if (name == "RealGDP")
            {
                YAxisName = "billions of dollars";
                return API.API.GetRealGDPData(interval);
            }
            else
            {
                YAxisName = "percent";
                return API.API.GetTreasuryYieldData(interval, maturity);
            }
        }

        private void MakeLineGraph()
        {
            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();
            foreach (DataDateValue dataDateValue in data)
            {
                values.Add(dataDateValue.value);
                labels.Add(dataDateValue.date);
            }
            min = values.Min();
            max = values.Max();

            PlotLineGraph(chosenRadioButtonOption, values, labels);
        }

        private void MakeBarGraph()
        {
            List<DataDateValue> dataBarPlot = scaleDataBarPlot(data);

            ChartValues<double> valuesBar = new ChartValues<double>();
            List<string> labelsBar = new List<string>();

            foreach (DataDateValue elem in dataBarPlot)
            {
                valuesBar.Add(elem.value);
                labelsBar.Add(elem.date);
            }
            PlotBarGraph(chosenRadioButtonOption, valuesBar, labelsBar);
        }

        private void PlotLineGraph(string name, ChartValues<double> values, List<string> labels)
        {
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

            if (LineLabels!=null)
            {
                LineLabels.Clear();
                LineLabels.AddRange(labels);
            }
            else
            {
                LineLabels = labels;
            }
            XLabelsSplit = (labels.Count / 9).ToString();
        }


        private void PlotBarGraph(string name, ChartValues<double> values, List<string> labels)
        {
            double min = values.Min();
            double max = values.Max();
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
            if (BarLabels != null)
            {
                BarLabels.Clear();
                BarLabels.AddRange(labels);
            }
            else
            {
                BarLabels = labels;
            }

        }
        private string GetIntervalValue()
        {
            if (intervalCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int intervalIndex = intervalCombobox.SelectedIndex;
            var selectedItem = intervalCombobox.Items[intervalIndex];
            return selectedItem.ToString().ToLower();
        }

        private string GetMaturityValue()
        {
            if (maturityCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int maturityIndex = maturityCombobox.SelectedIndex;
            var selectedItem = maturityCombobox.Items[maturityIndex];
            return Utils.CastMaturityForApi(selectedItem.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createChartBtn.IsEnabled = false;

            chosenInterval = GetIntervalValue();
            chosenMaturity = GetMaturityValue();

            data = getData(chosenRadioButtonOption, chosenInterval, chosenMaturity);

            MakeLineGraph();
            MakeBarGraph();
        }

        public void IntervalChanged(object sender, RoutedEventArgs e)
        {
            string newChosenInterval = GetIntervalValue();
            createChartBtn.IsEnabled = !chosenInterval.Equals(newChosenInterval);
        }
        public void MaturityChanged(object sender, RoutedEventArgs e)
        {
            string newChosenMaturity = GetMaturityValue();
            createChartBtn.IsEnabled = !chosenMaturity.Equals(newChosenMaturity);
        }

        private void Table_View_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSameQuery())
            {
                tableWindow = new TableWindow(ref data, ref min, ref max, chosenRadioButtonOption, chosenInterval, chosenMaturity);
                tableWindow.Show();
            }
            else
            {
                tableWindow.Focus();
            }
        }


        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton == Treasure)
            {
                chosenRadioButtonOption = "TREASURY_YIELD";

                intervalCombobox.ItemsSource = Utils.TreasureIntervals;
                intervalCombobox.SelectedIndex = Utils.TreasureIntervalsDefaulIndex;  // default = monthly

                maturityCombobox.Visibility = Visibility.Visible;
                maturityLabel.Visibility = Visibility.Visible;
            }
            else if (radioButton == GDP)
            {
                chosenRadioButtonOption = "RealGDP";

                intervalCombobox.ItemsSource = Utils.GDPIntervals;
                intervalCombobox.SelectedIndex = Utils.GDPIntervalDefaulIndex; // default = annual

                maturityCombobox.Visibility = Visibility.Hidden;
                maturityLabel.Visibility = Visibility.Hidden;
            }

        }

        private void FillComboboxes()
        {
            intervalCombobox.ItemsSource = Utils.GDPIntervals;
            intervalCombobox.SelectedIndex = Utils.GDPIntervalDefaulIndex;

            maturityCombobox.ItemsSource = Utils.TreasureMaturity;
            maturityCombobox.SelectedIndex = Utils.TreasureMaturityDefaulIndex;
        }

        private bool IsSameQuery()
        {   
            if(tableWindow == null)
            {
                return false;
            }
            if (tableWindow.IsClosed)
            {
                return false;
            }
            return tableWindow.DataOption.Equals(chosenRadioButtonOption) &&
                   tableWindow.IntervalOption.Equals(chosenInterval) &&
                   tableWindow.MaturityOption.Equals(chosenMaturity);
        }
    }
}

