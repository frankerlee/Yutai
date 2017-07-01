namespace Yutai.ArcGIS.Catalog.VCT.VCT
{
    internal class FileConfig
    {
        public static int MAP_DISTANCE;
        public static int STREAM_BUFFER_SIZE;

        static FileConfig()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            STREAM_BUFFER_SIZE = 1024000;
            MAP_DISTANCE = 10;
        }
    }
}