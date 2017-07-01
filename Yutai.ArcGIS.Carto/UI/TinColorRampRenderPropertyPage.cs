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
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class TinColorRampRenderPropertyPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private ITinLayer itinLayer_0 = null;

        public TinColorRampRenderPropertyPage()
        {
            this.InitializeComponent();
            this.listView1.OnValueChanged += new RenderInfoListView.OnValueChangedHandler(this.method_0);
            this.listView1.SetColumnEditable(2, true);
        }

        public void Apply()
        {
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
                        (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).Method = new UIDClass();
                        this.cboClassifyNum.Enabled = false;
                        break;

                    case 1:
                        classify = new EqualIntervalClass();
                        goto Label_006D;

                    case 2:
                        classify = new QuantileClass();
                        goto Label_006D;

                    case 3:
                        classify = new NaturalBreaksClass();
                        goto Label_006D;
                }
            }
            return;
            Label_006D:
            this.cboClassifyNum.Enabled = true;
            (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).Method = classify.ClassID;
            if (this.cboClassifyNum.SelectedIndex >= 0)
            {
                this.method_4();
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
                this.method_4();
            }
        }

        private void colorRampComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.icolorRamp_0 = this.colorRampComboBox1.GetSelectStyleGalleryItem().Item as IColorRamp;
                (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).ColorRamp = this.colorRampComboBox1.Text;
                if (this.itinColorRampRenderer_0.BreakCount > 0)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.itinColorRampRenderer_0.BreakCount;
                    this.icolorRamp_0.CreateRamp(out flag);
                    IEnumColors colors = this.icolorRamp_0.Colors;
                    colors.Reset();
                    this.listView1.BeginUpdate();
                    for (int i = 0; i < this.itinColorRampRenderer_0.BreakCount; i++)
                    {
                        ISymbol sym = this.itinColorRampRenderer_0.get_Symbol(i);
                        if (sym is IMarkerSymbol)
                        {
                            (sym as IMarkerSymbol).Color = colors.Next();
                        }
                        else if (sym is ILineSymbol)
                        {
                            (sym as ILineSymbol).Color = colors.Next();
                        }
                        else if (sym is IFillSymbol)
                        {
                            (sym as IFillSymbol).Color = colors.Next();
                        }
                        this.itinColorRampRenderer_0.set_Symbol(i, sym);
                        this.listView1.Items[i].Tag = sym;
                    }
                    this.listView1.EndUpdate();
                    this.listView1.Invalidate();
                }
            }
        }

        private void method_0(int int_0, object object_0)
        {
            if (object_0 is ISymbol)
            {
                this.itinColorRampRenderer_0.set_Symbol(int_0, object_0 as ISymbol);
            }
            else if (object_0 is string)
            {
                this.itinColorRampRenderer_0.set_Label(int_0, object_0.ToString());
            }
        }

        private void method_1()
        {
            object[] objArray = new object[3];
            this.listView1.Items.Clear();
            this.lblLabelInfo.Text = (this.itinColorRampRenderer_0 as ITinRenderer).Name;
            for (int i = 0; i < this.itinColorRampRenderer_0.BreakCount; i++)
            {
                objArray[0] = this.itinColorRampRenderer_0.get_Symbol(i);
                objArray[1] =
                    (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).get_LowBreak(
                        (this.itinColorRampRenderer_0.BreakCount - 1) - i).ToString("0.###") + " - " +
                    this.itinColorRampRenderer_0.get_Break((this.itinColorRampRenderer_0.BreakCount - 1) - i)
                        .ToString("0.###");
                objArray[2] = this.itinColorRampRenderer_0.get_Label(i);
                this.listView1.Add(objArray);
            }
            this.cboClassifyNum.SelectedIndex = this.itinColorRampRenderer_0.BreakCount - 1;
            UID method = (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).Method;
            if (method.Value.ToString() == "{00000000-0000-0000-0000-000000000000}")
            {
                this.cboClassifyMethod.SelectedIndex = 0;
                this.cboClassifyNum.Enabled = false;
            }
            else
            {
                this.cboClassifyNum.Enabled = true;
                IClassify classify = this.method_2(method) as IClassify;
                if (classify != null)
                {
                    this.Classifygroup.Enabled = true;
                    switch (classify.MethodName)
                    {
                        case "Equal Interval":
                        case "等间隔":
                            this.cboClassifyMethod.SelectedIndex = 1;
                            break;

                        case "Quantile":
                        case "分位数":
                            this.cboClassifyMethod.SelectedIndex = 2;
                            break;

                        case "Natural Breaks (Jenks)":
                        case "自然间隔(Jenks)":
                            this.cboClassifyMethod.SelectedIndex = 3;
                            break;
                    }
                }
                else
                {
                    this.Classifygroup.Enabled = false;
                }
            }
        }

        private object method_2(UID uid_0)
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

        private void method_3(IColorRamp icolorRamp_1, ITinColorRampRenderer itinColorRampRenderer_1, int int_0,
            ITin itin_0)
        {
            IClassify classify =
                this.method_2((itinColorRampRenderer_1 as IClassBreaksUIProperties).Method) as IClassify;
            if (classify != null)
            {
                bool flag;
                if (classify is IClassifyMinMax2)
                {
                    ITinAdvanced dataset = this.itinLayer_0.Dataset as ITinAdvanced;
                    double zMin = dataset.Extent.ZMin;
                    double zMax = dataset.Extent.ZMax;
                    (classify as IClassifyMinMax2).ClassifyMinMax(zMin, zMax, ref int_0);
                }
                else if (!(classify is IDeviationInterval))
                {
                }
                itinColorRampRenderer_1.BreakCount = int_0;
                this.bool_0 = false;
                this.cboClassifyNum.SelectedIndex = int_0 - 1;
                this.bool_0 = true;
                double[] classBreaks = (double[]) classify.ClassBreaks;
                if (classBreaks.Length == 0)
                {
                    icolorRamp_1.Size = 5;
                }
                else
                {
                    icolorRamp_1.Size = classBreaks.Length;
                }
                icolorRamp_1.CreateRamp(out flag);
                IEnumColors colors = icolorRamp_1.Colors;
                ISymbol sym = null;
                for (int i = 0; i < (classBreaks.Length - 1); i++)
                {
                    IColor color = colors.Next();
                    if ((itinColorRampRenderer_1 as ITinRenderer).Name == "Elevation")
                    {
                        ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass
                        {
                            Color = color,
                            Style = esriSimpleFillStyle.esriSFSSolid
                        };
                        sym = symbol2 as ISymbol;
                    }
                    else if ((itinColorRampRenderer_1 as ITinRenderer).Name == "Node elevation")
                    {
                        IMarkerSymbol symbol3 = new SimpleMarkerSymbolClass
                        {
                            Color = color
                        };
                        sym = symbol3 as ISymbol;
                    }
                    itinColorRampRenderer_1.set_Symbol(i, sym);
                    (itinColorRampRenderer_1 as IClassBreaksUIProperties).set_LowBreak(i, classBreaks[i]);
                    itinColorRampRenderer_1.set_Break(i, classBreaks[i + 1]);
                    string label = classBreaks[i].ToString() + " - " + classBreaks[i + 1].ToString();
                    itinColorRampRenderer_1.set_Label(i, label);
                }
            }
        }

        private void method_4()
        {
            if (((this.itinColorRampRenderer_0 as ITinRenderer).Name == "Elevation") ||
                ((this.itinColorRampRenderer_0 as ITinRenderer).Name == "Node elevation"))
            {
                this.method_3(this.icolorRamp_0, this.itinColorRampRenderer_0, this.cboClassifyNum.SelectedIndex + 1,
                    this.itinLayer_0.Dataset);
            }
            this.listView1.Items.Clear();
            string[] strArray = new string[3];
            int index = 0;
            while (true)
            {
                if (index >= this.itinColorRampRenderer_0.BreakCount)
                {
                    break;
                }
                strArray[0] = "";
                strArray[1] =
                    (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).get_LowBreak(index).ToString() + " - " +
                    this.itinColorRampRenderer_0.get_Break(index).ToString();
                strArray[2] = this.itinColorRampRenderer_0.get_Label(index);
                ListViewItemEx ex = new ListViewItemEx(strArray);
                try
                {
                    ex.Style = this.itinColorRampRenderer_0.get_Symbol(index);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
                this.listView1.Add(ex);
                index++;
            }
            this.cboClassifyNum.SelectedIndex = this.itinColorRampRenderer_0.BreakCount - 1;
        }

        private void TinColorRampRenderPropertyPage_Load(object sender, EventArgs e)
        {
            int num = -1;
            string colorRamp = (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).ColorRamp;
            if (this.istyleGallery_0 != null)
            {
                IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                item.Reset();
                for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                {
                    this.colorRampComboBox1.Add(item2);
                    if (item2.Name == colorRamp)
                    {
                        num = this.colorRampComboBox1.Items.Count - 1;
                    }
                }
                item = null;
                GC.Collect();
            }
            if (this.colorRampComboBox1.Items.Count == 0)
            {
                this.colorRampComboBox1.Enabled = false;
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
                this.colorRampComboBox1.SelectedIndex = num;
                if (this.colorRampComboBox1.SelectedIndex == -1)
                {
                    this.colorRampComboBox1.SelectedIndex = 0;
                }
                this.icolorRamp_0 = this.colorRampComboBox1.GetSelectStyleGalleryItem().Item as IColorRamp;
                (this.itinColorRampRenderer_0 as IClassBreaksUIProperties).ColorRamp = this.colorRampComboBox1.Text;
            }
            this.method_1();
            this.bool_0 = true;
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
                this.itinColorRampRenderer_0 = value as ITinColorRampRenderer;
                if (this.bool_0)
                {
                    this.bool_0 = false;
                    this.method_1();
                    this.bool_0 = true;
                }
            }
        }
    }
}