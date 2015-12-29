using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Prototyping
{
    public class ControlParameterBinder
    {
        private readonly IDictionary<string, object> _controlParameters;
        private readonly FrameworkElement _control;

        public ControlParameterBinder(IDictionary<string, object> controlParameters, FrameworkElement control)
        {
            _controlParameters = controlParameters;
            _control = control;
        }

        public IDictionary<DependencyProperty, Binding> Bind()
        {
            var keys = _controlParameters.Keys;

            return _control.GetType()
                .GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)
                .Where(x => x.FieldType == typeof (DependencyProperty))
                .Where(x => keys.Contains(x.Name)
                            && _controlParameters[x.Name] is string
                )
                .Select(x => new
                {
                    Binding = BindingExpressionParser.Parse((string) _controlParameters[x.Name]), 
                    DependencyProperty = (DependencyProperty) x.GetValue(null)
                })
                .ToDictionary(x => x.DependencyProperty, x => x.Binding);

        }
    }
}
