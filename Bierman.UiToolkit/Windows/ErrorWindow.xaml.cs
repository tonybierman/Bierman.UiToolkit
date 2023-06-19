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
            Loaded += ErrorWindow_Loaded;
        }

        public ErrorWindow(string message) : this()
        {
            _message = message;
        }

        public ErrorWindow(object ex) : this()
        {
            _ex = ex;
        }

        public ErrorWindow(string message, object ex) : this(message)
        {
            _ex = ex;
        }

        private void ErrorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Exception currentException = _ex as Exception;

            while (currentException != null)
            {
                // Process or log the current exception
                _message += "\n" + currentException.Message;

                // Move to the next inner exception
                currentException = currentException.InnerException;
            }

            if (!string.IsNullOrEmpty(_message))
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
