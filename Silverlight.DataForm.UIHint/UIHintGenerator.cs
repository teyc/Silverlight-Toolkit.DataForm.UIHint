using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using Silverlight.DataForm.UIHint.Core;

namespace Silverlight.DataForm.UIHint
{
    public class UIHintGenerator : IGenerateControl
    {
        private const FrameworkElement NoResult = null;
        private readonly IGenerateControl _generator;

        private readonly Type _propertyType;

        public UIHintGenerator(string propertyName, Type propertyType, System.Windows.Controls.DataForm dataForm)
        {
            _propertyType = propertyType;

            PropInfo = dataForm.CurrentItem.GetType().GetProperty(propertyName);

            UIHintAttribute = GetUiHintAttribute(dataForm, propertyName);
            _generator = UIHintAttribute
                ?.UIHint
                ?.Pipe(TypeLoader.GetType)
                ?.Pipe(x => typeof (IGenerateControl).IsAssignableFrom(x) ? x : null)
                ?.Pipe(x => (IGenerateControl) Activator.CreateInstance(x));
        }

        public PropertyInfo PropInfo { get; }

        public UIHintAttribute UIHintAttribute { get; }

        public virtual FrameworkElement Generate()
        {
            return _generator?.Generate() ?? NoResult;
        }

        public virtual FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo)
        {
            return _generator?.Bind(uiElement, uiHint, propInfo);
        }

        protected static UIHintAttribute GetUiHintAttribute(System.Windows.Controls.DataForm dataForm, string propertyName)
        {
            var dataType = dataForm.CurrentItem.GetType();
            var propInfo = dataType.GetProperty(propertyName);
            return (UIHintAttribute) propInfo.GetCustomAttributes(typeof (UIHintAttribute), true).FirstOrDefault();
        }
    }

    internal static class PipeExtension
    {
        internal static T Pipe<TIn, T>(this TIn input, Func<TIn, T> f)
        {
            return f(input);
        }
    }
}