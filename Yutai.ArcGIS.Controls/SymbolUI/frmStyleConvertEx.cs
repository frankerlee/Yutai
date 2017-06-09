using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class frmStyleConvertEx : Form
    {
        private Button btnConvert;
        private Button btnSaveTo;
        private Button btnSelect;
        private Button button2;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private bool m_IsStyleFile = false;
        private TextBox txtDest;

        public frmStyleConvertEx()
        {
            this.InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选择要转换的源符号库");
            }
            else if (this.txtDest.Text.Length == 0)
            {
                MessageBox.Show("请输入转出目录");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (object obj2 in this.listBox1.Items)
                {
                    try
                    {
                        string path = obj2.ToString();
                        SaveFileDialog dialog = new SaveFileDialog();
                        string str2 = ".style";
                        if (this.m_IsStyleFile)
                        {
                            str2 = ".ServerStyle";
                        }
                        string str3 = Path.Combine(this.txtDest.Text, Path.GetFileNameWithoutExtension(path) + str2);
                        if (File.Exists(str3))
                        {
                            File.Delete(str3);
                        }
                        dialog.FileName = str3;
                        Stream stream = dialog.OpenFile();
                        if (stream != null)
                        {
                            Stream manifestResourceStream;
                            int num;
                            IStyleGallery gallery;
                            IStyleGallery gallery2;
                            int num3;
                            string str4;
                            if (!this.m_IsStyleFile)
                            {
                                manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.SymbolUI.template.style");
                            }
                            else
                            {
                                manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.Controls.SymbolUI.template.ServerStyle");
                            }
                            byte[] buffer = new byte[0x1000];
                            while ((num = manifestResourceStream.Read(buffer, 0, 0x1000)) > 0)
                            {
                                stream.Write(buffer, 0, num);
                            }
                            stream.Close();
                            if (this.m_IsStyleFile)
                            {
                                gallery = new MyStyleGallery();
                                gallery2 = new ServerStyleGalleryClass();
                                num3 = (gallery2 as IStyleGalleryStorage).FileCount - 1;
                                while (num3 >= 0)
                                {
                                    str4 = (gallery2 as IStyleGalleryStorage).get_File(num3);
                                    (gallery2 as IStyleGalleryStorage).RemoveFile(str4);
                                    num3--;
                                }
                            }
                            else
                            {
                                gallery = new ServerStyleGalleryClass();
                                gallery2 = new MyStyleGallery();
                                num3 = (gallery as IStyleGalleryStorage).FileCount - 1;
                                while (num3 >= 0)
                                {
                                    str4 = (gallery as IStyleGalleryStorage).get_File(num3);
                                    (gallery as IStyleGalleryStorage).RemoveFile(str4);
                                    num3--;
                                }
                            }
                            gallery.Clear();
                            gallery2.Clear();
                            (gallery as IStyleGalleryStorage).AddFile(path);
                            (gallery2 as IStyleGalleryStorage).TargetFile = str3;
                            for (num3 = 0; num3 < gallery.ClassCount; num3++)
                            {
                                string name = gallery.get_Class(num3).Name;
                                IEnumBSTR mbstr = gallery.get_Categories(name);
                                mbstr.Reset();
                                for (string str6 = mbstr.Next(); str6 != null; str6 = mbstr.Next())
                                {
                                    IEnumStyleGalleryItem item = gallery.get_Items(name, path, str6);
                                    item.Reset();
                                    IStyleGalleryItem item2 = null;
                                    try
                                    {
                                        item2 = item.Next();
                                    }
                                    catch
                                    {
                                    }
                                    while (item2 != null)
                                    {
                                        if (this.m_IsStyleFile)
                                        {
                                            IStyleGalleryItem item3 = new ServerStyleGalleryItemClass();
                                            object obj3 = item2.Item;
                                            item3.Item = obj3;
                                            string str7 = item2.Name;
                                            item3.Name = str7;
                                            string category = item2.Category;
                                            item3.Category = category;
                                            gallery2.AddItem(item3);
                                        }
                                        else
                                        {
                                            gallery2.AddItem(item2);
                                        }
                                        try
                                        {
                                            item2 = item.Next();
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            (gallery as IStyleGalleryStorage).RemoveFile(path);
                            (gallery2 as IStyleGalleryStorage).RemoveFile(str3);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                this.Cursor = Cursors.Default;
                base.Close();
            }
        }

        private void btnSaveTo_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选择要合并的符号库");
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtDest.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (this.listBox1.Items.Count == 0)
            {
                dialog.Filter = "*.style|*.style|*.serverstyle|*.serverstyle";
            }
            else if (this.m_IsStyleFile)
            {
                dialog.Filter = "*.style|*.style";
            }
            else
            {
                dialog.Filter = "*.serverstyle|*.serverstyle";
            }
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (this.listBox1.Items.Count == 0)
                {
                    if (dialog.FilterIndex == 2)
                    {
                        this.m_IsStyleFile = false;
                    }
                    else
                    {
                        this.m_IsStyleFile = true;
                    }
                }
                foreach (string str in dialog.FileNames)
                {
                    this.listBox1.Items.Add(str);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtDest = new TextBox();
            this.btnConvert = new Button();
            this.button2 = new Button();
            this.btnSelect = new Button();
            this.btnSaveTo = new Button();
            this.listBox1 = new ListBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源符号库";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0xb3);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的符号库";
            this.txtDest.Location = new Point(80, 0xae);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new Size(0x103, 0x15);
            this.txtDest.TabIndex = 3;
            this.btnConvert.Location = new Point(0xf8, 0xc9);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(0x36, 0x19);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x13d, 0xc9);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x36, 0x19);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnSelect.Location = new Point(0x159, 14);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x1f, 0x15);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "....";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.btnSaveTo.Location = new Point(0x159, 0xae);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new Size(0x1f, 0x16);
            this.btnSaveTo.TabIndex = 7;
            this.btnSaveTo.Text = "....";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new EventHandler(this.btnSaveTo_Click);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(80, 0x11);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x103, 0x94);
            this.listBox1.TabIndex = 8;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18f, 0xed);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.btnSaveTo);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtDest);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmStyleConvertEx";
            this.Text = "符号库互转工具";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

