namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapNoAssistantFactory
    {
        public static MapNoAssistant CreateMapNoAssistant(string string_0)
        {
            if (string_0.IndexOf("-") >= -1)
            {
                if (string_0.Length == 8)
                {
                    return new LandUseMapNoAssistant(string_0);
                }
                if (string_0.Length >= 10)
                {
                    return new GBMapNoAssistant(string_0);
                }
            }
            return null;
        }
    }
}

