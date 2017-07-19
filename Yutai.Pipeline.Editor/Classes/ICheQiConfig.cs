// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ICheQiConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/11  15:42
// 更新时间 :  2017/07/11  15:42

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface ICheQiConfig
    {
        IFeatureLayer FlagLayer { get; }
        IFeatureLayer FlagLineLayer { get; }
        IFeatureLayer FlagAnnoLayer { get; }
        string Expression { get; }

        string FontName { get; }
        decimal FontSize { get; }
        bool Italic { get; }
        bool Underline { get; }
        bool Bold { get; }
        bool Strikethrough { get; }
        IRgbColor FontColor { get; }
    }
}