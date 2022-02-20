using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bierman.UiToolkit.Validators
{
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string msg = "Please provide a name of at least 3 characters";

            try
            {
                string v = (string)value;
                if(string.IsNullOrEmpty(v))
                    return new ValidationResult(false, msg);

                if(v.StartsWith("A New "))
                    return new ValidationResult(false, msg);
            
                if(v.Length <= 3)
                    return new ValidationResult(false, msg);
            }
            catch
            {
                return new ValidationResult(false, msg);
            }

            return new ValidationResult(true, null);
        }
    }
}
