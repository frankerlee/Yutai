using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class ScaleAndUnitsPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private MapTemplateElement mapTemplateElement_0 = null;
        private string string_0 = "比例和单位";

        public event OnValueChangeEventHandler OnValueChange;

        public ScaleAndUnitsPropertyPage()
        {
            this.InitializeComponent();
            MapCartoTemplateLib.ScaleBarEventsClass.ValueChange +=
                new MapCartoTemplateLib.ScaleBarEventsClass.ValueChangeHandler(this.method_2);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround =
                    (MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol unitLabelSymbol = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(unitLabelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        unitLabelSymbol = selector.GetSymbol() as ITextSymbol;
                        MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar.UnitLabelSymbol = unitLabelSymbol;
                        this.method_1();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelPosition = (esriScaleBarPos) this.cboLabelPosition.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboResizeHint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.ResizeHint = (esriScaleBarResizeHint) this.cboResizeHint.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Units = (esriUnits) this.cboUnits.SelectedIndex;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    if (this.chkDivisionsBeforeZero.Checked)
                    {
                        pScaleBar.DivisionsBeforeZero = 1;
                    }
                    else
                    {
                        pScaleBar.DivisionsBeforeZero = 0;
                    }
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void chkDivisionsBeforeZero_Click(object sender, EventArgs e)
        {
        }

        private void method_0()
        {
            IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
            this.txtDivisions.Text = pScaleBar.Divisions.ToString();
            this.txtsubDivisions.Text = pScaleBar.Subdivisions.ToString();
            this.chkDivisionsBeforeZero.Checked = pScaleBar.DivisionsBeforeZero == 1;
            this.cboResizeHint.SelectedIndex = (int) pScaleBar.ResizeHint;
            this.cboUnits.SelectedIndex = (int) pScaleBar.Units;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.UnitLabelPosition;
            this.txtLabel.Text = pScaleBar.UnitLabel;
            this.txtGap.Text = pScaleBar.UnitLabelGap.ToString("#.##");
        }

        private void method_1()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_2(object object_0)
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void ScaleAndUnitsPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
            this.imapSurroundFrame_0 = this.mapTemplateElement_0.Element as IMapSurroundFrame;
            if (MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar == null)
            {
                MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar =
                    (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        private void txtDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Divisions = short.Parse(this.txtDivisions.Text);
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabelGap = double.Parse(this.txtGap.Text);
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtLabel_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.UnitLabel = this.txtLabel.Text;
                    this.method_1();
                }
                catch
                {
                }
            }
        }

        private void txtsubDivisions_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = MapCartoTemplateLib.ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.Subdivisions = short.Parse(this.txtsubDivisions.Text);
                    this.method_1();
                }
                catch
                {
                }
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