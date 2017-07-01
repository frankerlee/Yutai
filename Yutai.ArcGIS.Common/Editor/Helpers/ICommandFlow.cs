using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public interface ICommandFlow
    {
        IAppContext AppContext { set; }

        string CurrentCommandInfo { get; }

        bool IsFinished { get; }

        bool HandleCommand(string string_0);

        void Reset();

        void ShowCommandLine();
    }
}