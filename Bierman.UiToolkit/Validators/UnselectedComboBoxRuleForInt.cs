using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bierman.UiToolkit.Validators
{
    public class UnselectedComboBoxRuleForInt: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string msg = "Please select an option";
            if(value == null)
                return new ValidationResult(false, msg);
            try
            {
                int? v = (int?)value;
                if (v == null)
                {
                    return new ValidationResult(false, msg);
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, msg);
            }

            return new ValidationResult(true, null);
        }
    }
}
