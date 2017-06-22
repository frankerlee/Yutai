namespace Yutai.Pipeline.Config.interfaces
{
    public interface IBasicLayerInfo
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool Visible { get; set; }
        string TemplateName { get; set; }
    }
}