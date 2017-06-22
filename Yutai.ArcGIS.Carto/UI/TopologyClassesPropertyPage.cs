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
    public partial class TopologyClassesPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ITopology itopology_0 = null;
        private ITopologyLayer itopologyLayer_0 = null;

        public TopologyClassesPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
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

        internal partial class TopoClassWrap
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

