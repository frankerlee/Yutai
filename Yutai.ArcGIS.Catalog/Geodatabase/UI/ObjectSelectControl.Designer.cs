using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ObjectSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectSelectControl));
            this.lblSelectObjects = new Label();
            this.textEditInputFeatures = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.lblOutFeatureClassName = new Label();
            this.txtOutFeatureClassName = new TextEdit();
            this.label1 = new Label();
            this.txtWhere = new TextEdit();
            this.btnQueryDef = new SimpleButton();
            this.panel1 = new Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new Label();
            this.textEditInputFeatures.Properties.BeginInit();
            this.txtOutLocation.Properties.BeginInit();
            this.txtOutFeatureClassName.Properties.BeginInit();
            this.txtWhere.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(56, 16);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "输入要素类";
            this.textEditInputFeatures.EditValue = "";
            this.textEditInputFeatures.Location = new Point(8, 24);
            this.textEditInputFeatures.Name = "textEditInputFeatures";
            this.textEditInputFeatures.Properties.ReadOnly = true;
            this.textEditInputFeatures.Size = new Size(256, 21);
            this.textEditInputFeatures.TabIndex = 1;
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(272, 24);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 2;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(8, 72);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(248, 21);
            this.txtOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Image = (Image) resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(272, 72);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(24, 24);
            this.btnSelectOutLocation.TabIndex = 4;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.lblOutFeatureClassName.AutoSize = true;
            this.lblOutFeatureClassName.Location = new Point(8, 104);
            this.lblOutFeatureClassName.Name = "lblOutFeatureClassName";
            this.lblOutFeatureClassName.Size = new Size(77, 12);
            this.lblOutFeatureClassName.TabIndex = 6;
            this.lblOutFeatureClassName.Text = "输出要素类名";
            this.txtOutFeatureClassName.EditValue = "";
            this.txtOutFeatureClassName.Location = new Point(8, 120);
            this.txtOutFeatureClassName.Name = "txtOutFeatureClassName";
            this.txtOutFeatureClassName.Size = new Size(248, 21);
            this.txtOutFeatureClassName.TabIndex = 7;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 152);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "表达式(可选)";
            this.txtWhere.EditValue = "";
            this.txtWhere.Location = new Point(8, 168);
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.Size = new Size(248, 21);
            this.txtWhere.TabIndex = 9;
            this.btnQueryDef.Image = (Image) resources.GetObject("btnQueryDef.Image");
            this.btnQueryDef.Location = new Point(264, 168);
            this.btnQueryDef.Name = "btnQueryDef";
            this.btnQueryDef.Size = new Size(24, 24);
            this.btnQueryDef.TabIndex = 10;
            this.btnQueryDef.Click += new EventHandler(this.btnQueryDef_Click);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(288, 192);
            this.panel1.TabIndex = 11;
            this.panel1.Visible = false;
            this.progressBar1.Location = new Point(16, 48);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(248, 24);
            this.progressBar1.TabIndex = 1;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(16, 16);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(0, 12);
            this.lblInfo.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnQueryDef);
            base.Controls.Add(this.txtWhere);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtOutFeatureClassName);
            base.Controls.Add(this.lblOutFeatureClassName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.textEditInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Name = "ObjectSelectControl";
            base.Size = new Size(304, 208);
            base.Load += new EventHandler(this.ObjectSelectControl_Load);
            this.textEditInputFeatures.Properties.EndInit();
            this.txtOutLocation.Properties.EndInit();
            this.txtOutFeatureClassName.Properties.EndInit();
            this.txtWhere.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnQueryDef;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private esriDatasetType esriDatasetType_0;
        private Label label1;
        private Label label2;
        private Label lblInfo;
        private Label lblOutFeatureClassName;
        private Label lblSelectObjects;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private TextEdit textEditInputFeatures;
        private TextEdit txtOutFeatureClassName;
        private TextEdit txtOutLocation;
        private TextEdit txtWhere;
    }
}