using System.Collections.Generic;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class EditTemplateManager
    {
        public static  event OnAddMoreTemplateHandler OnAddMoreTemplate;

        public static  event OnAddTemplateHandler OnAddTemplate;

        public static  event OnDeleteMoreTemplateHandler OnDeleteMoreTemplate;

        public static  event OnDeleteTemplateHandler OnDeleteTemplate;

        public static  event OnTemplatePropertyChangeHandler OnTemplatePropertyChange;

        public static void AddEditTemplate(JLKEditTemplate template)
        {
            if (OnAddTemplate != null)
            {
                OnAddTemplate(template);
            }
        }

        public static void AddMoreEditTemplate(List<JLKEditTemplate> template)
        {
            if (OnAddMoreTemplate != null)
            {
                OnAddMoreTemplate(template);
            }
        }

        public static void DeleteEditTemplate(JLKEditTemplate template)
        {
            if (OnDeleteTemplate != null)
            {
                OnDeleteTemplate(template);
            }
        }

        public static void DeleteMoreEditTemplate(List<JLKEditTemplate> template)
        {
            if (OnDeleteMoreTemplate != null)
            {
                OnDeleteMoreTemplate(template);
            }
        }

        public static void TemplatePropertyChange(JLKEditTemplate template)
        {
            if (OnTemplatePropertyChange != null)
            {
                OnTemplatePropertyChange(template);
            }
        }
    }
}

