using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Shared;

namespace Yutai.UI.Helpers
{
    public static class ComboBoxHelper
    {
        public static IEnumerable<ComboBoxEnumItem<T>> GetComboItems<T>(IEnumerable<T> items)
            where T : struct, IConvertible
        {
            return items.Select(item => new ComboBoxEnumItem<T>(item, EnumHelper.GetToStringFunction<T>()));
        }

        public static void AddItemsFromEnum<T>(this ComboBoxAdv box, IEnumerable<T> items)
            where T : struct, IConvertible
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            var comboItems = GetComboItems(items);
            foreach (var item in comboItems)
            {
                box.Items.Add(item);
            }
        }

        public static void AddItemsFromEnum<T>(this ComboBoxAdv box) where T : struct, IConvertible
        {
            var t = new T();
            var items = GetComboItems(Enum.GetValues(t.GetType()).OfType<T>());
            foreach (var item in items)
            {
                box.Items.Add(item);
            }
        }

        public static T GetValue<T>(this ComboBoxAdv box) where T : struct, IConvertible
        {
            if (box.SelectedItem == null)
            {
                return default(T);
            }

            var item = box.SelectedItem as ComboBoxEnumItem<T>;
            if (item == null)
            {
                throw new InvalidCastException("ComboBoxEnumItem was expected");
            }
            return item.GetValue();
        }

        public static void SetValue<T>(this ComboBoxAdv box, T value) where T : struct, IConvertible
        {
            foreach (var item in box.Items)
            {
                var enumItem = item as ComboBoxEnumItem<T>;
                if (enumItem == null)
                {
                    throw new InvalidCastException("ComboBoxEnumItem was expected");
                }
                if (enumItem.GetValue().Equals(value))
                {
                    box.SelectedItem = item;
                    break;
                }
            }
        }

        public static bool SetValue(this ComboBoxAdv box, string value)
        {
            foreach (var item in box.Items)
            {
                if (item.ToString().EqualsIgnoreCase(value))
                {
                    box.SelectedItem = item;
                    return true;
                }
            }

            return false;
        }

        public static void SetSelectedIndexSafe(this ComboBoxAdv comboBox, int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < comboBox.Items.Count)
            {
                comboBox.SelectedIndex = selectedIndex;
            }
        }

        public static void AddItemsFromList<T>(List<T> list, ComboBox comboBox, string displayMember, string valueMember, string defaultValue = null)
        {
            comboBox.DataSource = list;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;

            if (string.IsNullOrWhiteSpace(defaultValue) == false)
            {
                if (comboBox.Items.Contains(defaultValue))
                    comboBox.SelectedText = defaultValue;
            }
        }

        public static void AddItemsFromFields(IFields fields, ComboBox comboBox, bool isEditable = true, string defaultValue = null)
        {
            if (comboBox.Items.Count > 0)
                comboBox.Items.Clear();
            int count = fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField field = fields.Field[i];
                if (field != null)
                {
                    if (isEditable)
                    {
                        if (field.Editable)
                            comboBox.Items.Add(field.Name);
                    }
                    else
                    {
                        comboBox.Items.Add(field.Name);
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(defaultValue) == false)
            {
                if (comboBox.Items.Contains(defaultValue))
                    comboBox.SelectedText = defaultValue;
            }
        }

        public static void AddItemsFromSystemFonts(ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
                comboBox.Items.Clear();
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            foreach (FontFamily fontFamily in fontCollection.Families)
            {
                comboBox.Items.Add(fontFamily.Name);
            }
        }
    }
}