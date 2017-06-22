using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Events;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public class FindControlEx : UserControl, IDockContent
    {
        private bool bool_0 = false;
        private SimpleButton btnCancel;
        private SimpleButton btnFind;
        private SimpleButton btnStop;
        private ComboBoxEdit cboFields;
        private ComboBoxEdit cboLayers;
        private ComboBoxEdit cboSearchString;
        private CheckEdit chkContains;
        private Container container_0 = null;
        private IActiveViewEvents_Event iactiveViewEvents_Event_0;
        private IApplication iapplication_0 = null;
        private ILayer ilayer_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        internal string m_strFindField = "";
        internal string m_strFindLayers = "";
        internal string m_strSearch = "";
        private RadioGroup radioGroup;

        public FindControlEx()
        {
            this.method_0();
            this.cboFields.Enabled = false;
            base.Name = "FindControl";
            this.Text = "查找";
        }

        internal void AddSearchStingToComboBox()
        {
            if ((this.cboSearchString.Text.Length > 0) && (this.cboSearchString.SelectedIndex == -1))
            {
                this.cboSearchString.Properties.Items.Add(this.cboSearchString.Text);
                this.cboSearchString.SelectedIndex = this.cboSearchString.Properties.Items.Count - 1;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.btnFind.Enabled = false;
            this.btnStop.Enabled = true;
            IArray array = this.Find();
            FindResultControlEx ex = new FindResultControlEx {
                Caption = "在" + this.m_strFindLayers + "的" + this.m_strFindField + "中查找" + this.m_strSearch + "的结果",
                FindResults = array,
                FocusMap = this.iapplication_0.FocusMap as IBasicMap
            };
            this.iapplication_0.DockWindows(ex, null);
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.btnFind.Enabled = true;
            this.btnStop.Enabled = false;
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboFields.Properties.Items.Clear();
            if (this.cboLayers.SelectedIndex >= 0)
            {
                IFeatureLayer layer;
                IFields fields;
                int num2;
                IField field;
                if (this.cboLayers.SelectedIndex == 0)
                {
                    this.bool_0 = true;
                    this.ilayer_0 = null;
                    for (int i = 1; i < this.cboLayers.Properties.Items.Count; i++)
                    {
                        layer = (this.cboLayers.Properties.Items[i] as LayerObject).Layer as IFeatureLayer;
                        if (layer.FeatureClass != null)
                        {
                            fields = layer.FeatureClass.Fields;
                            num2 = 0;
                            while (num2 < fields.FieldCount)
                            {
                                field = fields.get_Field(num2);
                                if ((((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (this.cboFields.Properties.Items.IndexOf(field.AliasName) == -1))
                                {
                                    this.cboFields.Properties.Items.Add(field.AliasName);
                                }
                                num2++;
                            }
                        }
                    }
                }
                else
                {
                    this.bool_0 = false;
                    layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFeatureLayer;
                    this.ilayer_0 = layer;
                    fields = layer.FeatureClass.Fields;
                    if (layer.FeatureClass != null)
                    {
                        for (num2 = 0; num2 < fields.FieldCount; num2++)
                        {
                            field = fields.get_Field(num2);
                            if (((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                            {
                                this.cboFields.Properties.Items.Add(field.AliasName);
                            }
                        }
                    }
                }
                if (this.cboFields.Properties.Items.Count > 0)
                {
                    this.cboFields.SelectedIndex = 0;
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public IArray Find()
        {
            IFind layer;
            IArray array3;
            if (this.cboLayers.SelectedIndex < 0)
            {
                return null;
            }
            this.AddSearchStingToComboBox();
            IArray array2 = new ArrayClass();
            if (this.bool_0)
            {
                for (int i = 1; i < this.cboLayers.Properties.Items.Count; i++)
                {
                    layer = (this.cboLayers.Properties.Items[i] as LayerObject).Layer as IFind;
                    this.m_strFindLayers = "所有图层";
                    array3 = this.Find(layer);
                    if (array3 != null)
                    {
                        array2.Add(array3);
                    }
                }
                if (array2.Count == 0)
                {
                    return null;
                }
                return array2;
            }
            layer = (this.cboLayers.SelectedItem as LayerObject).Layer as IFind;
            this.m_strFindLayers = this.cboLayers.Text;
            array3 = this.Find(layer);
            if (array3 != null)
            {
                array2.Add(array3);
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
            try
            {
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
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, null);
            }
            return array;
        }

        private void FindControlEx_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void method_0()
        {
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.radioGroup = new RadioGroup();
            this.chkContains = new CheckEdit();
            this.btnStop = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnFind = new SimpleButton();
            this.cboSearchString = new ComboBoxEdit();
            this.cboLayers = new ComboBoxEdit();
            this.cboFields = new ComboBoxEdit();
            this.radioGroup.Properties.BeginInit();
            this.chkContains.Properties.BeginInit();
            this.cboSearchString.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.label3.AutoSize = true;
            this.label3.Location = new Point(200, 8);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "搜索:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "图层:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "查找:";
            this.radioGroup.Location = new Point(240, 8);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "所有字段"), new RadioGroupItem(null, "字段"), new RadioGroupItem(null, "图层主显示字段") });
            this.radioGroup.Size = new Size(168, 72);
            this.radioGroup.TabIndex = 23;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.chkContains.Location = new Point(16, 72);
            this.chkContains.Name = "chkContains";
            this.chkContains.Properties.Caption = "模糊查询";
            this.chkContains.Size = new Size(144, 19);
            this.chkContains.TabIndex = 24;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new Point(456, 40);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(56, 24);
            this.btnStop.TabIndex = 28;
            this.btnStop.Text = "停止";
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(456, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "取消";
            this.btnCancel.Visible = false;
            this.btnFind.Location = new Point(456, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(56, 24);
            this.btnFind.TabIndex = 26;
            this.btnFind.Text = "查找";
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.cboSearchString.EditValue = "";
            this.cboSearchString.Location = new Point(48, 7);
            this.cboSearchString.Name = "cboSearchString";
            this.cboSearchString.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSearchString.Size = new Size(128, 23);
            this.cboSearchString.TabIndex = 32;
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(48, 39);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayers.Size = new Size(128, 23);
            this.cboLayers.TabIndex = 33;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(296, 32);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(128, 23);
            this.cboFields.TabIndex = 34;
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.cboSearchString);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnFind);
            base.Controls.Add(this.chkContains);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "FindControlEx";
            base.Size = new Size(528, 112);
            base.Load += new EventHandler(this.FindControlEx_Load);
            this.radioGroup.Properties.EndInit();
            this.chkContains.Properties.EndInit();
            this.cboSearchString.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_1()
        {
            this.method_3();
        }

        private void method_10()
        {
            this.method_3();
        }

        private void method_11(object object_0)
        {
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.ItemAdded-=(new IActiveViewEvents_ItemAddedEventHandler(this.method_5));
                    this.iactiveViewEvents_Event_0.ItemDeleted-=(new IActiveViewEvents_ItemDeletedEventHandler(this.method_6));
                    this.iactiveViewEvents_Event_0.ItemReordered-=(new IActiveViewEvents_ItemReorderedEventHandler(this.method_7));
                    this.iactiveViewEvents_Event_0.ContentsCleared-=(new IActiveViewEvents_ContentsClearedEventHandler(this.method_8));
                }
                catch
                {
                }
            }
            this.imap_0 = this.iapplication_0.FocusMap;
            this.iactiveViewEvents_Event_0 = this.imap_0 as IActiveViewEvents_Event;
            if (this.iactiveViewEvents_Event_0 != null)
            {
                try
                {
                    this.iactiveViewEvents_Event_0.ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.method_5));
                    this.iactiveViewEvents_Event_0.ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.method_6));
                    this.iactiveViewEvents_Event_0.ItemReordered+=(new IActiveViewEvents_ItemReorderedEventHandler(this.method_7));
                    this.iactiveViewEvents_Event_0.ContentsCleared+=(new IActiveViewEvents_ContentsClearedEventHandler(this.method_8));
                }
                catch
                {
                }
                this.method_1();
            }
        }

        private void method_2(ICompositeLayer icompositeLayer_0, ref int int_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer, ref int_0);
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

        private void method_3()
        {
            this.cboLayers.Properties.Items.Clear();
            this.cboLayers.Properties.Items.Add("所有图层");
            int num = 0;
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer, ref num);
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

        private void method_4(object sender, EventArgs e)
        {
        }

        private void method_5(object object_0)
        {
            this.method_3();
        }

        private void method_6(object object_0)
        {
            this.method_3();
        }

        private void method_7(object object_0, int int_0)
        {
            this.method_3();
        }

        private void method_8()
        {
            this.method_3();
        }

        private void method_9()
        {
            this.method_3();
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

        public IApplication Application
        {
            set
            {
                this.iapplication_0 = value;
                (this.iapplication_0 as IApplicationEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_11);
                (this.iapplication_0 as IApplicationEvents).OnMapDocumentChangedEvent += new OnMapDocumentChangedEventHandler(this.method_9);
                this.imap_0 = this.iapplication_0.FocusMap;
                this.iactiveViewEvents_Event_0 = this.imap_0 as IActiveViewEvents_Event;
                this.iactiveViewEvents_Event_0.ItemAdded+=(new IActiveViewEvents_ItemAddedEventHandler(this.method_5));
                this.iactiveViewEvents_Event_0.ItemDeleted+=(new IActiveViewEvents_ItemDeletedEventHandler(this.method_6));
                this.iactiveViewEvents_Event_0.ItemReordered+=(new IActiveViewEvents_ItemReorderedEventHandler(this.method_7));
                this.iactiveViewEvents_Event_0.ContentsChanged+=(new IActiveViewEvents_ContentsChangedEventHandler(this.method_10));
                this.iactiveViewEvents_Event_0.ContentsCleared+=(new IActiveViewEvents_ContentsClearedEventHandler(this.method_8));
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Bottom;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }
    }
}

