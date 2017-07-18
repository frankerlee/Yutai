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
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    public partial class FrmClip : Form
    {
        private IAppContext _context;
        private List<CheckBox> _layerCheckBoxs;

        public FrmClip(IAppContext context)
        {
            InitializeComponent();
            _context = context;
            this.ucExtentSetting1.Context = _context;
            this.ucExtentSetting1.Map = _context.FocusMap;
            this.ucExtentSetting1.IsRadio = true;
            LoadLayers();
        }

        private void LoadLayers()
        {
            _layerCheckBoxs = new List<CheckBox>();
            panelLayers.Controls.Clear();
            List<IFeatureLayer> layers = MapHelper.GetFeatureLayers(_context.FocusMap);
            foreach (IFeatureLayer featureLayer in layers)
            {
                CheckBox checkBox = new CheckBox()
                {
                    Text = featureLayer.Name,
                    Dock = DockStyle.Top,
                    Tag = featureLayer.FeatureClass
                };
                panelLayers.Controls.Add(checkBox);
                _layerCheckBoxs.Add(checkBox);
            }
        }

        private void FrmClip_Load(object sender, EventArgs e)
        {
            this.ucExtentSetting1.DrawCompleteEvent += UcExtentSetting1OnDrawCompleteEvent;
        }

        private void UcExtentSetting1OnDrawCompleteEvent(object sender, EventArgs eventArgs)
        {
            this.Activate();
        }

        private void FrmClip_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ucExtentSetting1.DrawCompleteEvent -= UcExtentSetting1OnDrawCompleteEvent;
            this.ucExtentSetting1.Destory();
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
                if (_layerCheckBoxs == null || !_layerCheckBoxs.Any(c => c.Checked))
                {
                    MessageBox.Show(@"请选择要裁剪的图层！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.ucExportPath1.Path))
                {
                    MessageBox.Show(@"请选择要输出数据的存放位置！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<IFeatureClass> list = new List<IFeatureClass>();
                foreach (CheckBox checkBox in _layerCheckBoxs)
                {
                    if (checkBox.Checked)
                    {
                        list.Add(checkBox.Tag as IFeatureClass);
                    }
                }
                SDEToShapefile sde = new SDEToShapefile();
                sde.AddFeatureClasses(list);
                sde.ClipGeometry = boundGeometrys[0];
                sde.IsClip = true;
                sde.Convert(this.ucExportPath1.Workspace);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
