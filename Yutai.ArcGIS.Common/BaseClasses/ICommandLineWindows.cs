namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface ICommandLineWindows
    {
        void Init();
        void LockCommandLine(bool bool_0);
        void ShowCommandString(string string_0, short short_0);

        object Framework { get; set; }

        int MaxCommandLine { get; set; }
    }
}

