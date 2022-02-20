using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bierman.UiToolkit.Validators
{
    public class StringNotNullorEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string v = (string)value;
                if(string.IsNullOrEmpty(v))
                    return new ValidationResult(false, "Value required");
            }
            catch
            {
                return new ValidationResult(false, "Value required");
            }

            return new ValidationResult(true, null);
        }
    }
}
