using System.Windows.Controls;

namespace GestionCaisse_MVVM.Model.Entities
{
    public class AutoCompleteFocusableBox : AutoCompleteBox
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template.FindName("Text", this) is TextBox textbox) textbox.Focus();
        }
    }
}
