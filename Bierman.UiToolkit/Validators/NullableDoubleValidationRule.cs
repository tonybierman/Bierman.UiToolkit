using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bierman.UiToolkit.Validators
{
    public class NullableDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double? number = null;
            if (value != null && value is string && !String.IsNullOrWhiteSpace((string)value))
            {
                double temp;
                if (double.TryParse((string)value, out temp))
                {
                    number = temp;
                }
            }
            if (number == null || number <= 0)
            {
                return new ValidationResult(false, "Value must be a number greater than zero.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
