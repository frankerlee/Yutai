using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class InsetTemplateElement : BaseCommand, ICommandSubType
    {
        private ElementType elementType_0 = ElementType.ConstantText;
        private IYTHookHelper _hookHelper;

        public int GetCount()
        {
            return 10;
        }

        public override void OnClick()
        {
            ElementChangeEvent.InsertElement(this.elementType_0);
        }

        public override void OnCreate(object object_0)
        {
        }

        public void SetSubType(int int_0)
        {
            this.elementType_0 = int_0;
            switch (this.elementType_0)
            {
                case ElementType.ConstantText:
                    base.m_name = "InsetTemplateElement.Constant";
                    base.m_caption = "固定文本";
                    break;

                case ElementType.SingleText:
                    base.m_name = "InsetTemplateElement.SingleText";
                    base.m_caption = "单行文本";
                    break;

                case ElementType.MultiText:
                    base.m_name = "InsetTemplateElement.MultiText";
                    base.m_caption = "多行文本";
                    break;

                case ElementType.JoinTable:
                    base.m_name = "InsetTemplateElement.JoinTable";
                    base.m_caption = "接图表";
                    break;

                case ElementType.ScaleText:
                    base.m_name = "InsetTemplateElement.ScaleText";
                    base.m_caption = "比例尺文本";
                    break;

                case ElementType.CustomLegend:
                    base.m_name = "InsetTemplateElement.CustomLegend";
                    base.m_caption = "自定义图例";
                    break;

                case ElementType.Legend:
                    base.m_name = "InsetTemplateElement.Legend";
                    base.m_caption = "图例";
                    break;

                case ElementType.Picture:
                    base.m_name = "InsetTemplateElement.Picture";
                    base.m_caption = "位图";
                    break;

                case ElementType.OLE:
                    base.m_name = "InsetTemplateElement.OLE";
                    base.m_caption = "OLE";
                    break;

                case ElementType.ScaleBar:
                    base.m_name = "InsetTemplateElement.ScaleBar";
                    base.m_caption = "比例尺";
                    break;

                case ElementType.North:
                    base.m_name = "InsetTemplateElement.North";
                    base.m_caption = "指北针";
                    break;
            }
        }
    }
}

