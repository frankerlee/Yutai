// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IPipelineTemplate.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  17:33
// 更新时间 :  2017/06/22  17:33

using System.Collections.Generic;
using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IPipelineTemplate
    {
        string Name { get; set; }
        string Caption { get; set; }
        enumPipelineDataType DataType { get; set; }
        List<IYTField> Fields { get; set; }
        void ReadFromXml(XmlNode xmlNode);
        XmlNode ToXml(XmlDocument doc);
    }
}