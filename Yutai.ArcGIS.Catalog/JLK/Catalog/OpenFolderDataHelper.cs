namespace JLK.Catalog
{
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    internal sealed class OpenFolderDataHelper : Control
    {
        private IArray iarray_0 = new ArrayClass();
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0;
        private IGxObjectArray igxObjectArray_0;
        public IList<GxObjectStruct> m_DirectoryList = new List<GxObjectStruct>();
        public IList<GxObjectStruct> m_FileList = new List<GxObjectStruct>();
        private string string_0;

        internal event OnReadCompletedHander OnReadCompleted;

        public OpenFolderDataHelper(IGxObjectArray igxObjectArray_1, string string_1, IGxCatalog igxCatalog_1, IGxObject igxObject_1)
        {
            this.CreateHandle();
            base.CreateControl();
            this.igxObjectArray_0 = igxObjectArray_1;
            this.string_0 = string_1;
            this.igxCatalog_0 = igxCatalog_1;
            this.igxObject_0 = igxObject_1;
        }

        public void InvokeMethod(object object_0)
        {
            this.m_FileList.Clear();
            this.m_DirectoryList.Clear();
            JLK.Catalog.State state = (JLK.Catalog.State) object_0;
            if (!base.IsDisposed && base.IsHandleCreated)
            {
                try
                {
                    this.string_0 = (this.igxObject_0 as IGxFile).Path;
                    if (this.string_0 == null)
                    {
                        if (this.onReadCompletedHander_0 != null)
                        {
                            this.onReadCompletedHander_0();
                        }
                        state.Set();
                    }
                    else
                    {
                        GxObjectStruct struct2;
                        foreach (string str in Directory.GetDirectories(this.string_0))
                        {
                            struct2 = new GxObjectStruct {
                                Type = this.method_1(str),
                                Path = str
                            };
                            if (struct2.Type == "FOLDER")
                            {
                                this.m_DirectoryList.Add(struct2);
                            }
                            else
                            {
                                this.m_FileList.Add(struct2);
                            }
                        }
                        foreach (string str2 in Directory.GetFiles(this.string_0))
                        {
                            string str3 = Path.GetExtension(str2).ToLower();
                            if (this.method_2(str2, str3))
                            {
                                struct2 = new GxObjectStruct {
                                    Type = str3,
                                    Path = str2
                                };
                                this.m_FileList.Add(struct2);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public void InvokeMethod1()
        {
            if (!(base.IsDisposed || !base.IsHandleCreated))
            {
                base.Invoke(new MessageHandler(this.OpenFolder));
            }
        }

        public void InvokeMethod2(object object_0)
        {
            JLK.Catalog.State state = (JLK.Catalog.State) object_0;
            if (!base.IsDisposed && base.IsHandleCreated)
            {
                try
                {
                    this.string_0 = (this.igxObject_0 as IGxFile).Path;
                    if (this.string_0 == null)
                    {
                        if (this.onReadCompletedHander_0 != null)
                        {
                            this.onReadCompletedHander_0();
                        }
                        state.Set();
                        return;
                    }
                    string[] directories = Directory.GetDirectories(this.string_0);
                    state.Set();
                    base.Invoke(new MessageHandler1(this.method_3), new object[] { directories });
                    state.Reset();
                    state.Set();
                    base.Invoke(new MessageHandler(this.method_4));
                    state.Reset();
                    string[] files = Directory.GetFiles(this.string_0);
                    state.Set();
                    foreach (string str in files)
                    {
                        base.Invoke(new MessageHandler2(this.method_5), new object[] { str });
                    }
                }
                catch
                {
                }
                if (this.onReadCompletedHander_0 != null)
                {
                    this.onReadCompletedHander_0();
                }
            }
            else
            {
                state.Set();
                if (this.onReadCompletedHander_0 != null)
                {
                    this.onReadCompletedHander_0();
                }
            }
        }

        private bool method_0(string string_1, string string_2, bool bool_0)
        {
            IGxObject obj2 = null;
            IDatasetName name;
            IWorkspaceName name2;
            switch (string_2.ToLower())
            {
                case ".bmp":
                case ".png":
                case ".jpg":
                case ".tif":
                case ".img":
                case ".sid":
                    obj2 = new GxRasterDataset();
                    name = new RasterDatasetNameClass();
                    name2 = new WorkspaceNameClass {
                        WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                        PathName = Path.GetDirectoryName(string_1)
                    };
                    name.Name = Path.GetFileName(string_1);
                    name.WorkspaceName = name2;
                    (obj2 as IGxDataset).DatasetName = name;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
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
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
                    {
                        this.igxCatalog_0.ObjectAdded(obj2);
                    }
                    return true;

                case ".shp":
                    obj2 = new GxShapefileDataset();
                    name = new FeatureClassNameClass();
                    name2 = new WorkspaceNameClass {
                        WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                        PathName = Path.GetDirectoryName(string_1)
                    };
                    name.Name = Path.GetFileName(string_1);
                    name.WorkspaceName = name2;
                    (obj2 as IGxDataset).DatasetName = name;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
                    {
                        this.igxCatalog_0.ObjectAdded(obj2);
                    }
                    return true;

                case ".dbf":
                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(string_1), Path.GetFileNameWithoutExtension(string_1) + ".shp")))
                    {
                        obj2 = new GxDataset();
                        name = new TableNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = Path.GetDirectoryName(string_1)
                        };
                        name.Name = Path.GetFileName(string_1);
                        name.WorkspaceName = name2;
                        (obj2 as IGxDataset).DatasetName = name;
                        obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                        if (bool_0)
                        {
                            this.igxCatalog_0.ObjectAdded(obj2);
                        }
                        return true;
                    }
                    return false;

                case ".sde":
                    obj2 = new GxDatabase();
                    name2 = new WorkspaceNameClass {
                        WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory",
                        PathName = string_1
                    };
                    (obj2 as IGxDatabase).WorkspaceName = name2;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
                    {
                        this.igxCatalog_0.ObjectAdded(obj2);
                    }
                    return true;

                case ".lyr":
                    obj2 = new GxLayer();
                    (obj2 as IGxFile).Path = string_1;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
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
                        PathName = Path.GetDirectoryName(string_1)
                    };
                    name.Name = Path.GetFileName(string_1);
                    name.WorkspaceName = name2;
                    (obj2 as IGxDataset).DatasetName = name;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
                    {
                        this.igxCatalog_0.ObjectAdded(obj2);
                    }
                    return true;

                case ".doc":
                case ".xls":
                    obj2 = new GxOfficeFile();
                    (obj2 as IGxFile).Path = string_1;
                    obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                    if (bool_0)
                    {
                        this.igxCatalog_0.ObjectAdded(obj2);
                    }
                    return true;
            }
            return false;
        }

        private string method_1(string string_1)
        {
            string path = string_1;
            if (Path.GetFileName(path).ToLower() == "info")
            {
                return "INFO";
            }
            string str3 = path;
            if (path[path.Length - 1] == '\\')
            {
                str3 = path.Substring(0, path.Length - 1);
            }
            if ((Path.GetExtension(str3).ToLower() == ".gdb") && File.Exists(Path.Combine(path, "gdb")))
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
                string str6 = Path.GetDirectoryName(string_1) + @"\info";
                if (Directory.Exists(str6) && File.Exists(str6 + @"\arc.dir"))
                {
                    return "COVERAGE";
                }
            }
            return "FOLDER";
        }

        private bool method_2(string string_1, string string_2)
        {
            switch (string_2)
            {
                case ".bmp":
                case ".jpg":
                case ".tif":
                case ".img":
                case ".sid":
                    return true;

                case ".mdb":
                    return true;

                case ".shp":
                    return true;

                case ".dbf":
                    return !File.Exists(Path.Combine(Path.GetDirectoryName(string_1), Path.GetFileNameWithoutExtension(string_1) + ".shp"));

                case ".sde":
                    return true;

                case ".lyr":
                    return true;

                case ".dwg":
                case ".dxf":
                    return true;

                case ".doc":
                case ".xls":
                    return true;
            }
            return false;
        }

        private void method_3(string[] string_1)
        {
            foreach (string str in string_1)
            {
                IDatasetName name;
                IWorkspaceName name2;
                IGxObject unk = null;
                string str2 = this.method_1(str);
                switch (str2)
                {
                    case null:
                        break;

                    case "FOLDER":
                        unk = new GxFolder();
                        (unk as IGxFile).Path = str;
                        unk.Attach(this.igxObject_0, this.igxCatalog_0);
                        break;

                    case "TIN":
                        unk = new GxDataset();
                        name = new TinNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory",
                            PathName = Path.GetDirectoryName(str)
                        };
                        name.Name = Path.GetFileName(str);
                        name.WorkspaceName = name2;
                        (unk as IGxDataset).DatasetName = name;
                        this.iarray_0.Add(unk);
                        break;

                    case "GRID":
                        unk = new GxRasterDataset();
                        name = new RasterDatasetNameClass();
                        name2 = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                            PathName = Path.GetDirectoryName(str)
                        };
                        name.Name = Path.GetFileName(str);
                        name.WorkspaceName = name2;
                        (unk as IGxDataset).DatasetName = name;
                        this.iarray_0.Add(unk);
                        break;

                    default:
                        if (!(str2 == "COVERAGE"))
                        {
                            if (str2 == "FILEGDB")
                            {
                                unk = new GxDatabase();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1",
                                    PathName = str
                                };
                                (unk as IGxDatabase).WorkspaceName = name2;
                                this.iarray_0.Add(unk);
                            }
                        }
                        else
                        {
                            unk = new GxCoverageDataset();
                            name = new CoverageNameClass();
                            name2 = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1",
                                PathName = Path.GetDirectoryName(str)
                            };
                            name.Name = Path.GetFileName(str);
                            name.WorkspaceName = name2;
                            (unk as IGxDataset).DatasetName = name;
                            this.iarray_0.Add(unk);
                        }
                        break;
                }
            }
        }

        private void method_4()
        {
            for (int i = 0; i < this.iarray_0.Count; i++)
            {
                (this.iarray_0.get_Element(i) as IGxObject).Attach(this.igxObject_0, this.igxCatalog_0);
            }
            this.iarray_0.RemoveAll();
        }

        private void method_5(string string_1)
        {
            string str = Path.GetExtension(string_1).ToLower();
            if (str != null)
            {
                this.method_0(string_1, str, false);
            }
        }

        private void method_6()
        {
        }

        public void Open()
        {
        }

        public void OpenFolder()
        {
            try
            {
                try
                {
                    this.string_0 = (this.igxObject_0 as IGxFile).Path;
                    if (this.string_0 == null)
                    {
                        if (this.onReadCompletedHander_0 != null)
                        {
                            this.onReadCompletedHander_0();
                        }
                        return;
                    }
                    string[] directories = Directory.GetDirectories(this.string_0);
                    IArray array = new ArrayClass();
                    foreach (string str in directories)
                    {
                        IGxObject obj2;
                        IDatasetName name;
                        IWorkspaceName name2;
                        string str2 = this.method_1(str);
                        switch (str2)
                        {
                            case null:
                                break;

                            case "FOLDER":
                                obj2 = new GxFolder();
                                (obj2 as IGxFile).Path = str;
                                obj2.Attach(this.igxObject_0, this.igxCatalog_0);
                                break;

                            case "TIN":
                                obj2 = new GxDataset();
                                name = new TinNameClass();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory",
                                    PathName = Path.GetDirectoryName(str)
                                };
                                name.Name = Path.GetFileName(str);
                                name.WorkspaceName = name2;
                                (obj2 as IGxDataset).DatasetName = name;
                                array.Add(obj2);
                                break;

                            case "GRID":
                                obj2 = new GxRasterDataset();
                                name = new RasterDatasetNameClass();
                                name2 = new WorkspaceNameClass {
                                    WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory",
                                    PathName = Path.GetDirectoryName(str)
                                };
                                name.Name = Path.GetFileName(str);
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
                                        PathName = Path.GetDirectoryName(str)
                                    };
                                    name.Name = Path.GetFileName(str);
                                    name.WorkspaceName = name2;
                                    (obj2 as IGxDataset).DatasetName = name;
                                    array.Add(obj2);
                                }
                                break;
                        }
                    }
                    for (int i = 0; i < array.Count; i++)
                    {
                        (array.get_Element(i) as IGxObject).Attach(this.igxObject_0, this.igxCatalog_0);
                    }
                    foreach (string str3 in Directory.GetFiles(this.string_0))
                    {
                        string str4 = Path.GetExtension(str3).ToLower();
                        if (str4 != null)
                        {
                            this.method_0(str3, str4, false);
                        }
                    }
                }
                catch
                {
                }
            }
            finally
            {
            }
            if (this.onReadCompletedHander_0 != null)
            {
                this.onReadCompletedHander_0();
            }
        }

        public delegate void MessageHandler();

        public delegate void MessageHandler1(string[] string_0);

        public delegate void MessageHandler2(string string_0);
    }
}

