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

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class NumberAndLabelPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private string string_0 = "数字和标注";

        public event OnValueChangeEventHandler OnValueChange;

        public NumberAndLabelPropertyPage()
        {
            this.InitializeComponent();
            ScaleBarEventsClass.ValueChange += new ScaleBarEventsClass.ValueChangeHandler(this.method_1);
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.imapSurroundFrame_0.MapSurround =
                    (ScaleBarFormatPropertyPage.m_pScaleBar as IClone).Clone() as IMapSurround;
            }
        }

        private void btnDivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol divisionMarkSymbol = pScaleBar.DivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(divisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        divisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.DivisionMarkSymbol = divisionMarkSymbol;
                        this.method_2();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLabelSymbolSelector_Click(object sender, EventArgs e)
        {
            try
            {
                ITextSymbol labelSymbol = ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(labelSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        labelSymbol = selector.GetSymbol() as ITextSymbol;
                        ScaleBarFormatPropertyPage.m_pScaleBar.LabelSymbol = labelSymbol;
                        this.method_2();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSubdivisionMarkSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                ILineSymbol subdivisionMarkSymbol = pScaleBar.SubdivisionMarkSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(subdivisionMarkSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        subdivisionMarkSymbol = selector.GetSymbol() as ILineSymbol;
                        pScaleBar.SubdivisionMarkSymbol = subdivisionMarkSymbol;
                        this.method_2();
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

        private void cboLabelFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar.LabelFrequency =
                    (esriScaleBarFrequency) this.cboLabelFrequency.SelectedIndex;
                this.method_2();
            }
        }

        private void cboLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar.LabelPosition =
                    (esriVertPosEnum) this.cboLabelPosition.SelectedIndex;
                this.method_2();
            }
        }

        private void cboMarkFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkFrequency = (esriScaleBarFrequency) this.cboMarkFrequency.SelectedIndex;
                this.method_2();
            }
        }

        private void cboMarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                pScaleBar.MarkPosition = (esriVertPosEnum) this.cboMarkPosition.SelectedIndex;
                this.method_2();
            }
        }

        private void method_0()
        {
            IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
            IScaleMarks marks = pScaleBar as IScaleMarks;
            this.cboMarkFrequency.SelectedIndex = (int) marks.MarkFrequency;
            this.cboMarkPosition.SelectedIndex = (int) marks.MarkPosition;
            this.txtLabelGap.Text = pScaleBar.LabelGap.ToString("#.##");
            this.cboLabelFrequency.SelectedIndex = (int) pScaleBar.LabelFrequency;
            this.cboLabelPosition.SelectedIndex = (int) pScaleBar.LabelPosition;
            this.txtDivisionMarkHeight.Text = marks.DivisionMarkHeight.ToString("#.##");
            this.txtSubdivisionMarkHeight.Text = marks.SubdivisionMarkHeight.ToString("#.##");
            if (pScaleBar is IScaleLine)
            {
                this.lblMarkHeight.Enabled = true;
                this.lblPosition.Enabled = true;
                this.lblsubMarkHeight.Enabled = true;
                this.cboLabelPosition.Enabled = true;
                this.txtDivisionMarkHeight.Enabled = true;
                this.txtSubdivisionMarkHeight.Enabled = true;
            }
            else
            {
                this.lblMarkHeight.Enabled = false;
                this.lblPosition.Enabled = false;
                this.lblsubMarkHeight.Enabled = false;
                this.cboLabelPosition.Enabled = false;
                this.txtDivisionMarkHeight.Enabled = false;
                this.txtSubdivisionMarkHeight.Enabled = false;
            }
        }

        private void method_1(object object_0)
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void method_2()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void NumberAndLabelPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.imapSurroundFrame_0 = object_0 as IMapSurroundFrame;
            if (ScaleBarFormatPropertyPage.m_pScaleBar == null)
            {
                ScaleBarFormatPropertyPage.m_pScaleBar =
                    (this.imapSurroundFrame_0.MapSurround as IClone).Clone() as IScaleBar;
            }
        }

        private void txtDivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.DivisionMarkHeight = double.Parse(this.txtDivisionMarkHeight.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        private void txtLabelGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleBar pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar;
                try
                {
                    pScaleBar.LabelGap = double.Parse(this.txtLabelGap.Text);
                    this.method_2();
                }
                catch
                {
                }
            }
        }

        private void txtSubdivisionMarkHeight_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IScaleMarks pScaleBar = ScaleBarFormatPropertyPage.m_pScaleBar as IScaleMarks;
                try
                {
                    pScaleBar.SubdivisionMarkHeight = double.Parse(this.txtSubdivisionMarkHeight.Text);
                    this.method_2();
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