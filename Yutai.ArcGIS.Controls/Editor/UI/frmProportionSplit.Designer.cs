using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmProportionSplit
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
            ListViewItem item = new ListViewItem("<点击输入长度>");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProportionSplit));
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtLength = new TextEdit();
            this.txtInputLength = new TextEdit();
            this.txtSurplus = new TextEdit();
            this.txtError = new TextEdit();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.rdoStartType = new RadioGroup();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtLength.Properties.BeginInit();
            this.txtInputLength.Properties.BeginInit();
            this.txtSurplus.Properties.BeginInit();
            this.txtError.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.rdoStartType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对象长度:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "输入长度:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "相对误差:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "剩余:";
            this.txtLength.EditValue = "";
            this.txtLength.Location = new System.Drawing.Point(72, 8);
            this.txtLength.Name = "txtLength";
            this.txtLength.Properties.ReadOnly = true;
            this.txtLength.Size = new Size(80, 21);
            this.txtLength.TabIndex = 4;
            this.txtInputLength.EditValue = "";
            this.txtInputLength.Location = new System.Drawing.Point(72, 40);
            this.txtInputLength.Name = "txtInputLength";
            this.txtInputLength.Properties.ReadOnly = true;
            this.txtInputLength.Size = new Size(80, 21);
            this.txtInputLength.TabIndex = 5;
            this.txtSurplus.EditValue = "";
            this.txtSurplus.Location = new System.Drawing.Point(232, 8);
            this.txtSurplus.Name = "txtSurplus";
            this.txtSurplus.Properties.ReadOnly = true;
            this.txtSurplus.Size = new Size(80, 21);
            this.txtSurplus.TabIndex = 6;
            this.txtError.EditValue = "";
            this.txtError.Location = new System.Drawing.Point(232, 40);
            this.txtError.Name = "txtError";
            this.txtError.Properties.ReadOnly = true;
            this.txtError.Size = new Size(80, 21);
            this.txtError.TabIndex = 7;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            item.Tag = "0";
            this.listView1.Items.AddRange(new ListViewItem[] { item });
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(16, 72);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(336, 120);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.AfterLabelEdit += new LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.BeforeLabelEdit += new LabelEditEventHandler(this.listView1_BeforeLabelEdit);
            this.columnHeader1.Text = "长度";
            this.columnHeader1.Width = 130;
            this.columnHeader2.Text = "比例细分";
            this.columnHeader2.Width = 169;
            this.groupBox1.Controls.Add(this.rdoStartType);
            this.groupBox1.Location = new System.Drawing.Point(24, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(312, 72);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            this.rdoStartType.Location = new System.Drawing.Point(16, 17);
            this.rdoStartType.Name = "rdoStartType";
            this.rdoStartType.Properties.Appearance.BackColor = SystemColors.ControlLight;
            this.rdoStartType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoStartType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoStartType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "从线起点开始"), new RadioGroupItem(null, "从线终点开始") });
            this.rdoStartType.Size = new Size(112, 47);
            this.rdoStartType.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(192, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(272, 280);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(64, 24);
            this.simpleButton2.TabIndex = 11;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(360, 317);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.txtError);
            base.Controls.Add(this.txtSurplus);
            base.Controls.Add(this.txtInputLength);
            base.Controls.Add(this.txtLength);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProportionSplit";
            this.Text = "分割";
            base.Load += new EventHandler(this.frmPropertionSplit_Load);
            this.txtLength.Properties.EndInit();
            this.txtInputLength.Properties.EndInit();
            this.txtSurplus.Properties.EndInit();
            this.txtError.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.rdoStartType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private SimpleButton btnOK;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private RadioGroup rdoStartType;
        private SimpleButton simpleButton2;
        private TextEdit txtError;
        private TextEdit txtInputLength;
        private TextEdit txtLength;
        private TextEdit txtSurplus;
    }
}