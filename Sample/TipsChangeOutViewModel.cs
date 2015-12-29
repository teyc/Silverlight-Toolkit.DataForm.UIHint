using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using Caliburn.Micro;
using Silverlight.DataForm.UIHint;

namespace Prototyping
{
    public class TipsChangeOutViewModel
    {
        public TipsChangeOutViewModel()
        {
            Equipments = new BindableCollection<Equipment>
            {
                new Equipment(1, "DRE1"),
                new Equipment(2, "DRE2"),
                new Equipment(3, "DRE3")
            };

            OptionalEquipments = new NullableItemsSource<Equipment> {ItemsSource = Equipments};

            Toppings = new BindableCollection<Topping>
            {
                new Topping(1, "Pepperoni"),
                new Topping(2, "Cheese"),
                new Topping(3, "Tomato")
            };

            SelectedToppings = new BindableCollection<Topping>(new[] {Toppings[0]});
        }

        [Display(Name = "Changeout Date")]
        [Required]
        public DateTime? ChangeoutDate { get; set; }

        [Display(AutoGenerateField = false)]
        public BindableCollection<Equipment> Equipments { get; set; }

        [Display(AutoGenerateField = false)]
        public NullableItemsSource<Equipment> OptionalEquipments { get; set; }

        [Display(Name = "Required Equipment")]
        [Required]
        [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight",
            "ItemsSourceProperty", "{Binding Equipments}",
            "DisplayMemberPath", "EquipmentName")]
        public Equipment SelectedEquipment { get; set; }

        [Display(Name = "Optional equipment")]
        [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight",
            "ItemsSourceProperty", "{Binding OptionalEquipments}",
            "DisplayMemberPath", "EquipmentName")]
        public Equipment OptionalEquipment { get; set; }

        [Display(AutoGenerateField = false)]
        public BindableCollection<Topping> Toppings { get; set; }

        [Display(Name = "Any of these")]
        [UIHint("Silverlight.DataForm.UIHint.GenerateCheckBoxes, Silverlight.DataForm.UIHint", "Silverlight",
            "DisplayMemberPath", "ToppingName",
            "ItemsSourceProperty", "{Binding Toppings}")]
        public BindableCollection<Topping> SelectedToppings { get; set; }

        private delegate Binding BindingDelegate();
    }
}