using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmAddTinRender : Form
    {
        private IContainer icontainer_0 = null;
        private ITin itin_0 = null;
        private ITinLayer itinLayer_0 = null;
        private ITinRenderer itinRenderer_0 = null;

        public event OnAddTinRenderHander OnAddTinRender;

        public frmAddTinRender()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.method_1(this.listBoxControl1.SelectedIndex);
        }

 private void frmAddTinRender_Load(object sender, EventArgs e)
        {
        }

 private void listBoxControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int num = this.listBoxControl1.IndexFromPoint(new Point(e.X, e.Y));
            this.method_1(num);
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = this.listBoxControl1.SelectedIndex != -1;
        }

        private void method_0(IClassify iclassify_0, ITinColorRampRenderer itinColorRampRenderer_0, int int_0, ITin itin_1)
        {
            bool flag;
            IRandomColorRamp ramp = new RandomColorRampClass {
                StartHue = 40,
                EndHue = 120,
                MinValue = 65,
                MaxValue = 90,
                MinSaturation = 25,
                MaxSaturation = 45,
                Size = 5,
                Seed = 23
            };
            IColorRamp ramp2 = ramp;
            if (iclassify_0 is IClassifyMinMax2)
            {
                ITinAdvanced advanced = itin_1 as ITinAdvanced;
                double zMin = advanced.Extent.ZMin;
                double zMax = advanced.Extent.ZMax;
                (iclassify_0 as IClassifyMinMax2).ClassifyMinMax(zMin, zMax, ref int_0);
            }
            else if (!(iclassify_0 is IDeviationInterval))
            {
            }
            itinColorRampRenderer_0.BreakCount = int_0;
            double[] classBreaks = (double[]) iclassify_0.ClassBreaks;
            if (classBreaks.Length == 0)
            {
                ramp2.Size = 5;
            }
            else
            {
                ramp2.Size = classBreaks.Length;
            }
            ramp2.CreateRamp(out flag);
            IEnumColors colors = ramp2.Colors;
            ISymbol sym = null;
            for (int i = 0; i < (classBreaks.Length - 1); i++)
            {
                IColor color = colors.Next();
                if ((itinColorRampRenderer_0 as ITinRenderer).Name == "Elevation")
                {
                    ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass {
                        Color = color,
                        Style = esriSimpleFillStyle.esriSFSSolid
                    };
                    sym = symbol2 as ISymbol;
                }
                else if ((itinColorRampRenderer_0 as ITinRenderer).Name == "Node elevation")
                {
                    IMarkerSymbol symbol3 = new SimpleMarkerSymbolClass {
                        Color = color
                    };
                    sym = symbol3 as ISymbol;
                }
                itinColorRampRenderer_0.set_Symbol(i, sym);
                (itinColorRampRenderer_0 as IClassBreaksUIProperties).set_LowBreak(i, classBreaks[i]);
                itinColorRampRenderer_0.set_Break(i, classBreaks[i + 1]);
                string label = classBreaks[i].ToString() + " - " + classBreaks[i + 1].ToString();
                itinColorRampRenderer_0.set_Label(i, label);
            }
        }

        private void method_1(int int_0)
        {
            IClassify classify;
            switch (int_0)
            {
                case 0:
                    this.itinRenderer_0 = new TinBreaklineRendererClass();
                    this.itinRenderer_0.Tin = this.itin_0;
                    break;

                case 1:
                    this.itinRenderer_0 = new TinEdgeRendererClass();
                    this.itinRenderer_0.Tin = this.itin_0;
                    break;

                case 2:
                    this.itinRenderer_0 = new TinAspectRendererClass();
                    this.itinRenderer_0.Tin = this.itin_0;
                    break;

                case 3:
                    this.itinRenderer_0 = new TinElevationRendererClass();
                    classify = new EqualIntervalClass();
                    (this.itinRenderer_0 as IClassBreaksUIProperties).Method = classify.ClassID;
                    this.itinRenderer_0.Tin = this.itin_0;
                    this.method_0(classify, this.itinRenderer_0 as ITinColorRampRenderer, 10, this.itin_0);
                    break;

                case 4:
                    this.itinRenderer_0 = new TinSlopeRendererClass();
                    classify = new EqualIntervalClass();
                    (this.itinRenderer_0 as IClassBreaksUIProperties).Method = classify.ClassID;
                    this.itinRenderer_0.Tin = this.itin_0;
                    break;

                case 5:
                    if (this.itin_0.HasTriangleTagValues)
                    {
                        this.itinRenderer_0 = new TinFaceValueRendererClass();
                        this.itinRenderer_0.Tin = this.itin_0;
                        break;
                    }
                    MessageBox.Show("无面标签值，该渲染不能入");
                    return;

                case 6:
                    this.itinRenderer_0 = new TinFaceRendererClass();
                    break;

                case 7:
                    this.itinRenderer_0 = new TinNodeElevationRendererClass();
                    classify = new EqualIntervalClass();
                    (this.itinRenderer_0 as IClassBreaksUIProperties).Method = classify.ClassID;
                    this.itinRenderer_0.Tin = this.itin_0;
                    this.method_0(classify, this.itinRenderer_0 as ITinColorRampRenderer, 10, this.itin_0);
                    break;

                case 8:
                    if (this.itin_0.HasNodeTagValues)
                    {
                        this.itinRenderer_0 = new TinNodeValueRendererClass();
                        this.itinRenderer_0.Tin = this.itin_0;
                        break;
                    }
                    MessageBox.Show("无节点标签值，该渲染不能入");
                    return;

                case 9:
                    this.itinRenderer_0 = new TinNodeRendererClass();
                    this.itinRenderer_0.Tin = this.itin_0;
                    break;

                default:
                    return;
            }
            if (this.OnAddTinRender != null)
            {
                this.OnAddTinRender(this.itinRenderer_0);
            }
        }

        public ITinLayer TinLayer
        {
            set
            {
                this.itinLayer_0 = value;
                this.itin_0 = this.itinLayer_0.Dataset;
            }
        }

        public ITinRenderer TinRenderer
        {
            get
            {
                return this.itinRenderer_0;
            }
        }

        internal delegate void OnAddTinRenderHander(ITinRenderer itinRenderer_0);
    }
}

