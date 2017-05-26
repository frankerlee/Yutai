namespace Yutai.Plugins.Interfaces
{
    public interface IPlugin
    {
        string Description { get; }

        void Initialize(IAppContext context);

        void Terminate();
    }
}