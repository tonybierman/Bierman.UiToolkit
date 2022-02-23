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
    public partial class TextBoxWindow : Window
    {
        #region Text Dependency Property
        public object Text
        {
            get { return (object)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(object),
              typeof(TextBoxWindow),
              new FrameworkPropertyMetadata(false,
                 new PropertyChangedCallback(OnTextChangedCallBack)));

        private static void OnTextChangedCallBack(
            DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBoxWindow c = sender as TextBoxWindow;
            if (c != null && c.Text != null)
            {
                c.OnTextChanged();
            }
        }

        protected virtual void OnTextChanged()
        {
            if (this.Text is string)
            {

            }
        }
        #endregion

        public TextBoxWindow()
        {
            InitializeComponent();
        }

        public TextBoxWindow(string name) : this()
        {
            Text = name;
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
