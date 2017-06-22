using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class SelectTemplateCtrl
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.components = new Container();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.imageList1 = new ImageList(this.components);
            this.label1 = new Label();
            this.label2 = new Label();
            this.btnClearAll = new Button();
            this.btnSelectAll = new Button();
            base.SuspendLayout();
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.Location = new Point(17, 39);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(292, 160);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.columnHeader1.Text = "模板名称";
            this.columnHeader1.Width = 117;
            this.columnHeader2.Text = "类别";
            this.columnHeader2.Width = 148;
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new Size(16, 16);
            this.imageList1.TransparentColor = Color.Transparent;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "图层：";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(62, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "        ";
            this.btnClearAll.Location = new Point(315, 68);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(75, 23);
            this.btnClearAll.TabIndex = 4;
            this.btnClearAll.Text = "全部清除";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Location = new Point(315, 39);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.Name = "SelectTemplateCtrl";
            base.Size = new Size(398, 238);
            base.Load += new EventHandler(this.SelectTemplateCtrl_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private Button btnClearAll;
        private Button btnSelectAll;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private ListView listView1;
    }
}