using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmSDEConnectionDetialInfo : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnRefreshDatabaseTime;
        private ComboBoxEdit cboHistoricalMarker;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private DateTimePicker dateTimePicker1;
        private HISTORICALTYPE historicaltype_0 = HISTORICALTYPE.VERSION;
        private IContainer icontainer_0 = null;
        private IWorkspace iworkspace_0 = null;
        private ListView lvwVersions;
        private object object_0 = null;
        private RadioButton rdoConnectionDate;
        private RadioButton rdoConnectionHM;
        private RadioGroup rdoType;

        public frmSDEConnectionDetialInfo()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.rdoType.SelectedIndex == 0)
            {
                this.historicaltype_0 = HISTORICALTYPE.VERSION;
                if (this.lvwVersions.SelectedItems.Count == 0)
                {
                    this.object_0 = null;
                    return;
                }
                this.object_0 = this.lvwVersions.SelectedItems[0].Tag;
            }
            else if (this.rdoConnectionHM.Checked)
            {
                this.historicaltype_0 = HISTORICALTYPE.HISTORICALNAME;
                if (this.cboHistoricalMarker.SelectedIndex == -1)
                {
                    return;
                }
                this.object_0 = this.cboHistoricalMarker.Text;
            }
            else
            {
                this.historicaltype_0 = HISTORICALTYPE.HISTORICALTIMESTAMP;
                this.object_0 = this.dateTimePicker1.Value;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnRefreshDatabaseTime_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmSDEConnectionDetialInfo_Load(object sender, EventArgs e)
        {
            if (this.iworkspace_0 != null)
            {
                IEnumVersionInfo versions = (this.iworkspace_0 as IVersionedWorkspace2).Versions;
                versions.Reset();
                IVersionInfo info2 = versions.Next();
                string[] items = new string[2];
                while (info2 != null)
                {
                    items[0] = info2.VersionName;
                    items[1] = info2.Description;
                    ListViewItem item = new ListViewItem(items) {
                        Tag = info2
                    };
                    this.lvwVersions.Items.Add(item);
                    info2 = versions.Next();
                }
                IHistoricalWorkspace workspace = this.iworkspace_0 as IHistoricalWorkspace;
                IEnumHistoricalMarker historicalMarkers = workspace.HistoricalMarkers;
                historicalMarkers.Reset();
                for (IHistoricalMarker marker2 = historicalMarkers.Next(); marker2 != null; marker2 = historicalMarkers.Next())
                {
                    this.cboHistoricalMarker.Properties.Items.Add(marker2.Name);
                }
                if (this.cboHistoricalMarker.Properties.Items.Count > 0)
                {
                    this.cboHistoricalMarker.SelectedIndex = 0;
                }
            }
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
            this.rdoType.Size = new Size(0xda, 0x11b);
            this.rdoType.TabIndex = 3;
            this.rdoType.SelectedIndexChanged += new EventHandler(this.rdoType_SelectedIndexChanged);
            this.lvwVersions.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.lvwVersions.HideSelection = false;
            this.lvwVersions.Location = new Point(11, 0x1d);
            this.lvwVersions.Name = "lvwVersions";
            this.lvwVersions.Size = new Size(0x129, 0x6f);
            this.lvwVersions.TabIndex = 4;
            this.lvwVersions.UseCompatibleStateImageBehavior = false;
            this.lvwVersions.View = View.Details;
            this.columnHeader_0.Text = "版本名称";
            this.columnHeader_0.Width = 0x79;
            this.columnHeader_1.Text = "版本描述";
            this.columnHeader_1.Width = 0x9a;
            this.cboHistoricalMarker.Enabled = false;
            this.cboHistoricalMarker.Location = new Point(0x36, 0xb8);
            this.cboHistoricalMarker.Name = "cboHistoricalMarker";
            this.cboHistoricalMarker.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboHistoricalMarker.Size = new Size(0xc9, 0x15);
            this.cboHistoricalMarker.TabIndex = 6;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd H:mm:ss";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new Point(0x36, 0xe9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0xc1, 0x15);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.Value = new DateTime(0x7d7, 1, 3, 0, 0, 0, 0);
            this.btnRefreshDatabaseTime.Enabled = false;
            this.btnRefreshDatabaseTime.Image = (Image) resources.GetObject("btnRefreshDatabaseTime.Image");
            this.btnRefreshDatabaseTime.Location = new Point(0xfd, 0xe7);
            this.btnRefreshDatabaseTime.Name = "btnRefreshDatabaseTime";
            this.btnRefreshDatabaseTime.Size = new Size(0x19, 0x17);
            this.btnRefreshDatabaseTime.TabIndex = 9;
            this.btnRefreshDatabaseTime.Click += new EventHandler(this.btnRefreshDatabaseTime_Click);
            this.btnOK.Location = new Point(0x75, 0x10a);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xd1, 0x10a);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.rdoConnectionHM.AutoSize = true;
            this.rdoConnectionHM.Checked = true;
            this.rdoConnectionHM.Enabled = false;
            this.rdoConnectionHM.Location = new Point(0x25, 0xa2);
            this.rdoConnectionHM.Name = "rdoConnectionHM";
            this.rdoConnectionHM.Size = new Size(0x6b, 0x10);
            this.rdoConnectionHM.TabIndex = 13;
            this.rdoConnectionHM.TabStop = true;
            this.rdoConnectionHM.Text = "连接到历史标记";
            this.rdoConnectionHM.UseVisualStyleBackColor = true;
            this.rdoConnectionHM.Click += new EventHandler(this.rdoConnectionHM_Click);
            this.rdoConnectionHM.CheckedChanged += new EventHandler(this.rdoConnectionHM_CheckedChanged);
            this.rdoConnectionDate.AutoSize = true;
            this.rdoConnectionDate.Enabled = false;
            this.rdoConnectionDate.Location = new Point(0x25, 0xd3);
            this.rdoConnectionDate.Name = "rdoConnectionDate";
            this.rdoConnectionDate.Size = new Size(0x8f, 0x10);
            this.rdoConnectionDate.TabIndex = 14;
            this.rdoConnectionDate.TabStop = true;
            this.rdoConnectionDate.Text = "连接到指定日期和时间";
            this.rdoConnectionDate.UseVisualStyleBackColor = true;
            this.rdoConnectionDate.Click += new EventHandler(this.rdoConnectionDate_Click);
            this.rdoConnectionDate.CheckedChanged += new EventHandler(this.rdoConnectionDate_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x139, 0x139);
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

        private void rdoConnectionDate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoConnectionDate_Click(object sender, EventArgs e)
        {
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        private void rdoConnectionHM_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoConnectionHM_Click(object sender, EventArgs e)
        {
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        private void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.rdoType.SelectedIndex == 1;
            this.lvwVersions.Enabled = !flag;
            this.rdoConnectionDate.Enabled = flag;
            this.rdoConnectionHM.Enabled = flag;
            this.cboHistoricalMarker.Enabled = this.rdoConnectionHM.Checked;
            this.dateTimePicker1.Enabled = !this.rdoConnectionHM.Checked;
            this.btnRefreshDatabaseTime.Enabled = !this.rdoConnectionHM.Checked;
        }

        internal HISTORICALTYPE HISTORICAL
        {
            get
            {
                return this.historicaltype_0;
            }
        }

        public object HistoricalInfo
        {
            get
            {
                return this.object_0;
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return this.iworkspace_0;
            }
            set
            {
                this.iworkspace_0 = value;
            }
        }

        internal enum HISTORICALTYPE
        {
            VERSION,
            HISTORICALTIMESTAMP,
            HISTORICALNAME
        }
    }
}

