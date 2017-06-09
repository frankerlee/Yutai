using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public class AttributeQueryControl : UserControl, IDockContent
    {
        private AttributeQueryBuliderControl attributeQueryBuliderControl_0 = new AttributeQueryBuliderControl();
        private SimpleButton btnApply;
        private SimpleButton btnClear;
        private SimpleButton btnClose;
        private ComboBoxEdit cboSelectType;
        private CheckEdit chkShowSelectbaleLayer;
        private ComboBoxEdit comboBoxLayer;
        private Container container_0 = null;
        private esriSelectionResultEnum esriSelectionResultEnum_0 = esriSelectionResultEnum.esriSelectionResultNew;
        private IBasicMap ibasicMap_0;
        private ILayer ilayer_0 = null;
        private Label label1;
        private Label label3;
        private Panel panel1;
        private Panel panel2;

        public AttributeQueryControl()
        {
            this.InitializeComponent();
            this.attributeQueryBuliderControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.attributeQueryBuliderControl_0);
            this.Text = "属性查询";
        }

        private void AttributeQueryControl_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.ilayer_0 != null)
            {
                this.attributeQueryBuliderControl_0.Apply();
                try
                {
                    IQueryFilter filter = new QueryFilter{
                        WhereClause = this.attributeQueryBuliderControl_0.WhereCaluse
                    };
                    IFeatureSelection selection = this.ilayer_0 as IFeatureSelection;
                    if (selection != null)
                    {
                        selection.SelectFeatures(filter, this.esriSelectionResultEnum_0, false);
                        if (selection.SelectionSet.Count < 1)
                        {
                            MessageBox.Show("没有符合条件的纪录！");
                        }
                        else
                        {
                            (this.ibasicMap_0 as IActiveView).Refresh();
                        }
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.attributeQueryBuliderControl_0.ClearWhereCaluse();
        }

        private void cboSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.esriSelectionResultEnum_0 = (esriSelectionResultEnum) this.cboSelectType.SelectedIndex;
        }

        private void chkShowSelectbaleLayer_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(i);
                if ((layer is IFeatureLayer) && (!this.chkShowSelectbaleLayer.Checked || (layer as IFeatureLayer).Selectable))
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        private void comboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxLayer.SelectedItem != null)
            {
                this.ilayer_0 = (this.comboBoxLayer.SelectedItem as LayerObject).Layer;
                this.attributeQueryBuliderControl_0.CurrentLayer = this.ilayer_0;
            }
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
            this.panel2 = new Panel();
            this.btnClear = new SimpleButton();
            this.panel1 = new Panel();
            this.cboSelectType = new ComboBoxEdit();
            this.chkShowSelectbaleLayer = new CheckEdit();
            this.label3 = new Label();
            this.comboBoxLayer = new ComboBoxEdit();
            this.label1 = new Label();
            this.btnClose = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.panel1.SuspendLayout();
            this.cboSelectType.Properties.BeginInit();
            this.chkShowSelectbaleLayer.Properties.BeginInit();
            this.comboBoxLayer.Properties.BeginInit();
            base.SuspendLayout();
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0x58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x178, 360);
            this.panel2.TabIndex = 0x39;
            this.btnClear.Location = new Point(8, 0x1c8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x38, 0x18);
            this.btnClear.TabIndex = 0x38;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.panel1.Controls.Add(this.cboSelectType);
            this.panel1.Controls.Add(this.chkShowSelectbaleLayer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxLayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x178, 0x58);
            this.panel1.TabIndex = 0x37;
            this.cboSelectType.EditValue = "创建一个新的选择集";
            this.cboSelectType.Location = new Point(0x38, 0x38);
            this.cboSelectType.Name = "cboSelectType";
            this.cboSelectType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSelectType.Properties.Items.AddRange(new object[] { "创建一个新的选择集", "添加到现有选择集", "从现有选择集中删除", "从现有选择集中选择" });
            this.cboSelectType.Size = new Size(0x128, 0x17);
            this.cboSelectType.TabIndex = 0x33;
            this.cboSelectType.SelectedIndexChanged += new EventHandler(this.cboSelectType_SelectedIndexChanged);
            this.chkShowSelectbaleLayer.Location = new Point(0x38, 0x20);
            this.chkShowSelectbaleLayer.Name = "chkShowSelectbaleLayer";
            this.chkShowSelectbaleLayer.Properties.Caption = "只显示可选择图层";
            this.chkShowSelectbaleLayer.Size = new Size(0xa8, 0x13);
            this.chkShowSelectbaleLayer.TabIndex = 50;
            this.chkShowSelectbaleLayer.CheckedChanged += new EventHandler(this.chkShowSelectbaleLayer_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 0x31;
            this.label3.Text = "方法:";
            this.comboBoxLayer.EditValue = "";
            this.comboBoxLayer.Location = new Point(0x38, 7);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxLayer.Size = new Size(0x120, 0x17);
            this.comboBoxLayer.TabIndex = 0x30;
            this.comboBoxLayer.SelectedIndexChanged += new EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0x2f;
            this.label1.Text = "图层:";
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x128, 0x1c8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x38, 0x18);
            this.btnClose.TabIndex = 0x36;
            this.btnClose.Text = "关闭";
            this.btnApply.Location = new Point(0xe8, 0x1c8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 0x35;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.Name = "AttributeQueryControl";
            base.Size = new Size(0x178, 0x1e8);
            base.Load += new EventHandler(this.AttributeQueryControl_Load);
            this.panel1.ResumeLayout(false);
            this.cboSelectType.Properties.EndInit();
            this.chkShowSelectbaleLayer.Properties.EndInit();
            this.comboBoxLayer.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
        }

        private void method_1()
        {
            this.comboBoxLayer.Properties.Items.Clear();
            for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    this.method_0(layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    this.comboBoxLayer.Properties.Items.Add(new LayerObject(layer));
                }
            }
            if (this.comboBoxLayer.Properties.Items.Count > 0)
            {
                this.comboBoxLayer.SelectedIndex = 0;
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Float;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
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
    }
}

