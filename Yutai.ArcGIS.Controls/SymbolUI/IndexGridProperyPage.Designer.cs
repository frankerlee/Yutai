using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class IndexGridProperyPage
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
            this.txtColumnCount = new SpinEdit();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.txtRowCount = new SpinEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtColumnCount.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtRowCount.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtColumnCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(192, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列";
            int[] bits = new int[4];
            this.txtColumnCount.EditValue = new decimal(bits);
            this.txtColumnCount.Location = new Point(56, 24);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtColumnCount.Size = new Size(80, 23);
            this.txtColumnCount.TabIndex = 1;
            this.txtColumnCount.EditValueChanged += new EventHandler(this.txtColumnCount_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "列数";
            this.groupBox2.Controls.Add(this.txtRowCount);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(16, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(192, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "行";
            bits = new int[4];
            this.txtRowCount.EditValue = new decimal(bits);
            this.txtRowCount.Location = new Point(56, 24);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtRowCount.Properties.UseCtrlIncrement = false;
            this.txtRowCount.Size = new Size(80, 23);
            this.txtRowCount.TabIndex = 1;
            this.txtRowCount.EditValueChanged += new EventHandler(this.txtRowCount_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "行数";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IndexGridProperyPage";
            base.Size = new Size(312, 256);
            base.Load += new EventHandler(this.IndexGridProperyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtColumnCount.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtRowCount.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private SpinEdit txtColumnCount;
        private SpinEdit txtRowCount;
    }
}