using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class ScaleTextTextPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IMapSurroundFrame m_pMapSurroundFrame = null;
        private string m_Title = "比例尺文本";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleTextTextPropertyPage()
        {
            this.InitializeComponent();
            ScaleTextEventsClass.ValueChange += new ScaleTextEventsClass.ValueChangeHandler(this.ScaleTextEventsClass_ValueChange);
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.Name = "sdfsf";
                this.m_IsPageDirty = false;
                (ScaleTextFormatPropertyPage.m_pOldScaleText as IClone).Assign(ScaleTextFormatPropertyPage.m_pScaleText as IClone);
            }
        }

        public void Cancel()
        {
        }

        private void cboMapUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.MapUnits = (esriUnits) this.cboMapUnit.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void cboPageUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
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
                this.ValueChanged();
            }
        }

 public void Hide()
        {
        }

        private void Init()
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

 private void rdoStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.Style = (esriScaleTextStyleEnum) this.rdoStyle.SelectedIndex;
                this.ValueChanged();
            }
        }

        private void ScaleTextEventsClass_ValueChange(object sender)
        {
            if (sender is ScaleTextFormatPropertyPage)
            {
                this.m_CanDo = false;
                this.Init();
                this.symbolItem1.Invalidate();
                this.m_CanDo = true;
            }
        }

        private void ScaleTextTextPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.m_CanDo = true;
        }

        public void SetObjects(object @object)
        {
            if (ScaleTextFormatPropertyPage.m_pScaleText == null)
            {
                ScaleTextFormatPropertyPage.m_pOldScaleText = @object as IScaleText;
                ScaleTextFormatPropertyPage.m_pScaleText = (@object as IClone).Clone() as IScaleText;
            }
        }

        private void txtMapUnitLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.MapUnitLabel = this.txtMapUnitLabel.Text;
                this.ValueChanged();
            }
        }

        private void txtPageUnitLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ScaleTextFormatPropertyPage.m_pScaleText.PageUnitLabel = this.txtPageUnitLabel.Text;
                this.ValueChanged();
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
            this.symbolItem1.Invalidate();
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

