using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using Silverlight.DataForm.UIHint.Core;

namespace Silverlight.DataForm.UIHint
{
    public abstract class GenerateControlBase<T> : IGenerateControl
        where T : FrameworkElement
    {
        public abstract FrameworkElement Generate();

        public virtual FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo)
        {
            var controlParameters = uiHint.ControlParameters;

            foreach (var controlParameter in controlParameters)
            {
                uiElement.GetType()
                    .GetProperty(controlParameter.Key)
                    ?.SetValue(uiElement, controlParameter.Value, null);
            }

            foreach (var controlParameter in
                new ControlParameterBinder(controlParameters, uiElement)
                    .Bind()
                    .Where(controlParameter => controlParameter.Value != null))
            {
                uiElement.SetBinding(controlParameter.Key, controlParameter.Value);
            }

            return uiElement;
        }
    }
}
