using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class SelectedDataLoaderCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedDataLoaderCtrl));
            this.btnAdd = new SimpleButton();
            this.txtInputFeatureClass = new TextEdit();
            this.label2 = new Label();
            this.btnDelete = new SimpleButton();
            this.label1 = new Label();
            this.SourceDatalistBox = new ListBoxControl();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtInputFeatureClass.Properties.BeginInit();
            ((ISupportInitialize) this.SourceDatalistBox).BeginInit();
            base.SuspendLayout();
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = (Image) resources.GetObject("btnAdd.Image");
            this.btnAdd.Location = new Point(269, 88);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(24, 24);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.txtInputFeatureClass.EditValue = "";
            this.txtInputFeatureClass.Location = new Point(13, 32);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Properties.ReadOnly = true;
            this.txtInputFeatureClass.Size = new Size(248, 21);
            this.txtInputFeatureClass.TabIndex = 17;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "输入数据";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(269, 120);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 15;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 64);
            this.label1.Name = "label1";
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "源数据列表";
            this.SourceDatalistBox.ItemHeight = 15;
            this.SourceDatalistBox.Location = new Point(13, 88);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new Size(248, 136);
            this.SourceDatalistBox.TabIndex = 13;
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(269, 32);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 12;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtInputFeatureClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.SourceDatalistBox);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Name = "SelectedDataLoaderCtrl";
            base.Size = new Size(315, 249);
            this.txtInputFeatureClass.Properties.EndInit();
            ((ISupportInitialize) this.SourceDatalistBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private IDataset idataset_0;
        private Label label1;
        private Label label2;
        private ListBoxControl SourceDatalistBox;
        private TextEdit txtInputFeatureClass;
    }
}