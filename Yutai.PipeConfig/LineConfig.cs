using System;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public class LineConfig
	{
		public string sKind;

		public string sFDatasetName;

		public string sNetworkName;

		public string sLineTableName;

		public string sDM;

		public int iFlag;

		public int iGroup;

		public int iHeightFlag;

		public int iCorlor;

		public LineConfig()
		{
			this.sKind = "";
			this.sFDatasetName = "";
			this.sNetworkName = "";
			this.sLineTableName = "";
			this.sDM = "";
			this.iFlag = 0;
			this.iGroup = 0;
			this.iHeightFlag = 0;
			this.iCorlor = 0;
		}

		public void InitFromRow(IRow pRow)
		{
			try
			{
				int num = pRow.Fields.FindField("线表");
				if (num >= 0)
				{
					this.sLineTableName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("管线性质");
				if (num >= 0)
				{
					this.sKind = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("FeatureDataset");
				if (num >= 0)
				{
					this.sFDatasetName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("NetWork_Name");
				if (num >= 0)
				{
					this.sNetworkName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("代码");
				if (num >= 0)
				{
					this.sDM = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("类型");
				if (num >= 0)
				{
					this.iFlag = Convert.ToInt32(pRow.Value[num]);
				}
				num = pRow.Fields.FindField("分组");
				if (num >= 0)
				{
					this.iGroup = Convert.ToInt32(pRow.Value[num]);
				}
				num = pRow.Fields.FindField("高程特征");
				if (num >= 0)
				{
					this.iHeightFlag = Convert.ToInt32(pRow.Value[num]);
				}
				num = pRow.Fields.FindField("颜色");
				if (num >= 0)
				{
					this.iCorlor = Convert.ToInt32(pRow.Value[num]);
				}
			}
			catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
		}

		public void InitFromXML(ref XmlNodeList ValueList)
		{
			try
			{
				for (int i = 0; i < ValueList.Count; i++)
				{
					string name = ValueList[i].Name;
					if ("线表" == name)
					{
						this.sLineTableName = ValueList[i].InnerText;
					}
					else if ("管线性质" == name)
					{
						this.sKind = ValueList[i].InnerText;
					}
					else if ("FeatureDataset" == name)
					{
						this.sFDatasetName = ValueList[i].InnerText;
					}
					else if ("NetWork_Name" == name)
					{
						this.sNetworkName = ValueList[i].InnerText;
					}
					else if ("代码" == name)
					{
						this.sDM = ValueList[i].InnerText;
					}
					else if ("类型" == name)
					{
						this.iFlag = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("分组" == name)
					{
						this.iGroup = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("高程特征" == name)
					{
						this.iHeightFlag = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("颜色" == name)
					{
						this.iCorlor = Convert.ToInt32(ValueList[i].InnerText);
					}
				}
			}
			catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
		}
	}
}