using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class SelectLoadDataControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLoadDataControl));
            this.btnSelectInputFeatures = new SimpleButton();
            this.SourceDatalistBox = new ListBoxControl();
            this.label1 = new Label();
            this.btnDelete = new SimpleButton();
            this.label2 = new Label();
            this.txtInputFeatureClass = new TextEdit();
            this.btnAdd = new SimpleButton();
            ((ISupportInitialize) this.SourceDatalistBox).BeginInit();
            this.txtInputFeatureClass.Properties.BeginInit();
            base.SuspendLayout();
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(264, 32);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 3;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.SourceDatalistBox.ItemHeight = 15;
            this.SourceDatalistBox.Location = new Point(8, 88);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new Size(248, 136);
            this.SourceDatalistBox.TabIndex = 4;
            this.SourceDatalistBox.SelectedIndexChanged += new EventHandler(this.SourceDatalistBox_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "源数据列表";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(264, 120);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "输入数据";
            this.txtInputFeatureClass.EditValue = "";
            this.txtInputFeatureClass.Location = new Point(8, 32);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Properties.ReadOnly = true;
            this.txtInputFeatureClass.Size = new Size(248, 21);
            this.txtInputFeatureClass.TabIndex = 10;
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = (Image) resources.GetObject("btnAdd.Image");
            this.btnAdd.Location = new Point(264, 88);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(24, 24);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtInputFeatureClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.SourceDatalistBox);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Name = "SelectLoadDataControl";
            base.Size = new Size(304, 256);
            base.Load += new EventHandler(this.SelectLoadDataControl_Load);
            ((ISupportInitialize) this.SourceDatalistBox).EndInit();
            this.txtInputFeatureClass.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private Label label1;
        private Label label2;
        private ListBoxControl SourceDatalistBox;
        private TextEdit txtInputFeatureClass;
    }
}