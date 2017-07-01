using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Array = System.Array;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmTLConfig : Form
    {
        private IContainer icontainer_0 = null;
        private Image image_0 = null;
        private Image image_1 = null;
        private Image image_2 = null;
        private int int_0 = 50;
        private string string_0 = "";

        public frmTLConfig()
        {
            this.InitializeComponent();
        }

        private void btnDele_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = this.renderInfoListView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                index = this.renderInfoListView1.SelectedIndices[i];
                this.renderInfoListView1.Items.RemoveAt(index);
            }
            this.btnDeleteAll.Enabled = this.renderInfoListView1.Items.Count > 0;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.renderInfoListView1.Items.Clear();
            this.btnDeleteAll.Enabled = this.renderInfoListView1.Items.Count > 0;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int index = this.renderInfoListView1.SelectedIndices[0];
            ListViewItem item = this.renderInfoListView1.Items[index];
            this.renderInfoListView1.Items.RemoveAt(index);
            index++;
            if (index == this.renderInfoListView1.Items.Count)
            {
                this.renderInfoListView1.Items.Add(item);
            }
            else
            {
                this.renderInfoListView1.Items.Insert(index, item);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int index = this.renderInfoListView1.SelectedIndices[0];
            ListViewItem item = this.renderInfoListView1.Items[index];
            this.renderInfoListView1.Items.RemoveAt(index);
            index--;
            this.renderInfoListView1.Items.Insert(index, item);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                XmlElement documentElement = document.DocumentElement;
                this.method_9(documentElement);
                this.btnDeleteAll.Enabled = this.renderInfoListView1.Items.Count > 0;
            }
        }

        private void butNewRow_Click(object sender, EventArgs e)
        {
            frmNewLegendItem item = new frmNewLegendItem
            {
                StyleGallery = ApplicationBase.StyleGallery
            };
            if (item.ShowDialog() == DialogResult.OK)
            {
                object[] objArray = new object[] {item.YTLegendItem.Symbol, item.YTLegendItem.Description};
                this.renderInfoListView1.Add(objArray).Tag = item.YTLegendItem;
                this.btnDeleteAll.Enabled = this.renderInfoListView1.Items.Count > 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.method_8(dialog.FileName);
            }
        }

        private void frmTLConfig_Load(object sender, EventArgs e)
        {
        }

        private void method_0(string string_1)
        {
        }

        private Image method_1(object object_0)
        {
            MemoryStream stream = null;
            Bitmap image = null;
            Bitmap bitmap2 = null;
            Graphics graphics = null;
            if (object_0 is DBNull)
            {
                return null;
            }
            try
            {
                byte[] buffer = object_0 as byte[];
                stream = new MemoryStream(buffer);
                image = new Bitmap(stream);
                bitmap2 = new Bitmap(image.Width, image.Height, PixelFormat.Format16bppRgb555);
                graphics = Graphics.FromImage(bitmap2);
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return bitmap2;
        }

        private void method_10(XmlNode xmlNode_0)
        {
            int num = 0;
            Label_0002:
            if (num >= xmlNode_0.Attributes.Count)
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "legenditem")
                    {
                        this.method_11(node);
                    }
                }
            }
            else
            {
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "width":
                        this.txtWidth.Text = attribute.Value;
                        break;

                    case "height":
                        this.txtHeight.Text = attribute.Value;
                        break;

                    case "labelspace":
                        this.txtLabelSpace.Text = attribute.Value;
                        break;

                    case "hasborder":
                        try
                        {
                            this.chkItemHasBorder.Checked = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
                goto Label_0002;
            }
        }

        private void method_11(XmlNode xmlNode_0)
        {
            ISymbol symbol = null;
            string str = "";
            for (int i = 0; i < xmlNode_0.Attributes.Count; i++)
            {
                XmlAttribute attribute = xmlNode_0.Attributes[i];
                switch (attribute.Name.ToLower())
                {
                    case "description":
                        str = attribute.Value;
                        break;

                    case "symbol":
                        symbol = this.method_7(attribute.Value);
                        break;
                }
            }
            if (symbol != null)
            {
                YTLegendItem item = new YTLegendItem(symbol, str);
                object[] objArray = new object[] {item.Symbol, item.Description};
                this.renderInfoListView1.Add(objArray).Tag = item;
            }
        }

        private bool method_2(string string_1, string string_2, Image image_3, string string_3, string string_4,
            string string_5)
        {
            return false;
        }

        private bool method_3(string string_1, int int_1)
        {
            return false;
        }

        private void method_4(object sender, EventArgs e)
        {
        }

        private XmlAttribute method_5(XmlDocument xmlDocument_0, string string_1, string string_2)
        {
            XmlAttribute attribute = xmlDocument_0.CreateAttribute(string_1);
            attribute.Value = string_2;
            return attribute;
        }

        private string method_6(ISymbol isymbol_0)
        {
            Guid guid;
            object obj2;
            ESRI.ArcGIS.esriSystem.IPersistStream stream = (ESRI.ArcGIS.esriSystem.IPersistStream) isymbol_0;
            IMemoryBlobStream pstm = new MemoryBlobStreamClass();
            IObjectStream stream3 = new ObjectStreamClass
            {
                Stream = pstm
            };
            stream.GetClassID(out guid);
            stream.Save(pstm, 1);
            ((IMemoryBlobStreamVariant) pstm).ExportToVariant(out obj2);
            System.Array array = obj2 as Array;
            byte[] buffer = new byte[array.Length + 16];
            guid.ToByteArray().CopyTo(buffer, 0);
            array.CopyTo(buffer, 16);
            return Convert.ToBase64String(buffer);
        }

        private ISymbol method_7(string string_1)
        {
            int num2;
            byte[] buffer = Convert.FromBase64String(string_1);
            int num = buffer.Length - 16;
            byte[] b = new byte[16];
            for (num2 = 0; num2 < 16; num2++)
            {
                b[num2] = buffer[num2];
            }
            Guid clsid = new Guid(b);
            ESRI.ArcGIS.esriSystem.IPersistStream stream =
                Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid)) as IPersistStream;
            byte[] buffer3 = new byte[num];
            for (num2 = 0; num2 < num; num2++)
            {
                buffer3[num2] = buffer[num2 + 16];
            }
            IMemoryBlobStream pstm = new MemoryBlobStreamClass();
            ((IMemoryBlobStreamVariant) pstm).ImportFromVariant(buffer3);
            stream.Load(pstm);
            return (stream as ISymbol);
        }

        private void method_8(string string_1)
        {
            XmlDocument document = new XmlDocument();
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version='1.0' encoding='utf-8' ?>");
            builder.Append("<Legend>");
            builder.Append("</Legend>");
            document.LoadXml(builder.ToString());
            XmlNode node = document.ChildNodes[1];
            node.Attributes.Append(this.method_5(document, "Title", this.txtLegend.Text));
            node.Attributes.Append(this.method_5(document, "column", this.txtColumnNum.Text));
            node.Attributes.Append(this.method_5(document, "rowspace", this.txtRowSpace.Text));
            node.Attributes.Append(this.method_5(document, "columnspace", this.txtColumnSpace.Text));
            node.Attributes.Append(this.method_5(document, "HasBorder", this.chkHasBorder.Checked.ToString()));
            XmlNode newChild = document.CreateElement("LegendItems");
            newChild.Attributes.Append(this.method_5(document, "width", this.txtWidth.Text));
            newChild.Attributes.Append(this.method_5(document, "height", this.txtHeight.Text));
            newChild.Attributes.Append(this.method_5(document, "labelspace", this.txtLabelSpace.Text));
            newChild.Attributes.Append(this.method_5(document, "HasBorder", this.chkItemHasBorder.Checked.ToString()));
            node.AppendChild(newChild);
            for (int i = 0; i < this.renderInfoListView1.Items.Count; i++)
            {
                ListViewItem item = this.renderInfoListView1.Items[i];
                if (item.Tag is YTLegendItem)
                {
                    XmlNode node3 = document.CreateElement("LegendItem");
                    node3.Attributes.Append(this.method_5(document, "description",
                        (item.Tag as YTLegendItem).Description));
                    node3.Attributes.Append(this.method_5(document, "symbol",
                        this.method_6((item.Tag as YTLegendItem).Symbol)));
                    newChild.AppendChild(node3);
                }
            }
            if (File.Exists(string_1))
            {
                File.Delete(string_1);
            }
            document.Save(string_1);
        }

        private void method_9(XmlNode xmlNode_0)
        {
            int num = 0;
            Label_0002:
            if (num >= xmlNode_0.Attributes.Count)
            {
                for (int i = 0; i < xmlNode_0.ChildNodes.Count; i++)
                {
                    XmlNode node = xmlNode_0.ChildNodes[i];
                    if (node.Name.ToLower() == "legenditems")
                    {
                        this.method_10(node);
                    }
                }
            }
            else
            {
                XmlAttribute attribute = xmlNode_0.Attributes[num];
                switch (attribute.Name.ToLower())
                {
                    case "title":
                        this.txtLegend.Text = attribute.Value;
                        break;

                    case "column":
                        this.txtColumnNum.Text = attribute.Value;
                        break;

                    case "rowspace":
                        this.txtRowSpace.Text = attribute.Value;
                        break;

                    case "columnspace":
                        this.txtColumnSpace.Text = attribute.Value;
                        break;

                    case "hasborder":
                        try
                        {
                            this.chkHasBorder.Checked = bool.Parse(attribute.Value);
                        }
                        catch
                        {
                        }
                        break;
                }
                num++;
                goto Label_0002;
            }
        }

        private void renderInfoListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDele.Enabled = this.renderInfoListView1.SelectedIndices.Count > 0;
            if (this.renderInfoListView1.SelectedIndices.Count > 0)
            {
                if (this.renderInfoListView1.SelectedIndices.Count == 1)
                {
                    if (this.renderInfoListView1.SelectedIndices[0] == 0)
                    {
                        if (this.renderInfoListView1.Items.Count == 1)
                        {
                            this.btnMoveDown.Enabled = false;
                        }
                        else
                        {
                            this.btnMoveDown.Enabled = true;
                        }
                        this.btnMoveUp.Enabled = false;
                    }
                    else if (this.renderInfoListView1.SelectedIndices[0] == (this.renderInfoListView1.Items.Count - 1))
                    {
                        this.btnMoveDown.Enabled = false;
                        this.btnMoveUp.Enabled = true;
                    }
                    else
                    {
                        this.btnMoveDown.Enabled = true;
                        this.btnMoveUp.Enabled = true;
                    }
                }
                else
                {
                    this.btnMoveDown.Enabled = false;
                    this.btnMoveUp.Enabled = false;
                }
            }
            else
            {
                this.btnMoveDown.Enabled = false;
                this.btnMoveUp.Enabled = false;
            }
        }
    }
}