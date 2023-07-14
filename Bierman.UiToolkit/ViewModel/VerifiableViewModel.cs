using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Bierman.UiToolkit.Behaviors;

namespace Bierman.UiToolkit.ViewModel
{
    /// <summary>
    /// A base classe for ViewModel classes which supports validation using IDataErrorInfo interface. Properties must defines
    /// validation rules by using validation attributes defined in System.ComponentModel.DataAnnotations.
    /// </summary>
    public class VerifiableViewModel : ViewModelBase
    {
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
    }
}
