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

        public string DataOption { get; set; }
        public string Interval { get; set; }
        public string Maturity { get; set; }

        // private readonly List<DataDateValue> Data;
        public TableWindow(ref List<DataDateValue> data)
        {
            InitializeComponent();
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

            maxTrigger.Setters.Add(new Setter(BackgroundProperty, Brushes.LightSkyBlue));
            minTrigger.Setters.Add(new Setter(BackgroundProperty, Brushes.PaleVioletRed));

            rowStyle.Triggers.Add(minTrigger);
            rowStyle.Triggers.Add(maxTrigger);

            TableDataGrid.RowStyle = rowStyle;
        }
    }
}
