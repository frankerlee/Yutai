using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class frmMapGridType : Form
    {
        private IMapGrid m_pMapGrid = null;

        public frmMapGridType()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (this.radioGroup1.SelectedIndex)
            {
                case 0:
                    this.m_pMapGrid = new MgrsGridClass();
                    break;

                case 1:
                    this.m_pMapGrid = new GraticuleClass();
                    break;

                case 2:
                    this.m_pMapGrid = new MeasuredGridClass();
                    break;

                case 3:
                    this.m_pMapGrid = new IndexGridClass();
                    break;

                case 4:
                    this.m_pMapGrid = new CustomOverlayGridClass();
                    break;
            }
        }

 public IMapGrid MapGrid
        {
            get
            {
                return this.m_pMapGrid;
            }
        }
    }
}

