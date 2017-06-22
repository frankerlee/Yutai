using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class VCSCoordinateInfoPage
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
            this.label8 = new Label();
            this.btnImport = new SimpleButton();
            this.label7 = new Label();
            this.btnSaveAs = new SimpleButton();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.btnModify = new SimpleButton();
            this.btnClear = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.textBoxName = new TextEdit();
            this.textBoxDetail = new MemoEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textBoxName.Properties.BeginInit();
            this.textBoxDetail.Properties.BeginInit();
            base.SuspendLayout();
            this.label8.Location = new System.Drawing.Point(77, 284);
            this.label8.Name = "label8";
            this.label8.Size = new Size(208, 32);
            this.label8.TabIndex = 39;
            this.label8.Text = "从已存在的数据中导入坐标系统和Z域值";
            this.label8.Visible = false;
            this.btnImport.Location = new System.Drawing.Point(13, 284);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(56, 24);
            this.btnImport.TabIndex = 38;
            this.btnImport.Text = "导入";
            this.btnImport.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 418);
            this.label7.Name = "label7";
            this.label7.Size = new Size(113, 12);
            this.label7.TabIndex = 37;
            this.label7.Text = "保存坐标系统到文件";
            this.label7.Visible = false;
            this.btnSaveAs.Location = new System.Drawing.Point(13, 412);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(56, 24);
            this.btnSaveAs.TabIndex = 36;
            this.btnSaveAs.Text = "另存为...";
            this.btnSaveAs.Visible = false;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(77, 386);
            this.label6.Name = "label6";
            this.label6.Size = new Size(137, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "设置垂直坐标系统到未知";
            this.label6.Visible = false;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 353);
            this.label5.Name = "label5";
            this.label5.Size = new Size(161, 12);
            this.label5.TabIndex = 34;
            this.label5.Text = "编辑当前选择的坐标系统属性";
            this.label5.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 321);
            this.label4.Name = "label4";
            this.label4.Size = new Size(101, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "新建一个坐标系统";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 257);
            this.label3.Name = "label3";
            this.label3.Size = new Size(149, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "选择一个预定义的坐标系统";
            this.label3.Visible = false;
            this.btnModify.Location = new System.Drawing.Point(13, 348);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(56, 24);
            this.btnModify.TabIndex = 31;
            this.btnModify.Text = "修改...";
            this.btnModify.Visible = false;
            this.btnClear.Location = new System.Drawing.Point(13, 380);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "清空";
            this.btnClear.Visible = false;
            this.btnNew.Location = new System.Drawing.Point(13, 316);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(56, 24);
            this.btnNew.TabIndex = 29;
            this.btnNew.Text = "新建...";
            this.btnNew.Visible = false;
            this.btnSelect.Location = new System.Drawing.Point(13, 252);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(56, 24);
            this.btnSelect.TabIndex = 28;
            this.btnSelect.Text = "选择";
            this.btnSelect.Visible = false;
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(53, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(216, 21);
            this.textBoxName.TabIndex = 27;
            this.textBoxDetail.EditValue = "";
            this.textBoxDetail.Location = new System.Drawing.Point(13, 60);
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.textBoxDetail.Properties.Appearance.Options.UseBackColor = true;
            this.textBoxDetail.Properties.ReadOnly = true;
            this.textBoxDetail.Size = new Size(264, 184);
            this.textBoxDetail.TabIndex = 26;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "详细信息";
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "名字:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.label8);
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.btnSaveAs);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.textBoxDetail);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "VCSCoordinateInfoPage";
            base.Size = new Size(343, 444);
            base.Load += new EventHandler(this.VCSCoordinateInfoPage_Load);
            this.textBoxName.Properties.EndInit();
            this.textBoxDetail.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnClear;
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private SimpleButton btnSaveAs;
        private SimpleButton btnSelect;
        private IVerticalCoordinateSystem iverticalCoordinateSystem_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private MemoEdit textBoxDetail;
        private TextEdit textBoxName;
    }
}