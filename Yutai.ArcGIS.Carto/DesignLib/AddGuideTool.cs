using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class AddGuideTool : BaseTool
    {
        private IYTHookHelper _hookHelper;

        public AddGuideTool()
        {
            base.m_caption = "设置导航线";
            base.m_name = "AddGuideTool";
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper= object_0 as IYTHookHelper;
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IPoint point = null;
            point = this._hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            if (int_2 < int_3)
            {
                this._hookHelper.PageLayout.HorizontalSnapGuides.AddGuide(point.Y);
            }
            else
            {
                this._hookHelper.PageLayout.VerticalSnapGuides.AddGuide(point.X);
            }
            this._hookHelper.ActiveView.Refresh();
        }
    }
}

