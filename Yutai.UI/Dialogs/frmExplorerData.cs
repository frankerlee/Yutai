using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Catalog;
using Yutai.UI.Controls;

namespace Yutai.UI.Dialogs
{
    public partial class frmExplorerData : Form
    {
        private IArray _filterArray=new ESRI.ArcGIS.esriSystem.Array();
        private IArray _array2=new ESRI.ArcGIS.esriSystem.Array();

        private IGxCatalog _gxCatalog;
        private IGxObject _gxObject;
        private IGxObjectFilter _gxObjectFilter;
        private SortedList<string,int> _sortedList=new SortedList<string, int>();
        private IList<IGxObject> _gxObjects=new List<IGxObject>();
        private static object _pStartLocation;

        private bool _multiSelect = false;
        private int _modalType=0;

        public int ModalType
        {
            get { return _modalType; }
            set { _modalType = value; }
        }
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }
        public frmExplorerData()
        {
            InitializeComponent();
            base.TopMost = true;
            this.listView1.SmallImageList = imageList1;
            this.listView1.LargeImageList = imageList1;
            this.gisDataComboBox1.GISDataImageList = imageList1;
            _isFree = true;
        }

        public void AddFilter(IGxObjectFilter objectFilter, bool isSelect)
        {
            for (int i = 0; i < _filterArray.Count; i++)
            {
                IGxObjectFilter filter=_filterArray.Element[i] as IGxObjectFilter;
                if (filter.Name != objectFilter.Name)
                {
                    if (isSelect)
                    {
                        this._gxObjectFilter = filter;
                    }
                    return;
                }
            }
            _filterArray.Add(objectFilter);
            if (isSelect)
            {
                this._gxObjectFilter = objectFilter;
            }

        }

