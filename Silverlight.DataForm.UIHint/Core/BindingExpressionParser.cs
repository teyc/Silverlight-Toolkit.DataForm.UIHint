using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Silverlight.DataForm.UIHint.Core
{
    public class BindingExpressionParser
    {
        public static Binding Parse(string myBindingExpression)
        {
            var test = "<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Text=\""
                       + myBindingExpression + "\" />";
            var result = (TextBlock) XamlReader.Load(test);
            var bindingExpression = result.GetBindingExpression(TextBlock.TextProperty);
            return bindingExpression?.ParentBinding;
        }
    }
}