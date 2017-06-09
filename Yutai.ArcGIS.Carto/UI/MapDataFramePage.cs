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
    public class MapDataFramePage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = true;
        private SimpleButton btnBorderInfo;
        private SimpleButton btnBorderSelector;
        private Button btnExclude;
        private Button btnFullExtend;
        private Button btnSetClipEnt;
        private Button btnSetExtend;
        private StyleComboBox cboBorder;
        private System.Windows.Forms.ComboBox cboClipType;
        private System.Windows.Forms.ComboBox cboExtendType;
        private System.Windows.Forms.ComboBox cboMapScale;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private IEnvelope ienvelope_0 = null;
        private IEnvelope ienvelope_1 = null;
        private IEnvelope ienvelope_2 = null;
        private IGeometry igeometry_0 = null;
        private ImageList imageList_0;
        private IMapFrame imapFrame_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panelExtend;
        private Panel panelmapscale;
        private RadioButton rdoAllLayerExtend;
        private RadioButton rdoOther;
        private TextBox txtBottom;
        private TextBox txtLeft;
        private TextBox txtRight;
        private TextBox txtTop;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapDataFramePage));
            this.groupBox1 = new GroupBox();
            this.panelExtend = new Panel();
            this.btnSetExtend = new Button();
            this.txtRight = new TextBox();
            this.label5 = new Label();
            this.txtLeft = new TextBox();
            this.label4 = new Label();
            this.txtBottom = new TextBox();
            this.label3 = new Label();
            this.txtTop = new TextBox();
            this.label2 = new Label();
            this.panelmapscale = new Panel();
            this.cboMapScale = new System.Windows.Forms.ComboBox();
            this.label1 = new Label();
            this.cboExtendType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new GroupBox();
            this.checkBox1 = new CheckBox();
            this.btnSetClipEnt = new Button();
            this.btnExclude = new Button();
            this.cboClipType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new GroupBox();
            this.rdoOther = new RadioButton();
            this.rdoAllLayerExtend = new RadioButton();
            this.btnFullExtend = new Button();
            this.groupBox4 = new GroupBox();
            this.btnBorderInfo = new SimpleButton();
            this.btnBorderSelector = new SimpleButton();
            this.cboBorder = new StyleComboBox(this.icontainer_0);
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox1.SuspendLayout();
            this.panelExtend.SuspendLayout();
            this.panelmapscale.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.panelExtend);
            this.groupBox1.Controls.Add(this.panelmapscale);
            this.groupBox1.Controls.Add(this.cboExtendType);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x169, 0xa8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "范围";
            this.panelExtend.Controls.Add(this.btnSetExtend);
            this.panelExtend.Controls.Add(this.txtRight);
            this.panelExtend.Controls.Add(this.label5);
            this.panelExtend.Controls.Add(this.txtLeft);
            this.panelExtend.Controls.Add(this.label4);
            this.panelExtend.Controls.Add(this.txtBottom);
            this.panelExtend.Controls.Add(this.label3);
            this.panelExtend.Controls.Add(this.txtTop);
            this.panelExtend.Controls.Add(this.label2);
            this.panelExtend.Location = new System.Drawing.Point(9, 0x31);
            this.panelExtend.Name = "panelExtend";
            this.panelExtend.Size = new Size(0x15a, 0x69);
            this.panelExtend.TabIndex = 2;
            this.panelExtend.Paint += new PaintEventHandler(this.panelExtend_Paint);
            this.btnSetExtend.Location = new System.Drawing.Point(0xff, 0x4f);
            this.btnSetExtend.Name = "btnSetExtend";
            this.btnSetExtend.Size = new Size(0x4b, 0x17);
            this.btnSetExtend.TabIndex = 9;
            this.btnSetExtend.Text = "指定范围";
            this.btnSetExtend.UseVisualStyleBackColor = true;
            this.btnSetExtend.Click += new EventHandler(this.btnSetExtend_Click);
            this.txtRight.Location = new System.Drawing.Point(0xcd, 0x20);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new Size(0x75, 0x15);
            this.txtRight.TabIndex = 8;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0xb6, 0x23);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "右";
            this.txtLeft.Location = new System.Drawing.Point(0x2c, 0x20);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new Size(0x81, 0x15);
            this.txtLeft.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x15, 0x23);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "左";
            this.txtBottom.Location = new System.Drawing.Point(0x7e, 0x3b);
            this.txtBottom.Name = "txtBottom";
            this.txtBottom.Size = new Size(100, 0x15);
            this.txtBottom.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x67, 0x3e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "下";
            this.txtTop.Location = new System.Drawing.Point(0x7e, 3);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new Size(100, 0x15);
            this.txtTop.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x67, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "上";
            this.panelmapscale.Controls.Add(this.cboMapScale);
            this.panelmapscale.Controls.Add(this.label1);
            this.panelmapscale.Location = new System.Drawing.Point(0x13, 0x39);
            this.panelmapscale.Name = "panelmapscale";
            this.panelmapscale.Size = new Size(270, 0x31);
            this.panelmapscale.TabIndex = 1;
            this.cboMapScale.FormattingEnabled = true;
            this.cboMapScale.Items.AddRange(new object[] { "<使用当前比例尺>", "1:1000", "1:10000", "1:25000", "1:100000", "1:250000", "1:500000" });
            this.cboMapScale.Location = new System.Drawing.Point(0x40, 12);
            this.cboMapScale.Name = "cboMapScale";
            this.cboMapScale.Size = new Size(0xa2, 20);
            this.cboMapScale.TabIndex = 1;
            this.cboMapScale.SelectedIndexChanged += new EventHandler(this.cboMapScale_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "比例尺";
            this.cboExtendType.FormattingEnabled = true;
            this.cboExtendType.Items.AddRange(new object[] { "自动", "固体比例尺", "固定范围" });
            this.cboExtendType.Location = new System.Drawing.Point(3, 20);
            this.cboExtendType.Name = "cboExtendType";
            this.cboExtendType.Size = new Size(0xa2, 20);
            this.cboExtendType.TabIndex = 0;
            this.cboExtendType.SelectedIndexChanged += new EventHandler(this.cboExtendType_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.btnSetClipEnt);
            this.groupBox2.Controls.Add(this.btnExclude);
            this.groupBox2.Controls.Add(this.cboClipType);
            this.groupBox2.Location = new System.Drawing.Point(8, 0xc5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x164, 0x92);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据裁剪";
            this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0x13, 90);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(120, 0x10);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "裁剪格网和经纬网";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnSetClipEnt.Location = new System.Drawing.Point(0xe0, 20);
            this.btnSetClipEnt.Name = "btnSetClipEnt";
            this.btnSetClipEnt.Size = new Size(0x4b, 0x17);
            this.btnSetClipEnt.TabIndex = 2;
            this.btnSetClipEnt.Text = "指定形状";
            this.btnSetClipEnt.UseVisualStyleBackColor = true;
            this.btnSetClipEnt.Click += new EventHandler(this.btnSetClipEnt_Click);
            this.btnExclude.Location = new System.Drawing.Point(0x13, 0x2f);
            this.btnExclude.Name = "btnExclude";
            this.btnExclude.Size = new Size(0x4b, 0x17);
            this.btnExclude.TabIndex = 1;
            this.btnExclude.Text = "排除图层";
            this.btnExclude.UseVisualStyleBackColor = true;
            this.btnExclude.Click += new EventHandler(this.btnExclude_Click);
            this.cboClipType.FormattingEnabled = true;
            this.cboClipType.Items.AddRange(new object[] { "无裁剪", "裁剪到范围" });
            this.cboClipType.Location = new System.Drawing.Point(0x13, 20);
            this.cboClipType.Name = "cboClipType";
            this.cboClipType.Size = new Size(0xa2, 20);
            this.cboClipType.TabIndex = 0;
            this.cboClipType.SelectedIndexChanged += new EventHandler(this.cboClipType_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.rdoOther);
            this.groupBox3.Controls.Add(this.rdoAllLayerExtend);
            this.groupBox3.Controls.Add(this.btnFullExtend);
            this.groupBox3.Location = new System.Drawing.Point(20, 0x15d);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x166, 0x55);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "全图命令使用的范围";
            this.groupBox3.Visible = false;
            this.rdoOther.AutoSize = true;
            this.rdoOther.Location = new System.Drawing.Point(10, 0x36);
            this.rdoOther.Name = "rdoOther";
            this.rdoOther.Size = new Size(0x2f, 0x10);
            this.rdoOther.TabIndex = 4;
            this.rdoOther.Text = "其他";
            this.rdoOther.UseVisualStyleBackColor = true;
            this.rdoAllLayerExtend.AutoSize = true;
            this.rdoAllLayerExtend.Checked = true;
            this.rdoAllLayerExtend.Location = new System.Drawing.Point(8, 20);
            this.rdoAllLayerExtend.Name = "rdoAllLayerExtend";
            this.rdoAllLayerExtend.Size = new Size(0x83, 0x10);
            this.rdoAllLayerExtend.TabIndex = 3;
            this.rdoAllLayerExtend.TabStop = true;
            this.rdoAllLayerExtend.Text = "所有图层的数据范围";
            this.rdoAllLayerExtend.UseVisualStyleBackColor = true;
            this.btnFullExtend.Location = new System.Drawing.Point(0x60, 0x33);
            this.btnFullExtend.Name = "btnFullExtend";
            this.btnFullExtend.Size = new Size(0x4b, 0x17);
            this.btnFullExtend.TabIndex = 2;
            this.btnFullExtend.Text = "指定范围";
            this.btnFullExtend.UseVisualStyleBackColor = true;
            this.btnFullExtend.Click += new EventHandler(this.btnFullExtend_Click);
            this.groupBox4.Controls.Add(this.btnBorderInfo);
            this.groupBox4.Controls.Add(this.btnBorderSelector);
            this.groupBox4.Controls.Add(this.cboBorder);
            this.groupBox4.Location = new System.Drawing.Point(0x95, 0x31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xb5, 60);
            this.groupBox4.TabIndex = 0x29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "边框";
            this.btnBorderInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBorderInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderInfo.Appearance.Options.UseBackColor = true;
            this.btnBorderInfo.Appearance.Options.UseForeColor = true;
            this.btnBorderInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBorderInfo.ImageIndex = 1;
            this.btnBorderInfo.ImageList = this.imageList_0;
            this.btnBorderInfo.Location = new System.Drawing.Point(160, 0x24);
            this.btnBorderInfo.Name = "btnBorderInfo";
            this.btnBorderInfo.Size = new Size(0x10, 0x10);
            this.btnBorderInfo.TabIndex = 0x21;
            this.btnBorderInfo.Click += new EventHandler(this.btnBorderInfo_Click);
            this.btnBorderSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBorderSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderSelector.Appearance.Options.UseBackColor = true;
            this.btnBorderSelector.Appearance.Options.UseForeColor = true;
            this.btnBorderSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBorderSelector.ImageIndex = 0;
            this.btnBorderSelector.ImageList = this.imageList_0;
            this.btnBorderSelector.Location = new System.Drawing.Point(160, 20);
            this.btnBorderSelector.Name = "btnBorderSelector";
            this.btnBorderSelector.Size = new Size(0x10, 0x10);
            this.btnBorderSelector.TabIndex = 0x20;
            this.btnBorderSelector.Click += new EventHandler(this.btnBorderSelector_Click);
            this.cboBorder.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBorder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBorder.DropDownWidth = 160;
            this.cboBorder.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBorder.Location = new System.Drawing.Point(11, 20);
            this.cboBorder.Name = "cboBorder";
            this.cboBorder.Size = new Size(0x94, 0x1f);
            this.cboBorder.TabIndex = 0x1f;
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapDataFramePage";
            base.Size = new Size(0x17d, 0x192);
            base.Load += new EventHandler(this.MapDataFramePage_Load);
            this.groupBox1.ResumeLayout(false);
            this.panelExtend.ResumeLayout(false);
            this.panelExtend.PerformLayout();
            this.panelmapscale.ResumeLayout(false);
            this.panelmapscale.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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

