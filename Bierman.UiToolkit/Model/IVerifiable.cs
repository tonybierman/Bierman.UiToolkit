using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Model
{
    public interface IVerifiable
    {
        void Validate();
        bool IsValid { get; }
    }
}
