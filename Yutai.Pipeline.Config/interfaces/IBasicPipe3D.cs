// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IBasicPipe3D.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  15:41
// 更新时间 :  2017/06/22  15:41

using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicPipe3D
    {
        string TypeName { get; set; }
        IYTField NoField { get; set; }            // 编号字段
        IYTField MDateField { get; set; }         // 入库日期
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}