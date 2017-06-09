using GestionCaisse_MVVM.Model.Entities;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace GestionCaisse_MVVM.Validation
{
    public class ItemBaskValidationRule : ValidationRule
    {
        //TODO Doesn't detect negative entries (entry : -14, value returns 14...)
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var entry = (value as BindingGroup).Items[0] as BasketProduct;

            if (entry == null) return new ValidationResult(false, "Not valid!");
            return entry.Quantity >= 0 ? ValidationResult.ValidResult : new ValidationResult(false, "Not Valid !!!");
        }
    }
}
