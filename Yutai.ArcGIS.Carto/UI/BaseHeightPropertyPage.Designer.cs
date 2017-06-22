using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class BaseHeightPropertyPage
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
            this.groupBox1 = new GroupBox();
            this.btnOpenFile = new Button();
            this.cboSufer = new ComboBox();
            this.txtBaseExpression = new TextBox();
            this.rdoBaseShape = new RadioButton();
            this.rdoBaseSurface = new RadioButton();
            this.rdoBaseExpression = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.txtZFeator = new TextBox();
            this.comboBox2 = new ComboBox();
            this.label1 = new Label();
            this.groupBox3 = new GroupBox();
            this.txtOffset = new TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnOpenFile);
            this.groupBox1.Controls.Add(this.cboSufer);
            this.groupBox1.Controls.Add(this.txtBaseExpression);
            this.groupBox1.Controls.Add(this.rdoBaseShape);
            this.groupBox1.Controls.Add(this.rdoBaseSurface);
            this.groupBox1.Controls.Add(this.rdoBaseExpression);
            this.groupBox1.Location = new Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(390, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基准高程";
            this.btnOpenFile.Location = new Point(318, 97);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new Size(30, 23);
            this.btnOpenFile.TabIndex = 6;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new EventHandler(this.btnOpenFile_Click);
            this.cboSufer.FormattingEnabled = true;
            this.cboSufer.Location = new Point(20, 100);
            this.cboSufer.Name = "cboSufer";
            this.cboSufer.Size = new Size(292, 20);
            this.cboSufer.TabIndex = 5;
            this.txtBaseExpression.Location = new Point(20, 43);
            this.txtBaseExpression.Name = "txtBaseExpression";
            this.txtBaseExpression.Size = new Size(292, 21);
            this.txtBaseExpression.TabIndex = 4;
            this.txtBaseExpression.Text = "0";
            this.rdoBaseShape.AutoSize = true;
            this.rdoBaseShape.Location = new Point(20, 126);
            this.rdoBaseShape.Name = "rdoBaseShape";
            this.rdoBaseShape.Size = new Size(161, 16);
            this.rdoBaseShape.TabIndex = 3;
            this.rdoBaseShape.Text = "使用图层Z值设置图层高程";
            this.rdoBaseShape.UseVisualStyleBackColor = true;
            this.rdoBaseSurface.AutoSize = true;
            this.rdoBaseSurface.Location = new Point(20, 81);
            this.rdoBaseSurface.Name = "rdoBaseSurface";
            this.rdoBaseSurface.Size = new Size(131, 16);
            this.rdoBaseSurface.TabIndex = 2;
            this.rdoBaseSurface.Text = "丛表面获取图层高程";
            this.rdoBaseSurface.UseVisualStyleBackColor = true;
            this.rdoBaseExpression.AutoSize = true;
            this.rdoBaseExpression.Checked = true;
            this.rdoBaseExpression.Location = new Point(20, 21);
            this.rdoBaseExpression.Name = "rdoBaseExpression";
            this.rdoBaseExpression.Size = new Size(191, 16);
            this.rdoBaseExpression.TabIndex = 0;
            this.rdoBaseExpression.TabStop = true;
            this.rdoBaseExpression.Text = "使用常量或表达式设置图层高程";
            this.rdoBaseExpression.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.txtZFeator);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(13, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(390, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Z单位转换";
            this.txtZFeator.Location = new Point(304, 23);
            this.txtZFeator.Name = "txtZFeator";
            this.txtZFeator.Size = new Size(80, 21);
            this.txtZFeator.TabIndex = 2;
            this.txtZFeator.Text = "1.0000";
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "英尺到米", "米到英尺", "自定义" });
            this.comboBox2.Location = new Point(186, 23);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(94, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.Text = "自定义";
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(18, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(161, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高程单位和场景单位转换因子";
            this.groupBox3.Controls.Add(this.txtOffset);
            this.groupBox3.Location = new Point(13, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(390, 52);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "偏移";
            this.txtOffset.Location = new Point(6, 20);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(292, 21);
            this.txtOffset.TabIndex = 5;
            this.txtOffset.Text = "0";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BaseHeightPropertyPage";
            base.Size = new Size(427, 326);
            base.Load += new EventHandler(this.BaseHeightPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private Button btnOpenFile;
        private ComboBox cboSufer;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private RadioButton rdoBaseExpression;
        private RadioButton rdoBaseShape;
        private RadioButton rdoBaseSurface;
        private TextBox txtBaseExpression;
        private TextBox txtOffset;
        private TextBox txtZFeator;
    }
}