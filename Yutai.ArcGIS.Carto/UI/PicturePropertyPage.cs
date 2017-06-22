using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class PicturePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IPictureElement2 ipictureElement2_0 = null;
        private string string_0 = "图片";

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

