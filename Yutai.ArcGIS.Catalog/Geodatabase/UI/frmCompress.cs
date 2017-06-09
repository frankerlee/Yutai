using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmCompress : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private Container container_0 = null;
        private IDatabaseCompact idatabaseCompact_0 = null;
        private int int_0 = 0;
        private IVersionedWorkspace iversionedWorkspace_0 = null;
        private Label label1;
        private TextEdit textEdit1;

        public frmCompress()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Exception exception;
            if (this.iversionedWorkspace_0 != null)
            {
                try
                {
                    this.iversionedWorkspace_0.Compress();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageBox.Show(exception.Message);
                }
            }
            else if (this.idatabaseCompact_0 != null)
            {
                try
                {
                    if (this.idatabaseCompact_0.CanCompact())
                    {
                        this.idatabaseCompact_0.Compact();
                    }
                    else
                    {
                        MessageBox.Show("无法整理" + this.textEdit1.Text);
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            if (this.int_0 == 0)
            {
                file.AddFilter(new MyGxFilterGeoDatabases(), true);
            }
            else
            {
                file.AddFilter(new MyGxFilterPersonalGeodatabases(), true);
            }
            if ((file.DoModalOpen() == DialogResult.OK) && (file.Items.Count > 0))
            {
                IGxObject obj2 = file.Items.get_Element(0) as IGxObject;
                if (obj2 is IGxDatabase)
                {
                    if (this.int_0 == 0)
                    {
                        this.iversionedWorkspace_0 = (obj2 as IGxDatabase).Workspace as IVersionedWorkspace;
                        if (this.iversionedWorkspace_0 == null)
                        {
                            MessageBox.Show("请选择企业数据库!");
                            this.textEdit1.Text = "";
                            this.btnOK.Enabled = false;
                        }
                        else
                        {
                            this.textEdit1.Text = obj2.FullName;
                            this.btnOK.Enabled = true;
                        }
                    }
                    else
                    {
                        this.textEdit1.Text = obj2.FullName;
                        this.idatabaseCompact_0 = (obj2 as IGxDatabase).Workspace as IDatabaseCompact;
                        this.btnOK.Enabled = this.idatabaseCompact_0 != null;
                    }
                }
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCompress_Load(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    this.Text = "压缩空间数据库";
                    this.label1.Text = "输入空间数据库";
                    break;

                case 1:
                    this.Text = "整理个人空间数据库";
                    this.label1.Text = "输入个人空间数据库";
                    break;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompress));
            this.textEdit1 = new TextEdit();
            this.label1 = new Label();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 0x20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.BackColor = SystemColors.Control;
            this.textEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0xe8, 0x15);
            this.textEdit1.TabIndex = 0;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(160, 0x10);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入空间数据库连接";
            this.btnOpen.Image = (Image) resources.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(0xfd, 0x20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x1c, 0x1c);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(0xb8, 0x58);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.OK;
            this.btnCancel.Location = new Point(240, 0x58);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 130);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textEdit1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCompress";
            this.Text = "压缩数据库";
            base.Load += new EventHandler(this.frmCompress_Load);
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
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
    }
}

