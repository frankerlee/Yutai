using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class frmStyleConvert : Form
    {
        private Button btnConvert;
        private Button btnSaveTo;
        private Button btnSelect;
        private Button button2;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private bool m_IsStyleFile = false;
        private TextBox txtDest;
        private TextBox txtOrigin;

        public frmStyleConvert()
        {
            this.InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (this.txtOrigin.Text.Length == 0)
            {
                MessageBox.Show("请选择要转换的源符号库");
            }
            else if (this.txtDest.Text.Length == 0)
            {
                MessageBox.Show("请输入目的符号库");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    FileName = this.txtDest.Text
                };
                Stream stream = dialog.OpenFile();
                if (stream != null)
                {
                    Stream manifestResourceStream;
                    int num;
                    IStyleGallery gallery;
                    IStyleGallery gallery2;
                    int num3;
                    if (this.m_IsStyleFile)
                    {
                        manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.SymbolUI.template.style");
                    }
                    else
                    {
                        manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Controls.SymbolUI.template.ServerStyle");
                    }
                    byte[] buffer = new byte[0x1000];
                    while ((num = manifestResourceStream.Read(buffer, 0, 0x1000)) > 0)
                    {
                        stream.Write(buffer, 0, num);
                    }
                    stream.Close();
                    if (this.m_IsStyleFile)
                    {
                        gallery = new ServerStyleGalleryClass();
                        gallery2 = new MyStyleGallery();
                    }
                    else
                    {
                        gallery = new MyStyleGallery();
                        gallery2 = new ServerStyleGalleryClass();
                        for (num3 = (gallery2 as IStyleGalleryStorage).FileCount - 1; num3 >= 0; num3--)
                        {
                            string path = (gallery2 as IStyleGalleryStorage).get_File(num3);
                            (gallery2 as IStyleGalleryStorage).RemoveFile(path);
                        }
                    }
                    (gallery as IStyleGalleryStorage).AddFile(this.txtOrigin.Text);
                    (gallery2 as IStyleGalleryStorage).AddFile(this.txtDest.Text);
                    for (num3 = 0; num3 < gallery.ClassCount; num3++)
                    {
                        string name = gallery.get_Class(num3).Name;
                        IEnumStyleGalleryItem item = gallery.get_Items(name, this.txtOrigin.Text, "");
                        item.Reset();
                        for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                        {
                            gallery2.AddItem(item2);
                        }
                    }
                }
            }
        }

        private void btnSaveTo_Click(object sender, EventArgs e)
        {
            if (this.txtOrigin.Text.Length == 0)
            {
                MessageBox.Show("请选择要转换的源符号库");
            }
            else
            {
                string str = Path.GetExtension(this.txtOrigin.Text).ToLower();
                SaveFileDialog dialog = new SaveFileDialog();
                if (str == ".style")
                {
                    dialog.Filter = ".serverstyle|.serverstyle";
                }
                else
                {
                    dialog.Filter = ".style|.style";
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtDest.Text = dialog.FileName;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.style|.style|.serverstyle|.serverstyle"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtOrigin.Text = dialog.FileName;
                if (dialog.FilterIndex == 0)
                {
                    this.txtDest.Text = Path.GetFileNameWithoutExtension(dialog.FileName) + ".serverstyle";
                    this.m_IsStyleFile = false;
                }
                else
                {
                    this.txtDest.Text = Path.GetFileNameWithoutExtension(dialog.FileName) + ".style";
                    this.m_IsStyleFile = true;
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
            this.txtOrigin = new TextBox();
            this.txtDest = new TextBox();
            this.btnConvert = new Button();
            this.button2 = new Button();
            this.btnSelect = new Button();
            this.btnSaveTo = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源符号库";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的符号库";
            this.txtOrigin.Location = new Point(80, 14);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.ReadOnly = true;
            this.txtOrigin.Size = new Size(0x103, 0x15);
            this.txtOrigin.TabIndex = 2;
            this.txtDest.Location = new Point(80, 0x2e);
            this.txtDest.Name = "txtDest";
            this.txtDest.ReadOnly = true;
            this.txtDest.Size = new Size(0x103, 0x15);
            this.txtDest.TabIndex = 3;
            this.btnConvert.Location = new Point(0xf8, 0x49);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(0x36, 0x19);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x13d, 0x49);
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
            this.btnSaveTo.Location = new Point(0x159, 0x2e);
            this.btnSaveTo.Name = "btnSaveTo";
            this.btnSaveTo.Size = new Size(0x1f, 0x16);
            this.btnSaveTo.TabIndex = 7;
            this.btnSaveTo.Text = "....";
            this.btnSaveTo.UseVisualStyleBackColor = true;
            this.btnSaveTo.Click += new EventHandler(this.btnSaveTo_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x184, 0x74);
            base.Controls.Add(this.btnSaveTo);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtDest);
            base.Controls.Add(this.txtOrigin);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmStyleConvert";
            this.Text = "符号库互转工具";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

