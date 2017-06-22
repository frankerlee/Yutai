using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmLabelToAnnoSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelToAnnoSetup));
            this.groupBox1 = new GroupBox();
            this.rdoSaveType = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.lblRefrencesScale = new Label();
            this.groupBox3 = new GroupBox();
            this.radioGroup2 = new RadioGroup();
            this.label1 = new Label();
            this.txtFeatLayer = new TextEdit();
            this.chkFeatureLinked = new CheckEdit();
            this.txtAnnoName = new TextEdit();
            this.label2 = new Label();
            this.txtSavePos = new TextEdit();
            this.label3 = new Label();
            this.btnConvert = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.rdoSaveType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.radioGroup2.Properties.BeginInit();
            this.txtFeatLayer.Properties.BeginInit();
            this.chkFeatureLinked.Properties.BeginInit();
            this.txtAnnoName.Properties.BeginInit();
            this.txtSavePos.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoSaveType);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(192, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "保存注记";
            this.rdoSaveType.Location = new System.Drawing.Point(8, 24);
            this.rdoSaveType.Name = "rdoSaveType";
            this.rdoSaveType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoSaveType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoSaveType.Properties.BorderStyle = BorderStyles.Office2003;
            this.rdoSaveType.Properties.Columns = 2;
            this.rdoSaveType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在数据库中"), new RadioGroupItem(null, "在地图中") });
            this.rdoSaveType.Size = new Size(176, 24);
            this.rdoSaveType.TabIndex = 0;
            this.groupBox2.Controls.Add(this.lblRefrencesScale);
            this.groupBox2.Location = new System.Drawing.Point(224, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(192, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参考比例";
            this.lblRefrencesScale.Location = new System.Drawing.Point(8, 16);
            this.lblRefrencesScale.Name = "lblRefrencesScale";
            this.lblRefrencesScale.Size = new Size(160, 24);
            this.lblRefrencesScale.TabIndex = 0;
            this.groupBox3.Controls.Add(this.radioGroup2);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(8, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(408, 64);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "创建注记要素";
            this.radioGroup2.Location = new System.Drawing.Point(8, 24);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = BorderStyles.Office2003;
            this.radioGroup2.Properties.Columns = 3;
            this.radioGroup2.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "所有要素"), new RadioGroupItem(null, "当前范围内的要素"), new RadioGroupItem(null, "选中的要素") });
            this.radioGroup2.Size = new Size(384, 24);
            this.radioGroup2.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "要 素 图 层";
            this.txtFeatLayer.EditValue = "";
            this.txtFeatLayer.Location = new System.Drawing.Point(96, 152);
            this.txtFeatLayer.Name = "txtFeatLayer";
            this.txtFeatLayer.Properties.ReadOnly = true;
            this.txtFeatLayer.Size = new Size(192, 21);
            this.txtFeatLayer.TabIndex = 4;
            this.chkFeatureLinked.Location = new System.Drawing.Point(320, 152);
            this.chkFeatureLinked.Name = "chkFeatureLinked";
            this.chkFeatureLinked.Properties.Caption = "与要素关联";
            this.chkFeatureLinked.Size = new Size(96, 19);
            this.chkFeatureLinked.TabIndex = 5;
            this.txtAnnoName.EditValue = "";
            this.txtAnnoName.Location = new System.Drawing.Point(96, 184);
            this.txtAnnoName.Name = "txtAnnoName";
            this.txtAnnoName.Size = new Size(192, 21);
            this.txtAnnoName.TabIndex = 7;
            this.txtAnnoName.TextChanged += new EventHandler(this.txtAnnoName_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 184);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "注记要素类名";
            this.txtSavePos.EditValue = "";
            this.txtSavePos.Location = new System.Drawing.Point(96, 216);
            this.txtSavePos.Name = "txtSavePos";
            this.txtSavePos.Properties.ReadOnly = true;
            this.txtSavePos.Size = new Size(192, 21);
            this.txtSavePos.TabIndex = 9;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 224);
            this.label3.Name = "label3";
            this.label3.Size = new Size(71, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "保 存 位 置";
            this.btnConvert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConvert.Location = new System.Drawing.Point(216, 248);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(64, 24);
            this.btnConvert.TabIndex = 10;
            this.btnConvert.Text = "转换";
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(434, 284);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtSavePos);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtAnnoName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkFeatureLinked);
            base.Controls.Add(this.txtFeatLayer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLabelToAnnoSetup";
            this.Text = "标注转换为注记";
            base.Load += new EventHandler(this.frmLabelToAnnoSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.rdoSaveType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.radioGroup2.Properties.EndInit();
            this.txtFeatLayer.Properties.EndInit();
            this.chkFeatureLinked.Properties.EndInit();
            this.txtAnnoName.Properties.EndInit();
            this.txtSavePos.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnConvert;
        private CheckEdit chkFeatureLinked;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblRefrencesScale;
        private RadioGroup radioGroup2;
        private RadioGroup rdoSaveType;
        private TextEdit txtAnnoName;
        private TextEdit txtFeatLayer;
        private TextEdit txtSavePos;
    }
}