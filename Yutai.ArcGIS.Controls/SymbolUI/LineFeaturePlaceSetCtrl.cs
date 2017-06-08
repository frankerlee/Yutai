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
    internal class LineFeaturePlaceSetCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private ComboBoxEdit cboCoorType;
        private ComboBoxEdit cboPlaceLinePos;
        private CheckEdit chkBelow_Right;
        private CheckEdit chkOnTop;
        private CheckEdit chkTop_Left;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ILineLabelPosition m_LineLabelPosition = null;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private RadioGroup rdoLineLabelPosition;
        private TextEdit txtOffset;

        public event OnValueChangeEventHandler OnValueChange;

        public LineFeaturePlaceSetCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtOffset.Text);
                    this.m_LineLabelPosition.Offset = num;
                }
                catch
                {
                }
                this.m_LineLabelPosition.Horizontal = this.rdoLineLabelPosition.SelectedIndex == 0;
                this.m_LineLabelPosition.Parallel = this.rdoLineLabelPosition.SelectedIndex == 1;
                this.m_LineLabelPosition.ProduceCurvedLabels = this.rdoLineLabelPosition.SelectedIndex == 2;
                this.m_LineLabelPosition.Perpendicular = this.rdoLineLabelPosition.SelectedIndex == 3;
                this.m_LineLabelPosition.InLine = this.cboPlaceLinePos.SelectedIndex == 0;
                this.m_LineLabelPosition.AtStart = this.cboPlaceLinePos.SelectedIndex == 1;
                this.m_LineLabelPosition.AtEnd = this.cboPlaceLinePos.SelectedIndex == 2;
                if (this.cboCoorType.SelectedIndex == 0)
                {
                    this.m_LineLabelPosition.Right = this.chkBelow_Right.Checked;
                    this.m_LineLabelPosition.Below = false;
                    this.m_LineLabelPosition.Left = this.chkTop_Left.Checked;
                    this.m_LineLabelPosition.Above = false;
                }
                else
                {
                    this.m_LineLabelPosition.Right = false;
                    this.m_LineLabelPosition.Below = this.chkBelow_Right.Checked;
                    this.m_LineLabelPosition.Left = false;
                    this.m_LineLabelPosition.Above = this.chkTop_Left.Checked;
                }
                this.m_LineLabelPosition.OnTop = this.chkOnTop.Checked;
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboCoorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboCoorType.SelectedIndex == 0)
            {
                this.chkBelow_Right.Text = "右";
                this.chkTop_Left.Text = "左";
                this.pictureEdit1.Visible = true;
                this.pictureEdit2.Visible = false;
            }
            else
            {
                this.chkBelow_Right.Text = "下";
                this.chkTop_Left.Text = "上";
                this.pictureEdit2.Visible = true;
                this.pictureEdit1.Visible = false;
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

        private void cboPlaceLinePos_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkBelow_Right_CheckedChanged(object sender, EventArgs e)
        {
            if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
            {
                this.cboCoorType.Enabled = false;
            }
            else
            {
                this.cboCoorType.Enabled = true;
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

        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
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

        private void chkTop_Left_CheckedChanged(object sender, EventArgs e)
        {
            if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
            {
                this.cboCoorType.Enabled = false;
            }
            else
            {
                this.cboCoorType.Enabled = true;
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LineFeaturePlaceSetCtrl));
            this.groupBox1 = new GroupBox();
            this.rdoLineLabelPosition = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit1 = new PictureEdit();
            this.label4 = new Label();
            this.txtOffset = new TextEdit();
            this.label1 = new Label();
            this.cboCoorType = new ComboBoxEdit();
            this.chkBelow_Right = new CheckEdit();
            this.chkOnTop = new CheckEdit();
            this.chkTop_Left = new CheckEdit();
            this.label2 = new Label();
            this.groupBox3 = new GroupBox();
            this.cboPlaceLinePos = new ComboBoxEdit();
            this.label3 = new Label();
            this.groupBox1.SuspendLayout();
            this.rdoLineLabelPosition.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.txtOffset.Properties.BeginInit();
            this.cboCoorType.Properties.BeginInit();
            this.chkBelow_Right.Properties.BeginInit();
            this.chkOnTop.Properties.BeginInit();
            this.chkTop_Left.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.cboPlaceLinePos.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoLineLabelPosition);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x68, 160);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方向";
            this.rdoLineLabelPosition.Location = new Point(8, 0x18);
            this.rdoLineLabelPosition.Name = "rdoLineLabelPosition";
            this.rdoLineLabelPosition.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoLineLabelPosition.Properties.Appearance.Options.UseBackColor = true;
            this.rdoLineLabelPosition.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoLineLabelPosition.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "水平放置"), new RadioGroupItem(null, "平行放置"), new RadioGroupItem(null, "弯曲放置"), new RadioGroupItem(null, "垂直放置") });
            this.rdoLineLabelPosition.Size = new Size(0x58, 80);
            this.rdoLineLabelPosition.TabIndex = 0;
            this.rdoLineLabelPosition.SelectedIndexChanged += new EventHandler(this.rdoLineLabelPosition_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtOffset);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboCoorType);
            this.groupBox2.Controls.Add(this.chkBelow_Right);
            this.groupBox2.Controls.Add(this.chkOnTop);
            this.groupBox2.Controls.Add(this.chkTop_Left);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(120, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc0, 160);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "位置";
            this.pictureEdit2.EditValue = (object)resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(0x48, 9);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0x42, 0x51);
            this.pictureEdit2.TabIndex = 10;
            this.pictureEdit1.EditValue = (object)resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0x48, 9);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0x42, 0x51);
            this.pictureEdit1.TabIndex = 9;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x88, 0x80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x36, 0x11);
            this.label4.TabIndex = 8;
            this.label4.Text = "地图单位";
            this.txtOffset.EditValue = "0";
            this.txtOffset.Location = new Point(0x48, 0x7e);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(0x38, 0x17);
            this.txtOffset.TabIndex = 7;
            this.txtOffset.EditValueChanged += new EventHandler(this.txtOffset_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 6;
            this.label1.Text = "偏移";
            this.cboCoorType.EditValue = "页面";
            this.cboCoorType.Location = new Point(0x48, 0x60);
            this.cboCoorType.Name = "cboCoorType";
            this.cboCoorType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboCoorType.Properties.Items.AddRange(new object[] { "线", "页面" });
            this.cboCoorType.Size = new Size(0x40, 0x17);
            this.cboCoorType.TabIndex = 5;
            this.cboCoorType.SelectedIndexChanged += new EventHandler(this.cboCoorType_SelectedIndexChanged);
            this.chkBelow_Right.Location = new Point(0x10, 0x40);
            this.chkBelow_Right.Name = "chkBelow_Right";
            this.chkBelow_Right.Properties.Caption = "下";
            this.chkBelow_Right.Size = new Size(0x38, 0x13);
            this.chkBelow_Right.TabIndex = 4;
            this.chkBelow_Right.CheckedChanged += new EventHandler(this.chkBelow_Right_CheckedChanged);
            this.chkOnTop.Location = new Point(0x10, 40);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Properties.Caption = "在线上";
            this.chkOnTop.Size = new Size(0x38, 0x13);
            this.chkOnTop.TabIndex = 3;
            this.chkOnTop.CheckedChanged += new EventHandler(this.chkOnTop_CheckedChanged);
            this.chkTop_Left.Location = new Point(0x10, 0x10);
            this.chkTop_Left.Name = "chkTop_Left";
            this.chkTop_Left.Properties.Caption = "上";
            this.chkTop_Left.Size = new Size(0x38, 0x13);
            this.chkTop_Left.TabIndex = 2;
            this.chkTop_Left.CheckedChanged += new EventHandler(this.chkTop_Left_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x66);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "方向系统";
            this.groupBox3.Controls.Add(this.cboPlaceLinePos);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new Point(8, 0xa3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x130, 0x38);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "标注位置";
            this.cboPlaceLinePos.EditValue = "最佳位置";
            this.cboPlaceLinePos.Location = new Point(0x70, 0x10);
            this.cboPlaceLinePos.Name = "cboPlaceLinePos";
            this.cboPlaceLinePos.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPlaceLinePos.Properties.Items.AddRange(new object[] { "最佳位置", "开始位置", "结束位置" });
            this.cboPlaceLinePos.Size = new Size(0x58, 0x17);
            this.cboPlaceLinePos.TabIndex = 6;
            this.cboPlaceLinePos.SelectedIndexChanged += new EventHandler(this.cboPlaceLinePos_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x5b, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "沿线型方向标注";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "LineFeaturePlaceSetCtrl";
            base.Size = new Size(320, 0xe8);
            base.Load += new EventHandler(this.LineFeaturePlaceSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.rdoLineLabelPosition.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.txtOffset.Properties.EndInit();
            this.cboCoorType.Properties.EndInit();
            this.chkBelow_Right.Properties.EndInit();
            this.chkOnTop.Properties.EndInit();
            this.chkTop_Left.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.cboPlaceLinePos.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LineFeaturePlaceSetCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                this.m_LineLabelPosition = this.m_OverLayerProperty.LineLabelPosition;
                if (this.m_LineLabelPosition.Horizontal)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 0;
                }
                else if (this.m_LineLabelPosition.Parallel)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 1;
                }
                else if (this.m_LineLabelPosition.ProduceCurvedLabels)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 2;
                }
                else
                {
                    this.rdoLineLabelPosition.SelectedIndex = 3;
                }
                this.txtOffset.Text = this.m_LineLabelPosition.Offset.ToString();
                if (this.m_LineLabelPosition.InLine)
                {
                    this.cboPlaceLinePos.SelectedIndex = 0;
                }
                else if (this.m_LineLabelPosition.AtStart)
                {
                    this.cboPlaceLinePos.SelectedIndex = 1;
                }
                else
                {
                    this.cboPlaceLinePos.SelectedIndex = 2;
                }
                this.chkBelow_Right.Checked = this.m_LineLabelPosition.Below ? this.m_LineLabelPosition.Below : this.m_LineLabelPosition.Right;
                this.chkTop_Left.Checked = this.m_LineLabelPosition.Above ? this.m_LineLabelPosition.Above : this.m_LineLabelPosition.Left;
                this.chkOnTop.Checked = this.m_LineLabelPosition.OnTop;
                this.cboCoorType_SelectedIndexChanged(this, e);
                if (!(this.chkTop_Left.Checked || this.chkBelow_Right.Checked))
                {
                    this.cboCoorType.Enabled = false;
                }
                else
                {
                    this.cboCoorType.Enabled = true;
                }
                this.m_CanDo = true;
            }
        }

        private void rdoLineLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
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
            this.m_OverLayerProperty = @object as IBasicOverposterLayerProperties4;
        }

        private void txtOffset_EditValueChanged(object sender, EventArgs e)
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
                    num = double.Parse(this.txtOffset.Text);
                    this.txtOffset.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtOffset.ForeColor = Color.Red;
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
                return "";
            }
            set
            {
            }
        }
    }
}

