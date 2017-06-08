using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class LabelPlacementPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private ComboBoxEdit cboFeatureType;
        private Container components = null;
        private FillFeaturePlaceCtrl fillFeaturePlaceCtrl1;
        private GroupBox groupBox4;
        private GroupBox groupBoxFill;
        private GroupBox groupBoxLine;
        private GroupBox groupBoxPoint;
        private Label label1;
        private LineFeaturePlaceSetCtrl lineFeaturePlaceSetCtrl1;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_pOverposterLayerProperties = null;
        private PointFeatureLabelCtrl pointFeatureLabelCtrl1;
        private RadioGroup radioGroup1;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelPlacementPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.pointFeatureLabelCtrl1.Apply();
                this.lineFeaturePlaceSetCtrl1.Apply();
                this.fillFeaturePlaceCtrl1.Apply();
                this.m_pOverposterLayerProperties.FeatureType = (esriBasicOverposterFeatureType) this.cboFeatureType.SelectedIndex;
                this.m_pOverposterLayerProperties.NumLabelsOption = (esriBasicNumLabelsOption) (this.radioGroup1.SelectedIndex + 1);
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cboFeatureType.SelectedIndex)
            {
                case 0:
                    this.groupBox4.Enabled = false;
                    this.groupBoxPoint.Visible = true;
                    this.groupBoxLine.Visible = false;
                    this.groupBoxFill.Visible = false;
                    break;

                case 1:
                    this.groupBox4.Enabled = true;
                    this.groupBoxPoint.Visible = false;
                    this.groupBoxLine.Visible = true;
                    this.groupBoxFill.Visible = false;
                    break;

                case 2:
                    this.groupBox4.Enabled = true;
                    this.groupBoxPoint.Visible = false;
                    this.groupBoxLine.Visible = false;
                    this.groupBoxFill.Visible = true;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBoxPoint = new GroupBox();
            this.pointFeatureLabelCtrl1 = new PointFeatureLabelCtrl();
            this.groupBoxLine = new GroupBox();
            this.lineFeaturePlaceSetCtrl1 = new LineFeaturePlaceSetCtrl();
            this.groupBoxFill = new GroupBox();
            this.fillFeaturePlaceCtrl1 = new FillFeaturePlaceCtrl();
            this.groupBox4 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.cboFeatureType = new ComboBoxEdit();
            this.groupBoxPoint.SuspendLayout();
            this.groupBoxLine.SuspendLayout();
            this.groupBoxFill.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBoxPoint.Controls.Add(this.pointFeatureLabelCtrl1);
            this.groupBoxPoint.Location = new Point(8, 8);
            this.groupBoxPoint.Name = "groupBoxPoint";
            this.groupBoxPoint.Size = new Size(0x158, 0x110);
            this.groupBoxPoint.TabIndex = 0;
            this.groupBoxPoint.TabStop = false;
            this.groupBoxPoint.Text = "点设置";
            this.pointFeatureLabelCtrl1.Location = new Point(8, 0x10);
            this.pointFeatureLabelCtrl1.Name = "pointFeatureLabelCtrl1";
            this.pointFeatureLabelCtrl1.Size = new Size(0xe8, 0xf8);
            this.pointFeatureLabelCtrl1.TabIndex = 0;
            this.pointFeatureLabelCtrl1.Title = null;
            this.groupBoxLine.Controls.Add(this.lineFeaturePlaceSetCtrl1);
            this.groupBoxLine.Location = new Point(8, 8);
            this.groupBoxLine.Name = "groupBoxLine";
            this.groupBoxLine.Size = new Size(0x158, 0x110);
            this.groupBoxLine.TabIndex = 1;
            this.groupBoxLine.TabStop = false;
            this.groupBoxLine.Text = "线设置";
            this.lineFeaturePlaceSetCtrl1.Location = new Point(8, 0x10);
            this.lineFeaturePlaceSetCtrl1.Name = "lineFeaturePlaceSetCtrl1";
            this.lineFeaturePlaceSetCtrl1.Size = new Size(0x148, 0xe8);
            this.lineFeaturePlaceSetCtrl1.TabIndex = 0;
            this.groupBoxFill.Controls.Add(this.fillFeaturePlaceCtrl1);
            this.groupBoxFill.Location = new Point(8, 8);
            this.groupBoxFill.Name = "groupBoxFill";
            this.groupBoxFill.Size = new Size(0x158, 0x110);
            this.groupBoxFill.TabIndex = 2;
            this.groupBoxFill.TabStop = false;
            this.groupBoxFill.Text = "多边形设置";
            this.fillFeaturePlaceCtrl1.Location = new Point(8, 0x10);
            this.fillFeaturePlaceCtrl1.Name = "fillFeaturePlaceCtrl1";
            this.fillFeaturePlaceCtrl1.Size = new Size(0x130, 0xe0);
            this.fillFeaturePlaceCtrl1.TabIndex = 0;
            this.groupBox4.Controls.Add(this.radioGroup1);
            this.groupBox4.Location = new Point(8, 0x120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(240, 0x70);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "重复标注";
            this.radioGroup1.Location = new Point(8, 0x18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "删除重复标注"), new RadioGroupItem(null, "每个要素放置一个标注"), new RadioGroupItem(null, "每个要素的局部防置一个标注") });
            this.radioGroup1.Size = new Size(200, 80);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x100, 0x120);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 4;
            this.label1.Text = "要素类型";
            this.cboFeatureType.EditValue = "线";
            this.cboFeatureType.Location = new Point(0x100, 0x130);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "点", "线", "面" });
            this.cboFeatureType.Size = new Size(0x68, 0x17);
            this.cboFeatureType.TabIndex = 5;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            base.Controls.Add(this.cboFeatureType);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBoxFill);
            base.Controls.Add(this.groupBoxLine);
            base.Controls.Add(this.groupBoxPoint);
            base.Name = "LabelPlacementPropertyPage";
            base.Size = new Size(0x170, 0x198);
            base.Load += new EventHandler(this.LabelPlacementPropertyPage_Load);
            this.groupBoxPoint.ResumeLayout(false);
            this.groupBoxLine.ResumeLayout(false);
            this.groupBoxFill.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LabelPlacementPropertyPage_Load(object sender, EventArgs e)
        {
            this.pointFeatureLabelCtrl1.OnValueChange += new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.lineFeaturePlaceSetCtrl1.OnValueChange += new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.fillFeaturePlaceCtrl1.OnValueChange += new OnValueChangeEventHandler(this.pointFeatureLabelCtrl1_OnValueChange);
            this.cboFeatureType.SelectedIndex = (int) this.m_pOverposterLayerProperties.FeatureType;
            this.radioGroup1.SelectedIndex = ((int) this.m_pOverposterLayerProperties.NumLabelsOption) - 1;
            this.cboFeatureType_SelectedIndexChanged(this, e);
            this.m_CanDo = true;
        }

        private void pointFeatureLabelCtrl1_OnValueChange()
        {
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
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

        public void SetObjects(object @object)
        {
            this.m_pOverposterLayerProperties = @object as IBasicOverposterLayerProperties4;
            this.pointFeatureLabelCtrl1.SetObjects(@object);
            this.lineFeaturePlaceSetCtrl1.SetObjects(@object);
            this.fillFeaturePlaceCtrl1.SetObjects(@object);
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
                return "放置";
            }
            set
            {
            }
        }
    }
}

