using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HciMiniProject
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public string ErrorMessage  { get; set; }

        public ErrorWindow(string errorMessage)
        {
            ErrorMessage = errorMessage;
            InitializeComponent();
            Topmost = true;
            errorTextBlock.Text = ErrorMessage;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
