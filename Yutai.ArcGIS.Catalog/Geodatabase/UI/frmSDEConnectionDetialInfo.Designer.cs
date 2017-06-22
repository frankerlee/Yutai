using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmSDEConnectionDetialInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSDEConnectionDetialInfo));
            this.rdoType = new RadioGroup();
            this.lvwVersions = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.cboHistoricalMarker = new ComboBoxEdit();
            this.dateTimePicker1 = new DateTimePicker();
            this.btnRefreshDatabaseTime = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.rdoConnectionHM = new RadioButton();
            this.rdoConnectionDate = new RadioButton();
            this.rdoType.Properties.BeginInit();
            this.cboHistoricalMarker.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoType.Location = new Point(12, -59);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用版本"), new RadioGroupItem(null, "使用历史标记") });
            this.rdoType.Size = new Size(218, 283);
            this.rdoType.TabIndex = 3;
            this.rdoType.SelectedIndexChanged += new EventHandler(this.rdoType_SelectedIndexChanged);
            this.lvwVersions.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.lvwVersions.HideSelection = false;
            this.lvwVersions.Location = new Point(11, 29);
            this.lvwVersions.Name = "lvwVersions";
            this.lvwVersions.Size = new Size(297, 111);
            this.lvwVersions.TabIndex = 4;
            this.lvwVersions.UseCompatibleStateImageBehavior = false;
            this.lvwVersions.View = View.Details;
            this.columnHeader_0.Text = "版本名称";
            this.columnHeader_0.Width = 121;
            this.columnHeader_1.Text = "版本描述";
            this.columnHeader_1.Width = 154;
            this.cboHistoricalMarker.Enabled = false;
            this.cboHistoricalMarker.Location = new Point(54, 184);
            this.cboHistoricalMarker.Name = "cboHistoricalMarker";
            this.cboHistoricalMarker.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHistoricalMarker.Size = new Size(201, 21);
            this.cboHistoricalMarker.TabIndex = 6;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(54, 233);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(193, 21);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new DateTime(2007, 1, 3, 0, 0, 0, 0);
            this.btnRefreshDatabaseTime.Enabled = false;
            this.btnRefreshDatabaseTime.Image = (Image) resources.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(253, 231);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(25, 23);
            this.btnRefreshDatabaseTime.TabIndex = 9;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.btnOK.Location = new Point(117, 266);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(209, 266);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.rdoConnectionHM.AutoSize = true;
            this.rdoConnectionHM.Checked = true;
            this.rdoConnectionHM.Enabled = false;
            this.rdoConnectionHM.Location = new Point(37, 162);
            this.rdoConnectionHM.Name = "rdoConnectionHM";
            this.rdoConnectionHM.Size = new Size(107, 16);
            this.rdoConnectionHM.TabIndex = 13;
            this.rdoConnectionHM.TabStop = true;
            this.rdoConnectionHM.Text = "连接到历史标记";
            this.rdoConnectionHM.UseVisualStyleBackColor = true;
            this.rdoConnectionHM.Click += new EventHandler(this.rdoConnectionHM_Click);
            this.rdoConnectionHM.CheckedChanged += new EventHandler(this.rdoConnectionHM_CheckedChanged);
            this.rdoConnectionDate.AutoSize = true;
            this.rdoConnectionDate.Enabled = false;
            this.rdoConnectionDate.Location = new Point(37, 211);
            this.rdoConnectionDate.Name = "rdoConnectionDate";
            this.rdoConnectionDate.Size = new Size(143, 16);
            this.rdoConnectionDate.TabIndex = 14;
            this.rdoConnectionDate.TabStop = true;
            this.rdoConnectionDate.Text = "连接到指定日期和时间";
            this.rdoConnectionDate.UseVisualStyleBackColor = true;
            this.rdoConnectionDate.Click += new EventHandler(this.rdoConnectionDate_Click);
            this.rdoConnectionDate.CheckedChanged += new EventHandler(this.rdoConnectionDate_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(313, 313);
            base.Controls.Add(this.rdoConnectionDate);
            base.Controls.Add(this.rdoConnectionHM);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.dateTimePicker1);
            base.Controls.Add(this.btnRefreshDatabaseTime);
            base.Controls.Add(this.cboHistoricalMarker);
            base.Controls.Add(this.lvwVersions);
            base.Controls.Add(this.rdoType);
            
            base.Name = "frmSDEConnectionDetialInfo";
            this.Text = "连接信息";
            base.Load += new EventHandler(this.frmSDEConnectionDetialInfo_Load);
            this.rdoType.Properties.EndInit();
            this.cboHistoricalMarker.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnRefreshDatabaseTime;
        private ComboBoxEdit cboHistoricalMarker;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private DateTimePicker dateTimePicker1;
        private ListView lvwVersions;
        private RadioButton rdoConnectionDate;
        private RadioButton rdoConnectionHM;
        private RadioGroup rdoType;
    }
}