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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bierman.UiToolkit.Controls
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBoxUserControl.xaml
    /// </summary>
    /// <summary>  
    /// Interaction logic for Autocomplete Text Box UserControl  
    /// </summary>  
    public partial class AutoCompleteTextBox : UserControl
    {

        /// <summary>  
        /// Auto suggestion list property.  
        /// </summary>  
        private List<string> autoSuggestionList = new List<string>();


        /// <summary>  
        /// Initializes a new instance of the <see cref="AutoCompleteTextBoxUserControl" /> class.  
        /// </summary>  
        public AutoCompleteTextBox()
        {
            // Initialization.  
            this.InitializeComponent();
        }

        #region Text Dependency Property
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(AutoCompleteTextBox),
            new FrameworkPropertyMetadata(
                default(string),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnTextChangedCallBack)
            )
        );

        private static void OnTextChangedCallBack(
            DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AutoCompleteTextBox c = sender as AutoCompleteTextBox;
            if (c != null && c.Text != null)
            {
                c.OnTextChanged();
            }
        }

        protected virtual void OnTextChanged()
        {
            if (this.Text is string)
            {
                autoTextBox.Text = (string)this.Text;
            }
        }

        #endregion

        #region AutoSuggestionList dependency properties.  

        // Using a DependencyProperty as the backing store for AutoSuggestionList.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoSuggestionListProperty =
            DependencyProperty.Register(
                "AutoSuggestionList",
                typeof(List<string>),
                typeof(AutoCompleteTextBox),
                new PropertyMetadata(null, OnAutoSuggestionListPropertyChanged));

        // .NET property wrapper for the dependency property
        public List<string> AutoSuggestionList
        {
            get { return (List<string>)GetValue(AutoSuggestionListProperty); }
            set { SetValue(AutoSuggestionListProperty, value); }
        }

        // PropertyChangedCallback method that will be called when the AutoSuggestionList property changes
        private static void OnAutoSuggestionListPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AutoCompleteTextBox c = (AutoCompleteTextBox)sender;
            //List<string> newValue = (List<string>)e.NewValue;

            // Perform any necessary logic when the property changes
            // For example, you can update the UI or handle the new value.
            // The newValue parameter contains the updated value of the property.
            // customControl.YourUpdateLogic(newValue);
            if (c != null && c.Text != null)
            {
                c.OnTextChanged();
            }
        }

        protected virtual void OnAutoSuggestionListChanged()
        {
            if (this.AutoSuggestionList is List<string>)
            {
                this.autoList.ItemsSource = (List<string>)this.AutoSuggestionList;
            }
        }

        #endregion

        #region Open Auto Suggestion box method  

        /// <summary>  
        ///  Open Auto Suggestion box method  
        /// </summary>  
        private void OpenAutoSuggestionBox()
        {
            // Enable.  
            this.autoListPopup.Visibility = Visibility.Visible;
            this.autoListPopup.IsOpen = true;
            this.autoList.Visibility = Visibility.Visible;
        }

        #endregion

        #region Close Auto Suggestion box method  

        /// <summary>  
        ///  Close Auto Suggestion box method  
        /// </summary>  
        private void CloseAutoSuggestionBox()
        {
            // Enable.  
            this.autoListPopup.Visibility = Visibility.Collapsed;
            this.autoListPopup.IsOpen = false;
            this.autoList.Visibility = Visibility.Collapsed;
        }

        #endregion

        /// <summary>  
        ///  Auto Text Box text changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Verification.  
            if (string.IsNullOrEmpty(this.autoTextBox.Text))
            {
                // Disable.  
                this.CloseAutoSuggestionBox();

                return;
            }

            if(this.AutoSuggestionList != null)
            {
                // Settings.  
                var suggestions = this.AutoSuggestionList.Where(p => p.ToLower().Contains(this.autoTextBox.Text.ToLower())).ToList();
                this.autoList.ItemsSource = suggestions;
                if (suggestions.Count > 0)
                {
                    // Enable.  
                    this.OpenAutoSuggestionBox();
                }
            }

        }

        /// <summary>  
        ///  Auto list selection changed method.  
        /// </summary>  
        /// <param name="sender">Sender parameter</param>  
        /// <param name="e">Event parameter</param>  
        private void AutoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verification.  
            if (this.autoList.SelectedIndex <= -1)
            {
                // Disable.  
                this.CloseAutoSuggestionBox();

                // Info.  
                return;
            }

            // Disable.  
            this.CloseAutoSuggestionBox();

            // Settings.  
            this.autoTextBox.Text = this.autoList.SelectedItem.ToString();
            this.autoList.SelectedIndex = -1;
        }
    }
}
