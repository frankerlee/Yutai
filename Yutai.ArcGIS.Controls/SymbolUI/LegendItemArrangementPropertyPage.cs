using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class LegendItemArrangementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        internal static ILegendItem m_pLegendItem = null;
        internal static ILegendItem m_pOldLegendItem = null;
        private string m_Title = "排列方式";

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemArrangementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                if (m_pLegendItem != null)
                {
                    (m_pOldLegendItem as IClone).Assign(m_pLegendItem as IClone);
                }
            }
        }

        public void Cancel()
        {
        }

 public void Hide()
        {
        }

        private void Init()
        {
            if (m_pLegendItem is IVerticalLegendItem)
            {
                this.panel2.Visible = true;
                this.rdoVLegendItemArrangement.SelectedIndex = (int) (m_pLegendItem as IVerticalLegendItem).Arrangement;
            }
            else if (m_pLegendItem is IHorizontalLegendItem)
            {
                this.panel1.Visible = true;
                this.rdoHLegendItemArrangement.SelectedIndex = (int) (m_pLegendItem as IHorizontalLegendItem).Arrangement;
            }
        }

 private void LegendItemArrangementPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        private void rdoHLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (m_pLegendItem != null))
            {
                (m_pLegendItem as IHorizontalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoHLegendItemArrangement.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void rdoVLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo && (m_pLegendItem != null))
            {
                (m_pLegendItem as IVerticalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoVLegendItemArrangement.SelectedIndex;
                this.ValueChanged();
            }
        }

        public void SetObjects(object @object)
        {
            m_pOldLegendItem = @object as ILegendItem;
            if (m_pOldLegendItem != null)
            {
                m_pLegendItem = (m_pOldLegendItem as IClone).Clone() as ILegendItem;
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
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

