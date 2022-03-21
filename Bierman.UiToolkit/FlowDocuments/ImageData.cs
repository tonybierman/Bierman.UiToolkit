using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bierman.UiToolkit.FlowDocuments
{
    // https://stackoverflow.com/questions/25557891/xaml-wpf-how-to-add-inline-background-image-on-flowdocument
    public class ImageData : DependencyObject
    {
        public static readonly DependencyProperty Base64ImageDataProperty =
            DependencyProperty.Register("Base64ImageData",
            typeof(string),
            typeof(ImageData));

        public string Base64ImageData
        {
            get { return (string)(GetValue(Base64ImageDataProperty)); }
            set { SetValue(Base64ImageDataProperty, value); }
        }
    }
}
