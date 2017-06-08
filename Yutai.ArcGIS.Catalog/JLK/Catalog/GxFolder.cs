namespace JLK.Catalog
{
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Framework;
    using JLK.Utility;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class GxFolder : IGxObject, IGxFolder, IGxObjectContainer, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxFolderAdmin
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private OpenFolderDataHelper openFolderDataHelper_0 = null;
        private string string_0 = "";

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return igxObject_1;
        }

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            this.igxCatalog_0 = igxCatalog_1;
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).AddChild(this);
            }
        }

        public bool CanCopy()
        {
            return true;
        }

        public bool CanDelete()
        {
            return true;
        }

        public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
        {
            ienumName_0.Reset();
            IName name = ienumName_0.Next();
            bool flag = false;
            while (name != null)
            {
                if (name is IFileName)
                {
                    flag = true;
                }
                else if (name is IDatasetName)
                {
                    flag = (name as IDatasetName).WorkspaceName.Type == esriWorkspaceType.esriFileSystemWorkspace;
                }
                else
                {
                    if (!(name is IWorkspaceName))
                    {
                        return false;
                    }
                    flag = (name as IWorkspaceName).Type == esriWorkspaceType.esriLocalDatabaseWorkspace;
                }
                if (!flag)
                {
                    return false;
                }
                name = ienumName_0.Next();
            }
            bool_2 = true;
            return true;
        }

        public bool CanRename()
        {
            return true;
        }

        public void Close(bool bool_2)
        {
        }

        public void Delete()
        {
            try
            {
                Directory.Delete(this.string_0, true);
                this.Detach();
                this.igxObjectArray_0.Empty();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void DeleteChild(IGxObject igxObject_1)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectDeleted(this);
            }
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
            }
            this.igxObject_0 = null;
            this.igxCatalog_0 = null;
        }

        public void Edit()
        {
        }

        public void EditProperties(int int_0)
        {
        }

        public IGxObject FindChildFolder(string string_1)
        {
            return null;
        }

        public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
        {
        }

        public object GetProperty(string string_1)
        {
            return null;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("CopyItem", false);
            this.ipopuMenuWrap_0.AddItem("PasteItem", false);
            this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
            this.ipopuMenuWrap_0.AddItem("ReName", false);
            this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
            this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
            this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
            this.ipopuMenuWrap_0.AddItem("NewFolder", "NewBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("NewPersonGDB", "NewBarSubItem", true);
            this.ipopuMenuWrap_0.AddItem("NewFileGDB", "NewBarSubItem", false);
            this.ipopuMenuWrap_0.AddItem("NewShapefile", "NewBarSubItem", false);
        }

        private bool method_0(string string_1, bool bool_2)
        {
            string str = System.IO.Path.GetExtension(string_1).ToLower();
            if (str != null)
            {
                IDatasetName name;
                IWorkspaceName name2;
                IGxObject obj2 = null;
                switch (str)
                {
                    case ".bmp":
                    case ".jpg":
                    case ".tif":
                    case ".img":
                    case ".png":
                    case ".sid":
                        obj2 = new GxRasterDataset();
                        name = new RasterDatasetNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                            PathName = System.IO.Path.GetDirectoryName(string_1)
                        };
                        name.Name = System.IO.Path.GetFileName(string_1);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".vct":
                        obj2 = new GxVCTObject();
                        (obj2 as IGxFile).Path = string_1;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".mdb":
                        obj2 = new GxDatabase();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory",
                            PathName = string_1
                        };
                        (obj2 as IGxDatabase).WorkspaceName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".shp":
                        obj2 = new GxShapefileDataset();
                        name = new FeatureClassNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = System.IO.Path.GetDirectoryName(string_1)
                        };
                        name.Name = System.IO.Path.GetFileName(string_1);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".sde":
                        obj2 = new GxDatabase();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                            PathName = string_1
                        };
                        (obj2 as IGxDatabase).WorkspaceName = name2;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".dbf":
                        if (!File.Exists(string_1.Substring(0, string_1.Length - 3) + "shp"))
                        {
                            obj2 = new GxDataset();
                            name = new TableNameClass();
                            name2 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                                PathName = System.IO.Path.GetDirectoryName(string_1)
                            };
                            name.Name = System.IO.Path.GetFileName(string_1);
                            name.WorkspaceName = name2;
                            (obj2 as IGxDataset).DatasetName = name;
                            obj2.Attach(this, this.igxCatalog_0);
                            if (bool_2)
                            {
                                this.igxCatalog_0.ObjectAdded(obj2);
                            }
                            return true;
                        }
                        return false;

                    case ".lyr":
                        obj2 = new GxLayer();
                        (obj2 as IGxFile).Path = string_1;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".dwg":
                    case ".dxf":
                        obj2 = new GxCadDataset();
                        name = new CadDrawingNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory",
                            PathName = System.IO.Path.GetDirectoryName(string_1)
                        };
                        name.Name = System.IO.Path.GetFileName(string_1);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;

                    case ".doc":
                    case ".xls":
                        obj2 = new GxOfficeFile();
                        (obj2 as IGxFile).Path = string_1;
                        obj2.Attach(this, this.igxCatalog_0);
                        if (bool_2)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;
                }
            }
            return false;
        }

        private void method_1()
        {
            Thread thread = new Thread(new ThreadStart(this.method_3));
            thread.Start();
            while (!thread.IsAlive)
            {
            }
        }

        private string method_2(string string_1)
        {
            string path = string_1;
            if (System.IO.Path.GetFileName(path).ToLower() == "info")
            {
                return "INFO";
            }
            string str3 = path;
            if (path[path.Length - 1] == '\\')
            {
                str3 = path.Substring(0, path.Length - 1);
            }
            if ((System.IO.Path.GetExtension(str3).ToLower() == ".gdb") && File.Exists(System.IO.Path.Combine(path, "gdb")))
            {
                return "FILEGDB";
            }
            if (path[path.Length - 1] != '\\')
            {
                path = path + @"\";
            }
            if (File.Exists(path + "hdr.adf"))
            {
                return "GRID";
            }
            if (File.Exists(path + "tdenv.adf"))
            {
                return "TIN";
            }
            if (File.Exists(path + "dbltic.adf"))
            {
                string str6 = System.IO.Path.GetDirectoryName(string_1) + @"\info";
                if (Directory.Exists(str6) && File.Exists(str6 + @"\arc.dir"))
                {
                    return "COVERAGE";
                }
            }
            return "FOLDER";
        }

        private void method_3()
        {
            string[] directories;
            this.bool_0 = false;
            try
            {
                directories = Directory.GetDirectories(this.string_0);
            }
            catch
            {
                return;
            }
            IArray array = new ArrayClass();
            foreach (string str in directories)
            {
                IGxObject obj2;
                IDatasetName name;
                IWorkspaceName name2;
                string str2 = this.method_2(str);
                switch (str2)
                {
                    case null:
                        break;

                    case "FOLDER":
                        obj2 = new GxFolder();
                        (obj2 as IGxFile).Path = str;
                        obj2.Attach(this, this.igxCatalog_0);
                        break;

                    case "TIN":
                        obj2 = new GxDataset();
                        name = new TinNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory",
                            PathName = System.IO.Path.GetDirectoryName(str)
                        };
                        name.Name = System.IO.Path.GetFileName(str);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        array.Add(obj2);
                        break;

                    case "GRID":
                        obj2 = new GxRasterDataset();
                        name = new RasterDatasetNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                            PathName = System.IO.Path.GetDirectoryName(str)
                        };
                        name.Name = System.IO.Path.GetFileName(str);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        array.Add(obj2);
                        break;

                    default:
                        if (!(str2 == "COVERAGE"))
                        {
                            if (str2 == "FILEGDB")
                            {
                                obj2 = new GxDatabase();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1",
                                    PathName = str
                                };
                                (obj2 as IGxDatabase).WorkspaceName = name2;
                                array.Add(obj2);
                            }
                        }
                        else
                        {
                            obj2 = new GxCoverageDataset();
                            name = new CoverageNameClass();
                            name2 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1",
                                PathName = System.IO.Path.GetDirectoryName(str)
                            };
                            name.Name = System.IO.Path.GetFileName(str);
                            name.WorkspaceName = name2;
                            (obj2 as IGxDataset).DatasetName = name;
                            array.Add(obj2);
                        }
                        break;
                }
            }
            for (int i = 0; i < array.Count; i++)
            {
                (array.get_Element(i) as IGxObject).Attach(this, this.igxCatalog_0);
            }
            foreach (string str3 in Directory.GetFiles(this.string_0))
            {
                this.method_0(str3, false);
            }
            this.bool_1 = true;
        }

        private void method_4()
        {
            try
            {
                int num;
                IGxObject obj2;
                for (num = 0; num < this.openFolderDataHelper_0.m_DirectoryList.Count; num++)
                {
                    obj2 = new GxFolder();
                    (obj2 as IGxFile).Path = this.openFolderDataHelper_0.m_DirectoryList[num].Path;
                    obj2.Attach(this, this.igxCatalog_0);
                }
                for (num = 0; num < this.openFolderDataHelper_0.m_FileList.Count; num++)
                {
                    IDatasetName name;
                    IWorkspaceName name2;
                    string type = this.openFolderDataHelper_0.m_FileList[num].Type;
                    switch (type)
                    {
                        case null:
                            break;

                        case "TIN":
                        {
                            obj2 = new GxDataset();
                            name = new TinNameClass();
                            name2 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory",
                                PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[num].Path)
                            };
                            name.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[num].Path);
                            name.WorkspaceName = name2;
                            (obj2 as IGxDataset).DatasetName = name;
                            obj2.Attach(this, this.igxCatalog_0);
                            continue;
                        }
                        case "GRID":
                        {
                            obj2 = new GxRasterDataset();
                            name = new RasterDatasetNameClass();
                            name2 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                                PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[num].Path)
                            };
                            name.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[num].Path);
                            name.WorkspaceName = name2;
                            (obj2 as IGxDataset).DatasetName = name;
                            obj2.Attach(this, this.igxCatalog_0);
                            continue;
                        }
                        default:
                        {
                            if (!(type == "COVERAGE"))
                            {
                                if (!(type == "FILEGDB"))
                                {
                                    break;
                                }
                                obj2 = new GxDatabase();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1",
                                    PathName = this.openFolderDataHelper_0.m_FileList[num].Path
                                };
                                (obj2 as IGxDatabase).WorkspaceName = name2;
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                            else
                            {
                                obj2 = new GxCoverageDataset();
                                name = new CoverageNameClass();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1",
                                    PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[num].Path)
                                };
                                name.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[num].Path);
                                name.WorkspaceName = name2;
                                (obj2 as IGxDataset).DatasetName = name;
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                            continue;
                        }
                    }
                    this.method_0(this.openFolderDataHelper_0.m_FileList[num].Path, false);
                }
            }
            catch
            {
            }
        }

        private bool method_5(IDatasetName idatasetName_0, bool bool_2)
        {
            string extension = System.IO.Path.GetExtension(idatasetName_0.Name);
            string str2 = idatasetName_0.WorkspaceName.PathName + @"\" + System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name);
            string str3 = this.string_0 + @"\" + System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name);
            if (File.Exists(str3 + extension))
            {
                int num = 1;
                str3 = str3 + "复件";
                while (File.Exists(str3 + extension))
                {
                    str3 = str3 + num.ToString();
                    num++;
                }
            }
            if (bool_2)
            {
                File.Move(str2 + extension, str3 + extension);
            }
            else
            {
                File.Copy(str2 + extension, str3 + extension);
            }
            if (extension == ".shp")
            {
                this.method_9(str2 + ".dbf", str3 + ".dbf", bool_2);
                this.method_9(str2 + ".prj", str3 + ".prj", bool_2);
                this.method_9(str2 + ".sbn", str3 + ".sbn", bool_2);
                this.method_9(str2 + ".sbx", str3 + ".sbx", bool_2);
                this.method_9(str2 + ".shx", str3 + ".shx", bool_2);
                this.method_9(str2 + ".shp.xml", str3 + ".shp.xml", bool_2);
            }
            this.method_0(str3 + extension, true);
            return true;
        }

        private bool method_6(IDatasetName idatasetName_0, bool bool_2)
        {
            try
            {
                IDataset dataset = (idatasetName_0 as IName).Open() as IDataset;
                if (dataset != null)
                {
                    IWorkspaceName name = new WorkspaceNameClass {
                        PathName = this.string_0,
                        WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory"
                    };
                    IWorkspace copyWorkspace = (name as IName).Open() as IWorkspace;
                    dataset.Copy(idatasetName_0.Name, copyWorkspace);
                    this.method_0((this.string_0 + @"\" + System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name)) + System.IO.Path.GetExtension(idatasetName_0.Name), true);
                    return true;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return false;
        }

        private bool method_7(IWorkspaceName iworkspaceName_0, bool bool_2)
        {
            string pathName = iworkspaceName_0.PathName;
            string extension = System.IO.Path.GetExtension(pathName);
            string str3 = this.string_0 + @"\" + System.IO.Path.GetFileNameWithoutExtension(pathName);
            if (pathName.ToLower() == str3.ToLower())
            {
                MessageBox.Show("无法复制 " + System.IO.Path.GetFileName(pathName) + " 源文件和目标文件相同!");
                return false;
            }
            if (File.Exists(str3 + extension))
            {
                int num = 1;
                str3 = str3 + "复件";
                while (File.Exists(str3 + extension))
                {
                    str3 = str3 + num.ToString();
                    num++;
                }
            }
            if (bool_2)
            {
                File.Move(pathName, str3 + extension);
            }
            else
            {
                File.Copy(pathName, str3 + extension);
            }
            this.method_0(str3 + extension, true);
            return true;
        }

        private bool method_8(IFileName ifileName_0, bool bool_2)
        {
            int num;
            string path = ifileName_0.Path;
            if (Directory.Exists(path))
            {
                num = path.LastIndexOf(@"\");
                path = path.Substring(num + 1);
                path = this.string_0 + @"\" + path;
                if (bool_2)
                {
                    Directory.Move(ifileName_0.Path, path);
                }
                IGxObject obj2 = new GxFolder();
                (obj2 as IGxFile).Path = path;
                obj2.Attach(this, this.igxCatalog_0);
                return true;
            }
            num = path.LastIndexOf(@"\");
            path = path.Substring(num + 1);
            string str2 = System.IO.Path.GetExtension(ifileName_0.Path).ToLower();
            path = this.string_0 + @"\" + path;
            if (path.ToLower() == ifileName_0.Path.ToLower())
            {
                MessageBox.Show("无法复制 " + System.IO.Path.GetFileName(path) + " 源文件和目标文件相同!");
                return false;
            }
            if (File.Exists(path))
            {
                string str3 = path.Substring(0, path.Length - str2.Length);
                path = str3 + "复件" + str2;
                for (int i = 1; File.Exists(path); i++)
                {
                    path = str3 + "复件" + i.ToString() + str2;
                }
            }
            if (bool_2)
            {
                File.Move(ifileName_0.Path, path);
            }
            else
            {
                File.Copy(ifileName_0.Path, path);
            }
            this.method_0(path, true);
            return true;
        }

        private void method_9(string string_1, string string_2, bool bool_2)
        {
            if (File.Exists(string_1))
            {
                if (bool_2)
                {
                    File.Move(string_1, string_2);
                }
                else
                {
                    File.Copy(string_1, string_2, true);
                }
            }
        }

        public void New()
        {
        }

        public void Open()
        {
            this.bool_1 = false;
            if (this.openFolderDataHelper_0 == null)
            {
                this.openFolderDataHelper_0 = new OpenFolderDataHelper(this.igxObjectArray_0, this.string_0, this.igxCatalog_0, this);
            }
            ManualResetEvent[] waitHandles = new ManualResetEvent[] { new ManualResetEvent(false) };
            JLK.Catalog.State state = new JLK.Catalog.State(waitHandles[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.Open1), state);
            WaitHandle.WaitAll(waitHandles);
        }

        public void Open1(object object_0)
        {
            this.openFolderDataHelper_0.InvokeMethod(object_0);
        }

        public void OpenFolder1(object object_0)
        {
            this.method_3();
        }

        public bool Paste(IEnumName ienumName_0, ref bool bool_2)
        {
            try
            {
                ienumName_0.Reset();
                IName name = ienumName_0.Next();
                if (this.igxObjectArray_0.Count == 0)
                {
                    this.Open();
                }
                while (name != null)
                {
                    if (name is IFileName)
                    {
                        this.method_8(name as IFileName, bool_2);
                    }
                    else if (name is IDatasetName)
                    {
                        if ((name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            this.method_5(name as IDatasetName, bool_2);
                        }
                    }
                    else if (name is IWorkspaceName)
                    {
                        this.method_7(name as IWorkspaceName, bool_2);
                    }
                    name = ienumName_0.Next();
                }
                return true;
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
                return false;
            }
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.method_3();
            this.igxCatalog_0.ObjectRefreshed(this);
        }

        public void Rename(string string_1)
        {
            try
            {
                if (string_1 != null)
                {
                    if (string_1.Trim().Length == 0)
                    {
                        MessageBox.Show("必须键入目录名!");
                        this.igxCatalog_0.ObjectChanged(this);
                    }
                    else
                    {
                        string destDirName = System.IO.Path.GetDirectoryName(this.string_0) + @"\" + string_1;
                        if (destDirName.ToLower() != this.string_0.ToLower())
                        {
                            Directory.Move(this.string_0, destDirName);
                            this.string_0 = destDirName;
                            this.igxCatalog_0.ObjectChanged(this);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void Save()
        {
        }

        public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
        {
        }

        public void SetProperty(string string_1, object object_0)
        {
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public bool AreChildrenViewable
        {
            get
            {
                return (this.igxObjectArray_0.Count > 0);
            }
        }

        public string BaseName
        {
            get
            {
                return System.IO.Path.GetFileName(this.string_0);
            }
        }

        public string Category
        {
            get
            {
                return "文件夹";
            }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    this.method_3();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
        }

        public UID ClassID
        {
            get
            {
                return null;
            }
        }

        public UID ContextMenu
        {
            get
            {
                return null;
            }
        }

        public IEnumName FileSystemWorkspaceNames
        {
            get
            {
                return null;
            }
        }

        public string FullName
        {
            get
            {
                return this.string_0;
            }
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public IName InternalObjectName
        {
            get
            {
                IFileName name = new FileNameClass {
                    Path = this.string_0
                };
                return (name as IName);
            }
        }

        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        IName IGxObjectInternalName.InternalObjectName
        {
            get
            {
                IFileName name = new FileNameClass {
                    Path = this.string_0
                };
                return (name as IName);
            }
            set
            {
                IFileName name = value as IFileName;
                if (name != null)
                {
                    this.string_0 = name.Path;
                }
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(6);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(7);
            }
        }

        public string Name
        {
            get
            {
                if (this.igxObject_0 is IGxCatalog)
                {
                    return this.string_0;
                }
                return System.IO.Path.GetFileName(this.string_0);
            }
        }

        public UID NewMenu
        {
            get
            {
                return null;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return this.igxObject_0;
            }
        }

        public string Path
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

        public int PropertyCount
        {
            get
            {
                return 0;
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                return ImageLib.GetSmallImage(6);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(7);
            }
        }
    }
}

