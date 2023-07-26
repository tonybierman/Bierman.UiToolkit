using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Wizardry
{
    public class WizardStep<T> where T : IWizardData
    {
        public string? Title { get; set; }

        public T? Data { get; }

        public IWizardStep<T>? Previous { get; set; }

        public IWizardStep<T>? Next { get; set; }

        public WizardStep(T? d, string title = "Untitled")
        {
            Data = d;
            Title = title;
        }
    }
}
