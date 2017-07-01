using System.Collections.Generic;

namespace Yutai.ArcGIS.Common.Editor
{
    public class CopyEnvironment
    {
        public static ICopyMaps CopyMaps { get; set; }

        public static bool HasCopyMap { get; set; }

        public static bool IsAutoUnion { get; set; }

        public List<LayerMap> LayerMaps { get; set; }

        static CopyEnvironment()
        {
            CopyEnvironment.old_acctor_mc();
        }

        public CopyEnvironment()
        {
        }

        private static void old_acctor_mc()
        {
        }
    }
}