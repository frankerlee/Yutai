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
    internal partial class TinUniqueRenderPropertyPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 10;
        private IStyleGallery istyleGallery_0 = null;
        private ITinLayer itinLayer_0 = null;

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
            frmAddValues values = new frmAddValues
            {
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
                        ISymbol symbol =
                            this.itinUniqueValueRenderer_0.get_Symbol(this.itinUniqueValueRenderer_0.get_Value(i));
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
                    (this.itinLayer_0.Dataset as ITinAdvanced2).GetCountedUniqueTagValues(
                        esriTinElementType.esriTinTriangle, out array, out array2);
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
                    IMarkerSymbol symbol = new SimpleMarkerSymbolClass
                    {
                        Size = 3.0
                    };
                    return (symbol as ISymbol);
                }
                case esriGeometryType.esriGeometryPolyline:
                {
                    ILineSymbol symbol2 = new SimpleLineSymbolClass
                    {
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
                IRandomColorRamp ramp = new RandomColorRampClass
                {
                    StartHue = 40,
                    EndHue = 120,
                    MinValue = 65,
                    MaxValue = 90,
                    MinSaturation = 25,
                    MaxSaturation = 45,
                    Size = 5,
                    Seed = 23
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
            set { this.itinLayer_0 = value as ITinLayer; }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
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