using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Windows
{
    public interface ICloseable
    {
        string WindowTitle { get; set; }
        event EventHandler CloseRequest;
        event EventHandler CancelRequest;
    }
}
