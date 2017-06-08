using ESRI.ArcGIS.ADF.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class AddElementCommand : BaseCommand
    {
        public AddElementCommand()
        {
            base.m_caption = "添加元素";
            base.m_name = "AddElementCommand";
        }

        public override void OnClick()
        {
            MapTemplateAssis.ExcuteAddElement();
        }

        public override void OnCreate(object object_0)
        {
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
        }
    }
}

