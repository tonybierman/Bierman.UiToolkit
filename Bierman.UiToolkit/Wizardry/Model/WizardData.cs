using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Wizardry.Model
{
    public class WizardData : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        protected void ValidatePositiveNumber(string name, double? input)
        {
            ClearErrors(name);
            if (input.HasValue && input.Value > 0)
                return;

            AddError(name, $"{name} must be a positive number");
        }

        protected void ValidateNotNull(string name, string? input)
        {
            ClearErrors(name);
            if (string.IsNullOrWhiteSpace(input))
            {
                AddError(name, $"{name} cannot be empty");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyDataErrorInfo implementation
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
                return null;

            return _errors[propertyName];
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
