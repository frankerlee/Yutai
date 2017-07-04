using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;


namespace Yutai.ArcGIS.Common.Helpers
{
    public class IndexHelper
    {
        public static void CreateGridIndex(IFeatureClass pClass, IEnvelope pEnvelope, double width, double height, string keyName)
        {
            pEnvelope.XMin = Math.Floor(pEnvelope.XMin/width)*width;
            pEnvelope.YMin = Math.Floor(pEnvelope.YMin / height) * height;

            pEnvelope.XMax = Math.Ceiling(pEnvelope.XMax / width) * width;
            pEnvelope.YMax = Math.Ceiling(pEnvelope.YMax / height) * height;

            using (ComReleaser comReleaser = new ComReleaser())
            {
                // Create a feature buffer.
                IFeatureBuffer featureBuffer = pClass.CreateFeatureBuffer();
                comReleaser.ManageLifetime(featureBuffer);

                // Create an insert cursor.
                IFeatureCursor insertCursor = pClass.Insert(true);
                comReleaser.ManageLifetime(insertCursor);

                // All of the features to be created are classified as Primary Highways.
                int typeFieldIndex = pClass.FindField(keyName);
                int ii = 0;
                int jj = 0;

                double x1 = pEnvelope.XMin;
                double y1 = pEnvelope.YMin;
                double x2 = pEnvelope.XMax;
                double y2 = pEnvelope.YMax;
                object missing = Missing.Value;
                double x = x1;
                double y = y1;
                int counter = 0;
                while (y < y2)
                {
                    jj++;
                    x = x1;
                    ii = 0;
                    double yy = y + height;
                    while (x < x2)
                    {
                        ii++;
                        double xx = x + width;
                       IPolygon polygon=new Polygon() as IPolygon;
                        IPointCollection pointCollection=polygon as IPointCollection;
                        IPoint point = new Point();
                        point.PutCoords(x,y);
                        IPoint startPoint = point;
                        pointCollection.AddPoint(point,ref missing,ref missing);
                        point = new Point();
                        point.PutCoords(x, yy);
                        pointCollection.AddPoint(point, ref missing, ref missing);
                        point = new Point();
                        point.PutCoords(xx, yy);
                        pointCollection.AddPoint(point, ref missing, ref missing);
                        point = new Point();
                        point.PutCoords(xx, y);
                        pointCollection.AddPoint(point, ref missing, ref missing);
                        pointCollection.AddPoint(startPoint, ref missing, ref missing);

                        featureBuffer.Shape = polygon;
                        if (typeFieldIndex >= 0)
                        {
                            featureBuffer.Value[typeFieldIndex] = string.Format("{0}-{1}", ii, jj);
                        }
                        insertCursor.InsertFeature(featureBuffer);
                        counter++;
                        if (counter >= 1000)
                        {
                            insertCursor.Flush();
                            insertCursor = pClass.Insert(true);
                            comReleaser.ManageLifetime(insertCursor);
                            counter = 0;
                        }
                        x = xx;
                    }
                    y = yy;
                }

                insertCursor.Flush();

            }
        }
    
    }
}
