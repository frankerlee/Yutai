using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class BufferGeometrySetCtrl : UserControl
    {
        private ComboBoxEdit cboLayers;
        private CheckEdit chkOnlyUseSelectedFeature;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label lblSelectInfo;
        private RadioButton rdoUseFeature;
        private RadioButton rdoUseGraphic;

        public BufferGeometrySetCtrl()
        {
            this.InitializeComponent();
        }

        private void BufferGeometrySetCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void cboLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLayers.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pFeatureLayer = (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                this.lblSelectInfo.Text = (BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count.ToString();
                if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.chkOnlyUseSelectedFeature.Enabled = true;
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    this.chkOnlyUseSelectedFeature.Enabled = false;
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void chkOnlyUseSelectedFeature_CheckedChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
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
            this.groupBox1 = new GroupBox();
            this.rdoUseFeature = new RadioButton();
            this.rdoUseGraphic = new RadioButton();
            this.label2 = new Label();
            this.chkOnlyUseSelectedFeature = new CheckEdit();
            this.label1 = new Label();
            this.cboLayers = new ComboBoxEdit();
            this.lblSelectInfo = new Label();
            this.groupBox1.SuspendLayout();
            this.chkOnlyUseSelectedFeature.Properties.BeginInit();
            this.cboLayers.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lblSelectInfo);
            this.groupBox1.Controls.Add(this.rdoUseFeature);
            this.groupBox1.Controls.Add(this.rdoUseGraphic);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkOnlyUseSelectedFeature);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboLayers);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x160, 0xb8);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "做缓冲区的元素";
            this.rdoUseFeature.Location = new Point(0x10, 40);
            this.rdoUseFeature.Name = "rdoUseFeature";
            this.rdoUseFeature.Size = new Size(120, 0x18);
            this.rdoUseFeature.TabIndex = 6;
            this.rdoUseFeature.Text = "图层中的要素";
            this.rdoUseFeature.Click += new EventHandler(this.rdoUseFeature_Click);
            this.rdoUseGraphic.Location = new Point(0x10, 0x10);
            this.rdoUseGraphic.Name = "rdoUseGraphic";
            this.rdoUseGraphic.Size = new Size(0x70, 0x18);
            this.rdoUseGraphic.TabIndex = 5;
            this.rdoUseGraphic.Text = "数据框中的图形";
            this.rdoUseGraphic.Click += new EventHandler(this.rdoUseGraphic_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x88, 0x68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 12);
            this.label2.TabIndex = 4;
            this.chkOnlyUseSelectedFeature.Location = new Point(0x20, 0x88);
            this.chkOnlyUseSelectedFeature.Name = "chkOnlyUseSelectedFeature";
            this.chkOnlyUseSelectedFeature.Properties.Caption = "只使用选中要素";
            this.chkOnlyUseSelectedFeature.Size = new Size(120, 0x13);
            this.chkOnlyUseSelectedFeature.TabIndex = 3;
            this.chkOnlyUseSelectedFeature.CheckedChanged += new EventHandler(this.chkOnlyUseSelectedFeature_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x20, 0x68);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5f, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选中的要素个数:";
            this.cboLayers.EditValue = "";
            this.cboLayers.Location = new Point(0x20, 0x48);
            this.cboLayers.Name = "cboLayers";
            this.cboLayers.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayers.Size = new Size(0xd8, 0x15);
            this.cboLayers.TabIndex = 1;
            this.cboLayers.SelectedIndexChanged += new EventHandler(this.cboLayers_SelectedIndexChanged);
            this.lblSelectInfo.AutoSize = true;
            this.lblSelectInfo.Location = new Point(0x85, 0x68);
            this.lblSelectInfo.Name = "lblSelectInfo";
            this.lblSelectInfo.Size = new Size(0, 12);
            this.lblSelectInfo.TabIndex = 7;
            base.Controls.Add(this.groupBox1);
            base.Name = "BufferGeometrySetCtrl";
            base.Size = new Size(0x198, 0xe8);
            base.Load += new EventHandler(this.BufferGeometrySetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.chkOnlyUseSelectedFeature.Properties.EndInit();
            this.cboLayers.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            UID uid = new UIDClass {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = BufferHelper.m_BufferHelper.m_pFocusMap.get_Layers(uid, true);
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 is IFeatureLayer; layer2 = layer.Next())
            {
                if (((layer2 as IFeatureLayer).FeatureClass != null) && ((layer2 as IFeatureLayer).FeatureClass.FeatureType == esriFeatureType.esriFTSimple))
                {
                    this.cboLayers.Properties.Items.Add(new ObjectWrap(layer2));
                }
            }
            if (this.cboLayers.Properties.Items.Count > 0)
            {
                this.cboLayers.SelectedIndex = 0;
            }
            if ((BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainer).Next() == null)
            {
                this.rdoUseGraphic.Enabled = false;
                this.rdoUseFeature.Checked = true;
                BufferHelper.m_BufferHelper.m_SourceType = 1;
                this.cboLayers.Enabled = true;
                if (this.cboLayers.SelectedIndex != -1)
                {
                    BufferHelper.m_BufferHelper.m_pFeatureLayer = (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                    this.lblSelectInfo.Text = (BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count.ToString();
                    if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                    {
                        this.chkOnlyUseSelectedFeature.Enabled = true;
                        BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                    }
                    else
                    {
                        this.chkOnlyUseSelectedFeature.Enabled = false;
                        BufferHelper.m_BufferHelper.bUseSelect = false;
                    }
                }
            }
            else
            {
                this.chkOnlyUseSelectedFeature.Enabled = (BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainerSelect).ElementSelectionCount > 0;
                if (this.chkOnlyUseSelectedFeature.Enabled)
                {
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void rdoUseFeature_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_SourceType = 1;
            this.rdoUseGraphic.Checked = false;
            this.rdoUseFeature.Checked = true;
            this.cboLayers.Enabled = true;
            if (this.cboLayers.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pFeatureLayer = (this.cboLayers.SelectedItem as ObjectWrap).Object as IFeatureLayer;
                if ((BufferHelper.m_BufferHelper.m_pFeatureLayer as IFeatureSelection).SelectionSet.Count > 0)
                {
                    this.chkOnlyUseSelectedFeature.Enabled = true;
                    BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
                }
                else
                {
                    this.chkOnlyUseSelectedFeature.Enabled = false;
                    BufferHelper.m_BufferHelper.bUseSelect = false;
                }
            }
        }

        private void rdoUseGraphic_Click(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_SourceType = 0;
            this.rdoUseGraphic.Checked = true;
            this.rdoUseFeature.Checked = false;
            this.cboLayers.Enabled = false;
            this.chkOnlyUseSelectedFeature.Enabled = (BufferHelper.m_BufferHelper.m_pFocusMap as IGraphicsContainerSelect).ElementSelectionCount > 0;
            if (this.chkOnlyUseSelectedFeature.Enabled)
            {
                BufferHelper.m_BufferHelper.bUseSelect = this.chkOnlyUseSelectedFeature.Checked;
            }
            else
            {
                BufferHelper.m_BufferHelper.bUseSelect = false;
            }
        }
    }
}

