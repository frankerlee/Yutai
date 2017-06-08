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
    internal class FindControl : UserControl
    {
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayers;
        private ComboBoxEdit cboSearchString;
        private CheckEdit chkContains;
        private Container container_0 = null;
        private IActiveViewEvents_Event iactiveViewEvents_Event_0;
        private ILayer ilayer_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        public string m_strFindField = "";
        public string m_strFindLayers = "";
        public string m_strSearch = "";
        private RadioGroup radioGroup;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.chkContains = new CheckEdit();
            this.label3 = new Label();
            this.radioGroup = new RadioGroup();
            this.cboSearchString = new ComboBoxEdit();
            this.cboLayers = new ComboBoxEdit();
            this.cboFields = new ComboBoxEdit();
            this.chkContains.Properties.BeginInit();
            this.radioGroup.Properties.BeginInit();
            this.cboSearchString.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x2c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "图层:";
            this.chkContains.Location = new Point(8, 0x48);
            this.chkContains.Name = "chkContains";
            this.chkContains.Properties.Caption = "模糊查找";
            this.chkContains.Size = new Size(160, 0x13);
            this.chkContains.TabIndex = 2;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(200, 11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 5;
            this.label3.Text = "搜索:";
            this.radioGroup.Location = new Point(240, 8);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Size = new Size(0xa8, 0x48);
            this.radioGroup.TabIndex = 6;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.cboSearchString.EditValue = "";
            this.cboSearchString.Location = new Point(0x38, 8);
            this.cboSearchString.Name = "cboSearchString";
            this.cboSearchString.Size = new Size(0x80, 0x17);
            this.cboSearchString.TabIndex = 12;
            this.cboSearchString.TextChanged += new EventHandler(this.cboSearchString_TextChanged);
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(0x38, 40);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Size = new Size(0x80, 0x17);
            this.cboLayers.TabIndex = 13;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x130, 0x20);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x80, 0x17);
            this.cboFields.TabIndex = 14;
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.cboSearchString);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.chkContains);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "FindControl";
            base.Size = new Size(0x1f0, 0x68);
            base.Load += new EventHandler(this.FindControl_Load);
            this.chkContains.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.cboSearchString.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
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

