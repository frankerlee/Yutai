using ESRI.ArcGIS.Geoprocessor;

namespace Yutai.ArcGIS.Controls.DataConversionTools
{
    internal class ImportCoordinateFile : IGPProcess
    {
        public string Alias
        {
            get
            {
                return "导入坐标数据文件";
            }
        }

        public object[] ParameterInfo
        {
            get
            {
                return null;
            }
        }

        public string ToolboxDirectory
        {
            get
            {
                return "";
            }
        }

        public string ToolboxName
        {
            get
            {
                return "";
            }
        }

        public string ToolName
        {
            get
            {
                return "";
            }
        }
    }
}

