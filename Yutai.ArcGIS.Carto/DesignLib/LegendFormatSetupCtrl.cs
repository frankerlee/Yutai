using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class LegendFormatSetupCtrl : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ILegend ilegend_0 = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private PictureEdit pictureEdit4;
        private PictureEdit pictureEdit5;
        private PictureEdit pictureEdit6;
        private PictureEdit pictureEdit7;
        private PictureEdit pictureEdit8;
        private TextEdit txtGroupGap;
        private TextEdit txtHeadingGap;
        private TextEdit txtHorizontalItemGap;
        private TextEdit txtHorizontalPatchGap;
        private TextEdit txtTextGap;
        private TextEdit txtTitleGap;
        private TextEdit txtVerticalItemGap;

        public LegendFormatSetupCtrl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendFormatSetupCtrl));
            this.groupBox1 = new GroupBox();
            this.label14 = new Label();
            this.label13 = new Label();
            this.label12 = new Label();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.txtHorizontalPatchGap = new TextEdit();
            this.txtGroupGap = new TextEdit();
            this.txtTextGap = new TextEdit();
            this.txtHeadingGap = new TextEdit();
            this.txtHorizontalItemGap = new TextEdit();
            this.txtVerticalItemGap = new TextEdit();
            this.txtTitleGap = new TextEdit();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.pictureEdit1 = new PictureEdit();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit3 = new PictureEdit();
            this.pictureEdit4 = new PictureEdit();
            this.pictureEdit5 = new PictureEdit();
            this.pictureEdit6 = new PictureEdit();
            this.pictureEdit7 = new PictureEdit();
            this.pictureEdit8 = new PictureEdit();
            this.groupBox1.SuspendLayout();
            this.txtHorizontalPatchGap.Properties.BeginInit();
            this.txtGroupGap.Properties.BeginInit();
            this.txtTextGap.Properties.BeginInit();
            this.txtHeadingGap.Properties.BeginInit();
            this.txtHorizontalItemGap.Properties.BeginInit();
            this.txtVerticalItemGap.Properties.BeginInit();
            this.txtTitleGap.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit3.Properties.BeginInit();
            this.pictureEdit4.Properties.BeginInit();
            this.pictureEdit5.Properties.BeginInit();
            this.pictureEdit6.Properties.BeginInit();
            this.pictureEdit7.Properties.BeginInit();
            this.pictureEdit8.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtHorizontalPatchGap);
            this.groupBox1.Controls.Add(this.txtGroupGap);
            this.groupBox1.Controls.Add(this.txtTextGap);
            this.groupBox1.Controls.Add(this.txtHeadingGap);
            this.groupBox1.Controls.Add(this.txtHorizontalItemGap);
            this.groupBox1.Controls.Add(this.txtVerticalItemGap);
            this.groupBox1.Controls.Add(this.txtTitleGap);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xb8, 0xe0);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "间隔";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(160, 0xc4);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x11, 0x11);
            this.label14.TabIndex = 20;
            this.label14.Text = "点";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(160, 0xa7);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 0x11);
            this.label13.TabIndex = 0x13;
            this.label13.Text = "点";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(160, 0x8d);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 0x11);
            this.label12.TabIndex = 0x12;
            this.label12.Text = "点";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(160, 0x70);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x11, 0x11);
            this.label11.TabIndex = 0x11;
            this.label11.Text = "点";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(160, 0x52);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 0x11);
            this.label10.TabIndex = 0x10;
            this.label10.Text = "点";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(160, 0x36);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 0x11);
            this.label9.TabIndex = 15;
            this.label9.Text = "点";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(160, 0x1d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 14;
            this.label8.Text = "点";
            this.txtHorizontalPatchGap.EditValue = "5";
            this.txtHorizontalPatchGap.Location = new Point(0x58, 0xc0);
            this.txtHorizontalPatchGap.Name = "txtHorizontalPatchGap";
            this.txtHorizontalPatchGap.Size = new Size(0x40, 0x15);
            this.txtHorizontalPatchGap.TabIndex = 13;
            this.txtHorizontalPatchGap.Enter += new EventHandler(this.txtHorizontalPatchGap_Enter);
            this.txtHorizontalPatchGap.EditValueChanged += new EventHandler(this.txtHorizontalPatchGap_EditValueChanged);
            this.txtHorizontalPatchGap.Leave += new EventHandler(this.txtHorizontalPatchGap_Leave);
            this.txtGroupGap.EditValue = "5";
            this.txtGroupGap.Location = new Point(0x58, 0xa4);
            this.txtGroupGap.Name = "txtGroupGap";
            this.txtGroupGap.Size = new Size(0x40, 0x15);
            this.txtGroupGap.TabIndex = 12;
            this.txtGroupGap.Enter += new EventHandler(this.txtGroupGap_Enter);
            this.txtGroupGap.EditValueChanged += new EventHandler(this.txtGroupGap_EditValueChanged);
            this.txtGroupGap.Leave += new EventHandler(this.txtGroupGap_Leave);
            this.txtTextGap.EditValue = "5";
            this.txtTextGap.Location = new Point(0x58, 0x88);
            this.txtTextGap.Name = "txtTextGap";
            this.txtTextGap.Size = new Size(0x40, 0x15);
            this.txtTextGap.TabIndex = 11;
            this.txtTextGap.Enter += new EventHandler(this.txtTextGap_Enter);
            this.txtTextGap.EditValueChanged += new EventHandler(this.txtTextGap_EditValueChanged);
            this.txtTextGap.Leave += new EventHandler(this.txtTextGap_Leave);
            this.txtHeadingGap.EditValue = "5";
            this.txtHeadingGap.Location = new Point(0x58, 0x6c);
            this.txtHeadingGap.Name = "txtHeadingGap";
            this.txtHeadingGap.Size = new Size(0x40, 0x15);
            this.txtHeadingGap.TabIndex = 10;
            this.txtHeadingGap.Enter += new EventHandler(this.txtHeadingGap_Enter);
            this.txtHeadingGap.EditValueChanged += new EventHandler(this.txtHeadingGap_EditValueChanged);
            this.txtHeadingGap.Leave += new EventHandler(this.txtHeadingGap_Leave);
            this.txtHorizontalItemGap.EditValue = "5";
            this.txtHorizontalItemGap.Location = new Point(0x58, 80);
            this.txtHorizontalItemGap.Name = "txtHorizontalItemGap";
            this.txtHorizontalItemGap.Size = new Size(0x40, 0x15);
            this.txtHorizontalItemGap.TabIndex = 9;
            this.txtHorizontalItemGap.Enter += new EventHandler(this.txtHorizontalItemGap_Enter);
            this.txtHorizontalItemGap.EditValueChanged += new EventHandler(this.txtHorizontalItemGap_EditValueChanged);
            this.txtHorizontalItemGap.Leave += new EventHandler(this.txtHorizontalItemGap_Leave);
            this.txtVerticalItemGap.EditValue = "5";
            this.txtVerticalItemGap.Location = new Point(0x58, 0x34);
            this.txtVerticalItemGap.Name = "txtVerticalItemGap";
            this.txtVerticalItemGap.Size = new Size(0x40, 0x15);
            this.txtVerticalItemGap.TabIndex = 8;
            this.txtVerticalItemGap.Enter += new EventHandler(this.txtVerticalItemGap_Enter);
            this.txtVerticalItemGap.EditValueChanged += new EventHandler(this.txtVerticalItemGap_EditValueChanged);
            this.txtVerticalItemGap.Leave += new EventHandler(this.txtVerticalItemGap_Leave);
            this.txtTitleGap.EditValue = "8";
            this.txtTitleGap.Location = new Point(0x58, 0x18);
            this.txtTitleGap.Name = "txtTitleGap";
            this.txtTitleGap.Size = new Size(0x40, 0x15);
            this.txtTitleGap.TabIndex = 7;
            this.txtTitleGap.Enter += new EventHandler(this.txtTitleGap_Enter);
            this.txtTitleGap.EditValueChanged += new EventHandler(this.txtTitleGap_EditValueChanged);
            this.txtTitleGap.Leave += new EventHandler(this.txtTitleGap_Leave);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(8, 0xc4);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x42, 0x11);
            this.label7.TabIndex = 6;
            this.label7.Text = "区块和标注";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0xa7);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x42, 0x11);
            this.label6.TabIndex = 5;
            this.label6.Text = "区块(纵向)";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x8d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x42, 0x11);
            this.label5.TabIndex = 4;
            this.label5.Text = "标注和描述";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x70);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2a, 0x11);
            this.label4.TabIndex = 3;
            this.label4.Text = "头和类";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x52);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "列";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x36);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2a, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "图例项";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x1d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题和图例项";
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(200, 0x18);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0xee, 190);
            this.pictureEdit1.TabIndex = 1;
            this.pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(200, 0x18);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0xee, 190);
            this.pictureEdit2.TabIndex = 2;
            this.pictureEdit3.EditValue = resources.GetObject("pictureEdit3.EditValue");
            this.pictureEdit3.Location = new Point(200, 0x18);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit3.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit3.Size = new Size(0xee, 190);
            this.pictureEdit3.TabIndex = 3;
            this.pictureEdit4.EditValue = resources.GetObject("pictureEdit4.EditValue");
            this.pictureEdit4.Location = new Point(200, 0x18);
            this.pictureEdit4.Name = "pictureEdit4";
            this.pictureEdit4.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit4.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit4.Size = new Size(0xee, 190);
            this.pictureEdit4.TabIndex = 4;
            this.pictureEdit5.EditValue = resources.GetObject("pictureEdit5.EditValue");
            this.pictureEdit5.Location = new Point(200, 0x18);
            this.pictureEdit5.Name = "pictureEdit5";
            this.pictureEdit5.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit5.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit5.Size = new Size(0xee, 190);
            this.pictureEdit5.TabIndex = 5;
            this.pictureEdit6.EditValue = resources.GetObject("pictureEdit6.EditValue");
            this.pictureEdit6.Location = new Point(200, 0x18);
            this.pictureEdit6.Name = "pictureEdit6";
            this.pictureEdit6.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit6.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit6.Size = new Size(0xee, 190);
            this.pictureEdit6.TabIndex = 6;
            this.pictureEdit7.EditValue = resources.GetObject("pictureEdit7.EditValue");
            this.pictureEdit7.Location = new Point(200, 0x18);
            this.pictureEdit7.Name = "pictureEdit7";
            this.pictureEdit7.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit7.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit7.Size = new Size(0xee, 190);
            this.pictureEdit7.TabIndex = 7;
            this.pictureEdit8.EditValue = resources.GetObject("pictureEdit8.EditValue");
            this.pictureEdit8.Location = new Point(200, 0x18);
            this.pictureEdit8.Name = "pictureEdit8";
            this.pictureEdit8.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit8.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit8.Size = new Size(0xee, 190);
            this.pictureEdit8.TabIndex = 8;
            base.Controls.Add(this.pictureEdit8);
            base.Controls.Add(this.pictureEdit7);
            base.Controls.Add(this.pictureEdit6);
            base.Controls.Add(this.pictureEdit5);
            base.Controls.Add(this.pictureEdit4);
            base.Controls.Add(this.pictureEdit3);
            base.Controls.Add(this.pictureEdit2);
            base.Controls.Add(this.pictureEdit1);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendFormatSetupCtrl";
            base.Size = new Size(0x1c8, 0x108);
            base.Load += new EventHandler(this.LegendFormatSetupCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtHorizontalPatchGap.Properties.EndInit();
            this.txtGroupGap.Properties.EndInit();
            this.txtTextGap.Properties.EndInit();
            this.txtHeadingGap.Properties.EndInit();
            this.txtHorizontalItemGap.Properties.EndInit();
            this.txtVerticalItemGap.Properties.EndInit();
            this.txtTitleGap.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit3.Properties.EndInit();
            this.pictureEdit4.Properties.EndInit();
            this.pictureEdit5.Properties.EndInit();
            this.pictureEdit6.Properties.EndInit();
            this.pictureEdit7.Properties.EndInit();
            this.pictureEdit8.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LegendFormatSetupCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.txtGroupGap.Text = this.ilegend_0.Format.GroupGap.ToString();
            this.txtHeadingGap.Text = this.ilegend_0.Format.HeadingGap.ToString();
            this.txtHorizontalItemGap.Text = this.ilegend_0.Format.HorizontalItemGap.ToString();
            this.txtHorizontalPatchGap.Text = this.ilegend_0.Format.HorizontalPatchGap.ToString();
            this.txtTextGap.Text = this.ilegend_0.Format.TextGap.ToString();
            this.txtTitleGap.Text = this.ilegend_0.Format.TitleGap.ToString();
            this.txtVerticalItemGap.Text = this.ilegend_0.Format.VerticalItemGap.ToString();
        }

        private void txtGroupGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtGroupGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtGroupGap.Text);
                    this.ilegend_0.Format.GroupGap = num;
                }
                catch (Exception)
                {
                    this.txtGroupGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtGroupGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = true;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtGroupGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHeadingGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHeadingGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHeadingGap.Text);
                    this.ilegend_0.Format.HeadingGap = num;
                }
                catch (Exception)
                {
                    this.txtHeadingGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHeadingGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = true;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtHeadingGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHorizontalItemGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHorizontalItemGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHorizontalItemGap.Text);
                    this.ilegend_0.Format.HorizontalItemGap = num;
                }
                catch (Exception)
                {
                    this.txtHorizontalItemGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHorizontalItemGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = true;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtHorizontalItemGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtHorizontalPatchGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtHorizontalPatchGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtHorizontalPatchGap.Text);
                    this.ilegend_0.Format.HorizontalPatchGap = num;
                }
                catch (Exception)
                {
                    this.txtHorizontalPatchGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtHorizontalPatchGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = true;
            this.pictureEdit8.Visible = false;
        }

        private void txtHorizontalPatchGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtTextGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtTextGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtTextGap.Text);
                    this.ilegend_0.Format.TextGap = num;
                }
                catch (Exception)
                {
                    this.txtTextGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtTextGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = true;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtTextGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtTitleGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtTitleGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtTitleGap.Text);
                    this.ilegend_0.Format.TitleGap = num;
                }
                catch (Exception)
                {
                    this.txtTitleGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtTitleGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = true;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtTitleGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        private void txtVerticalItemGap_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    this.txtVerticalItemGap.ForeColor = Color.Black;
                    double num = double.Parse(this.txtVerticalItemGap.Text);
                    this.ilegend_0.Format.VerticalItemGap = num;
                }
                catch (Exception)
                {
                    this.txtVerticalItemGap.ForeColor = Color.Red;
                }
            }
        }

        private void txtVerticalItemGap_Enter(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = true;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = false;
        }

        private void txtVerticalItemGap_Leave(object sender, EventArgs e)
        {
            this.pictureEdit1.Visible = false;
            this.pictureEdit2.Visible = false;
            this.pictureEdit3.Visible = false;
            this.pictureEdit4.Visible = false;
            this.pictureEdit5.Visible = false;
            this.pictureEdit6.Visible = false;
            this.pictureEdit7.Visible = false;
            this.pictureEdit8.Visible = true;
        }

        public ILegend Legend
        {
            set
            {
                this.ilegend_0 = value;
            }
        }
    }
}

