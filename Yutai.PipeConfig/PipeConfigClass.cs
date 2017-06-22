using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public class PipeConfigClass : IPipeConfig, IMetaPointTable, IMetaLineTable, ICoding, IMetaTerrainTable, IEMLayerInfo, IEMDataSetInfo
	{
		private Dictionary<int, CodeItem> m_dicCode = new Dictionary<int, CodeItem>();

		private SortedList<string, CodeItem>[] m_dicCodeTab = new SortedList<string, CodeItem>[10];

		private Dictionary<string, PointConfig> m_dicPointConfig = new Dictionary<string, PointConfig>();

		private Dictionary<string, LineConfig> m_dicLineConfig = new Dictionary<string, LineConfig>();

		private Dictionary<string, LayerConfig> m_dicLayerConfig = new Dictionary<string, LayerConfig>();

		private Dictionary<string, DataSetConfig> m_dicDatasetConfig = new Dictionary<string, DataSetConfig>();

		private Dictionary<string, string> m_dicMetaPoint = new Dictionary<string, string>();

		private Dictionary<string, string> m_dicMetaLine = new Dictionary<string, string>();

		private Dictionary<string, string> m_dicMetaTerrain = new Dictionary<string, string>();

		public string Adjunct
		{
			get
			{
				return this.GetPointTableFieldName("附件");
			}
			set
			{
			}
		}

		public string AdjunctPertain
		{
			get
			{
				return this.GetPointTableFieldName("所属");
			}
			set
			{
			}
		}

		public string BuildHigh
		{
			get
			{
				return this.GetTerrainTableFieldName("楼底高程");
			}
			set
			{
			}
		}

		public string Diameter
		{
			get
			{
				return this.GetLineTableFieldName("管径");
			}
			set
			{
			}
		}

		public string FloorHigh
		{
			get
			{
				return this.GetTerrainTableFieldName("单层楼高");
			}
			set
			{
			}
		}

		public string FloorNumber
		{
			get
			{
				return this.GetTerrainTableFieldName("楼层");
			}
			set
			{
			}
		}

		public string FlowDirection
		{
			get
			{
				return this.GetLineTableFieldName("流向");
			}
			set
			{
			}
		}

		public string Height
		{
			get
			{
				return this.GetPointTableFieldName("地面高程");
			}
			set
			{
			}
		}

		public string Kind
		{
			get
			{
				return this.GetLineTableFieldName("管线性质");
			}
			set
			{
			}
		}

		public string Material
		{
			get
			{
				return this.GetLineTableFieldName("材质");
			}
			set
			{
			}
		}

		public string Pertain
		{
			get
			{
				return this.GetLineTableFieldName("所属");
			}
			set
			{
			}
		}

		public string Place
		{
			get
			{
				return this.GetLineTableFieldName("所在道路");
			}
			set
			{
			}
		}

		public string PointKind
		{
			get
			{
				return this.GetPointTableFieldName("点性");
			}
			set
			{
			}
		}

		public string Section_Size
		{
			get
			{
				return this.GetLineTableFieldName("断面尺寸");
			}
			set
			{
			}
		}

		public PipeConfigClass()
		{
		}

		private void AddToCodeTab(CodeItem cfg)
		{
			int code = cfg.Code / 1000;
			if ((code <= 9 && code >= 1))
			{
				if (!this.m_dicCodeTab[code].ContainsKey(cfg.CodeName))
				{
					this.m_dicCodeTab[code].Add(cfg.CodeName, cfg);
				}
			}
		}

		public DataSetConfig DatasetConfigItem(int idx)
		{
			DataSetConfig dataSetConfig;
			DataSetConfig dataSetConfig1;
			try
			{
				if ((idx >= 0 && idx < this.m_dicDatasetConfig.Count))
				{
					Dictionary<string, DataSetConfig>.ValueCollection values = this.m_dicDatasetConfig.Values;
					int num = 0;
					foreach (DataSetConfig value in values)
					{
						if (num != idx)
						{
							num++;
						}
						else
						{
							dataSetConfig = value;
							dataSetConfig1 = dataSetConfig;
							return dataSetConfig1;
						}
					}
					dataSetConfig = null;
				}
				else
				{
					dataSetConfig = null;
				}
			}
			catch (Exception exception)
			{
				dataSetConfig = null;
			}
			dataSetConfig1 = dataSetConfig;
			return dataSetConfig1;
		}

		public DataSetConfig DatasetConfigItem(string sTableName)
		{
			DataSetConfig item;
			try
			{
				item = this.m_dicDatasetConfig[sTableName];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public DataSetConfig DataSetConfigItem(string sTableName)
		{
			DataSetConfig item;
			try
			{
				int num = sTableName.LastIndexOf(".", StringComparison.Ordinal);
				if (num != -1)
				{
					sTableName = sTableName.Substring(num + 1);
				}
				item = this.m_dicDatasetConfig[sTableName];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public int getCode(int ig, string sName)
		{
			CodeItem codeItem = this.getCodeItem(ig, sName);
			return (codeItem == null ? 0 : codeItem.Code);
		}

		public int getCodeCount(int iGroup)
		{
			int num;
			num = ((iGroup <= 9 && iGroup >= 1) ? this.m_dicCodeTab[iGroup].Count : -1);
			return num;
		}

		public int getCodeGroup(string sKind)
		{
			int num;
			int num1 = 1;
			while (true)
			{
				if (num1 >= 10)
				{
					num = -1;
					break;
				}
				else if (!this.m_dicCodeTab[num1].ContainsKey(sKind))
				{
					num1++;
				}
				else
				{
					num = num1;
					break;
				}
			}
			return num;
		}

		public CodeItem getCodeItem(int iCode)
		{
			CodeItem item;
			try
			{
				item = this.m_dicCode[iCode];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public CodeItem getCodeItem(int ig, string sName)
		{
			CodeItem codeItem;
			CodeItem codeItem1;
			CodeItem codeItem2;
			if ((ig <= 9 && ig >= 1))
			{
				try
				{
					if (!this.m_dicCodeTab[ig].TryGetValue(sName, out codeItem1))
					{
						codeItem = null;
					}
					else
					{
						codeItem = codeItem1;
					}
				}
				catch (Exception exception)
				{
					codeItem = null;
				}
				codeItem2 = codeItem;
			}
			else
			{
				codeItem2 = null;
			}
			return codeItem2;
		}

		public CodeItem getCodeItem(int iGroup, int idx)
		{
			CodeItem item;
			if ((iGroup <= 9 && iGroup >= 1))
			{
				item = this.m_dicCodeTab[iGroup].Values[idx];
			}
			else
			{
				item = null;
			}
			return item;
		}

		public int getCodeKind(int iCode)
		{
			CodeItem codeItem = this.getCodeItem(iCode);
			return (codeItem == null ? 0 : codeItem.CodeKind);
		}

		public string getCodeName(int iCode)
		{
			CodeItem codeItem = this.getCodeItem(iCode);
			return (codeItem == null ? "" : codeItem.CodeName);
		}

		public int getCodeSize(int iCode)
		{
			CodeItem codeItem = this.getCodeItem(iCode);
			return (codeItem == null ? 0 : codeItem.CodeSize);
		}

		public string GetDataSetNameByEName(string strAlias)
		{
			DataSetConfig dataSetConfig = this.DatasetConfigItem(strAlias);
			return (dataSetConfig != null ? dataSetConfig.strName : "");
		}

		public int GetDataSetNum()
		{
			return this.m_dicDatasetConfig.Count;
		}

		public int GetLayerNum()
		{
			return this.m_dicLayerConfig.Count;
		}

		public int getLineConfig_Color(string sLineTableName)
		{
			LayerConfig layerConfig = this.LayerConfigItem(sLineTableName);
			return (layerConfig != null ? layerConfig.iColor : 0);
		}

		public string getLineConfig_DM(string sLineTableName)
		{
			LineConfig lineConfig = this.LineConfigItem(sLineTableName);
			return (lineConfig != null ? lineConfig.sDM : "");
		}

		public string getLineConfig_FDatasetName(string sLineTableName)
		{
			LayerConfig layerConfig = this.LayerConfigItem(sLineTableName);
			return (layerConfig != null ? layerConfig.sFDat_name : "");
		}

		public int getLineConfig_Flag(string sLineTableName)
		{
			LineConfig lineConfig = this.LineConfigItem(sLineTableName);
			return (lineConfig != null ? lineConfig.iFlag : 0);
		}

		public int getLineConfig_Group(string sLineTableName)
		{
			LineConfig lineConfig = this.LineConfigItem(sLineTableName);
			return (lineConfig != null ? lineConfig.iGroup : 0);
		}

		public int getLineConfig_HeightFlag(string sLineTableName)
		{
			int num;
			LayerConfig layerConfig = this.LayerConfigItem(sLineTableName);
			if (layerConfig != null)
			{
				DataSetConfig dataSetConfig = this.DataSetConfigItem(layerConfig.sFDat_name);
				if (dataSetConfig == null)
				{
					num = 0;
				}
				else if (dataSetConfig.sGCTZ == "管顶")
				{
					num = 0;
				}
				else if (dataSetConfig.sGCTZ != "管中")
				{
					num = (2);
				}
				else
				{
					num = 1;
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}

		public string getLineConfig_Kind(string sLineTableName)
		{
			LayerConfig layerConfig = this.LayerConfigItem(sLineTableName);
			return (layerConfig != null ? layerConfig.sGXLB : "");
		}

		public string getLineConfig_NetworkName(string sLineTableName)
		{
			LineConfig lineConfig = this.LineConfigItem(sLineTableName);
			return (lineConfig != null ? lineConfig.sNetworkName : "");
		}

		public int getLineGclx(string sLineTableName, string value)
		{
			int num;
			LayerConfig layerConfig = this.LayerConfigItem(sLineTableName);
			if (layerConfig != null)
			{
				int num1 = 0;
				string str = layerConfig.sGXLB;
				string str1 = str;
				if (str1 != null)
				{
					switch (str1)
					{
						case "给水管线":
						{
							if (value == "直埋")
							{
								num1 = 0;
							}
							else if (value == "架空")
							{
								num1 = 1;
							}
							break;
						}
						case "排水管线":
						{
							if (value == "自留")
							{
								num1 = 1;
							}
							else if (value == "压力")
							{
								num1 = 0;
							}
							break;
						}
						case "燃气管线":
						{
							if (value == "直埋")
							{
								num1 = 0;
							}
							else if (value == "架空")
							{
								num1 = 1;
							}
							break;
						}
						case "工业管线":
						{
							if (value == "直埋")
							{
								num1 = 0;
							}
							else if (value == "架空")
							{
								num1 = 1;
							}
							break;
						}
						case "热力管线":
						{
							if (value == "直埋")
							{
								num1 = 0;
							}
							else if (value == "架空")
							{
								num1 = 1;
							}
							break;
						}
						case "电力管线":
						case "电信管线":
						{
							num1 = 0;
							break;
						}
					}
				}
				num = num1;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		public string GetLineTableFieldName(string sKey)
		{
			string item;
			try
			{
				item = this.m_dicMetaLine[sKey];
			}
			catch (Exception exception)
			{
				item = "";
			}
			return item;
		}

		public string GetPipeLayerNameByAlias(string strAlias)
		{
			int num = strAlias.LastIndexOf(".", StringComparison.Ordinal);
			if (num != -1)
			{
				strAlias = strAlias.Substring(num + 1);
			}
			LayerConfig layerConfig = this.LayerConfigItem(strAlias);
			return (layerConfig != null ? layerConfig.sLayer_name : "");
		}

		public string getPointConfig_Control(string sPointTableName)
		{
			PointConfig pointConfig = this.PointConfigItem(sPointTableName);
			return (pointConfig != null ? pointConfig.LineTableName : "");
		}

		public int getPointConfig_Corlor(string sPointTableName)
		{
			LayerConfig layerConfig = this.LayerConfigItem(sPointTableName);
			return (layerConfig != null ? layerConfig.iColor : 0);
		}

		public int getPointConfig_Flag(string sPointTableName)
		{
			LayerConfig layerConfig = this.LayerConfigItem(sPointTableName);
			return (layerConfig != null ? layerConfig.iYSDM : 0);
		}

		public string getPointConfig_LineTableName(string sPointTableName)
		{
			string sFClsName;
			LayerConfig layerConfig = this.LayerConfigItem(sPointTableName);
			if (layerConfig != null)
			{
				string sFDatName = layerConfig.sFDat_name;
				foreach (LayerConfig value in this.m_dicLayerConfig.Values)
				{
					if ((value.sFDat_name == sFDatName && value.sJHTZ == "Line"))
					{
						sFClsName = value.sFCls_name;
						return sFClsName;
					}
				}
				sFClsName = layerConfig.sFCls_name;
			}
			else
			{
				sFClsName = "";
			}
			return sFClsName;
		}

		public string GetPointTableFieldName(string sKey)
		{
			string item;
			try
			{
				item = this.m_dicMetaPoint[sKey];
			}
			catch (Exception exception)
			{
				item = "";
			}
			return item;
		}

		public string GetTerrainTableFieldName(string sKey)
		{
			string item;
			try
			{
				item = this.m_dicMetaTerrain[sKey];
			}
			catch (Exception exception)
			{
				item = "";
			}
			return item;
		}

		public bool IsPipeLine(string sLayername)
		{
			bool flag;
			int num = sLayername.LastIndexOf(".", StringComparison.Ordinal);
			sLayername = sLayername.Substring(num + 1);
			sLayername = sLayername.ToUpper();
			LayerConfig layerConfig = this.LayerConfigItem(sLayername);
			if (layerConfig == null)
			{
				flag = false;
			}
			else
			{
				flag = (layerConfig.sGXLB != "" && layerConfig.sJHTZ == "Line");
			}
			return flag;
		}

		public bool IsPipePoint(string sLayername)
		{
			bool flag;
			int num = sLayername.LastIndexOf(".", StringComparison.Ordinal);
			sLayername = sLayername.Substring(num + 1);
			sLayername = sLayername.ToUpper();
			LayerConfig layerConfig = this.LayerConfigItem(sLayername);
			if (layerConfig == null)
			{
				flag = false;
			}
			else
			{
				flag = (layerConfig.sGXLB != "" && layerConfig.sJHTZ == "Point");
			}
			return flag;
		}

		public LayerConfig LayerConfigItem(string sTableName)
		{
			LayerConfig item;
			try
			{
				int num = sTableName.LastIndexOf(".", StringComparison.Ordinal);
				if (num != -1)
				{
					sTableName = sTableName.Substring(num + 1);
				}
				item = this.m_dicLayerConfig[sTableName.ToUpper()];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public LayerConfig LayerConfigItem(int idx)
		{
			LayerConfig layerConfig;
			LayerConfig layerConfig1;
			try
			{
				if ((idx >= 0 && idx < this.m_dicLayerConfig.Count))
				{
					Dictionary<string, LayerConfig>.ValueCollection values = this.m_dicLayerConfig.Values;
					int num = 0;
					foreach (LayerConfig value in values)
					{
						if (num != idx)
						{
							num++;
						}
						else
						{
							layerConfig = value;
							layerConfig1 = layerConfig;
							return layerConfig1;
						}
					}
					layerConfig = null;
				}
				else
				{
					layerConfig = null;
				}
			}
			catch (Exception exception)
			{
				layerConfig = null;
			}
			layerConfig1 = layerConfig;
			return layerConfig1;
		}

		public LineConfig LineConfigItem(string sLineTableName)
		{
			LineConfig item;
			try
			{
				item = this.m_dicLineConfig[sLineTableName];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public PointConfig PointConfigItem(string sPointTableName)
		{
			PointConfig item;
			try
			{
				item = this.m_dicPointConfig[sPointTableName];
			}
			catch (Exception exception)
			{
				item = null;
			}
			return item;
		}

		public bool ReadCodeFile(string sFile)
		{
			bool flag;
			this.m_dicCode.Clear();
			for (int i = 0; i < 10; i++)
			{
				if (this.m_dicCodeTab[i] != null)
				{
					this.m_dicCodeTab[i].Clear();
				}
				else
				{
					this.m_dicCodeTab[i] = new SortedList<string, CodeItem>();
				}
			}
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(sFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("dataroot");
				if (elementsByTagName.Count >= 1)
				{
					for (int j = 0; j < elementsByTagName.Count; j++)
					{
						XmlNodeList childNodes = elementsByTagName[j].ChildNodes;
						for (int k = 0; k < childNodes.Count; k++)
						{
							XmlNode itemOf = childNodes[k];
							if ("公共管点" != itemOf.Name)
							{
								XmlNodeList xmlNodeLists = itemOf.ChildNodes;
								for (int l = 0; l < xmlNodeLists.Count; l++)
								{
									XmlNodeList childNodes1 = xmlNodeLists[l].ChildNodes;
									CodeItem codeItem = new CodeItem();
									codeItem.InitFromXML(ref childNodes1);
									if ((codeItem.CodeName.Length > 0 && !this.m_dicCode.ContainsKey(codeItem.Code)))
									{
										this.m_dicCode.Add(codeItem.Code, codeItem);
										this.AddToCodeTab(codeItem);
									}
								}
							}
							else
							{
								XmlNodeList xmlNodeLists1 = itemOf.ChildNodes;
								for (int m = 0; m < xmlNodeLists1.Count; m++)
								{
									XmlNodeList childNodes2 = xmlNodeLists1[m].ChildNodes;
									CodeItem codeItem1 = new CodeItem();
									codeItem1.InitFromXML(ref childNodes2);
									if ((codeItem1.CodeName.Length > 0 && !this.m_dicCode.ContainsKey(codeItem1.Code)))
									{
										this.m_dicCode.Add(codeItem1.Code, codeItem1);
										this.AddToCodeTab(codeItem1);
									}
								}
							}
						}
					}
				}
				else
				{
					flag = false;
					return flag;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadConfigFile(string sPath)
		{
			this.ReadCodeFile(string.Concat(sPath, "CodingTable.xml"));
			this.ReadMetaLineTableFile(string.Concat(sPath, "Pipe_Line_Attr.xml"));
			this.ReadMetaPointTableFile(string.Concat(sPath, "Pipe_Point_Attr.xml"));
			this.ReadLineTableConfigXML(string.Concat(sPath, "Pipe_Line.xml"));
			this.ReadPointTableConfigXML(string.Concat(sPath, "Pipe_Point.xml"));
			this.ReadLayerInfoFromFile(string.Concat(sPath, "EMLayerInfo.xml"));
			this.ReadDataSetInfoFromFile(string.Concat(sPath, "EmDatasetInfo.xml"));
			return true;
		}

		public bool ReadDataSetInfo(ITable pTable)
		{
			bool flag;
			this.m_dicDatasetConfig.Clear();
			try
			{
				ICursor cursor = pTable.Search(new QueryFilterClass(), false);
				for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
				{
					DataSetConfig dataSetConfig = new DataSetConfig();
					dataSetConfig.InitFromRow(i);
					if ((dataSetConfig.strDatasetName.Length > 0 && !this.m_dicDatasetConfig.ContainsKey(dataSetConfig.strDatasetName.ToUpper())))
					{
						this.m_dicDatasetConfig.Add(dataSetConfig.strDatasetName.ToUpper(), dataSetConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadDataSetInfoFromFile(string szFile)
		{
			bool flag;
			this.m_dicDatasetConfig.Clear();
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(szFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("EMDataSetInfo");
				for (int i = 0; i < elementsByTagName.Count; i++)
				{
					XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
					DataSetConfig dataSetConfig = new DataSetConfig();
					dataSetConfig.InitFromXML(ref childNodes);
					if ((dataSetConfig.strDatasetName.Length > 0 && !this.m_dicDatasetConfig.ContainsKey(dataSetConfig.strDatasetName.ToUpper())))
					{
						this.m_dicDatasetConfig.Add(dataSetConfig.strDatasetName.ToUpper(), dataSetConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadFromDatabase(IFeatureWorkspace pFWks)
		{
			bool flag;
			if (pFWks != null)
			{
				this.ReadLayerInfo(pFWks.OpenTable("EMFEATURECLASSINFO"));
				this.ReadDataSetInfo(pFWks.OpenTable("EMFEATUREDATASETINFO"));
				IWorkspace2 workspace2 = (IWorkspace2)pFWks;
				if (workspace2.NameExists[esriDatasetType.esriDTTable, "EMPIPELINEATTR"])
				{
					this.ReadMetaLineTableFile(pFWks.OpenTable("EMPIPELINEATTR"));
				}
				if (workspace2.NameExists[esriDatasetType.esriDTTable, "EMPIPEPOINTATTR"])
				{
					this.ReadMetaPointTableFile(pFWks.OpenTable("EMPIPEPOINTATTR"));
				}
				if (workspace2.NameExists[esriDatasetType.esriDTTable, "EMTERRAINATTR"])
				{
					this.ReadMetaTerrainTableFile(pFWks.OpenTable("EMTERRAINATTR"));
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool ReadLayerInfo(ITable pTable)
		{
			bool flag;
			this.m_dicLayerConfig.Clear();
			try
			{
				ICursor cursor = pTable.Search(new QueryFilterClass(), false);
				for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
				{
					LayerConfig layerConfig = new LayerConfig();
					layerConfig.InitFromRow(i);
					if ((layerConfig.sFCls_name.Length > 0 && !this.m_dicLayerConfig.ContainsKey(layerConfig.sFCls_name.ToUpper())))
					{
						this.m_dicLayerConfig.Add(layerConfig.sFCls_name.ToUpper(), layerConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadLayerInfoFromFile(string szFile)
		{
			bool flag;
			this.m_dicLayerConfig.Clear();
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(szFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("EMLayerInfo");
				for (int i = 0; i < elementsByTagName.Count; i++)
				{
					XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
					LayerConfig layerConfig = new LayerConfig();
					layerConfig.InitFromXML(ref childNodes);
					if ((layerConfig.sFCls_name.Length > 0 && !this.m_dicLayerConfig.ContainsKey(layerConfig.sFCls_name.ToUpper())))
					{
						this.m_dicLayerConfig.Add(layerConfig.sFCls_name.ToUpper(), layerConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadLineTableConfig(ITable pTable)
		{
			bool flag;
			this.m_dicLineConfig.Clear();
			try
			{
				ICursor cursor = pTable.Search(new QueryFilterClass(), false);
				for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
				{
					LineConfig lineConfig = new LineConfig();
					lineConfig.InitFromRow(i);
					if ((lineConfig.sLineTableName.Length > 0 && !this.m_dicLineConfig.ContainsKey(lineConfig.sLineTableName)))
					{
						this.m_dicLineConfig.Add(lineConfig.sLineTableName, lineConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadLineTableConfigXML(string sFile)
		{
			bool flag;
			this.m_dicLineConfig.Clear();
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(sFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Pipe_Line");
				for (int i = 0; i < elementsByTagName.Count; i++)
				{
					XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
					LineConfig lineConfig = new LineConfig();
					lineConfig.InitFromXML(ref childNodes);
					if ((lineConfig.sLineTableName.Length > 0 && !this.m_dicLineConfig.ContainsKey(lineConfig.sLineTableName)))
					{
						this.m_dicLineConfig.Add(lineConfig.sLineTableName, lineConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadMetaLineTableFile(string szFile)
		{
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(szFile);
				XmlNodeList childNodes = xmlDocument.GetElementsByTagName("Pipe_Attr")[0].ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					if (!this.m_dicMetaLine.ContainsKey(childNodes[i].Name))
					{
						this.m_dicMetaLine.Add(childNodes[i].Name, childNodes[i].InnerText);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadMetaLineTableFile(ITable pTable)
		{
			bool flag;
			try
			{
				int fieldCount = pTable.Fields.FieldCount;
				IRow row = pTable.Search(new QueryFilterClass(), false).NextRow();
				if (row != null)
				{
					for (int i = 1; i < fieldCount; i++)
					{
						string name = row.Fields.Field[i].Name;
						if (!this.m_dicMetaLine.ContainsKey(name))
						{
							this.m_dicMetaLine.Add(name, row.Value[i].ToString());
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadMetaPointTableFile(string szFile)
		{
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(szFile);
				XmlNodeList childNodes = xmlDocument.GetElementsByTagName("Pipe_Attr")[0].ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					if (!this.m_dicMetaPoint.ContainsKey(childNodes[i].Name))
					{
						this.m_dicMetaPoint.Add(childNodes[i].Name, childNodes[i].InnerText);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadMetaPointTableFile(ITable pTable)
		{
			bool flag;
			try
			{
				int fieldCount = pTable.Fields.FieldCount;
				IRow row = pTable.Search(new QueryFilterClass(), false).NextRow();
				if (row != null)
				{
					for (int i = 1; i < fieldCount; i++)
					{
						string name = row.Fields.Field[i].Name;
						if (!this.m_dicMetaPoint.ContainsKey(name))
						{
							this.m_dicMetaPoint.Add(name, row.Value[i].ToString());
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadMetaTerrainTableFile(ITable pTable)
		{
			bool flag;
			try
			{
				int fieldCount = pTable.Fields.FieldCount;
				IRow row = pTable.Search(new QueryFilterClass(), false).NextRow();
				if (row != null)
				{
					for (int i = 1; i < fieldCount; i++)
					{
						string name = row.Fields.Field[i].Name;
						if (!this.m_dicMetaTerrain.ContainsKey(name))
						{
							this.m_dicMetaTerrain.Add(name, row.Value[i].ToString());
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadPointTableConfig(ITable pTable)
		{
			bool flag;
			this.m_dicPointConfig.Clear();
			try
			{
				ICursor cursor = pTable.Search(new QueryFilterClass(), false);
				for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
				{
					PointConfig pointConfig = new PointConfig();
					pointConfig.InitFromRow(i);
					if ((pointConfig.PointTableName.Length > 0 && !this.m_dicPointConfig.ContainsKey(pointConfig.PointTableName)))
					{
						this.m_dicPointConfig.Add(pointConfig.PointTableName, pointConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}

		public bool ReadPointTableConfigXML(string sFile)
		{
			bool flag;
			this.m_dicPointConfig.Clear();
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(sFile);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Pipe_Point");
				for (int i = 0; i < elementsByTagName.Count; i++)
				{
					XmlNodeList childNodes = elementsByTagName[i].ChildNodes;
					PointConfig pointConfig = new PointConfig();
					pointConfig.InitFromXML(ref childNodes);
					if ((pointConfig.PointTableName.Length > 0 && !this.m_dicPointConfig.ContainsKey(pointConfig.PointTableName)))
					{
						this.m_dicPointConfig.Add(pointConfig.PointTableName, pointConfig);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
				return flag;
			}
			flag = true;
			return flag;
		}
	}
}