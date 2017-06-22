using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmGeographicTransformationConvert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeographicTransformationConvert));
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.label2 = new Label();
            this.cboTarget = new ComboBox();
            this.label3 = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.btnNew = new Button();
            this.txtConvertMethod = new TextBox();
            this.btnEdit = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源地理坐标";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(4, 22);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(229, 64);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 98);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目的地理坐标";
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Items.AddRange(new object[] { "北京54", "西安80", "WGS84" });
            this.cboTarget.Location = new System.Drawing.Point(4, 122);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new Size(217, 20);
            this.cboTarget.TabIndex = 3;
            this.cboTarget.SelectedIndexChanged += new EventHandler(this.cboTarget_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 156);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "转换方法";
            this.btnOK.FlatStyle = FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(239, 22);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(239, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnNew.FlatStyle = FlatStyle.Popup;
            this.btnNew.Location = new System.Drawing.Point(108, 198);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(56, 24);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.txtConvertMethod.Location = new System.Drawing.Point(4, 171);
            this.txtConvertMethod.Name = "txtConvertMethod";
            this.txtConvertMethod.ReadOnly = true;
            this.txtConvertMethod.Size = new Size(215, 21);
            this.txtConvertMethod.TabIndex = 9;
            this.btnEdit.FlatStyle = FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(170, 198);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(56, 24);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(307, 236);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.txtConvertMethod);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboTarget);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeographicTransformationConvert";
            this.Text = "地理坐标转换";
            base.Load += new EventHandler(this.frmGeographicTransformationConvert_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnCancel;
        private Button btnEdit;
        private Button btnNew;
        private Button btnOK;
        private ComboBox cboTarget;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private TextBox txtConvertMethod;
    }
}