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
using Yutai.ArcGIS.Common.Wrapper;
using ItemCheckEventArgs = System.Windows.Forms.ItemCheckEventArgs;
using ItemCheckEventHandler = System.Windows.Forms.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public class SelectByLocationCtrl : UserControl
    {
        private bool bool_0 = true;
        private SimpleButton btnApply;
        private ComboBoxEdit cboOperationType;
        private ComboBoxEdit cboSourceLayer;
        private ComboBoxEdit cboSpatialRelation;
        private ComboBoxEdit cboUnit;
        private CheckedListBox checkedListBoxLayer;
        private CheckEdit chkUseBuffer;
        private CheckEdit chkUseSelectFeature;
        private CheckEdit chkUsetSelectedLayer;
        private Container container_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblStatu;
        private Panel panel1;
        private TextEdit txtRadius;

        public SelectByLocationCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.cboSourceLayer.SelectedIndex != -1)
            {
                IFeatureLayer layer = null;
                layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
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
                for (int i = 0; i < this.checkedListBoxLayer.CheckedItems.Count; i++)
                {
                    layer2 = (this.checkedListBoxLayer.CheckedItems[i] as LayerObject).Layer as IFeatureLayer;
                    this.method_6(layer2, cursor as IFeatureCursor, esriSpatialRelIntersects, esriSelectionResultNew);
                }
                (this.imap_0 as IActiveView).Refresh();
                ComReleaser.ReleaseCOMObject(cursor);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.btnApply.Enabled = false;
            this.panel1.Visible = true;
            this.bool_0 = false;
            try
            {
                this.Apply();
            }
            catch
            {
            }
            this.bool_0 = true;
            this.btnApply.Enabled = true;
            this.panel1.Visible = false;
            Cursor = Cursors.Default;
        }

        private void cboSourceLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSourceLayer.SelectedIndex > -1)
            {
                IFeatureLayer layer = (this.cboSourceLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
                if ((layer as IFeatureSelection).SelectionSet.Count == 0)
                {
                    this.chkUseSelectFeature.Enabled = false;
                    this.chkUseSelectFeature.Checked = false;
                }
                else
                {
                    this.chkUseSelectFeature.Checked = true;
                    if (this.method_5(layer))
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

        private void checkedListBoxLayer_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.checkedListBoxLayer.SetItemCheckState(e.Index, e.NewValue);
                this.bool_0 = true;
                this.method_3();
            }
        }

        private void chkUseBuffer_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRadius.Enabled = this.chkUseBuffer.Checked;
            this.cboUnit.Enabled = this.chkUseBuffer.Checked;
        }

        private void chkUsetSelectedLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUsetSelectedLayer.Checked)
            {
                for (int i = this.checkedListBoxLayer.Items.Count - 1; i >= 0; i--)
                {
                    LayerObject obj2 = this.checkedListBoxLayer.Items[i] as LayerObject;
                    if (!(obj2.Layer as IFeatureLayer).Selectable)
                    {
                        this.checkedListBoxLayer.Items.RemoveAt(i);
                    }
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboOperationType = new ComboBoxEdit();
            this.checkedListBoxLayer = new CheckedListBox();
            this.label2 = new Label();
            this.chkUsetSelectedLayer = new CheckEdit();
            this.cboSpatialRelation = new ComboBoxEdit();
            this.label3 = new Label();
            this.cboSourceLayer = new ComboBoxEdit();
            this.chkUseSelectFeature = new CheckEdit();
            this.chkUseBuffer = new CheckEdit();
            this.txtRadius = new TextEdit();
            this.cboUnit = new ComboBoxEdit();
            this.btnApply = new SimpleButton();
            this.panel1 = new Panel();
            this.lblStatu = new Label();
            this.cboOperationType.Properties.BeginInit();
            this.chkUsetSelectedLayer.Properties.BeginInit();
            this.cboSpatialRelation.Properties.BeginInit();
            this.cboSourceLayer.Properties.BeginInit();
            this.chkUseSelectFeature.Properties.BeginInit();
            this.chkUseBuffer.Properties.BeginInit();
            this.txtRadius.Properties.BeginInit();
            this.cboUnit.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xd1, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "根据要素的空间关系从图层中选择要素";
            this.cboOperationType.EditValue = "选择要素";
            this.cboOperationType.Location = new System.Drawing.Point(8, 0xa8);
            this.cboOperationType.Name = "cboOperationType";
            this.cboOperationType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboOperationType.Properties.Items.AddRange(new object[] { "选择要素", "选择要素,并添加到选择集中", "的选择集中删除", "的选择集中选择" });
            this.cboOperationType.Size = new Size(0xf8, 0x15);
            this.cboOperationType.TabIndex = 1;
            this.checkedListBoxLayer.Location = new System.Drawing.Point(8, 0x38);
            this.checkedListBoxLayer.Name = "checkedListBoxLayer";
            this.checkedListBoxLayer.Size = new Size(0xf8, 100);
            this.checkedListBoxLayer.TabIndex = 2;
            this.checkedListBoxLayer.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxLayer_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "从下列图层";
            this.chkUsetSelectedLayer.Location = new System.Drawing.Point(0x60, 0x20);
            this.chkUsetSelectedLayer.Name = "chkUsetSelectedLayer";
            this.chkUsetSelectedLayer.Properties.Caption = "只显示可选图层";
            this.chkUsetSelectedLayer.Size = new Size(0x70, 0x13);
            this.chkUsetSelectedLayer.TabIndex = 4;
            this.chkUsetSelectedLayer.Visible = false;
            this.chkUsetSelectedLayer.CheckedChanged += new EventHandler(this.chkUsetSelectedLayer_CheckedChanged);
            this.cboSpatialRelation.EditValue = "相交";
            this.cboSpatialRelation.Location = new System.Drawing.Point(8, 0xf8);
            this.cboSpatialRelation.Name = "cboSpatialRelation";
            this.cboSpatialRelation.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSpatialRelation.Properties.Items.AddRange(new object[] { "相交", "包围矩形相交", "相接", "重叠", "被包含", "包含" });
            this.cboSpatialRelation.Size = new Size(0xf8, 0x15);
            this.cboSpatialRelation.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 200);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "与图层中的要素";
            this.cboSourceLayer.EditValue = "";
            this.cboSourceLayer.Location = new System.Drawing.Point(8, 0xd8);
            this.cboSourceLayer.Name = "cboSourceLayer";
            this.cboSourceLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSourceLayer.Size = new Size(0x90, 0x15);
            this.cboSourceLayer.TabIndex = 7;
            this.cboSourceLayer.SelectedIndexChanged += new EventHandler(this.cboSourceLayer_SelectedIndexChanged);
            this.chkUseSelectFeature.Location = new System.Drawing.Point(160, 0xd8);
            this.chkUseSelectFeature.Name = "chkUseSelectFeature";
            this.chkUseSelectFeature.Properties.Caption = "使用选中的要素";
            this.chkUseSelectFeature.Size = new Size(0x68, 0x13);
            this.chkUseSelectFeature.TabIndex = 8;
            this.chkUseBuffer.Location = new System.Drawing.Point(8, 280);
            this.chkUseBuffer.Name = "chkUseBuffer";
            this.chkUseBuffer.Properties.Caption = "对要素进行缓冲区操作";
            this.chkUseBuffer.Size = new Size(0xa8, 0x13);
            this.chkUseBuffer.TabIndex = 9;
            this.chkUseBuffer.CheckedChanged += new EventHandler(this.chkUseBuffer_CheckedChanged);
            this.txtRadius.EditValue = "0";
            this.txtRadius.Location = new System.Drawing.Point(8, 0x130);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new Size(0x58, 0x15);
            this.txtRadius.TabIndex = 10;
            this.cboUnit.EditValue = "";
            this.cboUnit.Location = new System.Drawing.Point(0x68, 0x130);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboUnit.Size = new Size(0x60, 0x15);
            this.cboUnit.TabIndex = 11;
            this.cboUnit.Visible = false;
            this.btnApply.Location = new System.Drawing.Point(0x90, 0x150);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x40, 0x18);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.panel1.Controls.Add(this.lblStatu);
            this.panel1.Location = new System.Drawing.Point(40, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x98, 0x40);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            this.lblStatu.AutoSize = true;
            this.lblStatu.Location = new System.Drawing.Point(8, 8);
            this.lblStatu.Name = "lblStatu";
            this.lblStatu.Size = new Size(0x6b, 12);
            this.lblStatu.TabIndex = 14;
            this.lblStatu.Text = "正在查找请稍候...";
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.cboUnit);
            base.Controls.Add(this.txtRadius);
            base.Controls.Add(this.chkUseBuffer);
            base.Controls.Add(this.chkUseSelectFeature);
            base.Controls.Add(this.cboSourceLayer);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboSpatialRelation);
            base.Controls.Add(this.chkUsetSelectedLayer);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.checkedListBoxLayer);
            base.Controls.Add(this.cboOperationType);
            base.Controls.Add(this.label1);
            base.Name = "SelectByLocationCtrl";
            base.Size = new Size(280, 0x180);
            base.Load += new EventHandler(this.SelectByLocationCtrl_Load);
            this.cboOperationType.Properties.EndInit();
            this.chkUsetSelectedLayer.Properties.EndInit();
            this.cboSpatialRelation.Properties.EndInit();
            this.cboSourceLayer.Properties.EndInit();
            this.chkUseSelectFeature.Properties.EndInit();
            this.chkUseBuffer.Properties.EndInit();
            this.txtRadius.Properties.EndInit();
            this.cboUnit.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if (((layer as IFeatureLayer).FeatureClass != null) && !((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable) ? this.chkUsetSelectedLayer.Checked : false))
                    {
                        this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
            }
        }

        private void method_1()
        {
            this.checkedListBoxLayer.Items.Clear();
            for (int i = 0; i < this.imap_0.LayerCount; i++)
            {
                ILayer layer = this.imap_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if (((layer as IFeatureLayer).FeatureClass != null) && !((!this.chkUsetSelectedLayer.Checked || !(layer as IFeatureLayer).Selectable) ? this.chkUsetSelectedLayer.Checked : false))
                    {
                        if (layer is IFeatureLayerSelectionEvents_Event)
                        {
                            (layer as IFeatureLayerSelectionEvents_Event).FeatureLayerSelectionChanged+=(new IFeatureLayerSelectionEvents_FeatureLayerSelectionChangedEventHandler(this.method_4));
                        }
                        this.checkedListBoxLayer.Items.Add(new LayerObject(layer));
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
            }
        }

        private void method_2(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    if ((layer as IFeatureLayer).FeatureClass != null)
                    {
                        if (this.method_5(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                            }
                        }
                        else
                        {
                            this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer);
                }
            }
        }

        private void method_3()
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
                        if (this.method_5(layer))
                        {
                            IFeatureLayer layer2 = layer as IFeatureLayer;
                            if ((layer2 as IFeatureSelection).SelectionSet.Count > 0)
                            {
                                this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                            }
                        }
                        else
                        {
                            this.cboSourceLayer.Properties.Items.Add(new LayerObject(layer));
                        }
                    }
                }
                else if (layer is IGroupLayer)
                {
                    this.method_2(layer as ICompositeLayer);
                }
            }
        }

        private void method_4()
        {
            if (this.bool_0)
            {
                this.method_3();
                this.cboSourceLayer_SelectedIndexChanged(null, null);
            }
        }

        private bool method_5(ILayer ilayer_0)
        {
            for (int i = 0; i < this.checkedListBoxLayer.Items.Count; i++)
            {
                if ((this.checkedListBoxLayer.Items[i] as LayerObject).Layer == ilayer_0)
                {
                    return this.checkedListBoxLayer.GetItemChecked(i);
                }
            }
            return false;
        }

        private void method_6(IFeatureLayer ifeatureLayer_0, IFeatureCursor ifeatureCursor_0, esriSpatialRelEnum esriSpatialRelEnum_0, esriSelectionResultEnum esriSelectionResultEnum_0)
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
                            SpatialRel = esriSpatialRelEnum_0
                        };
                        if (selection.BufferDistance > 0.0)
                        {
                            filter.Geometry = (feature.Shape as ITopologicalOperator).Buffer(selection.BufferDistance);
                        }
                        else
                        {
                            IGeometry shape = feature.Shape;
                            filter.Geometry = shape;
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

        private void SelectByLocationCtrl_Load(object sender, EventArgs e)
        {
            this.method_1();
            this.method_3();
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

