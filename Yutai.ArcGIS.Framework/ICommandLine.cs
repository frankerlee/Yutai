namespace Yutai.ArcGIS.Framework
{
    public interface ICommandLine
    {
        void ActiveCommand();
        void Cancel();
        void HandleCommandParameter(string string_0);

        string CommandLines { set; }

        string CommandName { get; }

        COMMANDTYPE CommandType { get; }
    }
}