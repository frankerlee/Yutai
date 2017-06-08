using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmAttachment : Form
    {
        private Button btnAdd;
        private Button btnAllSaveAs;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnOk;
        private Button btnOpen;
        private Button btnSaveAs;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private IContainer components = null;
        private ListView listView1;
        private List<IAttachment> m_DeleteAttachment = new List<IAttachment>();

        public frmAttachment()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "所有文件|*.*",
                Multiselect = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ITableAttachments attachments = (ITableAttachments) this.Object.Class;
                IAttachmentManager attachmentManager = attachments.AttachmentManager;
                ListViewItem item = null;
                string[] items = new string[2];
                foreach (string str in dialog.FileNames)
                {
                    IMemoryBlobStream stream = new MemoryBlobStreamClass();
                    stream.LoadFromFile(str);
                    string fileName = Path.GetFileName(str);
                    AttachmentClass class2 = new AttachmentClass {
                        ContentType = this.GetType(str),
                        Data = stream,
                        Name = fileName
                    };
                    IAttachment attachment = class2;
                    items[0] = attachment.Name;
                    items[1] = attachment.Size.ToString();
                    item = new ListViewItem(items) {
                        Tag = attachment
                    };
                    this.listView1.Items.Add(item);
                }
            }
        }

        private void btnAllSaveAs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    IAttachment tag = item.Tag as IAttachment;
                    string fileName = Path.Combine(selectedPath, tag.Name);
                    tag.Data.SaveToFile(fileName);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IAttachment tag = this.listView1.SelectedItems[0].Tag as IAttachment;
            if (tag.AttachmentID > 0)
            {
                this.m_DeleteAttachment.Add(tag);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.Attachments == null)
            {
                ITableAttachments attachments = (ITableAttachments) this.Object.Class;
                IAttachmentManager attachmentManager = attachments.AttachmentManager;
                foreach (IAttachment attachment in this.m_DeleteAttachment)
                {
                    attachmentManager.DeleteAttachment(attachment.AttachmentID);
                }
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    IAttachment tag = this.listView1.Items[i].Tag as IAttachment;
                    if (tag.AttachmentID == -1)
                    {
                        attachmentManager.AddAttachment(this.Object.OID, tag);
                    }
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IAttachment tag = item.Tag as IAttachment;
                string startupPath = Application.StartupPath;
                string str2 = Guid.NewGuid().ToString().Replace("-", "");
                string extension = Path.GetExtension(tag.Name);
                startupPath = Path.Combine(startupPath, "Temp");
                if (!Directory.Exists(startupPath))
                {
                    Directory.CreateDirectory(startupPath);
                }
                string fileName = Path.Combine(startupPath, str2 + extension);
                tag.Data.SaveToFile(fileName);
                Process.Start(fileName);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IAttachment tag = item.Tag as IAttachment;
                SaveFileDialog dialog = new SaveFileDialog {
                    FileName = tag.Name
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    tag.Data.SaveToFile(fileName);
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

        private void frmAttachment_Load(object sender, EventArgs e)
        {
            ListViewItem item;
            string[] strArray;
            if (this.Attachments != null)
            {
                this.btnAdd.Visible = false;
                this.btnDelete.Visible = false;
                item = null;
                strArray = new string[2];
                foreach (IAttachment attachment in this.Attachments)
                {
                    strArray[0] = attachment.Name;
                    strArray[1] = attachment.Size.ToString();
                    item = new ListViewItem(strArray) {
                        Tag = attachment
                    };
                    this.listView1.Items.Add(item);
                }
                this.btnAllSaveAs.Enabled = this.listView1.Items.Count > 0;
            }
            else
            {
                ITableAttachments attachments = (ITableAttachments) this.Object.Class;
                IAttachmentManager attachmentManager = attachments.AttachmentManager;
                ILongArray oids = new LongArrayClass();
                oids.Add(this.Object.OID);
                IEnumAttachment attachmentsByParentIDs = attachmentManager.GetAttachmentsByParentIDs(oids, true);
                attachmentsByParentIDs.Reset();
                IAttachment attachment = null;
                item = null;
                strArray = new string[2];
                while ((attachment = attachmentsByParentIDs.Next()) != null)
                {
                    strArray[0] = attachment.Name;
                    strArray[1] = attachment.Size.ToString();
                    item = new ListViewItem(strArray) {
                        Tag = attachment
                    };
                    this.listView1.Items.Add(item);
                }
                this.btnAllSaveAs.Enabled = this.listView1.Items.Count > 0;
            }
        }

        private string GetType(string fn)
        {
            switch (Path.GetExtension(fn).ToLower())
            {
                case ".doc":
                case ".docx":
                    return "application/msword";

                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";

                case ".png":
                    return "image/png";

                case ".jpg":
                    return "image/jpeg";
            }
            return "Unknown";
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.btnOpen = new Button();
            this.btnSaveAs = new Button();
            this.btnAllSaveAs = new Button();
            this.btnDelete = new Button();
            this.btnAdd = new Button();
            this.btnCancel = new Button();
            this.btnOk = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x146, 0xb5);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 0xcf;
            this.columnHeader2.Text = "大小";
            this.columnHeader2.Width = 0x54;
            this.btnOpen.Location = new Point(0x159, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x4b, 0x17);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Location = new Point(0x159, 0x2a);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new Size(0x4b, 0x17);
            this.btnSaveAs.TabIndex = 2;
            this.btnSaveAs.Text = "另存为";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new EventHandler(this.btnSaveAs_Click);
            this.btnAllSaveAs.Location = new Point(0x159, 0x47);
            this.btnAllSaveAs.Name = "btnAllSaveAs";
            this.btnAllSaveAs.Size = new Size(0x4b, 0x17);
            this.btnAllSaveAs.TabIndex = 3;
            this.btnAllSaveAs.Text = "全部保存";
            this.btnAllSaveAs.UseVisualStyleBackColor = true;
            this.btnAllSaveAs.Click += new EventHandler(this.btnAllSaveAs_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(0x159, 170);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x4b, 0x17);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "移除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAdd.Location = new Point(0x159, 0x8d);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x4b, 0x17);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x151, 0xd8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOk.Location = new Point(0xf4, 0xd8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x4b, 0x17);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a8, 0xf4);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.btnAllSaveAs);
            base.Controls.Add(this.btnSaveAs);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAttachment";
            this.Text = "附件";
            base.Load += new EventHandler(this.frmAttachment_Load);
            base.ResumeLayout(false);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IAttachment tag = item.Tag as IAttachment;
                string startupPath = Application.StartupPath;
                string str2 = Guid.NewGuid().ToString().Replace("-", "");
                string extension = Path.GetExtension(tag.Name);
                startupPath = Path.Combine(startupPath, "Temp");
                if (!Directory.Exists(startupPath))
                {
                    Directory.CreateDirectory(startupPath);
                }
                string fileName = Path.Combine(startupPath, str2 + extension);
                tag.Data.SaveToFile(fileName);
                Process.Start(fileName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
            this.btnSaveAs.Enabled = this.listView1.SelectedItems.Count > 0;
        }

        public List<IAttachment> Attachments { get; set; }

        public IObject Object { get; set; }
    }
}

