using System.Collections.Generic;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class EditTemplateManager
    {
        public static event OnAddMoreTemplateHandler OnAddMoreTemplate;

        public static event OnAddTemplateHandler OnAddTemplate;

        public static event OnDeleteMoreTemplateHandler OnDeleteMoreTemplate;

        public static event OnDeleteTemplateHandler OnDeleteTemplate;

        public static event OnTemplatePropertyChangeHandler OnTemplatePropertyChange;

        public static void AddEditTemplate(YTEditTemplate template)
        {
            if (OnAddTemplate != null)
            {
                OnAddTemplate(template);
            }
        }

        public static void AddMoreEditTemplate(List<YTEditTemplate> template)
        {
            if (OnAddMoreTemplate != null)
            {
                OnAddMoreTemplate(template);
            }
        }

        public static void DeleteEditTemplate(YTEditTemplate template)
        {
            if (OnDeleteTemplate != null)
            {
                OnDeleteTemplate(template);
            }
        }

        public static void DeleteMoreEditTemplate(List<YTEditTemplate> template)
        {
            if (OnDeleteMoreTemplate != null)
            {
                OnDeleteMoreTemplate(template);
            }
        }

        public static void TemplatePropertyChange(YTEditTemplate template)
        {
            if (OnTemplatePropertyChange != null)
            {
                OnTemplatePropertyChange(template);
            }
        }
    }
}