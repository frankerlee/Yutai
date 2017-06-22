using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class PicturePropertyPage
    {
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
            this.chkSavePictureInDocument = new CheckEdit();
            this.label1 = new Label();
            this.txtDescription = new MemoEdit();
            this.chkSavePictureInDocument.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.chkSavePictureInDocument.EditValue = false;
            this.chkSavePictureInDocument.Location = new Point(16, 16);
            this.chkSavePictureInDocument.Name = "chkSavePictureInDocument";
            this.chkSavePictureInDocument.Properties.Caption = "图片存为文件的一部份";
            this.chkSavePictureInDocument.Size = new Size(168, 19);
            this.chkSavePictureInDocument.TabIndex = 0;
            this.chkSavePictureInDocument.CheckedChanged += new EventHandler(this.chkSavePictureInDocument_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "说明";
            this.txtDescription.EditValue = "memoEdit1";
            this.txtDescription.Location = new Point(16, 72);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new Size(184, 112);
            this.txtDescription.TabIndex = 2;
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.chkSavePictureInDocument);
            base.Name = "PicturePropertyPage";
            base.Size = new Size(240, 216);
            base.Load += new EventHandler(this.PicturePropertyPage_Load);
            this.chkSavePictureInDocument.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private CheckEdit chkSavePictureInDocument;
        private Label label1;
        private MemoEdit txtDescription;
    }
}