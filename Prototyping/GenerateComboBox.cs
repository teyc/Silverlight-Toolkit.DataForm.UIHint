using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Prototyping
{
    public class GenerateComboBox : IGenerateControl
    {
        public FrameworkElement Generate()
        {
            return new ComboBox();
        }

        public FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo)
        {
            var comboBox = (ComboBox) uiElement;
            var controlParameters = uiHint.ControlParameters;

            foreach (var controlParameter in controlParameters)
            {
                comboBox.GetType()
                    .GetProperty(controlParameter.Key)
                    ?.SetValue(comboBox, controlParameter.Value, null);
            }

            foreach (var controlParameter in 
                new ControlParameterBinder(controlParameters, uiElement)
                    .Bind()
                    .Where(controlParameter => controlParameter.Value != null))
            {
                comboBox.SetBinding(controlParameter.Key, controlParameter.Value);
            }

            uiElement.SetBinding(Selector.SelectedItemProperty,
                new Binding(propInfo.Name) {Mode = BindingMode.TwoWay});

            return uiElement;
        }
    }
}