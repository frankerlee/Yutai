using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class ScaleTextTextPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private MapTemplateElement mapTemplateElement_0 = null;
        private string string_0 = "比例尺文本";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleTextTextPropertyPage()
        {
            this.InitializeComponent();
            ScaleTextEventsClass.ValueChange += new ScaleTextEventsClass.ValueChangeHandler(this.method_1);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround =
                    (ScaleTextFormatPropertyPage.m_pScaleText as IClone).Clone() as IMapSurround;
            }
        }

        public void Cancel()
        {
        }

        private void cboMapUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.MapUnits = (esriUnits) this.cboMapUnit.SelectedIndex;
                this.method_2();
            }
        }

        private void cboPageUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboPageUnit.SelectedIndex == 0)
                {
                    ScaleTextFormatPropertyPage.m_pScaleText.PageUnits = esriUnits.esriCentimeters;
                }
                else if (this.cboPageUnit.SelectedIndex == 1)
                {
                    ScaleTextFormatPropertyPage.m_pScaleText.PageUnits = esriUnits.esriInches;
                }
                else if (this.cboPageUnit.SelectedIndex == 2)
                {
                    ScaleTextFormatPropertyPage.m_pScaleText.PageUnits = esriUnits.esriPoints;
                }
                this.method_2();
            }
        }

        private void method_0()
        {
            if (ScaleTextFormatPropertyPage.m_pScaleText != null)
            {
                this.symbolItem1.Symbol = ScaleTextFormatPropertyPage.m_pScaleText;
                this.txtMapUnitLabel.Text = ScaleTextFormatPropertyPage.m_pScaleText.MapUnitLabel;
                this.txtPageUnitLabel.Text = ScaleTextFormatPropertyPage.m_pScaleText.PageUnitLabel;
                this.cboMapUnit.SelectedIndex = (int) ScaleTextFormatPropertyPage.m_pScaleText.MapUnits;
                this.rdoStyle.SelectedIndex = (int) ScaleTextFormatPropertyPage.m_pScaleText.Style;
                esriUnits pageUnits = ScaleTextFormatPropertyPage.m_pScaleText.PageUnits;
                switch (pageUnits)
                {
                    case esriUnits.esriInches:
                        this.cboPageUnit.SelectedIndex = 1;
                        return;

                    case esriUnits.esriPoints:
                        this.cboPageUnit.SelectedIndex = 2;
                        return;
                }
                if (pageUnits == esriUnits.esriCentimeters)
                {
                    this.cboPageUnit.SelectedIndex = 0;
                }
            }
        }

        private void method_1(object object_0)
        {
            if (object_0 is ScaleTextFormatPropertyPage)
            {
                this.bool_0 = false;
                this.method_0();
                this.symbolItem1.Invalidate();
                this.bool_0 = true;
            }
        }

        private void method_2()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.symbolItem1.Invalidate();
        }

        private void rdoStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.Style = (esriScaleTextStyleEnum) this.rdoStyle.SelectedIndex;
                this.method_2();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void ScaleTextTextPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
            this.imapSurroundFrame_0 = this.mapTemplateElement_0.Element as IMapSurroundFrame;
            if ((this.imapSurroundFrame_0 != null) && (ScaleTextFormatPropertyPage.m_pScaleText == null))
            {
                ScaleTextFormatPropertyPage.m_pScaleText =
                    (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleText;
            }
        }

        private void txtMapUnitLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.MapUnitLabel = this.txtMapUnitLabel.Text;
                this.method_2();
            }
        }

        private void txtPageUnitLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.PageUnitLabel = this.txtPageUnitLabel.Text;
                this.method_2();
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
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
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}