using Bierman.UiToolkit.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;

namespace Bierman.UiToolkit.Model
{
    public class DataTransferObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isValid;

        /// <summary>
        /// Gets a value indicating whether the form is valid in its current state. If all properties
        /// wich validation are valid, this property returns true.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            protected set
            {
                _isValid = value;
                OnPropertyChanged("IsValid");
            }
        }
        public DataTransferObject()
        {
            //PropertyChanged += OnPropertyChanged;
        }

        //private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{

        //}

        #region INotifyPropertyChanged
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                Debug.Fail("Invalid property name: " + propertyName);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            this.PropertyChangedCompleted(propertyName);
        }

        protected virtual void PropertyChangedCompleted(string propertyName)
        {
            ValidateProperty(propertyName);
        }
        #endregion

        #region INotifyDataErrorInfo

        private Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();

        public bool HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        protected void ValidateProperty(string propertyName)
        {
            var property = GetType().GetProperty(propertyName);
            if (property == null)
                return;

            var value = property.GetValue(this);
            var context = new ValidationContext(this) { MemberName = propertyName };
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            bool isValid = Validator.TryValidateProperty(value, context, results);

            ClearErrors(propertyName);
            if (!isValid)
            {
                AddErrors(propertyName, results.Select(r => r.ErrorMessage));
            }
        }

        private void AddErrors(string propertyName, IEnumerable<string?> errors)
        {
            if (!_validationErrors.ContainsKey(propertyName))
                _validationErrors[propertyName] = new List<string>();

            foreach (var error in errors)
            {
                if (error != null) { _validationErrors[propertyName].Add(error); }
            }
            OnErrorsChanged(propertyName);
        }

        private void ClearErrors(string propertyName)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
