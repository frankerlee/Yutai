using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Controls;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public partial class FrmMultiCheQiConfig : Form, IMultiCheQiConfig
    {
        private readonly List<IFieldSetting> _fieldSettingList;

        public FrmMultiCheQiConfig(IAppContext context)
        {
            InitializeComponent();
            this.ucSelectFeatureClasses1.Map = context.FocusMap;
            this.ucSelectFeatureClasses1.GeometryType = esriGeometryType.esriGeometryPolyline;
            this.cmbFlagAnnoLayer.Map = context.FocusMap;
            this.cmbFlagLineLayer.Map = context.FocusMap;
            this.cmbFlagLineLayer.GeometryType = esriGeometryType.esriGeometryPolyline;
            _fieldSettingList = new List<IFieldSetting>();
        }

        public List<IFeatureLayer> FlagLayerList => ucSelectFeatureClasses1.SelectedFeatureLayerList;

        public IFeatureLayer FlagLineLayer => cmbFlagLineLayer.SelectFeatureLayer;

        public IFeatureLayer FlagAnnoLayer => cmbFlagAnnoLayer.SelectFeatureLayer;

        public List<IFieldSetting> FieldSettingList => _fieldSettingList;

        public IFontConfig HeaderFontConfig => ucFontHeader;

        public IFontConfig ContentFontConfig => ucFontContent;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (FlagLayerList == null || FlagLayerList.Count <= 0)
                return;
            UcFieldSetting control = new UcFieldSetting();
            control.Index = _fieldSettingList.Count + 1;
            control.Fields = FlagLayerList[0].FeatureClass.Fields;
            control.Dock = DockStyle.Top;
            control.RemoveEvent += ControlOnRemoveEvent;
            _fieldSettingList.Add(control);
            panelFields.Controls.Add(control);
            control.BringToFront();
        }

        private void ControlOnRemoveEvent(object sender, object o)
        {
            _fieldSettingList.Remove((IFieldSetting)sender);
            panelFields.Controls.Remove((UcFieldSetting)sender);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _fieldSettingList?.Clear();
            panelFields.Controls.Clear();
        }
    }
}
