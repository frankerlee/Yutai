using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Raster;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCreateRasterFolder
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateRasterFolder));
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
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出位置";
            this.btnSelectOutLocation.Image = (Image)resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(176, 24);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(24, 24);
            this.btnSelectOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new Size(101, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "栅格数据目录名称";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 272);
            this.label4.Name = "label4";
            this.label4.Size = new Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "栅格管理类型";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 208);
            this.label5.Name = "label5";
            this.label5.Size = new Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "配置关键值";
            this.btnOK.Location = new System.Drawing.Point(96, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(48, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(152, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(48, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnSelectSpatialRef.Image = (Image)resources.GetObject("btnSelectSpatialRef.Image");
            this.btnSelectSpatialRef.Location = new System.Drawing.Point(176, 176);
            this.btnSelectSpatialRef.Name = "btnSelectSpatialRef";
            this.btnSelectSpatialRef.Size = new Size(24, 24);
            this.btnSelectSpatialRef.TabIndex = 16;
            this.btnSelectSpatialRef.Click += new EventHandler(this.btnSelectSpatialRef_Click);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 152);
            this.label6.Name = "label6";
            this.label6.Size = new Size(101, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "几何坐标系(可选)";
            this.txtLoaction.EditValue = "";
            this.txtLoaction.Location = new System.Drawing.Point(8, 24);
            this.txtLoaction.Name = "txtLoaction";
            this.txtLoaction.Size = new Size(160, 21);
            this.txtLoaction.TabIndex = 17;
            this.txtRasterDatastName.EditValue = "";
            this.txtRasterDatastName.Location = new System.Drawing.Point(8, 72);
            this.txtRasterDatastName.Name = "txtRasterDatastName";
            this.txtRasterDatastName.Size = new Size(192, 21);
            this.txtRasterDatastName.TabIndex = 18;
            this.txtSpatialRefName.EditValue = "";
            this.txtSpatialRefName.Location = new System.Drawing.Point(8, 176);
            this.txtSpatialRefName.Name = "txtSpatialRefName";
            this.txtSpatialRefName.Size = new Size(160, 21);
            this.txtSpatialRefName.TabIndex = 21;
            this.cboPixelType.EditValue = "被管制的";
            this.cboPixelType.Location = new System.Drawing.Point(8, 288);
            this.cboPixelType.Name = "cboPixelType";
            this.cboPixelType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPixelType.Properties.Items.AddRange(new object[] { "被管制的", "非管制的" });
            this.cboPixelType.Size = new Size(192, 21);
            this.cboPixelType.TabIndex = 22;
            this.cboPixelType.SelectedIndexChanged += new EventHandler(this.cboPixelType_SelectedIndexChanged);
            this.txtRasterSpatialRefName.EditValue = "";
            this.txtRasterSpatialRefName.Location = new System.Drawing.Point(8, 120);
            this.txtRasterSpatialRefName.Name = "txtRasterSpatialRefName";
            this.txtRasterSpatialRefName.Size = new Size(160, 21);
            this.txtRasterSpatialRefName.TabIndex = 25;
            this.btnSelectRasterSpatialRef.Image = (Image)resources.GetObject("btnSelectRasterSpatialRef.Image");
            this.btnSelectRasterSpatialRef.Location = new System.Drawing.Point(176, 120);
            this.btnSelectRasterSpatialRef.Name = "btnSelectRasterSpatialRef";
            this.btnSelectRasterSpatialRef.Size = new Size(24, 24);
            this.btnSelectRasterSpatialRef.TabIndex = 24;
            this.btnSelectRasterSpatialRef.Click += new EventHandler(this.btnSelectRasterSpatialRef_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 104);
            this.label3.Name = "label3";
            this.label3.Size = new Size(101, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "栅格坐标系(可选)";
            this.cboConfigKey.EditValue = "";
            this.cboConfigKey.Location = new System.Drawing.Point(8, 232);
            this.cboConfigKey.Name = "cboConfigKey";
            this.cboConfigKey.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboConfigKey.Size = new Size(192, 21);
            this.cboConfigKey.TabIndex = 26;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(216, 349);
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
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
           
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

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectOutLocation;
        private SimpleButton btnSelectRasterSpatialRef;
        private SimpleButton btnSelectSpatialRef;
        private ComboBoxEdit cboConfigKey;
        private ComboBoxEdit cboPixelType;
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
    }
}