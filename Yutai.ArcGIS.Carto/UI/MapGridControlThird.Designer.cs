using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapGridControlThird
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnSubLineStyle = new StyleButton();
            this.btnMainLineStyle = new StyleButton();
            this.checkEdit2 = new CheckEdit();
            this.checkEdit1 = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.styleButton1 = new StyleButton();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.checkEdit2.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSubLineStyle);
            this.groupBox1.Controls.Add(this.btnMainLineStyle);
            this.groupBox1.Controls.Add(this.checkEdit2);
            this.groupBox1.Controls.Add(this.checkEdit1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标轴";
            this.btnSubLineStyle.Location = new Point(104, 61);
            this.btnSubLineStyle.Name = "btnSubLineStyle";
            this.btnSubLineStyle.Size = new Size(72, 24);
            this.btnSubLineStyle.Style = null;
            this.btnSubLineStyle.TabIndex = 3;
            this.btnMainLineStyle.Location = new Point(104, 23);
            this.btnMainLineStyle.Name = "btnMainLineStyle";
            this.btnMainLineStyle.Size = new Size(72, 25);
            this.btnMainLineStyle.Style = null;
            this.btnMainLineStyle.TabIndex = 2;
            this.checkEdit2.EditValue = false;
            this.checkEdit2.Location = new Point(16, 62);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "次划分刻度";
            this.checkEdit2.Size = new Size(88, 19);
            this.checkEdit2.TabIndex = 1;
            this.checkEdit1.EditValue = false;
            this.checkEdit1.Location = new Point(16, 25);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "主划分刻度";
            this.checkEdit1.Size = new Size(88, 19);
            this.checkEdit1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.styleButton1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(240, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注";
            this.styleButton1.Location = new Point(104, 32);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(84, 32);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本样式:";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridControlThird";
            base.Size = new Size(272, 240);
            base.VisibleChanged += new EventHandler(this.MapGridControlThird_VisibleChanged);
            base.Load += new EventHandler(this.MapGridControlThird_Load);
            this.groupBox1.ResumeLayout(false);
            this.checkEdit2.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private StyleButton btnMainLineStyle;
        private StyleButton btnSubLineStyle;
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private StyleButton styleButton1;
    }
}