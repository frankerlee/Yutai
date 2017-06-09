using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class GraphicsSelectData : UserControl
    {
        private ComboBoxEdit cboLayers;
        private Container components = null;
        private Label label1;
        private IMap m_pMap = null;
        private RadioGroup radioGroupExportType;

        public GraphicsSelectData()
        {
            this.InitializeComponent();
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedItem != null)
            {
                if (((this.cboLayers.SelectedItem as LayerObjectWrap).Layer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.radioGroupExportType.Enabled = true;
                }
                else
                {
                    this.radioGroupExportType.SelectedIndex = 0;
                    this.radioGroupExportType.Enabled = false;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool Do()
        {
            if (this.cboLayers.SelectedItem == null)
            {
                MessageBox.Show("请图层!");
                return false;
            }
            ILayer layer = (this.cboLayers.SelectedItem as LayerObjectWrap).Layer;
            if (this.radioGroupExportType.SelectedIndex == 1)
            {
                GraphicHelper.pGraphicHelper.DataSource = (layer as IFeatureSelection).SelectionSet;
            }
            else
            {
                GraphicHelper.pGraphicHelper.DataSource = (layer as IFeatureLayer).FeatureClass;
            }
            return true;
        }

        private void ExportToExcelSelectData_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_pMap.LayerCount; i++)
            {
                ILayer pLayer = this.m_pMap.get_Layer(i);
                if ((pLayer is IFeatureLayer) && ((pLayer as IFeatureLayer).FeatureClass != null))
                {
                    this.cboLayers.Properties.Items.Add(new LayerObjectWrap(pLayer));
                }
            }
            if (this.cboLayers.Properties.Items.Count > 0)
            {
                this.cboLayers.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.radioGroupExportType = new RadioGroup();
            this.cboLayers = new ComboBoxEdit();
            this.radioGroupExportType.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择图层:";
            this.radioGroupExportType.Location = new Point(0x18, 0x38);
            this.radioGroupExportType.Name = "radioGroupExportType";
            this.radioGroupExportType.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupExportType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupExportType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupExportType.Properties.Columns = 2;
            this.radioGroupExportType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "导出全部要素"), new RadioGroupItem(null, "只导出选择要素") });
            this.radioGroupExportType.Size = new Size(0xe8, 0x18);
            this.radioGroupExportType.TabIndex = 3;
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(0x48, 8);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayers.Size = new Size(0xb0, 0x17);
            this.cboLayers.TabIndex = 4;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.BackColor = SystemColors.ControlLight;
            base.Controls.Add(this.cboLayers);
            base.Controls.Add(this.radioGroupExportType);
            base.Controls.Add(this.label1);
            base.Name = "GraphicsSelectData";
            base.Size = new Size(0x120, 0x98);
            base.Load += new EventHandler(this.ExportToExcelSelectData_Load);
            this.radioGroupExportType.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public IMap Map
        {
            get
            {
                return this.m_pMap;
            }
            set
            {
                this.m_pMap = value;
            }
        }

        protected class LayerObjectWrap
        {
            private ILayer m_pLayer = null;

            public LayerObjectWrap(ILayer pLayer)
            {
                this.m_pLayer = pLayer;
            }

            public override string ToString()
            {
                if (this.m_pLayer != null)
                {
                    return this.m_pLayer.Name;
                }
                return "";
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

