using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Menu
{
    internal static class StatusBarKeys
    {
        public const string ViewStyleDropDown = "statusViewStyleDropDown";
        public const string ViewStyleAll = "statusViewStyleAll";
        public const string ViewStyle2D = "statusViewStyle2D";
        public const string ViewStyle3D = "statusViewStyle3D";
        public const string ViewStyleConfig = "statusViewStyleConfig";
        public const string SelectedCount = "statusSelectedCount";
        public const string ModifiedCount = "statusModifiedCount";
        public const string MapUnits = "statusMapUnits";
        public const string XYCoordinates = "statusCoordinates";
        public const string TileProvider = "statusTileProvider";
        public const string ProgressMsg = "statusProgressMsg";
        public const string ProgressBar = "statusProgressBar";
        public const string MapScale = "statusMapScale";

        public static string GetStatusItemName(string key)
        {
            switch (key)
            {
                case ViewStyleDropDown:
                    return "视图类型";
                case TileProvider:
                    return "底图图层";
                case MapScale:
                    return "当前比例";
                case MapUnits:
                    return "单位";
                case XYCoordinates:
                    return "坐标";
            }

            return key;
        }
    }
}
