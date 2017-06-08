using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class frmNewVerticalSectionSet : Form
    {
        private ComboBox cboBOTTOMHField;
        private ComboBox cboPipleLineLayers;
        private ComboBox cboPiplePointLayers;
        private ComboBox cboSURFHField;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private IFeatureLayer m_pPipePointLayer = null;
        private IPolyline m_pPipePolyLine = null;
        private IPolyline m_pTerrainPolyLine = null;
        private string m_strBOTTOMH = "BOTTOM_H";
        private string m_strSURFH = "SURF_H";

        public frmNewVerticalSectionSet()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewVerticalSectionSet));
            this.label1 = new Label();
            this.cboPipleLineLayers = new ComboBox();
            this.label2 = new Label();
            this.cboPiplePointLayers = new ComboBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cboSURFHField = new ComboBox();
            this.cboBOTTOMHField = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线层";
            this.cboPipleLineLayers.Location = new System.Drawing.Point(0x48, 0x10);
            this.cboPipleLineLayers.Name = "cboPipleLineLayers";
            this.cboPipleLineLayers.Size = new Size(0x98, 20);
            this.cboPipleLineLayers.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "点层";
            this.cboPiplePointLayers.Location = new System.Drawing.Point(0x48, 0x38);
            this.cboPiplePointLayers.Name = "cboPiplePointLayers";
            this.cboPiplePointLayers.Size = new Size(0x98, 20);
            this.cboPiplePointLayers.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "地面点高程字段";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x90);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4d, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "管底高程字段";
            this.cboSURFHField.Location = new System.Drawing.Point(0x70, 0x68);
            this.cboSURFHField.Name = "cboSURFHField";
            this.cboSURFHField.Size = new Size(0x70, 20);
            this.cboSURFHField.TabIndex = 6;
            this.cboBOTTOMHField.Location = new System.Drawing.Point(0x70, 0x88);
            this.cboBOTTOMHField.Name = "cboBOTTOMHField";
            this.cboBOTTOMHField.Size = new Size(0x70, 20);
            this.cboBOTTOMHField.TabIndex = 7;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x100, 0x102);
            base.Controls.Add(this.cboBOTTOMHField);
            base.Controls.Add(this.cboSURFHField);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboPiplePointLayers);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboPipleLineLayers);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmNewVerticalSectionSet";
            this.Text = "纵断面设置";
            base.ResumeLayout(false);
            base.PerformLayout();
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

