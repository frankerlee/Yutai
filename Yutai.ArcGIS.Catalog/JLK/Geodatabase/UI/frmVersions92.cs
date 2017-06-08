namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmVersions92 : Form
    {
        private bool bool_0 = false;
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
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private IWorkspace iworkspace_0 = null;
        private LabelControl labelControl1;
        private LabelControl lblDateTime;
        private LabelControl lblDateTimeValue;
        private LabelControl lblVersionName;
        private object object_0 = null;
        private Panel panelHistorical;
        private Panel panelTransaction;
        private RadioGroup rdoHistoryType;
        private ListView VersionInfolist;

        public frmVersions92()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboVersionType.SelectedIndex == 0)
            {
                this.int_0 = 0;
                if (this.VersionInfolist.SelectedItems.Count != -1)
                {
                    this.object_0 = this.VersionInfolist.SelectedItems[0].SubItems[1].Text + "." + this.VersionInfolist.SelectedItems[0].Text;
                }
            }
            else if (this.cboVersionType.SelectedIndex == 1)
            {
                if (this.rdoHistoryType.SelectedIndex == 0)
                {
                    this.int_0 = 1;
                    this.object_0 = this.cboHistoricalMarker.Text;
                }
                else
                {
                    this.object_0 = this.dateTimePicker1.Value;
                    this.int_0 = 2;
                }
            }
        }

        private void btnRefreshDatabaseTime_Click(object sender, EventArgs e)
        {
            if (this.iworkspace_0 is IDatabaseConnectionInfo2)
            {
                this.dateTimePicker1.Value = (DateTime) (this.iworkspace_0 as IDatabaseConnectionInfo2).ConnectionCurrentDateTime;
            }
            else
            {
                this.dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void cboHistoricalMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboHistoricalMarker.SelectedIndex != -1)
            {
                IHistoricalVersion version = (this.iworkspace_0 as IHistoricalWorkspace).FindHistoricalVersionByName(this.cboHistoricalMarker.Text);
                this.lblDateTimeValue.Text = version.TimeStamp.ToString();
            }
        }

        private void cboVersionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboVersionType.SelectedIndex == 0)
                {
                    this.int_0 = 0;
                }
                else if ((this.cboVersionType.SelectedIndex == 1) && (this.int_0 == 0))
                {
                    this.int_0 = 1;
                }
            }
            this.panelHistorical.Visible = this.cboVersionType.SelectedIndex == 1;
            this.panelTransaction.Visible = this.cboVersionType.SelectedIndex == 0;
            if (this.int_0 == 1)
            {
                this.rdoHistoryType.SelectedIndex = 0;
                this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
            }
            else if (this.int_0 == 2)
            {
                this.rdoHistoryType.SelectedIndex = 1;
                this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmVersions92_Load(object sender, EventArgs e)
        {
            this.method_2();
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmVersions92));
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
            this.labelControl1.Size = new Size(0x30, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "版本类型";
            this.cboVersionType.EditValue = "事务型";
            this.cboVersionType.Location = new Point(80, 3);
            this.cboVersionType.Name = "cboVersionType";
            this.cboVersionType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboVersionType.Properties.Items.AddRange(new object[] { "事务型", "历史型" });
            this.cboVersionType.Size = new Size(0xe4, 0x15);
            this.cboVersionType.TabIndex = 1;
            this.cboVersionType.SelectedIndexChanged += new EventHandler(this.cboVersionType_SelectedIndexChanged);
            this.panelTransaction.Controls.Add(this.VersionInfolist);
            this.panelTransaction.Location = new Point(13, 0x29);
            this.panelTransaction.Name = "panelTransaction";
            this.panelTransaction.Size = new Size(0x127, 0x95);
            this.panelTransaction.TabIndex = 2;
            this.VersionInfolist.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3, this.columnHeader_4 });
            this.VersionInfolist.Dock = DockStyle.Fill;
            this.VersionInfolist.FullRowSelect = true;
            this.VersionInfolist.LabelEdit = true;
            this.VersionInfolist.Location = new Point(0, 0);
            this.VersionInfolist.Name = "VersionInfolist";
            this.VersionInfolist.Size = new Size(0x127, 0x95);
            this.VersionInfolist.TabIndex = 1;
            this.VersionInfolist.UseCompatibleStateImageBehavior = false;
            this.VersionInfolist.View = View.Details;
            this.VersionInfolist.SelectedIndexChanged += new EventHandler(this.VersionInfolist_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_1.Text = "所有者";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "访问方式";
            this.columnHeader_2.Width = 0x4c;
            this.columnHeader_3.Text = "创建日期";
            this.columnHeader_3.Width = 0x55;
            this.columnHeader_4.Text = "最后修改日期";
            this.columnHeader_4.Width = 0x76;
            this.panelHistorical.Controls.Add(this.dateTimePicker1);
            this.panelHistorical.Controls.Add(this.btnRefreshDatabaseTime);
            this.panelHistorical.Controls.Add(this.lblDateTimeValue);
            this.panelHistorical.Controls.Add(this.lblDateTime);
            this.panelHistorical.Controls.Add(this.lblVersionName);
            this.panelHistorical.Controls.Add(this.cboHistoricalMarker);
            this.panelHistorical.Controls.Add(this.rdoHistoryType);
            this.panelHistorical.Location = new Point(13, 0x29);
            this.panelHistorical.Name = "panelHistorical";
            this.panelHistorical.Size = new Size(0x127, 0x95);
            this.panelHistorical.TabIndex = 3;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(0x2f, 0x6c);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0xc1, 0x15);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new DateTime(0x7d7, 1, 3, 0, 0, 0, 0);
            this.btnRefreshDatabaseTime.Image = (Image) manager.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(0xf6, 0x6a);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(0x19, 0x17);
            this.btnRefreshDatabaseTime.TabIndex = 9;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.lblDateTimeValue.Location = new Point(110, 0x3d);
            this.lblDateTimeValue.Name = "lblDateTimeValue";
            this.lblDateTimeValue.Size = new Size(0, 14);
            this.lblDateTimeValue.TabIndex = 8;
            this.lblDateTime.Location = new Point(0x25, 60);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new Size(0x40, 14);
            this.lblDateTime.TabIndex = 7;
            this.lblDateTime.Text = "日期和时间:";
            this.lblVersionName.Location = new Point(0x25, 30);
            this.lblVersionName.Name = "lblVersionName";
            this.lblVersionName.Size = new Size(0x1c, 14);
            this.lblVersionName.TabIndex = 6;
            this.lblVersionName.Text = "名称:";
            this.cboHistoricalMarker.EditValue = "";
            this.cboHistoricalMarker.Location = new Point(0x6a, 0x1c);
            this.cboHistoricalMarker.Name = "cboHistoricalMarker";
            this.cboHistoricalMarker.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHistoricalMarker.Size = new Size(0xb6, 0x15);
            this.cboHistoricalMarker.TabIndex = 5;
            this.cboHistoricalMarker.SelectedIndexChanged += new EventHandler(this.cboHistoricalMarker_SelectedIndexChanged);
            this.rdoHistoryType.Location = new Point(11, -31);
            this.rdoHistoryType.Name = "rdoHistoryType";
            this.rdoHistoryType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoHistoryType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoHistoryType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用历史标记"), new RadioGroupItem(null, "使用指定的日期和时间") });
            this.rdoHistoryType.Size = new Size(0x106, 0xaf);
            this.rdoHistoryType.TabIndex = 4;
            this.rdoHistoryType.SelectedIndexChanged += new EventHandler(this.rdoHistoryType_SelectedIndexChanged);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe9, 0xc4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x48, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xa1, 0xc4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x148, 230);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.panelHistorical);
            base.Controls.Add(this.panelTransaction);
            base.Controls.Add(this.cboVersionType);
            base.Controls.Add(this.labelControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
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

        private int method_0(IWorkspace iworkspace_1, out object object_1)
        {
            int num = -1;
            object_1 = "";
            IPropertySet connectionProperties = iworkspace_1.ConnectionProperties;
            bool flag = false;
            try
            {
                object_1 = connectionProperties.GetProperty("Version").ToString();
                flag = true;
                num = 0;
            }
            catch
            {
            }
            if (!flag)
            {
                try
                {
                    object_1 = connectionProperties.GetProperty("HISTORICAL_NAME").ToString();
                    flag = true;
                    num = 1;
                }
                catch
                {
                }
            }
            if (!flag)
            {
                try
                {
                    object_1 = connectionProperties.GetProperty("HISTORICAL_TIMESTAMP");
                    flag = true;
                    num = 2;
                }
                catch
                {
                }
            }
            return num;
        }

        private string method_1(esriVersionAccess esriVersionAccess_0)
        {
            switch (esriVersionAccess_0)
            {
                case esriVersionAccess.esriVersionAccessPrivate:
                    return "私有";

                case esriVersionAccess.esriVersionAccessProtected:
                    return "保护";
            }
            return "公共";
        }

        private void method_2()
        {
            if (this.iworkspace_0 != null)
            {
                this.VersionInfolist.Items.Clear();
                IEnumVersionInfo versions = (this.iworkspace_0 as IVersionedWorkspace).Versions;
                versions.Reset();
                IVersionInfo info2 = versions.Next();
                string[] items = new string[5];
                while (info2 != null)
                {
                    string versionName = info2.VersionName;
                    string[] strArray2 = versionName.Split(new char[] { '.' });
                    if (strArray2.Length > 1)
                    {
                        items[0] = strArray2[1];
                        items[1] = strArray2[0];
                    }
                    else
                    {
                        items[0] = strArray2[0];
                        items[1] = "";
                    }
                    items[2] = this.method_1(info2.Access);
                    items[3] = info2.Created.ToString();
                    items[4] = info2.Modified.ToString();
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info2
                    };
                    this.VersionInfolist.Items.Add(item);
                    if ((this.int_0 == 0) && (versionName == this.object_0.ToString()))
                    {
                        item.Selected = true;
                    }
                    info2 = versions.Next();
                }
                IHistoricalWorkspace workspace = this.iworkspace_0 as IHistoricalWorkspace;
                IEnumHistoricalMarker historicalMarkers = workspace.HistoricalMarkers;
                historicalMarkers.Reset();
                for (IHistoricalMarker marker2 = historicalMarkers.Next(); marker2 != null; marker2 = historicalMarkers.Next())
                {
                    this.cboHistoricalMarker.Properties.Items.Add(marker2.Name);
                    if ((this.int_0 == 1) && (marker2.Name == this.object_0.ToString()))
                    {
                        this.cboHistoricalMarker.SelectedIndex = this.cboHistoricalMarker.Properties.Items.Count - 1;
                    }
                }
                if ((this.cboHistoricalMarker.Properties.Items.Count > 0) && (this.cboHistoricalMarker.SelectedIndex == -1))
                {
                    this.cboHistoricalMarker.SelectedIndex = 0;
                }
                this.dateTimePicker1.Value = DateTime.Now;
                if (this.int_0 == 0)
                {
                    this.cboVersionType.SelectedIndex = 0;
                    this.panelHistorical.Visible = false;
                }
                else
                {
                    if (this.int_0 == 2)
                    {
                        this.dateTimePicker1.Value = (DateTime) this.object_0;
                    }
                    this.cboVersionType.SelectedIndex = 1;
                    this.panelTransaction.Visible = false;
                    this.cboVersionType_SelectedIndexChanged(this, new EventArgs());
                    this.rdoHistoryType_SelectedIndexChanged(this, new EventArgs());
                }
            }
        }

        private void rdoHistoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoHistoryType.SelectedIndex == 0)
            {
                this.cboHistoricalMarker.Enabled = true;
                this.lblDateTime.Enabled = true;
                this.lblDateTimeValue.Enabled = true;
                this.dateTimePicker1.Enabled = false;
                this.btnRefreshDatabaseTime.Enabled = false;
            }
            else if (this.rdoHistoryType.SelectedIndex == 1)
            {
                this.cboHistoricalMarker.Enabled = false;
                this.lblDateTime.Enabled = false;
                this.lblDateTimeValue.Enabled = false;
                this.dateTimePicker1.Enabled = true;
                this.btnRefreshDatabaseTime.Enabled = true;
            }
        }

        private void VersionInfolist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public int Type
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public object VersionName
        {
            get
            {
                return this.object_0;
            }
            set
            {
                this.object_0 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
                this.int_0 = this.method_0(this.iworkspace_0, out this.object_0);
            }
        }
    }
}

