using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class LabelStylePropertyPage
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
            this.groupBox1 = new GroupBox();
            this.btnLabelSymbol = new SimpleButton();
            this.symbolItem1 = new SymbolItem();
            this.groupBox2 = new GroupBox();
            this.btnLabelPlacement = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnLabelSymbol);
            this.groupBox1.Controls.Add(this.symbolItem1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(264, 96);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本符号属性";
            this.btnLabelSymbol.Location = new Point(168, 32);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(80, 24);
            this.btnLabelSymbol.TabIndex = 3;
            this.btnLabelSymbol.Text = "符号属性...";
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.symbolItem1.BackColor = SystemColors.ControlLight;
            this.symbolItem1.Location = new Point(16, 32);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(128, 32);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 2;
            this.groupBox2.Controls.Add(this.btnLabelPlacement);
            this.groupBox2.Location = new Point(8, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(264, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放置";
            this.btnLabelPlacement.Location = new Point(16, 24);
            this.btnLabelPlacement.Name = "btnLabelPlacement";
            this.btnLabelPlacement.Size = new Size(128, 24);
            this.btnLabelPlacement.TabIndex = 4;
            this.btnLabelPlacement.Text = "标注放置选项...";
            this.btnLabelPlacement.Click += new EventHandler(this.btnLabelPlacement_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelStylePropertyPage";
            base.Size = new Size(296, 216);
            base.Load += new EventHandler(this.LabelStylePropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Container components = null;
        private SimpleButton btnLabelPlacement;
        private SimpleButton btnLabelSymbol;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private SymbolItem symbolItem1;
        private IAppContext _context;
    }
}