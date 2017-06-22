using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class MapDataFramePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = true;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private IEnvelope ienvelope_0 = null;
        private IEnvelope ienvelope_1 = null;
        private IEnvelope ienvelope_2 = null;
        private IGeometry igeometry_0 = null;
        private IMapFrame imapFrame_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public MapDataFramePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.cboExtendType.SelectedIndex == 0)
            {
                (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentDefault;
            }
            else if (this.cboExtendType.SelectedIndex == 1)
            {
                (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentScale;
                double mapScale = 10.0;
                if (this.cboMapScale.SelectedIndex == 0)
                {
                    try
                    {
                        if ((this.ibasicMap_0 as IMap).MapUnits != esriUnits.esriUnknownUnits)
                        {
                            mapScale = (this.ibasicMap_0 as IMap).MapScale;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    mapScale = double.Parse(this.cboMapScale.Text.Split(new char[] { ':' })[1]);
                }
                (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentScale = mapScale;
            }
            else if (this.cboExtendType.SelectedIndex == 2)
            {
                (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentBounds;
                if (this.ienvelope_1 == null)
                {
                    double num2;
                    double num3;
                    double num4;
                    double num5;
                    if (!double.TryParse(this.txtBottom.Text, out num2))
                    {
                        MessageBox.Show("底部值输入错误!");
                        return;
                    }
                    if (!double.TryParse(this.txtLeft.Text, out num3))
                    {
                        MessageBox.Show("左边值输入错误!");
                        return;
                    }
                    if (!double.TryParse(this.txtTop.Text, out num4))
                    {
                        MessageBox.Show("顶部值输入错误!");
                        return;
                    }
                    if (!double.TryParse(this.txtRight.Text, out num5))
                    {
                        MessageBox.Show("右边值输入错误!");
                        return;
                    }
                    IEnvelope envelope = new EnvelopeClass {
                        XMin = num3,
                        XMax = num5,
                        YMax = num4,
                        YMin = num2
                    };
                    this.ienvelope_1 = envelope;
                }
                (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentBounds = this.ienvelope_1;
            }
            if (this.cboClipType.SelectedIndex == 0)
            {
                (this.ibasicMap_0 as IMapClipOptions).ClipType = esriMapClipType.esriMapClipNone;
            }
            else
            {
                (this.ibasicMap_0 as IMapClipOptions).ClipType = esriMapClipType.esriMapClipShape;
                (this.ibasicMap_0 as IMapClipOptions).ClipGeometry = this.igeometry_0;
                (this.ibasicMap_0 as IMapClipOptions).ClipGridAndGraticules = this.checkBox1.Checked;
                IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem != null)
                {
                    (this.ibasicMap_0 as IMapClipOptions).ClipBorder = selectStyleGalleryItem.Item as IBorder;
                }
            }
        }

        private void btnBorderInfo_Click(object sender, EventArgs e)
        {
            IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
            IBorder item = null;
            if (selectStyleGalleryItem != null)
            {
                item = selectStyleGalleryItem.Item as IBorder;
            }
            if (item != null)
            {
                frmElementProperty property = new frmElementProperty();
                BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(item))
                {
                    this.bool_0 = false;
                    selectStyleGalleryItem = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                    if (selectStyleGalleryItem != null)
                    {
                        if (selectStyleGalleryItem.Name == "<定制>")
                        {
                            selectStyleGalleryItem.Item = item;
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = item
                            };
                            this.cboBorder.Add(selectStyleGalleryItem);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                    }
                    else
                    {
                        selectStyleGalleryItem = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = item
                        };
                        this.cboBorder.Add(selectStyleGalleryItem);
                        this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void btnBorderSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
                    IBorder pSym = null;
                    if (selectStyleGalleryItem != null)
                    {
                        pSym = selectStyleGalleryItem.Item as IBorder;
                    }
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    if (pSym != null)
                    {
                        selector.SetSymbol(pSym);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolBorderClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IBorder;
                        this.bool_0 = false;
                        selectStyleGalleryItem = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                        if (selectStyleGalleryItem != null)
                        {
                            if (selectStyleGalleryItem.Name == "<定制>")
                            {
                                selectStyleGalleryItem.Item = pSym;
                            }
                            else
                            {
                                selectStyleGalleryItem = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = pSym
                                };
                                this.cboBorder.Add(selectStyleGalleryItem);
                                this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                            }
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = pSym
                            };
                            this.cboBorder.Add(selectStyleGalleryItem);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnExclude_Click(object sender, EventArgs e)
        {
            new frmLayerCheck { Map = this.ibasicMap_0 as IMap }.ShowDialog();
        }

        private void btnFullExtend_Click(object sender, EventArgs e)
        {
            frmExtendSet set = new frmExtendSet {
                Text = "全图",
                Map = this.ibasicMap_0,
                ExtendType = ExtendSetType.FullExtentRange
            };
            if (set.ShowDialog() != DialogResult.OK)
            {
            }
        }

        private void btnSetClipEnt_Click(object sender, EventArgs e)
        {
            frmExtendSet set = new frmExtendSet {
                Text = "裁剪",
                Map = this.ibasicMap_0,
                ExtendType = ExtendSetType.ClipRange,
                ClipGeometry = this.igeometry_0
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                this.igeometry_0 = set.ClipGeometry;
            }
        }

        private void btnSetExtend_Click(object sender, EventArgs e)
        {
            frmExtendSet set = new frmExtendSet {
                Text = "范围",
                Map = this.ibasicMap_0,
                ExtendType = ExtendSetType.Range,
                ClipGeometry = this.ienvelope_1
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                this.ienvelope_1 = set.ClipGeometry.Envelope;
                this.method_0(this.ienvelope_1);
            }
        }

        public void Cancel()
        {
        }

        private void cboClipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnExclude.Enabled = this.cboClipType.SelectedIndex == 1;
            this.btnSetClipEnt.Enabled = this.cboClipType.SelectedIndex == 1;
            this.groupBox4.Enabled = this.cboClipType.SelectedIndex == 1;
        }

        private void cboExtendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelExtend.Visible = this.cboExtendType.SelectedIndex == 2;
            this.panelmapscale.Visible = this.cboExtendType.SelectedIndex == 1;
            if (this.cboExtendType.SelectedIndex == 1)
            {
                try
                {
                    this.cboMapScale.Text = "1:" + (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentScale.ToString("0");
                }
                catch
                {
                }
            }
            else if (this.cboExtendType.SelectedIndex == 2)
            {
                this.method_0((this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentBounds);
            }
        }

        private void cboMapScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.cboMapScale.SelectedIndex == 0))
            {
                this.bool_0 = false;
                try
                {
                    this.cboMapScale.Text = "1:" + (this.ibasicMap_0 as IMap).MapScale.ToString("0");
                }
                catch
                {
                }
                this.bool_0 = true;
            }
        }

 private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

 private void MapDataFramePage_Load(object sender, EventArgs e)
        {
            this.cboBorder.Add(null);
            if (ApplicationBase.StyleGallery != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = ApplicationBase.StyleGallery.get_Items("Borders", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboBorder.Add(item);
                }
                if (this.cboBorder.Items.Count > 0)
                {
                    this.cboBorder.SelectedIndex = 0;
                }
            }
            this.bool_0 = true;
            this.ienvelope_0 = (this.ibasicMap_0 as IActiveView).Extent;
            if (this.ibasicMap_0 is IMapAutoExtentOptions)
            {
                if ((this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentType == esriExtentTypeEnum.esriExtentDefault)
                {
                    this.cboExtendType.SelectedIndex = 0;
                }
                else if ((this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentType == esriExtentTypeEnum.esriExtentScale)
                {
                    this.cboExtendType.SelectedIndex = 1;
                }
                else
                {
                    this.cboExtendType.SelectedIndex = 2;
                    this.ienvelope_0 = (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentBounds;
                }
                this.ienvelope_0 = (this.ibasicMap_0 as IMapAutoExtentOptions).AutoExtentBounds;
            }
            if (this.ibasicMap_0 is IMapClipOptions)
            {
                if ((this.ibasicMap_0 as IMapClipOptions).ClipType == esriMapClipType.esriMapClipNone)
                {
                    this.cboClipType.SelectedIndex = 0;
                }
                else
                {
                    this.cboClipType.SelectedIndex = 1;
                }
                this.groupBox4.Enabled = this.cboClipType.SelectedIndex == 1;
                this.checkBox1.Checked = (this.ibasicMap_0 as IMapClipOptions).ClipGridAndGraticules;
                if ((this.ibasicMap_0 as IMapClipOptions).ClipBorder != null)
                {
                    IStyleGalleryItem item3 = new MyStyleGalleryItem {
                        Item = (this.ibasicMap_0 as IMapClipOptions).ClipBorder,
                        Name = ""
                    };
                    this.cboBorder.Add(item3);
                    this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                }
                this.igeometry_0 = (this.ibasicMap_0 as IMapClipOptions).ClipGeometry;
            }
        }

        private void method_0(IEnvelope ienvelope_3)
        {
            if (!ienvelope_3.IsEmpty)
            {
                this.txtTop.Text = ienvelope_3.YMax.ToString("0.000");
                this.txtBottom.Text = ienvelope_3.YMin.ToString("0.000");
                this.txtLeft.Text = ienvelope_3.XMin.ToString("0.000");
                this.txtRight.Text = ienvelope_3.XMax.ToString("0.000");
            }
        }

        private void panelExtend_Paint(object sender, PaintEventArgs e)
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            if (object_0 is IBasicMap)
            {
                this.ibasicMap_0 = object_0 as IBasicMap;
            }
            else if (object_0 is IMapFrame)
            {
                this.MapFrame = object_0 as IMapFrame;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public IBasicMap Map
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
                this.ibasicMap_0 = this.imapFrame_0.Map as IBasicMap;
            }
        }

        public string Title
        {
            get
            {
                return "框架";
            }
            set
            {
            }
        }
    }
}

