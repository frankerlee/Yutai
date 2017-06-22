using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    partial class FlowArrorPropertyPage
    {
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
            this.listBox1.Location = new System.Drawing.Point(16, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(112, 112);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "流向类型";
            this.btnSym.Location = new System.Drawing.Point(144, 56);
            this.btnSym.Name = "btnSym";
            this.btnSym.Size = new Size(80, 56);
            this.btnSym.Style = null;
            this.btnSym.TabIndex = 2;
            this.btnSym.Click += new EventHandler(this.btnSym_Click);
            base.Controls.Add(this.btnSym);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listBox1);
            base.Name = "FlowArrorPropertyPage";
            base.Size = new Size(248, 216);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private StyleButton btnSym;
        private Label label1;
        private ListBox listBox1;
    }
}