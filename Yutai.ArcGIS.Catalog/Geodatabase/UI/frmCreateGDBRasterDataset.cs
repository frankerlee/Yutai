using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Raster;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class frmCreateGDBRasterDataset : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectOutLocation;
        private SimpleButton btnSelectSpatialRef;
        private ComboBoxEdit cboPixelType;
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private ISpatialReference ispatialReference_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextEdit txtLoaction;
        private TextEdit txtPiexlSize;
        private TextEdit txtRasterBand;
        private TextEdit txtRasterDatastName;
        private TextEdit txtSpatialRefName;

        public frmCreateGDBRasterDataset()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtRasterDatastName.Text.Trim().Length == 0)
            {
                MessageBox.Show("新建栅格数据集名字不能为空!");
            }
            else
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                try
                {
                    if (RasterUtility.CreateSDERasterDs(ex, this.txtRasterDatastName.Text, int.Parse(this.txtRasterBand.Text), (rstPixelType) this.cboPixelType.SelectedIndex, this.ispatialReference_0, null, null, "") != null)
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                }
                catch (COMException exception)
                {
                    int errorCode = exception.ErrorCode;
                    MessageBox.Show(exception.Message);
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
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_0(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    this.txtLoaction.Text = this.igxObject_0.FullName;
                }
            }
        }

        private void btnSelectSpatialRef_Click(object sender, EventArgs e)
        {
            IRasterWorkspaceEx ex;
            ISpatialReference reference2;
            frmSpatialReference reference = new frmSpatialReference();
            if (this.ispatialReference_0 == null)
            {
                ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                reference2 = this.method_1(ex as IGeodatabaseRelease);
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
                if (this.igxObject_0 != null)
                {
                    ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_0(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    ComReleaser.ReleaseCOMObject(ex);
                }
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

        private void frmCreateGDBRasterDataset_Load(object sender, EventArgs e)
        {
            if (this.igxObject_0 != null)
            {
                this.txtLoaction.Text = this.igxObject_0.FullName;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateGDBRasterDataset));
            this.label1 = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtRasterBand = new TextEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnSelectSpatialRef = new SimpleButton();
            this.label6 = new Label();
            this.txtLoaction = new TextEdit();
            this.txtRasterDatastName = new TextEdit();
            this.txtPiexlSize = new TextEdit();
            this.txtSpatialRefName = new TextEdit();
            this.cboPixelType = new ComboBoxEdit();
            this.txtRasterBand.Properties.BeginInit();
            this.txtLoaction.Properties.BeginInit();
            this.txtRasterDatastName.Properties.BeginInit();
            this.txtPiexlSize.Properties.BeginInit();
            this.txtSpatialRefName.Properties.BeginInit();
            this.cboPixelType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出位置";
            this.btnSelectOutLocation.Image = (Image)resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(0xb0, 0x18);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "栅格数据集名称";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "像素大小(可选)";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 0x8f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "像素类型(可选)";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0xf2);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "波段个数";
            this.txtRasterBand.EditValue = "1";
            this.txtRasterBand.Location = new System.Drawing.Point(8, 0x108);
            this.txtRasterBand.Name = "txtRasterBand";
            this.txtRasterBand.Size = new Size(0xc0, 0x15);
            this.txtRasterBand.TabIndex = 11;
            this.btnOK.Location = new System.Drawing.Point(0x58, 0x128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x90, 0x128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnSelectSpatialRef.Image = (Image)resources.GetObject("btnSelectSpatialRef.Image");
            this.btnSelectSpatialRef.Location = new System.Drawing.Point(0xb0, 210);
            this.btnSelectSpatialRef.Name = "btnSelectSpatialRef";
            this.btnSelectSpatialRef.Size = new Size(0x18, 0x18);
            this.btnSelectSpatialRef.TabIndex = 0x10;
            this.btnSelectSpatialRef.Click += new EventHandler(this.btnSelectSpatialRef_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 0xbb);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "栅格坐标系(可选)";
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
            this.txtPiexlSize.EditValue = "";
            this.txtPiexlSize.Location = new System.Drawing.Point(8, 0x70);
            this.txtPiexlSize.Name = "txtPiexlSize";
            this.txtPiexlSize.Size = new Size(0xc0, 0x15);
            this.txtPiexlSize.TabIndex = 0x13;
            this.txtSpatialRefName.EditValue = "";
            this.txtSpatialRefName.Location = new System.Drawing.Point(8, 210);
            this.txtSpatialRefName.Name = "txtSpatialRefName";
            this.txtSpatialRefName.Size = new Size(160, 0x15);
            this.txtSpatialRefName.TabIndex = 0x15;
            this.cboPixelType.EditValue = "8 bit unsigned";
            this.cboPixelType.Location = new System.Drawing.Point(12, 0x9e);
            this.cboPixelType.Name = "cboPixelType";
            this.cboPixelType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPixelType.Properties.Items.AddRange(new object[] { "1 bit", "2 bit", "4 bit", "8 bit unsigned", "8 bit signed", "16 bit unsigned", "16 bit signed", "32 bit unsigned", "32 bit signed", "32 bit float", "64 bit" });
            this.cboPixelType.Size = new Size(0xc0, 0x15);
            this.cboPixelType.TabIndex = 0x16;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xd8, 0x145);
            base.Controls.Add(this.cboPixelType);
            base.Controls.Add(this.txtSpatialRefName);
            base.Controls.Add(this.txtPiexlSize);
            base.Controls.Add(this.txtRasterDatastName);
            base.Controls.Add(this.txtLoaction);
            base.Controls.Add(this.btnSelectSpatialRef);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtRasterBand);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.label1);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmCreateGDBRasterDataset";
            this.Text = "创建栅格数据集";
            base.Load += new EventHandler(this.frmCreateGDBRasterDataset_Load);
            this.txtRasterBand.Properties.EndInit();
            this.txtLoaction.Properties.EndInit();
            this.txtRasterDatastName.Properties.EndInit();
            this.txtPiexlSize.Properties.EndInit();
            this.txtSpatialRefName.Properties.EndInit();
            this.cboPixelType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(IGeodatabaseRelease igeodatabaseRelease_0, ISpatialReference ispatialReference_1)
        {
            SpatialReferenctOperator.ChangeCoordinateSystem(igeodatabaseRelease_0, ispatialReference_1, true);
        }

        private ISpatialReference method_1(IGeodatabaseRelease igeodatabaseRelease_0)
        {
            return SpatialReferenctOperator.ConstructCoordinateSystem(igeodatabaseRelease_0);
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

