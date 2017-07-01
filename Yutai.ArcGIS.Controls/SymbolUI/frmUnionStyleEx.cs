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
    public partial class frmUnionStyleEx : Form
    {
        private bool m_IsStyleFile = false;

        public frmUnionStyleEx()
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
                IStyleGallery gallery;
                int num3;
                string str;
                SaveFileDialog dialog = new SaveFileDialog
                {
                    FileName = this.txtDest.Text
                };
                Stream stream = dialog.OpenFile();
                if (stream != null)
                {
                    Stream manifestResourceStream;
                    int num;
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
                }
                if (this.m_IsStyleFile)
                {
                    gallery = new ServerStyleGalleryClass();
                    num3 = (gallery as IStyleGalleryStorage).FileCount - 1;
                    while (num3 >= 0)
                    {
                        str = (gallery as IStyleGalleryStorage).get_File(num3);
                        (gallery as IStyleGalleryStorage).RemoveFile(str);
                        num3--;
                    }
                }
                else
                {
                    gallery = new MyStyleGallery();
                }
                gallery.Clear();
                (gallery as IStyleGalleryStorage).TargetFile = this.txtDest.Text;
                foreach (object obj2 in this.listBox1.Items)
                {
                    try
                    {
                        IStyleGallery gallery2;
                        if (this.m_IsStyleFile)
                        {
                            gallery2 = new MyStyleGallery();
                        }
                        else
                        {
                            gallery2 = new ServerStyleGalleryClass();
                            num3 = (gallery2 as IStyleGalleryStorage).FileCount - 1;
                            while (num3 >= 0)
                            {
                                str = (gallery2 as IStyleGalleryStorage).get_File(num3);
                                (gallery2 as IStyleGalleryStorage).RemoveFile(str);
                                num3--;
                            }
                        }
                        gallery2.Clear();
                        (gallery2 as IStyleGalleryStorage).AddFile(obj2.ToString());
                        for (num3 = 0; num3 < gallery2.ClassCount; num3++)
                        {
                            string name = gallery2.get_Class(num3).Name;
                            IEnumBSTR mbstr = gallery2.get_Categories(name);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(obj2.ToString());
                            mbstr.Reset();
                            for (string str4 = mbstr.Next(); str4 != null; str4 = mbstr.Next())
                            {
                                IEnumStyleGalleryItem item = gallery2.get_Items(name, obj2.ToString(), str4);
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
                                    string str6;
                                    if (this.m_IsStyleFile)
                                    {
                                        IStyleGalleryItem item3 = new ServerStyleGalleryItemClass();
                                        object obj3 = item2.Item;
                                        item3.Item = obj3;
                                        string str5 = item2.Name;
                                        item3.Name = str5;
                                        if (item2.Category.Length > 0)
                                        {
                                            str6 = fileNameWithoutExtension + "_" + item2.Category;
                                            item3.Category = str6;
                                        }
                                        else
                                        {
                                            item3.Category = fileNameWithoutExtension;
                                        }
                                        try
                                        {
                                            string defaultStylePath = (gallery as IStyleGalleryStorage).DefaultStylePath;
                                            int fileCount = (gallery as IStyleGalleryStorage).FileCount;
                                            gallery.AddItem(item3);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    else
                                    {
                                        if (item2.Category.Length > 0)
                                        {
                                            str6 = fileNameWithoutExtension + "_" + item2.Category;
                                            item2.Category = str6;
                                        }
                                        else
                                        {
                                            item2.Category = fileNameWithoutExtension;
                                        }
                                        gallery.AddItem(item2);
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
                        (gallery2 as IStyleGalleryStorage).RemoveFile(obj2.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
                try
                {
                    (gallery as IStyleGalleryStorage).RemoveFile(this.txtDest.Text);
                }
                catch
                {
                }
                base.Close();
            }
        }

        private void btnSaveTo_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count == 0)
            {
                MessageBox.Show("请选择要转换的源符号库");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (!this.m_IsStyleFile)
                {
                    dialog.Filter = "*.style|*.style";
                }
                else
                {
                    dialog.Filter = "*.ServerStyle|*.ServerStyle";
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtDest.Text = dialog.FileName;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (this.listBox1.Items.Count == 0)
            {
                dialog.Filter = "*.style|*.style|*.ServerStyle|*.ServerStyle";
            }
            else if (this.m_IsStyleFile)
            {
                dialog.Filter = "*.style|*.style";
            }
            else
            {
                dialog.Filter = "*.ServerStyle|*.ServerStyle";
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