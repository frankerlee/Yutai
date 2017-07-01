using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Analysis.Helpers
{
    public class NetworkHelper
    {
        public static IFeature FindFeature(IEnumFeatureClass pEnumFC, int userClassID, int userID)
        {
            IFeatureClass pFC = pEnumFC.Next();
            while (pFC != null)
            {
                if (pFC.ObjectClassID == userClassID)
                {
                    return pFC.GetFeature(userID);
                }
            }
            return null;
        }

        public IEnumNetEID GetEIDsByOID(INetElements elements, int userId, int userClassID, esriElementType elementType)
        {
            return elements.GetEIDs(userClassID, userId, elementType);
        }

        public IFeature GetEIDFeature(IGeometricNetwork geometricNetwork, int eid, esriElementType elementType)
        {
            int userClassId, userId, userSubId;
            bool isValid = ((INetElements) geometricNetwork.Network).IsValidElement(eid, elementType);
            ((INetElements) geometricNetwork.Network).QueryIDs(eid, elementType, out userClassId, out userId,
                out userSubId);
            if (!isValid) return null;
            return FindFeature(GetEnumFeatureClasses(geometricNetwork, esriFeatureType.esriFTSimpleEdge), userClassId,
                userId);
        }

        public IEnumFeatureClass GetEnumFeatureClasses(IGeometricNetwork geometricNetwork, esriFeatureType featureType)
        {
            return geometricNetwork.ClassesByType[featureType];
        }
    }
}