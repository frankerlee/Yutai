using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class DataExclusionLegendCtrl : UserControl
    {
        private StyleButton btnStyle;
        private CheckEdit chkShowExclusionClass;
        private Container container_0 = null;
        private esriGeometryType esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
        private IDataExclusion idataExclusion_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private MemoEdit txtDescription;
        private TextEdit txtLabel;

        public DataExclusionLegendCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.idataExclusion_0 != null)
            {
                this.idataExclusion_0.ShowExclusionClass = this.chkShowExclusionClass.Checked;
                if (this.idataExclusion_0.ShowExclusionClass)
                {
                    this.idataExclusion_0.ExclusionSymbol = this.btnStyle.Style as ISymbol;
                    this.idataExclusion_0.ExclusionLabel = this.txtLabel.Text;
                    this.idataExclusion_0.ExclusionDescription = this.txtDescription.Text;
                }
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.istyleGallery_0);
                    selector.SetSymbol(this.btnStyle.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnStyle.Style = selector.GetSymbol();
                        this.idataExclusion_0.ExclusionSymbol = this.btnStyle.Style as ISymbol;
                    }
                }
            }
            catch
            {
            }
        }

        private void chkShowExclusionClass_CheckedChanged(object sender, EventArgs e)
        {
            this.btnStyle.Enabled = this.chkShowExclusionClass.Checked;
            this.txtLabel.Enabled = this.chkShowExclusionClass.Checked;
            this.txtDescription.Enabled = this.chkShowExclusionClass.Checked;
        }

        private void DataExclusionLegendCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.chkShowExclusionClass = new CheckEdit();
            this.btnStyle = new StyleButton();
            this.txtLabel = new TextEdit();
            this.txtDescription = new MemoEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.chkShowExclusionClass.Properties.BeginInit();
            this.txtLabel.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowExclusionClass.EditValue = false;
            this.chkShowExclusionClass.Location = new System.Drawing.Point(8, 0x10);
            this.chkShowExclusionClass.Name = "chkShowExclusionClass";
            this.chkShowExclusionClass.Properties.Caption = "显示排除数据";
            this.chkShowExclusionClass.Size = new Size(0x68, 0x13);
            this.chkShowExclusionClass.TabIndex = 0;
            this.chkShowExclusionClass.CheckedChanged += new EventHandler(this.chkShowExclusionClass_CheckedChanged);
            this.btnStyle.Location = new System.Drawing.Point(0x30, 0x30);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(0x58, 0x20);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 0x2b;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.txtLabel.EditValue = "";
            this.txtLabel.Location = new System.Drawing.Point(0x30, 0x58);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x88, 0x15);
            this.txtLabel.TabIndex = 0x2c;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new System.Drawing.Point(0x30, 120);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(160, 0x58);
            this.txtDescription.TabIndex = 0x2d;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 0x38);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0x2e;
            this.label1.Text = "符号";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 90);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 0x2f;
            this.label2.Text = "标注";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 0x30;
            this.label3.Text = "说明";
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtLabel);
            base.Controls.Add(this.btnStyle);
            base.Controls.Add(this.chkShowExclusionClass);
            base.Name = "DataExclusionLegendCtrl";
            base.Size = new Size(0x128, 0x108);
            base.Load += new EventHandler(this.DataExclusionLegendCtrl_Load);
            this.chkShowExclusionClass.Properties.EndInit();
            this.txtLabel.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            bool showExclusionClass = false;
            if (this.idataExclusion_0 != null)
            {
                showExclusionClass = this.idataExclusion_0.ShowExclusionClass;
            }
            this.chkShowExclusionClass.Checked = showExclusionClass;
            this.btnStyle.Enabled = showExclusionClass;
            this.txtLabel.Enabled = showExclusionClass;
            this.txtDescription.Enabled = showExclusionClass;
            if (showExclusionClass)
            {
                if (this.idataExclusion_0.ExclusionSymbol == null)
                {
                    this.idataExclusion_0.ExclusionSymbol = this.method_1(this.esriGeometryType_0);
                }
                this.btnStyle.Style = this.idataExclusion_0.ExclusionSymbol;
                this.txtLabel.Text = this.idataExclusion_0.ExclusionLabel;
                this.txtDescription.Text = this.idataExclusion_0.ExclusionDescription;
            }
        }

        private ISymbol method_1(esriGeometryType esriGeometryType_1)
        {
            switch (esriGeometryType_1)
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

        public IDataExclusion DataExclusion
        {
            set
            {
                this.idataExclusion_0 = value;
            }
        }

        public esriGeometryType GeometryType
        {
            set
            {
                this.esriGeometryType_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }
    }
}

