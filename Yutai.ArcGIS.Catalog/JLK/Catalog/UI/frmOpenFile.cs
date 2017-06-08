namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.DataSourcesGDB;
    using ESRI.ArcGIS.DataSourcesOleDB;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.ControlExtend;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class frmOpenFile : Form
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private SimpleButton btnAdd;
        private SimpleButton btnCancel;
        private SimpleButton btnDetial;
        private SimpleButton btnLargeIcon;
        private SimpleButton btnList;
        private SimpleButton btnUpper;
        private ComboBoxEdit cboShowType;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private FolderBrowserDialog folderBrowserDialog_0;
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IContainer icontainer_0;
        private IGxCatalog igxCatalog_0;
        private IGxObject igxObject_0 = null;
        private IGxObjectFilter igxObjectFilter_0 = null;
        private IList ilist_0 = new List<IGxObject>();
        private ImageComboBoxEditEx imageComboBoxEdit1;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private int int_0 = 0;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        internal static object m_pStartingLocation;
        private SortedList<string, int> sortedList_0 = new SortedList<string, int>();
        private string string_0 = "";
        private string string_1 = "";
        private TextEdit txtName;

        static frmOpenFile()
        {
            old_acctor_mc();
        }

        public frmOpenFile()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
                base.TopMost = true;
                this.string_1 = System.IO.Path.Combine(Application.StartupPath, "Location.dat");
                if (File.Exists(this.string_1))
                {
                    StreamReader reader = File.OpenText(this.string_1);
                    m_pStartingLocation = reader.ReadLine();
                    reader.Close();
                }
            }
        }

        public void AddFilter(IGxObjectFilter igxObjectFilter_1, bool bool_2)
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                IGxObjectFilter filter = this.iarray_0.get_Element(i) as IGxObjectFilter;
                if (filter.Name == igxObjectFilter_1.Name)
                {
                    if (bool_2)
                    {
                        this.igxObjectFilter_0 = filter;
                    }
                    return;
                }
            }
            this.iarray_0.Add(igxObjectFilter_1);
            if (bool_2)
            {
                this.igxObjectFilter_0 = igxObjectFilter_1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((this.txtName.Text.Length > 1) && (this.txtName.Text[1] == ':'))
            {
                this.method_3(this.txtName.Text);
            }
            if (this.int_0 == 0)
            {
                this.method_16();
            }
            else if (this.int_0 == 1)
            {
                this.method_18();
            }
            else
            {
                this.method_19();
            }
        }

        private void btnDetial_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }

        private void btnLargeIcon_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.List;
        }

        private void btnUpper_Click(object sender, EventArgs e)
        {
            ImageComboBoxItemEx ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
            if (ex.Degree != 0)
            {
                if (ex.Degree == 1)
                {
                    this.imageComboBoxEdit1.SelectedIndex = 0;
                }
                else
                {
                    int num = this.imageComboBoxEdit1.SelectedIndex - 1;
                    this.imageComboBoxEdit1.SelectedIndex = num;
                }
            }
        }

        private void cboShowType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboShowType.SelectedIndex == this.iarray_0.Count)
            {
                this.igxObjectFilter_0 = null;
            }
            else
            {
                this.igxObjectFilter_0 = this.iarray_0.get_Element(this.cboShowType.SelectedIndex) as IGxObjectFilter;
            }
            this.method_8();
            ImageComboBoxItemEx ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
            IGxObject tag = ex.Tag as IGxObject;
            this.method_10(tag);
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2)
            {
                if (this.icontainer_0 != null)
                {
                    this.icontainer_0.Dispose();
                }
                this.igxCatalog_0 = null;
            }
            base.Dispose(bool_2);
        }

        public DialogResult DoModalOpen()
        {
            try
            {
                this.int_0 = 0;
                this.listView1.MultiSelect = this.bool_1;
                this.btnAdd.Text = "添加";
                return base.ShowDialog();
            }
            catch (Exception)
            {
            }
            return DialogResult.Cancel;
        }

        public DialogResult DoModalSave()
        {
            this.int_0 = 1;
            this.btnAdd.Text = "保存";
            this.bool_1 = false;
            this.listView1.MultiSelect = this.bool_1;
            return base.ShowDialog();
        }

        public DialogResult DoModalSaveLocation()
        {
            this.int_0 = 2;
            this.btnAdd.Text = "保存";
            this.bool_1 = false;
            this.listView1.MultiSelect = this.bool_1;
            return base.ShowDialog();
        }

        private void frmOpenFile_Load(object sender, EventArgs e)
        {
            if (this.igxCatalog_0 == null)
            {
                this.igxCatalog_0 = new JLK.Catalog.GxCatalog();
            }
            ImageComboBoxItemEx item = new ImageComboBoxItemEx((this.igxCatalog_0 as IGxObject).Name, this.igxCatalog_0, this.method_2(this.igxCatalog_0 as IGxObject), 0) {
                Tag = this.igxCatalog_0
            };
            this.imageComboBoxEdit1.Properties.Items.Add(item);
            IEnumGxObject children = (this.igxCatalog_0 as IGxObjectContainer).Children;
            children.Reset();
            for (IGxObject obj3 = children.Next(); obj3 != null; obj3 = children.Next())
            {
                item = new ImageComboBoxItemEx(obj3.Name, obj3.FullName, this.method_2(obj3), 1) {
                    Tag = obj3
                };
                this.imageComboBoxEdit1.Properties.Items.Add(item);
            }
            this.imageComboBoxEdit1.SelectedIndex = 0;
            this.iarray_1.RemoveAll();
            this.ilist_0.Clear();
            this.cboShowType.Properties.Items.Clear();
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                IGxObjectFilter filter = this.iarray_0.get_Element(i) as IGxObjectFilter;
                this.cboShowType.Properties.Items.Add(filter.Description);
                if (filter == this.igxObjectFilter_0)
                {
                    this.cboShowType.SelectedIndex = i;
                }
            }
            if ((this.int_0 == 0) && (this.iarray_0.Count > 1))
            {
                this.cboShowType.Properties.Items.Add("已列出的所有过滤条件");
            }
            if (this.cboShowType.SelectedIndex == -1)
            {
                this.cboShowType.SelectedIndex = this.cboShowType.Properties.Items.Count - 1;
            }
            if ((m_pStartingLocation != null) && (m_pStartingLocation is string))
            {
                this.method_3(m_pStartingLocation as string);
            }
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                ImageComboBoxItemEx ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
                IGxObject tag = ex.Tag as IGxObject;
                this.method_10(tag);
                this.method_12();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmOpenFile));
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.folderBrowserDialog_0 = new FolderBrowserDialog();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtName = new TextEdit();
            this.btnAdd = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnUpper = new SimpleButton();
            this.imageComboBoxEdit1 = new ImageComboBoxEditEx();
            this.cboShowType = new ComboBoxEdit();
            this.btnLargeIcon = new SimpleButton();
            this.btnList = new SimpleButton();
            this.btnDetial = new SimpleButton();
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.txtName.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.cboShowType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找";
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList_0;
            this.listView1.Location = new System.Drawing.Point(8, 40);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x1d8, 0xb0);
            this.listView1.SmallImageList = this.imageList_0;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyUp += new KeyEventHandler(this.listView1_KeyUp);
            this.listView1.Click += new EventHandler(this.listView1_Click);
            this.columnHeader_0.Text = "名字";
            this.columnHeader_0.Width = 0xd6;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 0xcf;
            this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.TransparentColor = Color.Magenta;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0xe8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "名字";
            this.label3.Location = new System.Drawing.Point(8, 0x100);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x40, 0x10);
            this.label3.TabIndex = 4;
            this.label3.Text = "显示类型";
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(80, 0xe0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x120, 0x15);
            this.txtName.TabIndex = 6;
            this.btnAdd.Location = new System.Drawing.Point(0x188, 0xe0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x38, 0x18);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x188, 0x100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnUpper.Image = (Image) manager.GetObject("btnUpper.Image");
            this.btnUpper.Location = new System.Drawing.Point(360, 8);
            this.btnUpper.Name = "btnUpper";
            this.btnUpper.Size = new Size(0x18, 0x18);
            this.btnUpper.TabIndex = 10;
            this.btnUpper.ToolTip = "上一级";
            this.btnUpper.Click += new EventHandler(this.btnUpper_Click);
            this.imageComboBoxEdit1.EditValue = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(0x38, 8);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(0x128, 0x15);
            this.imageComboBoxEdit1.TabIndex = 11;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            this.cboShowType.EditValue = "";
            this.cboShowType.Location = new System.Drawing.Point(80, 0x100);
            this.cboShowType.Name = "cboShowType";
            this.cboShowType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboShowType.Size = new Size(0x120, 0x15);
            this.cboShowType.TabIndex = 12;
            this.cboShowType.SelectedIndexChanged += new EventHandler(this.cboShowType_SelectedIndexChanged);
            this.btnLargeIcon.Image = (Image) manager.GetObject("btnLargeIcon.Image");
            this.btnLargeIcon.Location = new System.Drawing.Point(400, 8);
            this.btnLargeIcon.Name = "btnLargeIcon";
            this.btnLargeIcon.Size = new Size(0x18, 0x18);
            this.btnLargeIcon.TabIndex = 13;
            this.btnLargeIcon.ToolTip = "大图标";
            this.btnLargeIcon.Click += new EventHandler(this.btnLargeIcon_Click);
            this.btnList.Image = (Image) manager.GetObject("btnList.Image");
            this.btnList.Location = new System.Drawing.Point(0x1a8, 8);
            this.btnList.Name = "btnList";
            this.btnList.Size = new Size(0x18, 0x18);
            this.btnList.TabIndex = 14;
            this.btnList.ToolTip = "列表";
            this.btnList.Click += new EventHandler(this.btnList_Click);
            this.btnDetial.Image = (Image) manager.GetObject("btnDetial.Image");
            this.btnDetial.Location = new System.Drawing.Point(0x1c0, 8);
            this.btnDetial.Name = "btnDetial";
            this.btnDetial.Size = new Size(0x18, 0x18);
            this.btnDetial.TabIndex = 15;
            this.btnDetial.ToolTip = "详细信息";
            this.btnDetial.Click += new EventHandler(this.btnDetial_Click);
            this.imageList_1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_1.ImageSize = new Size(0x10, 0x10);
            this.imageList_1.TransparentColor = Color.Transparent;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(490, 0x11f);
            base.Controls.Add(this.btnDetial);
            base.Controls.Add(this.btnList);
            base.Controls.Add(this.btnLargeIcon);
            base.Controls.Add(this.cboShowType);
            base.Controls.Add(this.imageComboBoxEdit1);
            base.Controls.Add(this.btnUpper);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmOpenFile";
            base.ShowInTaskbar = false;
            base.Load += new EventHandler(this.frmOpenFile_Load);
            this.txtName.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            this.cboShowType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                MyDoubleClickResult myDCRShowChildren;
                IGxObject tag = this.listView1.SelectedItems[0].Tag as IGxObject;
                if ((this.igxObjectFilter_0 != null) && (tag is IGxDatabase))
                {
                    myDCRShowChildren = MyDoubleClickResult.myDCRShowChildren;
                    this.igxObjectFilter_0.CanChooseObject(tag, ref myDCRShowChildren);
                    if (myDCRShowChildren == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        this.method_4();
                        return;
                    }
                }
                if (!(tag is IGxObjectContainer))
                {
                    if (tag is IGxNewDatabase)
                    {
                        IWorkspaceName name;
                        IGxObject obj3;
                        ListViewItem item;
                        if (tag.FullName == "添加OLE DB连接")
                        {
                            try
                            {
                                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Documents and Settings\Administrator\Application Data\ESRI\ArcCatalog\";
                                string str2 = path + "OLE DB Connection.odc";
                                if (Directory.Exists(path))
                                {
                                    str2 = this.method_6(str2);
                                    IWorkspaceFactory factory = new OLEDBWorkspaceFactoryClass();
                                    name = factory.Create(path, System.IO.Path.GetFileName(str2), null, 0);
                                    obj3 = new GxDatabase();
                                    (obj3 as IGxDatabase).WorkspaceName = name;
                                    obj3.Attach(tag.Parent, this.igxCatalog_0);
                                    item = new ListViewItem(new string[] { obj3.Name, obj3.Category }, this.method_2(obj3)) {
                                        Tag = obj3
                                    };
                                    this.listView1.Items.Add(item);
                                }
                            }
                            catch (Exception exception)
                            {
                                exception.ToString();
                            }
                        }
                        else if (tag.FullName == "添加空间数据库连接")
                        {
                            frmCreateGDBConnection connection = new frmCreateGDBConnection {
                                TopMost = true
                            };
                            if (connection.ShowDialog() == DialogResult.OK)
                            {
                                obj3 = new GxDatabase();
                                name = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                                    PathName = connection.ConnectionPath
                                };
                                (obj3 as IGxDatabase).WorkspaceName = name;
                                obj3.Attach(tag.Parent, this.igxCatalog_0);
                                item = new ListViewItem(new string[] { obj3.Name, obj3.Category }, this.method_2(obj3)) {
                                    Tag = obj3
                                };
                                this.listView1.Items.Add(item);
                            }
                        }
                    }
                    else
                    {
                        this.method_4();
                    }
                }
                else
                {
                    ImageComboBoxItemEx ex;
                    if (tag is IGxDataset)
                    {
                        esriDatasetType type = (tag as IGxDataset).Type;
                        myDCRShowChildren = MyDoubleClickResult.myDCRShowChildren;
                        this.igxObjectFilter_0.CanChooseObject(tag, ref myDCRShowChildren);
                        if ((((type != esriDatasetType.esriDTFeatureDataset) && (type != esriDatasetType.esriDTContainer)) && ((type != esriDatasetType.esriDTRasterCatalog) && (type != esriDatasetType.esriDTCadDrawing))) && (type != esriDatasetType.esriDTRasterDataset))
                        {
                            this.method_4();
                            return;
                        }
                    }
                    this.method_10(tag);
                    this.bool_0 = false;
                    if (tag.Parent is IGxCatalog)
                    {
                        for (int i = 0; i < this.imageComboBoxEdit1.Properties.Items.Count; i++)
                        {
                            ex = this.imageComboBoxEdit1.Properties.Items[i] as ImageComboBoxItemEx;
                            if (ex.Tag == tag)
                            {
                                this.imageComboBoxEdit1.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
                        int degree = ex.Degree;
                        ex = new ImageComboBoxItemEx(tag.Name, tag.FullName, this.method_2(tag), degree + 1) {
                            Tag = tag
                        };
                        int selectedIndex = this.imageComboBoxEdit1.SelectedIndex;
                        this.imageComboBoxEdit1.Properties.Items.Insert(selectedIndex + 1, ex);
                        this.imageComboBoxEdit1.SelectedIndex = selectedIndex + 1;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.listView1_DoubleClick(sender, new EventArgs());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem current;
            string text = "";
            if ((this.int_0 == 0) || ((this.igxObjectFilter_0 != null) && (this.igxObjectFilter_0.Name == "GxFilterWorkspaces")))
            {
                using (IEnumerator enumerator = this.listView1.SelectedItems.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        current = (ListViewItem) enumerator.Current;
                        if (text.Length > 0)
                        {
                            text = text + "; " + current.Text;
                        }
                        else
                        {
                            text = current.Text;
                        }
                    }
                    goto Label_00E5;
                }
            }
            if (((this.int_0 == 2) && (this.igxObjectFilter_0 != null)) && (this.listView1.SelectedItems.Count > 0))
            {
                current = this.listView1.SelectedItems[0];
                text = current.Text;
            }
        Label_00E5:
            this.txtName.Text = text;
        }

        private string method_0(IFeatureClass ifeatureClass_0)
        {
            switch (ifeatureClass_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        private string method_1(IFeatureClassName ifeatureClassName_0)
        {
            switch (ifeatureClassName_0.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return " POINT";

                case esriGeometryType.esriGeometryMultipoint:
                    return " POINT";

                case esriGeometryType.esriGeometryPolyline:
                    return " LINE";

                case esriGeometryType.esriGeometryPolygon:
                    return " FILL";
            }
            return "";
        }

        private void method_10(IGxObject igxObject_1)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            string[] items = new string[2];
            this.method_8();
            if (igxObject_1 is IGxObjectContainer)
            {
                if (igxObject_1 is IGxDatabase)
                {
                    if (!(igxObject_1 as IGxDatabase).IsConnected)
                    {
                        (igxObject_1 as IGxDatabase).Connect();
                    }
                    if (!(igxObject_1 as IGxDatabase).IsConnected)
                    {
                        return;
                    }
                }
                else if (igxObject_1 is IGxAGSConnection)
                {
                    if (!(igxObject_1 as IGxAGSConnection).IsConnected)
                    {
                        (igxObject_1 as IGxAGSConnection).Connect();
                    }
                    if (!(igxObject_1 as IGxAGSConnection).IsConnected)
                    {
                        return;
                    }
                }
                IEnumGxObject children = (igxObject_1 as IGxObjectContainer).Children;
                children.Reset();
                IGxObject obj3 = children.Next();
                bool flag = true;
                IGxObjectFilter filter = null;
                while (obj3 != null)
                {
                    if (obj3 is IGxNewDatabase)
                    {
                        flag = true;
                    }
                    else if (this.igxObjectFilter_0 != null)
                    {
                        flag = this.igxObjectFilter_0.CanDisplayObject(obj3);
                    }
                    else
                    {
                        for (int i = 0; i < this.iarray_0.Count; i++)
                        {
                            filter = this.iarray_0.get_Element(i) as IGxObjectFilter;
                            if (flag = filter.CanDisplayObject(obj3))
                            {
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        items[0] = obj3.Name;
                        items[1] = obj3.Category;
                        ListViewItem item = new ListViewItem(items, this.method_2(obj3)) {
                            Tag = obj3
                        };
                        this.listView1.Items.Add(item);
                    }
                    obj3 = children.Next();
                }
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private bool method_11(IGxObject igxObject_1, IGxObject igxObject_2)
        {
            for (IGxObject obj2 = igxObject_2.Parent; obj2 != null; obj2 = obj2.Parent)
            {
                if (igxObject_1 == obj2)
                {
                    return true;
                }
            }
            return false;
        }

        private void method_12()
        {
            ImageComboBoxItemEx ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
            IGxObject tag = ex.Tag as IGxObject;
            for (int i = this.imageComboBoxEdit1.Properties.Items.Count - 1; i > 0; i--)
            {
                ex = this.imageComboBoxEdit1.Properties.Items[i] as ImageComboBoxItemEx;
                IGxObject obj3 = ex.Tag as IGxObject;
                if ((!(obj3 is IGxCatalog) && !(obj3.Parent is IGxCatalog)) && ((obj3 != tag) && !this.method_11(obj3, tag)))
                {
                    this.imageComboBoxEdit1.Properties.Items.RemoveAt(i);
                }
            }
        }

        private void method_13(string string_2)
        {
        }

        private string method_14(string string_2)
        {
            return "";
        }

        private bool method_15(string string_2)
        {
            return true;
        }

        private void method_16()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.txtName.Text.Trim();
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    IGxObject tag = item.Tag as IGxObject;
                    if ((((tag != null) && !(tag is IGxDataset)) && this.method_5(tag)) || (tag is IGxNewDatabase))
                    {
                        return;
                    }
                }
                this.method_4();
            }
        }

        private string method_17(string string_2, IGxObjectFilter igxObjectFilter_1)
        {
            string str = "";
            string name = igxObjectFilter_1.Name;
            if (name != null)
            {
                if (!(name == "RasterFormatTifFilter"))
                {
                    if (name == "RasterFormatImgFilter")
                    {
                        str = ".img";
                    }
                }
                else
                {
                    str = ".tif";
                }
            }
            if (str == "")
            {
                return string_2;
            }
            int num = string_2.LastIndexOf(".");
            if ((num != -1) && (string_2.Substring(num + 1).ToLower() != "tif"))
            {
                return (string_2 + str);
            }
            return (string_2 + str);
        }

        private void method_18()
        {
            if (this.txtName.Text.Trim().Length > 0)
            {
                ImageComboBoxItemEx selectedItem = this.imageComboBoxEdit1.SelectedItem as ImageComboBoxItemEx;
                IGxObject tag = selectedItem.Tag as IGxObject;
                if ((tag != null) && (this.igxObjectFilter_0 != null))
                {
                    bool flag = false;
                    string str = this.txtName.Text.Trim();
                    if (this.igxObjectFilter_0.CanSaveObject(tag, str, ref flag))
                    {
                        this.igxObject_0 = tag;
                        this.iarray_1.RemoveAll();
                        this.ilist_0.Clear();
                        if (this.igxObjectFilter_0.Name == "GxFilterWorkspaces")
                        {
                            IWorkspaceName name = null;
                            IGxObject unk = null;
                            unk = new GxDatabase();
                            name = new WorkspaceNameClass();
                            string path = (tag as IGxFile).Path + @"\" + str;
                            string str3 = System.IO.Path.GetExtension(path).ToLower();
                            if (str3 == ".mdb")
                            {
                                name.WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory";
                            }
                            else if ((str3 == ".sde") && flag)
                            {
                                name.WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                            }
                            else
                            {
                                name.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                            }
                            if (!flag)
                            {
                                if (str3 == ".mdb")
                                {
                                    IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
                                    try
                                    {
                                        factory.Create(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path), null, 0);
                                    }
                                    catch (Exception exception)
                                    {
                                        MessageBox.Show(this, exception.Message);
                                    }
                                }
                                else
                                {
                                    Directory.CreateDirectory(path);
                                }
                            }
                            name.PathName = path;
                            (unk as IGxDatabase).WorkspaceName = name;
                            this.iarray_1.Add(unk);
                            this.ilist_0.Add(unk);
                        }
                        else
                        {
                            if (((tag is IGxFolder) || (tag is IGxDiskConnection)) || (tag is IGxDatabase))
                            {
                                this.iarray_1.Add(tag);
                                this.ilist_0.Add(tag);
                            }
                            else
                            {
                                if (!(tag is IGxDataset))
                                {
                                    return;
                                }
                                if ((tag as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    this.iarray_1.Add(tag);
                                    this.ilist_0.Add(tag);
                                }
                            }
                            this.string_0 = this.method_17(str, this.igxObjectFilter_0);
                        }
                        base.DialogResult = DialogResult.OK;
                        if (selectedItem.Tag != null)
                        {
                            m_pStartingLocation = (selectedItem.Tag as IGxObject).FullName;
                        }
                        base.Close();
                    }
                }
            }
        }

        private void method_19()
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IGxObject tag = item.Tag as IGxObject;
                if ((tag != null) && (this.igxObjectFilter_0 != null))
                {
                    MyDoubleClickResult myDCRDefault = MyDoubleClickResult.myDCRDefault;
                    this.igxObjectFilter_0.CanChooseObject(tag, ref myDCRDefault);
                    if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        this.iarray_1.RemoveAll();
                        this.iarray_1.Add(tag);
                        this.ilist_0.Clear();
                        this.ilist_0.Add(tag);
                        base.DialogResult = DialogResult.OK;
                        ImageComboBoxItemEx selectedItem = this.imageComboBoxEdit1.SelectedItem as ImageComboBoxItemEx;
                        if (selectedItem.Tag != null)
                        {
                            m_pStartingLocation = (selectedItem.Tag as IGxObject).FullName;
                        }
                        base.Close();
                    }
                }
            }
        }

        private int method_2(IGxObject igxObject_1)
        {
            IFeatureClass featureClass;
            int num = 0;
            string category = igxObject_1.Category;
            if (igxObject_1 is IGxDataset)
            {
                if ((igxObject_1 as IGxDataset).Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName datasetName = (igxObject_1 as IGxDataset).DatasetName as IFeatureClassName;
                    if (datasetName.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        category = category + " 注记";
                    }
                    else if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
                    {
                        try
                        {
                            featureClass = (datasetName as IName).Open() as IFeatureClass;
                            category = category + this.method_0(featureClass);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        category = category + this.method_1(datasetName);
                    }
                }
            }
            else if (igxObject_1 is IGxDatabase)
            {
                if ((igxObject_1 as IGxDatabase).IsRemoteDatabase && (igxObject_1 as IGxDatabase).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (igxObject_1 is IGxVCTLayerObject)
            {
                category = "VCT" + (igxObject_1 as IGxVCTLayerObject).LayerTypeName;
            }
            else if (igxObject_1 is IGxLayer)
            {
                ILayer layer = (igxObject_1 as IGxLayer).Layer;
                if (layer == null)
                {
                    category = category + " Unknown";
                }
                else if (layer is IGroupLayer)
                {
                    category = category + " GroupLayer";
                }
                else if (layer is IRasterLayer)
                {
                    category = category + " RasterLayer";
                }
                else if (layer is IFeatureLayer)
                {
                    featureClass = (layer as IFeatureLayer).FeatureClass;
                    if (featureClass == null)
                    {
                        category = category + " Unknown";
                    }
                    else
                    {
                        category = category + this.method_0(featureClass);
                    }
                }
            }
            else if ((igxObject_1 is IGxDiskConnection) && !Directory.Exists((igxObject_1 as IGxFile).Path))
            {
                category = category + "_Error";
            }
            if (this.sortedList_0.ContainsKey(category))
            {
                return this.sortedList_0[category];
            }
            this.imageList_0.Images.Add((igxObject_1 as IGxObjectUI).SmallImage);
            num = this.imageList_0.Images.Count - 1;
            this.sortedList_0.Add(category, num);
            return num;
        }

        private void method_3(string string_2)
        {
            string[] strArray = string_2.Split(new char[] { '\\' });
            IEnumGxObject children = (this.igxCatalog_0 as IGxObjectContainer).Children;
            children.Reset();
            IGxObject obj3 = null;
            if ((strArray[0] == "数据库连接") || (strArray[0] == "Database Connections"))
            {
                for (obj3 = children.Next(); obj3 != null; obj3 = children.Next())
                {
                    if (obj3 is IGxRemoteDatabaseFolder)
                    {
                        break;
                    }
                }
            }
            else
            {
                obj3 = children.Next();
                strArray[0] = strArray[0] + @"\";
                while (obj3 != null)
                {
                    if (string.Compare(obj3.Name, strArray[0], true) == 0)
                    {
                        break;
                    }
                    obj3 = children.Next();
                }
            }
            if (obj3 != null)
            {
                this.method_7(obj3);
                MyDoubleClickResult myDCRNothing = MyDoubleClickResult.myDCRNothing;
                if (this.igxObjectFilter_0 != null)
                {
                    this.igxObjectFilter_0.CanChooseObject(obj3, ref myDCRNothing);
                }
                if (myDCRNothing != MyDoubleClickResult.myDCRChooseAndDismiss)
                {
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        if (strArray[i] != "")
                        {
                            if (!(obj3 is IGxObjectContainer))
                            {
                                break;
                            }
                            children = (obj3 as IGxObjectContainer).Children;
                            children.Reset();
                            obj3 = children.Next();
                            while (obj3 != null)
                            {
                                if (string.Compare(obj3.Name, strArray[i], true) == 0)
                                {
                                    break;
                                }
                                obj3 = children.Next();
                            }
                            if (obj3 == null)
                            {
                                string str = "";
                                for (int j = i; j < strArray.Length; j++)
                                {
                                    if (j != i)
                                    {
                                        str = str + @"\";
                                    }
                                    str = str + strArray[j];
                                }
                                this.txtName.Text = str;
                                break;
                            }
                            if (this.igxObjectFilter_0 != null)
                            {
                                this.igxObjectFilter_0.CanChooseObject(obj3, ref myDCRNothing);
                            }
                            if (myDCRNothing == MyDoubleClickResult.myDCRChooseAndDismiss)
                            {
                                break;
                            }
                            this.method_7(obj3);
                        }
                    }
                }
            }
        }

        private void method_4()
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                IGxObject tag = item.Tag as IGxObject;
                if (tag != null)
                {
                    this.iarray_1.Add(tag);
                    this.ilist_0.Add(tag);
                }
            }
            base.DialogResult = DialogResult.OK;
            ImageComboBoxItemEx selectedItem = this.imageComboBoxEdit1.SelectedItem as ImageComboBoxItemEx;
            this.igxObject_0 = selectedItem.Tag as IGxObject;
            m_pStartingLocation = this.igxObject_0.FullName;
            if (m_pStartingLocation != null)
            {
                StreamWriter writer = null;
                try
                {
                    writer = File.CreateText(this.string_1);
                    writer.WriteLine(m_pStartingLocation.ToString());
                    writer.Close();
                }
                catch (Exception)
                {
                }
            }
            base.Close();
        }

        private bool method_5(IGxObject igxObject_1)
        {
            ImageComboBoxItemEx ex;
            MyDoubleClickResult myDCRDefault = MyDoubleClickResult.myDCRDefault;
            if (this.igxObjectFilter_0 != null)
            {
                this.igxObjectFilter_0.CanChooseObject(igxObject_1, ref myDCRDefault);
                if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                {
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    (this.iarray_0.get_Element(i) as IGxObjectFilter).CanChooseObject(igxObject_1, ref myDCRDefault);
                    if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        return false;
                    }
                }
            }
            if (!(igxObject_1 is IGxObjectContainer))
            {
                return false;
            }
            this.method_10(igxObject_1);
            this.bool_0 = false;
            if (igxObject_1.Parent is IGxCatalog)
            {
                for (int j = 0; j < this.imageComboBoxEdit1.Properties.Items.Count; j++)
                {
                    ex = this.imageComboBoxEdit1.Properties.Items[j] as ImageComboBoxItemEx;
                    if (ex.Tag == igxObject_1)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = j;
                        break;
                    }
                }
            }
            else
            {
                ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
                int degree = ex.Degree;
                ex = new ImageComboBoxItemEx(igxObject_1.Name, igxObject_1.FullName, this.method_2(igxObject_1), degree + 1) {
                    Tag = igxObject_1
                };
                this.imageComboBoxEdit1.Properties.Items.Insert(this.imageComboBoxEdit1.SelectedIndex + 1, ex);
                this.imageComboBoxEdit1.SelectedIndex++;
            }
            this.bool_0 = true;
            return true;
        }

        private string method_6(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").odc";
            }
            return string_2;
        }

        private void method_7(IGxObject igxObject_1)
        {
            ImageComboBoxItemEx ex;
            this.method_10(igxObject_1);
            this.bool_0 = false;
            if (igxObject_1.Parent is IGxCatalog)
            {
                for (int i = 0; i < this.imageComboBoxEdit1.Properties.Items.Count; i++)
                {
                    ex = this.imageComboBoxEdit1.Properties.Items[i] as ImageComboBoxItemEx;
                    if (ex.Tag == igxObject_1)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                ex = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex] as ImageComboBoxItemEx;
                int degree = ex.Degree;
                ex = new ImageComboBoxItemEx(igxObject_1.Name, igxObject_1.FullName, this.method_2(igxObject_1), degree + 1) {
                    Tag = igxObject_1
                };
                this.imageComboBoxEdit1.Properties.Items.Insert(this.imageComboBoxEdit1.SelectedIndex + 1, ex);
                this.imageComboBoxEdit1.SelectedIndex++;
            }
            this.bool_0 = true;
        }

        private void method_8()
        {
            this.listView1.SelectedItems.Clear();
            this.listView1.Items.Clear();
        }

        private void method_9(object object_0)
        {
            if (!(base.IsDisposed || !base.IsHandleCreated))
            {
                Class1 class2 = (Class1) object_0;
                class2.Set();
                base.Invoke(new Delegate0(this.method_10), new object[] { class2.PGxObject });
            }
        }

        private static void old_acctor_mc()
        {
            m_pStartingLocation = null;
        }

        public void RemoveAllFilters()
        {
            this.iarray_0.RemoveAll();
        }

        public bool AllowMultiSelect
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public string ButtonCaption
        {
            set
            {
                this.btnAdd.Text = value;
            }
        }

        public IGxObject FinalLocation
        {
            get
            {
                return this.igxObject_0;
            }
        }

        public IGxCatalog GxCatalog
        {
            set
            {
                this.igxCatalog_0 = value;
            }
        }

        public IArray Items
        {
            get
            {
                return this.iarray_1;
            }
        }

        public IGxObjectFilter ObjectFilter
        {
            get
            {
                return this.igxObjectFilter_0;
            }
            set
            {
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    IGxObjectFilter filter = this.iarray_0.get_Element(i) as IGxObjectFilter;
                    if (filter.Name == value.Name)
                    {
                        this.igxObjectFilter_0 = filter;
                        return;
                    }
                }
                this.iarray_0.Add(value);
                this.igxObjectFilter_0 = value;
            }
        }

        public bool ReplacingObject
        {
            get
            {
                return false;
            }
        }

        public string SaveName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public IList SelectedItems
        {
            get
            {
                return this.ilist_0;
            }
        }

        public object StartingLocation
        {
            set
            {
                m_pStartingLocation = value;
            }
        }

        private class Class1
        {
            public ManualResetEvent ManualEvent;
            public IGxObject PGxObject;

            public Class1(IGxObject igxObject_0, ManualResetEvent manualResetEvent_0)
            {
                this.ManualEvent = manualResetEvent_0;
                this.PGxObject = igxObject_0;
            }

            public void Set()
            {
                if (this.ManualEvent != null)
                {
                    this.ManualEvent.Set();
                }
            }
        }

        private delegate void Delegate0(IGxObject igxObject_0);
    }
}

