using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class CoverageGeneralPropertyPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtName = new TextEdit();
            this.txtPercise = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label4 = new Label();
            this.txtFeatureCount = new TextEdit();
            this.btnBulid = new SimpleButton();
            this.btnClean = new SimpleButton();
            this.txtName.Properties.BeginInit();
            this.txtPercise.Properties.BeginInit();
            this.txtFeatureCount.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "精度:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new Size(48, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "要素类:";
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(64, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtName.Size = new Size(160, 19);
            this.txtName.TabIndex = 3;
            this.txtPercise.EditValue = "双精度";
            this.txtPercise.Location = new Point(60, 40);
            this.txtPercise.Name = "txtPercise";
            this.txtPercise.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtPercise.Properties.Appearance.Options.UseBackColor = true;
            this.txtPercise.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtPercise.Size = new Size(160, 19);
            this.txtPercise.TabIndex = 4;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(16, 96);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 120);
            this.listView1.TabIndex = 6;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 83;
            this.columnHeader_1.Text = "拓扑";
            this.columnHeader_1.Width = 81;
            this.columnHeader_2.Text = "有FAT吗";
            this.columnHeader_2.Width = 68;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 224);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "要素数量:";
            this.label4.Visible = false;
            this.txtFeatureCount.EditValue = "";
            this.txtFeatureCount.Location = new Point(88, 224);
            this.txtFeatureCount.Name = "txtFeatureCount";
            this.txtFeatureCount.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtFeatureCount.Properties.Appearance.Options.UseBackColor = true;
            this.txtFeatureCount.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtFeatureCount.Size = new Size(160, 19);
            this.txtFeatureCount.TabIndex = 8;
            this.btnBulid.Location = new Point(264, 104);
            this.btnBulid.Name = "btnBulid";
            this.btnBulid.Size = new Size(64, 24);
            this.btnBulid.TabIndex = 9;
            this.btnBulid.Text = "Bulid";
            this.btnBulid.Click += new EventHandler(this.btnBulid_Click);
            this.btnClean.Location = new Point(264, 136);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new Size(64, 24);
            this.btnClean.TabIndex = 10;
            this.btnClean.Text = "Clean";
            this.btnClean.Click += new EventHandler(this.btnClean_Click);
            base.Controls.Add(this.btnClean);
            base.Controls.Add(this.btnBulid);
            base.Controls.Add(this.txtFeatureCount);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.txtPercise);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CoverageGeneralPropertyPage";
            base.Size = new Size(344, 272);
            base.Load += new EventHandler(this.CoverageGeneralPropertyPage_Load);
            this.txtName.Properties.EndInit();
            this.txtPercise.Properties.EndInit();
            this.txtFeatureCount.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnBulid;
        private SimpleButton btnClean;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private TextEdit txtFeatureCount;
        private TextEdit txtName;
        private TextEdit txtPercise;
    }
}