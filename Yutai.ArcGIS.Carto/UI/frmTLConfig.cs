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
    internal class frmTLConfig : Form
    {
        private Button btnDele;
        private Button btnDeleteAll;
        private Button btnMoveDown;
        private Button btnMoveUp;
        private Button btnOpen;
        private Button butNewRow;
        private Button button1;
        private Button button2;
        private CheckBox chkHasBorder;
        private CheckBox chkItemHasBorder;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private Image image_0 = null;
        private Image image_1 = null;
        private Image image_2 = null;
        private ImageList imageList_0;
        private int int_0 = 50;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labPoint;
        private RenderInfoListView renderInfoListView1;
        private string string_0 = "";
        private TextBox txtColumnNum;
        private TextBox txtColumnSpace;
        private TextBox txtHeight;
        private TextBox txtLabelSpace;
        private TextBox txtLegend;
        private TextBox txtRowSpace;
        private TextBox txtWidth;

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
            OpenFileDialog dialog = new OpenFileDialog {
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
            frmNewLegendItem item = new frmNewLegendItem {
                StyleGallery = ApplicationBase.StyleGallery
            };
            if (item.ShowDialog() == DialogResult.OK)
            {
                object[] objArray = new object[] { item.YTLegendItem.Symbol, item.YTLegendItem.Description };
                this.renderInfoListView1.Add(objArray).Tag = item.YTLegendItem;
                this.btnDeleteAll.Enabled = this.renderInfoListView1.Items.Count > 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.method_8(dialog.FileName);
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmTLConfig_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTLConfig));
            this.txtLegend = new TextBox();
            this.labPoint = new Label();
            this.btnDele = new Button();
            this.butNewRow = new Button();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.renderInfoListView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.txtColumnNum = new TextBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.btnDeleteAll = new Button();
            this.btnMoveDown = new Button();
            this.btnMoveUp = new Button();
            this.chkItemHasBorder = new CheckBox();
            this.label8 = new Label();
            this.txtLabelSpace = new TextBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.txtHeight = new TextBox();
            this.label4 = new Label();
            this.txtWidth = new TextBox();
            this.label3 = new Label();
            this.txtRowSpace = new TextBox();
            this.label2 = new Label();
            this.chkHasBorder = new CheckBox();
            this.txtColumnSpace = new TextBox();
            this.label9 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            this.label10 = new Label();
            this.label11 = new Label();
            this.btnOpen = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.txtLegend.Location = new Point(0x43, 6);
            this.txtLegend.Name = "txtLegend";
            this.txtLegend.Size = new Size(0x12d, 0x15);
            this.txtLegend.TabIndex = 0x22;
            this.labPoint.AutoSize = true;
            this.labPoint.Location = new Point(7, 9);
            this.labPoint.Name = "labPoint";
            this.labPoint.Size = new Size(0x3b, 12);
            this.labPoint.TabIndex = 0x25;
            this.labPoint.Text = "图例标题:";
            this.btnDele.Location = new Point(0x8f, 0xc3);
            this.btnDele.Name = "btnDele";
            this.btnDele.Size = new Size(70, 0x17);
            this.btnDele.TabIndex = 0x2b;
            this.btnDele.Text = "删除";
            this.btnDele.UseVisualStyleBackColor = true;
            this.btnDele.Click += new EventHandler(this.btnDele_Click);
            this.butNewRow.Location = new Point(0x3a, 0xc3);
            this.butNewRow.Name = "butNewRow";
            this.butNewRow.Size = new Size(70, 0x17);
            this.butNewRow.TabIndex = 0x2c;
            this.butNewRow.Text = "添加";
            this.butNewRow.UseVisualStyleBackColor = true;
            this.butNewRow.Click += new EventHandler(this.butNewRow_Click);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "Buffer.ico");
            this.renderInfoListView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.renderInfoListView1.FullRowSelect = true;
            this.renderInfoListView1.Location = new Point(7, 0x4e);
            this.renderInfoListView1.Name = "renderInfoListView1";
            this.renderInfoListView1.Size = new Size(0x124, 0x6f);
            this.renderInfoListView1.TabIndex = 0x30;
            this.renderInfoListView1.UseCompatibleStateImageBehavior = false;
            this.renderInfoListView1.View = View.Details;
            this.renderInfoListView1.SelectedIndexChanged += new EventHandler(this.renderInfoListView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "图例项符号";
            this.columnHeader_0.Width = 110;
            this.columnHeader_1.Text = "图例项说明";
            this.columnHeader_1.Width = 0x84;
            this.txtColumnNum.Location = new Point(0x43, 0x2a);
            this.txtColumnNum.Name = "txtColumnNum";
            this.txtColumnNum.Size = new Size(0x75, 0x15);
            this.txtColumnNum.TabIndex = 0x31;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x2d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "图例列数:";
            this.groupBox1.Controls.Add(this.btnDeleteAll);
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.chkItemHasBorder);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtLabelSpace);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.renderInfoListView1);
            this.groupBox1.Controls.Add(this.butNewRow);
            this.groupBox1.Controls.Add(this.btnDele);
            this.groupBox1.Location = new Point(9, 0x77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x167, 0xf6);
            this.groupBox1.TabIndex = 0x33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图例项信息";
            this.btnDeleteAll.Location = new Point(0xdb, 0xc3);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 0x18);
            this.btnDeleteAll.TabIndex = 0x42;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Image = (System.Drawing.Image)resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(0x131, 0x76);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x18, 0x18);
            this.btnMoveDown.TabIndex = 0x41;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Image = (System.Drawing.Image)resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x131, 0x4e);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 0x40;
            this.btnMoveUp.Text = "z";
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.chkItemHasBorder.AutoSize = true;
            this.chkItemHasBorder.Location = new Point(0xe4, 0x30);
            this.chkItemHasBorder.Name = "chkItemHasBorder";
            this.chkItemHasBorder.Size = new Size(0x60, 0x10);
            this.chkItemHasBorder.TabIndex = 0x3f;
            this.chkItemHasBorder.Text = "是否添加边框";
            this.chkItemHasBorder.UseVisualStyleBackColor = true;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xbd, 0x31);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 12);
            this.label8.TabIndex = 0x3e;
            this.label8.Text = "厘米";
            this.txtLabelSpace.Location = new Point(0x63, 0x2e);
            this.txtLabelSpace.Name = "txtLabelSpace";
            this.txtLabelSpace.Size = new Size(0x54, 0x15);
            this.txtLabelSpace.TabIndex = 60;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(6, 0x31);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x59, 12);
            this.label7.TabIndex = 0x3d;
            this.label7.Text = "符号与说明间距";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x13e, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 0x3b;
            this.label6.Text = "厘米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x7d, 0x11);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x3a;
            this.label5.Text = "厘米";
            this.txtHeight.Location = new Point(0xe4, 14);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new Size(0x54, 0x15);
            this.txtHeight.TabIndex = 0x38;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xc3, 0x11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x17, 12);
            this.label4.TabIndex = 0x39;
            this.label4.Text = "高:";
            this.txtWidth.Location = new Point(0x23, 14);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0x54, 0x15);
            this.txtWidth.TabIndex = 0x36;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x17, 12);
            this.label3.TabIndex = 0x37;
            this.label3.Text = "宽:";
            this.txtRowSpace.Location = new Point(0x43, 0x48);
            this.txtRowSpace.Name = "txtRowSpace";
            this.txtRowSpace.Size = new Size(0x54, 0x15);
            this.txtRowSpace.TabIndex = 0x34;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x4b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 0x35;
            this.label2.Text = "行间距:";
            this.chkHasBorder.AutoSize = true;
            this.chkHasBorder.Location = new Point(0xf8, 0x2f);
            this.chkHasBorder.Name = "chkHasBorder";
            this.chkHasBorder.Size = new Size(120, 0x10);
            this.chkHasBorder.TabIndex = 0x40;
            this.chkHasBorder.Text = "图例是否添加边框";
            this.chkHasBorder.UseVisualStyleBackColor = true;
            this.txtColumnSpace.Location = new Point(0x102, 0x48);
            this.txtColumnSpace.Name = "txtColumnSpace";
            this.txtColumnSpace.Size = new Size(0x4b, 0x15);
            this.txtColumnSpace.TabIndex = 0x41;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xc6, 0x4b);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x2f, 12);
            this.label9.TabIndex = 0x42;
            this.label9.Text = "列间距:";
            this.button1.Location = new Point(200, 0x173);
            this.button1.Name = "button1";
            this.button1.Size = new Size(70, 0x17);
            this.button1.TabIndex = 0x44;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x12a, 0x173);
            this.button2.Name = "button2";
            this.button2.Size = new Size(70, 0x17);
            this.button2.TabIndex = 0x43;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x9b, 0x4b);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1d, 12);
            this.label10.TabIndex = 0x45;
            this.label10.Text = "厘米";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x153, 0x4b);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1d, 12);
            this.label11.TabIndex = 70;
            this.label11.Text = "厘米";
            this.btnOpen.Location = new Point(9, 0x173);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(70, 0x17);
            this.btnOpen.TabIndex = 0x47;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x17d, 0x195);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.label11);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.txtColumnSpace);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.chkHasBorder);
            base.Controls.Add(this.txtRowSpace);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtColumnNum);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtLegend);
            base.Controls.Add(this.labPoint);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmTLConfig";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "图例设置";
            base.Load += new EventHandler(this.frmTLConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
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
                object[] objArray = new object[] { item.Symbol, item.Description };
                this.renderInfoListView1.Add(objArray).Tag = item;
            }
        }

        private bool method_2(string string_1, string string_2, Image image_3, string string_3, string string_4, string string_5)
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
            IObjectStream stream3 = new ObjectStreamClass {
                Stream = pstm
            };
            stream.GetClassID(out guid);
            stream.Save(pstm, 1);
            ((IMemoryBlobStreamVariant) pstm).ExportToVariant(out obj2);
            System.Array array = obj2 as Array;
            byte[] buffer = new byte[array.Length + 0x10];
            guid.ToByteArray().CopyTo(buffer, 0);
            array.CopyTo(buffer, 0x10);
            return Convert.ToBase64String(buffer);
        }

        private ISymbol method_7(string string_1)
        {
            int num2;
            byte[] buffer = Convert.FromBase64String(string_1);
            int num = buffer.Length - 0x10;
            byte[] b = new byte[0x10];
            for (num2 = 0; num2 < 0x10; num2++)
            {
                b[num2] = buffer[num2];
            }
            Guid clsid = new Guid(b);
            ESRI.ArcGIS.esriSystem.IPersistStream stream = Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid)) as IPersistStream;
            byte[] buffer3 = new byte[num];
            for (num2 = 0; num2 < num; num2++)
            {
                buffer3[num2] = buffer[num2 + 0x10];
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
                    node3.Attributes.Append(this.method_5(document, "description", (item.Tag as YTLegendItem).Description));
                    node3.Attributes.Append(this.method_5(document, "symbol", this.method_6((item.Tag as YTLegendItem).Symbol)));
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

