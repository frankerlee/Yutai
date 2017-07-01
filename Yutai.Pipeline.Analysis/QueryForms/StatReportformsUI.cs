using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class StatReportformsUI : Form
    {
        private partial class LayerboxItem
        {
            public IFeatureLayer m_pPipeLayer;

            public override string ToString()
            {
                return this.m_pPipeLayer.Name;
            }
        }

        public IGeometry m_ipGeo;

        public IAppContext m_context;

        public IMapControl3 MapControl;

        public IPipelineConfig pPipeCfg;

        private IFeatureLayer _featureLayer;

        public StatReportformsUI()
        {
            this.InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            _featureLayer = ((LayerboxItem) this.CmbLayers.SelectedItem).m_pPipeLayer;
            if (CmbCalField.SelectedItem == null || CmbStatField.SelectedItem == null || CmbStatWay.SelectedItem == null)
                return;
            string statFieldName = this.CmbStatField.SelectedItem.ToString();
            string calFieldName = this.CmbCalField.SelectedItem.ToString();
            string statWay = this.CmbStatWay.SelectedItem.ToString();
            ITable table = (ITable) _featureLayer.FeatureClass;
            ICursor cursor = table.Search(null, false);
            int idxStatField = cursor.FindField(statFieldName);
            int idxCalField = cursor.FindField(calFieldName);
            if (idxStatField <= 0 || idxCalField <= 0)
            {
                MessageBox.Show(@"字段不存在!");
                return;
            }
            IRow row;
            DataTable dataTable = CreateDataTable(statFieldName, calFieldName);
            while ((row = cursor.NextRow()) != null)
            {
                dataTable.Rows.Add(row.Value[idxStatField], row.Value[idxCalField]);
            }

            StatForm form = new StatForm();
            form.Form_CalField = this.CmbCalField.SelectedItem.ToString();
            form.Form_StatField = this.CmbStatField.SelectedItem.ToString();
            form.Form_StatWay = statWay;
            form.resultTable = dataTable;
            form.TopMost = true;
            form.ShowDialog(this);
        }

        private DataTable CreateDataTable(string field1, string field2)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn(field1)
            {
                DataType = System.Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn(field2)
            {
                DataType = System.Type.GetType("System.Double")
            });
            return dataTable;
        }

        private void StatReportformsUI_Load(object sender, EventArgs e)
        {
            this.AutoFlash();
        }

        private void AutoFlash()
        {
            this.CmbLayers.Items.Clear();
            int layerCount = m_context.FocusMap.LayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = m_context.FocusMap.Layer[i];
                this.AddLayer(layer);
            }
            if (this.CmbLayers.Items.Count > 0)
            {
                this.CmbLayers.SelectedIndex = 0;
            }
            FillFields();
        }

        private void AddLayer(ILayer ipLay)
        {
            if (ipLay is IFeatureLayer)
            {
                this.AddFeatureLayer((IFeatureLayer) ipLay);
            }
            else if (ipLay is IGroupLayer)
            {
                this.AddGroupLayer((IGroupLayer) ipLay);
            }
        }

        private void AddGroupLayer(IGroupLayer iGLayer)
        {
            ICompositeLayer compositeLayer = (ICompositeLayer) iGLayer;
            if (compositeLayer != null)
            {
                int count = compositeLayer.Count;
                for (int i = 0; i < count; i++)
                {
                    ILayer ipLay = compositeLayer.Layer[i];
                    this.AddLayer(ipLay);
                }
            }
        }

        private void AddFeatureLayer(IFeatureLayer iFLayer)
        {
            if (iFLayer != null)
            {
                if (this.pPipeCfg.IsPipelineLayer(iFLayer.Name, enumPipelineDataType.Line))
                {
                    LayerboxItem layerboxItem = new LayerboxItem();
                    layerboxItem.m_pPipeLayer = iFLayer;
                    this.CmbLayers.Items.Add(layerboxItem);
                }
            }
        }

        private void FillFields()
        {
            _featureLayer = ((LayerboxItem) this.CmbLayers.SelectedItem).m_pPipeLayer;
            SetComboBoxItems(this.CmbStatField, _featureLayer.FeatureClass.Fields,
                new List<esriFieldType>() {esriFieldType.esriFieldTypeString});
            SetComboBoxItems(this.CmbCalField, _featureLayer.FeatureClass.Fields,
                new List<esriFieldType>()
                {
                    esriFieldType.esriFieldTypeDouble,
                    esriFieldType.esriFieldTypeInteger,
                    esriFieldType.esriFieldTypeSingle,
                    esriFieldType.esriFieldTypeSmallInteger
                });
        }

        public void SetComboBoxItems(ComboBox comboBox, IFields fields, List<esriFieldType> types)
        {
            if (comboBox.Items.Count > 0)
                comboBox.Items.Clear();
            int count = fields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField pField = fields.Field[i];
                if (pField != null && types != null && types.Contains(pField.Type))
                {
                    comboBox.Items.Add(pField.Name);
                }
            }
        }

        private void CmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillFields();
        }
    }
}