// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IMultiCheQiConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/20  10:55
// 更新时间 :  2017/07/20  10:55

using System.Collections.Generic;
using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface IMultiCheQiConfig
    {
        List<IFeatureLayer> FlagLayerList { get; }
        IFeatureLayer FlagLineLayer { get; }
        IFeatureLayer FlagAnnoLayer { get; }
        List<IFieldSetting> FieldSettingList { get; }
        IFontConfig HeaderFontConfig { get; }
        IFontConfig ContentFontConfig { get; }
    }
}