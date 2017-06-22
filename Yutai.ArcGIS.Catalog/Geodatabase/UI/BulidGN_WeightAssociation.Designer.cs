using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGN_WeightAssociation
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.comboBoxEdit = new ComboBoxEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label2 = new Label();
            this.comboBoxEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "网络权重";
            this.comboBoxEdit.EditValue = "";
            this.comboBoxEdit.Location = new Point(16, 40);
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit.Size = new Size(240, 23);
            this.comboBoxEdit.TabIndex = 1;
            this.comboBoxEdit.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_SelectedIndexChanged);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(24, 104);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 104);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.listView1.MouseDown += new MouseEventHandler(this.listView1_MouseDown);
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 102;
            this.columnHeader_1.Text = "字段";
            this.columnHeader_1.Width = 122;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new Size(153, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "权重和要素类字段关联设置";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.comboBoxEdit);
            base.Controls.Add(this.label1);
            base.Name = "BulidGN_WeightAssociation";
            base.Size = new Size(304, 232);
            base.Load += new EventHandler(this.BulidGN_WeightAssociation_Load);
            this.comboBoxEdit.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private System.Windows.Forms.ComboBox comboBox_0;
        private ComboBoxEdit comboBoxEdit;
        private Label label1;
        private Label label2;
        private ListView listView1;
    }
}