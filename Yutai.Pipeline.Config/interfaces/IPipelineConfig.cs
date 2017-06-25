// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IPipelineConfig.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/22  17:32
// 更新时间 :  2017/06/22  17:32

using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IPipelineConfig
    {
        string ConfigDatabaseName { get; set; }

        IFeatureWorkspace Workspace { get; set; }
        List<IPipelineTemplate> Templates { get; set; }
        List<IPipelineLayer> Layers { get; set; }
        void LoadFromXml(string fileName);

        void SaveToXml(string fileName);

        string XmlFile { get; set; }

        void Save();

        //! 通过依据数据库配置对地图内图层的自动识别创建有关配置
        void OrganizeMap(IMap pMap);

        //! 在读取配置文件后，需要通过链接Map来读取对应的图层，并关联有关事件
        bool LinkMap(IMap pMap);

        //! 依据名字来判断是否为符合要求的管线图层
        bool IsPipelineLayer(string classAliasName);
        bool IsPipelineLayer(IFeatureClass pClass);
        bool IsPipelineLayer(string classAliasName, enumPipelineDataType dataType);
        //! 依据FeatureLayer获取BaiscLayerInfo对象
        IBasicLayerInfo GetBasicLayerInfo(IFeatureClass pClass);

        IBasicLayerInfo GetBasicLayerInfo(string pClassAliasName);

        IYTField GetSpecialField(string classAliasName, string typeWord);
    }
}