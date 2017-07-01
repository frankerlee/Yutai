using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class CustomOverlayGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_IsPageDirty = false;

        public event OnValueChangeEventHandler OnValueChange;

        public CustomOverlayGridPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
            }
        }

        public void Cancel()
        {
        }

        private void CustomOverlayGridPropertyPage_Load(object sender, EventArgs e)
        {
        }

        public void Hide()
        {
        }

        public void SetObjects(object @object)
        {
        }

        public int Height
        {
            get { return 0; }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        public string Title
        {
            get { return "定制覆盖"; }
            set { }
        }

        public int Width
        {
            get { return 0; }
        }
    }
}