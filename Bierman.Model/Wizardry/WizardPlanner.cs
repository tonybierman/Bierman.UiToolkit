using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Bierman.Model.Wizardry
{
    public static class WizardPlanner
    {
        public static List<StepPlanItem> GenerateStepPlan(Type dataObjectType)
        {
            List<StepPlanItem> stepPlan = new List<StepPlanItem>();

            var stepTypes = dataObjectType.GetProperties()
                .SelectMany(property =>
                    property.GetCustomAttributes(typeof(StepInputAttribute), true)
                            .OfType<StepInputAttribute>()
                            .Select(attr => attr.StepType))
                .Distinct();

            foreach (var stepType in stepTypes)
            {
                var inputProperty = StepInputExtractor.GetStepInput(dataObjectType, stepType);
                if (inputProperty != null)
                {
                    var attribute = inputProperty.GetCustomAttribute<StepInputAttribute>();
                    stepPlan.Add(new StepPlanItem
                    {
                        StepType = stepType,
                        InputProperty = inputProperty,
                        Description = attribute.Description
                    });
                }
            }

            return stepPlan.OrderBy(item => item.InputProperty.GetCustomAttribute<StepInputAttribute>()?.Order).ToList();
        }

        public static IWizardStep<T> InstantiatePlan<T>(List<StepPlanItem> plan, T data) where T : IWizardData
        {
            List<IWizardStep<T>> steps = new List<IWizardStep<T>>();
            foreach (var item in plan)
            {
                if (item.InputProperty.GetValue(data) == null) // Check if the property value is null
                {
                    IWizardStep<T>? stepObject = WizardPlanner.InstantiateStep(item, ref data);
                    if (stepObject == null)
                        throw new NullReferenceException(nameof(stepObject));

                    steps.Add(stepObject);
                }
            }

            var retval = steps.First();

            for (int i = 0; i < steps.Count; i++)
            {
                var currentItem = steps[i];
                if (i < steps.Count - 1)
                {
                    var nextItem = steps[i + 1];
                    if (nextItem != null)
                    {
                        currentItem.Next = nextItem;
                        nextItem.Previous = currentItem;
                    }
                }
            }

            return retval;
        }


        //public static IWizardStep<T> InstantiatePlan<T>(List<StepPlanItem> plan, T data) where T : IWizardData
        //{
        //    List<IWizardStep<T>> steps = new List<IWizardStep<T>>();
        //    foreach (var item in plan)
        //    {
        //        IWizardStep<T>? stepObject = WizardPlanner.InstantiateStep(item, ref data);
        //        if (stepObject == null)
        //            throw new NullReferenceException(nameof(stepObject));

        //        steps.Add(stepObject);
        //    }

        //    var retval = steps.First();

        //    for (int i = 0; i < steps.Count; i++)
        //    {
        //        var currentItem = steps[i];
        //        if (i < steps.Count - 1)
        //        {
        //            var nextItem = steps[i + 1];
        //            if (nextItem != null)
        //            {
        //                currentItem.Next = nextItem;
        //                nextItem.Previous = currentItem;
        //            }
        //        }
        //    }

        //    return retval;
        //}

        public static IWizardStep<T>? InstantiateStep<T>(StepPlanItem stepPlanItem, ref T data) where T : IWizardData
        {
            Type stepType = stepPlanItem.StepType;
            if (typeof(IWizardStep<T>).IsAssignableFrom(stepType))
            {
                var constructors = stepType.GetConstructors();
                var constructor = constructors.FirstOrDefault(ctor =>
                {
                    var parameters = ctor.GetParameters();
                    return parameters.Length == 2
                        && parameters[0].ParameterType == typeof(T)
                        && parameters[1].ParameterType == typeof(string);
                });

                if (constructor != null)
                {
                    var instance = constructor.Invoke(new object[] { data, stepPlanItem.Description }) as IWizardStep<T>;
                    return instance;
                }
            }

            throw new ArgumentException("Invalid step type or constructor is not found.");
        }
    }
}
