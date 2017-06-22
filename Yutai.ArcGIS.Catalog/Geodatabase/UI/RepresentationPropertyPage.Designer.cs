using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class RepresentationPropertyPage
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label1 = new Label();
            this.btnNew = new Button();
            this.btnDelete = new Button();
            this.btnProperty = new Button();
            this.btnReName = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(14, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(293, 242);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 86;
            this.columnHeader_1.Text = "RuleID";
            this.columnHeader_1.Width = 75;
            this.columnHeader_2.Text = "Override";
            this.columnHeader_2.Width = 102;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "制图表现列表";
            this.btnNew.Location = new Point(313, 27);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(57, 32);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnDelete.Location = new Point(313, 65);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(57, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnProperty.Location = new Point(313, 103);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(57, 32);
            this.btnProperty.TabIndex = 4;
            this.btnProperty.Text = "属性";
            this.btnProperty.UseVisualStyleBackColor = true;
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.btnReName.Location = new Point(313, 141);
            this.btnReName.Name = "btnReName";
            this.btnReName.Size = new Size(57, 32);
            this.btnReName.TabIndex = 5;
            this.btnReName.Text = "重命名";
            this.btnReName.UseVisualStyleBackColor = true;
            this.btnReName.Click += new EventHandler(this.btnReName_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnReName);
            base.Controls.Add(this.btnProperty);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.Name = "RepresentationPropertyPage";
            base.Size = new Size(382, 321);
            base.Load += new EventHandler(this.RepresentationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnDelete;
        private Button btnNew;
        private Button btnProperty;
        private Button btnReName;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Label label1;
        private ListView listView1;
    }
}