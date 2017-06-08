using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.UI
{
    public class LayerFieldsPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBoxEdit cboFields;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0;
        private ILayerFields ilayerFields_0 = null;
        private Label label1;
        private Label label2;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private LVColumnHeader lvcolumnHeader_3;
        private LVColumnHeader lvcolumnHeader_4;

        public LayerFieldsPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.cboFields.SelectedIndex >= 0)
                {
                    if (this.ilayer_0 is IFeatureLayer)
                    {
                        (this.ilayer_0 as IFeatureLayer).DisplayField = this.cboFields.Text;
                    }
                    else if (this.ilayer_0 is ITinLayer)
                    {
                        (this.ilayer_0 as ITinLayer).DisplayField = this.cboFields.Text;
                    }
                }
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    IFieldInfo info = this.ilayerFields_0.get_FieldInfo(i);
                    string text = item.SubItems[1].Text;
                    if (text != info.Alias)
                    {
                        info.Alias = text;
                    }
                    info.Visible = item.Checked;
                }
            }
            return true;
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.lvcolumnHeader_3 = new LVColumnHeader();
            this.lvcolumnHeader_4 = new LVColumnHeader();
            this.cboFields = new ComboBoxEdit();
            this.label2 = new Label();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x2c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设置字段可见性";
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2, this.lvcolumnHeader_3, this.lvcolumnHeader_4 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(3, 0x3b);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x1c7, 0xb7);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "字段名称";
            this.lvcolumnHeader_0.Width = 0x95;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "别名";
            this.lvcolumnHeader_1.Width = 0x6c;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_2.Text = "类型";
            this.lvcolumnHeader_3.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_3.Text = "长度";
            this.lvcolumnHeader_4.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_4.Text = "精度";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x57, 5);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x90, 0x15);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主显示字段";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cboFields);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Name = "LayerFieldsPage";
            base.Size = new Size(0x1d8, 0x102);
            base.Load += new EventHandler(this.LayerFieldsPage_Load);
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LayerFieldsPage_Load(object sender, EventArgs e)
        {
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            if (this.ilayerFields_0 != null)
            {
                int num = -1;
                string displayField = "";
                if (this.ilayer_0 is IFeatureLayer)
                {
                    displayField = (this.ilayer_0 as IFeatureLayer).DisplayField;
                }
                else if (this.ilayer_0 is ITinLayer)
                {
                    displayField = (this.ilayer_0 as ITinLayer).DisplayField;
                }
                try
                {
                    string[] items = new string[5];
                    for (int i = 0; i < this.ilayerFields_0.FieldCount; i++)
                    {
                        IField field = this.ilayerFields_0.get_Field(i);
                        IFieldInfo info = this.ilayerFields_0.get_FieldInfo(i);
                        items[0] = field.Name;
                        items[1] = info.Alias;
                        items[2] = this.method_0(field.Type);
                        items[3] = field.Length.ToString();
                        items[4] = field.Precision.ToString();
                        ListViewItem item = new ListViewItem(items) {
                            Checked = info.Visible
                        };
                        this.listView1.Items.Add(item);
                        if (((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeBlob))
                        {
                            this.cboFields.Properties.Items.Add(field.Name);
                            if (field.Name == displayField)
                            {
                                num = this.cboFields.Properties.Items.Count - 1;
                            }
                        }
                    }
                }
                catch
                {
                }
                this.listView1.ValueChanged += new ValueChangedHandler(this.method_2);
                if (displayField.Length == 0)
                {
                    this.cboFields.Enabled = false;
                    this.cboFields.Text = "";
                }
                else
                {
                    this.cboFields.Enabled = true;
                    this.cboFields.SelectedIndex = num;
                }
                this.bool_1 = true;
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private string method_0(esriFieldType esriFieldType_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "短整形";

                case esriFieldType.esriFieldTypeInteger:
                    return "整形";

                case esriFieldType.esriFieldTypeSingle:
                    return "单精度";

                case esriFieldType.esriFieldTypeDouble:
                    return "双精度";

                case esriFieldType.esriFieldTypeString:
                    return "字符串";

                case esriFieldType.esriFieldTypeDate:
                    return "日期";

                case esriFieldType.esriFieldTypeOID:
                    return "OID";

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何对象";

                case esriFieldType.esriFieldTypeBlob:
                    return "二进制";

                case esriFieldType.esriFieldTypeRaster:
                    return "栅格";

                case esriFieldType.esriFieldTypeGUID:
                    return "GUID";

                case esriFieldType.esriFieldTypeGlobalID:
                    return "GlobalID";

                case esriFieldType.esriFieldTypeXML:
                    return "XML";
            }
            return "未知类型";
        }

        private ITable method_1()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_2(object sender, ValueChangedEventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayerFields_0 = value as ILayerFields;
                this.ilayer_0 = value as ILayer;
            }
        }
    }
}

