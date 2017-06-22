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
    public partial class frmAttachment : Form
    {
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

