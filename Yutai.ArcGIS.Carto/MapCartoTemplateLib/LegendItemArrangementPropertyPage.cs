using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class LegendItemArrangementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILegendItem ilegendItem_0 = null;
        private string string_0 = "排列方式";

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemArrangementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.ilegendItem_0 is IVerticalLegendItem)
                {
                    (this.ilegendItem_0 as IVerticalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoVLegendItemArrangement.SelectedIndex;
                }
                else if (this.ilegendItem_0 is IHorizontalLegendItem)
                {
                    (this.ilegendItem_0 as IHorizontalLegendItem).Arrangement = (esriLegendItemArrangement) this.rdoHLegendItemArrangement.SelectedIndex;
                }
            }
        }

        public void Cancel()
        {
        }

 private void LegendItemArrangementPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
            if (this.ilegendItem_0 is IVerticalLegendItem)
            {
                this.panel2.Visible = true;
                this.rdoVLegendItemArrangement.SelectedIndex = (int) (this.ilegendItem_0 as IVerticalLegendItem).Arrangement;
            }
            else if (this.ilegendItem_0 is IHorizontalLegendItem)
            {
                this.panel1.Visible = true;
                this.rdoHLegendItemArrangement.SelectedIndex = (int) (this.ilegendItem_0 as IHorizontalLegendItem).Arrangement;
            }
        }

        private void rdoHLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void rdoVLegendItemArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.ilegendItem_0 = object_0 as ILegendItem;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

