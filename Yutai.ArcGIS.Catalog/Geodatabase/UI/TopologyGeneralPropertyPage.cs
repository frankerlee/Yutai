using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class TopologyGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ITopology itopology_0 = null;
        private string string_0 = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public TopologyGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                try
                {
                    if (this.txtName.Text.Trim().Length > 0)
                    {
                        (this.itopology_0 as IDataset).Rename(this.txtName.Text.Trim());
                    }
                }
                catch
                {
                }
                try
                {
                    double.Parse(this.txtClusterTolerance.Text);
                }
                catch
                {
                }
            }
        }

        public void Cancel()
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.itopology_0 = object_0 as ITopology;
        }

        private void TopologyGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            ISchemaLock @lock = this.itopology_0 as ISchemaLock;
            IEnumSchemaLockInfo schemaLockInfo = null;
            @lock.GetCurrentSchemaLocks(out schemaLockInfo);
            schemaLockInfo.Reset();
            if (schemaLockInfo.Next() == null)
            {
                this.txtName.Properties.ReadOnly = true;
                this.txtClusterTolerance.Properties.ReadOnly = true;
            }
            this.txtName.Text = (this.itopology_0 as IDataset).Name;
            this.txtClusterTolerance.Text = this.itopology_0.ClusterTolerance.ToString();
            this.bool_0 = true;
            switch (this.itopology_0.State)
            {
                case esriTopologyState.esriTSUnanalyzed:
                    this.lblTopoError1.Text = "没有校验";
                    this.lblTopoError2.Text = "在拓扑中存在一个或多个脏区。脏区是指被编辑过的区域";
                    break;

                case esriTopologyState.esriTSAnalyzedWithErrors:
                    this.lblTopoError1.Text = "已经校验 - 存在错误";
                    this.lblTopoError2.Text = "所有编辑过的拓扑已经被校验过。有一个或多个拓扑错误存在";
                    break;

                case esriTopologyState.esriTSAnalyzedWithoutErrors:
                    this.lblTopoError1.Text = "已经校验 - 没有错误误";
                    this.lblTopoError2.Text = "所有编辑过的拓扑已经被校验过。没有拓扑错误存在";
                    break;
            }
        }

        private void txtClusterTolerance_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}