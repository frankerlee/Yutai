using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class TinUniqueRenderPropertyPage
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
            this.listView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.btnDeleteAll = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAddValue = new SimpleButton();
            this.btnAddAllValues = new SimpleButton();
            this.cboColorRamp = new StyleComboBox(this.icontainer_0);
            this.groupBox1 = new GroupBox();
            this.lblLabelInfo = new Label();
            this.groupBox2 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 57);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(368, 160);
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "符号";
            this.columnHeader_1.Text = "值";
            this.columnHeader_1.Width = 102;
            this.columnHeader_2.Text = "标注";
            this.columnHeader_2.Width = 119;
            this.columnHeader_3.Text = "数目";
            this.columnHeader_3.Width = 63;
            this.btnDeleteAll.Location = new System.Drawing.Point(290, 223);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 24);
            this.btnDeleteAll.TabIndex = 18;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(210, 223);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 24);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAddValue.Location = new System.Drawing.Point(138, 223);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new Size(72, 24);
            this.btnAddValue.TabIndex = 16;
            this.btnAddValue.Text = "添加值";
            this.btnAddValue.Click += new EventHandler(this.btnAddValue_Click);
            this.btnAddAllValues.Location = new System.Drawing.Point(54, 223);
            this.btnAddAllValues.Name = "btnAddAllValues";
            this.btnAddAllValues.Size = new Size(80, 24);
            this.btnAddAllValues.TabIndex = 15;
            this.btnAddAllValues.Text = "添加所有值";
            this.btnAddAllValues.Click += new EventHandler(this.btnAddAllValues_Click);
            this.cboColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboColorRamp.DropDownWidth = 160;
            this.cboColorRamp.Location = new System.Drawing.Point(8, 18);
            this.cboColorRamp.Name = "cboColorRamp";
            this.cboColorRamp.Size = new Size(176, 22);
            this.cboColorRamp.TabIndex = 14;
            this.cboColorRamp.SelectedIndexChanged += new EventHandler(this.cboColorRamp_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.lblLabelInfo);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(172, 48);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签值字段";
            this.lblLabelInfo.AutoSize = true;
            this.lblLabelInfo.Location = new System.Drawing.Point(12, 21);
            this.lblLabelInfo.Name = "lblLabelInfo";
            this.lblLabelInfo.Size = new Size(0, 12);
            this.lblLabelInfo.TabIndex = 0;
            this.groupBox2.Controls.Add(this.cboColorRamp);
            this.groupBox2.Location = new System.Drawing.Point(181, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(190, 48);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "颜色配置";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddValue);
            base.Controls.Add(this.btnAddAllValues);
            base.Name = "TinUniqueRenderPropertyPage";
            base.Size = new Size(389, 258);
            base.Load += new EventHandler(this.TinUniqueRenderPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddAllValues;
        private SimpleButton btnAddValue;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private StyleComboBox cboColorRamp;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ITinUniqueValueRenderer itinUniqueValueRenderer_0;
        private Label lblLabelInfo;
        private RenderInfoListView listView1;
    }
}