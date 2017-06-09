using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class LoadTemplate : BaseCommand
    {
        private IYTHookHelper _hookHelper;

        public LoadTemplate()
        {
            base.m_caption = "装载模板";
            base.m_name = "LoadTemplate";
        }

        public override void OnClick()
        {
            if (this._hookHelper.PageLayout != null)
            {
                double num;
                double num2;
                IMapFrame frame = (this._hookHelper.PageLayout as IGraphicsContainer).FindFrame(this._hookHelper.FocusMap) as IMapFrame;
                IMapDocument document = new MapDocumentClass();
                IGraphicsContainer pageLayout = document.PageLayout as IGraphicsContainer;
                pageLayout.Reset();
                IElement element = pageLayout.Next();
                document.PageLayout.Page.QuerySize(out num, out num2);
                this._hookHelper.PageLayout.Page.PutCustomSize(num, num2);
                while (element != null)
                {
                    if (element is IMapFrame)
                    {
                        esriUnits mapUnits = (element as IMapFrame).Map.MapUnits;
                        frame.Map.SpatialReferenceLocked = false;
                        frame.Map.SpatialReference = (element as IMapFrame).Map.SpatialReference;
                        frame.Map.DistanceUnits = (element as IMapFrame).Map.DistanceUnits;
                        frame.Map.MapUnits = (element as IMapFrame).Map.MapUnits;
                        (frame as IElement).Geometry = element.Geometry;
                        if ((element as IMapFrame).ExtentType == esriExtentTypeEnum.esriExtentBounds)
                        {
                            frame.ExtentType = esriExtentTypeEnum.esriExtentBounds;
                            frame.MapBounds = (element as IMapFrame).MapBounds;
                        }
                        else if ((element as IMapFrame).ExtentType == esriExtentTypeEnum.esriExtentScale)
                        {
                            frame.ExtentType = esriExtentTypeEnum.esriExtentScale;
                            frame.MapScale = (element as IMapFrame).MapScale;
                        }
                    }
                    else
                    {
                        (this._hookHelper.PageLayout as IGraphicsContainer).AddElement(element, -1);
                    }
                    element = pageLayout.Next();
                }
            }
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }
    }
}

