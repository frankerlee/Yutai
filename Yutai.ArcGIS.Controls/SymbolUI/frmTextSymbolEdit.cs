using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class frmTextSymbolEdit : Form
    {
        private bool m_CanDo = true;
        private double[] m_dblScaleRatio = new double[] { 0.25, 0.5, 1.0, 1.25, 2.0 };
        public IStyleGallery m_pSG;
        private ITextSymbol m_pTextSymbol = null;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] { 1.0, 0.01388889, 0.0352777778, 0.352777778 };

        public frmTextSymbolEdit()
        {
            this.InitializeComponent();
        }

        private void btnFixZoomIn_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 0)
            {
                this.m_ScaleIndex--;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void btnFixZoomOut_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 4)
            {
                this.m_ScaleIndex++;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
        }

        private void btnOnt2One_Click(object sender, EventArgs e)
        {
            if (this.m_ScaleIndex != 2)
            {
                this.m_ScaleIndex = 2;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.m_CanDo = false;
                this.cboScale.SelectedIndex = this.m_ScaleIndex;
                this.m_CanDo = true;
                this.symbolItem1.Invalidate();
            }
        }

        private void cboScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_ScaleIndex = this.cboScale.SelectedIndex;
                this.symbolItem1.ScaleRatio = this.m_dblScaleRatio[this.m_ScaleIndex];
                this.symbolItem1.Invalidate();
            }
        }

        private void cboTextType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.tabControl1.TabPages.Clear();
                if (this.cboTextType.SelectedIndex == 0)
                {
                    this.m_pTextSymbol = new TextSymbolClass();
                    TextGeneralControl control = new TextGeneralControl();
                    MaskControl control2 = new MaskControl();
                    TabPage page = new TabPage("常规");
                    this.tabControl1.TabPages.Add(page);
                    control.m_pTextSymbol = this.m_pTextSymbol;
                    control2.m_pMask = this.m_pTextSymbol as IMask;
                    control2.m_pSG = this.m_pSG;
                    control.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    control2.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                    control.Dock = DockStyle.Fill;
                    control2.Dock = DockStyle.Fill;
                    page.Controls.Add(control);
                    page = new TabPage("掩模");
                    this.tabControl1.TabPages.Add(page);
                    page.Controls.Add(control2);
                }
                this.symbolItem1.Symbol = this.m_pTextSymbol as ISymbol;
                this.symbolItem1.Invalidate();
            }
        }

        private void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    CommonInterface interface2 = this.tabControl1.TabPages[i].Controls[0] as CommonInterface;
                    if (interface2 != null)
                    {
                        interface2.ChangeUnit(this.point_unit_to[this.cboUnit.SelectedIndex]);
                    }
                }
            }
        }

 private void frmTextSymbolEdit_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        public ISymbol GetSymbol()
        {
            return (this.m_pTextSymbol as ISymbol);
        }

        private void Init()
        {
            this.m_CanDo = false;
            this.cboTextType.SelectedIndex = 0;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.tabControl1.TabPages.Clear();
            if (this.cboTextType.SelectedIndex == 0)
            {
                TextGeneralControl control = new TextGeneralControl();
                MaskControl control2 = new MaskControl();
                TabPage page = new TabPage("常规");
                this.tabControl1.TabPages.Add(page);
                control.m_pTextSymbol = this.m_pTextSymbol;
                control2.m_pMask = this.m_pTextSymbol as IMask;
                control2.m_pSG = this.m_pSG;
                control.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                control2.ValueChanged += new ValueChangedHandler(this.ValueChanged);
                control.Dock = DockStyle.Fill;
                control2.Dock = DockStyle.Fill;
                page.Controls.Add(control);
                page = new TabPage("掩模");
                this.tabControl1.TabPages.Add(page);
                page.Controls.Add(control2);
            }
            this.symbolItem1.Symbol = this.m_pTextSymbol as ISymbol;
            this.m_CanDo = true;
        }

 private void pFractionTextSymbolPage_ValueChanged(object sender, EventArgs e)
        {
            this.symbolItem1.Invalidate();
        }

        public void SetSymbol(ISymbol pSym)
        {
            this.m_pOldSymbol = pSym;
            this.m_pTextSymbol = (pSym as IClone).Clone() as ITextSymbol;
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            this.symbolItem1.Invalidate();
        }
    }
}

