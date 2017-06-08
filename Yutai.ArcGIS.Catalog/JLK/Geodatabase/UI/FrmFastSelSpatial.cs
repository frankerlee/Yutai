namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class FrmFastSelSpatial : Form
    {
        private Button btnCancle;
        private Button btnOK;
        private ComboBox cmbBandNum;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private double double_0 = -10000.0;
        private double double_1 = 11474.83645;
        private double double_2 = -10000.0;
        private double double_3 = 11474.83645;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0 = null;
        private ISpatialReference ispatialReference_0;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private ListView lstGeographic;
        private ListView lstProjected;
        private RadioButton rdBand3;
        private RadioButton rdBand6;
        private RadioButton rdBj54;
        private RadioButton rdBj80;
        private RadioButton rdGeo;
        private RadioButton rdPrj;
        private string string_0 = (Application.StartupPath + @"\Application.prj");
        private string string_1;

        public FrmFastSelSpatial()
        {
            this.InitializeComponent();
            this.method_2();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str2;
            if (this.rdGeo.Checked)
            {
                string str;
                if (this.rdBj54.Checked)
                {
                    str = "1954";
                }
                else
                {
                    str = "1980";
                }
                str2 = this.method_4(this.lstGeographic, str);
            }
            else
            {
                string str3;
                string str4;
                if (this.rdBj54.Checked)
                {
                    str3 = "54";
                }
                else
                {
                    str3 = "80";
                }
                if (this.rdBand3.Checked)
                {
                    str4 = "3";
                }
                else
                {
                    str4 = "6";
                }
                string str5 = this.cmbBandNum.Text.Trim();
                str2 = this.method_4(this.lstProjected, str3 + ":" + str4 + ":" + str5);
            }
            if (str2 != "")
            {
                try
                {
                    if (this.Line(str2))
                    {
                        this.ispatialReference_0 = ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(this.string_0);
                        this.method_0();
                        this.ispatialReference_0.SetDomain(this.double_0, this.double_1, this.double_2, this.double_3);
                        base.DialogResult = DialogResult.OK;
                        File.Delete(this.string_0);
                        base.Close();
                    }
                }
                catch
                {
                }
            }
            else
            {
                MessageBox.Show("错误的选择", "提示");
            }
        }

        private void cmbBandNum_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            ListViewItem item = new ListViewItem("54:3:25");
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmFastSelSpatial));
            ListViewItem item2 = new ListViewItem("54:3:26");
            ListViewItem item3 = new ListViewItem("54:3:27");
            ListViewItem item4 = new ListViewItem("54:3:28");
            ListViewItem item5 = new ListViewItem("54:3:29");
            ListViewItem item6 = new ListViewItem("54:3:30");
            ListViewItem item7 = new ListViewItem("54:3:31");
            ListViewItem item8 = new ListViewItem("54:3:32");
            ListViewItem item9 = new ListViewItem("54:3:33");
            ListViewItem item10 = new ListViewItem("54:3:34");
            ListViewItem item11 = new ListViewItem("54:3:35");
            ListViewItem item12 = new ListViewItem("54:3:36");
            ListViewItem item13 = new ListViewItem("54:3:37");
            ListViewItem item14 = new ListViewItem("54:3:38");
            ListViewItem item15 = new ListViewItem("54:3:39");
            ListViewItem item16 = new ListViewItem(new string[] { "54:3:40", "" }, -1);
            ListViewItem item17 = new ListViewItem("54:3:41");
            ListViewItem item18 = new ListViewItem("54:3:42");
            ListViewItem item19 = new ListViewItem("54:3:43");
            ListViewItem item20 = new ListViewItem("54:3:44");
            ListViewItem item21 = new ListViewItem("54:3:45");
            ListViewItem item22 = new ListViewItem("54:6:13");
            ListViewItem item23 = new ListViewItem("54:6:14");
            ListViewItem item24 = new ListViewItem("54:6:15");
            ListViewItem item25 = new ListViewItem("54:6:16");
            ListViewItem item26 = new ListViewItem("54:6:17");
            ListViewItem item27 = new ListViewItem("54:6:18");
            ListViewItem item28 = new ListViewItem("54:6:19");
            ListViewItem item29 = new ListViewItem("54:6:20");
            ListViewItem item30 = new ListViewItem("54:6:21");
            ListViewItem item31 = new ListViewItem("54:6:22");
            ListViewItem item32 = new ListViewItem("54:6:23");
            ListViewItem item33 = new ListViewItem("80:3:25");
            ListViewItem item34 = new ListViewItem("80:3:26");
            ListViewItem item35 = new ListViewItem("80:3:27");
            ListViewItem item36 = new ListViewItem("80:3:28");
            ListViewItem item37 = new ListViewItem("80:3:29");
            ListViewItem item38 = new ListViewItem("80:3:30");
            ListViewItem item39 = new ListViewItem("80:3:31");
            ListViewItem item40 = new ListViewItem("80:3:32");
            ListViewItem item41 = new ListViewItem("80:3:33");
            ListViewItem item42 = new ListViewItem("80:3:34");
            ListViewItem item43 = new ListViewItem("80:3:35");
            ListViewItem item44 = new ListViewItem("80:3:36");
            ListViewItem item45 = new ListViewItem("80:3:37");
            ListViewItem item46 = new ListViewItem("80:3:38");
            ListViewItem item47 = new ListViewItem("80:3:39");
            ListViewItem item48 = new ListViewItem("80:3:40");
            ListViewItem item49 = new ListViewItem("80:3:41");
            ListViewItem item50 = new ListViewItem("80:3:42");
            ListViewItem item51 = new ListViewItem("80:3:43");
            ListViewItem item52 = new ListViewItem("80:3:44");
            ListViewItem item53 = new ListViewItem("80:3:45");
            ListViewItem item54 = new ListViewItem("80:6:13");
            ListViewItem item55 = new ListViewItem("80:6:14");
            ListViewItem item56 = new ListViewItem("80:6:15");
            ListViewItem item57 = new ListViewItem("80:6:16");
            ListViewItem item58 = new ListViewItem("80:6:17");
            ListViewItem item59 = new ListViewItem("80:6:18");
            ListViewItem item60 = new ListViewItem("80:6:19");
            ListViewItem item61 = new ListViewItem("80:6:20");
            ListViewItem item62 = new ListViewItem("80:6:21");
            ListViewItem item63 = new ListViewItem("80:6:22");
            ListViewItem item64 = new ListViewItem("80:6:23");
            ListViewItem item65 = new ListViewItem("1954");
            ListViewItem item66 = new ListViewItem("1980");
            this.groupBox1 = new GroupBox();
            this.rdPrj = new RadioButton();
            this.rdGeo = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.cmbBandNum = new ComboBox();
            this.rdBand6 = new RadioButton();
            this.rdBand3 = new RadioButton();
            this.groupBox3 = new GroupBox();
            this.rdBj80 = new RadioButton();
            this.rdBj54 = new RadioButton();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            this.lstProjected = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.lstGeographic = new ListView();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdPrj);
            this.groupBox1.Controls.Add(this.rdGeo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x116, 0x30);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "坐标系统";
            this.rdPrj.AutoSize = true;
            this.rdPrj.Checked = true;
            this.rdPrj.Location = new System.Drawing.Point(0x92, 20);
            this.rdPrj.Name = "rdPrj";
            this.rdPrj.Size = new Size(0x47, 0x10);
            this.rdPrj.TabIndex = 1;
            this.rdPrj.TabStop = true;
            this.rdPrj.Text = "投影坐标";
            this.rdPrj.UseVisualStyleBackColor = true;
            this.rdPrj.Click += new EventHandler(this.rdPrj_Click);
            this.rdGeo.AutoSize = true;
            this.rdGeo.Location = new System.Drawing.Point(0x12, 20);
            this.rdGeo.Name = "rdGeo";
            this.rdGeo.Size = new Size(0x47, 0x10);
            this.rdGeo.TabIndex = 0;
            this.rdGeo.Text = "地理坐标";
            this.rdGeo.UseVisualStyleBackColor = true;
            this.rdGeo.Click += new EventHandler(this.rdGeo_Click);
            this.groupBox2.Controls.Add(this.cmbBandNum);
            this.groupBox2.Controls.Add(this.rdBand6);
            this.groupBox2.Controls.Add(this.rdBand3);
            this.groupBox2.Location = new System.Drawing.Point(12, 0x7c);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x11c, 0x39);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分带信息";
            this.cmbBandNum.FormattingEnabled = true;
            this.cmbBandNum.Location = new System.Drawing.Point(0xa4, 0x13);
            this.cmbBandNum.Name = "cmbBandNum";
            this.cmbBandNum.Size = new Size(0x72, 20);
            this.cmbBandNum.TabIndex = 2;
            this.cmbBandNum.SelectedIndexChanged += new EventHandler(this.cmbBandNum_SelectedIndexChanged);
            this.rdBand6.AutoSize = true;
            this.rdBand6.Location = new System.Drawing.Point(0x62, 20);
            this.rdBand6.Name = "rdBand6";
            this.rdBand6.Size = new Size(0x35, 0x10);
            this.rdBand6.TabIndex = 1;
            this.rdBand6.Text = "6度带";
            this.rdBand6.UseVisualStyleBackColor = true;
            this.rdBand6.CheckedChanged += new EventHandler(this.rdBand6_CheckedChanged);
            this.rdBand3.AutoSize = true;
            this.rdBand3.Checked = true;
            this.rdBand3.Location = new System.Drawing.Point(0x1d, 20);
            this.rdBand3.Name = "rdBand3";
            this.rdBand3.Size = new Size(0x35, 0x10);
            this.rdBand3.TabIndex = 0;
            this.rdBand3.TabStop = true;
            this.rdBand3.Text = "3度带";
            this.rdBand3.UseVisualStyleBackColor = true;
            this.rdBand3.CheckedChanged += new EventHandler(this.rdBand3_CheckedChanged);
            this.groupBox3.Controls.Add(this.rdBj80);
            this.groupBox3.Controls.Add(this.rdBj54);
            this.groupBox3.Location = new System.Drawing.Point(12, 0x42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x11c, 0x34);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "大地基准面";
            this.rdBj80.AutoSize = true;
            this.rdBj80.Location = new System.Drawing.Point(0x9c, 0x1d);
            this.rdBj80.Name = "rdBj80";
            this.rdBj80.Size = new Size(0x3b, 0x10);
            this.rdBj80.TabIndex = 1;
            this.rdBj80.Text = "西安80";
            this.rdBj80.UseVisualStyleBackColor = true;
            this.rdBj54.AutoSize = true;
            this.rdBj54.Checked = true;
            this.rdBj54.Location = new System.Drawing.Point(30, 0x1c);
            this.rdBj54.Name = "rdBj54";
            this.rdBj54.Size = new Size(0x3b, 0x10);
            this.rdBj54.TabIndex = 0;
            this.rdBj54.TabStop = true;
            this.rdBj54.Text = "北京54";
            this.rdBj54.UseVisualStyleBackColor = true;
            this.btnOK.Location = new System.Drawing.Point(0x8b, 0xbb);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.Location = new System.Drawing.Point(0xdd, 0xbb);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x4b, 0x17);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.lstProjected.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            item.Tag = manager.GetString("listViewItem1.Tag");
            item.ToolTipText = "17250718.461:-10002137.4978:33749281.539:10002137.4978";
            item2.Tag = manager.GetString("listViewItem2.Tag");
            item2.ToolTipText = "18250718.461:-10002137.4978:34749281.539:10002137.4978";
            item3.Tag = manager.GetString("listViewItem3.Tag");
            item3.ToolTipText = "19250718.461:-10002137.4978:35749281.539:10002137.4978";
            item4.Tag = manager.GetString("listViewItem4.Tag");
            item4.ToolTipText = "20250718.461:-10002137.4978:36749281.539:10002137.4978";
            item5.Tag = manager.GetString("listViewItem5.Tag");
            item5.ToolTipText = "21250718.461:-10002137.4978:37749281.539:10002137.4978";
            item6.Tag = manager.GetString("listViewItem6.Tag");
            item6.ToolTipText = "22250718.461:-10002137.4978:38749281.539:10002137.4978";
            item7.Tag = manager.GetString("listViewItem7.Tag");
            item7.ToolTipText = "23250718.461:-10002137.4978:39749281.539:10002137.4978";
            item8.Tag = manager.GetString("listViewItem8.Tag");
            item8.ToolTipText = "24250718.461:-10002137.4978:40749281.539:10002137.4978";
            item9.Tag = manager.GetString("listViewItem9.Tag");
            item9.ToolTipText = "25250718.461:-10002137.4978:41749281.539:10002137.4978";
            item10.Tag = manager.GetString("listViewItem10.Tag");
            item10.ToolTipText = "26250718.461:-10002137.4978:42749281.539:10002137.4978";
            item11.Tag = manager.GetString("listViewItem11.Tag");
            item11.ToolTipText = "27250718.461:-10002137.4978:43749281.539:10002137.4978";
            item12.Tag = manager.GetString("listViewItem12.Tag");
            item12.ToolTipText = "28250718.461:-10002137.4978:44749281.539:10002137.4978";
            item13.Tag = manager.GetString("listViewItem13.Tag");
            item13.ToolTipText = "29250718.461:-10002137.4978:45749281.539:10002137.4978";
            item14.Tag = manager.GetString("listViewItem14.Tag");
            item14.ToolTipText = "30250718.461:-10002137.4978:46749281.539:10002137.4978";
            item15.Tag = manager.GetString("listViewItem15.Tag");
            item15.ToolTipText = "31250718.461:-10002137.4978:47749281.539:10002137.4978";
            item16.Tag = manager.GetString("listViewItem16.Tag");
            item16.ToolTipText = "32250718.461:-10002137.4978:48749281.539:10002137.4978";
            item17.Tag = manager.GetString("listViewItem17.Tag");
            item17.ToolTipText = "33250718.461:-10002137.4978:49749281.539:10002137.4978";
            item18.Tag = manager.GetString("listViewItem18.Tag");
            item18.ToolTipText = "34250718.461:-10002137.4978:50749281.539:10002137.4978";
            item19.Tag = manager.GetString("listViewItem19.Tag");
            item19.ToolTipText = "35250718.461:-10002137.4978:51749281.539:10002137.4978";
            item20.Tag = manager.GetString("listViewItem20.Tag");
            item20.ToolTipText = "36250718.461:-10002137.4978:52749281.539:10002137.4978";
            item21.Tag = manager.GetString("listViewItem21.Tag");
            item21.ToolTipText = "37250718.461:-10002137.4978:53749281.539:10002137.4978";
            item22.Tag = manager.GetString("listViewItem22.Tag");
            item22.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item23.Tag = manager.GetString("listViewItem23.Tag");
            item23.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item24.Tag = manager.GetString("listViewItem24.Tag");
            item24.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item25.Tag = manager.GetString("listViewItem25.Tag");
            item25.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item26.Tag = manager.GetString("listViewItem26.Tag");
            item26.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item27.Tag = manager.GetString("listViewItem27.Tag");
            item27.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item28.Tag = manager.GetString("listViewItem28.Tag");
            item28.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item29.Tag = manager.GetString("listViewItem29.Tag");
            item29.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item30.Tag = manager.GetString("listViewItem30.Tag");
            item30.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item31.Tag = manager.GetString("listViewItem31.Tag");
            item31.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item32.Tag = manager.GetString("listViewItem32.Tag");
            item32.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item33.Tag = manager.GetString("listViewItem33.Tag");
            item33.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item34.Tag = manager.GetString("listViewItem34.Tag");
            item34.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item35.Tag = manager.GetString("listViewItem35.Tag");
            item35.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item36.Tag = manager.GetString("listViewItem36.Tag");
            item36.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item37.Tag = manager.GetString("listViewItem37.Tag");
            item37.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item38.Tag = manager.GetString("listViewItem38.Tag");
            item38.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item39.Tag = manager.GetString("listViewItem39.Tag");
            item39.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item40.Tag = manager.GetString("listViewItem40.Tag");
            item40.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item41.Tag = manager.GetString("listViewItem41.Tag");
            item41.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item42.Tag = manager.GetString("listViewItem42.Tag");
            item42.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item43.Tag = manager.GetString("listViewItem43.Tag");
            item43.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item44.Tag = manager.GetString("listViewItem44.Tag");
            item44.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item45.Tag = manager.GetString("listViewItem45.Tag");
            item45.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item46.Tag = manager.GetString("listViewItem46.Tag");
            item46.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item47.Tag = manager.GetString("listViewItem47.Tag");
            item47.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item48.Tag = manager.GetString("listViewItem48.Tag");
            item48.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item49.Tag = manager.GetString("listViewItem49.Tag");
            item49.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item50.Tag = manager.GetString("listViewItem50.Tag");
            item50.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item51.Tag = manager.GetString("listViewItem51.Tag");
            item51.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item52.Tag = manager.GetString("listViewItem52.Tag");
            item52.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item53.Tag = manager.GetString("listViewItem53.Tag");
            item53.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item54.Tag = manager.GetString("listViewItem54.Tag");
            item54.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item55.Tag = manager.GetString("listViewItem55.Tag");
            item55.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item56.Tag = manager.GetString("listViewItem56.Tag");
            item56.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item57.Tag = manager.GetString("listViewItem57.Tag");
            item57.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item58.Tag = manager.GetString("listViewItem58.Tag");
            item58.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item59.Tag = manager.GetString("listViewItem59.Tag");
            item59.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item60.Tag = manager.GetString("listViewItem60.Tag");
            item60.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item61.Tag = manager.GetString("listViewItem61.Tag");
            item61.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item62.Tag = manager.GetString("listViewItem62.Tag");
            item62.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item63.Tag = manager.GetString("listViewItem63.Tag");
            item63.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            item64.Tag = manager.GetString("listViewItem64.Tag");
            item64.ToolTipText = "17250718.461:-10002137.4978:53749281.539:10002137.4978";
            this.lstProjected.Items.AddRange(new ListViewItem[] { 
                item, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16, 
                item17, item18, item19, item20, item21, item22, item23, item24, item25, item26, item27, item28, item29, item30, item31, item32, 
                item33, item34, item35, item36, item37, item38, item39, item40, item41, item42, item43, item44, item45, item46, item47, item48, 
                item49, item50, item51, item52, item53, item54, item55, item56, item57, item58, item59, item60, item61, item62, item63, item64
             });
            this.lstProjected.Location = new System.Drawing.Point(12, 0x175);
            this.lstProjected.Name = "lstProjected";
            this.lstProjected.Size = new Size(0x11c, 0x72);
            this.lstProjected.TabIndex = 5;
            this.lstProjected.UseCompatibleStateImageBehavior = false;
            this.lstProjected.View = View.Details;
            this.columnHeader_0.Text = "坐标系";
            this.columnHeader_0.Width = 0xe8;
            this.lstGeographic.Columns.AddRange(new ColumnHeader[] { this.columnHeader_2, this.columnHeader_3 });
            item65.Tag = "GEOGCS[\"GCS_Beijing_1954\",DATUM[\"D_Beijing_1954\",SPHEROID[\"Krasovsky_1940\",6378245,298.3]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]]";
            item66.Tag = "GEOGCS[\"GCS_Xian_1980\",DATUM[\"D_Xian_1980\",SPHEROID[\"Xian_1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]";
            this.lstGeographic.Items.AddRange(new ListViewItem[] { item65, item66 });
            this.lstGeographic.Location = new System.Drawing.Point(12, 0xf2);
            this.lstGeographic.Name = "lstGeographic";
            this.lstGeographic.Size = new Size(0x11c, 0x7d);
            this.lstGeographic.TabIndex = 6;
            this.lstGeographic.UseCompatibleStateImageBehavior = false;
            this.lstGeographic.View = View.Details;
            this.columnHeader_2.Text = "坐标系";
            this.columnHeader_2.Width = 0xe8;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x131, 0xe2);
            base.Controls.Add(this.lstGeographic);
            base.Controls.Add(this.lstProjected);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmFastSelSpatial";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "选择投影参考";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        public bool Line(string string_2)
        {
            StreamWriter writer = new StreamWriter(this.string_0, false);
            bool flag = false;
            try
            {
                writer.WriteLine(string_2);
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                throw exception;
            }
            finally
            {
                writer.Dispose();
                writer.Close();
                writer = null;
            }
            return flag;
        }

        private void method_0()
        {
            string str = "";
            str = this.method_1(this.string_1, ':', 0);
            if (str != "")
            {
                this.double_0 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 1);
            if (str != "")
            {
                this.double_2 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 2);
            if (str != "")
            {
                this.double_1 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 3);
            if (str != "")
            {
                this.double_3 = Convert.ToDouble(str);
            }
        }

        private string method_1(string string_2, char char_0, int int_0)
        {
            try
            {
                string[] strArray = string_2.Split(new char[] { char_0 });
                if (strArray.GetUpperBound(0) >= 0)
                {
                    return strArray[int_0];
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        private void method_2()
        {
            int num;
            this.cmbBandNum.Text = "";
            this.cmbBandNum.Items.Clear();
            if (this.rdBand3.Checked)
            {
                for (num = 0x19; num <= 0x2d; num++)
                {
                    this.cmbBandNum.Items.Add(num.ToString());
                }
            }
            else
            {
                for (num = 13; num <= 0x17; num++)
                {
                    this.cmbBandNum.Items.Add(num.ToString());
                }
            }
        }

        private void method_3(bool bool_0)
        {
            this.groupBox2.Enabled = bool_0;
            this.rdBand3.Enabled = bool_0;
            this.rdBand6.Enabled = bool_0;
            this.cmbBandNum.Enabled = bool_0;
        }

        private string method_4(ListView listView_0, string string_2)
        {
            for (int i = 0; i < listView_0.Items.Count; i++)
            {
                if (string_2.ToUpper() == listView_0.Items[i].Text.ToUpper())
                {
                    this.string_1 = listView_0.Items[i].ToolTipText;
                    return listView_0.Items[i].Tag.ToString();
                }
            }
            return "";
        }

        private void rdBand3_CheckedChanged(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void rdBand6_CheckedChanged(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void rdGeo_Click(object sender, EventArgs e)
        {
            this.method_3(false);
        }

        private void rdPrj_Click(object sender, EventArgs e)
        {
            this.method_3(true);
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.ispatialReference_0;
            }
        }
    }
}