        private void btnLarge_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.List;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            
        }

        private void btnNewGDB_Click(object sender, EventArgs e)
        {
            
        }

        private void btnUpper_Click(object sender, EventArgs e)
        {
            GISDataComboItem comboItem=gisDataComboBox1.SelectedItem as GISDataComboItem;
            if (comboItem == null) return;
            if (comboItem.Level != 0)
            {
                if (comboItem.Level == 1)
                {
                    gisDataComboBox1.SelectedIndex = 0;
                }
                else
                {
                    int num = gisDataComboBox1.SelectedIndex - 1;
                    gisDataComboBox1.SelectedIndex = num;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.txtName.Text.Length > 1) && (this.txtName.Text[1] == ':'))
            {
                this.LoadViewer(this.txtName.Text);
            }
            if (this._modalType == 0)
            {
                this.EndOpen();
            }
            else if (this._modalType == 1)
            {
                this.EndSave();
            }
            else
            {
                this.EndLocation();
            }
        }
        private void EndLocation()
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IGxObject tag = item.Tag as IGxObject;
                if ((tag != null) && (this._gxObjectFilter != null))
                {
                    MyDoubleClickResult myDCRDefault = MyDoubleClickResult.myDCRDefault;
                    this._gxObjectFilter.CanChooseObject(tag, ref myDCRDefault);
                    if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        this._array2.RemoveAll();
                        this._array2.Add(tag);
                        this._gxObjects.Clear();
                        this._gxObjects.Add(tag);
                        base.DialogResult = DialogResult.OK;
                        GISDataComboItem selectedItem = this.gisDataComboBox1.SelectedItem as GISDataComboItem;
                        if (selectedItem.Tag != null)
                        {
                            _pStartLocation = (selectedItem.Tag as IGxObject).FullName;
                        }
                        base.Close();
                    }
                }
            }
        }

        private void EndSave()
        {
            if (this.txtName.Text.Trim().Length > 0)
            {
                GISDataComboItem selectedItem = this.gisDataComboBox1.SelectedItem as GISDataComboItem;
                IGxObject tag = selectedItem.Tag as IGxObject;
                if ((tag != null) && (this._gxObjectFilter != null))
                {
                    bool flag = false;
                    string str = this.txtName.Text.Trim();
                    if (this._gxObjectFilter.CanSaveObject(tag, str, ref flag))
                    {
                        this._gxObject = tag;
                        this._array2.RemoveAll();
                        this._gxObjects.Clear();
                        if (this._gxObjectFilter.Name == "GxFilterWorkspaces")
                        {
                            IWorkspaceName name = null;
                            IGxObject unk = null;
                            unk = new GxDatabase() as IGxObject;
                            name = new WorkspaceName() as IWorkspaceName;
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
                                    IWorkspaceFactory factory = new AccessWorkspaceFactory() as IWorkspaceFactory;
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
                            this._array2.Add(unk);
                            this._gxObjects.Add(unk);
                        }
                        else
                        {
                            if (((tag is IGxFolder) || (tag is IGxDiskConnection)) || (tag is IGxDatabase))
                            {
                                this._array2.Add(tag);
                                this._gxObjects.Add(tag);
                            }
                            else
                            {
                                if (!(tag is IGxDataset))
                                {
                                    return;
                                }
                                if ((tag as IGxDataset).Type == esriDatasetType.esriDTFeatureDataset)
                                {
                                    this._array2.Add(tag);
                                    this._gxObjects.Add(tag);
                                }
                            }
                            this._saveName = this.CheckExtension(str, this._gxObjectFilter);
                        }
                        base.DialogResult = DialogResult.OK;
                        if (selectedItem.Tag != null)
                        {
                            _pStartLocation = (selectedItem.Tag as IGxObject).FullName;
                        }
                        base.Close();
                    }
                }
            }
        }

        public string _saveName;
        public string SaveName
        {
            get
            {
                return this._saveName;
            }
            set
            {
                this._saveName = value;
            }
        }

        public IList SelectedItems
        {
            get
            {
                return this._gxObjects as IList;
            }
        }

        public object StartingLocation
        {
            set
            {
                _pStartLocation = value;
            }
        }

        private string CheckExtension(string string_2, IGxObjectFilter igxObjectFilter_1)
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

        private bool CheckObject(IGxObject gxObject)
        {
            GISDataComboItem ex;
            MyDoubleClickResult myDCRDefault = MyDoubleClickResult.myDCRDefault;
            if (this._gxObjectFilter != null)
            {
                this._gxObjectFilter.CanChooseObject(gxObject, ref myDCRDefault);
                if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                {
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < this._filterArray.Count; i++)
                {
                    (this._filterArray.Element[i] as IGxObjectFilter).CanChooseObject(gxObject, ref myDCRDefault);
                    if (myDCRDefault == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        return false;
                    }
                }
            }
            if (!(gxObject is IGxObjectContainer))
            {
                return false;
            }
            this.LoadViewer(gxObject);
            this._isFree = false;
            if (gxObject.Parent is IGxCatalog)
            {
                for (int j = 0; j < this.gisDataComboBox1.Items.Count; j++)
                {
                    ex = this.gisDataComboBox1.Items[j] as GISDataComboItem;
                    if (ex.Tag == gxObject)
                    {
                        this.gisDataComboBox1.SelectedIndex = j;
                        break;
                    }
                }
            }
            else
            {
                ex = this.gisDataComboBox1.Items[this.gisDataComboBox1.SelectedIndex] as GISDataComboItem;
                int degree = ex.Level;
                ex = new GISDataComboItem(gxObject.Name, gxObject.FullName, this.GetImageIndex(gxObject), degree + 1)
                {
                    Tag = gxObject
                };
                this.gisDataComboBox1.AddChildNode(ex);
                this.gisDataComboBox1.SelectedIndex++;
            }
            this._isFree = true;
            return true;
        }
        private void EndOpen()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.txtName.Text.Trim();
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    IGxObject tag = item.Tag as IGxObject;
                    if ((((tag != null) && !(tag is IGxDataset)) && this.CheckObject(tag)) || (tag is IGxNewDatabase))
                    {
                        return;
                    }
                }
                this.PassListItem();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void cboShowType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboShowType.SelectedIndex == _filterArray.Count)
            {
                this._gxObjectFilter = null;
            }
            else
            {
                this._gxObjectFilter=_filterArray.Element[this.cboShowType.SelectedIndex] as IGxObjectFilter;
            }
            ClearViewer();
            GISDataComboItem item=gisDataComboBox1.Items[gisDataComboBox1.SelectedIndex] as GISDataComboItem;
            IGxObject linkTag=item.Tag as IGxObject;

            LoadViewer(linkTag);
        }

        private void LoadViewer(IGxObject gxObject)
        {
            Cursor = Cursors.WaitCursor;
            string[] items = new string[2];
            ClearViewer();
            if (gxObject is IGxObjectContainer)
            {
                if (gxObject is IGxDatabase)
                {
                    if (!(gxObject as IGxDatabase).IsConnected)
                    {
                        (gxObject as IGxDatabase).Connect();
                    }
                    if (!(gxObject as IGxDatabase).IsConnected)
                    {
                        return;
                    }
                }
                else if (gxObject is IGxAGSConnection)
                {
                    if (!(gxObject as IGxAGSConnection).IsConnected)
                    {
                        (gxObject as IGxAGSConnection).Connect();
                    }
                    if (!(gxObject as IGxAGSConnection).IsConnected)
                    {
                        return;
                    }
                }
                IEnumGxObject children = (gxObject as IGxObjectContainer).Children;
                children.Reset();
                IGxObject subObj = children.Next();
                bool canDisplay = true;
                while (subObj != null)
                {
                    if (subObj is IGxNewDatabase)
                    {
                        canDisplay = true;
                    }
                    else if (_gxObjectFilter != null)
                    {
                        canDisplay = _gxObjectFilter.CanDisplayObject(subObj);
                    }
                    else
                    {
                        for (int i = 0; i < _filterArray.Count; i++)
                        {
                            IGxObjectFilter filter=_filterArray.Element[i] as IGxObjectFilter;
                            if (canDisplay = filter.CanDisplayObject(subObj)) break;
                        }
                    }
                    if (canDisplay)
                    {
                        items[0] = subObj.Name;
                        items[1] = subObj.Category;
                        ListViewItem item=new ListViewItem(items,GetImageIndex(subObj))
                        {
                            Tag = subObj
                        };
                        listView1.Items.Add(item);
                    }
                    subObj = children.Next();
                }
            }
            Cursor = Cursors.Default;
        }

        private string GetShapeTypeString(IFeatureClass fClass)
        {
            switch (fClass.ShapeType)
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

        private string GetShapeTypeString(IFeatureClassName fName)
        {
            switch (fName.ShapeType)
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
        private int GetImageIndex(IGxObject oneObj)
        {
            IFeatureClass fClass;
            int imgIdx = 0;
            string category = oneObj.Category;
            if (oneObj is IGxDataset)
            {
                if ((oneObj as IGxDataset).Type == esriDatasetType.esriDTFeatureClass)
                {
                    IFeatureClassName datasetName= (oneObj as IGxDataset).DatasetName as IFeatureClassName;
                    if (datasetName.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        category = category + " 注记";
                    }
                    else if (datasetName.ShapeType == esriGeometryType.esriGeometryNull)
                    {
                        try
                        {
                            fClass = (datasetName as IName).Open() as IFeatureClass;
                            category = category + this.GetShapeTypeString(fClass);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        category = category + this.GetShapeTypeString(datasetName);
                    }
                }
            }
            else if (oneObj is IGxDatabase)
            {
                if ((oneObj as IGxDatabase).IsRemoteDatabase && (oneObj as IGxDatabase).IsConnected)
                {
                    category = category + " Connect";
                }
            }
            else if (oneObj is IGxLayer)
            {
                ILayer layer = (oneObj as IGxLayer).Layer;
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
                    fClass = (layer as IFeatureLayer).FeatureClass;
                    if (fClass == null)
                    {
                        category = category + " Unknown";
                    }
                    else
                    {
                        category = category + this.GetShapeTypeString(fClass);
                    }
                }
            }
            else if ((oneObj is IGxDiskConnection) && !Directory.Exists((oneObj as IGxFile).Path))
            {
                category = category + "_Error";
            }
            if (this._sortedList.ContainsKey(category))
            {
                return this._sortedList[category];
            }
            this.imageList1.Images.Add((oneObj as IGxObjectUI).SmallImage);
            imgIdx = this.imageList1.Images.Count - 1;
            this._sortedList.Add(category, imgIdx);
            return imgIdx;
        }

        public DialogResult DoModalOpen()
        {
            try
            {
               this._modalType = 0;
                this.listView1.MultiSelect = this._multiSelect;
                this.btnOK.Text = "添加";
                return base.ShowDialog();
            }
            catch (Exception)
            {
            }
            return DialogResult.Cancel;
        }

        public DialogResult DoModalSave()
        {
            this._modalType = 1;
            this.btnOK.Text = "保存";
            this._multiSelect = false;
            this.listView1.MultiSelect = this._multiSelect;
            return base.ShowDialog();
        }

        public DialogResult DoModalSaveLocation()
        {
            this._modalType = 2;
            this.btnOK.Text = "保存";
            this._multiSelect = false;
            this.listView1.MultiSelect = this._multiSelect;
            return base.ShowDialog();
        }

        private void ClearViewer()
        {
            listView1.SelectedItems.Clear();
            listView1.Items.Clear();
        }

        private void frmExplorerData_Load(object sender, EventArgs e)
        {
            if (_gxCatalog == null)
            {
                _gxCatalog=new GxCatalog();
            }
            GISDataComboItem item=new GISDataComboItem((_gxCatalog as IGxObject).Name, (_gxCatalog as IGxObject).Name, this.GetImageIndex(this._gxCatalog as IGxObject), 0)
            {
                Tag=_gxCatalog
            };
            gisDataComboBox1.Items.Add(item);
            IEnumGxObject children = (this._gxCatalog as IGxObjectContainer).Children;
            children.Reset();
            for (IGxObject subObj = children.Next(); subObj != null; subObj = children.Next())
            {
                item = new GISDataComboItem(subObj.Name, subObj.FullName, this.GetImageIndex(subObj), 1)
                {
                    Tag = subObj
                };
                this.gisDataComboBox1.AddChildNode(item);
            }
            gisDataComboBox1.SelectedIndex = 0;
            this._array2.RemoveAll();
            this._gxObjects.Clear();
            this.cboShowType.Items.Clear();
            for (int i = 0; i < this._filterArray.Count; i++)
            {
                IGxObjectFilter filter = this._filterArray.Element[i] as IGxObjectFilter;
                this.cboShowType.Items.Add(filter.Description);
                if (filter == this._gxObjectFilter)
                {
                    this.cboShowType.SelectedIndex = i;
                }
            }
            if ((this._modalType == 0) && (this._filterArray.Count > 1))
            {
                this.cboShowType.Items.Add("已列出的所有过滤条件");
            }
            if (this.cboShowType.SelectedIndex == -1)
            {
                this.cboShowType.SelectedIndex = this.cboShowType.Items.Count - 1;
            }
            if ((_pStartLocation != null) && (_pStartLocation is string))
            {
                this.LoadViewer(_pStartLocation as string);
            }
        }

        private void LoadViewer(string location)
        {
            string[] strArray = location.Split(new char[] {'\\'});
            IEnumGxObject chirdren = (_gxCatalog as IGxObjectContainer).Children;
            chirdren.Reset();
            IGxObject subObj = null;
            if ((strArray[0] == "数据库连接") || (strArray[0] == "Database Connections"))
            {
                for (subObj = chirdren.Next(); subObj != null; subObj = chirdren.Next())
                {
                    if (subObj is IGxRemoteDatabaseFolder) break;
                }
            }
            else
            {
                subObj = chirdren.Next();
                strArray[0] = strArray[0] + @"\";
                while (subObj != null)
                {
                    if (string.Compare(subObj.Name, strArray[0], true) == 0)
                    {
                        break;
                    }
                    subObj = chirdren.Next();
                }
            }
            if (subObj != null)
            {
                this.LoadGISDataCombo(subObj);
                MyDoubleClickResult myDCRNothing = MyDoubleClickResult.myDCRNothing;
                if (this._gxObjectFilter != null)
                {
                    this._gxObjectFilter.CanChooseObject(subObj, ref myDCRNothing);
                }
                if (myDCRNothing != MyDoubleClickResult.myDCRChooseAndDismiss)
                {
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        if (strArray[i] != "")
                        {
                            if (!(subObj is IGxObjectContainer))
                            {
                                break;
                            }
                            chirdren = (subObj as IGxObjectContainer).Children;
                            chirdren.Reset();
                            subObj = chirdren.Next();
                            while (subObj != null)
                            {
                                if (string.Compare(subObj.Name, strArray[i], true) == 0)
                                {
                                    break;
                                }
                                subObj = chirdren.Next();
                            }
                            if (subObj == null)
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
                            if (this._gxObjectFilter != null)
                            {
                                this._gxObjectFilter.CanChooseObject(subObj, ref myDCRNothing);
                            }
                            if (myDCRNothing == MyDoubleClickResult.myDCRChooseAndDismiss)
                            {
                                break;
                            }
                            this.LoadGISDataCombo(subObj);
                        }
                    }
                }
            }
        }

        private bool _isFree;
        private void LoadGISDataCombo(IGxObject gxObject)
        {
            GISDataComboItem ex;
            this.LoadViewer(gxObject);
            this._isFree = false;
            if (gxObject.Parent is IGxCatalog)
            {
                for (int i = 0; i < this.gisDataComboBox1.Items.Count; i++)
                {
                    ex = this.gisDataComboBox1.Items[i] as GISDataComboItem;
                    if (ex.Tag == gxObject)
                    {
                        this.gisDataComboBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                ex = this.gisDataComboBox1.Items[this.gisDataComboBox1.SelectedIndex] as GISDataComboItem;
                int degree = ex.Level;
                ex = new GISDataComboItem(gxObject.Name, gxObject.FullName, this.GetImageIndex(gxObject), degree + 1)
                {
                    Tag = gxObject
                };
                this.gisDataComboBox1.AddChildNode( ex);
                this.gisDataComboBox1.SelectedIndex++;
            }
            this._isFree = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem current;
            string text = "";
            if ((this._modalType == 0) || ((this._gxObjectFilter != null) && (this._gxObjectFilter.Name == "GxFilterWorkspaces")))
            {
                IEnumerator listItems = this.listView1.SelectedItems.GetEnumerator();
                object item = listItems.MoveNext();
                    while( item!=null)
                    {
                        current = (ListViewItem)listItems.Current;
                        if (text.Length > 0)
                        {
                            text = text + "; " + current.Text;
                        }
                        else
                        {
                            text = current.Text;
                        }
                    item = listItems.MoveNext();
                    this.txtName.Text = text;
                }
                
                
            }
            if (((this._modalType == 2) && (this._gxObjectFilter != null)) && (this.listView1.SelectedItems.Count > 0))
            {
                current = this.listView1.SelectedItems[0];
                text = current.Text;
            }
      
        }

        private bool IsParent(IGxObject gxObject1, IGxObject gxObject2)
        {
            for (IGxObject obj2 = gxObject2.Parent; obj2 != null; obj2 = obj2.Parent)
            {
                if (gxObject1 == obj2)
                {
                    return true;
                }
            }
            return false;
        }

        private void LoadByCombo()
        {
            GISDataComboItem ex = this.gisDataComboBox1.Items[this.gisDataComboBox1.SelectedIndex] as GISDataComboItem;
            IGxObject tag = ex.Tag as IGxObject;
            for (int i = this.gisDataComboBox1.Items.Count - 1; i > 0; i--)
            {
                ex = this.gisDataComboBox1.Items[i] as GISDataComboItem;
                IGxObject obj3 = ex.Tag as IGxObject;
                if ((!(obj3 is IGxCatalog) && !(obj3.Parent is IGxCatalog)) && ((obj3 != tag) && !this.IsParent(obj3, tag)))
                {
                    this.gisDataComboBox1.Items.RemoveAt(i);
                }
            }
        }

        public bool AllowMultiSelect
        {
            set
            {
                this._multiSelect = value;
            }
        }
        public IArray Items
        {
            get
            {
                return this._array2;
            }
        }

        public IGxObjectFilter ObjectFilter
        {
            get
            {
                return this._gxObjectFilter;
            }
            set
            {
                for (int i = 0; i < this._filterArray.Count; i++)
                {
                    IGxObjectFilter filter = this._filterArray.Element[i] as IGxObjectFilter;
                    if (filter.Name == value.Name)
                    {
                        this._gxObjectFilter = filter;
                        return;
                    }
                }
                this._filterArray.Add(value);
                this._gxObjectFilter = value;
            }
        }

        public bool ReplacingObject
        {
            get
            {
                return false;
            }
        }

        public IGxObject FinalLocation
        {
            get
            {
                return this._gxObject;
            }
        }

        public IGxCatalog GxCatalog
        {
            set
            {
                this._gxCatalog = value;
            }
        }
        public string ButtonCaption
        {
            set
            {
                this.btnOK.Text = value;
            }
        }

       

        public void RemoveAllFilters()
        {
            this._filterArray.RemoveAll();
        }
        private string ChangeODCExtension(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").odc";
            }
            return string_2;
        }

        private  string _pStartFilePath = "";
        private void PassListItem()
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                IGxObject tag = item.Tag as IGxObject;
                if (tag != null)
                {
                    this._array2.Add(tag);
                    this._gxObjects.Add(tag);
                }
            }
            base.DialogResult = DialogResult.OK;
            GISDataComboItem selectedItem = this.gisDataComboBox1.SelectedItem as GISDataComboItem;
            this._gxObject = selectedItem.Tag as IGxObject;
            _pStartLocation = this._gxObject.FullName;
            if (_pStartLocation != null)
            {
                StreamWriter writer = null;
                try
                {
                    writer = File.CreateText(this._pStartFilePath);
                    writer.WriteLine(_pStartLocation.ToString());
                    writer.Close();
                }
                catch (Exception)
                {
                }
            }
            base.Close();
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                MyDoubleClickResult myDCRShowChildren;
                IGxObject tag = this.listView1.SelectedItems[0].Tag as IGxObject;
                if ((this._gxObjectFilter != null) && (tag is IGxDatabase))
                {
                    myDCRShowChildren = MyDoubleClickResult.myDCRShowChildren;
                    this._gxObjectFilter.CanChooseObject(tag, ref myDCRShowChildren);
                    if (myDCRShowChildren == MyDoubleClickResult.myDCRChooseAndDismiss)
                    {
                        this.PassListItem();
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
                                    str2 = this.ChangeODCExtension(str2);
                                    IWorkspaceFactory factory = new OLEDBWorkspaceFactory() as IWorkspaceFactory;
                                    name = factory.Create(path, System.IO.Path.GetFileName(str2), null, 0);
                                    IGxObject gxDatabase = new GxDatabase() as IGxObject;
                                    (gxDatabase as IGxDatabase).WorkspaceName = name;
                                    gxDatabase.Attach(tag.Parent, this._gxCatalog);
                                    item = new ListViewItem(new string[] { gxDatabase.Name, gxDatabase.Category }, this.GetImageIndex(gxDatabase))
                                    {
                                        Tag = gxDatabase
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
                            //frmCreateGDBConnection connection = new frmCreateGDBConnection
                            //{
                            //    TopMost = true
                            //};
                            //if (connection.ShowDialog() == DialogResult.OK)
                            //{
                            //    IGxObject oneObj= new GxDatabase() as IGxObject;
                            //    IWorkspaceName name2 = new WorkspaceName() as IWorkspaceName;
                            //    name2.WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                            //    name2.PathName = connection.ConnectionPath;
                          
                            //    (oneObj as IGxDatabase).WorkspaceName = name2;
                            //    oneObj.Attach(tag.Parent, this._gxCatalog);
                            //    item = new ListViewItem(new string[] { oneObj.Name, oneObj.Category }, this.GetImageIndex(oneObj))
                            //    {
                            //        Tag = oneObj
                            //    };
                            //    this.listView1.Items.Add(item);
                            //}
                        }
                    }
                    else
                    {
                        this.PassListItem();
                    }
                }
                else
                {
                    GISDataComboItem ex;
                    if (tag is IGxDataset)
                    {
                        esriDatasetType type = (tag as IGxDataset).Type;
                        myDCRShowChildren = MyDoubleClickResult.myDCRShowChildren;
                        this._gxObjectFilter.CanChooseObject(tag, ref myDCRShowChildren);
                        if ((((type != esriDatasetType.esriDTFeatureDataset) && (type != esriDatasetType.esriDTContainer)) && ((type != esriDatasetType.esriDTRasterCatalog) && (type != esriDatasetType.esriDTCadDrawing))) && (type != esriDatasetType.esriDTRasterDataset))
                        {
                            this.PassListItem();
                            return;
                        }
                    }
                    this.LoadViewer(tag);
                    this._isFree = false;
                    if (tag.Parent is IGxCatalog)
                    {
                        for (int i = 0; i < this.gisDataComboBox1.Items.Count; i++)
                        {
                            ex = this.gisDataComboBox1.Items[i] as GISDataComboItem;
                            if (ex.Tag == tag)
                            {
                                this.gisDataComboBox1.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ex = this.gisDataComboBox1.Items[this.gisDataComboBox1.SelectedIndex] as GISDataComboItem;
                        int degree = ex.Level;
                        ex = new GISDataComboItem(tag.Name, tag.FullName, this.GetImageIndex(tag), degree + 1)
                        {
                            Tag = tag
                        };
                        int selectedIndex = this.gisDataComboBox1.SelectedIndex;
                        this.gisDataComboBox1.AddChildNode( ex);
                        this.gisDataComboBox1.SelectedIndex = selectedIndex + 1;
                    }
                    this._isFree = true;
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

        private void gisDataComboBox1_SelectedItemChanged(GISDataComboItem item)
        {
            if (this._isFree)
            {
                Cursor = Cursors.WaitCursor;
                GISDataComboItem ex = this.gisDataComboBox1.Items[this.gisDataComboBox1.SelectedIndex] as GISDataComboItem;
                IGxObject tag = ex.Tag as IGxObject;
                this.LoadViewer(tag);
                this.LoadByCombo();
                Cursor = Cursors.Default;
            }
        }
    }
 
}
