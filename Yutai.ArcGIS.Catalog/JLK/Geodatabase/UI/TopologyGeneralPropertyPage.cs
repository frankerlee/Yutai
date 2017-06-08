namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Utility.BaseClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class TopologyGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ITopology itopology_0 = null;
        private Label label1;
        private Label label2;
        private Label lblStatus;
        private Label lblTopoError1;
        private Label lblTopoError2;
        private string string_0 = "常规";
        private TextEdit txtClusterTolerance;
        private TextEdit txtName;

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
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.lblStatus = new Label();
            this.txtName = new TextEdit();
            this.txtClusterTolerance = new TextEdit();
            this.lblTopoError1 = new Label();
            this.lblTopoError2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtName.Properties.BeginInit();
            this.txtClusterTolerance.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "容限值:";
            this.groupBox1.Controls.Add(this.lblTopoError2);
            this.groupBox1.Controls.Add(this.lblTopoError1);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Location = new Point(0x10, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 0x98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new Point(0x20, 0x20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0, 0x11);
            this.lblStatus.TabIndex = 0;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x40, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0xb8, 0x17);
            this.txtName.TabIndex = 3;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.txtClusterTolerance.EditValue = "";
            this.txtClusterTolerance.Location = new Point(0x40, 0x30);
            this.txtClusterTolerance.Name = "txtClusterTolerance";
            this.txtClusterTolerance.Size = new Size(0xb8, 0x17);
            this.txtClusterTolerance.TabIndex = 4;
            this.txtClusterTolerance.EditValueChanged += new EventHandler(this.txtClusterTolerance_EditValueChanged);
            this.lblTopoError1.Location = new Point(0x10, 0x18);
            this.lblTopoError1.Name = "lblTopoError1";
            this.lblTopoError1.Size = new Size(0x88, 0x18);
            this.lblTopoError1.TabIndex = 1;
            this.lblTopoError2.Location = new Point(0x30, 0x38);
            this.lblTopoError2.Name = "lblTopoError2";
            this.lblTopoError2.Size = new Size(0xd8, 0x38);
            this.lblTopoError2.TabIndex = 2;
            base.Controls.Add(this.txtClusterTolerance);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TopologyGeneralPropertyPage";
            base.Size = new Size(0x158, 0x108);
            base.Load += new EventHandler(this.TopologyGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtName.Properties.EndInit();
            this.txtClusterTolerance.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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
                if (this.onValueChangeEventHandler_0 != null)
                {
                    this.onValueChangeEventHandler_0();
                }
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.onValueChangeEventHandler_0 != null)
                {
                    this.onValueChangeEventHandler_0();
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

