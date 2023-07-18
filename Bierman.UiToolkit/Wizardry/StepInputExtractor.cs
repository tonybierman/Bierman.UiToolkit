using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.UiToolkit.Wizardry
{
    public static class StepInputExtractor
    {
        public static PropertyInfo? GetStepInput(Type dataObjectType, Type stepType)
        {
            return dataObjectType.GetProperties()
                .FirstOrDefault(property =>
                    property.GetCustomAttributes(typeof(StepInputAttribute), true)
                            .OfType<StepInputAttribute>()
                            .Any(attribute => attribute.StepType == stepType));
        }
    }

}
