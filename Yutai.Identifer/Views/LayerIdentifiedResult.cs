using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Identifer.Enums;

namespace Yutai.Plugins.Identifer.Views
{
    public class LayerIdentifiedResult
    {
        private ILayer identifyLayer;
        private LayerFeatureType geometryType;
        private List<IFeatureIdentifyObj> identifiedFeatureObjList;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LayerIdentifiedResult()
        {
            //新建结果列表对象
            identifiedFeatureObjList = new List<IFeatureIdentifyObj>();
        }

        /// <summary>
        /// 图层要素的集合类型
        /// </summary>
        public LayerFeatureType GeometryType
        {
            get { return geometryType; }
            set { geometryType = value; }
        }

        /// <summary>
        /// 查询的对象图层
        /// </summary>
        public ILayer IdentifyLayer
        {
            get { return identifyLayer; }
            set { identifyLayer = value; }
        }

        /// <summary>
        /// 此图层的要素查询列表
        /// </summary>
        public List<IFeatureIdentifyObj> IdentifiedFeatureObjList
        {
            get { return identifiedFeatureObjList; }
        }
    }
}