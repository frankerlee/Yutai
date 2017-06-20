using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmNetworkPropertySheet : Form
    {
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_10;
        private ColumnHeader columnHeader_11;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ColumnHeader columnHeader_5;
        private ColumnHeader columnHeader_6;
        private ColumnHeader columnHeader_7;
        private ColumnHeader columnHeader_8;
        private ColumnHeader columnHeader_9;
        private ComboBox comboBox1;
        private IContainer icontainer_0 = null;
        private INetworkDataset inetworkDataset_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblNetworkElements;
        private Label lblNetworkName;
        private Label lblNetworkType;
        private ListView lsvAttributes;
        private ListView lsvElevation;
        private ListView lsvSource;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;

        public frmNetworkPropertySheet()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmNetworkPropertySheet_Load(object sender, EventArgs e)
        {
            int num2;
            string[] strArray;
            ListViewItem item;
            this.lblNetworkName.Text = (this.inetworkDataset_0 as IDataset).Name;
            this.lblNetworkType.Text = this.method_0(this.inetworkDataset_0.NetworkType);
            INetworkQuery query = this.inetworkDataset_0 as INetworkQuery;
            string str = "";
            str = ((query.get_ElementCount(esriNetworkElementType.esriNETJunction).ToString() + "个连接点\r\n") + query.get_ElementCount(esriNetworkElementType.esriNETEdge).ToString() + "条边\r\n") + query.get_ElementCount(esriNetworkElementType.esriNETTurn).ToString() + "个转向\r\n";
            this.lblNetworkElements.Text = str;
            for (num2 = 0; num2 < this.inetworkDataset_0.SourceCount; num2++)
            {
                strArray = new string[3];
                INetworkSource source = this.inetworkDataset_0.get_Source(num2);
                strArray[0] = source.Name;
                strArray[1] = this.method_1(source.SourceType);
                strArray[2] = this.method_2(source.ElementType);
                item = new ListViewItem(strArray);
                this.lsvSource.Items.Add(item);
                if (source.SourceType == esriNetworkSourceType.esriNSTEdgeFeature)
                {
                    strArray[0] = source.Name;
                    strArray[1] = "From End";
                    strArray[2] = (source as IEdgeFeatureSource).FromElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                    strArray[1] = "To End";
                    strArray[2] = (source as IEdgeFeatureSource).ToElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                    if (source.NetworkSourceDirections == null)
                    {
                    }
                }
                else if (source.SourceType == esriNetworkSourceType.esriNSTJunctionFeature)
                {
                    strArray[0] = source.Name;
                    strArray[1] = "";
                    strArray[2] = (source as IJunctionFeatureSource).ElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                }
            }
            for (num2 = 0; num2 < this.inetworkDataset_0.AttributeCount; num2++)
            {
                strArray = new string[6];
                INetworkAttribute attribute = this.inetworkDataset_0.get_Attribute(num2);
                strArray[0] = "";
                strArray[1] = "";
                strArray[2] = attribute.Name;
                strArray[3] = CommonHelper.GetUsageTypeDescriptor(attribute.UsageType);
                strArray[4] = CommonHelper.GetNetworkUnitTypeDescriptor(attribute.Units);
                strArray[5] = CommonHelper.GetDataTypeDescriptor(attribute.DataType);
                item = new ListViewItem(strArray);
                this.lsvAttributes.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNetworkPropertySheet));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.lblNetworkElements = new Label();
            this.lblNetworkType = new Label();
            this.lblNetworkName = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.lsvSource = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.tabPage3 = new TabPage();
            this.lsvElevation = new ListView();
            this.columnHeader_9 = new ColumnHeader();
            this.columnHeader_10 = new ColumnHeader();
            this.columnHeader_11 = new ColumnHeader();
            this.comboBox1 = new ComboBox();
            this.tabPage4 = new TabPage();
            this.lsvAttributes = new ListView();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.columnHeader_5 = new ColumnHeader();
            this.columnHeader_6 = new ColumnHeader();
            this.columnHeader_7 = new ColumnHeader();
            this.columnHeader_8 = new ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1d5, 0x120);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.lblNetworkElements);
            this.tabPage1.Controls.Add(this.lblNetworkType);
            this.tabPage1.Controls.Add(this.lblNetworkName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1cd, 0x107);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.lblNetworkElements.Location = new Point(0x31, 0x3d);
            this.lblNetworkElements.Name = "lblNetworkElements";
            this.lblNetworkElements.Size = new Size(0x12b, 0x54);
            this.lblNetworkElements.TabIndex = 5;
            this.lblNetworkElements.Text = "　　　　　　　　　";
            this.lblNetworkType.AutoSize = true;
            this.lblNetworkType.Location = new Point(0x31, 0x22);
            this.lblNetworkType.Name = "lblNetworkType";
            this.lblNetworkType.Size = new Size(0x71, 12);
            this.lblNetworkType.TabIndex = 4;
            this.lblNetworkType.Text = "　　　　　　　　　";
            this.lblNetworkName.AutoSize = true;
            this.lblNetworkName.Location = new Point(0x31, 7);
            this.lblNetworkName.Name = "lblNetworkName";
            this.lblNetworkName.Size = new Size(0x71, 12);
            this.lblNetworkName.TabIndex = 3;
            this.lblNetworkName.Text = "　　　　　　　　　";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(7, 0x39);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "元素;";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x22);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "类型;";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名字;";
            this.tabPage2.Controls.Add(this.lsvSource);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1cd, 0x107);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "源";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.lsvSource.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.lsvSource.Location = new Point(6, 0x16);
            this.lsvSource.Name = "lsvSource";
            this.lsvSource.Size = new Size(0x199, 0xdb);
            this.lsvSource.TabIndex = 0;
            this.lsvSource.UseCompatibleStateImageBehavior = false;
            this.lsvSource.View = View.Details;
            this.columnHeader_0.Text = "名字";
            this.columnHeader_0.Width = 0x76;
            this.columnHeader_1.Text = "元素类型";
            this.columnHeader_1.Width = 0x83;
            this.columnHeader_2.Text = "类型";
            this.columnHeader_2.Width = 0x85;
            this.tabPage3.Controls.Add(this.lsvElevation);
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x1cd, 0x107);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "高程";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.lsvElevation.Columns.AddRange(new ColumnHeader[] { this.columnHeader_9, this.columnHeader_10, this.columnHeader_11 });
            this.lsvElevation.Enabled = false;
            this.lsvElevation.FullRowSelect = true;
            this.lsvElevation.Location = new Point(6, 0x11);
            this.lsvElevation.Name = "lsvElevation";
            this.lsvElevation.Size = new Size(420, 0xcd);
            this.lsvElevation.TabIndex = 11;
            this.lsvElevation.UseCompatibleStateImageBehavior = false;
            this.lsvElevation.View = View.Details;
            this.columnHeader_9.Text = "源";
            this.columnHeader_9.Width = 0x65;
            this.columnHeader_10.Text = "End";
            this.columnHeader_10.Width = 0x62;
            this.columnHeader_11.Text = "Field";
            this.columnHeader_11.Width = 110;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0xd5, 70);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Visible = false;
            this.tabPage4.Controls.Add(this.lsvAttributes);
            this.tabPage4.Location = new Point(4, 0x15);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x1cd, 0x107);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.lsvAttributes.Columns.AddRange(new ColumnHeader[] { this.columnHeader_3, this.columnHeader_4, this.columnHeader_5, this.columnHeader_6, this.columnHeader_7, this.columnHeader_8 });
            this.lsvAttributes.Location = new Point(15, 0x17);
            this.lsvAttributes.Name = "lsvAttributes";
            this.lsvAttributes.Size = new Size(0x193, 0xd4);
            this.lsvAttributes.TabIndex = 1;
            this.lsvAttributes.UseCompatibleStateImageBehavior = false;
            this.lsvAttributes.View = View.Details;
            this.columnHeader_3.Text = "!";
            this.columnHeader_3.Width = 0x20;
            this.columnHeader_4.Text = "";
            this.columnHeader_4.Width = 0x24;
            this.columnHeader_5.Text = "名称";
            this.columnHeader_5.Width = 0x2d;
            this.columnHeader_6.Text = "使用方式";
            this.columnHeader_7.Text = "单位";
            this.columnHeader_8.Text = "数据类型";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ed, 0x158);
            base.Controls.Add(this.tabControl1);
            
            base.Name = "frmNetworkPropertySheet";
            this.Text = "网络要素集属性";
            base.Load += new EventHandler(this.frmNetworkPropertySheet_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private string method_0(esriNetworkDatasetType esriNetworkDatasetType_0)
        {
            switch (esriNetworkDatasetType_0)
            {
                case esriNetworkDatasetType.esriNDTGeodatabase:
                    return "Geodatabase-based network dataset";

                case esriNetworkDatasetType.esriNDTShapefile:
                    return "Shapefile-based network dataset";

                case esriNetworkDatasetType.esriNDTSDC:
                    return "SDC-based network dataset";
            }
            return "The network dataset type is unknown";
        }

        private string method_1(esriNetworkSourceType esriNetworkSourceType_0)
        {
            switch (esriNetworkSourceType_0)
            {
                case esriNetworkSourceType.esriNSTSystemJunction:
                    return "SystemJunction";

                case esriNetworkSourceType.esriNSTJunctionFeature:
                    return "JunctionFeature";

                case esriNetworkSourceType.esriNSTEdgeFeature:
                    return "EdgeFeature";

                case esriNetworkSourceType.esriNSTTurnFeature:
                    return "TurnFeature";
            }
            return "NetworkSource";
        }

        private string method_2(esriNetworkElementType esriNetworkElementType_0)
        {
            switch (esriNetworkElementType_0)
            {
                case esriNetworkElementType.esriNETJunction:
                    return "Junction";

                case esriNetworkElementType.esriNETEdge:
                    return "Edge";
            }
            return "Turn";
        }

        public INetworkDataset NetworkDataset
        {
            set
            {
                this.inetworkDataset_0 = value;
            }
        }
    }
}

