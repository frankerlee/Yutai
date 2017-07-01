using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class frmRepresationRule : Form
    {
        internal static IStyleGallery m_pSG = null;
        private RepresationRuleCtrl m_RepresationRuleCtrl = new RepresationRuleCtrl();

        public frmRepresationRule()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void frmRepresationRule_Load(object sender, EventArgs e)
        {
            base.Controls.Add(this.m_RepresationRuleCtrl);
        }

        public IRepresentationRuleItem RepresentationRuleItem
        {
            get { return this.m_RepresationRuleCtrl.RepresentationRuleItem; }
            set { this.m_RepresationRuleCtrl.RepresentationRuleItem = value; }
        }
    }
}