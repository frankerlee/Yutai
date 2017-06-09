using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.UI;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ShowMapFrameProperty : BaseCommand
    {
        private IYTHookHelper _hookHelper;

        public ShowMapFrameProperty()
        {
            base.m_caption = "数据框属性";
            base.m_name = "MapFrameProperty";
        }

        public override void OnClick()
        {
            frmDataFrameProperty property = new frmDataFrameProperty {
                FocusMap = this._hookHelper.FocusMap as IBasicMap
            };
            if (property.ShowDialog() != DialogResult.OK)
            {
            }
        }

        public override void OnCreate(object object_0)
        {
            this._hookHelper.Hook = object_0;
        }
    }
}

