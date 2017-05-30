using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Array = ESRI.ArcGIS.esriSystem.Array;

namespace Yutai.Catalog
{
	internal sealed class OpenFolderDataHelper : Control
	{
		public IList<GxObjectStruct> m_DirectoryList = new List<GxObjectStruct>();

		public IList<GxObjectStruct> m_FileList = new List<GxObjectStruct>();

		private IGxObject igxObject_0;

		private IGxObjectArray igxObjectArray_0;

		private string string_0;

		private IGxCatalog igxCatalog_0 = null;

		private IArray iarray_0 = new Array() as IArray;

		private OnReadCompletedHander onReadCompletedHander_0;

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
			int i;
			GxObjectStruct gxObjectStruct;
			this.m_FileList.Clear();
			this.m_DirectoryList.Clear();
			State object0 = (State)object_0;
			if ((base.IsDisposed ? false : base.IsHandleCreated))
			{
				try
				{
					this.string_0 = (this.igxObject_0 as IGxFile).Path;
					if (this.string_0 != null)
					{
						string[] directories = Directory.GetDirectories(this.string_0);
						for (i = 0; i < (int)directories.Length; i++)
						{
							string str = directories[i];
							gxObjectStruct = new GxObjectStruct()
							{
								Type = this.method_1(str),
								Path = str
							};
							if (gxObjectStruct.Type != "FOLDER")
							{
								this.m_FileList.Add(gxObjectStruct);
							}
							else
							{
								this.m_DirectoryList.Add(gxObjectStruct);
							}
						}
						directories = Directory.GetFiles(this.string_0);
						for (i = 0; i < (int)directories.Length; i++)
						{
							string str1 = directories[i];
							string lower = Path.GetExtension(str1).ToLower();
							if (this.method_2(str1, lower))
							{
								gxObjectStruct = new GxObjectStruct()
								{
									Type = lower,
									Path = str1
								};
								this.m_FileList.Add(gxObjectStruct);
							}
						}
					}
					else
					{
						if (this.onReadCompletedHander_0 != null)
						{
							this.onReadCompletedHander_0();
						}
						object0.Set();
					}
				}
				catch
				{
				}
			}
		}

		public void InvokeMethod1()
		{
			if ((base.IsDisposed ? false : base.IsHandleCreated))
			{
				base.Invoke(new OpenFolderDataHelper.MessageHandler(this.OpenFolder));
			}
		}

