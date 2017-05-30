using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Catalog.UI
{
    partial class AGSDirectoryPropertyPage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.Hostlist = new System.Windows.Forms.ListView();
            this.columnHeader_0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(288, 120);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 24);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "编辑...";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(288, 88);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 24);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(288, 56);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 24);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加...";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Hostlist
            // 
            this.Hostlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.Hostlist.Location = new System.Drawing.Point(24, 56);
            this.Hostlist.Name = "Hostlist";
            this.Hostlist.Size = new System.Drawing.Size(256, 112);
            this.Hostlist.TabIndex = 6;
            this.Hostlist.UseCompatibleStateImageBehavior = false;
            this.Hostlist.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_0
            // 
            this.columnHeader_0.Text = "主机";
            this.columnHeader_0.Width = 115;
            // 
            // columnHeader_1
            // 
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 128;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "server objects可用的服务器目录";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "主机";
            this.columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "描述";
            this.columnHeader2.Width = 128;
            // 
            // AGSDirectoryPropertyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.Hostlist);
            this.Controls.Add(this.label1);
            this.Name = "AGSDirectoryPropertyPage";
            this.Size = new System.Drawing.Size(361, 193);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnEdit;

        private Button btnDelete;

        private Button btnAdd;

        private ListView Hostlist;

        private ColumnHeader columnHeader_0;

        private ColumnHeader columnHeader_1;

        private Label label1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
    }
}
