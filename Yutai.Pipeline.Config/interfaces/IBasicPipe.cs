// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IBasicPipe.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  15:40
// 更新时间 :  2017/06/22  15:40

using System.Xml;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicPipe
    {
        IBasicPipePoint BasicPipePoint { get; set; }
        IBasicPipeLine BasicPipeLine { get; set; }
        IBasicPipeNetwork BasicPipeNetwork { get; set; }
        IBasicPipe3D BasicPipe3D { get; set; }
        void ReadFromXml(XmlNode xml);
        XmlNode ToXml(XmlDocument doc);
    }
}