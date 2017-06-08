using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Historical
{
    public class frmHistorialMarkerManager : Form
    {
        private SimpleButton btnColse;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private SimpleButton btnNew;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private IContainer components = null;
        private Label label1;
        private ListView listView1;
        private IHistoricalWorkspace m_pHistoricalWorkspace = null;

        public frmHistorialMarkerManager()
        {
            this.InitializeComponent();
        }

        private void AddToList(IHistoricalMarker pHistoricalMarker)
        {
            ListViewItem item = new ListViewItem(new string[] { pHistoricalMarker.Name, pHistoricalMarker.TimeStamp.ToString() }) {
                Tag = pHistoricalMarker
            };
            this.listView1.Items.Add(item);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)
            {
                IHistoricalMarker tag = this.listView1.SelectedItems[i].Tag as IHistoricalMarker;
                try
                {
                    this.m_pHistoricalWorkspace.RemoveHistoricalMarker(tag.Name);
                    this.listView1.Items.Remove(this.listView1.SelectedItems[i]);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            IHistoricalMarker tag = item.Tag as IHistoricalMarker;
            frmHistoricalMarker marker2 = new frmHistoricalMarker {
                HistoricalMarker = tag
            };
            if (marker2.ShowDialog() == DialogResult.OK)
            {
                this.m_pHistoricalWorkspace.RemoveHistoricalMarker(tag.Name);
                tag = this.m_pHistoricalWorkspace.AddHistoricalMarker(marker2.HistoricalMarkerName, marker2.HistoricalMarkerTimeStamp);
                item.Text = tag.Name;
                item.SubItems[1].Text = tag.TimeStamp.ToString();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmHistoricalMarker marker = new frmHistoricalMarker {
                DatabaseConnectionInfo = this.m_pHistoricalWorkspace as IDatabaseConnectionInfo2
            };
            if (marker.ShowDialog() == DialogResult.OK)
            {
                IHistoricalMarker marker2 = null;
                if (marker2 != null)
                {
                    MessageBox.Show("名称的时间标记以存在！");
                }
                else
                {
                    this.AddToList(this.m_pHistoricalWorkspace.AddHistoricalMarker(marker.HistoricalMarkerName, marker.HistoricalMarkerTimeStamp));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmHistorialMarkerManager_Load(object sender, EventArgs e)
        {
            IEnumHistoricalMarker historicalMarkers = this.m_pHistoricalWorkspace.HistoricalMarkers;
            historicalMarkers.Reset();
            IHistoricalMarker marker2 = historicalMarkers.Next();
            string[] items = new string[2];
            IVersionedWorkspace pHistoricalWorkspace = this.m_pHistoricalWorkspace as IVersionedWorkspace;
            while (marker2 != null)
            {
                if (marker2.Name != "DEFAULT")
                {
                    items[0] = marker2.Name;
                    items[1] = marker2.TimeStamp.ToString();
                    ListViewItem item = new ListViewItem(items) {
                        Tag = marker2
                    };
                    this.listView1.Items.Add(item);
                }
                marker2 = historicalMarkers.Next();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistorialMarkerManager));
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.btnNew = new SimpleButton();
            this.btnEdit = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnColse = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "历史标记";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(15, 0x1a);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x157, 0xc0);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 0xa2;
            this.columnHeader2.Text = "时间标记";
            this.columnHeader2.Width = 0x9b;
            this.btnNew.Location = new Point(7, 0xee);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x4b, 0x17);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new Point(0x58, 0xee);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x4b, 0x17);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "编辑...";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(170, 0xee);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnColse.DialogResult = DialogResult.Cancel;
            this.btnColse.Location = new Point(0x125, 0xee);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new Size(0x4b, 0x17);
            this.btnColse.TabIndex = 5;
            this.btnColse.Text = "关闭";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(380, 0x111);
            base.Controls.Add(this.btnColse);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmHistorialMarkerManager";
            this.Text = "历史标记管理";
            base.Load += new EventHandler(this.frmHistorialMarkerManager_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedIndices.Count > 0;
            this.btnEdit.Enabled = this.listView1.SelectedIndices.Count == 1;
        }

        public IHistoricalWorkspace HistoricalWorkspace
        {
            set
            {
                this.m_pHistoricalWorkspace = value;
            }
        }
    }
}

