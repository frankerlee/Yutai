using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class OverviewWindowsProperty : Form
    {
        private SimpleButton btnCancel;
        private NewSymbolButton btnFillSymbol;
        private SimpleButton btnOK;
        private CheckEdit checkEdit1;
        private ColorEdit colorEdit1;
        private ComboBoxEdit comboBoxEdit1;
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Color m_BackgroundColor = Color.White;
        private IFillSymbol m_pFillSymbol = new SimpleFillSymbolClass();
        private IMap m_pMap = null;
        private IMap m_pOverviewMap = null;
        private IStyleGallery m_pSG = null;
        private bool m_ZoomWithMainView = false;

        public OverviewWindowsProperty()
        {
            this.InitializeComponent();
        }

        private void btnFillSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(this.m_pSG);
                selector.SetSymbol(this.btnFillSymbol.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnFillSymbol.Style = selector.GetSymbol();
                }
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_ZoomWithMainView = this.checkEdit1.Checked;
            if (this.comboBoxEdit1.SelectedIndex != -1)
            {
                this.m_pOverviewMap.ClearLayers();
                ILayer pInObject = (this.comboBoxEdit1.SelectedItem as LayerWrap).Layer;
                if (pInObject != null)
                {
                    this.RestMap(this.m_pOverviewMap);
                    IObjectCopy copy = new ObjectCopyClass();
                    this.m_pOverviewMap.AddLayer(copy.Copy(pInObject) as ILayer);
                    this.m_pOverviewMap.RecalcFullExtent();
                    (this.m_pOverviewMap as IActiveView).Extent = (this.m_pOverviewMap as IActiveView).FullExtent;
                }
            }
            this.m_BackgroundColor = this.colorEdit1.Color;
            this.m_pFillSymbol = this.btnFillSymbol.Style as IFillSymbol;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewWindowsProperty));
            this.label1 = new Label();
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.label2 = new Label();
            this.label3 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.btnFillSymbol = new NewSymbolButton();
            this.checkEdit1 = new CheckEdit();
            this.comboBoxEdit1.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "参考图层";
            this.comboBoxEdit1.EditValue = "";
            this.comboBoxEdit1.Location = new Point(8, 0x20);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Size = new Size(240, 0x15);
            this.comboBoxEdit1.TabIndex = 1;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xa2, 0x9b);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xe2, 0x9b);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "范围符号";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(160, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "背景色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0xd0, 80);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 6;
            this.btnFillSymbol.Location = new Point(80, 0x48);
            this.btnFillSymbol.Name = "btnFillSymbol";
            this.btnFillSymbol.Size = new Size(0x40, 0x20);
            this.btnFillSymbol.Style = null;
            this.btnFillSymbol.TabIndex = 7;
            this.btnFillSymbol.Click += new EventHandler(this.btnFillSymbol_Click);
            this.checkEdit1.Location = new Point(12, 0x71);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "随主视图缩放";
            this.checkEdit1.Size = new Size(0xa7, 0x13);
            this.checkEdit1.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x124, 0xbf);
            base.Controls.Add(this.checkEdit1);
            base.Controls.Add(this.btnFillSymbol);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.comboBoxEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "OverviewWindowsProperty";
            this.Text = "鹰眼属性";
            base.Load += new EventHandler(this.OverviewWindowsProperty_Load);
            this.comboBoxEdit1.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void OverviewWindowsProperty_Load(object sender, EventArgs e)
        {
            this.checkEdit1.Checked = this.m_ZoomWithMainView;
            this.colorEdit1.Color = this.m_BackgroundColor;
            this.btnFillSymbol.Style = this.m_pFillSymbol;
            ILayer pOverviewLayer = null;
            if (this.m_pOverviewMap.LayerCount > 0)
            {
                pOverviewLayer = this.m_pOverviewMap.get_Layer(0);
            }
            this.comboBoxEdit1.Properties.Items.Add(new LayerWrap(null));
            int nIndex = 0;
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer pLayer = this.m_pMap.get_Layer(i);
                this.comboBoxEdit1.Properties.Items.Add(new LayerWrap(pLayer));
                if (((pOverviewLayer != null) && (nIndex == 0)) && (pOverviewLayer.Name == pLayer.Name))
                {
                    nIndex = this.comboBoxEdit1.Properties.Items.Count - 1;
                }
                if (pLayer is IGroupLayer)
                {
                    this.ReadGroupLayer(pLayer as IGroupLayer, pOverviewLayer, ref nIndex);
                }
            }
            if ((pOverviewLayer != null) && (nIndex == 0))
            {
                if (this.comboBoxEdit1.Properties.Items.Count == 1)
                {
                    this.comboBoxEdit1.Properties.Items.Add(new LayerWrap(pOverviewLayer));
                }
                else
                {
                    this.comboBoxEdit1.Properties.Items.Insert(1, new LayerWrap(pOverviewLayer));
                }
                nIndex = 1;
            }
            this.comboBoxEdit1.SelectedIndex = nIndex;
        }

        private void ReadGroupLayer(IGroupLayer pGroupLayer, ILayer pOverviewLayer, ref int nIndex)
        {
            for (int i = 0; i < (pGroupLayer as ICompositeLayer).Count; i++)
            {
                ILayer pLayer = (pGroupLayer as ICompositeLayer).get_Layer(i);
                this.comboBoxEdit1.Properties.Items.Add(new LayerWrap(pLayer));
                if (((pOverviewLayer != null) && (nIndex == 0)) && (pOverviewLayer.Name == pLayer.Name))
                {
                    nIndex = this.comboBoxEdit1.Properties.Items.Count - 1;
                }
                if (pLayer is IGroupLayer)
                {
                    this.ReadGroupLayer(pLayer as IGroupLayer, pOverviewLayer, ref nIndex);
                }
            }
        }

        internal void RestMap(IMap pMap)
        {
            pMap.ClearLayers();
            pMap.SpatialReferenceLocked = false;
            pMap.SpatialReference = null;
            pMap.MapUnits = esriUnits.esriUnknownUnits;
            pMap.DistanceUnits = esriUnits.esriUnknownUnits;
            pMap.RecalcFullExtent();
        }

        public IFillSymbol FillSymbol
        {
            get
            {
                return this.m_pFillSymbol;
            }
            set
            {
                if (value != null)
                {
                    this.m_pFillSymbol = value;
                }
            }
        }

        public IMap Map
        {
            set
            {
                this.m_pMap = value;
            }
        }

        public Color MapCtrlBackgroundColor
        {
            get
            {
                return this.m_BackgroundColor;
            }
            set
            {
                this.m_BackgroundColor = value;
            }
        }

        public IMap OverviewMap
        {
            set
            {
                this.m_pOverviewMap = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public bool ZoomWithMainView
        {
            get
            {
                return this.m_ZoomWithMainView;
            }
            set
            {
                this.m_ZoomWithMainView = value;
            }
        }

        internal class LayerWrap
        {
            private ILayer m_pLayer = null;

            public LayerWrap(ILayer pLayer)
            {
                this.m_pLayer = pLayer;
            }

            public override string ToString()
            {
                if (this.m_pLayer == null)
                {
                    return "<无>";
                }
                return this.m_pLayer.Name;
            }

            public ILayer Layer
            {
                get
                {
                    return this.m_pLayer;
                }
            }
        }
    }
}

