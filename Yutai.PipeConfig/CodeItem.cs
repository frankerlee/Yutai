using System;
using System.Xml;

namespace Yutai.PipeConfig
{
	public class CodeItem
	{
		public string CodeName;

		public int Code;

		public int CodeSize;

		public int CodeKind;

		public CodeItem()
		{
			this.CodeName = "";
			this.Code = 0;
			this.CodeSize = 0;
			this.CodeKind = 0;
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
						this.CodeName = ValueList[i].InnerText;
					}
					else if ("编码" == name)
					{
						this.Code = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("规格" == name)
					{
						this.CodeSize = Convert.ToInt32(ValueList[i].InnerText);
					}
					else if ("分类" == name)
					{
						this.CodeKind = Convert.ToInt32(ValueList[i].InnerText);
					}
				}
			}
			catch (Exception exception)
			{
			}
		}
	}
}