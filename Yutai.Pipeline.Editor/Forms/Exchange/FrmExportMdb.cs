using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Controls;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmExportMdb : Form
    {
        private IAppContext _context;
        private List<UcLayerFields> _layerFieldsList;

        public FrmExportMdb(IAppContext context)
        {
            InitializeComponent();
            _context = context;
            this.ucExtentSetting1.Context = _context;
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
                if (string.IsNullOrWhiteSpace(this.ucSelectFile1.FileName))
                {
                    MessageBox.Show(@"请选择要输出数据的存放位置！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                AccessTools tools = new AccessTools(this.ucSelectFile1.FileName);
                foreach (UcLayerFields layerFields in _layerFieldsList)
                {
                    try
                    {
                        if (!layerFields.Checked)
                            continue;
                        DataTable dataTable =
                            FeatureClassUtil.FeatureClassToDataTable(layerFields.FeatureLayer.FeatureClass, boundGeometrys, null, layerFields.SelectedFieldDictionary);
                        if (tools.CreateTable(layerFields.SelectedFieldDictionary, layerFields.FeatureLayer.FeatureClass.AliasName))
                            tools.FillTable(dataTable);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (!string.IsNullOrEmpty(tools.GetStrErrorInfo()))
                {
                    MessageBox.Show(tools.GetStrErrorInfo());
                }
                else
                {
                    Process.Start(ucSelectFile1.FileName);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void FrmExportData_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ucExtentSetting1.DrawCompleteEvent -= UcExtentSetting1OnDrawCompleteEvent;
            this.ucExtentSetting1.Destory();
        }

        private void FrmExportData_Load(object sender, EventArgs e)
        {
            this.ucExtentSetting1.DrawCompleteEvent += UcExtentSetting1OnDrawCompleteEvent;
        }

        private void UcExtentSetting1OnDrawCompleteEvent(object sender, EventArgs eventArgs)
        {
            this.Activate();
        }
    }
}
