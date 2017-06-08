namespace Yutai.ArcGIS.Carto
{
    public class ApplyMapTemplateAssist
    {
        public static  event OnActivePageLayoutHandler OnActivePageLayout;

        public static void ActivePageLayout()
        {
            if (OnActivePageLayout != null)
            {
                OnActivePageLayout();
            }
        }
    }
}

