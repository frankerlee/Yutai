using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class RasterClassifiedRenderPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IRasterClassifyColorRampRenderer irasterClassifyColorRampRenderer_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IStyleGallery istyleGallery_0 = null;

        public RasterClassifiedRenderPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                IObjectCopy copy = new ObjectCopyClass();
                IRasterClassifyColorRampRenderer renderer1 =
                    copy.Copy(this.irasterClassifyColorRampRenderer_0) as IRasterClassifyColorRampRenderer;
                this.irasterLayer_0.Renderer = this.irasterClassifyColorRampRenderer_0 as IRasterRenderer;
            }
            catch (Exception)
            {
            }
        }

        private void cboClassifyMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            IClassify classify;
            if (this.bool_0)
            {
                classify = null;
                switch (this.cboClassifyMethod.SelectedIndex)
                {
                    case 0:
                        classify = new EqualIntervalClass();
                        goto Label_0043;

                    case 1:
                        classify = new QuantileClass();
                        goto Label_0043;

                    case 2:
                        classify = new NaturalBreaksClass();
                        goto Label_0043;
                }
            }
            return;
            Label_0043:
            (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ClassificationMethod =
                classify.ClassID;
            if (this.cboClassifyNum.SelectedIndex >= 0)
            {
                this.method_3();
            }
            else
            {
                this.cboClassifyNum.SelectedIndex = 4;
            }
        }

        private void cboClassifyNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_3();
            }
        }

        private void cboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp =
                    this.cboColorRamp.Text;
                if (this.irasterClassifyColorRampRenderer_0.ClassCount > 0)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.irasterClassifyColorRampRenderer_0.ClassCount;
                    this.icolorRamp_0.CreateRamp(out flag);
                    IEnumColors colors = this.icolorRamp_0.Colors;
                    colors.Reset();
                    this.listView1.BeginUpdate();
                    for (int i = 0; i < this.irasterClassifyColorRampRenderer_0.ClassCount; i++)
                    {
                        ISymbol symbol = this.irasterClassifyColorRampRenderer_0.get_Symbol(i);
                        if (symbol is IMarkerSymbol)
                        {
                            (symbol as IMarkerSymbol).Color = colors.Next();
                        }
                        else if (symbol is ILineSymbol)
                        {
                            (symbol as ILineSymbol).Color = colors.Next();
                        }
                        else if (symbol is IFillSymbol)
                        {
                            (symbol as IFillSymbol).Color = colors.Next();
                        }
                        this.irasterClassifyColorRampRenderer_0.set_Symbol(i, symbol);
                        if (this.listView1.Items[i] is ListViewItemEx)
                        {
                            (this.listView1.Items[i] as ListViewItemEx).Style = symbol;
                        }
                        this.listView1.Items[i].Tag = symbol;
                    }
                    this.listView1.EndUpdate();
                    this.listView1.Invalidate();
                }
            }
        }

        private void cboNormFields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboValueFields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).UseHillShade = this.checkBox1.Checked;
                this.lblZFactor.Enabled = this.checkBox1.Checked;
                this.txtZFactor.Enabled = this.checkBox1.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor noDataColor = (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
                if (noDataColor != null)
                {
                    this.method_8(this.colorEdit1, noDataColor);
                    (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor = noDataColor;
                }
            }
        }

        private object method_0(UID uid_0)
        {
            if (uid_0 == null)
            {
                return null;
            }
            try
            {
                Guid clsid = new Guid(uid_0.Value.ToString());
                return Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid));
            }
            catch
            {
                return null;
            }
        }

        private void method_1()
        {
            int num;
            this.cboValueFields.Properties.Items.Clear();
            this.cboValueFields.Properties.Items.Add("<VALUE>");
            this.cboNormFields.Properties.Items.Clear();
            this.cboNormFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Add("<百分比>");
            this.cboNormFields.Properties.Items.Add("<LOG>");
            IFields fields = null;
            ITable displayTable = (this.irasterLayer_0 as IDisplayTable).DisplayTable;
            if (displayTable != null)
            {
                fields = displayTable.Fields;
            }
            if (fields != null)
            {
                for (num = 0; num < fields.FieldCount; num++)
                {
                    IField field = fields.get_Field(num);
                    switch (field.Type)
                    {
                        case esriFieldType.esriFieldTypeDouble:
                        case esriFieldType.esriFieldTypeInteger:
                        case esriFieldType.esriFieldTypeSingle:
                        case esriFieldType.esriFieldTypeSmallInteger:
                            this.cboValueFields.Properties.Items.Add(new FieldWrap(field));
                            this.cboNormFields.Properties.Items.Add(new FieldWrap(field));
                            break;
                    }
                }
            }
            if ((this.irasterClassifyColorRampRenderer_0.ClassField == "<VALUE>") ||
                (this.irasterClassifyColorRampRenderer_0.ClassField == ""))
            {
                this.cboValueFields.SelectedIndex = 0;
            }
            else
            {
                num = 1;
                while (num < this.cboValueFields.Properties.Items.Count)
                {
                    if ((this.cboValueFields.Properties.Items[num] as FieldWrap).Name ==
                        this.irasterClassifyColorRampRenderer_0.ClassField)
                    {
                        this.cboValueFields.SelectedIndex = num;
                        break;
                    }
                    num++;
                }
            }
            this.cboNormFields.Text = this.irasterClassifyColorRampRenderer_0.NormField;
            if (this.cboValueFields.Properties.Items.Count == 1)
            {
                this.cboValueFields.Enabled = false;
                this.cboNormFields.Enabled = false;
            }
            object[] objArray = new object[3];
            this.listView1.Items.Clear();
            for (num = 0; num < this.irasterClassifyColorRampRenderer_0.ClassCount; num++)
            {
                objArray[0] = this.irasterClassifyColorRampRenderer_0.get_Symbol(num);
                if (num == 0)
                {
                    objArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(num).ToString("0.####");
                }
                else
                {
                    objArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(num - 1).ToString("0.####") + " - " +
                                  this.irasterClassifyColorRampRenderer_0.get_Break(num).ToString("0.####");
                }
                objArray[2] = this.irasterClassifyColorRampRenderer_0.get_Label(num);
                this.listView1.Add(objArray);
            }
            this.cboClassifyNum.SelectedIndex = this.irasterClassifyColorRampRenderer_0.ClassCount - 1;
            IClassify classify =
                this.method_0(
                        (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ClassificationMethod)
                    as
                    IClassify;
            if (classify != null)
            {
                this.Classifygroup.Enabled = true;
                switch (classify.MethodName)
                {
                    case "Equal Interval":
                    case "等间隔":
                        this.cboClassifyMethod.SelectedIndex = 0;
                        break;

                    case "Quantile":
                    case "分位数":
                        this.cboClassifyMethod.SelectedIndex = 1;
                        break;

                    case "Natural Breaks (Jenks)":
                    case "自然间隔(Jenks)":
                        this.cboClassifyMethod.SelectedIndex = 2;
                        break;
                }
            }
            else
            {
                this.Classifygroup.Enabled = false;
            }
            IColor noDataColor = (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
            if (noDataColor != null)
            {
                this.method_5(this.colorEdit1, noDataColor);
            }
            this.checkBox1.Checked = (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).UseHillShade;
            this.txtZFactor.Text = (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).ZScale.ToString();
            this.lblZFactor.Enabled = this.checkBox1.Checked;
            this.txtZFactor.Enabled = this.checkBox1.Checked;
        }

        private ITable method_2()
        {
            if (this.irasterLayer_0 == null)
            {
                return null;
            }
            if (this.irasterLayer_0 is IDisplayTable)
            {
                return (this.irasterLayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.irasterLayer_0 is IAttributeTable)
            {
                return (this.irasterLayer_0 as IAttributeTable).AttributeTable;
            }
            return (this.irasterLayer_0 as ITable);
        }

        private void method_3()
        {
            string text = this.cboNormFields.Text;
            string name = "";
            if (this.cboValueFields.SelectedIndex > 0)
            {
                name = (this.cboValueFields.SelectedItem as FieldWrap).Name;
            }
            else
            {
                name = "";
            }
            this.method_4(this.icolorRamp_0, this.irasterClassifyColorRampRenderer_0,
                this.cboClassifyNum.SelectedIndex + 1, this.method_2(), name, text);
            this.listView1.Items.Clear();
            string[] strArray = new string[3];
            int index = 0;
            while (true)
            {
                if (index >= this.irasterClassifyColorRampRenderer_0.ClassCount)
                {
                    break;
                }
                strArray[0] = "";
                if (index == 0)
                {
                    strArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(index).ToString();
                }
                else
                {
                    strArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(index - 1).ToString() + " - " +
                                  this.irasterClassifyColorRampRenderer_0.get_Break(index).ToString();
                }
                strArray[2] = this.irasterClassifyColorRampRenderer_0.get_Label(index);
                ListViewItemEx ex = new ListViewItemEx(strArray);
                try
                {
                    ex.Style = this.irasterClassifyColorRampRenderer_0.get_Symbol(index);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
                this.listView1.Add(ex);
                index++;
            }
            this.cboClassifyNum.SelectedIndex = this.irasterClassifyColorRampRenderer_0.ClassCount - 1;
        }

        private void method_4(IColorRamp icolorRamp_1,
            IRasterClassifyColorRampRenderer irasterClassifyColorRampRenderer_1, int int_0, ITable itable_0,
            string string_0, string string_1)
        {
            try
            {
                bool flag;
                if (string_0.Length > 0)
                {
                    irasterClassifyColorRampRenderer_1.ClassField = string_0;
                }
                if (string_1.Length > 0)
                {
                    irasterClassifyColorRampRenderer_1.NormField = string_1;
                }
                IRasterRenderer renderer = (IRasterRenderer) irasterClassifyColorRampRenderer_1;
                renderer.Raster = this.irasterLayer_0.Raster;
                irasterClassifyColorRampRenderer_1.ClassCount = int_0;
                renderer.Update();
                this.bool_0 = false;
                this.cboClassifyNum.SelectedIndex = irasterClassifyColorRampRenderer_1.ClassCount - 1;
                this.bool_0 = true;
                icolorRamp_1.Size = irasterClassifyColorRampRenderer_1.ClassCount;
                icolorRamp_1.CreateRamp(out flag);
                IEnumColors colors = icolorRamp_1.Colors;
                ISymbol symbol = null;
                for (int i = 0; i < irasterClassifyColorRampRenderer_1.ClassCount; i++)
                {
                    string str;
                    IColor color = colors.Next();
                    ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass
                    {
                        Color = color,
                        Style = esriSimpleFillStyle.esriSFSSolid
                    };
                    symbol = symbol2 as ISymbol;
                    irasterClassifyColorRampRenderer_1.set_Symbol(i, symbol);
                    if (i == (irasterClassifyColorRampRenderer_1.ClassCount - 1))
                    {
                        str = irasterClassifyColorRampRenderer_1.get_Break(i).ToString("0.####");
                    }
                    else
                    {
                        str = irasterClassifyColorRampRenderer_1.get_Break(i).ToString("0.####") + " - " +
                              irasterClassifyColorRampRenderer_1.get_Break(i + 1).ToString("0.####");
                    }
                    irasterClassifyColorRampRenderer_1.set_Label(i, str);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void method_5(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                uint rGB = (uint) icolor_0.RGB;
                this.method_6(rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_6(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 16711680;
            int_2 = (int) (num >> 16);
            num = uint_0 & 65280;
            int_1 = (int) (num >> 8);
            num = uint_0 & 255;
            int_0 = (int) num;
        }

        private int method_7(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |= (uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_8(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_7(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void RasterClassifiedRenderPage_Load(object sender, EventArgs e)
        {
            int num = -1;
            if (this.irasterClassifyColorRampRenderer_0 != null)
            {
                string colorRamp = (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp;
                if (this.istyleGallery_0 != null)
                {
                    IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                    item.Reset();
                    for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                    {
                        this.cboColorRamp.Add(item2);
                        if (item2.Name == colorRamp)
                        {
                            num = this.cboColorRamp.Items.Count - 1;
                        }
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
                    this.cboColorRamp.SelectedIndex = num;
                    if (this.cboColorRamp.SelectedIndex == -1)
                    {
                        this.cboColorRamp.SelectedIndex = 0;
                    }
                    this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                    (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp =
                        this.cboColorRamp.Text;
                }
                if (this.irasterLayer_0 != null)
                {
                    this.method_1();
                }
                this.bool_0 = true;
            }
        }

        private void txtZFactor_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).ZScale =
                        double.Parse(this.txtZFactor.Text);
                }
                catch
                {
                }
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.irasterLayer_0 = value as IRasterLayer;
                if (this.irasterLayer_0 == null)
                {
                    this.irasterClassifyColorRampRenderer_0 = null;
                }
                else
                {
                    IRasterClassifyColorRampRenderer pInObject =
                        this.irasterLayer_0.Renderer as IRasterClassifyColorRampRenderer;
                    if (pInObject == null)
                    {
                        if (this.irasterClassifyColorRampRenderer_0 == null)
                        {
                            this.irasterClassifyColorRampRenderer_0 =
                                RenderHelper.RasterClassifyRenderer(this.irasterLayer_0);
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.irasterClassifyColorRampRenderer_0 =
                            copy.Copy(pInObject) as IRasterClassifyColorRampRenderer;
                    }
                    if (this.bool_0)
                    {
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
                    }
                }
            }
        }

        bool IUserControl.Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
                this.listView1.StyleGallery = this.istyleGallery_0;
            }
        }
    }
}