using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal partial class frmNewVerticalSectionSet : Form
    {
        private IFeatureLayer m_pPipePointLayer = null;
        private IPolyline m_pPipePolyLine = null;
        private IPolyline m_pTerrainPolyLine = null;
        private string m_strBOTTOMH = "BOTTOM_H";
        private string m_strSURFH = "SURF_H";

        public frmNewVerticalSectionSet()
        {
            this.InitializeComponent();
        }

 private void sbCalculatePointSurfaceHeight()
        {
            IFields fields = this.m_pPipePointLayer.FeatureClass.Fields;
            int index = fields.FindField(this.m_strSURFH);
            int num2 = fields.FindField(this.m_strBOTTOMH);
            if (index == -1)
            {
                MessageBox.Show("点层数据无地面高程字段");
            }
            else if (num2 == -1)
            {
                MessageBox.Show("点层数据无管底高程字段");
            }
            else
            {
                ISpatialFilter filter = new SpatialFilterClass {
                    Geometry = this.m_pPipePolyLine,
                    GeometryField = this.m_pPipePointLayer.FeatureClass.ShapeFieldName,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelTouches
                };
                IFeatureCursor cursor = this.m_pPipePointLayer.FeatureClass.Search(filter, false);
                if (cursor != null)
                {
                    IFeature feature = cursor.NextFeature();
                    if (feature != null)
                    {
                        double num3 = (double) feature.get_Value(index);
                        double num4 = (double) feature.get_Value(num2);
                        IPoint point = new PointClass {
                            X = this.m_pPipePolyLine.FromPoint.X,
                            Y = this.m_pPipePolyLine.FromPoint.Y,
                            Z = num4
                        };
                        this.m_pPipePolyLine.FromPoint = point;
                        point = new PointClass {
                            X = this.m_pTerrainPolyLine.FromPoint.X,
                            Y = this.m_pTerrainPolyLine.FromPoint.Y,
                            Z = num3
                        };
                        this.m_pTerrainPolyLine.FromPoint = point;
                        feature = cursor.NextFeature();
                        if (feature != null)
                        {
                            double num5 = (double) feature.get_Value(index);
                            double num6 = (double) feature.get_Value(num2);
                            point = new PointClass {
                                X = this.m_pPipePolyLine.ToPoint.X,
                                Y = this.m_pPipePolyLine.ToPoint.Y,
                                Z = num6
                            };
                            this.m_pPipePolyLine.ToPoint = point;
                            point = new PointClass {
                                X = this.m_pTerrainPolyLine.ToPoint.X,
                                Y = this.m_pTerrainPolyLine.ToPoint.Y,
                                Z = num5
                            };
                            this.m_pTerrainPolyLine.ToPoint = point;
                        }
                    }
                }
            }
        }
    }
}

