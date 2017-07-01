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
    public partial class frmStyleConvertEx : Form
    {
        private bool m_IsStyleFile = false;

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
                                manifestResourceStream =
                                    base.GetType()
                                        .Assembly.GetManifestResourceStream(
                                            "Yutai.ArcGIS.Controls.Controls.SymbolUI.template.style");
                            }
                            else
                            {
                                manifestResourceStream =
                                    base.GetType()
                                        .Assembly.GetManifestResourceStream(
                                            "Yutai.ArcGIS.Controls.Controls.SymbolUI.template.ServerStyle");
                            }
                            byte[] buffer = new byte[4096];
                            while ((num = manifestResourceStream.Read(buffer, 0, 4096)) > 0)
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
    }
}