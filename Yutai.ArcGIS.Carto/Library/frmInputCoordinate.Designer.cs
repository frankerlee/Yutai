using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    partial class frmInputCoordinate
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
            this.label1 = new Label();
            this.btnAdd = new Button();
            this.btnModify = new Button();
            this.btnDelete = new Button();
            this.btnMoveUp = new Button();
            this.btnMoveDown = new Button();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label3 = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(14, 47);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(259, 172);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "横坐标";
            this.columnHeader_0.Width = 108;
            this.columnHeader_1.Text = "纵坐标";
            this.columnHeader_1.Width = 113;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "坐标列表";
            this.btnAdd.Location = new System.Drawing.Point(293, 47);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnModify.Location = new System.Drawing.Point(293, 77);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(75, 23);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnDelete.Location = new System.Drawing.Point(293, 106);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnMoveUp.Location = new System.Drawing.Point(293, 147);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(75, 23);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnMoveDown.Location = new System.Drawing.Point(293, 176);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(75, 23);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(274, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new System.Drawing.Point(179, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 24);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "公里";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "坐标单位:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(394, 273);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnMoveDown);
            base.Controls.Add(this.btnMoveUp);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmInputCoordinate";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "输入坐标";
            base.Load += new EventHandler(this.frmInputCoordinate_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnAdd;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnModify;
        private Button btnMoveDown;
        private Button btnMoveUp;
        private Button btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Label label1;
        private Label label3;
        private Label label4;
        private ListView listView1;
    }
}