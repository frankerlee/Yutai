using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class FindControl : UserControl
    {
        private Container container_0 = null;
        private ILayer ilayer_0 = null;
        private IMap imap_0 = null;
        public string m_strFindField = "";
        public string m_strFindLayers = "";
        public string m_strSearch = "";

        public FindControl()
        {
            this.InitializeComponent();
            this.cboFields.Enabled = false;
        }

        public void AddSearchStingToComboBox()
        {
            if ((this.cboSearchString.Text.Length > 0) && (this.cboSearchString.SelectedIndex == -1))
            {
                this.cboSearchString.Properties.Items.Add(this.cboSearchString.Text);
                this.cboSearchString.SelectedIndex = this.cboSearchString.Properties.Items.Count - 1;
            }
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedIndex >= 0)
            {
                IFeatureLayer layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
                this.ilayer_0 = layer;
                IFields fields = layer.FeatureClass.Fields;
                this.cboFields.Properties.Items.Clear();
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        this.cboFields.Properties.Items.Add(field.AliasName);
                    }
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
        }

        private void cboSearchString_TextChanged(object sender, EventArgs e)
        {
        }

 public IArray Find()
        {
            if (this.cboLayers.SelectedIndex < 0)
            {
                return null;
            }
            this.AddSearchStingToComboBox();
            IArray array2 = new ArrayClass();
            IFind layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFind;
            this.m_strFindLayers = this.cboLayers.Text;
            IArray unk = this.Find(layer);
            if (unk != null)
            {
                array2.Add(unk);
            }
            if (array2.Count == 0)
            {
                return null;
            }
            return array2;
        }

        public IArray Find(IFind ifind_0)
        {
            IArray array = null;
            bool contains = this.chkContains.Checked;
            string text = this.cboSearchString.Text;
            this.m_strSearch = this.cboSearchString.Text;
            string[] fields = new string[1];
            switch (this.radioGroup.SelectedIndex)
            {
                case 0:
                    array = ifind_0.Find(text, contains, ifind_0.FindFields, null);
                    this.m_strFindField = "所有字段";
                    return array;

                case 1:
                    fields[0] = this.cboFields.Text;
                    array = ifind_0.Find(text, contains, fields, null);
                    this.m_strFindField = this.cboFields.Text;
                    return array;
            }
            fields[0] = ifind_0.FindDisplayField;
            array = ifind_0.Find(text, contains, fields, null);
            this.m_strFindField = ifind_0.FindDisplayField;
            return array;
        }

        private void FindControl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            this.method_2();
        }

        private void method_1(ICompositeLayer icompositeLayer_0, ref int int_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer, ref int_0);
                }
                else if (layer is IFeatureLayer)
                {
                    this.cboLayers.Properties.Items.Add(new LayerObject(layer));
                    if (this.ilayer_0 == layer)
                    {
                        int_0 = this.cboLayers.Properties.Items.Count - 1;
                    }
                }
            }
        }

        private void method_2()
        {
            this.cboLayers.Properties.Items.Clear();
            int num = 0;
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_1(layer as ICompositeLayer, ref num);
                }
                else if (layer is IFeatureLayer)
                {
                    this.cboLayers.Properties.Items.Add(new LayerObject(layer));
                    if (this.ilayer_0 == layer)
                    {
                        num = this.cboLayers.Properties.Items.Count - 1;
                    }
                }
            }
            if (this.cboLayers.Properties.Items.Count > 0)
            {
                this.cboLayers.SelectedIndex = num;
            }
        }

        private void method_3(object object_0)
        {
            this.method_2();
        }

        private void method_4(object object_0)
        {
            this.method_2();
        }

        private void method_5(object object_0, int int_0)
        {
            this.method_2();
        }

        private void method_6()
        {
            this.method_2();
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup.SelectedIndex == 1)
            {
                this.cboFields.Properties.Enabled = true;
            }
            else
            {
                this.cboFields.Properties.Enabled = false;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.imap_0 = value;
                this.iactiveViewEvents_Event_0 = value as IActiveViewEvents_Event;
                this.iactiveViewEvents_Event_0.ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.method_3));
                this.iactiveViewEvents_Event_0.ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.method_4));
                this.iactiveViewEvents_Event_0.ItemReordered+=(new IActiveViewEvents_ItemReorderedEventHandler(this.method_5));
                this.iactiveViewEvents_Event_0.ContentsCleared+=(new IActiveViewEvents_ContentsClearedEventHandler(this.method_6));
            }
        }
    }
}

