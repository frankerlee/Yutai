using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    internal class PopupImageComboBoxEditListBoxFormEx : PopupImageComboBoxEditListBoxForm
    {
        public PopupImageComboBoxEditListBoxFormEx(ComboBoxEdit comboBoxEdit_0) : base(comboBoxEdit_0)
        {
        }

        protected override PopupListBox CreateListBox()
        {
            return new PopupImageComboBoxEditListBoxEx(this);
        }
    }
}