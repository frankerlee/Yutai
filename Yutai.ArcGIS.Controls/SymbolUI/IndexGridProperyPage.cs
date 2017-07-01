using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class IndexGridProperyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "索引";

        public event OnValueChangeEventHandler OnValueChange;

        public IndexGridProperyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).ColumnCount = (int) this.txtColumnCount.Value;
                (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).RowCount = (int) this.txtRowCount.Value;
            }
        }

        public void Cancel()
        {
        }

        public void Hide()
        {
        }

        private void IndexGridProperyPage_Load(object sender, EventArgs e)
        {
            this.txtColumnCount.Value = (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).ColumnCount;
            this.txtRowCount.Value = (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).RowCount;
            this.m_CanDo = true;
        }

        public void SetObjects(object @object)
        {
        }

        private void txtColumnCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtRowCount_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}