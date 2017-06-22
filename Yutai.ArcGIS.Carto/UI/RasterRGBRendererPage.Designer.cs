using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class RasterRGBRendererPage
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.exListView1 = new EXListView();
            this.txtBackgroundR = new TextBox();
            this.btnStretch = new Button();
            this.label3 = new Label();
            this.chkShowBackground = new CheckBox();
            this.colorEdit2 = new ColorEdit();
            this.colorEdit1 = new ColorEdit();
            this.label4 = new Label();
            this.txtBackgroundG = new TextBox();
            this.txtBackgroundB = new TextBox();
            this.colorEdit2.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.exListView1.FullRowSelect = true;
            this.exListView1.Location = new Point(14, 16);
            this.exListView1.Name = "exListView1";
            this.exListView1.OwnerDraw = true;
            this.exListView1.Size = new Size(294, 97);
            this.exListView1.TabIndex = 0;
            this.exListView1.UseCompatibleStateImageBehavior = false;
            this.exListView1.View = View.Details;
            this.txtBackgroundR.Location = new Point(104, 117);
            this.txtBackgroundR.Name = "txtBackgroundR";
            this.txtBackgroundR.Size = new Size(33, 21);
            this.txtBackgroundR.TabIndex = 65;
            this.txtBackgroundR.Text = "0";
            this.txtBackgroundR.TextChanged += new EventHandler(this.txtBackgroundR_TextChanged);
            this.btnStretch.Location = new Point(229, 169);
            this.btnStretch.Name = "btnStretch";
            this.btnStretch.Size = new Size(94, 23);
            this.btnStretch.TabIndex = 64;
            this.btnStretch.Text = "拉伸设置...";
            this.btnStretch.UseVisualStyleBackColor = true;
            this.btnStretch.Click += new EventHandler(this.btnStretch_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(227, 145);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 63;
            this.label3.Text = "空值显示颜色";
            this.chkShowBackground.AutoSize = true;
            this.chkShowBackground.Location = new Point(14, 119);
            this.chkShowBackground.Name = "chkShowBackground";
            this.chkShowBackground.Size = new Size(84, 16);
            this.chkShowBackground.TabIndex = 62;
            this.chkShowBackground.Text = "显示背景值";
            this.chkShowBackground.UseVisualStyleBackColor = true;
            this.chkShowBackground.CheckedChanged += new EventHandler(this.chkShowBackground_CheckedChanged);
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(310, 141);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(48, 21);
            this.colorEdit2.TabIndex = 61;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(310, 114);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 60;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(227, 120);
            this.label4.Name = "label4";
            this.label4.Size = new Size(65, 12);
            this.label4.TabIndex = 59;
            this.label4.Text = "背景值颜色";
            this.txtBackgroundG.Location = new Point(143, 117);
            this.txtBackgroundG.Name = "txtBackgroundG";
            this.txtBackgroundG.Size = new Size(33, 21);
            this.txtBackgroundG.TabIndex = 66;
            this.txtBackgroundG.Text = "0";
            this.txtBackgroundG.TextChanged += new EventHandler(this.txtBackgroundG_TextChanged);
            this.txtBackgroundB.Location = new Point(182, 117);
            this.txtBackgroundB.Name = "txtBackgroundB";
            this.txtBackgroundB.Size = new Size(33, 21);
            this.txtBackgroundB.TabIndex = 67;
            this.txtBackgroundB.Text = "0";
            this.txtBackgroundB.TextChanged += new EventHandler(this.txtBackgroundB_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.txtBackgroundB);
            base.Controls.Add(this.txtBackgroundG);
            base.Controls.Add(this.txtBackgroundR);
            base.Controls.Add(this.btnStretch);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.chkShowBackground);
            base.Controls.Add(this.colorEdit2);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.exListView1);
            base.Name = "RasterRGBRendererPage";
            base.Size = new Size(381, 275);
            base.Load += new EventHandler(this.RasterRGBRendererPage_Load);
            this.colorEdit2.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnStretch;
        private CheckBox chkShowBackground;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private System.Windows.Forms.ComboBox comboBox_0;
        private EXListView exListView1;
        private Label label3;
        private Label label4;
        private TextBox txtBackgroundB;
        private TextBox txtBackgroundG;
        private TextBox txtBackgroundR;
    }
}