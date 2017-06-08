namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using JLK.Utility.Raster;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmCreateRasterFolder : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectOutLocation;
        private SimpleButton btnSelectRasterSpatialRef;
        private SimpleButton btnSelectSpatialRef;
        private ComboBoxEdit cboConfigKey;
        private ComboBoxEdit cboPixelType;
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReference ispatialReference_1 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextEdit txtLoaction;
        private TextEdit txtRasterDatastName;
        private TextEdit txtRasterSpatialRefName;
        private TextEdit txtSpatialRefName;

        public frmCreateRasterFolder()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtRasterDatastName.Text.Trim().Length == 0)
            {
                MessageBox.Show("新建栅格目录名字不能为空!");
            }
            else
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                if (RasterUtility.createCatalog(ex, this.txtRasterDatastName.Text, "Raster", "Shape", this.ispatialReference_0, this.ispatialReference_1, this.cboPixelType.SelectedIndex == 0, null, this.cboConfigKey.Text) != null)
                {
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.cboConfigKey.Properties.Items.Clear();
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_1(ex as IGeodatabaseRelease, this.ispatialReference_1);
                    this.method_1(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    this.txtLoaction.Name = this.igxObject_0.FullName;
                    if ((this.igxObject_0 is IGxDatabase) && (this.igxObject_0 as IGxDatabase).IsRemoteDatabase)
                    {
                        this.method_2((this.igxObject_0 as IGxDatabase).Workspace as IWorkspaceConfiguration);
                    }
                }
            }
        }

        private void btnSelectRasterSpatialRef_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference {
                SpatialRefrence = this.ispatialReference_1
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_1 = reference.SpatialRefrence;
                this.txtRasterSpatialRefName.Text = this.ispatialReference_1.Name;
            }
        }

        private void btnSelectSpatialRef_Click(object sender, EventArgs e)
        {
            ISpatialReference reference2;
            frmSpatialReference reference = new frmSpatialReference();
            if (this.ispatialReference_0 == null)
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                reference2 = this.method_0(ex as IGeodatabaseRelease);
            }
            else
            {
                reference2 = this.ispatialReference_0;
            }
            reference.SpatialRefrence = reference2;
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = reference.SpatialRefrence;
                this.txtSpatialRefName.Text = this.ispatialReference_0.Name;
            }
        }

        private void cboPixelType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCreateRasterFolder_Load(object sender, EventArgs e)
        {
            this.cboConfigKey.Properties.Items.Clear();
            if (this.igxObject_0 != null)
            {
                this.txtLoaction.Text = this.igxObject_0.FullName;
                if ((this.igxObject_0 is IGxDatabase) && (this.igxObject_0 as IGxDatabase).IsRemoteDatabase)
                {
                    this.method_2((this.igxObject_0 as IGxDatabase).Workspace as IWorkspaceConfiguration);
                }
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmCreateRasterFolder));
            this.label1 = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnSelectSpatialRef = new SimpleButton();
            this.label6 = new Label();
            this.txtLoaction = new TextEdit();
            this.txtRasterDatastName = new TextEdit();
            this.txtSpatialRefName = new TextEdit();
            this.cboPixelType = new ComboBoxEdit();
            this.txtRasterSpatialRefName = new TextEdit();
            this.btnSelectRasterSpatialRef = new SimpleButton();
            this.label3 = new Label();
            this.cboConfigKey = new ComboBoxEdit();
            this.txtLoaction.Properties.BeginInit();
            this.txtRasterDatastName.Properties.BeginInit();
            this.txtSpatialRefName.Properties.BeginInit();
            this.cboPixelType.Properties.BeginInit();
            this.txtRasterSpatialRefName.Properties.BeginInit();
            this.cboConfigKey.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出位置";
            this.btnSelectOutLocation.Image = (Image) manager.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(0xb0, 0x18);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "栅格数据目录名称";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x110);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4d, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "栅格管理类型";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0xd0);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "配置关键值";
            this.btnOK.Location = new System.Drawing.Point(0x60, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x98, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnSelectSpatialRef.Image = (Image) manager.GetObject("btnSelectSpatialRef.Image");
            this.btnSelectSpatialRef.Location = new System.Drawing.Point(0xb0, 0xb0);
            this.btnSelectSpatialRef.Name = "btnSelectSpatialRef";
            this.btnSelectSpatialRef.Size = new Size(0x18, 0x18);
            this.btnSelectSpatialRef.TabIndex = 0x10;
            this.btnSelectSpatialRef.Click += new EventHandler(this.btnSelectSpatialRef_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 0x98);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "几何坐标系(可选)";
            this.txtLoaction.EditValue = "";
            this.txtLoaction.Location = new System.Drawing.Point(8, 0x18);
            this.txtLoaction.Name = "txtLoaction";
            this.txtLoaction.Size = new Size(160, 0x15);
            this.txtLoaction.TabIndex = 0x11;
            this.txtRasterDatastName.EditValue = "";
            this.txtRasterDatastName.Location = new System.Drawing.Point(8, 0x48);
            this.txtRasterDatastName.Name = "txtRasterDatastName";
            this.txtRasterDatastName.Size = new Size(0xc0, 0x15);
            this.txtRasterDatastName.TabIndex = 0x12;
            this.txtSpatialRefName.EditValue = "";
            this.txtSpatialRefName.Location = new System.Drawing.Point(8, 0xb0);
            this.txtSpatialRefName.Name = "txtSpatialRefName";
            this.txtSpatialRefName.Size = new Size(160, 0x15);
            this.txtSpatialRefName.TabIndex = 0x15;
            this.cboPixelType.EditValue = "被管制的";
            this.cboPixelType.Location = new System.Drawing.Point(8, 0x120);
            this.cboPixelType.Name = "cboPixelType";
            this.cboPixelType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPixelType.Properties.Items.AddRange(new object[] { "被管制的", "非管制的" });
            this.cboPixelType.Size = new Size(0xc0, 0x15);
            this.cboPixelType.TabIndex = 0x16;
            this.cboPixelType.SelectedIndexChanged += new EventHandler(this.cboPixelType_SelectedIndexChanged);
            this.txtRasterSpatialRefName.EditValue = "";
            this.txtRasterSpatialRefName.Location = new System.Drawing.Point(8, 120);
            this.txtRasterSpatialRefName.Name = "txtRasterSpatialRefName";
            this.txtRasterSpatialRefName.Size = new Size(160, 0x15);
            this.txtRasterSpatialRefName.TabIndex = 0x19;
            this.btnSelectRasterSpatialRef.Image = (Image) manager.GetObject("btnSelectRasterSpatialRef.Image");
            this.btnSelectRasterSpatialRef.Location = new System.Drawing.Point(0xb0, 120);
            this.btnSelectRasterSpatialRef.Name = "btnSelectRasterSpatialRef";
            this.btnSelectRasterSpatialRef.Size = new Size(0x18, 0x18);
            this.btnSelectRasterSpatialRef.TabIndex = 0x18;
            this.btnSelectRasterSpatialRef.Click += new EventHandler(this.btnSelectRasterSpatialRef_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x65, 12);
            this.label3.TabIndex = 0x17;
            this.label3.Text = "栅格坐标系(可选)";
            this.cboConfigKey.EditValue = "";
            this.cboConfigKey.Location = new System.Drawing.Point(8, 0xe8);
            this.cboConfigKey.Name = "cboConfigKey";
            this.cboConfigKey.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboConfigKey.Size = new Size(0xc0, 0x15);
            this.cboConfigKey.TabIndex = 0x1a;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xd8, 0x15d);
            base.Controls.Add(this.cboConfigKey);
            base.Controls.Add(this.txtRasterSpatialRefName);
            base.Controls.Add(this.btnSelectRasterSpatialRef);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboPixelType);
            base.Controls.Add(this.txtSpatialRefName);
            base.Controls.Add(this.txtRasterDatastName);
            base.Controls.Add(this.txtLoaction);
            base.Controls.Add(this.btnSelectSpatialRef);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnSelectOutLocation);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCreateRasterFolder";
            this.Text = "创建栅格目录";
            base.Load += new EventHandler(this.frmCreateRasterFolder_Load);
            this.txtLoaction.Properties.EndInit();
            this.txtRasterDatastName.Properties.EndInit();
            this.txtSpatialRefName.Properties.EndInit();
            this.cboPixelType.Properties.EndInit();
            this.txtRasterSpatialRefName.Properties.EndInit();
            this.cboConfigKey.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private ISpatialReference method_0(IGeodatabaseRelease igeodatabaseRelease_0)
        {
            return SpatialReferenctOperator.ConstructCoordinateSystem(igeodatabaseRelease_0);
        }

        private void method_1(IGeodatabaseRelease igeodatabaseRelease_0, ISpatialReference ispatialReference_2)
        {
            SpatialReferenctOperator.ChangeCoordinateSystem(igeodatabaseRelease_0, ispatialReference_2, true);
        }

        private void method_2(IWorkspaceConfiguration iworkspaceConfiguration_0)
        {
            IEnumConfigurationKeyword configurationKeywords = iworkspaceConfiguration_0.ConfigurationKeywords;
            configurationKeywords.Reset();
            for (IConfigurationKeyword keyword2 = configurationKeywords.Next(); keyword2 != null; keyword2 = configurationKeywords.Next())
            {
                this.cboConfigKey.Properties.Items.Add(keyword2.Name);
            }
        }

        public IGxObject OutLocation
        {
            set
            {
                this.igxObject_0 = value;
            }
        }
    }
}

