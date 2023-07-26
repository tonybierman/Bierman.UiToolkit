using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Model.Wizardry
{
    public class StepPlanItem
    {
        public Type StepType { get; set; }
        public PropertyInfo InputProperty { get; set; }
        public string Description { get; set; }
    }
}
