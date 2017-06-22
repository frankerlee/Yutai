using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class frmStyleConvert : Form
    {
        private bool m_IsStyleFile = false;

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
                    byte[] buffer = new byte[4096];
                    while ((num = manifestResourceStream.Read(buffer, 0, 4096)) > 0)
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


    }
}