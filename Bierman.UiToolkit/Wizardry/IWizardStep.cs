using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bierman.UiToolkit.ViewModel;

namespace Bierman.UiToolkit.Wizardry
{
    public interface IWizardStep<T> where T : IWizardData
    {
        string? Name { get; }
        T? Data { get; }
        IWizardStep<T>? Previous { get; set; }
        IWizardStep<T>? Next { get; set; }
    }
}
