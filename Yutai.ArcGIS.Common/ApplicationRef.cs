using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common
{
    public class ApplicationRef
    {
        public static int ControlType
        {
            get;
            set;
        }

        public static IAppContext AppContext
        {
            get;
            set;
        }

        public static PyramidPromptType PyramidPromptType
        {
            get
            {
                PyramidPromptType result;
                if (ApplicationRef.AppContext == null)
                {
                    result = PyramidPromptType.AlwaysPrompt;
                }
                else
                {
                    result = (PyramidPromptType) ApplicationRef.AppContext.Config.PyramidPromptType;
                }
                return result;
            }
        }

        public static object BarManage
        {
            get;
            set;
        }

        public static ApplicationBase Application { get; set; }
    }
}