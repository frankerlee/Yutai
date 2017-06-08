using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NetworkCommonTool
    {
        public static int EdgeFeatCount(ISimpleJunctionFeature pSJF)
        {
            IRow row = null;
            IRow row2 = null;
            int num = 0;
            int edgeFeatureCount = 0;
            edgeFeatureCount = pSJF.EdgeFeatureCount;
            num = edgeFeatureCount;
            for (int i = 0; i < (edgeFeatureCount - 1); i++)
            {
                for (int j = i + 1; j < edgeFeatureCount; j++)
                {
                    row = pSJF.get_EdgeFeature(i) as IRow;
                    row2 = pSJF.get_EdgeFeature(j) as IRow;
                    if (row.OID == row2.OID)
                    {
                        num--;
                    }
                }
            }
            return num;
        }
    }
}

