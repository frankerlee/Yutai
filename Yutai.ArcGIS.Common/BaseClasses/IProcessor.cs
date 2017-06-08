namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface IProcessor
    {
        event OnCompleteHander OnComplete;

        event OnMessageHandler OnMessage;

        void Cancel();
        bool Excute();

        string Name { get; }
    }
}

