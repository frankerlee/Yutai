using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class ChartRendererCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartRendererCtrl));
            this.chkOverposter = new CheckEdit();
            this.lblBackground = new Label();
            this.colorRampComboBox1 = new ColorRampComboBox();
            this.label3 = new Label();
            this.btnStyle = new StyleButton();
            this.groupBox1 = new GroupBox();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.SelectFieldslistView = new RenderInfoListView();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_0 = new ColumnHeader();
            this.btnUnSelectAll = new SimpleButton();
            this.btnUnSelect = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.FieldsListBoxCtrl = new ListBoxControl();
            this.chkOverposter.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.FieldsListBoxCtrl).BeginInit();
            base.SuspendLayout();
            this.chkOverposter.Location = new System.Drawing.Point(16, 208);
            this.chkOverposter.Name = "chkOverposter";
            this.chkOverposter.Properties.Caption = "防止图表被覆盖";
            this.chkOverposter.Size = new Size(120, 19);
            this.chkOverposter.TabIndex = 39;
            this.chkOverposter.CheckedChanged += new EventHandler(this.chkOverposter_CheckedChanged);
            this.lblBackground.AutoSize = true;
            this.lblBackground.Location = new System.Drawing.Point(8, 176);
            this.lblBackground.Name = "lblBackground";
            this.lblBackground.Size = new Size(29, 17);
            this.lblBackground.TabIndex = 37;
            this.lblBackground.Text = "背景";
            this.colorRampComboBox1.DrawMode = DrawMode.OwnerDrawVariable;
            this.colorRampComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorRampComboBox1.Location = new System.Drawing.Point(264, 176);
            this.colorRampComboBox1.Name = "colorRampComboBox1";
            this.colorRampComboBox1.Size = new Size(136, 22);
            this.colorRampComboBox1.TabIndex = 36;
            this.colorRampComboBox1.SelectedIndexChanged += new EventHandler(this.colorRampComboBox1_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 176);
            this.label3.Name = "label3";
            this.label3.Size = new Size(54, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "颜色模型";
            this.btnStyle.Location = new System.Drawing.Point(48, 168);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(72, 32);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 41;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.SelectFieldslistView);
            this.groupBox1.Controls.Add(this.btnUnSelectAll);
            this.groupBox1.Controls.Add(this.btnUnSelect);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.FieldsListBoxCtrl);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(400, 152);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段集";
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new System.Drawing.Point(368, 80);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(24, 24);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(368, 40);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(24, 24);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.SelectFieldslistView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_1, this.columnHeader_0 });
            this.SelectFieldslistView.FullRowSelect = true;
            this.SelectFieldslistView.Location = new System.Drawing.Point(168, 32);
            this.SelectFieldslistView.Name = "SelectFieldslistView";
            this.SelectFieldslistView.Size = new Size(192, 104);
            this.SelectFieldslistView.TabIndex = 4;
            this.SelectFieldslistView.View = View.Details;
            this.SelectFieldslistView.SelectedIndexChanged += new EventHandler(this.SelectFieldslistView_SelectedIndexChanged);
            this.columnHeader_1.Text = "符号";
            this.columnHeader_0.Text = "字段";
            this.columnHeader_0.Width = 95;
            this.btnUnSelectAll.Location = new System.Drawing.Point(128, 104);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new Size(32, 24);
            this.btnUnSelectAll.TabIndex = 3;
            this.btnUnSelectAll.Text = "<<";
            this.btnUnSelectAll.Click += new EventHandler(this.btnUnSelectAll_Click);
            this.btnUnSelect.Enabled = false;
            this.btnUnSelect.Location = new System.Drawing.Point(128, 72);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new Size(32, 24);
            this.btnUnSelect.TabIndex = 2;
            this.btnUnSelect.Text = "<";
            this.btnUnSelect.Click += new EventHandler(this.btnUnSelect_Click);
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new System.Drawing.Point(128, 40);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(32, 24);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = ">";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.FieldsListBoxCtrl.ItemHeight = 17;
            this.FieldsListBoxCtrl.Location = new System.Drawing.Point(8, 24);
            this.FieldsListBoxCtrl.Name = "FieldsListBoxCtrl";
            this.FieldsListBoxCtrl.SelectionMode = SelectionMode.MultiExtended;
            this.FieldsListBoxCtrl.Size = new Size(112, 120);
            this.FieldsListBoxCtrl.TabIndex = 0;
            this.FieldsListBoxCtrl.SelectedIndexChanged += new EventHandler(this.FieldsListBoxCtrl_SelectedIndexChanged);
            base.Controls.Add(this.btnStyle);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.chkOverposter);
            base.Controls.Add(this.lblBackground);
            base.Controls.Add(this.colorRampComboBox1);
            base.Controls.Add(this.label3);
            base.Name = "ChartRendererCtrl";
            base.Size = new Size(416, 240);
            base.Load += new EventHandler(this.ChartRendererCtrl_Load);
            this.chkOverposter.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.FieldsListBoxCtrl).EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private SimpleButton btnSelect;
        private StyleButton btnStyle;
        private SimpleButton btnUnSelect;
        private SimpleButton btnUnSelectAll;
        private CheckEdit chkOverposter;
        private ColorRampComboBox colorRampComboBox1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ListBoxControl FieldsListBoxCtrl;
        private GroupBox groupBox1;
        private Label label3;
        private Label lblBackground;
        private RenderInfoListView SelectFieldslistView;
    }
}