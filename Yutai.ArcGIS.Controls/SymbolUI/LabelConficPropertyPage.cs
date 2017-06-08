using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class LabelConficPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private ComboBoxEdit cboFeatureType;
        private ComboBoxEdit cboLabelWeight;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties m_pOverposterLayerProperties = null;
        private TextEdit txtBufferRatio;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelConficPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pOverposterLayerProperties.FeatureWeight = (esriBasicOverposterWeight) this.cboFeatureType.SelectedIndex;
                this.m_pOverposterLayerProperties.LabelWeight = (esriBasicOverposterWeight) (this.cboLabelWeight.SelectedIndex - 1);
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtBufferRatio.Text);
                    if ((num >= 0.0) && (num <= 1.0))
                    {
                        this.m_pOverposterLayerProperties.BufferRatio = num;
                    }
                }
                catch
                {
                }
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboLabelWeight_SelectedIndexChanged(object sender, EventArgs e)
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

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.label1 = new Label();
            this.cboLabelWeight = new ComboBoxEdit();
            this.cboFeatureType = new ComboBoxEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtBufferRatio = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cboLabelWeight.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            this.txtBufferRatio.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboLabelWeight);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 0x60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注权值";
            this.groupBox2.Controls.Add(this.cboFeatureType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(8, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x120, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "要素权值";
            this.groupBox3.Controls.Add(this.txtBufferRatio);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new Point(8, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x120, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "缓冲";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "标注权值:";
            this.cboLabelWeight.EditValue = "高";
            this.cboLabelWeight.Location = new Point(0x60, 0x18);
            this.cboLabelWeight.Name = "cboLabelWeight";
            this.cboLabelWeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelWeight.Properties.Items.AddRange(new object[] { "低", "中", "高" });
            this.cboLabelWeight.Size = new Size(0x58, 0x17);
            this.cboLabelWeight.TabIndex = 1;
            this.cboLabelWeight.SelectedIndexChanged += new EventHandler(this.cboLabelWeight_SelectedIndexChanged);
            this.cboFeatureType.EditValue = "无";
            this.cboFeatureType.Location = new Point(100, 0x29);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "无", "低", "中", "高" });
            this.cboFeatureType.Size = new Size(0x58, 0x17);
            this.cboFeatureType.TabIndex = 3;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 0x29);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "要素权值:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x93, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "高标注高度比率定义缓冲:";
            this.txtBufferRatio.EditValue = "0";
            this.txtBufferRatio.Location = new Point(0x98, 0x20);
            this.txtBufferRatio.Name = "txtBufferRatio";
            this.txtBufferRatio.Size = new Size(0x60, 0x17);
            this.txtBufferRatio.TabIndex = 4;
            this.txtBufferRatio.EditValueChanged += new EventHandler(this.txtBufferRatio_EditValueChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LabelConficPropertyPage";
            base.Size = new Size(320, 0x138);
            base.Load += new EventHandler(this.LabelConficPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.cboLabelWeight.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            this.txtBufferRatio.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LabelConficPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pOverposterLayerProperties != null)
            {
                this.cboFeatureType.SelectedIndex = (int) this.m_pOverposterLayerProperties.FeatureWeight;
                this.cboLabelWeight.SelectedIndex = ((int) this.m_pOverposterLayerProperties.LabelWeight) - 1;
                this.txtBufferRatio.Text = this.m_pOverposterLayerProperties.BufferRatio.ToString();
                this.m_CanDo = true;
            }
        }

        public void SetObjects(object @object)
        {
            this.m_pOverposterLayerProperties = @object as IBasicOverposterLayerProperties;
        }

        private void txtBufferRatio_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtBufferRatio.Text);
                    if ((num >= 0.0) && (num <= 1.0))
                    {
                        this.txtBufferRatio.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.txtBufferRatio.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    this.txtBufferRatio.ForeColor = Color.Red;
                }
            }
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
                return "冲突检测";
            }
            set
            {
            }
        }
    }
}