		public void InvokeMethod2(object object_0)
		{
			State object0 = (State)object_0;
			if ((base.IsDisposed ? true : !base.IsHandleCreated))
			{
				object0.Set();
				if (this.onReadCompletedHander_0 != null)
				{
					this.onReadCompletedHander_0();
				}
			}
			else
			{
				try
				{
					this.string_0 = (this.igxObject_0 as IGxFile).Path;
					if (this.string_0 != null)
					{
						string[] directories = Directory.GetDirectories(this.string_0);
						object0.Set();
						OpenFolderDataHelper.MessageHandler1 messageHandler1 = new OpenFolderDataHelper.MessageHandler1(this.method_3);
						object[] objArray = new object[] { directories };
						base.Invoke(messageHandler1, objArray);
						object0.Reset();
						object0.Set();
						base.Invoke(new OpenFolderDataHelper.MessageHandler(this.method_4));
						object0.Reset();
						string[] files = Directory.GetFiles(this.string_0);
						object0.Set();
						string[] strArrays = files;
						for (int i = 0; i < (int)strArrays.Length; i++)
						{
							string str = strArrays[i];
							OpenFolderDataHelper.MessageHandler2 messageHandler2 = new OpenFolderDataHelper.MessageHandler2(this.method_5);
							objArray = new object[] { str };
							base.Invoke(messageHandler2, objArray);
						}
					}
					else
					{
						if (this.onReadCompletedHander_0 != null)
						{
							this.onReadCompletedHander_0();
						}
						object0.Set();
						return;
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
		}

		private bool method_0(string string_1, string string_2, bool bool_0)
		{
			IDatasetName rasterDatasetNameClass;
			IWorkspaceName workspaceNameClass;
			bool flag;
			IGxObject gxRasterDataset = null;
			string lower = string_2.ToLower();
			if (lower != null)
			{
				switch (lower)
				{
					case ".bmp":
					case ".png":
					case ".jpg":
					case ".tif":
					case ".img":
					case ".sid":
					{
						gxRasterDataset = new GxRasterDataset();
						rasterDatasetNameClass = new RasterDatasetName() as IDatasetName;
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
					    workspaceNameClass.PathName = Path.GetDirectoryName(string_1);
						rasterDatasetNameClass.Name = Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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
					    workspaceNameClass.PathName = Path.GetDirectoryName(string_1);
                      
						rasterDatasetNameClass.Name = Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
						{
							this.igxCatalog_0.ObjectAdded(gxRasterDataset);
						}
						flag = true;
						break;
					}
					case ".dbf":
					{
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(string_1), string.Concat(Path.GetFileNameWithoutExtension(string_1), ".shp"))))
						{
							gxRasterDataset = new GxDataset();
							rasterDatasetNameClass = new TableName() as IDatasetName;
							workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                                workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                                workspaceNameClass.PathName = Path.GetDirectoryName(string_1);
                              
							rasterDatasetNameClass.Name = Path.GetFileName(string_1);
							rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
							(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
							gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
							if (bool_0)
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
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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
					    workspaceNameClass.PathName = Path.GetDirectoryName(string_1);

						rasterDatasetNameClass.Name = Path.GetFileName(string_1);
						rasterDatasetNameClass.WorkspaceName = workspaceNameClass;
						(gxRasterDataset as IGxDataset).DatasetName = rasterDatasetNameClass;
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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
						gxRasterDataset.Attach(this.igxObject_0, this.igxCatalog_0);
						if (bool_0)
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

		private string method_1(string string_1)
		{
			string str;
			string string1 = string_1;
			if (Path.GetFileName(string1).ToLower() != "info")
			{
				string str1 = string1;
				if (string1[string1.Length - 1] == '\\')
				{
					str1 = string1.Substring(0, string1.Length - 1);
				}
				if (!(Path.GetExtension(str1).ToLower() == ".gdb") || !File.Exists(Path.Combine(string1, "gdb")))
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
							string str2 = string.Concat(Path.GetDirectoryName(string_1), "\\info");
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

		private bool method_2(string string_1, string string_2)
		{
			bool flag;
			string string2 = string_2;
			if (string2 != null)
			{
				switch (string2)
				{
					case ".bmp":
					case ".jpg":
					case ".tif":
					case ".img":
					case ".sid":
					{
						flag = true;
						break;
					}
					case ".mdb":
					{
						flag = true;
						break;
					}
					case ".shp":
					{
						flag = true;
						break;
					}
					case ".dbf":
					{
						if (!File.Exists(Path.Combine(Path.GetDirectoryName(string_1), string.Concat(Path.GetFileNameWithoutExtension(string_1), ".shp"))))
						{
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
						flag = true;
						break;
					}
					case ".lyr":
					{
						flag = true;
						break;
					}
					case ".dwg":
					case ".dxf":
					{
						flag = true;
						break;
					}
					case ".doc":
					case ".xls":
					{
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

		private void method_3(string[] string_1)
		{
			IDatasetName tinNameClass;
			IWorkspaceName workspaceNameClass;
			string[] string1 = string_1;
			for (int i = 0; i < (int)string1.Length; i++)
			{
				string str = string1[i];
				IGxObject gxFolder = null;
				string str1 = this.method_1(str);
				if (str1 != null)
				{
					if (str1 == "FOLDER")
					{
						gxFolder = new GxFolder();
						(gxFolder as IGxFile).Path = str;
						gxFolder.Attach(this.igxObject_0, this.igxCatalog_0);
					}
					else if (str1 == "TIN")
					{
						gxFolder = new GxDataset();
						tinNameClass = new TinName() as IDatasetName;
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
					    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory";
					    workspaceNameClass.PathName = Path.GetDirectoryName(str);
                        
						tinNameClass.Name = Path.GetFileName(str);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						this.iarray_0.Add(gxFolder);
					}
					else if (str1 == "GRID")
					{
						gxFolder = new GxRasterDataset();
						tinNameClass = new RasterDatasetName() as IDatasetName;
						workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                        workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.RasterWorkspaceFactory";
                        workspaceNameClass.PathName = Path.GetDirectoryName(str);
                       
						tinNameClass.Name = Path.GetFileName(str);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						this.iarray_0.Add(gxFolder);
					}
					else if (str1 == "COVERAGE")
					{
						gxFolder = new GxCoverageDataset();
						tinNameClass = new CoverageName() as IDatasetName;
						workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                        workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory";
                        workspaceNameClass.PathName = Path.GetDirectoryName(str);
                        
						tinNameClass.Name = Path.GetFileName(str);
						tinNameClass.WorkspaceName = workspaceNameClass;
						(gxFolder as IGxDataset).DatasetName = tinNameClass;
						this.iarray_0.Add(gxFolder);
					}
					else if (str1 == "FILEGDB")
					{
						gxFolder = new GxDatabase();
					    workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                        workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesGDB.FileGDBWorkspaceFactory.1";
                        workspaceNameClass.PathName = str;
                       
						(gxFolder as IGxDatabase).WorkspaceName = workspaceNameClass;
						this.iarray_0.Add(gxFolder);
					}
				}
			}
		}

		private void method_4()
		{
			for (int i = 0; i < this.iarray_0.Count; i++)
			{
				IGxObject element = this.iarray_0.Element[i] as IGxObject;
				element.Attach(this.igxObject_0, this.igxCatalog_0);
			}
			this.iarray_0.RemoveAll();
		}

		private void method_5(string string_1)
		{
			string lower = Path.GetExtension(string_1).ToLower();
			if (lower != null)
			{
				this.method_0(string_1, lower, false);
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
			int i;
			IGxObject gxFolder;
			IDatasetName tinNameClass;
			IWorkspaceName workspaceNameClass;
			try
			{
				try
				{
					this.string_0 = (this.igxObject_0 as IGxFile).Path;
					if (this.string_0 != null)
					{
						string[] directories = Directory.GetDirectories(this.string_0);
						IArray arrayClass = new Array();
						string[] files = directories;
						for (i = 0; i < (int)files.Length; i++)
						{
							string str = files[i];
							string str1 = this.method_1(str);
							if (str1 != null)
							{
								if (str1 == "FOLDER")
								{
									gxFolder = new GxFolder();
									(gxFolder as IGxFile).Path = str;
									gxFolder.Attach(this.igxObject_0, this.igxCatalog_0);
								}
								else if (str1 == "TIN")
								{
									gxFolder = new GxDataset();
									tinNameClass = new TinName() as IDatasetName;
									workspaceNameClass = new WorkspaceName() as IWorkspaceName;
								    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.TinWorkspaceFactory";
								    workspaceNameClass.PathName = Path.GetDirectoryName(str);

                                   
									tinNameClass.Name = Path.GetFileName(str);
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
                                    workspaceNameClass.PathName = Path.GetDirectoryName(str);
                                    
									tinNameClass.Name = Path.GetFileName(str);
									tinNameClass.WorkspaceName = workspaceNameClass;
									(gxFolder as IGxDataset).DatasetName = tinNameClass;
									arrayClass.Add(gxFolder);
								}
								else if (str1 == "COVERAGE")
								{
									gxFolder = new GxCoverageDataset();
									tinNameClass = new CoverageName() as IDatasetName;
									workspaceNameClass = new WorkspaceName() as IWorkspaceName;
                                    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.ArcInfoWorkspaceFactory";
                                    workspaceNameClass.PathName = Path.GetDirectoryName(str);
                                    
									tinNameClass.Name = Path.GetFileName(str);
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
							gxFolder.Attach(this.igxObject_0, this.igxCatalog_0);
						}
						files = Directory.GetFiles(this.string_0);
						for (i = 0; i < (int)files.Length; i++)
						{
							string str2 = files[i];
							string lower = Path.GetExtension(str2).ToLower();
							if (lower != null)
							{
								this.method_0(str2, lower, false);
							}
						}
					}
					else
					{
						if (this.onReadCompletedHander_0 != null)
						{
							this.onReadCompletedHander_0();
						}
						return;
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

		public delegate void MessageHandler();

		public delegate void MessageHandler1(string[] string_0);

		public delegate void MessageHandler2(string string_0);
	}
}