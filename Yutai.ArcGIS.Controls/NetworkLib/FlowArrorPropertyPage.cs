using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class FlowArrorPropertyPage : UserControl
    {
        private StyleButton btnSym;
        private Container components = null;
        private Label label1;
        private ListBox listBox1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBox1 = new ListBox();
            this.label1 = new Label();
            this.btnSym = new StyleButton();
            base.SuspendLayout();
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] { "确定流向", "不确定流向", "未初始化流向" });
            this.listBox1.Location = new System.Drawing.Point(0x10, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x70, 0x70);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "流向类型";
            this.btnSym.Location = new System.Drawing.Point(0x90, 0x38);
            this.btnSym.Name = "btnSym";
            this.btnSym.Size = new Size(80, 0x38);
            this.btnSym.Style = null;
            this.btnSym.TabIndex = 2;
            this.btnSym.Click += new EventHandler(this.btnSym_Click);
            base.Controls.Add(this.btnSym);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listBox1);
            base.Name = "FlowArrorPropertyPage";
            base.Size = new Size(0xf8, 0xd8);
            base.ResumeLayout(false);
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

