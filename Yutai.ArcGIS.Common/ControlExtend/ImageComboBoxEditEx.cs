using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class ImageComboBoxEditEx : ImageComboBoxEdit
    {
        protected override PopupBaseForm CreatePopupForm()
        {
            return new PopupImageComboBoxEditListBoxFormEx(this);
        }
    }
}

