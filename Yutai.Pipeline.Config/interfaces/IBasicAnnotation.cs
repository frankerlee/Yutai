// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IBasicAnnotation.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  16:46
// 更新时间 :  2017/06/22  16:46

using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicAnnotation
    {
        string TypeName { get; set; }
        IYTField CodeField { get; set; }          // 要素代码
        IYTField XField { get; set; }             // X坐标
        IYTField YField { get; set; }             // Y坐标
        IYTField AngleField { get; set; }         // 标注的角度
        IYTField TextStringField { get; set; }    // 标注的内容
        IYTField RemarkField { get; set; }        // 备注
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}