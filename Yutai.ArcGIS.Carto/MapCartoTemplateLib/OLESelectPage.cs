using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class OLESelectPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public OLESelectPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
        }

        private void btnSelectPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "所有支持格式|*.bmp;*.jpg;*.gif;*.emf;*.tif;*.png|位图文件|*.bmp|JPEG|*.jpg|TIFF|*.tif|EMF|*.emf|PNG|*.png|GIF|*.gif"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.FileName;
            }
        }

        public void Cancel()
        {
        }

 public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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
                return "图片";
            }
            set
            {
            }
        }
    }
}

