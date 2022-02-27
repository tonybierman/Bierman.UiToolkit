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

namespace Bierman.UiToolkit.Windows
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private string _message;
        private object _ex;

        public ErrorWindow()
        {
            InitializeComponent();
        }

        public ErrorWindow(string message) : this()
        {
            _message = message;
            Loaded += ErrorWindow_Loaded;
        }

        public ErrorWindow(string message, object ex) : this(message)
        {
            _ex = ex;
        }

        private void ErrorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(_message))
            {
                LabelMessage.Text = _message;
            }
        }
        private void Confirm(object s, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
