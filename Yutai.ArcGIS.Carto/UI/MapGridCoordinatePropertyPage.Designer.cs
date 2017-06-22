using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapGridCoordinatePropertyPage
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
            this.groupBox2 = new GroupBox();
            this.styleButton1 = new StyleButton();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.spinSubTickCount = new SpinEdit();
            this.label2 = new Label();
            this.btnSubLineStyle = new StyleButton();
            this.btnMainLineStyle = new StyleButton();
            this.chkSubTickVisibility = new CheckEdit();
            this.chkTickVisibility = new CheckEdit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.spinSubTickCount.Properties.BeginInit();
            this.chkSubTickVisibility.Properties.BeginInit();
            this.chkTickVisibility.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.styleButton1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(240, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注";
            this.styleButton1.Location = new Point(104, 32);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(84, 32);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 3;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 39);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "文本样式:";
            this.groupBox1.Controls.Add(this.spinSubTickCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSubLineStyle);
            this.groupBox1.Controls.Add(this.btnMainLineStyle);
            this.groupBox1.Controls.Add(this.chkSubTickVisibility);
            this.groupBox1.Controls.Add(this.chkTickVisibility);
            this.groupBox1.Location = new Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 136);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标轴";
            int[] bits = new int[4];
            this.spinSubTickCount.EditValue = new decimal(bits);
            this.spinSubTickCount.Location = new Point(112, 96);
            this.spinSubTickCount.Name = "spinSubTickCount";
            this.spinSubTickCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinSubTickCount.Properties.UseCtrlIncrement = false;
            this.spinSubTickCount.Size = new Size(72, 23);
            this.spinSubTickCount.TabIndex = 5;
            this.spinSubTickCount.EditValueChanged += new EventHandler(this.spinSubTickCount_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(32, 104);
            this.label2.Name = "label2";
            this.label2.Size = new Size(79, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "主分划刻度数";
            this.btnSubLineStyle.Location = new Point(104, 61);
            this.btnSubLineStyle.Name = "btnSubLineStyle";
            this.btnSubLineStyle.Size = new Size(72, 24);
            this.btnSubLineStyle.Style = null;
            this.btnSubLineStyle.TabIndex = 3;
            this.btnSubLineStyle.Click += new EventHandler(this.btnSubLineStyle_Click);
            this.btnMainLineStyle.Location = new Point(104, 23);
            this.btnMainLineStyle.Name = "btnMainLineStyle";
            this.btnMainLineStyle.Size = new Size(72, 25);
            this.btnMainLineStyle.Style = null;
            this.btnMainLineStyle.TabIndex = 2;
            this.btnMainLineStyle.Click += new EventHandler(this.btnMainLineStyle_Click);
            this.chkSubTickVisibility.Location = new Point(16, 62);
            this.chkSubTickVisibility.Name = "chkSubTickVisibility";
            this.chkSubTickVisibility.Properties.Caption = "次划分刻度";
            this.chkSubTickVisibility.Size = new Size(88, 19);
            this.chkSubTickVisibility.TabIndex = 1;
            this.chkSubTickVisibility.CheckedChanged += new EventHandler(this.chkSubTickVisibility_CheckedChanged);
            this.chkTickVisibility.Location = new Point(16, 25);
            this.chkTickVisibility.Name = "chkTickVisibility";
            this.chkTickVisibility.Properties.Caption = "主划分刻度";
            this.chkTickVisibility.Size = new Size(88, 19);
            this.chkTickVisibility.TabIndex = 0;
            this.chkTickVisibility.CheckedChanged += new EventHandler(this.chkTickVisibility_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridCoordinatePropertyPage";
            base.Size = new Size(328, 312);
            base.Load += new EventHandler(this.MapGridCoordinatePropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.spinSubTickCount.Properties.EndInit();
            this.chkSubTickVisibility.Properties.EndInit();
            this.chkTickVisibility.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnMainLineStyle;
        private StyleButton btnSubLineStyle;
        private CheckEdit chkSubTickVisibility;
        private CheckEdit chkTickVisibility;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private SpinEdit spinSubTickCount;
        private StyleButton styleButton1;
    }
}