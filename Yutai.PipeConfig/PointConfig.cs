using System;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public class PointConfig
	{
		public string PointTableName;

		public string LineTableName;

		public int iCorlor;

		public int bFlag;

		public string sControl;

		public PointConfig()
		{
			this.PointTableName = "";
			this.LineTableName = "";
			this.iCorlor = 0;
			this.bFlag = 0;
			this.sControl = "";
		}

		public void InitFromRow(IRow pRow)
		{
			try
			{
				int num = pRow.Fields.FindField("点表");
				if (num >= 0)
				{
					this.PointTableName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("线表");
				if (num >= 0)
				{
					this.LineTableName = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("颜色");
				if (num >= 0)
				{
					this.iCorlor = Convert.ToInt32(pRow.Value[num]);
				}
				num = pRow.Fields.FindField("标志");
				if (num >= 0)
				{
					this.bFlag = Convert.ToInt32(pRow.Value[num]);
				}
				num = pRow.Fields.FindField("控制值");
				if (num >= 0)
				{
					this.sControl = pRow.Value[num].ToString();
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
					if ("点表" == name)
					{
						this.PointTableName = ValueList[i].InnerText;
					}
					else if ("线表" == name)
					{
						this.LineTableName = ValueList[i].InnerText;
					}
					else if ("颜色" == name)
					{
						this.iCorlor = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("标志" == name)
					{
						this.bFlag = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("控制值" == name)
					{
						this.sControl = ValueList[i].InnerText;
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