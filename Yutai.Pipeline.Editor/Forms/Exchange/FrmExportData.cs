using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Controls;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmExportData : Form
    {
        private IAppContext _context;
        private List<UcLayerFields> _layerFieldsList;

        public FrmExportData(IAppContext context)
        {
            InitializeComponent();
            _context = context;
            this.ucExtentSetting1.Map = _context.FocusMap;
            LoadLayers();
        }

        private void LoadLayers()
        {
            _layerFieldsList = new List<UcLayerFields>();
            panelLayers.Controls.Clear();
            List<IFeatureLayer> layers = MapHelper.GetFeatureLayers(_context.FocusMap);
            foreach (IFeatureLayer featureLayer in layers)
            {
                UcLayerFields control = new UcLayerFields(featureLayer);
                control.Dock = DockStyle.Top;
                panelLayers.Controls.Add(control);
                _layerFieldsList.Add(control);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary<int, IGeometry> boundGeometrys = this.ucExtentSetting1.BoundGeometrys;
                if (boundGeometrys == null || boundGeometrys.Count <= 0)
                {
                    MessageBox.Show(@"请选择输出数据的范围！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_layerFieldsList == null || !_layerFieldsList.Any(c => c.Checked))
                {
                    MessageBox.Show(@"请选择要输出的图层！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.ucExportPath1.Path))
                {
                    MessageBox.Show(@"请选择要输出数据的存放位置！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IWorkspaceEdit workspaceEdit = this.ucExportPath1.Workspace as IWorkspaceEdit;
                if (workspaceEdit == null)
                    return;
                try
                {
                    workspaceEdit.StartEditing(false);
                    workspaceEdit.StartEditOperation();

                    foreach (UcLayerFields layerFields in _layerFieldsList)
                    {
                        try
                        {
                            if (!layerFields.Checked)
                                continue;
                            IFeatureClass featureClass = layerFields.CreateFeatureClass(this.ucExportPath1.Workspace,
                                this.ucExportPath1.Dataset as IFeatureDataset,
                                this.ucSpatialReference1.ReferenceType == EnumSpatialReferenceType.MapReference
                                    ? _context.FocusMap.SpatialReference
                                    : null);
                            foreach (KeyValuePair<int, IGeometry> boundGeometry in boundGeometrys)
                            {
                                ISpatialFilter filter = new SpatialFilterClass();
                                filter.Geometry = boundGeometry.Value;
                                filter.GeometryField = layerFields.FeatureLayer.FeatureClass.ShapeFieldName;
                                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                                IFeatureCursor featureCursor = layerFields.FeatureLayer.FeatureClass.Search(filter, false);
                                IFeature feature;
                                IFeatureCursor insertCursor = featureClass.Insert(true);
                                while ((feature = featureCursor.NextFeature()) != null)
                                {
                                    try
                                    {
                                        IFeatureBuffer insertFeature = featureClass.CreateFeatureBuffer();

                                        insertFeature.Shape = feature.ShapeCopy;
                                        foreach (KeyValuePair<int, int> keyValuePair in layerFields.FieldMappingInts)
                                        {
                                            try
                                            {
                                                insertFeature.Value[keyValuePair.Key] = feature.Value[keyValuePair.Value] is DBNull && ucExportPath1.ExportType == EnumExportType.Shapefile ? " " : feature.Value[keyValuePair.Value];
                                            }
                                            catch (Exception exception)
                                            {
                                                throw new Exception($"[{insertFeature.Fields.Field[keyValuePair.Key].Name}]:{exception.Message}");
                                            }
                                        }
                                        insertCursor.InsertFeature(insertFeature);
                                    }
                                    catch (Exception exception)
                                    {
                                        throw new Exception($"{feature.OID}:{exception.Message}");
                                    }
                                }
                                Marshal.ReleaseComObject(insertCursor);
                                Marshal.ReleaseComObject(featureCursor);
                            }
                        }
                        catch (Exception exception)
                        {
                            throw new Exception($"{layerFields.FeatureLayer.Name}:{exception.Message}");
                        }
                    }
                    MessageBox.Show(@"输出完成！");
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
