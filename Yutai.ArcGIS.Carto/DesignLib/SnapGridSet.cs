using ESRI.ArcGIS.ADF.BaseClasses;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class SnapGridSet : BaseCommand
    {
        private IYTHookHelper _hookHelper;

        public SnapGridSet()
        {
            base.m_caption = "捕捉格网设置";
            base.m_name = "SnapGridSet";
        }

        public override void OnClick()
        {
            if (this._hookHelper.PageLayout != null)
            {
                this._hookHelper.PageLayout.SnapGrid.IsVisible = true;
                this._hookHelper.PageLayout.RulerSettings.SmallestDivision = 2.0;
            }
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }
    }
}

