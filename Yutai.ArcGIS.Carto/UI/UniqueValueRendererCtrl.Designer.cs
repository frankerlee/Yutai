using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class UniqueValueRendererCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UniqueValueRendererCtrl));
            this.label1 = new Label();
            this.label2 = new Label();
            this.cboColorRamp = new StyleComboBox(this.icontainer_0);
            this.btnAddAllValues = new SimpleButton();
            this.btnAddValue = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.cboFields = new ComboBoxEdit();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.menuitemGroup = new ToolStripMenuItem();
            this.menuitemUnGroup = new ToolStripMenuItem();
            this.cboFields.Properties.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "字段";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "颜色方案";
            this.cboColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboColorRamp.DropDownWidth = 160;
            this.cboColorRamp.Location = new System.Drawing.Point(248, 8);
            this.cboColorRamp.Name = "cboColorRamp";
            this.cboColorRamp.Size = new Size(144, 22);
            this.cboColorRamp.TabIndex = 5;
            this.cboColorRamp.SelectedIndexChanged += new EventHandler(this.cboColorRamp_SelectedIndexChanged);
            this.btnAddAllValues.Location = new System.Drawing.Point(76, 208);
            this.btnAddAllValues.Name = "btnAddAllValues";
            this.btnAddAllValues.Size = new Size(80, 24);
            this.btnAddAllValues.TabIndex = 8;
            this.btnAddAllValues.Text = "添加所有值";
            this.btnAddAllValues.Click += new EventHandler(this.btnAddAllValues_Click);
            this.btnAddValue.Location = new System.Drawing.Point(160, 208);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new Size(72, 24);
            this.btnAddValue.TabIndex = 9;
            this.btnAddValue.Text = "添加值";
            this.btnAddValue.Click += new EventHandler(this.btnAddValue_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(232, 208);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 24);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new System.Drawing.Point(312, 208);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 24);
            this.btnDeleteAll.TabIndex = 11;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.listView1.BackColor = SystemColors.Window;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_3, this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(368, 160);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.columnHeader_3.Text = "符号";
            this.columnHeader_0.Text = "值";
            this.columnHeader_0.Width = 102;
            this.columnHeader_1.Text = "标注";
            this.columnHeader_1.Width = 119;
            this.columnHeader_2.Text = "数目";
            this.columnHeader_2.Width = 63;
            this.cboFields.EditValue = "";
            this.cboFields.Location = new System.Drawing.Point(48, 7);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(136, 21);
            this.cboFields.TabIndex = 48;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new System.Drawing.Point(384, 96);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(24, 24);
            this.btnMoveDown.TabIndex = 50;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new System.Drawing.Point(384, 56);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(24, 24);
            this.btnMoveUp.TabIndex = 49;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.menuitemGroup, this.menuitemUnGroup });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(153, 70);
            this.menuitemGroup.Name = "menuitemGroup";
            this.menuitemGroup.Size = new Size(152, 22);
            this.menuitemGroup.Text = "值分组";
            this.menuitemGroup.Click += new EventHandler(this.menuitemGroup_Click);
            this.menuitemUnGroup.Name = "menuitemUnGroup";
            this.menuitemUnGroup.Size = new Size(152, 22);
            this.menuitemUnGroup.Text = "取消分组";
            this.menuitemUnGroup.Click += new EventHandler(this.menuitemUnGroup_Click);
            base.Controls.Add(this.btnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddValue);
            base.Controls.Add(this.btnAddAllValues);
            base.Controls.Add(this.cboColorRamp);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "UniqueValueRendererCtrl";
            base.Size = new Size(424, 248);
            base.Load += new EventHandler(this.UniqueValueRendererCtrl_Load);
            this.cboFields.Properties.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAddAllValues;
        private SimpleButton btnAddValue;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private StyleComboBox cboColorRamp;
        private ComboBoxEdit cboFields;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ContextMenuStrip contextMenuStrip1;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private RenderInfoListView listView1;
        private ToolStripMenuItem menuitemGroup;
        private ToolStripMenuItem menuitemUnGroup;
    }
}