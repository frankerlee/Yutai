using System;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public class DataSetConfig
	{
		public string strName;

		public string strDatasetName;

		public int iYCDM;

		public string sYCDH;

		public string sSSZL;

		public string sSSDL;

		public string sSSLB;

		public string sGXLB;

		public string sGCTZ;

		public DataSetConfig()
		{
		}

		public void InitFromRow(IRow pRow)
		{
			try
			{
				int num = pRow.Fields.FindField("名称");
				if (num >= 0)
				{
					this.strName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("数据集");
				if (num >= 0)
				{
					this.strDatasetName = pRow.Value[num].ToString().ToUpper();
				}
				num = pRow.Fields.FindField("要素代码");
				if (num >= 0)
				{
					this.iYCDM = Convert.ToInt32(pRow.Value[num].ToString());
				}
				num = pRow.Fields.FindField("要素代号");
				if (num >= 0)
				{
					this.sYCDH = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("所属中类");
				if (num >= 0)
				{
					this.sSSZL = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("所属大类");
				if (num >= 0)
				{
					this.sSSDL = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("所属种类");
				if (num >= 0)
				{
					this.sSSLB = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("管线类别");
				if (num >= 0)
				{
					this.sGXLB = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("高程特征");
				if (num >= 0)
				{
					this.sGCTZ = pRow.Value[num].ToString();
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
					if ("名称" == name)
					{
						this.strName = ValueList[i].InnerText;
					}
					else if ("数据集" == name)
					{
						this.strDatasetName = ValueList[i].InnerText;
					}
					else if ("要素代码" == name)
					{
						this.iYCDM = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("要素代号" == name)
					{
						this.sYCDH = ValueList[i].InnerText;
					}
					else if ("所属中类" == name)
					{
						this.sSSZL = ValueList[i].InnerText;
					}
					else if ("所属大类" == name)
					{
						this.sSSDL = ValueList[i].InnerText;
					}
					else if ("所属种类" == name)
					{
						this.sSSLB = ValueList[i].InnerText;
					}
					else if ("管线类别" == name)
					{
						this.sGXLB = ValueList[i].InnerText;
					}
					else if ("高程特征" == name)
					{
						this.sGCTZ = ValueList[i].InnerText;
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