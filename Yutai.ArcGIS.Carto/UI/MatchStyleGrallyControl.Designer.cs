using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MatchStyleGrallyControl
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
            this.cboFields = new ComboBoxEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnAddAllValues = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.cboLayers = new ComboBoxEdit();
            this.label4 = new Label();
            this.btnLookUp = new Button();
            this.cboStyleGrally = new ComboBoxEdit();
            this.cboFields.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            this.cboStyleGrally.Properties.BeginInit();
            base.SuspendLayout();
            this.cboFields.EditValue = "";
            this.cboFields.Location = new System.Drawing.Point(88, 40);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(168, 21);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "字段";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "匹配样式库";
            this.btnAddAllValues.Location = new System.Drawing.Point(8, 264);
            this.btnAddAllValues.Name = "btnAddAllValues";
            this.btnAddAllValues.Size = new Size(80, 24);
            this.btnAddAllValues.TabIndex = 8;
            this.btnAddAllValues.Text = "添加所有值";
            this.btnAddAllValues.Click += new EventHandler(this.btnAddAllValues_Click);
            this.simpleButton1.Location = new System.Drawing.Point(95, 264);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(72, 24);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "添加值";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(168, 264);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 24);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new System.Drawing.Point(256, 264);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 24);
            this.btnDeleteAll.TabIndex = 11;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(16, 120);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(320, 128);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "值";
            this.columnHeader_1.Text = "标注";
            this.columnHeader_1.Width = 108;
            this.columnHeader_2.Text = "数目";
            this.columnHeader_2.Width = 125;
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new System.Drawing.Point(88, 8);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayers.Size = new Size(168, 21);
            this.cboLayers.TabIndex = 14;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "图层";
            this.btnLookUp.Location = new System.Drawing.Point(264, 72);
            this.btnLookUp.Name = "btnLookUp";
            this.btnLookUp.Size = new Size(56, 24);
            this.btnLookUp.TabIndex = 15;
            this.btnLookUp.Text = "浏览...";
            this.btnLookUp.Click += new EventHandler(this.btnLookUp_Click);
            this.cboStyleGrally.EditValue = "";
            this.cboStyleGrally.Location = new System.Drawing.Point(88, 72);
            this.cboStyleGrally.Name = "cboStyleGrally";
            this.cboStyleGrally.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyleGrally.Size = new Size(168, 21);
            this.cboStyleGrally.TabIndex = 16;
            base.Controls.Add(this.cboStyleGrally);
            base.Controls.Add(this.btnLookUp);
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnAddAllValues);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.label1);
            base.Name = "MatchStyleGrallyControl";
            base.Size = new Size(360, 296);
            base.Load += new EventHandler(this.MatchStyleGrallyControl_Load);
            this.cboFields.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            this.cboStyleGrally.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAddAllValues;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private Button btnLookUp;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayers;
        private ComboBoxEdit cboStyleGrally;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private IContainer icontainer_0;
        private Label label1;
        private Label label2;
        private Label label4;
        private ListView listView1;
        private SimpleButton simpleButton1;
    }
}