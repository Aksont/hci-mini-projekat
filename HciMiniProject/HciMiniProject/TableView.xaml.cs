using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HciMiniProject.API;
using System.Windows.Media;
using System.Windows.Data;

namespace HciMiniProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public string DataOption { get; private set; }
        public string IntervalOption { get; private set; }
        public string MaturityOption { get; private set; }
        public bool IsClosed { get; private set; }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        // private readonly List<DataDateValue> Data;
        public TableWindow(ref List<DataDateValue> data, ref double minValue, ref double maxValue, string chosenDataOption, string intervalOption, string maturityOption)
        {
            InitializeComponent();

            MinValue = minValue;
            MaxValue = maxValue;

            DataOption = chosenDataOption;
            IntervalOption = intervalOption;
            MaturityOption = maturityOption;

            interval.Text = "Interval: " + intervalOption;
            if (chosenDataOption.Equals("RealGDP"))
            {
                maturity.Text = "";
                dataOption.Text = "Table of REAL GDP";
            }
            else
            {
                maturity.Text = "Maturity: " + maturityOption + "s";
                dataOption.Text = "Table of TREASURY YIELD";
            }
            
            TableDataGrid.ItemsSource = data;
            SetMinMaxStyle();
        }

        private void SetMinMaxStyle()
        {
            Style rowStyle = new Style
            {
                TargetType = typeof(DataGridRow)
            };

            DataTrigger maxTrigger = new DataTrigger()
            {
                Value = MaxValue,
                Binding = new Binding("value")
            };

            DataTrigger minTrigger = new DataTrigger()
            {
                Value = MinValue,
                Binding = new Binding("value")
            };

            maxTrigger.Setters.Add(new Setter(BackgroundProperty, Brushes.PaleVioletRed));
            minTrigger.Setters.Add(new Setter(BackgroundProperty, Brushes.LightSkyBlue));

            rowStyle.Triggers.Add(minTrigger);
            rowStyle.Triggers.Add(maxTrigger);

            TableDataGrid.RowStyle = rowStyle;
        }
    }
}
