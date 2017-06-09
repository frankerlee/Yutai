using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;


namespace Yutai.ArcGIS.Carto.UI
{
    public class TopologyClassesPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private GridControl gridControl1;
        private GridView gridView1;
        private ITopology itopology_0 = null;
        private ITopologyLayer itopologyLayer_0 = null;
        private Label label1;
        private SpinEdit txtValue;
        private VertXtraGrid vertXtraGrid_0;

        public TopologyClassesPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.txtValue = new SpinEdit();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            this.txtValue.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "优先级数值:";
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new Point(0x10, 40);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(0x138, 200);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            int[] bits = new int[4];
            bits[0] = 1;
            this.txtValue.EditValue = new decimal(bits);
            this.txtValue.Location = new Point(0x60, 8);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtValue.Properties.Enabled = false;
            int[] bits2 = new int[4];
            bits2[0] = 50;
            this.txtValue.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 1;
            this.txtValue.Properties.MinValue = new decimal(bits3);
            this.txtValue.Properties.UseCtrlIncrement = false;
            this.txtValue.Size = new Size(0x60, 0x17);
            this.txtValue.TabIndex = 6;
            base.Controls.Add(this.txtValue);
            base.Controls.Add(this.gridControl1);
            base.Controls.Add(this.label1);
            base.Name = "TopologyClassesPropertyPage";
            base.Size = new Size(0x198, 0x120);
            base.Load += new EventHandler(this.TopologyClassesPropertyPage_Load);
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            this.txtValue.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void TopologyClassesPropertyPage_Load(object sender, EventArgs e)
        {
            try
            {
                int num2;
                this.vertXtraGrid_0.EditorName = "要素类";
                this.vertXtraGrid_0.EditorValue = "优先级";
                int num = 1;
                IEnumFeatureClass classes = (this.itopology_0 as IFeatureClassContainer).Classes;
                classes.Reset();
                ITopologyClass class3 = classes.Next() as ITopologyClass;
                IList list = new ArrayList();
                for (num2 = 1; num2 <= num; num2++)
                {
                    list.Add(num2);
                }
                while (class3 != null)
                {
                    if (class3.IsInTopology)
                    {
                        num = (num < class3.XYRank) ? class3.XYRank : num;
                        this.vertXtraGrid_0.AddComBoBox((class3 as IDataset).Name, class3.XYRank, list, false, new TopoClassWrap(class3, false));
                    }
                    class3 = classes.Next() as ITopologyClass;
                }
                this.txtValue.Value = num;
                list.Clear();
                for (num2 = 1; num2 <= num; num2++)
                {
                    list.Add(num2);
                }
                for (num2 = 0; num2 < this.gridView1.RowCount; num2++)
                {
                    this.vertXtraGrid_0.ChangeItem(num2, ColumnAttribute.CA_COMBOBOX, list, 0.0, 0.0);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            this.bool_0 = true;
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public object SelectItem
        {
            set
            {
                this.itopologyLayer_0 = value as ITopologyLayer;
                this.itopology_0 = this.itopologyLayer_0.Topology;
            }
        }

        internal class TopoClassWrap
        {
            private bool bool_0 = false;
            private ITopologyClass itopologyClass_0 = null;

            public TopoClassWrap(ITopologyClass itopologyClass_1, bool bool_1)
            {
                this.itopologyClass_0 = itopologyClass_1;
                this.bool_0 = bool_1;
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public ITopologyClass TopoClass
            {
                get
                {
                    return this.itopologyClass_0;
                }
            }
        }
    }
}

