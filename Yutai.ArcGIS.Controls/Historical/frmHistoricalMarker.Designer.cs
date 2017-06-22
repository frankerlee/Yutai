using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    partial class frmHistoricalMarker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistoricalMarker));
            this.label1 = new Label();
            this.txtMarkerName = new TextEdit();
            this.label2 = new Label();
            this.btnRefreshDatabaseTime = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.dateTimePicker1 = new DateTimePicker();
            this.txtMarkerName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入历史标记名称";
            this.txtMarkerName.Location = new Point(14, 25);
            this.txtMarkerName.Name = "txtMarkerName";
            this.txtMarkerName.Size = new Size(224, 21);
            this.txtMarkerName.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new Size(137, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入历史标记的时间标记";
            this.btnRefreshDatabaseTime.Image = (Image) resources.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(213, 69);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(25, 23);
            this.btnRefreshDatabaseTime.TabIndex = 5;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.btnOK.Location = new Point(249, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(61, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(250, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(14, 71);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(193, 21);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.Value = new DateTime(2007, 1, 3, 0, 0, 0, 0);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(324, 106);
            base.Controls.Add(this.dateTimePicker1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnRefreshDatabaseTime);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtMarkerName);
            base.Controls.Add(this.label1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHistoricalMarker";
            this.Text = "frmHistoricalMarker";
            base.Load += new EventHandler(this.frmHistoricalMarker_Load);
            this.txtMarkerName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnRefreshDatabaseTime;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private Label label2;
        private object m_HistoricalMarkerTimeStamp;
        private TextEdit txtMarkerName;
    }
}