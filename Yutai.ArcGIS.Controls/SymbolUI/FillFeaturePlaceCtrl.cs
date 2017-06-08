using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class FillFeaturePlaceCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private CheckEdit chkPlaceOnlyInsidePolygon;
        private Container components = null;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private RadioGroup rdoPolygonPlacementMethod;

        public event OnValueChangeEventHandler OnValueChange;

        public FillFeaturePlaceCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_OverLayerProperty.PolygonPlacementMethod = (esriOverposterPolygonPlacementMethod) this.rdoPolygonPlacementMethod.SelectedIndex;
                this.m_OverLayerProperty.PlaceOnlyInsidePolygon = this.chkPlaceOnlyInsidePolygon.Checked;
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void chkPlaceOnlyInsidePolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillFeaturePlaceCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                this.rdoPolygonPlacementMethod.SelectedIndex = (int) this.m_OverLayerProperty.PolygonPlacementMethod;
                this.chkPlaceOnlyInsidePolygon.Checked = this.m_OverLayerProperty.PlaceOnlyInsidePolygon;
                this.rdoPolygonPlacementMethod_SelectedIndexChanged(this, e);
                this.m_CanDo = true;
            }
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FillFeaturePlaceCtrl));
            this.rdoPolygonPlacementMethod = new RadioGroup();
            this.chkPlaceOnlyInsidePolygon = new CheckEdit();
            this.pictureEdit1 = new PictureEdit();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit3 = new PictureEdit();
            this.rdoPolygonPlacementMethod.Properties.BeginInit();
            this.chkPlaceOnlyInsidePolygon.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit3.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoPolygonPlacementMethod.Location = new Point(0x10, 0x10);
            this.rdoPolygonPlacementMethod.Name = "rdoPolygonPlacementMethod";
            this.rdoPolygonPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPolygonPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPolygonPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPolygonPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "总是水平"), new RadioGroupItem(null, "总是垂直"), new RadioGroupItem(null, "尽量先水平，然后在垂直") });
            this.rdoPolygonPlacementMethod.Size = new Size(0xa8, 80);
            this.rdoPolygonPlacementMethod.TabIndex = 0;
            this.rdoPolygonPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPolygonPlacementMethod_SelectedIndexChanged);
            this.chkPlaceOnlyInsidePolygon.Location = new Point(0x18, 0xb0);
            this.chkPlaceOnlyInsidePolygon.Name = "chkPlaceOnlyInsidePolygon";
            this.chkPlaceOnlyInsidePolygon.Properties.Caption = "只将标注放在多边形内部";
            this.chkPlaceOnlyInsidePolygon.Size = new Size(0xa8, 0x13);
            this.chkPlaceOnlyInsidePolygon.TabIndex = 1;
            this.chkPlaceOnlyInsidePolygon.CheckedChanged += new EventHandler(this.chkPlaceOnlyInsidePolygon_CheckedChanged);
            this.pictureEdit1.EditValue = (object)resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0xb8, 8);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0x4a, 0x51);
            this.pictureEdit1.TabIndex = 11;
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.EditValue = (object)resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(0xb8, 8);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0x4a, 0x51);
            this.pictureEdit2.TabIndex = 12;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.EditValue = (object)resources.GetObject("pictureEdit3.EditValue");
            this.pictureEdit3.Location = new Point(0xb8, 8);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit3.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit3.Size = new Size(0x4a, 0x51);
            this.pictureEdit3.TabIndex = 13;
            this.pictureEdit3.Visible = false;
            base.Controls.Add(this.pictureEdit3);
            base.Controls.Add(this.pictureEdit2);
            base.Controls.Add(this.pictureEdit1);
            base.Controls.Add(this.chkPlaceOnlyInsidePolygon);
            base.Controls.Add(this.rdoPolygonPlacementMethod);
            base.Name = "FillFeaturePlaceCtrl";
            base.Size = new Size(280, 0xe8);
            base.Load += new EventHandler(this.FillFeaturePlaceCtrl_Load);
            this.rdoPolygonPlacementMethod.Properties.EndInit();
            this.chkPlaceOnlyInsidePolygon.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit3.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void rdoPolygonPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPolygonPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.pictureEdit1.Visible = true;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = false;
                    break;

                case 1:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = true;
                    this.pictureEdit3.Visible = false;
                    break;

                case 2:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = true;
                    break;
            }
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void SetObjects(object @object)
        {
            this.m_OverLayerProperty = @object as IBasicOverposterLayerProperties4;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return "";
            }
            set
            {
            }
        }
    }
}

