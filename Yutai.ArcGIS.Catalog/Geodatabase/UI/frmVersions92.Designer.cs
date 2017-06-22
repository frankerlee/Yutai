using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmVersions92
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersions92));
            this.labelControl1 = new LabelControl();
            this.cboVersionType = new ComboBoxEdit();
            this.panelTransaction = new Panel();
            this.VersionInfolist = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.panelHistorical = new Panel();
            this.dateTimePicker1 = new DateTimePicker();
            this.btnRefreshDatabaseTime = new SimpleButton();
            this.lblDateTimeValue = new LabelControl();
            this.lblDateTime = new LabelControl();
            this.lblVersionName = new LabelControl();
            this.cboHistoricalMarker = new ComboBoxEdit();
            this.rdoHistoryType = new RadioGroup();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.cboVersionType.Properties.BeginInit();
            this.panelTransaction.SuspendLayout();
            this.panelHistorical.SuspendLayout();
            this.cboHistoricalMarker.Properties.BeginInit();
            this.rdoHistoryType.Properties.BeginInit();
            base.SuspendLayout();
            this.labelControl1.Location = new Point(11, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "版本类型";
            this.cboVersionType.EditValue = "事务型";
            this.cboVersionType.Location = new Point(80, 3);
            this.cboVersionType.Name = "cboVersionType";
            this.cboVersionType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboVersionType.Properties.Items.AddRange(new object[] { "事务型", "历史型" });
            this.cboVersionType.Size = new Size(228, 21);
            this.cboVersionType.TabIndex = 1;
            this.cboVersionType.SelectedIndexChanged += new EventHandler(this.cboVersionType_SelectedIndexChanged);
            this.panelTransaction.Controls.Add(this.VersionInfolist);
            this.panelTransaction.Location = new Point(13, 41);
            this.panelTransaction.Name = "panelTransaction";
            this.panelTransaction.Size = new Size(295, 149);
            this.panelTransaction.TabIndex = 2;
            this.VersionInfolist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3, this.columnHeader_4 });
            this.VersionInfolist.Dock = DockStyle.Fill;
            this.VersionInfolist.FullRowSelect = true;
            this.VersionInfolist.LabelEdit = true;
            this.VersionInfolist.Location = new Point(0, 0);
            this.VersionInfolist.Name = "VersionInfolist";
            this.VersionInfolist.Size = new Size(295, 149);
            this.VersionInfolist.TabIndex = 1;
            this.VersionInfolist.UseCompatibleStateImageBehavior = false;
            this.VersionInfolist.View = View.Details;
            this.VersionInfolist.SelectedIndexChanged += new EventHandler(this.VersionInfolist_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_1.Text = "所有者";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "访问方式";
            this.columnHeader_2.Width = 76;
            this.columnHeader_3.Text = "创建日期";
            this.columnHeader_3.Width = 85;
            this.columnHeader_4.Text = "最后修改日期";
            this.columnHeader_4.Width = 118;
            this.panelHistorical.Controls.Add(this.dateTimePicker1);
            this.panelHistorical.Controls.Add(this.btnRefreshDatabaseTime);
            this.panelHistorical.Controls.Add(this.lblDateTimeValue);
            this.panelHistorical.Controls.Add(this.lblDateTime);
            this.panelHistorical.Controls.Add(this.lblVersionName);
            this.panelHistorical.Controls.Add(this.cboHistoricalMarker);
            this.panelHistorical.Controls.Add(this.rdoHistoryType);
            this.panelHistorical.Location = new Point(13, 41);
            this.panelHistorical.Name = "panelHistorical";
            this.panelHistorical.Size = new Size(295, 149);
            this.panelHistorical.TabIndex = 3;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(47, 108);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(193, 21);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new DateTime(2007, 1, 3, 0, 0, 0, 0);
            this.btnRefreshDatabaseTime.Image = (Image) resources.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(246, 106);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(25, 23);
            this.btnRefreshDatabaseTime.TabIndex = 9;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.lblDateTimeValue.Location = new Point(110, 61);
            this.lblDateTimeValue.Name = "lblDateTimeValue";
            this.lblDateTimeValue.Size = new Size(0, 14);
            this.lblDateTimeValue.TabIndex = 8;
            this.lblDateTime.Location = new Point(37, 60);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new Size(64, 14);
            this.lblDateTime.TabIndex = 7;
            this.lblDateTime.Text = "日期和时间:";
            this.lblVersionName.Location = new Point(37, 30);
            this.lblVersionName.Name = "lblVersionName";
            this.lblVersionName.Size = new Size(28, 14);
            this.lblVersionName.TabIndex = 6;
            this.lblVersionName.Text = "名称:";
            this.cboHistoricalMarker.EditValue = "";
            this.cboHistoricalMarker.Location = new Point(106, 28);
            this.cboHistoricalMarker.Name = "cboHistoricalMarker";
            this.cboHistoricalMarker.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHistoricalMarker.Size = new Size(182, 21);
            this.cboHistoricalMarker.TabIndex = 5;
            this.cboHistoricalMarker.SelectedIndexChanged += new EventHandler(this.cboHistoricalMarker_SelectedIndexChanged);
            this.rdoHistoryType.Location = new Point(11, -31);
            this.rdoHistoryType.Name = "rdoHistoryType";
            this.rdoHistoryType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoHistoryType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoHistoryType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用历史标记"), new RadioGroupItem(null, "使用指定的日期和时间") });
            this.rdoHistoryType.Size = new Size(262, 175);
            this.rdoHistoryType.TabIndex = 4;
            this.rdoHistoryType.SelectedIndexChanged += new EventHandler(this.rdoHistoryType_SelectedIndexChanged);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(233, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(72, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(161, 196);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(328, 230);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.panelHistorical);
            base.Controls.Add(this.panelTransaction);
            base.Controls.Add(this.cboVersionType);
            base.Controls.Add(this.labelControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmVersions92";
            this.Text = "改变版本";
            base.Load += new EventHandler(this.frmVersions92_Load);
            this.cboVersionType.Properties.EndInit();
            this.panelTransaction.ResumeLayout(false);
            this.panelHistorical.ResumeLayout(false);
            this.panelHistorical.PerformLayout();
            this.cboHistoricalMarker.Properties.EndInit();
            this.rdoHistoryType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnRefreshDatabaseTime;
        private ComboBoxEdit cboHistoricalMarker;
        private ComboBoxEdit cboVersionType;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private DateTimePicker dateTimePicker1;
        private LabelControl labelControl1;
        private LabelControl lblDateTime;
        private LabelControl lblDateTimeValue;
        private LabelControl lblVersionName;
        private Panel panelHistorical;
        private Panel panelTransaction;
        private RadioGroup rdoHistoryType;
        private ListView VersionInfolist;
    }
}