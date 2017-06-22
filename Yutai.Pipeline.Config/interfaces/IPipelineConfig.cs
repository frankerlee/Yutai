// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IPipelineConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  17:32
// 更新时间 :  2017/06/22  17:32

using System.Collections.Generic;

namespace Yutai.Pipeline.Config.interfaces
{
    public interface IPipelineConfig
    {
        List<IPipelineTemplate> Templates { get; set; }
        List<IPipelineLayer> Layers { get; set; }
        void LoadFromXml(string fileName);
    }
}