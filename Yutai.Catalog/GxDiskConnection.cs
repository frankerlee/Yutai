using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Array = ESRI.ArcGIS.esriSystem.Array;

namespace Yutai.Catalog
{
	public class GxDiskConnection : IGxObject, IGxFolder, IGxObjectContainer, IGxDiskConnection, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxFolderAdmin
	{
		private OnReadCompletedHander onReadCompletedHander_0;

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private string string_0;

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private OpenFolderDataHelper openFolderDataHelper_0 = null;

		private bool bool_0 = false;

		private bool bool_1 = false;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public bool AreChildrenViewable
		{
			get
			{
				return this.igxObjectArray_0.Count > 0;
			}
		}

		public string BaseName
		{
			get
			{
				return this.string_0;
			}
		}

		public string Category
		{
			get
			{
				return "文件夹连接";
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				DateTime now = DateTime.Now;
				if (this.igxObjectArray_0.Count == 0)
				{
					this.OpenFolder();
				}
				return this.igxObjectArray_0 as IEnumGxObject;
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

		public bool HasCachedChildren
		{
			get
			{
				return false;
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
			    IFileName pName = new FileName() as IFileName;
                pName.Path = this.string_0;
                return (IName)pName;
				
			}
		}

		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
				IFileName fileNameClass = new FileName() as IFileName;
				(fileNameClass as IName).NameString = string.Concat("file:", this.string_0);
				fileNameClass.Path = this.string_0;
				return fileNameClass as IName;
			}
			set
			{
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!Directory.Exists(this.string_0) ? ImageLib.GetSmallImage(5) : ImageLib.GetSmallImage(1));
				return bitmap;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!Directory.Exists(this.string_0) ? ImageLib.GetSmallImage(5) : ImageLib.GetSmallImage(1));
				return bitmap;
			}
		}

		public string Name
		{
			get
			{
				return this.string_0;
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
				Bitmap bitmap;
				bitmap = (!Directory.Exists(this.string_0) ? ImageLib.GetSmallImage(5) : ImageLib.GetSmallImage(1));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!Directory.Exists(this.string_0) ? ImageLib.GetSmallImage(5) : ImageLib.GetSmallImage(1));
				return bitmap;
			}
		}

		public GxDiskConnection()
		{
			this.OnReadCompleted += new OnReadCompletedHander(this.method_7);
		}

		public IGxObject AddChild(IGxObject igxObject_1)
		{
			igxObject_1.Name.ToUpper();
			this.igxObjectArray_0.Insert(-1, igxObject_1);
			return igxObject_1;
		}

		public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
		{
			this.igxCatalog_0 = igxCatalog_1;
			this.igxObject_0 = igxObject_1;
			if (igxObject_1 is IGxObjectContainer)
			{
				(igxObject_1 as IGxObjectContainer).AddChild(this);
			}
		}

		public bool CanCopy()
		{
			return false;
		}

		public bool CanDelete()
		{
			return false;
		}

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
			ienumName_0.Reset();
			IName name = ienumName_0.Next();
			bool type = false;
			while (true)
			{
				if (name != null)
				{
					if (name is IFileName)
					{
						type = true;
					}
					else if (name is IDatasetName)
					{
						type = (name as IDatasetName).WorkspaceName.Type == esriWorkspaceType.esriFileSystemWorkspace;
					}
					else if (!(name is IWorkspaceName))
					{
						flag = false;
						break;
					}
					else
					{
						type = (name as IWorkspaceName).Type == esriWorkspaceType.esriLocalDatabaseWorkspace;
					}
					if (!type)
					{
						flag = false;
						break;
					}
					else
					{
						name = ienumName_0.Next();
					}
				}
				else
				{
					bool_2 = true;
					flag = true;
					break;
				}
			}
			return flag;
		}

		public bool CanRename()
		{
			return false;
		}

		public void Close(bool bool_2)
		{
		}

		public void Delete()
		{
		}

		public void DeleteChild(IGxObject igxObject_1)
		{
			int num = 0;
			while (true)
			{
				if (num >= this.igxObjectArray_0.Count)
				{
					break;
				}
				else if (this.igxObjectArray_0.Item(num) == igxObject_1)
				{
					this.igxObjectArray_0.Remove(num);
					break;
				}
				else
				{
					num++;
				}
			}
		}

		public void Detach()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxCatalog_0.ObjectDeleted(this);
			}
			this.igxCatalog_0 = null;
			if (this.igxObject_0 is IGxObjectContainer)
			{
				(this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
			}
			this.igxObject_0 = null;
		}

		public void Edit()
		{
		}

		public void EditProperties(int int_0)
		{
		}

		public IGxObject FindChildFolder(string string_1)
		{
			return this.method_3(string_1, this.igxObjectArray_0 as IEnumGxObject);
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
			this.ipopuMenuWrap_0.AddItem("PasteItem", false);
			this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
			this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
			this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
			this.ipopuMenuWrap_0.AddItem("NewFolder", "NewBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("NewPersonGDB", "NewBarSubItem", true);
			this.ipopuMenuWrap_0.AddItem("NewFileGDB", "NewBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("NewShapefile", "NewBarSubItem", false);
		}

		private bool method_0(string string_1, string string_2, bool bool_2)
		{
			IDatasetName rasterDatasetNameClass;
			IWorkspaceName workspaceNameClass;
			bool flag;
			IGxObject gxRasterDataset = null;
			string string2 = string_2;
			if (string2 != null)
			{
				switch (string2)
				{
					case ".bmp":
					case ".jpg":
					case ".tif":
					case ".img":
					case ".png":
					{
						gxRasterDataset = new GxRasterDataset();
						rasterDatasetNameClass = new RasterDatasetName() as IDatasetName;
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
					    workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);

                        
						rasterDatasetNameClass.Name = System.IO.Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".mdb":
					{
						gxRasterDataset = new GxDatabase();
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.AccessWorkspaceFactory";
					    workspaceNameClass.PathName = string_1;

						(gxRasterDataset as IGxDatabase).WorkspaceName = workspaceNameClass;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".vct":
					{
						gxRasterDataset = new GxVCTObject();
						(gxRasterDataset as IGxFile).Path = string_1;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".shp":
					{
						gxRasterDataset = new GxShapefileDataset();
						rasterDatasetNameClass = new FeatureClassName() as IDatasetName;
                            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                            workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);
                          
						rasterDatasetNameClass.Name = System.IO.Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".dbf":
					{
						if (!File.Exists(string.Concat(string_1.Substring(0, string_1.Length - 3), "shp")))
						{
							gxRasterDataset = new GxDataset();
							rasterDatasetNameClass = new TableName() as IDatasetName;
                                workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                                workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                                workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);
                               
							rasterDatasetNameClass.Name = System.IO.Path.GetFileName(string_1);
							rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
							(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
							gxRasterDataset.Attach(this, this.igxCatalog_0);
							if (bool_2)
							{
								this.igxCatalog_0.ObjectAdded(gxRasterDataset);
							}
							flag = true;
							break;
						}
						else
						{
							flag = false;
							break;
						}
					}
					case ".sde":
					{
						gxRasterDataset = new GxDatabase();
						workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                            workspaceNameClass.PathName = string_1;
                            
						(gxRasterDataset as IGxDatabase).WorkspaceName = workspaceNameClass;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".lyr":
					{
						gxRasterDataset = new GxLayer();
						(gxRasterDataset as IGxFile).Path = string_1;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".dwg":
					case ".dxf":
					{
						gxRasterDataset = new GxCadDataset();
						rasterDatasetNameClass = new CadDrawingName() as IDatasetName;
                            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
                            workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);
                           
						rasterDatasetNameClass.Name = System.IO.Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".doc":
					case ".xls":
					{
						gxRasterDataset = new GxOfficeFile();
						(gxRasterDataset as IGxFile).Path = string_1;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
						if (bool_2)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					default:
					{
						flag = false;
						return flag;
					}
				}
			}
			else
			{
				flag = false;
				return flag;
			}
			return flag;
		}

		private void method_1(string string_1)
		{
			IGxObject gxCadDataset = new GxCadDataset();
			IGxObject gxCadDrawing = new GxCadDrawing();
			IDatasetName cadDrawingNameClass = new CadDrawingName() as IDatasetName;
            IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName;
            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
            workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);
           
			cadDrawingNameClass.Name = System.IO.Path.GetFileName(string_1);
			cadDrawingNameClass.WorkspaceName = workspaceNameClass;
			(gxCadDataset as IGxDataset).DatasetName = cadDrawingNameClass;
			cadDrawingNameClass = new CadDrawingName() as IDatasetName;
            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
            workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(string_1);
            cadDrawingNameClass.Name = System.IO.Path.GetFileName(string_1);
			cadDrawingNameClass.WorkspaceName = workspaceNameClass;
			(gxCadDrawing as IGxDataset).DatasetName = cadDrawingNameClass;
			gxCadDataset.Attach(this, this.igxCatalog_0);
			gxCadDrawing.Attach(this, this.igxCatalog_0);
		}

		private bool method_10(IFileName ifileName_0, bool bool_2)
		{
			int num;
			bool flag;
			string path = ifileName_0.Path;
			if (!Directory.Exists(path))
			{
				num = path.LastIndexOf("\\");
				path = path.Substring(num + 1);
				string lower = System.IO.Path.GetExtension(ifileName_0.Path).ToLower();
				path = string.Concat(this.string_0, path);
				if (path.ToLower() != ifileName_0.Path.ToLower())
				{
					if (File.Exists(path))
					{
						string str = path.Substring(0, path.Length - lower.Length);
						path = string.Concat(str, "复件", lower);
						int num1 = 1;
						while (File.Exists(path))
						{
							path = string.Concat(str, "复件", num1.ToString(), lower);
							num1++;
						}
					}
					if (!bool_2)
					{
						File.Copy(ifileName_0.Path, path);
					}
					else
					{
						File.Move(ifileName_0.Path, path);
					}
					string lower1 = System.IO.Path.GetExtension(path).ToLower();
					this.method_0(path, lower1, true);
					flag = true;
				}
				else
				{
					MessageBox.Show(string.Concat("无法复制 ", System.IO.Path.GetFileName(path), " 源文件和目标文件相同!"));
					flag = false;
				}
			}
			else
			{
				num = path.LastIndexOf("\\");
				path = path.Substring(num + 1);
				path = string.Concat(this.string_0, path);
				if (bool_2)
				{
					Directory.Move(ifileName_0.Path, path);
				}
				IGxObject gxFolder = new GxFolder();
				(gxFolder as IGxFile).Path = path;
				gxFolder.Attach(this, this.igxCatalog_0);
				flag = true;
			}
			return flag;
		}

		private void method_11(string string_1, string string_2, bool bool_2)
		{
			if (File.Exists(string_1))
			{
				if (!bool_2)
				{
					File.Copy(string_1, string_2, true);
				}
				else
				{
					File.Move(string_1, string_2);
				}
			}
		}

		private void method_2(string string_1)
		{
			string[] files = Directory.GetFiles(this.string_0, string.Concat("*", string_1));
			string[] strArrays = files;
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				this.method_0(strArrays[i], string_1, false);
			}
		}

		private IGxObject method_3(string string_1, IEnumGxObject ienumGxObject_0)
		{
			IGxObject gxObject;
			ienumGxObject_0.Reset();
			IGxObject gxObject1 = ienumGxObject_0.Next();
			while (true)
			{
				if (gxObject1 == null)
				{
					gxObject = null;
					break;
				}
				else if (gxObject1.Name == string_1)
				{
					gxObject = gxObject1;
					break;
				}
				else
				{
					if (gxObject1 is IGxFolder)
					{
						gxObject1 = this.method_3(string_1, (gxObject1 as IGxObjectContainer).Children);
						if (gxObject1 != null)
						{
							gxObject = gxObject1;
							break;
						}
					}
					gxObject1 = ienumGxObject_0.Next();
				}
			}
			return gxObject;
		}

		private string method_4(string string_1)
		{
			string str;
			string string1 = string_1;
			if (System.IO.Path.GetFileName(string1).ToLower() != "info")
			{
				string str1 = string1;
				if (string1[string1.Length - 1] == '\\')
				{
					str1 = string1.Substring(0, string1.Length - 1);
				}
				if (!(System.IO.Path.GetExtension(str1).ToLower() == ".gdb") || !File.Exists(System.IO.Path.Combine(string1, "gdb")))
				{
					if (string1[string1.Length - 1] != '\\')
					{
						string1 = string.Concat(string1, "\\");
					}
					if (File.Exists(string.Concat(string1, "hdr.adf")))
					{
						str = "GRID";
					}
					else if (!File.Exists(string.Concat(string1, "tdenv.adf")))
					{
						if (File.Exists(string.Concat(string1, "dbltic.adf")))
						{
							string str2 = string.Concat(System.IO.Path.GetDirectoryName(string_1), "\\info");
							if (!Directory.Exists(str2) || !File.Exists(string.Concat(str2, "\\arc.dir")))
							{
								goto Label1;
							}
							str = "COVERAGE";
							return str;
						}
					Label1:
						str = "FOLDER";
					}
					else
					{
						str = "TIN";
					}
				}
				else
				{
					str = "FILEGDB";
				}
			}
			else
			{
				str = "INFO";
			}
			return str;
		}

		private void method_5()
		{
			int i;
			IGxObject gxFolder;
			string type;
			IDatasetName tinNameClass;
			IWorkspaceName workspaceNameClass;
			try
			{
				for (i = 0; i < this.openFolderDataHelper_0.m_DirectoryList.Count; i++)
				{
					gxFolder = new GxFolder();
					(gxFolder as IGxFile).Path = this.openFolderDataHelper_0.m_DirectoryList[i].Path;
					gxFolder.Attach(this, this.igxCatalog_0);
				}
			    for (i = 0; i < this.openFolderDataHelper_0.m_FileList.Count; i++)
			    {
			        type = this.openFolderDataHelper_0.m_FileList[i].Type;
			        string str = type;
			        if (str == null)
			        {
			            this.method_0(this.openFolderDataHelper_0.m_FileList[i].Path, type, false);
			        }
			        else if (str == "TIN")
			        {
			            gxFolder = new GxDataset();
			            tinNameClass = new TinName() as IDatasetName;
			            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
			            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory";
			            workspaceNameClass.PathName =
			                System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);

			            tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
			            tinNameClass.WorkspaceName = workspaceNameClass;
			            (gxFolder as IGxDataset).DatasetName = tinNameClass;
			            gxFolder.Attach(this, this.igxCatalog_0);
			        }
			        else if (str == "GRID")
			        {
			            gxFolder = new GxRasterDataset();
			            tinNameClass = new RasterDatasetName() as IDatasetName;
			            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
			            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
			            workspaceNameClass.PathName =
			                System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);


			            tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
			            tinNameClass.WorkspaceName = workspaceNameClass;
			            (gxFolder as IGxDataset).DatasetName = tinNameClass;
			            gxFolder.Attach(this, this.igxCatalog_0);
			        }
			        else if (str == "COVERAGE")
			        {
			            gxFolder = new GxCoverageDataset();
			            tinNameClass = new CoverageName() as IDatasetName;
			            workspaceNameClass = new WorkspaceName() as IWorkspaceName;

			            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1";
			            workspaceNameClass.PathName =
			                System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);


			            tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
			            tinNameClass.WorkspaceName = workspaceNameClass;
			            (gxFolder as IGxDataset).DatasetName = tinNameClass;
			            gxFolder.Attach(this, this.igxCatalog_0);
			        }
			        else if (str == "FILEGDB")
			        {

			            gxFolder = new GxDatabase();
			            workspaceNameClass = new WorkspaceName() as IWorkspaceName;
			            workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1";
			            workspaceNameClass.PathName = this.openFolderDataHelper_0.m_FileList[i].Path;

			            (gxFolder as IGxDatabase).WorkspaceName = workspaceNameClass;
			            gxFolder.Attach(this, this.igxCatalog_0);
			        }
			        else
			        {
			            this.method_0(this.openFolderDataHelper_0.m_FileList[i].Path, type, false);
			        }

			    }
			}
			catch
			{
			}
	
		}

		private void method_6()
		{
			this.bool_1 = true;
		}

		private void method_7()
		{
			this.bool_1 = true;
			this.bool_0 = false;
		}

		private bool method_8(IDatasetName idatasetName_0, bool bool_2)
		{
			string extension = System.IO.Path.GetExtension(idatasetName_0.Name);
			string str = string.Concat(idatasetName_0.WorkspaceName.PathName, "\\", System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name));
			string str1 = string.Concat(this.string_0, System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name));
			if (File.Exists(string.Concat(str1, extension)))
			{
				int num = 1;
				str1 = string.Concat(str1, "复件");
				while (File.Exists(string.Concat(str1, extension)))
				{
					str1 = string.Concat(str1, num.ToString());
					num++;
				}
			}
			if (!bool_2)
			{
				File.Copy(string.Concat(str, extension), string.Concat(str1, extension));
			}
			else
			{
				File.Move(string.Concat(str, extension), string.Concat(str1, extension));
			}
			if (extension == ".shp")
			{
				this.method_11(string.Concat(str, ".dbf"), string.Concat(str1, ".dbf"), bool_2);
				this.method_11(string.Concat(str, ".prj"), string.Concat(str1, ".prj"), bool_2);
				this.method_11(string.Concat(str, ".sbn"), string.Concat(str1, ".sbn"), bool_2);
				this.method_11(string.Concat(str, ".sbx"), string.Concat(str1, ".sbx"), bool_2);
				this.method_11(string.Concat(str, ".shx"), string.Concat(str1, ".shx"), bool_2);
				this.method_11(string.Concat(str, ".shp.xml"), string.Concat(str1, ".shp.xml"), bool_2);
			}
			this.method_0(string.Concat(str1, extension), extension, true);
			return true;
		}

		private bool method_9(IWorkspaceName iworkspaceName_0, bool bool_2)
		{
			bool flag;
			string pathName = iworkspaceName_0.PathName;
			string lower = System.IO.Path.GetExtension(pathName).ToLower();
			string str = string.Concat(this.string_0, System.IO.Path.GetFileNameWithoutExtension(pathName));
			if (pathName.ToLower() != str.ToLower())
			{
				if (File.Exists(string.Concat(str, lower)))
				{
					int num = 1;
					str = string.Concat(str, "复件");
					while (File.Exists(string.Concat(str, lower)))
					{
						str = string.Concat(str, num.ToString());
						num++;
					}
				}
				if (!bool_2)
				{
					File.Copy(pathName, string.Concat(str, lower));
				}
				else
				{
					File.Move(pathName, string.Concat(str, lower));
				}
				this.method_0(pathName, lower, true);
				flag = true;
			}
			else
			{
				MessageBox.Show(string.Concat("无法复制 ", System.IO.Path.GetFileName(pathName), " 源文件和目标文件相同!"));
				flag = false;
			}
			return flag;
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
			ManualResetEvent[] manualResetEvent = new ManualResetEvent[] { new ManualResetEvent(false) };
			State state = new State(manualResetEvent[0]);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.Open1), state);
			WaitHandle.WaitAll(manualResetEvent);
		}

		public void Open1(object object_0)
		{
			this.openFolderDataHelper_0.InvokeMethod(object_0);
		}

		public void OpenFolder()
		{
			int i;
			IGxObject gxFolder;
			IDatasetName tinNameClass;
			IWorkspaceName workspaceNameClass;
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					string[] directories = Directory.GetDirectories(this.string_0);
					IArray arrayClass = new Array() as IArray;
					string[] files = directories;
					for (i = 0; i < (int)files.Length; i++)
					{
						string str = files[i];
						string str1 = this.method_4(str);
						if (str1 != null)
						{
							if (str1 == "FOLDER")
							{
								gxFolder = new GxFolder();
								(gxFolder as IGxFile).Path = str;
								gxFolder.Attach(this, this.igxCatalog_0);
							}
							else if (str1 == "TIN")
							{
								gxFolder = new GxDataset();
								tinNameClass = new TinName()  as IDatasetName;
							    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
							    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory";
							    workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(str);

                          
								tinNameClass.Name = System.IO.Path.GetFileName(str);
								tinNameClass.WorkspaceName = workspaceNameClass;
								(gxFolder as IGxDataset).DatasetName = tinNameClass;
								arrayClass.Add(gxFolder);
							}
							else if (str1 == "GRID")
							{
								gxFolder = new GxRasterDataset();
								tinNameClass = new RasterDatasetName() as IDatasetName;
                                workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                                workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
                                workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(str);
                               
								tinNameClass.Name = System.IO.Path.GetFileName(str);
								tinNameClass.WorkspaceName = workspaceNameClass;
								(gxFolder as IGxDataset).DatasetName = tinNameClass;
								arrayClass.Add(gxFolder);
							}
							else if (str1 == "COVERAGE")
							{
								gxFolder = new GxCoverageDataset();
								tinNameClass = new CoverageName() as IDatasetName;
                                workspaceNameClass = new WorkspaceName() as IWorkspaceName;
							    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1";
                                workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(str);
                               
								tinNameClass.Name = System.IO.Path.GetFileName(str);
								tinNameClass.WorkspaceName = workspaceNameClass;
								(gxFolder as IGxDataset).DatasetName = tinNameClass;
								arrayClass.Add(gxFolder);
							}
							else if (str1 == "FILEGDB")
							{
								gxFolder = new GxDatabase();
								workspaceNameClass = new WorkspaceName() as IWorkspaceName;
							    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1";
							    workspaceNameClass.PathName = str;
                               
								(gxFolder as IGxDatabase).WorkspaceName = workspaceNameClass;
								arrayClass.Add(gxFolder);
							}
						}
					}
					for (int j = 0; j < arrayClass.Count; j++)
					{
						gxFolder = arrayClass.Element[j] as IGxObject;
						gxFolder.Attach(this, this.igxCatalog_0);
					}
					files = Directory.GetFiles(this.string_0);
					for (i = 0; i < (int)files.Length; i++)
					{
						string str2 = files[i];
						string lower = System.IO.Path.GetExtension(str2).ToLower();
						if (lower != null)
						{
							this.method_0(str2, lower, false);
						}
					}
				}
				catch
				{
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		public void OpenFolder1(object object_0)
		{
			this.OpenFolder();
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
			try
			{
				ienumName_0.Reset();
				IName name = ienumName_0.Next();
				bool flag1 = false;
				if (this.igxObjectArray_0.Count == 0)
				{
					this.Open();
				}
				while (name != null)
				{
					if (name is IFileName)
					{
						flag1 = this.method_10(name as IFileName, bool_2);
					}
					else if (name is IDatasetName)
					{
						if ((name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
						{
							flag1 = this.method_8(name as IDatasetName, bool_2);
						}
					}
					else if (name is IWorkspaceName)
					{
						flag1 = this.method_9(name as IWorkspaceName, bool_2);
					}
					name = ienumName_0.Next();
				}
				flag = flag1;
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(this, exception, "");
				flag = false;
			}
			return flag;
		}

		public void Refresh()
		{
			this.bool_0 = true;
			this.igxObjectArray_0.Empty();
			this.Open();
			this.method_5();
			this.igxCatalog_0.ObjectRefreshed(this);
		}

		public void RefreshStatus()
		{
		}

		public void Rename(string string_1)
		{
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

		internal event OnReadCompletedHander OnReadCompleted
		{
			add
			{
				OnReadCompletedHander onReadCompletedHander;
				OnReadCompletedHander onReadCompletedHander0 = this.onReadCompletedHander_0;
				do
				{
					onReadCompletedHander = onReadCompletedHander0;
					OnReadCompletedHander onReadCompletedHander1 = (OnReadCompletedHander)Delegate.Combine(onReadCompletedHander, value);
					onReadCompletedHander0 = Interlocked.CompareExchange<OnReadCompletedHander>(ref this.onReadCompletedHander_0, onReadCompletedHander1, onReadCompletedHander);
				}
				while ((object)onReadCompletedHander0 != (object)onReadCompletedHander);
			}
			remove
			{
				OnReadCompletedHander onReadCompletedHander;
				OnReadCompletedHander onReadCompletedHander0 = this.onReadCompletedHander_0;
				do
				{
					onReadCompletedHander = onReadCompletedHander0;
					OnReadCompletedHander onReadCompletedHander1 = (OnReadCompletedHander)Delegate.Remove(onReadCompletedHander, value);
					onReadCompletedHander0 = Interlocked.CompareExchange<OnReadCompletedHander>(ref this.onReadCompletedHander_0, onReadCompletedHander1, onReadCompletedHander);
				}
				while ((object)onReadCompletedHander0 != (object)onReadCompletedHander);
			}
		}
	}
}