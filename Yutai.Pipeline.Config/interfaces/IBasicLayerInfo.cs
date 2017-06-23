namespace Yutai.Pipeline.Config.Interfaces
{
    public interface IBasicLayerInfo
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        string TemplateName { get; set; }

        void LoadTemplate(IPipelineTemplate template);
    }
}