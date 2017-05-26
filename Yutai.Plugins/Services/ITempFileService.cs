namespace Yutai.Plugins.Services
{
    public interface ITempFileService
    {
        void DeleteAll();
        void Register(string name);
        string GetTempFilename(string extensionWithDot);
    }
}