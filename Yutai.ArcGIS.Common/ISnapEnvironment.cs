using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common
{


    public interface ISnapEnvironment : IEngineSnapEnvironment
    {
        double MapUnitTolerance
        {
            get;
        }

        bool SnapPoint(IPoint ipoint_0, IPoint ipoint_1);
    }

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
                    result = ApplicationRef.AppContext.Config.PyramidPromptType;
                }
                return result;
            }
        }

        public static object BarManage
        {
            get;
            set;
        }
    }

 



}