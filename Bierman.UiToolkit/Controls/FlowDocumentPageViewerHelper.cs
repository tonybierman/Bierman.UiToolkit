using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Controls
{
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Markup;

    public class FlowDocumentPageViewerHelper : DependencyObject
    {
        private static HashSet<Thread> _recursionProtection = new HashSet<Thread>();

        public static string GetDocumentXaml(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentXamlProperty);
        }

        public static void SetDocumentXaml(DependencyObject obj, string value)
        {
            _recursionProtection.Add(Thread.CurrentThread);
            obj.SetValue(DocumentXamlProperty, value);
            _recursionProtection.Remove(Thread.CurrentThread);
        }

        public static readonly DependencyProperty DocumentXamlProperty = DependencyProperty.RegisterAttached(
            "DocumentXaml",
            typeof(string),
            typeof(FlowDocumentPageViewerHelper),
            new FrameworkPropertyMetadata(
                "",
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (obj, e) =>
                {
                    if (_recursionProtection.Contains(Thread.CurrentThread))
                        return;

                    var FlowDocumentPageViewer = (FlowDocumentPageViewer)obj;

                    // Parse the XAML to a document (or use XamlReader.Parse())

                    try
                    {
                        var stream = new MemoryStream(Encoding.UTF8.GetBytes(GetDocumentXaml(FlowDocumentPageViewer)));
                        var doc = (FlowDocument)XamlReader.Load(stream);

                        // Set the document
                        FlowDocumentPageViewer.Document = doc;
                    }
                    catch (Exception)
                    {
                        FlowDocumentPageViewer.Document = new FlowDocument();
                    }

                    // When the document changes update the source
                    //FlowDocumentPageViewer.TextChanged += (obj2, e2) =>
                    //    {
                    //        FlowDocumentPageViewer FlowDocumentPageViewer2 = obj2 as FlowDocumentPageViewer;
                    //        if (FlowDocumentPageViewer2 != null)
                    //        {
                    //            SetDocumentXaml(FlowDocumentPageViewer, XamlWriter.Save(FlowDocumentPageViewer2.Document));
                    //        }
                    //    };
                }
            )
        );
    }
}
