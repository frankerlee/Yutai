using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public class SpatialAndAttributeQueryCtrl : UserControl
    {
        private SimpleButton btnApply;
        private SimpleButton btnCreateQuery;
        private ComboBoxEdit cboOperationType;
        private ComboBoxEdit cboSourceLayer;
        private ComboBoxEdit cboSpatialRelation;
        private ComboBoxEdit cboUnit;
        private CheckEdit chkUseBuffer;
        private CheckEdit chkUseSelectFeature;
        private CheckEdit chkUsetSelectedLayer;
        private ComboBoxEdit comboBoxLayer;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblStatu;
        private MemoEdit memEditWhereCaluse;
        private Panel panel1;
        private TextEdit txtRadius;

        public SpatialAndAttributeQueryCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if ((this.cboSourceLayer.SelectedIndex != -1) && (this.comboBoxLayer.SelectedIndex != -1))
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
                ICursor cursor = null;
                if (this.chkUseSelectFeature.Checked)
                {
                    (layer as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                }
                else
                {
                    cursor = layer.Search(null, false) as ICursor;
                }
                esriSpatialRelEnum esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelIntersects;
                string text = this.cboSpatialRelation.Text;
                switch (text)
                {
                    case null:
                        break;

                    case "相交":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelIntersects;
                        break;

                    case "包围矩形相交":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                        break;

                    case "相接":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelTouches;
                        break;

                    case "重叠":
                        esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelOverlaps;
                        break;

                    default:
                        if (!(text == "被包含"))
                        {
                            if (text == "包含")
                            {
                                esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelContains;
                            }
                        }
                        else
                        {
                            esriSpatialRelIntersects = esriSpatialRelEnum.esriSpatialRelWithin;
                        }
                        break;
                }
                esriSelectionResultEnum esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultNew;
                switch (this.cboOperationType.SelectedIndex)
                {
                    case 0:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultNew;
                        break;

                    case 1:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultAdd;
                        break;

                    case 2:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultSubtract;
                        break;

                    case 3:
                        esriSelectionResultNew = esriSelectionResultEnum.esriSelectionResultAnd;
                        break;
                }
                IFeatureLayer layer2 = null;
                layer2 = (this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
                this.method_3(layer2, cursor as IFeatureCursor, esriSpatialRelIntersects, esriSelectionResultNew, this.memEditWhereCaluse.Text);
                (this.imap_0 as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.btnApply.Enabled = false;
            this.panel1.Visible = true;
            try
            {
                this.Apply();
            }
            catch
            {
            }
            this.btnApply.Enabled = true;
            this.panel1.Visible = false;
            Cursor = Cursors.Default;
        }

        private void btnCreateQuery_Click(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedIndex != -1)
            {
                frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder();
                ILayer layer = (this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer;
                builder.CurrentLayer = layer;
                builder.WhereCaluse = this.memEditWhereCaluse.Text;
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    this.memEditWhereCaluse.Text = builder.WhereCaluse;
                }
            }
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as LayerObjectWrap).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count == 0)
                {
                    this.chkUseSelectFeature.Enabled = false;
                    this.chkUseSelectFeature.Checked = false;
                }
                else
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (this.method_1(layer))
                    {
                        this.chkUseSelectFeature.Enabled = false;
                    }
                    else
                    {
                        this.chkUseSelectFeature.Enabled = true;
                    }
                }
            }
        }

        private void chkUseBuffer_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRadius.Enabled = this.chkUseBuffer.Checked;
            this.cboUnit.Enabled = this.chkUseBuffer.Checked;
        }

        private void chkUsetSelectedLayer_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxLayer.Properties.Items.Clear();
            this.method_2(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.method_2(this.cboSourceLayer, false);
            if (this.cboSourceLayer.Properties.Items.Count > 0)
            {
                this.cboSourceLayer.SelectedIndex = 0;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.btnApply = new SimpleButton();
            this.chkUsetSelectedLayer = new CheckEdit();
            this.label2 = new Label();
            this.cboOperationType = new ComboBoxEdit();
            this.label1 = new Label();
            this.memEditWhereCaluse = new MemoEdit();
            this.comboBoxLayer = new ComboBoxEdit();
            this.label4 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboUnit = new ComboBoxEdit();
            this.txtRadius = new TextEdit();
            this.chkUseBuffer = new CheckEdit();
            this.chkUseSelectFeature = new CheckEdit();
            this.cboSourceLayer = new ComboBoxEdit();
            this.label3 = new Label();
            this.cboSpatialRelation = new ComboBoxEdit();
            this.panel1 = new Panel();
            this.lblStatu = new Label();
            this.btnCreateQuery = new SimpleButton();
            this.chkUsetSelectedLayer.Properties.BeginInit();
            this.cboOperationType.Properties.BeginInit();
            this.memEditWhereCaluse.Properties.BeginInit();
            this.comboBoxLayer.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboUnit.Properties.BeginInit();
            this.txtRadius.Properties.BeginInit();
            this.chkUseBuffer.Properties.BeginInit();
            this.chkUseSelectFeature.Properties.BeginInit();
            this.cboSourceLayer.Properties.BeginInit();
            this.cboSpatialRelation.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnApply.Location = new System.Drawing.Point(0x10b, 0x14b);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x40, 0x18);
            this.btnApply.TabIndex = 0x1b;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.chkUsetSelectedLayer.Location = new System.Drawing.Point(0x34, 0x2d);
            this.chkUsetSelectedLayer.Name = "chkUsetSelectedLayer";
            this.chkUsetSelectedLayer.Properties.Caption = "只显示可选图层";
            this.chkUsetSelectedLayer.Size = new Size(0x70, 0x13);
            this.chkUsetSelectedLayer.TabIndex = 0x13;
            this.chkUsetSelectedLayer.Visible = false;
            this.chkUsetSelectedLayer.CheckedChanged += new EventHandler(this.chkUsetSelectedLayer_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x11, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x12;
            this.label2.Text = "图层";
            this.cboOperationType.EditValue = "选择要素";
            this.cboOperationType.Location = new System.Drawing.Point(0x11, 70);
            this.cboOperationType.Name = "cboOperationType";
            this.cboOperationType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboOperationType.Properties.Items.AddRange(new object[] { "选择要素", "选择要素,并添加到选择集中", "的选择集中删除", "的选择集中选择" });
            this.cboOperationType.Size = new Size(0xf8, 0x15);
            this.cboOperationType.TabIndex = 0x10;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x11, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xd1, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "根据要素的空间关系从图层中选择要素";
            this.memEditWhereCaluse.EditValue = "";
            this.memEditWhereCaluse.Location = new System.Drawing.Point(7, 0x107);
            this.memEditWhereCaluse.Name = "memEditWhereCaluse";
            this.memEditWhereCaluse.Size = new Size(0x157, 0x3e);
            this.memEditWhereCaluse.TabIndex = 0x1d;
            this.comboBoxLayer.EditValue = "";
            this.comboBoxLayer.Location = new System.Drawing.Point(0x34, 0x12);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxLayer.Size = new Size(0x120, 0x15);
            this.comboBoxLayer.TabIndex = 0x31;
            this.comboBoxLayer.SelectedIndexChanged += new EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 0xf1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x7d, 12);
            this.label4.TabIndex = 50;
            this.label4.Text = "并满足如下条件的要素";
            this.groupBox1.Controls.Add(this.cboUnit);
            this.groupBox1.Controls.Add(this.txtRadius);
            this.groupBox1.Controls.Add(this.chkUseBuffer);
            this.groupBox1.Controls.Add(this.chkUseSelectFeature);
            this.groupBox1.Controls.Add(this.cboSourceLayer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboSpatialRelation);
            this.groupBox1.Location = new System.Drawing.Point(7, 0x5d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x157, 0x91);
            this.groupBox1.TabIndex = 0x33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间过滤";
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new System.Drawing.Point(0x6a, 0x76);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Size = new Size(0x60, 0x15);
            this.cboUnit.TabIndex = 0x21;
            this.cboUnit.Visible = false;
            this.txtRadius.EditValue = "0";
            this.txtRadius.Location = new System.Drawing.Point(10, 0x76);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new Size(0x58, 0x15);
            this.txtRadius.TabIndex = 0x20;
            this.chkUseBuffer.Location = new System.Drawing.Point(10, 0x5e);
            this.chkUseBuffer.Name = "chkUseBuffer";
            this.chkUseBuffer.Properties.Caption = "对要素进行缓冲区操作";
            this.chkUseBuffer.Size = new Size(0xa8, 0x13);
            this.chkUseBuffer.TabIndex = 0x1f;
            this.chkUseBuffer.CheckedChanged += new EventHandler(this.chkUseBuffer_CheckedChanged);
            this.chkUseSelectFeature.Location = new System.Drawing.Point(0xa2, 0x24);
            this.chkUseSelectFeature.Name = "chkUseSelectFeature";
            this.chkUseSelectFeature.Properties.Caption = "使用选中的要素";
            this.chkUseSelectFeature.Size = new Size(0x68, 0x13);
            this.chkUseSelectFeature.TabIndex = 30;
            this.cboSourceLayer.EditValue = "";
            this.cboSourceLayer.Location = new System.Drawing.Point(10, 0x24);
            this.cboSourceLayer.Name = "cboSourceLayer";
            this.cboSourceLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSourceLayer.Size = new Size(0x90, 0x15);
            this.cboSourceLayer.TabIndex = 0x1d;
            this.cboSourceLayer.SelectedIndexChanged += new EventHandler(this.cboSourceLayer_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 0x1c;
            this.label3.Text = "与图层中的要素";
            this.cboSpatialRelation.EditValue = "相交";
            this.cboSpatialRelation.Location = new System.Drawing.Point(10, 0x41);
            this.cboSpatialRelation.Name = "cboSpatialRelation";
            this.cboSpatialRelation.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSpatialRelation.Properties.Items.AddRange(new object[] { "相交", "包围矩形相交", "相接", "重叠", "被包含", "包含" });
            this.cboSpatialRelation.Size = new Size(0xf8, 0x15);
            this.cboSpatialRelation.TabIndex = 0x1b;
            this.panel1.Controls.Add(this.lblStatu);
            this.panel1.Location = new System.Drawing.Point(0xa2, 0xbf);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x98, 0x40);
            this.panel1.TabIndex = 0x34;
            this.panel1.Visible = false;
            this.lblStatu.AutoSize = true;
            this.lblStatu.Location = new System.Drawing.Point(8, 8);
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new Size(0x6b, 12);
            this.lblStatu.TabIndex = 14;
            this.lblStatu.Text = "正在查找请稍候...";
            this.btnCreateQuery.Location = new System.Drawing.Point(7, 0x14b);
            this.btnCreateQuery.Name = "btnCreateQuery";
            this.btnCreateQuery.Size = new Size(0x5d, 0x18);
            this.btnCreateQuery.TabIndex = 0x35;
            this.btnCreateQuery.Text = "构建查询条件";
            this.btnCreateQuery.Click += new EventHandler(this.btnCreateQuery_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnCreateQuery);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.comboBoxLayer);
            base.Controls.Add(this.memEditWhereCaluse);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.chkUsetSelectedLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboOperationType);
            base.Controls.Add(this.label1);
            base.Name = "SpatialAndAttributeQueryCtrl";
            base.Size = new Size(0x16e, 0x180);
            base.Load += new EventHandler(this.SpatialAndAttributeQueryCtrl_Load);
            this.chkUsetSelectedLayer.Properties.EndInit();
            this.cboOperationType.Properties.EndInit();
            this.memEditWhereCaluse.Properties.EndInit();
            this.comboBoxLayer.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboUnit.Properties.EndInit();
            this.txtRadius.Properties.EndInit();
            this.chkUseBuffer.Properties.EndInit();
            this.chkUseSelectFeature.Properties.EndInit();
            this.cboSourceLayer.Properties.EndInit();
            this.cboSpatialRelation.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(ComboBoxEdit comboBoxEdit_0, ICompositeLayer icompositeLayer_0, bool bool_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (bool_0 && this.method_1(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else if (!bool_0)
                        {
                            if (this.chkUseBuffer.Checked)
                            {
                                if ((layer as IFeatureLayer).Selectable)
                                {
                                    comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                                }
                            }
                            else
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else
                        {
                            comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(comboBoxEdit_0, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private bool method_1(ILayer ilayer_0)
        {
            return ((this.comboBoxLayer.SelectedIndex != -1) && ((this.comboBoxLayer.SelectedItem as LayerObjectWrap).Layer == ilayer_0));
        }

        private void method_2(ComboBoxEdit comboBoxEdit_0, bool bool_0)
        {
            this.cboSourceLayer.Properties.Items.Clear();
            this.cboSourceLayer.Text = "";
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (bool_0 && this.method_1(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else if (!bool_0)
                        {
                            if (this.chkUseBuffer.Checked)
                            {
                                if ((layer as IFeatureLayer).Selectable)
                                {
                                    comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                                }
                            }
                            else
                            {
                                comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                            }
                        }
                        else
                        {
                            comboBoxEdit_0.Properties.Items.Add(new LayerObjectWrap(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(comboBoxEdit_0, layer as ICompositeLayer, bool_0);
                }
            }
        }

        private void method_3(IFeatureLayer ifeatureLayer_0, IFeatureCursor ifeatureCursor_0, esriSpatialRelEnum esriSpatialRelEnum_0, esriSelectionResultEnum esriSelectionResultEnum_0, string string_0)
        {
            IFeature feature = ifeatureCursor_0.NextFeature();
            IFeatureSelection selection = ifeatureLayer_0 as IFeatureSelection;
            double bufferDistance = selection.BufferDistance;
            if (this.chkUseBuffer.Checked)
            {
                try
                {
                    selection.BufferDistance = double.Parse(this.txtRadius.Text);
                }
                catch
                {
                }
            }
            bool flag = true;
            while (feature != null)
            {
                if (feature.Shape == null)
                {
                    feature = ifeatureCursor_0.NextFeature();
                }
                else if (feature.Shape.IsEmpty)
                {
                    feature = ifeatureCursor_0.NextFeature();
                }
                else
                {
                    try
                    {
                        ISpatialFilter filter = new SpatialFilter {
                            SpatialRel = esriSpatialRelEnum_0,
                            WhereClause = string_0
                        };
                        if (selection.BufferDistance > 0.0)
                        {
                            filter.Geometry = (feature.Shape as ITopologicalOperator).Buffer(selection.BufferDistance);
                        }
                        else
                        {
                            filter.Geometry = feature.Shape;
                        }
                        if (flag)
                        {
                            selection.SelectFeatures(filter, esriSelectionResultEnum_0, false);
                            flag = false;
                        }
                        else
                        {
                            selection.SelectFeatures(filter, esriSelectionResultEnum.esriSelectionResultAdd, false);
                        }
                    }
                    catch
                    {
                    }
                    feature = ifeatureCursor_0.NextFeature();
                }
            }
            selection.BufferDistance = bufferDistance;
        }

        private void SpatialAndAttributeQueryCtrl_Load(object sender, EventArgs e)
        {
            this.method_2(this.comboBoxLayer, false);
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }

        internal class LayerObjectWrap
        {
            private ILayer ilayer_0 = null;

            public LayerObjectWrap(ILayer ilayer_1)
            {
                this.ilayer_0 = ilayer_1;
            }

            public override string ToString()
            {
                if (this.ilayer_0 != null)
                {
                    return this.ilayer_0.Name;
                }
                return "";
            }

            public ILayer Layer
            {
                get
                {
                    return this.ilayer_0;
                }
            }
        }
    }
}

