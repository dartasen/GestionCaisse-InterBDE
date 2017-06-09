using System.Windows;
using System.Windows.Controls;

namespace GestionCaisse_MVVM.TemplateSelector
{
    /// <summary>
    ///     Select wheter or not the product displayed in the ListView of ProductInsertionView
    ///     is out of stock and so which DataTemplate to display.
    /// </summary>
    public class InsertProductDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            var product = item as Product;

            if (product.Quantity > 0)
                return element.FindResource("SimpleItemTemplate") as DataTemplate;
            return element.FindResource("OutOfSaleItemTemplate") as DataTemplate;
        }
    }
}