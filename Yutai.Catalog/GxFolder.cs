using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxFolder : IGxObject, IGxFolder, IGxObjectContainer, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxFolderAdmin
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private string string_0 = "";

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private bool bool_0 = false;

		private bool bool_1 = false;

		private OpenFolderDataHelper openFolderDataHelper_0 = null;

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

                return pName as IName;
			}
		}

		public bool IsValid
		{
			get
			{
				return false;
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
			    IFileName pName = new FileName() as IFileName;
			    pName.Path = this.string_0;

				return pName as IName;
			}
			set
			{
				IFileName fileName = value as IFileName;
				if (fileName != null)
				{
					this.string_0 = fileName.Path;
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
				string str;
				str = (!(this.igxObject_0 is IGxCatalog) ? System.IO.Path.GetFileName(this.string_0) : this.string_0);
				return str;
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

		public GxFolder()
		{
		}

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
			bool flag;
			IDatasetName rasterDatasetNameClass;
			IWorkspaceName workspaceNameClass;
			string lower = System.IO.Path.GetExtension(string_1).ToLower();
			if (lower != null)
			{
				IGxObject gxRasterDataset = null;
				string str = lower;
				if (str != null)
				{
					switch (str)
					{
						case ".bmp":
						case ".jpg":
						case ".tif":
						case ".img":
						case ".png":
						case ".sid":
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
							return flag;
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
							return flag;
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
							return flag;
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
							return flag;
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
							return flag;
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
								return flag;
							}
							else
							{
								flag = false;
								return flag;
							}
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
							return flag;
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
							return flag;
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
							return flag;
						}
					}
				}
				flag = false;
			}
			else
			{
				flag = false;
			}
			return flag;
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

		private void method_3()
		{
			string[] directories;
			int i;
			IGxObject gxFolder;
			IDatasetName tinNameClass;
			IWorkspaceName workspaceNameClass;
			this.bool_0 = false;
			try
			{
				directories = Directory.GetDirectories(this.string_0);
			}
			catch
			{
				return;
			}
			IArray arrayClass = new ESRI.ArcGIS.esriSystem.Array();
			string[] files = directories;
			for (i = 0; i < (int)files.Length; i++)
			{
				string str = files[i];
				string str1 = this.method_2(str);
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
						tinNameClass = new TinName() as IDatasetName;
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
				this.method_0(files[i], false);
			}
			this.bool_1 = true;
		}

		private void method_4()
		{
			int i;
			IGxObject gxFolder;
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
					string type = this.openFolderDataHelper_0.m_FileList[i].Type;
					if (type == null)
					{
                        this.method_0(this.openFolderDataHelper_0.m_FileList[i].Path, false);
                        continue;
                    }
					else if (type == "TIN")
					{
						gxFolder = new GxDataset();
						tinNameClass = new TinName() as IDatasetName;
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory";
					    workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);

						tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						gxFolder.Attach(this, this.igxCatalog_0);
					}
					else if (type == "GRID")
					{
						gxFolder = new GxRasterDataset();
						tinNameClass = new RasterDatasetName() as IDatasetName;
                        workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                        workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
                        workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);
                       
						tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						gxFolder.Attach(this, this.igxCatalog_0);
					}
					else if (type == "COVERAGE")
					{
						gxFolder = new GxCoverageDataset();
						tinNameClass = new CoverageName() as IDatasetName;
                        workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory.1";
                        workspaceNameClass.PathName = System.IO.Path.GetDirectoryName(this.openFolderDataHelper_0.m_FileList[i].Path);
                       
						tinNameClass.Name = System.IO.Path.GetFileName(this.openFolderDataHelper_0.m_FileList[i].Path);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						gxFolder.Attach(this, this.igxCatalog_0);
					}
					else
					{
						if (type != "FILEGDB")
						{
                            this.method_0(this.openFolderDataHelper_0.m_FileList[i].Path, false);
						    continue;
						}
						gxFolder = new GxDatabase();
						workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1";
					    workspaceNameClass.PathName = this.openFolderDataHelper_0.m_FileList[i].Path;
                        
						(gxFolder as IGxDatabase).WorkspaceName = workspaceNameClass;
						gxFolder.Attach(this, this.igxCatalog_0);
					}
				
				}
			}
			catch
			{
			}
			return;
	
		}

		private bool method_5(IDatasetName idatasetName_0, bool bool_2)
		{
			string extension = System.IO.Path.GetExtension(idatasetName_0.Name);
			string str = string.Concat(idatasetName_0.WorkspaceName.PathName, "\\", System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name));
			string str1 = string.Concat(this.string_0, "\\", System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name));
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
				this.method_9(string.Concat(str, ".dbf"), string.Concat(str1, ".dbf"), bool_2);
				this.method_9(string.Concat(str, ".prj"), string.Concat(str1, ".prj"), bool_2);
				this.method_9(string.Concat(str, ".sbn"), string.Concat(str1, ".sbn"), bool_2);
				this.method_9(string.Concat(str, ".sbx"), string.Concat(str1, ".sbx"), bool_2);
				this.method_9(string.Concat(str, ".shx"), string.Concat(str1, ".shx"), bool_2);
				this.method_9(string.Concat(str, ".shp.xml"), string.Concat(str1, ".shp.xml"), bool_2);
			}
			this.method_0(string.Concat(str1, extension), true);
			return true;
		}

		private bool method_6(IDatasetName idatasetName_0, bool bool_2)
		{
			bool flag;
			try
			{
				IDataset dataset = (idatasetName_0 as IName).Open() as IDataset;
				if (dataset != null)
				{
				    IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName;
				    
				        workspaceNameClass.PathName = this.string_0;
				        workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
				    
					IWorkspace workspace = (workspaceNameClass as IName).Open() as IWorkspace;
					dataset.Copy(idatasetName_0.Name, workspace);
					string extension = System.IO.Path.GetExtension(idatasetName_0.Name);
					string str = string.Concat(this.string_0, "\\", System.IO.Path.GetFileNameWithoutExtension(idatasetName_0.Name));
					this.method_0(string.Concat(str, extension), true);
					flag = true;
					return flag;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
			flag = false;
			return flag;
		}

		private bool method_7(IWorkspaceName iworkspaceName_0, bool bool_2)
		{
			bool flag;
			string pathName = iworkspaceName_0.PathName;
			string extension = System.IO.Path.GetExtension(pathName);
			string str = string.Concat(this.string_0, "\\", System.IO.Path.GetFileNameWithoutExtension(pathName));
			if (pathName.ToLower() != str.ToLower())
			{
				if (File.Exists(string.Concat(str, extension)))
				{
					int num = 1;
					str = string.Concat(str, "复件");
					while (File.Exists(string.Concat(str, extension)))
					{
						str = string.Concat(str, num.ToString());
						num++;
					}
				}
				if (!bool_2)
				{
					File.Copy(pathName, string.Concat(str, extension));
				}
				else
				{
					File.Move(pathName, string.Concat(str, extension));
				}
				this.method_0(string.Concat(str, extension), true);
				flag = true;
			}
			else
			{
				MessageBox.Show(string.Concat("无法复制 ", System.IO.Path.GetFileName(pathName), " 源文件和目标文件相同!"));
				flag = false;
			}
			return flag;
		}

		private bool method_8(IFileName ifileName_0, bool bool_2)
		{
			int num;
			bool flag;
			string path = ifileName_0.Path;
			if (!Directory.Exists(path))
			{
				num = path.LastIndexOf("\\");
				path = path.Substring(num + 1);
				string lower = System.IO.Path.GetExtension(ifileName_0.Path).ToLower();
				path = string.Concat(this.string_0, "\\", path);
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
					this.method_0(path, true);
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
				path = string.Concat(this.string_0, "\\", path);
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

		private void method_9(string string_1, string string_2, bool bool_2)
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

		public void OpenFolder1(object object_0)
		{
			this.method_3();
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
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
				flag = true;
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
					if (string_1.Trim().Length != 0)
					{
						string str = string.Concat(System.IO.Path.GetDirectoryName(this.string_0), "\\", string_1);
						if (str.ToLower() != this.string_0.ToLower())
						{
							Directory.Move(this.string_0, str);
							this.string_0 = str;
							this.igxCatalog_0.ObjectChanged(this);
						}
						else
						{
							return;
						}
					}
					else
					{
						MessageBox.Show("必须键入目录名!");
						this.igxCatalog_0.ObjectChanged(this);
						return;
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
	}
}