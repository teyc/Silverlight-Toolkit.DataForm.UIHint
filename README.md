Silverlight.DataForm.UIHint
------------------------------

This package provides support for UIHint attribute to be used with your Silverlight data forms

Usage:

    Add `please:Use.UiHint` to your DataForm

        <toolkit:DataForm
            CommandButtonsVisibility="Commit,Cancel"
            CurrentItem="{Binding .}"
            Grid.Column="1"
            Grid.Row="1"
            xmlns:please="clr-namespace:Silverlight.DataForm.UIHint;assembly=Silverlight.DataForm.UIHint"
            please:Use.UiHint="true" />

Next, annotate your properties like below

    [Display(Name = "Required Equipment")] 
    [Required]
    [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight", 
        "ItemsSourceProperty", "{Binding Equipments}",
        "DisplayMemberPath", "EquipmentName")]
    public Equipment SelectedEquipment { get; set; }

Let's take it apart and see what it means:

1. The first parameter `"Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint"`
   identifies the type of control that will be generated, and where the assembly is. Unfortunately,
   just specifying "System.Windows.Controls.ComboxBox, System.Windows.Controls" doesn't work, assembly
   the control still needs to be data bound to the right fields.

2. The second line `"ItemsSourceProperty", "{Binding Equipments}"` creates a data binding between
    the ItemsSource and the list of equipments that the user can select from.

3. The third line `"DisplayMemberPath", "EquipmentName"` assigns a string to `comboBox.DisplayMemberPath`

Other Helpers
---------------

Occassionally, you may want to allow the user to leave the item as null. I've given you a NullableItemsSource
class that you can use to bind to a list, and it will add `null` to the beginning of the list.

    [Display(AutoGenerateField = false)]
    public NullableItemsSource<Equipment> OptionalEquipments { get; set; }

    [Display(Name = "Optional equipment")]
    [UIHint("Silverlight.DataForm.UIHint.GenerateComboBox, Silverlight.DataForm.UIHint", "Silverlight",
        "ItemsSourceProperty", "{Binding OptionalEquipments}",
        "DisplayMemberPath", "EquipmentName")]
    public Equipment OptionalEquipment { get; set; }

In your constructor, you can load the NullableItemsSource with a list

    public MyViewModel()
    {
        OptionalEquipments = new NullableItemsSource<Equipment>() {ItemsSource = Equipments};
    }

    