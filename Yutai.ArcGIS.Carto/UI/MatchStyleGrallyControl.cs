using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MatchStyleGrallyControl : UserControl
    {
        private bool bool_0 = false;
        private IColorRamp icolorRamp_0 = new RandomColorRampClass();
        private IFeatureLayer ifeatureLayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IMap imap_0 = null;
        public IStyleGallery m_pSG = null;

        public MatchStyleGrallyControl()
        {
            this.InitializeComponent();
            IRandomColorRamp ramp = this.icolorRamp_0 as RandomColorRamp;
            if (ramp != null)
            {
                ramp.StartHue = 40;
                ramp.EndHue = 120;
                ramp.MinValue = 65;
                ramp.MaxValue = 90;
                ramp.MinSaturation = 25;
                ramp.MaxSaturation = 45;
            }
        }

        private void btnAddAllValues_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.ilist_0.Clear();
            string[] items = new string[3];
            IFeatureLayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
            IFields fields = layer.FeatureClass.Fields;
            IField field = fields.get_Field(fields.FindFieldByAliasName(this.cboFields.SelectedItem.ToString()));
            if (this.GetUniqueValues(layer, field.Name, this.ilist_0))
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                this.method_4(layer.FeatureClass.ShapeType);
                for (int i = this.ilist_0.Count - 1; i >= 0; i--)
                {
                    items[0] = this.ilist_0[i].ToString();
                    items[1] = this.ilist_0[i].ToString();
                    queryFilter.WhereClause = field.Name + " = " +
                                              this.ConvertFieldValueToString(field.Type, this.ilist_0[i]);
                    items[2] = layer.FeatureClass.FeatureCount(queryFilter).ToString();
                    ListViewItem item = new ListViewItem(items);
                    this.listView1.Items.Add(item);
                    this.ilist_0.RemoveAt(i);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                string text = this.listView1.SelectedItems[i].Text;
                index = this.listView1.SelectedIndices[i];
                this.listView1.Items.RemoveAt(index);
                this.ilist_0.Add(text);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.ilist_0.Clear();
            this.bool_0 = false;
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "符号库 (*.Style)|*.Style",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IStyleGalleryStorage pSG = (IStyleGalleryStorage) this.m_pSG;
                int fileCount = pSG.FileCount;
                pSG.AddFile(dialog.FileName);
                if (fileCount == pSG.FileCount)
                {
                    pSG = null;
                }
                else
                {
                    pSG = null;
                    this.cboStyleGrally.Properties.Items.Add(dialog.FileName);
                }
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.ilist_0.Clear();
            if (this.cboFields.SelectedIndex >= 0)
            {
            }
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboFields.Properties.Items.Clear();
            IFeatureLayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
            IFields fields = layer.FeatureClass.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if ((((field.Type != esriFieldType.esriFieldTypeOID) &&
                      (field.Type != esriFieldType.esriFieldTypeGeometry)) &&
                     (field.Type != esriFieldType.esriFieldTypeBlob)) &&
                    (field.Type != esriFieldType.esriFieldTypeRaster))
                {
                    this.cboFields.Properties.Items.Add(field.AliasName);
                }
            }
            if (this.cboFields.Properties.Items.Count > 0)
            {
                this.cboFields.SelectedIndex = 0;
            }
        }

        public string ConvertFieldValueToString(esriFieldType esriFieldType_0, object object_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                case esriFieldType.esriFieldTypeOID:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeString:
                    return ("'" + object_0.ToString() + "'");

                case esriFieldType.esriFieldTypeDate:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何数据";

                case esriFieldType.esriFieldTypeBlob:
                    return "长二进制串";
            }
            return "";
        }

        public bool GetUniqueValues(ILayer ilayer_0, string string_0, IList ilist_1)
        {
            try
            {
                IAttributeTable table = ilayer_0 as IAttributeTable;
                if (table == null)
                {
                    return false;
                }
                ITable attributeTable = table.AttributeTable;
                IQueryFilter queryFilter = new QueryFilterClass
                {
                    WhereClause = "1=1"
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + string_0;
                ICursor cursor = attributeTable.Search(queryFilter, false);
                IDataStatistics statistics = new DataStatisticsClass
                {
                    Field = string_0,
                    Cursor = cursor
                };
                if ((statistics.UniqueValueCount > 500) &&
                    (MessageBox.Show("唯一值数大于500，是否继续", "提示", MessageBoxButtons.YesNo) == DialogResult.No))
                {
                    return false;
                }
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                while (uniqueValues.MoveNext())
                {
                    ilist_1.Add(uniqueValues.Current);
                }
                return true;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        public IUniqueValueRenderer MakeUniqueValueRenderer()
        {
            IFeatureLayer data = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
            IUniqueValueRenderer renderer = new UniqueValueRendererClass();
            ISymbol symbol = this.method_3(data.FeatureClass.ShapeType);
            if (symbol != null)
            {
                renderer.FieldCount = 1;
                renderer.set_Field(0, this.cboFields.Text);
                renderer.DefaultLabel = "默认符号";
                renderer.DefaultSymbol = symbol;
                renderer.UseDefaultSymbol = true;
                try
                {
                    bool flag;
                    string str = this.method_4(data.FeatureClass.ShapeType);
                    this.icolorRamp_0.Size = this.listView1.Items.Count;
                    this.icolorRamp_0.CreateRamp(out flag);
                    IEnumColors colors = this.icolorRamp_0.Colors;
                    colors.Reset();
                    for (int i = 0; i < this.listView1.Items.Count; i++)
                    {
                        ISymbol symbol2;
                        ListViewItem item = this.listView1.Items[i];
                        IStyleGalleryItem item2 = SymbolFind.FindStyleGalleryItem(item.SubItems[0].Text, this.m_pSG,
                            this.cboStyleGrally.Text, str, "");
                        if (item2 == null)
                        {
                            IColor color = colors.Next();
                            symbol2 = (symbol as IClone).Clone() as ISymbol;
                            this.method_2(symbol2, color);
                        }
                        else
                        {
                            symbol2 = (item2.Item as IClone).Clone() as ISymbol;
                        }
                        renderer.AddValue(item.SubItems[0].Text, null, symbol2);
                    }
                    (data as IGeoFeatureLayer).Renderer = renderer as IFeatureRenderer;
                    (this.imap_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, data, null);
                    return renderer;
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
            }
            return renderer;
        }

        private void MatchStyleGrallyControl_Load(object sender, EventArgs e)
        {
            if (this.m_pSG != null)
            {
                for (int i = 0; i < (this.m_pSG as IStyleGalleryStorage).FileCount; i++)
                {
                    this.cboStyleGrally.Properties.Items.Add((this.m_pSG as IStyleGalleryStorage).get_File(i));
                }
            }
            if (this.cboStyleGrally.Properties.Items.Count > 0)
            {
                this.cboStyleGrally.SelectedIndex = 0;
            }
            this.method_0();
        }

        private void method_0()
        {
            if (this.imap_0 != null)
            {
                for (int i = 0; i < this.imap_0.LayerCount; i++)
                {
                    IFeatureLayer layer = this.imap_0.get_Layer(i) as IFeatureLayer;
                    if (layer != null)
                    {
                        this.cboLayers.Properties.Items.Add(new LayerObject(layer));
                    }
                }
                if (this.cboLayers.Properties.Items.Count > 0)
                {
                    this.cboLayers.SelectedIndex = 0;
                }
            }
        }

        private void method_1(object sender, EventArgs e)
        {
        }

        private void method_2(ISymbol isymbol_0, IColor icolor_0)
        {
            if (isymbol_0 is IMarkerSymbol)
            {
                (isymbol_0 as IMarkerSymbol).Color = icolor_0;
            }
            else if (isymbol_0 is ILineSymbol)
            {
                (isymbol_0 as ILineSymbol).Color = icolor_0;
            }
            else if (isymbol_0 is IFillSymbol)
            {
                (isymbol_0 as IFillSymbol).Color = icolor_0;
            }
        }

        private ISymbol method_3(esriGeometryType esriGeometryType_0)
        {
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                    return new SimpleMarkerSymbolClass();

                case esriGeometryType.esriGeometryMultipoint:
                    return new SimpleMarkerSymbolClass();

                case esriGeometryType.esriGeometryPolyline:
                    return new SimpleLineSymbolClass();

                case esriGeometryType.esriGeometryPolygon:
                    return new SimpleFillSymbolClass();
            }
            return null;
        }

        private string method_4(esriGeometryType esriGeometryType_0)
        {
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                    return "Marker Symbols";

                case esriGeometryType.esriGeometryMultipoint:
                    return "Marker Symbols";

                case esriGeometryType.esriGeometryPolyline:
                    return "Line Symbols";

                case esriGeometryType.esriGeometryPolygon:
                    return "Fill Symbols";
            }
            return null;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmAddValues values = new frmAddValues();
            IFeatureLayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
            IFields fields = layer.FeatureClass.Fields;
            IField field = fields.get_Field(fields.FindFieldByAliasName(this.cboFields.SelectedItem.ToString()));
            values.Layer = (this.cboLayers.SelectedItem as LayerObject).Layer;
            values.FieldName = field.Name;
            values.List = this.ilist_0 as ArrayList;
            values.List = this.ilist_0 as ArrayList;
            values.GetAllValues = this.bool_0;
            if (values.ShowDialog() == DialogResult.OK)
            {
                string[] items = new string[3];
                IQueryFilter queryFilter = new QueryFilterClass();
                for (int i = 0; i < values.SelectedItems.Count; i++)
                {
                    items[0] = values.SelectedItems[i].ToString();
                    items[1] = values.SelectedItems[i].ToString();
                    queryFilter.WhereClause = field.Name + " = " +
                                              this.ConvertFieldValueToString(field.Type, values.SelectedItems[i]);
                    items[2] = layer.FeatureClass.FeatureCount(queryFilter).ToString();
                    ListViewItem item = new ListViewItem(items);
                    this.listView1.Items.Add(item);
                }
            }
            this.bool_0 = values.GetAllValues;
        }

        public IFeatureLayer FeatureLayer
        {
            set { this.ifeatureLayer_0 = value; }
        }

        public IMap FocusMap
        {
            set { this.imap_0 = value; }
        }
    }
}