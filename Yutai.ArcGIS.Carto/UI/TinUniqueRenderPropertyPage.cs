using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class TinUniqueRenderPropertyPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnAddAllValues;
        private SimpleButton btnAddValue;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private StyleComboBox cboColorRamp;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 10;
        private IStyleGallery istyleGallery_0 = null;
        private ITinLayer itinLayer_0 = null;
        private ITinUniqueValueRenderer itinUniqueValueRenderer_0;
        private Label lblLabelInfo;
        private RenderInfoListView listView1;

        public TinUniqueRenderPropertyPage()
        {
            this.InitializeComponent();
            this.listView1.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_0);
            this.listView1.SetColumnEditable(2, true);
        }

        public void Apply()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void btnAddAllValues_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.itinUniqueValueRenderer_0.RemoveAllValues();
            this.method_1();
            this.btnAddValue.Enabled = false;
            object[] objArray = new object[4];
            ISymbol defaultSymbol = this.itinUniqueValueRenderer_0.DefaultSymbol;
            int num = 0;
            while (true)
            {
                if (num >= this.ilist_0.Count)
                {
                    break;
                }
                try
                {
                    ISymbol symbol2 = (defaultSymbol as IClone).Clone() as ISymbol;
                    IColor color = this.ienumColors_0.Next();
                    if (color == null)
                    {
                        this.ienumColors_0.Reset();
                        color = this.ienumColors_0.Next();
                    }
                    this.method_3(symbol2, color);
                    objArray[0] = symbol2;
                    objArray[1] = this.ilist_0[num].ToString();
                    objArray[2] = this.ilist_0[num].ToString();
                    objArray[3] = "?";
                    this.itinUniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol2);
                    this.listView1.Add(objArray);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
                num++;
            }
            this.ilist_0.Clear();
        }

        private void btnAddValue_Click(object sender, EventArgs e)
        {
            frmAddValues values = new frmAddValues {
                List = this.ilist_0 as ArrayList,
                GetAllValues = true
            };
            if (values.ShowDialog() == DialogResult.OK)
            {
                ISymbol defaultSymbol = this.itinUniqueValueRenderer_0.DefaultSymbol;
                object[] objArray = new object[4];
                for (int i = 0; i < values.SelectedItems.Count; i++)
                {
                    ISymbol symbol2 = (defaultSymbol as IClone).Clone() as ISymbol;
                    IColor color = this.ienumColors_0.Next();
                    if (color == null)
                    {
                        this.ienumColors_0.Reset();
                        color = this.ienumColors_0.Next();
                    }
                    this.method_3(symbol2, color);
                    objArray[0] = symbol2;
                    objArray[1] = values.SelectedItems[i].ToString();
                    objArray[2] = values.SelectedItems[i].ToString();
                    objArray[3] = "?";
                    this.itinUniqueValueRenderer_0.AddValue(objArray[1].ToString(), null, symbol2);
                    this.listView1.Add(objArray);
                }
                this.btnAddValue.Enabled = this.ilist_0.Count > 0;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                index = this.listView1.SelectedIndices[i];
                this.listView1.Items.RemoveAt(index);
                string str = this.itinUniqueValueRenderer_0.get_Value(index);
                this.itinUniqueValueRenderer_0.RemoveValue(str);
                this.ilist_0.Add(str);
            }
            index = this.listView1.SelectedIndices.Count;
            this.btnAddValue.Enabled = this.ilist_0.Count > 0;
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            this.itinUniqueValueRenderer_0.RemoveAllValues();
            this.method_1();
            this.btnAddValue.Enabled = true;
        }

        private void cboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.itinUniqueValueRenderer_0.ColorScheme = this.cboColorRamp.Text;
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                if (this.icolorRamp_0 != null)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.int_0;
                    this.icolorRamp_0.CreateRamp(out flag);
                    this.ienumColors_0 = this.icolorRamp_0.Colors;
                    this.ienumColors_0.Reset();
                    IColor color = null;
                    for (int i = 0; i < this.itinUniqueValueRenderer_0.ValueCount; i++)
                    {
                        ISymbol symbol = this.itinUniqueValueRenderer_0.get_Symbol(this.itinUniqueValueRenderer_0.get_Value(i));
                        color = this.ienumColors_0.Next();
                        if (color == null)
                        {
                            this.ienumColors_0.Reset();
                            color = this.ienumColors_0.Next();
                        }
                        this.method_3(symbol, color);
                        this.itinUniqueValueRenderer_0.set_Symbol(this.itinUniqueValueRenderer_0.get_Value(i), symbol);
                        this.listView1.Items[i].Tag = symbol;
                    }
                    this.listView1.Invalidate();
                }
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
            this.icontainer_0 = new Container();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.btnDeleteAll = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAddValue = new SimpleButton();
            this.btnAddAllValues = new SimpleButton();
            this.cboColorRamp = new StyleComboBox(this.icontainer_0);
            this.groupBox1 = new GroupBox();
            this.lblLabelInfo = new Label();
            this.groupBox2 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 0x39);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x170, 160);
            this.listView1.TabIndex = 0x13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "符号";
            this.columnHeader_1.Text = "值";
            this.columnHeader_1.Width = 0x66;
            this.columnHeader_2.Text = "标注";
            this.columnHeader_2.Width = 0x77;
            this.columnHeader_3.Text = "数目";
            this.columnHeader_3.Width = 0x3f;
            this.btnDeleteAll.Location = new System.Drawing.Point(290, 0xdf);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 0x18);
            this.btnDeleteAll.TabIndex = 0x12;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(210, 0xdf);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(80, 0x18);
            this.btnDelete.TabIndex = 0x11;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAddValue.Location = new System.Drawing.Point(0x8a, 0xdf);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new Size(0x48, 0x18);
            this.btnAddValue.TabIndex = 0x10;
            this.btnAddValue.Text = "添加值";
            this.btnAddValue.Click += new EventHandler(this.btnAddValue_Click);
            this.btnAddAllValues.Location = new System.Drawing.Point(0x36, 0xdf);
            this.btnAddAllValues.Name = "btnAddAllValues";
            this.btnAddAllValues.Size = new Size(80, 0x18);
            this.btnAddAllValues.TabIndex = 15;
            this.btnAddAllValues.Text = "添加所有值";
            this.btnAddAllValues.Click += new EventHandler(this.btnAddAllValues_Click);
            this.cboColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboColorRamp.DropDownWidth = 160;
            this.cboColorRamp.Location = new System.Drawing.Point(8, 0x12);
            this.cboColorRamp.Name = "cboColorRamp";
            this.cboColorRamp.Size = new Size(0xb0, 0x16);
            this.cboColorRamp.TabIndex = 14;
            this.cboColorRamp.SelectedIndexChanged += new EventHandler(this.cboColorRamp_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.lblLabelInfo);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xac, 0x30);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签值字段";
            this.lblLabelInfo.AutoSize = true;
            this.lblLabelInfo.Location = new System.Drawing.Point(12, 0x15);
            this.lblLabelInfo.Name = "lblLabelInfo";
            this.lblLabelInfo.Size = new Size(0, 12);
            this.lblLabelInfo.TabIndex = 0;
            this.groupBox2.Controls.Add(this.cboColorRamp);
            this.groupBox2.Location = new System.Drawing.Point(0xb5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(190, 0x30);
            this.groupBox2.TabIndex = 0x15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "颜色配置";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddValue);
            base.Controls.Add(this.btnAddAllValues);
            base.Name = "TinUniqueRenderPropertyPage";
            base.Size = new Size(0x185, 0x102);
            base.Load += new EventHandler(this.TinUniqueRenderPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
                if (this.listView1.SelectedIndices.Count != 1)
                {
                }
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        private void method_0(int int_1, object object_0)
        {
            ListViewItem item = this.listView1.Items[int_1];
            string text = item.SubItems[1].Text;
            if (object_0 is ISymbol)
            {
                this.itinUniqueValueRenderer_0.set_Symbol(text, object_0 as ISymbol);
            }
            else if (object_0 is string)
            {
                this.itinUniqueValueRenderer_0.set_Label(text, object_0.ToString());
            }
        }

        private void method_1()
        {
            this.ilist_0.Clear();
            if ((this.itinUniqueValueRenderer_0 as ITinRenderer).Name == "Edge types")
            {
                this.ilist_0.Add("0");
                this.ilist_0.Add("1");
                this.ilist_0.Add("2");
                this.ilist_0.Add("3");
            }
            else
            {
                ITinAdvanced2 dataset;
                ILongArray array;
                ILongArray array2;
                int num;
                if ((this.itinUniqueValueRenderer_0 as ITinRenderer).Name == "Node tag")
                {
                    dataset = this.itinLayer_0.Dataset as ITinAdvanced2;
                    dataset.GetCountedUniqueTagValues(esriTinElementType.esriTinNode, out array, out array2);
                    for (num = 0; num < array.Count; num++)
                    {
                        this.ilist_0.Add(array.get_Element(num).ToString());
                    }
                }
                else if ((this.itinUniqueValueRenderer_0 as ITinRenderer).Name == "Edge tag")
                {
                    dataset = this.itinLayer_0.Dataset as ITinAdvanced2;
                    dataset.GetCountedUniqueTagValues(esriTinElementType.esriTinEdge, out array, out array2);
                    for (num = 0; num < array.Count; num++)
                    {
                        this.ilist_0.Add(array.get_Element(num).ToString());
                    }
                }
                else if ((this.itinUniqueValueRenderer_0 as ITinRenderer).Name == "Triangle tag")
                {
                    (this.itinLayer_0.Dataset as ITinAdvanced2).GetCountedUniqueTagValues(esriTinElementType.esriTinTriangle, out array, out array2);
                    for (num = 0; num < array.Count; num++)
                    {
                        this.ilist_0.Add(array.get_Element(num).ToString());
                    }
                }
            }
        }

        private void method_2()
        {
            this.lblLabelInfo.Text = (this.itinUniqueValueRenderer_0 as ITinRenderer).Name;
            this.listView1.Items.Clear();
            this.btnDelete.Enabled = false;
            this.method_1();
            if (this.icolorRamp_0 != null)
            {
                bool flag;
                this.icolorRamp_0.Size = this.int_0;
                this.icolorRamp_0.CreateRamp(out flag);
                this.ienumColors_0 = this.icolorRamp_0.Colors;
                this.ienumColors_0.Reset();
            }
            object[] objArray = new object[4];
            for (int i = 0; i < this.itinUniqueValueRenderer_0.ValueCount; i++)
            {
                string str = this.itinUniqueValueRenderer_0.get_Value(i);
                int index = this.ilist_0.IndexOf(str);
                if (index != -1)
                {
                    this.ilist_0.RemoveAt(index);
                }
                objArray[0] = this.itinUniqueValueRenderer_0.get_Symbol(str);
                objArray[1] = str;
                objArray[2] = this.itinUniqueValueRenderer_0.get_Label(str);
                objArray[3] = "?";
                this.listView1.Add(objArray);
            }
            this.btnAddValue.Enabled = this.ilist_0.Count > 0;
        }

        private void method_3(ISymbol isymbol_0, IColor icolor_0)
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

        private ISymbol method_4(esriGeometryType esriGeometryType_0)
        {
            switch (esriGeometryType_0)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                {
                    IMarkerSymbol symbol = new SimpleMarkerSymbolClass {
                        Size = 3.0
                    };
                    return (symbol as ISymbol);
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ILineSymbol symbol2 = new SimpleLineSymbolClass {
                        Width = 0.2
                    };
                    return (symbol2 as ISymbol);
                }
                case esriGeometryType.esriGeometryPolygon:
                    return new SimpleFillSymbolClass();
            }
            return null;
        }

        private void TinUniqueRenderPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.istyleGallery_0 != null)
            {
                IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.cboColorRamp.Add(item2);
                }
                item = null;
                GC.Collect();
            }
            if (this.cboColorRamp.Items.Count == 0)
            {
                this.cboColorRamp.Enabled = false;
                IRandomColorRamp ramp = new RandomColorRampClass {
                    StartHue = 40,
                    EndHue = 120,
                    MinValue = 0x41,
                    MaxValue = 90,
                    MinSaturation = 0x19,
                    MaxSaturation = 0x2d,
                    Size = 5,
                    Seed = 0x17
                };
                this.icolorRamp_0 = ramp;
            }
            else
            {
                this.cboColorRamp.Text = this.itinUniqueValueRenderer_0.ColorScheme;
                if (this.cboColorRamp.SelectedIndex == -1)
                {
                    this.cboColorRamp.SelectedIndex = 0;
                    this.itinUniqueValueRenderer_0.ColorScheme = this.cboColorRamp.Text;
                }
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
            }
            this.method_2();
            this.bool_1 = true;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.itinLayer_0 = value as ITinLayer;
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }

        public ITinRenderer TinRenderer
        {
            set
            {
                this.itinUniqueValueRenderer_0 = value as ITinUniqueValueRenderer;
                if (this.bool_1)
                {
                    this.bool_1 = false;
                    this.method_2();
                    this.bool_1 = true;
                }
            }
        }
    }
}

