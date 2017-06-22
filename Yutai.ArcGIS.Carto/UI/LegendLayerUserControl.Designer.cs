using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LegendLayerUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendLayerUserControl));
            this.groupBox1 = new GroupBox();
            this.listLegendLayers = new ListBoxControl();
            this.listMapLayers = new ListBoxControl();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnUnSelectAll = new SimpleButton();
            this.btnUnSelect = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.label2 = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.spinEdit1 = new SpinEdit();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.listLegendLayers).BeginInit();
            ((ISupportInitialize) this.listMapLayers).BeginInit();
            this.spinEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.listLegendLayers);
            this.groupBox1.Controls.Add(this.listMapLayers);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnUnSelectAll);
            this.groupBox1.Controls.Add(this.btnUnSelect);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(344, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择要创建图例的图层";
            this.listLegendLayers.ItemHeight = 15;
            this.listLegendLayers.Location = new Point(176, 48);
            this.listLegendLayers.Name = "listLegendLayers";
            this.listLegendLayers.Size = new Size(112, 144);
            this.listLegendLayers.TabIndex = 13;
            this.listLegendLayers.SelectedIndexChanged += new EventHandler(this.listLegendLayers_SelectedIndexChanged);
            this.listMapLayers.ItemHeight = 15;
            this.listMapLayers.Location = new Point(8, 48);
            this.listMapLayers.Name = "listMapLayers";
            this.listMapLayers.Size = new Size(104, 136);
            this.listMapLayers.TabIndex = 12;
            this.listMapLayers.SelectedIndexChanged += new EventHandler(this.listMapLayers_SelectedIndexChanged);
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(296, 88);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(32, 24);
            this.btnMoveDown.TabIndex = 9;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(296, 48);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(32, 24);
            this.btnMoveUp.TabIndex = 8;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnUnSelectAll.Location = new Point(128, 168);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new Size(32, 24);
            this.btnUnSelectAll.TabIndex = 7;
            this.btnUnSelectAll.Text = "<<";
            this.btnUnSelectAll.Click += new EventHandler(this.btnUnSelectAll_Click);
            this.btnUnSelect.Enabled = false;
            this.btnUnSelect.Location = new Point(128, 136);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new Size(32, 24);
            this.btnUnSelect.TabIndex = 6;
            this.btnUnSelect.Text = "<";
            this.btnUnSelect.Click += new EventHandler(this.btnUnSelect_Click);
            this.btnSelectAll.Location = new Point(128, 80);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(32, 24);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new Point(128, 48);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(32, 24);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = ">";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(176, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(42, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "图例项";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "地图图层:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(24, 227);
            this.label3.Name = "label3";
            this.label3.Size = new Size(54, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "图例列数";
            this.label3.Visible = false;
            int[] bits = new int[4];
            bits[0] = 1;
            this.spinEdit1.EditValue = new decimal(bits);
            this.spinEdit1.Location = new Point(88, 223);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit1.Properties.UseCtrlIncrement = false;
            this.spinEdit1.Size = new Size(104, 23);
            this.spinEdit1.TabIndex = 2;
            this.spinEdit1.Visible = false;
            this.spinEdit1.EditValueChanged += new EventHandler(this.spinEdit1_EditValueChanged);
            base.Controls.Add(this.spinEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendLayerUserControl";
            base.Size = new Size(400, 264);
            base.Load += new EventHandler(this.LegendLayerUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.listLegendLayers).EndInit();
            ((ISupportInitialize) this.listMapLayers).EndInit();
            this.spinEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnSelect;
        private SimpleButton btnSelectAll;
        private SimpleButton btnUnSelect;
        private SimpleButton btnUnSelectAll;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBoxControl listLegendLayers;
        private ListBoxControl listMapLayers;
        private SpinEdit spinEdit1;
    }
}