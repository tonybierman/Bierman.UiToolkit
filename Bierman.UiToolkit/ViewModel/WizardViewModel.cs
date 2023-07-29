using Bierman.UiToolkit.Command;
using Bierman.Model.Wizardry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bierman.UiToolkit.ViewModel
{
    public class WizardViewModel<T> : PresentableViewModel where T : IWizardData
    {
        public ICommand NavigateCommand { get; protected set; }

        protected List<IWizardStep<T>>? _steps;

        private IWizardStep<T>? _currentStep;
        public IWizardStep<T>? CurrentStep
        {
            get { return _currentStep; }
            set
            {
                _currentStep = value;
                this.OnPropertyChanged("CurrentStep");
            }
        }

        protected WizardViewModel(string formName) : base(formName) 
        {
            NavigateCommand = new RelayCommand<string>(GoToStep);
            this.WindowTitle = this.FormName;
        }

        protected virtual void GoToStep(string s)
        {
            switch (s)
            {
                case "next":
                    if (CurrentStep == null)
                    {

                    }
                    else if (CurrentStep.Next == null)
                    {
                        // Last Step
                    }
                    else
                    {
                        CurrentStep = CurrentStep.Next;
                    }
                    break;

                case "back":
                    {
                        CurrentStep = CurrentStep?.Previous;
                    }
                    break;

                default:
                    break;
            }
        }

        protected override void SaveFormExecute()
        {
            if (Data != null)
            {
                Data.Validate();

                if (Data.IsValid)
                {
                    base.SaveFormExecute();
                }
            }
        }

        protected void EnsureWizardPlan(T data)
        {
            //throw new NotImplementedException();
            var plan = WizardPlanner.GenerateStepPlan(typeof(T));
            _steps = WizardPlanner.InstantiatePlan(plan, data);
            CurrentStep = _steps.First();
            Data = data;
            this.WindowTitle = this.FormName;
        }
    }
}

