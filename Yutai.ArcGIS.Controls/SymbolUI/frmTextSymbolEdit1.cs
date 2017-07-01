using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    [Guid("F9680E83-DA0D-4738-92F3-ED156D55318F")]
    internal partial class frmTextSymbolEdit1 : Form
    {
        private bool m_CanDo = true;
        private double[] m_dblScaleRatio = new double[] {0.25, 0.5, 1.0, 1.25, 2.0};
        private MaskControl m_pMaskCtrl = new MaskControl();
        public IStyleGallery m_pSG;
        private TextGeneralControl m_pTextGeneralCtrl = new TextGeneralControl();
        private ITextSymbol m_pTextSymbol = null;
        private int m_ScaleIndex = 2;
        private double[] point_unit_to = new double[] {1.0, 0.01388889, 0.0352777778, 0.352777778};

        public frmTextSymbolEdit1()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_pSG = null;
            base.Close();
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

        private void cboFillType_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void frmFillSymbolEdit_Load(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            this.cboUnit.SelectedIndex = 0;
            this.cboScale.SelectedIndex = this.m_ScaleIndex;
            this.m_CanDo = true;
            this.m_pTextGeneralCtrl.m_pTextSymbol = this.m_pTextSymbol;
            this.m_pMaskCtrl.m_pMask = this.m_pTextSymbol as IMask;
            this.m_pMaskCtrl.m_pSG = this.m_pSG;
            this.m_pTextGeneralCtrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
            this.m_pMaskCtrl.ValueChanged += new ValueChangedHandler(this.ValueChanged);
            this.m_pTextGeneralCtrl.Dock = DockStyle.Fill;
            this.m_pMaskCtrl.Dock = DockStyle.Fill;
            this.pageGeneral.Controls.Add(this.m_pTextGeneralCtrl);
            this.pageMask.Controls.Add(this.m_pMaskCtrl);
            this.symbolItem1.Symbol = this.m_pTextSymbol as ISymbol;
            this.m_CanDo = true;
        }

        public ISymbol GetSymbol()
        {
            return (this.m_pTextSymbol as ISymbol);
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