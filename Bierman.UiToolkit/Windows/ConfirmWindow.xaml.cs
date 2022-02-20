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
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        private string _subject;
        private string _predicate;

        public ConfirmWindow()
        {
            InitializeComponent();
        }

        public ConfirmWindow(string predicate, string subject) : this()
        {
            _subject = subject;
            _predicate = predicate;

            Loaded += ConfirmWindow_Loaded;
        }

        private void ConfirmWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_subject) && !string.IsNullOrEmpty(_predicate))
            {
                LabelPrompt.Text = $"{_predicate} {_subject}?";
            }
        }

        private void Confirm(object s, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Cancel(object s, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
