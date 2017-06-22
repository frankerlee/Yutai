using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class IndexSetCtrl
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
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.listBoxIndexName = new ListBox();
            this.btnAddIndex = new SimpleButton();
            this.btnDeleteIndex = new SimpleButton();
            this.lblIsUnique = new Label();
            this.lblIsAscending = new Label();
            this.listBoxField = new ListBox();
            this.label1 = new Label();
            this.lblGridSize3 = new Label();
            this.lblGridSize2 = new Label();
            this.lblGridSize1 = new Label();
            this.btnDeleteGridIndex = new SimpleButton();
            this.btnAddGridIndex = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBoxField);
            this.groupBox1.Controls.Add(this.lblIsAscending);
            this.groupBox1.Controls.Add(this.lblIsUnique);
            this.groupBox1.Controls.Add(this.btnDeleteIndex);
            this.groupBox1.Controls.Add(this.btnAddIndex);
            this.groupBox1.Controls.Add(this.listBoxIndexName);
            this.groupBox1.Location = new Point(24, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 256);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性索引";
            this.groupBox2.Controls.Add(this.btnDeleteGridIndex);
            this.groupBox2.Controls.Add(this.btnAddGridIndex);
            this.groupBox2.Controls.Add(this.lblGridSize3);
            this.groupBox2.Controls.Add(this.lblGridSize2);
            this.groupBox2.Controls.Add(this.lblGridSize1);
            this.groupBox2.Location = new Point(24, 280);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(280, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "空间索引";
            this.listBoxIndexName.ItemHeight = 12;
            this.listBoxIndexName.Location = new Point(8, 16);
            this.listBoxIndexName.Name = "listBoxIndexName";
            this.listBoxIndexName.Size = new Size(192, 88);
            this.listBoxIndexName.TabIndex = 0;
            this.btnAddIndex.Location = new Point(216, 16);
            this.btnAddIndex.Name = "btnAddIndex";
            this.btnAddIndex.Size = new Size(48, 24);
            this.btnAddIndex.TabIndex = 1;
            this.btnAddIndex.Text = "添加...";
            this.btnDeleteIndex.Location = new Point(216, 48);
            this.btnDeleteIndex.Name = "btnDeleteIndex";
            this.btnDeleteIndex.Size = new Size(48, 24);
            this.btnDeleteIndex.TabIndex = 2;
            this.btnDeleteIndex.Text = "删除";
            this.lblIsUnique.AutoSize = true;
            this.lblIsUnique.Location = new Point(16, 112);
            this.lblIsUnique.Name = "lblIsUnique";
            this.lblIsUnique.Size = new Size(48, 17);
            this.lblIsUnique.TabIndex = 3;
            this.lblIsUnique.Text = "唯一值:";
            this.lblIsAscending.AutoSize = true;
            this.lblIsAscending.Location = new Point(16, 136);
            this.lblIsAscending.Name = "lblIsAscending";
            this.lblIsAscending.Size = new Size(35, 17);
            this.lblIsAscending.TabIndex = 4;
            this.lblIsAscending.Text = "升序:";
            this.listBoxField.ItemHeight = 12;
            this.listBoxField.Location = new Point(16, 184);
            this.listBoxField.Name = "listBoxField";
            this.listBoxField.Size = new Size(184, 64);
            this.listBoxField.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "字段:";
            this.lblGridSize3.AutoSize = true;
            this.lblGridSize3.Location = new Point(16, 72);
            this.lblGridSize3.Name = "lblGridSize3";
            this.lblGridSize3.Size = new Size(48, 17);
            this.lblGridSize3.TabIndex = 9;
            this.lblGridSize3.Text = "Grid 3:";
            this.lblGridSize2.AutoSize = true;
            this.lblGridSize2.Location = new Point(16, 48);
            this.lblGridSize2.Name = "lblGridSize2";
            this.lblGridSize2.Size = new Size(48, 17);
            this.lblGridSize2.TabIndex = 8;
            this.lblGridSize2.Text = "Grid 2:";
            this.lblGridSize1.AutoSize = true;
            this.lblGridSize1.Location = new Point(16, 24);
            this.lblGridSize1.Name = "lblGridSize1";
            this.lblGridSize1.Size = new Size(48, 17);
            this.lblGridSize1.TabIndex = 7;
            this.lblGridSize1.Text = "Grid 1:";
            this.btnDeleteGridIndex.Location = new Point(216, 48);
            this.btnDeleteGridIndex.Name = "btnDeleteGridIndex";
            this.btnDeleteGridIndex.Size = new Size(48, 24);
            this.btnDeleteGridIndex.TabIndex = 11;
            this.btnDeleteGridIndex.Text = "删除";
            this.btnDeleteGridIndex.Click += new EventHandler(this.btnDeleteGridIndex_Click);
            this.btnAddGridIndex.Location = new Point(216, 16);
            this.btnAddGridIndex.Name = "btnAddGridIndex";
            this.btnAddGridIndex.Size = new Size(48, 24);
            this.btnAddGridIndex.TabIndex = 10;
            this.btnAddGridIndex.Text = "添加...";
            this.btnAddGridIndex.Click += new EventHandler(this.btnAddGridIndex_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IndexSetCtrl";
            base.Size = new Size(328, 392);
            base.Load += new EventHandler(this.IndexSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAddGridIndex;
        private SimpleButton btnAddIndex;
        private SimpleButton btnDeleteGridIndex;
        private SimpleButton btnDeleteIndex;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label lblGridSize1;
        private Label lblGridSize2;
        private Label lblGridSize3;
        private Label lblIsAscending;
        private Label lblIsUnique;
        private ListBox listBoxField;
        private ListBox listBoxIndexName;
    }
}