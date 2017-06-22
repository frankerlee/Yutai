using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LegendSetupUserControl
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
            this.icontainer_0 = new Container();
            this.groupBox1 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.icontainer_0);
            this.cboLinePatches = new StyleComboBox(this.icontainer_0);
            this.label6 = new Label();
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtHeight = new TextEdit();
            this.txtWidth = new TextEdit();
            this.label3 = new Label();
            this.label1 = new Label();
            this.listLegendLayers = new ListBoxControl();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            ((ISupportInitialize) this.listLegendLayers).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboAreaPatches);
            this.groupBox1.Controls.Add(this.cboLinePatches);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(168, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(184, 168);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "区块";
            this.cboAreaPatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboAreaPatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAreaPatches.DropDownWidth = 160;
            this.cboAreaPatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.cboAreaPatches.Location = new System.Drawing.Point(56, 128);
            this.cboAreaPatches.Name = "cboAreaPatches";
            this.cboAreaPatches.Size = new Size(72, 31);
            this.cboAreaPatches.TabIndex = 9;
            this.cboAreaPatches.SelectedIndexChanged += new EventHandler(this.cboAreaPatches_SelectedIndexChanged);
            this.cboLinePatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboLinePatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLinePatches.DropDownWidth = 160;
            this.cboLinePatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.cboLinePatches.Location = new System.Drawing.Point(56, 88);
            this.cboLinePatches.Name = "cboLinePatches";
            this.cboLinePatches.Size = new Size(72, 31);
            this.cboLinePatches.TabIndex = 8;
            this.cboLinePatches.SelectedIndexChanged += new EventHandler(this.cboLinePatches_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 136);
            this.label6.Name = "label6";
            this.label6.Size = new Size(23, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "面:";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 96);
            this.label7.Name = "label7";
            this.label7.Size = new Size(23, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "线:";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(17, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "点";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "点";
            this.txtHeight.EditValue = "";
            this.txtHeight.Location = new System.Drawing.Point(56, 48);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(80, 23);
            this.txtHeight.TabIndex = 3;
            this.txtHeight.EditValueChanged += new EventHandler(this.txtHeight_EditValueChanged);
            this.txtHeight.TextChanged += new EventHandler(this.txtHeight_TextChanged);
            this.txtWidth.EditValue = "";
            this.txtWidth.Location = new System.Drawing.Point(56, 16);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(80, 23);
            this.txtWidth.TabIndex = 2;
            this.txtWidth.EditValueChanged += new EventHandler(this.txtWidth_EditValueChanged);
            this.txtWidth.TextChanged += new EventHandler(this.txtWidth_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "高度:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "宽度:";
            this.listLegendLayers.ItemHeight = 15;
            this.listLegendLayers.Location = new System.Drawing.Point(8, 40);
            this.listLegendLayers.Name = "listLegendLayers";
            this.listLegendLayers.Size = new Size(144, 160);
            this.listLegendLayers.TabIndex = 8;
            this.listLegendLayers.SelectedIndexChanged += new EventHandler(this.listLegendLayers_SelectedIndexChanged);
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new Size(88, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "图例项";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listLegendLayers);
            base.Controls.Add(this.label2);
            base.Name = "LegendSetupUserControl";
            base.Size = new Size(400, 272);
            base.Load += new EventHandler(this.LegendSetupUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            ((ISupportInitialize) this.listLegendLayers).EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleComboBox cboAreaPatches;
        private StyleComboBox cboLinePatches;
        private GroupBox groupBox1;
        private IArray iarray_0;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private ListBoxControl listLegendLayers;
        private TextEdit txtHeight;
        private TextEdit txtWidth;
    }
}