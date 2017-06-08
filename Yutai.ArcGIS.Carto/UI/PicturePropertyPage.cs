using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public class PicturePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private CheckEdit chkSavePictureInDocument;
        private Container container_0 = null;
        private IPictureElement2 ipictureElement2_0 = null;
        private Label label1;
        private string string_0 = "图片";
        private MemoEdit txtDescription;

        public event OnValueChangeEventHandler OnValueChange;

        public PicturePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ipictureElement2_0.SavePictureInDocument = this.chkSavePictureInDocument.Checked;
            }
        }

        public void Cancel()
        {
        }

        private void chkSavePictureInDocument_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
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
            this.chkSavePictureInDocument = new CheckEdit();
            this.label1 = new Label();
            this.txtDescription = new MemoEdit();
            this.chkSavePictureInDocument.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.chkSavePictureInDocument.EditValue = false;
            this.chkSavePictureInDocument.Location = new Point(0x10, 0x10);
            this.chkSavePictureInDocument.Name = "chkSavePictureInDocument";
            this.chkSavePictureInDocument.Properties.Caption = "图片存为文件的一部份";
            this.chkSavePictureInDocument.Size = new Size(0xa8, 0x13);
            this.chkSavePictureInDocument.TabIndex = 0;
            this.chkSavePictureInDocument.CheckedChanged += new EventHandler(this.chkSavePictureInDocument_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "说明";
            this.txtDescription.EditValue = "memoEdit1";
            this.txtDescription.Location = new Point(0x10, 0x48);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new Size(0xb8, 0x70);
            this.txtDescription.TabIndex = 2;
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.chkSavePictureInDocument);
            base.Name = "PicturePropertyPage";
            base.Size = new Size(240, 0xd8);
            base.Load += new EventHandler(this.PicturePropertyPage_Load);
            this.chkSavePictureInDocument.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (this.ipictureElement2_0 != null)
            {
                this.chkSavePictureInDocument.Checked = this.ipictureElement2_0.SavePictureInDocument;
                this.txtDescription.Text = this.ipictureElement2_0.PictureDescription;
            }
        }

        private void PicturePropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.ipictureElement2_0 = object_0 as IPictureElement2;
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

