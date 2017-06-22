using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class PointMaplexLabelCtrl : UserControl
    {
        private bool m_CanDo = false;
        private IMaplexOverposterLayerProperties m_pMaplexOLP = null;

        public PointMaplexLabelCtrl()
        {
            this.InitializeComponent();
        }

 private void PointMaplexLabelCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_pMaplexOLP != null)
            {
            }
        }
    }
}

