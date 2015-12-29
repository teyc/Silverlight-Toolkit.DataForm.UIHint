using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;

namespace Silverlight.DataForm.UIHint
{
    public interface IGenerateControl
    {
        FrameworkElement Generate();

        FrameworkElement Bind(FrameworkElement uiElement, UIHintAttribute uiHint, PropertyInfo propInfo);
    }
}
