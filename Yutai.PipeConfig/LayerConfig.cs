using System;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.PipeConfig
{
	public class LayerConfig
	{
		public string sFCls_name;

		public string sLayer_name;

		public string sFDat_name;

		public int iYSDM;

		public string sYSDH;

		public string sSSZL;

		public string sSSDL;

		public string sSSLB;

		public string sGXLB;

		public string sJHTZ;

		public int iColor;

		public bool bVisible;

		public bool bSelect;

		public double iMaxScale;

		public double iMinScale;

		public LayerConfig()
		{
		}

		public void InitFromRow(IRow pRow)
		{
			try
			{
				int num = pRow.Fields.FindField("要素类");
				if (num >= 0)
				{
					this.sFCls_name = pRow.Value[num].ToString().ToUpper();
				}
				num = pRow.Fields.FindField("层名");
				if (num >= 0)
				{
					this.sLayer_name = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("数据集名称");
				if (num >= 0)
				{
					this.sFDat_name = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("要素代码");
				if (num >= 0)
				{
					string str = pRow.Value[num].ToString();
					if (str != "")
					{
						this.iYSDM = Convert.ToInt32(str);
					}
					else
					{
						this.iYSDM = 0;
					}
				}
				num = pRow.Fields.FindField("要素代号");
				if (num >= 0)
				{
					this.sYSDH = pRow.Value[num].ToString();
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
				num = pRow.Fields.FindField("几何特征");
				if (num >= 0)
				{
					this.sJHTZ = pRow.Value[num].ToString();
				}
				num = pRow.Fields.FindField("颜色");
				if (num >= 0)
				{
					if (pRow.Value[num].ToString() != "")
					{
						this.iColor = Convert.ToInt32(pRow.Value[num].ToString());
					}
					else
					{
						this.iColor = 0;
					}
				}
				num = pRow.Fields.FindField("可见");
				if (num >= 0)
				{
					if (pRow.Value[num].ToString() != "1")
					{
						this.bVisible = false;
					}
					else
					{
						this.bVisible = true;
					}
				}
				num = pRow.Fields.FindField("可选");
				if (num >= 0)
				{
					if (pRow.Value[num].ToString() != "1")
					{
						this.bSelect = false;
					}
					else
					{
						this.bSelect = true;
					}
				}
				num = pRow.Fields.FindField("最大显示比例");
				if (num >= 0)
				{
					if (pRow.Value[num].ToString() != "")
					{
						this.iMaxScale = Convert.ToDouble(pRow.Value[num].ToString());
					}
					else
					{
						this.iMaxScale = 0;
					}
				}
				num = pRow.Fields.FindField("最小显示比例");
				if (num >= 0)
				{
					if (pRow.Value[num].ToString() != "")
					{
						this.iMinScale = Convert.ToDouble(pRow.Value[num].ToString());
					}
					else
					{
						this.iMinScale = 0;
					}
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
					if ("要素类" == name)
					{
						this.sFCls_name = ValueList[i].InnerText.ToUpper();
					}
					else if ("层名" == name)
					{
						this.sLayer_name = ValueList[i].InnerText;
					}
					else if ("数据集名" == name)
					{
						this.sFDat_name = ValueList[i].InnerText;
					}
					else if ("要素代码" == name)
					{
						this.iYSDM = Convert.ToUInt16(ValueList[i].InnerText);
					}
					else if ("要素代号" == name)
					{
						this.sYSDH = ValueList[i].InnerText;
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
					else if ("几何特征" == name)
					{
						this.sJHTZ = ValueList[i].InnerText;
					}
					else if ("颜色" == name)
					{
						string innerText = ValueList[i].InnerText;
						if (innerText != "")
						{
							this.iColor = Convert.ToInt32(innerText);
						}
						else
						{
							this.iColor = 0;
						}
					}
					else if ("可见" == name)
					{
						if (ValueList[i].InnerText != "1")
						{
							this.bVisible = false;
						}
						else
						{
							this.bVisible = true;
						}
					}
					else if ("可选" == name)
					{
						if (ValueList[i].InnerText != "1")
						{
							this.bSelect = false;
						}
						else
						{
							this.bSelect = true;
						}
					}
					else if ("最大显示比例" == name)
					{
						string str = ValueList[i].InnerText;
						if (str != "")
						{
							this.iMaxScale = Convert.ToDouble(str);
						}
						else
						{
							this.iMaxScale = 0;
						}
					}
					else if ("最小显示比例" == name)
					{
						string innerText1 = ValueList[i].InnerText;
						if (innerText1 != "")
						{
							this.iMinScale = Convert.ToDouble(innerText1);
						}
						else
						{
							this.iMinScale = 0;
						}
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