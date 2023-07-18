using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Wizardry
{
    public interface IWizardData
    {
        void Validate();
        bool IsValid { get; }
    }
}
