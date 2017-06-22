using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class MeasureCoordinatePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "坐标系统";

        public event OnValueChangeEventHandler OnValueChange;

        public MeasureCoordinatePropertyPage()
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

        private void btnCoordinate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("需要修改!");
        }

        public void Cancel()
        {
        }

 public void Hide()
        {
        }

 private void MeasureCoordinatePropertyPage_Load(object sender, EventArgs e)
        {
            if ((GridAxisPropertyPage.m_pMapGrid as IProjectedGrid).SpatialReference != null)
            {
                this.radioGroup.SelectedIndex = 1;
            }
            else
            {
                this.radioGroup.SelectedIndex = 0;
            }
            this.m_CanDo = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.btnCoordinate.Enabled = this.radioGroup.SelectedIndex == 1;
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void SetObjects(object @object)
        {
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

