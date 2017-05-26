namespace Yutai.Shared
{
    public static class MapUnitsHelper
    {
        public static string GetMapUnitsDesc(int mapUnits)
        {
            string str;
            switch (mapUnits)
            {
                case 0:
                {
                    str = "未知单位";
                    break;
                }
                case 1:
                {
                    str = "英寸";
                    break;
                }
                case 2:
                {
                    str = "点";
                    break;
                }
                case 3:
                {
                    str = "英尺";
                    break;
                }
                case 4:
                {
                    str = "码";
                    break;
                }
                case 5:
                {
                    str = "英里";
                    break;
                }
                case 6:
                {
                    str = "海里";
                    break;
                }
                case 7:
                {
                    str = "毫米";
                    break;
                }
                case 8:
                {
                    str = "厘米";
                    break;
                }
                case 9:
                {
                    str = "米";
                    break;
                }
                case 10:
                {
                    str = "公里";
                    break;
                }
                case 11:
                {
                    str = "度";
                    break;
                }
                case 12:
                {
                    str = "分米";
                    break;
                }
                default:
                {
                    str = "未知单位";
                    break;
                }
            }
            return str;
        }
    }
}