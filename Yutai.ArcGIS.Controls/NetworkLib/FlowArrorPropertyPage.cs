using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class FlowArrorPropertyPage : UserControl
    {
        public FlowArrorPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnSym_Click(object sender, EventArgs e)
        {
            if (this.btnSym.Style != null)
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetSymbol(this.btnSym.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnSym.Style = selector.GetSymbol();
                    switch (this.listBox1.SelectedIndex)
                    {
                        case 0:
                            NetworkAnalyst.m_pFlowSymbol.DeterminateFolwArrow = this.btnSym.Style as ISymbol;
                            break;

                        case 1:
                            NetworkAnalyst.m_pFlowSymbol.IndeterminateFolwArrow = this.btnSym.Style as ISymbol;
                            break;

                        case 2:
                            NetworkAnalyst.m_pFlowSymbol.UninitializedFolwArrow = this.btnSym.Style as ISymbol;
                            break;
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.listBox1.SelectedIndex)
            {
                case 0:
                    this.btnSym.Style = NetworkAnalyst.m_pFlowSymbol.DeterminateFolwArrow;
                    break;

                case 1:
                    this.btnSym.Style = NetworkAnalyst.m_pFlowSymbol.IndeterminateFolwArrow;
                    break;

                case 2:
                    this.btnSym.Style = NetworkAnalyst.m_pFlowSymbol.UninitializedFolwArrow;
                    break;
            }
        }
    }
}