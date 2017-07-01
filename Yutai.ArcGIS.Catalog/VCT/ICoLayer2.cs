namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoLayer2
    {
        void AppendRule(CoRuleClass coRuleClass_0);
        void DeleteRule(CoRuleClass coRuleClass_0);
        void DeleteRule(string string_0);

        CoRuleCollection Rules { get; }
    }
}