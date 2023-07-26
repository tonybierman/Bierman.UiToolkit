using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bierman.Model.Wizardry
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StepInputAttribute : Attribute
    {
        public Type StepType { get; }
        public int Order { get; }
        public string Description { get; }

        public StepInputAttribute(Type stepType, int order, string description)
        {
            StepType = stepType;
            Order = order;
            Description = description;
        }
    }
}
