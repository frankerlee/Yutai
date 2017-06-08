using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal class PointFeatureLabelCtrl : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnAddAngle;
        private SimpleButton btnDeleteLayer;
        private SimpleButton btnMoveDown;
        private SimpleButton btnMoveUp;
        private ComboBoxEdit cboFields;
        private CheckEdit chkPerpendicularToAngle;
        private IContainer components;
        private GroupBox groupBox1;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox listPointPlacementAngles;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IBasicOverposterLayerProperties4 m_OverLayerProperty = null;
        private IPointPlacementPriorities m_Priorities = null;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private RadioGroup rdoPointPlacementMethod;
        private RadioGroup rdoRotationType;
        private TextEdit txtAngle;

        public event OnValueChangeEventHandler OnValueChange;

        public PointFeatureLabelCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex];
                this.m_OverLayerProperty.PointPlacementMethod = (esriOverposterPointPlacementMethod) this.rdoPointPlacementMethod.SelectedIndex;
                switch (this.rdoPointPlacementMethod.SelectedIndex)
                {
                    case 0:
                        this.SetPlacementPriorities(item.Value.ToString(), this.m_Priorities);
                        this.m_OverLayerProperty.PointPlacementPriorities = this.m_Priorities;
                        break;

                    case 1:
                    {
                        double[] numArray = new double[this.listPointPlacementAngles.Items.Count];
                        for (int i = 0; i < this.listPointPlacementAngles.Items.Count; i++)
                        {
                            numArray[i] = (double) this.listPointPlacementAngles.Items[i];
                        }
                        this.m_OverLayerProperty.PointPlacementAngles = numArray;
                        break;
                    }
                    case 3:
                        if (this.cboFields.SelectedIndex != -1)
                        {
                            this.m_OverLayerProperty.RotationField = this.cboFields.Text;
                        }
                        this.m_OverLayerProperty.PerpendicularToAngle = this.chkPerpendicularToAngle.Checked;
                        if (this.rdoRotationType.SelectedIndex == 0)
                        {
                            this.m_OverLayerProperty.RotationType = esriLabelRotationType.esriRotateLabelGeographic;
                        }
                        else
                        {
                            this.m_OverLayerProperty.RotationType = esriLabelRotationType.esriRotateLabelArithmetic;
                        }
                        break;
                }
            }
        }

        private void btnAddAngle_Click(object sender, EventArgs e)
        {
            try
            {
                double item = double.Parse(this.txtAngle.Text);
                this.listPointPlacementAngles.Items.Add(item);
                if (this.m_CanDo)
                {
                    this.m_IsPageDirty = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if (selectedIndex != -1)
                {
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if ((selectedIndex > -1) && (selectedIndex < (this.listPointPlacementAngles.Items.Count - 1)))
                {
                    object item = this.listPointPlacementAngles.Items[selectedIndex];
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                    if (this.listPointPlacementAngles.Items.Count == selectedIndex)
                    {
                        this.listPointPlacementAngles.Items.Add(item);
                    }
                    else
                    {
                        this.listPointPlacementAngles.Items.Insert(selectedIndex + 1, item);
                    }
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsPageDirty = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
                int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
                if (selectedIndex > 0)
                {
                    object item = this.listPointPlacementAngles.Items[selectedIndex];
                    this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                    this.listPointPlacementAngles.Items.Insert(selectedIndex - 1, item);
                }
            }
        }

        public void Cancel()
        {
        }

        private void chkPerpendicularToAngle_CheckedChanged(object sender, EventArgs e)
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

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
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

        private int GetNumber(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;
            }
            return 0;
        }

        private string GetPlacementPrioritiesStr(IPointPlacementPriorities Priorities)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetValue(Priorities.AboveLeft));
            builder.Append(this.GetValue(Priorities.AboveCenter));
            builder.Append(this.GetValue(Priorities.AboveRight));
            builder.Append(this.GetValue(Priorities.CenterLeft));
            builder.Append(this.GetValue(0));
            builder.Append(this.GetValue(Priorities.CenterRight));
            builder.Append(this.GetValue(Priorities.BelowLeft));
            builder.Append(this.GetValue(Priorities.BelowCenter));
            builder.Append(this.GetValue(Priorities.BelowRight));
            return builder.ToString();
        }

        private int GetValue(int i)
        {
            if (i > 3)
            {
                return 3;
            }
            return i;
        }

        public void Hide()
        {
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PointFeatureLabelCtrl));
            this.rdoPointPlacementMethod = new RadioGroup();
            this.panel1 = new Panel();
            this.label3 = new Label();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.imageList1 = new ImageList(this.components);
            this.panel2 = new Panel();
            this.label1 = new Label();
            this.txtAngle = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnMoveDown = new SimpleButton();
            this.btnMoveUp = new SimpleButton();
            this.btnDeleteLayer = new SimpleButton();
            this.listPointPlacementAngles = new ListBox();
            this.btnAddAngle = new SimpleButton();
            this.panel3 = new Panel();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit1 = new PictureEdit();
            this.chkPerpendicularToAngle = new CheckEdit();
            this.cboFields = new ComboBoxEdit();
            this.rdoRotationType = new RadioGroup();
            this.label2 = new Label();
            this.rdoPointPlacementMethod.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.txtAngle.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.chkPerpendicularToAngle.Properties.BeginInit();
            this.cboFields.Properties.BeginInit();
            this.rdoRotationType.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoPointPlacementMethod.Location = new Point(8, 1);
            this.rdoPointPlacementMethod.Name = "rdoPointPlacementMethod";
            this.rdoPointPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPointPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPointPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPointPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在点周围水平偏移标注"), new RadioGroupItem(null, "在点上部放置标注"), new RadioGroupItem(null, "以指定角度放置标注"), new RadioGroupItem(null, "根据字段值设置标注角度") });
            this.rdoPointPlacementMethod.Size = new Size(0xe0, 0x48);
            this.rdoPointPlacementMethod.TabIndex = 0;
            this.rdoPointPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPointPlacementMethod_SelectedIndexChanged);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.imageComboBoxEdit1);
            this.panel1.Location = new Point(0x10, 0x4a);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xe0, 120);
            this.panel1.TabIndex = 1;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xc4, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "优先级：0=禁止，1=最高，3=最低 ";
            this.imageComboBoxEdit1.EditValue = "001000000";
            this.imageComboBoxEdit1.Location = new Point(0x10, 0x10);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new object[] { 
                new ImageComboBoxItem("仅在右上", "001000000", 0), new ImageComboBoxItem("", "010000000", 1), new ImageComboBoxItem("", "100000000", 2), new ImageComboBoxItem("", "000001000", 3), new ImageComboBoxItem("", "000000001", 4), new ImageComboBoxItem("", "000000010", 5), new ImageComboBoxItem("", "000000100", 6), new ImageComboBoxItem("", "000100000", 7), new ImageComboBoxItem("", "221000000", 8), new ImageComboBoxItem("", "001002002", 9), new ImageComboBoxItem("", "212000000", 10), new ImageComboBoxItem("", "002001002", 11), new ImageComboBoxItem("", "002002001", 12), new ImageComboBoxItem("", "000000221", 13), new ImageComboBoxItem("", "000000212", 14), new ImageComboBoxItem("", "000000122", 15), 
                new ImageComboBoxItem("", "200200100", 0x10), new ImageComboBoxItem("", "200100200", 0x11), new ImageComboBoxItem("", "100200200", 0x12), new ImageComboBoxItem("", "122000000", 0x13), new ImageComboBoxItem("", "221003003", 20), new ImageComboBoxItem("", "331002002", 0x15), new ImageComboBoxItem("", "002002331", 0x16), new ImageComboBoxItem("", "003003221", 0x17), new ImageComboBoxItem("", "200200133", 0x18), new ImageComboBoxItem("", "300300122", 0x19), new ImageComboBoxItem("", "122300300", 0x1a), new ImageComboBoxItem("", "133200200", 0x1b), new ImageComboBoxItem("", "221302332", 0x1c), new ImageComboBoxItem("", "212202232", 0x1d), new ImageComboBoxItem("", "122203233", 30), new ImageComboBoxItem("", "222103222", 0x1f), 
                new ImageComboBoxItem("", "233203122", 0x20), new ImageComboBoxItem("", "232202212", 0x21), new ImageComboBoxItem("", "332302221", 0x22), new ImageComboBoxItem("", "222301222", 0x23)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList1;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList1;
            this.imageComboBoxEdit1.Size = new Size(0x68, 0x43);
            this.imageComboBoxEdit1.TabIndex = 1;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            this.imageList1.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new Size(0x4e, 0x3f);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtAngle);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnAddAngle);
            this.panel2.Location = new Point(0x10, 0x4a);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(240, 0xa8);
            this.panel2.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "新建角度";
            this.txtAngle.EditValue = "";
            this.txtAngle.Location = new Point(0x48, 4);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(0x40, 0x17);
            this.txtAngle.TabIndex = 2;
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnDeleteLayer);
            this.groupBox1.Controls.Add(this.listPointPlacementAngles);
            this.groupBox1.Location = new Point(0x10, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 0x88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角度";
            this.btnMoveDown.Image = (Image) resources.GetObject("btnMoveDown.Image");
            this.btnMoveDown.Location = new Point(0x88, 0x30);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new Size(0x18, 0x18);
            this.btnMoveDown.TabIndex = 0x18;
            this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
            this.btnMoveUp.Image = (Image) resources.GetObject("btnMoveUp.Image");
            this.btnMoveUp.Location = new Point(0x88, 0x10);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new Size(0x18, 0x18);
            this.btnMoveUp.TabIndex = 0x17;
            this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
            this.btnDeleteLayer.Image = (Image) resources.GetObject("btnDeleteLayer.Image");
            this.btnDeleteLayer.Location = new Point(0x88, 80);
            this.btnDeleteLayer.Name = "btnDeleteLayer";
            this.btnDeleteLayer.Size = new Size(0x18, 0x18);
            this.btnDeleteLayer.TabIndex = 0x16;
            this.btnDeleteLayer.Click += new EventHandler(this.btnDeleteLayer_Click);
            this.listPointPlacementAngles.ItemHeight = 12;
            this.listPointPlacementAngles.Location = new Point(8, 0x10);
            this.listPointPlacementAngles.Name = "listPointPlacementAngles";
            this.listPointPlacementAngles.Size = new Size(120, 0x58);
            this.listPointPlacementAngles.TabIndex = 0;
            this.listPointPlacementAngles.SelectedIndexChanged += new EventHandler(this.listPointPlacementAngles_SelectedIndexChanged);
            this.btnAddAngle.Location = new Point(0x90, 8);
            this.btnAddAngle.Name = "btnAddAngle";
            this.btnAddAngle.Size = new Size(0x48, 0x18);
            this.btnAddAngle.TabIndex = 0;
            this.btnAddAngle.Text = "添加角度";
            this.btnAddAngle.Click += new EventHandler(this.btnAddAngle_Click);
            this.panel3.Controls.Add(this.pictureEdit2);
            this.panel3.Controls.Add(this.pictureEdit1);
            this.panel3.Controls.Add(this.chkPerpendicularToAngle);
            this.panel3.Controls.Add(this.cboFields);
            this.panel3.Controls.Add(this.rdoRotationType);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new Point(0x10, 0x4a);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0xc0, 0xa8);
            this.panel3.TabIndex = 3;
            this.pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(0x58, 0x30);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0x3b, 0x3b);
            this.pictureEdit2.TabIndex = 9;
            this.pictureEdit1.EditValue = (object)resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0x10, 0x30);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0x3b, 0x3b);
            this.pictureEdit1.TabIndex = 8;
            this.chkPerpendicularToAngle.Location = new Point(0x10, 0x88);
            this.chkPerpendicularToAngle.Name = "chkPerpendicularToAngle";
            this.chkPerpendicularToAngle.Properties.Caption = "标注方向垂直于该角度";
            this.chkPerpendicularToAngle.Size = new Size(0x98, 0x13);
            this.chkPerpendicularToAngle.TabIndex = 7;
            this.chkPerpendicularToAngle.CheckedChanged += new EventHandler(this.chkPerpendicularToAngle_CheckedChanged);
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x30, 0x10);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x70, 0x17);
            this.cboFields.TabIndex = 6;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.rdoRotationType.Location = new Point(0x10, 0x68);
            this.rdoRotationType.Name = "rdoRotationType";
            this.rdoRotationType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoRotationType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRotationType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRotationType.Properties.Columns = 2;
            this.rdoRotationType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "地理的"), new RadioGroupItem(null, "数学的") });
            this.rdoRotationType.Size = new Size(0x98, 0x20);
            this.rdoRotationType.TabIndex = 5;
            this.rdoRotationType.SelectedIndexChanged += new EventHandler(this.rdoRotationType_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 4;
            this.label2.Text = "字段";
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.rdoPointPlacementMethod);
            base.Name = "PointFeatureLabelCtrl";
            base.Size = new Size(0x110, 0x100);
            base.Load += new EventHandler(this.PointFeatureLabelCtrl_Load);
            this.rdoPointPlacementMethod.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.imageComboBoxEdit1.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.txtAngle.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.chkPerpendicularToAngle.Properties.EndInit();
            this.cboFields.Properties.EndInit();
            this.rdoRotationType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void listPointPlacementAngles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listPointPlacementAngles.SelectedIndex != -1)
            {
                if (this.listPointPlacementAngles.Items.Count == 1)
                {
                    this.btnMoveUp.Enabled = false;
                    this.btnMoveDown.Enabled = false;
                }
                else if (this.listPointPlacementAngles.SelectedIndex == 0)
                {
                    this.btnMoveUp.Enabled = false;
                    this.btnMoveDown.Enabled = true;
                }
                else if (this.listPointPlacementAngles.SelectedIndex == (this.listPointPlacementAngles.Items.Count - 1))
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = false;
                }
                else
                {
                    this.btnMoveUp.Enabled = true;
                    this.btnMoveDown.Enabled = true;
                }
                this.btnDeleteLayer.Enabled = true;
            }
            else
            {
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnDeleteLayer.Enabled = false;
            }
        }

        private void PointFeatureLabelCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_OverLayerProperty != null)
            {
                int num;
                this.rdoPointPlacementMethod.SelectedIndex = (int) this.m_OverLayerProperty.PointPlacementMethod;
                if (this.m_OverLayerProperty.RotationType == esriLabelRotationType.esriRotateLabelGeographic)
                {
                    this.rdoRotationType.SelectedIndex = 0;
                }
                else
                {
                    this.rdoRotationType.SelectedIndex = 1;
                }
                this.chkPerpendicularToAngle.Checked = this.m_OverLayerProperty.PerpendicularToAngle;
                this.m_Priorities = this.m_OverLayerProperty.PointPlacementPriorities;
                string placementPrioritiesStr = this.GetPlacementPrioritiesStr(this.m_Priorities);
                for (num = 0; num < this.imageComboBoxEdit1.Properties.Items.Count; num++)
                {
                    ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[num];
                    if (item.Value.ToString() == placementPrioritiesStr)
                    {
                        this.imageComboBoxEdit1.SelectedIndex = num;
                        break;
                    }
                }
                double[] pointPlacementAngles = (double[]) this.m_OverLayerProperty.PointPlacementAngles;
                for (num = 0; num < pointPlacementAngles.Length; num++)
                {
                    this.listPointPlacementAngles.Items.Add(pointPlacementAngles[num]);
                }
                this.rdoPointPlacementMethod_SelectedIndexChanged(this, e);
                this.m_CanDo = true;
            }
        }

        private void rdoPointPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPointPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                    break;

                case 1:
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.panel3.Visible = false;
                    break;

                case 2:
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = false;
                    break;

                case 3:
                    this.panel1.Visible = false;
                    this.panel2.Visible = false;
                    this.panel3.Visible = true;
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

        private void rdoRotationType_SelectedIndexChanged(object sender, EventArgs e)
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

        private void SetPlacementPriorities(string s, IPointPlacementPriorities Priorities)
        {
            Priorities.AboveLeft = this.GetNumber(s[0]);
            Priorities.AboveCenter = this.GetNumber(s[1]);
            Priorities.AboveRight = this.GetNumber(s[2]);
            Priorities.CenterLeft = this.GetNumber(s[3]);
            Priorities.CenterRight = this.GetNumber(s[5]);
            Priorities.BelowLeft = this.GetNumber(s[6]);
            Priorities.BelowCenter = this.GetNumber(s[7]);
            Priorities.BelowRight = this.GetNumber(s[8]);
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

