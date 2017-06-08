using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public class TrailsSetCtrl : UserControl, BaseClass.IPropertyPage, BaseClass.IPropertyPageEvents
    {
        private NewSymbolButton btnLineSymbol;
        private NewSymbolButton btnPointSymbol;
        private ColorRampComboBox cboSpeedColorRamp;
        private CheckBox chkLineTrail;
        private CheckBox chkPointTrail;
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IPositionTrails m_pPositionTrails = null;
        private IStyleGallery m_pSG = null;
        private string m_Title = "轨迹";
        private TextBox txtDistance;
        private TextBox txtLineLength;
        private TextBox txtPointNum;

        public event BaseClass.OnValueChangeEventHandler OnValueChange;

        public TrailsSetCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_pPositionTrails.ShowLinearTrail = this.chkLineTrail.Checked;
                this.m_pPositionTrails.ShowMarkerTrails = this.chkPointTrail.Checked;
                this.m_pPositionTrails.LinearTrailSymbol = this.btnLineSymbol.Style as ILineSymbol;
                this.m_pPositionTrails.MarkerTrailSymbol = this.btnPointSymbol.Style as IMarkerSymbol;
                try
                {
                    this.m_pPositionTrails.MarkerTrailDistance = double.Parse(this.txtDistance.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pPositionTrails.MarkerTrailCount = int.Parse(this.txtPointNum.Text);
                }
                catch
                {
                }
                try
                {
                    this.m_pPositionTrails.LinearTrailDistance = double.Parse(this.txtLineLength.Text);
                }
                catch
                {
                }
                if (this.cboSpeedColorRamp.SelectedIndex != -1)
                {
                    this.m_pPositionTrails.MarkerTrailColorRamp = this.cboSpeedColorRamp.GetSelectColorRamp();
                }
            }
        }

        private void btnLineSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                ILineSymbol style = this.btnLineSymbol.Style as ILineSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        style = selector.GetSymbol() as ILineSymbol;
                        this.btnLineSymbol.Style = style;
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnPointSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                IMarkerSymbol style = this.btnPointSymbol.Style as IMarkerSymbol;
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        style = selector.GetSymbol() as IMarkerSymbol;
                        this.btnPointSymbol.Style = style;
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void cboSpeedColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkLineTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void chkPointTrail_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtDistance = new TextBox();
            this.txtPointNum = new TextBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.btnPointSymbol = new NewSymbolButton();
            this.chkPointTrail = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.txtLineLength = new TextBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.btnLineSymbol = new NewSymbolButton();
            this.chkLineTrail = new CheckBox();
            this.cboSpeedColorRamp = new ColorRampComboBox();
            this.label6 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboSpeedColorRamp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDistance);
            this.groupBox1.Controls.Add(this.txtPointNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPointSymbol);
            this.groupBox1.Controls.Add(this.chkPointTrail);
            this.groupBox1.Location = new Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x12a, 0xbf);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "点轨迹";
            this.txtDistance.Location = new Point(0x5c, 0xa1);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new Size(0x5f, 0x15);
            this.txtDistance.TabIndex = 6;
            this.txtDistance.TextChanged += new EventHandler(this.txtDistance_TextChanged);
            this.txtPointNum.Location = new Point(0x5c, 0x89);
            this.txtPointNum.Name = "txtPointNum";
            this.txtPointNum.Size = new Size(0x5f, 0x15);
            this.txtPointNum.TabIndex = 5;
            this.txtPointNum.TextChanged += new EventHandler(this.txtPointNum_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(10, 0xa4);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "间距:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 140);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "点数:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 0x3e);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "符号:";
            this.btnPointSymbol.Location = new Point(0x5c, 50);
            this.btnPointSymbol.Name = "btnPointSymbol";
            this.btnPointSymbol.Size = new Size(70, 0x27);
            this.btnPointSymbol.Style = null;
            this.btnPointSymbol.TabIndex = 1;
            this.btnPointSymbol.Click += new EventHandler(this.btnPointSymbol_Click);
            this.chkPointTrail.AutoSize = true;
            this.chkPointTrail.Location = new Point(12, 0x1a);
            this.chkPointTrail.Name = "chkPointTrail";
            this.chkPointTrail.Size = new Size(0x54, 0x10);
            this.chkPointTrail.TabIndex = 0;
            this.chkPointTrail.Text = "显示点轨迹";
            this.chkPointTrail.UseVisualStyleBackColor = true;
            this.chkPointTrail.CheckedChanged += new EventHandler(this.chkPointTrail_CheckedChanged);
            this.groupBox2.Controls.Add(this.txtLineLength);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnLineSymbol);
            this.groupBox2.Controls.Add(this.chkLineTrail);
            this.groupBox2.Location = new Point(8, 0xd3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x12a, 0x87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线轨迹";
            this.txtLineLength.Location = new Point(0x4c, 0x62);
            this.txtLineLength.Name = "txtLineLength";
            this.txtLineLength.Size = new Size(0x5f, 0x15);
            this.txtLineLength.TabIndex = 6;
            this.txtLineLength.TextChanged += new EventHandler(this.txtLineLength_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(10, 0x65);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2f, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "线长度:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(10, 0x3a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "符号:";
            this.btnLineSymbol.Location = new Point(0x4c, 0x2a);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(100, 0x27);
            this.btnLineSymbol.Style = null;
            this.btnLineSymbol.TabIndex = 2;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.chkLineTrail.AutoSize = true;
            this.chkLineTrail.Location = new Point(12, 20);
            this.chkLineTrail.Name = "chkLineTrail";
            this.chkLineTrail.Size = new Size(0x54, 0x10);
            this.chkLineTrail.TabIndex = 1;
            this.chkLineTrail.Text = "显示线轨迹";
            this.chkLineTrail.UseVisualStyleBackColor = true;
            this.chkLineTrail.CheckedChanged += new EventHandler(this.chkLineTrail_CheckedChanged);
            this.cboSpeedColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboSpeedColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSpeedColorRamp.FormattingEnabled = true;
            this.cboSpeedColorRamp.Location = new Point(0x5c, 0x66);
            this.cboSpeedColorRamp.Name = "cboSpeedColorRamp";
            this.cboSpeedColorRamp.Size = new Size(0xbd, 0x16);
            this.cboSpeedColorRamp.TabIndex = 13;
            this.cboSpeedColorRamp.SelectedIndexChanged += new EventHandler(this.cboSpeedColorRamp_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(10, 0x69);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x35, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "符号范围";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "TrailsSetCtrl";
            base.Size = new Size(0x13f, 0x171);
            base.Load += new EventHandler(this.TrailsSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        void BaseClass.IPropertyPage.Hide()
        {
            base.Hide();
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_pPositionTrails = @object as IPositionTrails;
        }

        private void TrailsSetCtrl_Load(object sender, EventArgs e)
        {
            if (this.m_pPositionTrails != null)
            {
                IStyleGalleryItem item;
                this.chkLineTrail.Checked = this.m_pPositionTrails.ShowLinearTrail;
                this.chkPointTrail.Checked = this.m_pPositionTrails.ShowMarkerTrails;
                this.btnLineSymbol.Style = this.m_pPositionTrails.LinearTrailSymbol;
                this.btnPointSymbol.Style = this.m_pPositionTrails.MarkerTrailSymbol;
                this.txtDistance.Text = this.m_pPositionTrails.MarkerTrailDistance.ToString();
                this.txtPointNum.Text = this.m_pPositionTrails.MarkerTrailCount.ToString();
                this.txtLineLength.Text = this.m_pPositionTrails.LinearTrailDistance.ToString();
                if (this.m_pPositionTrails.MarkerTrailColorRamp != null)
                {
                    item = new ServerStyleGalleryItemClass {
                        Item = this.m_pPositionTrails.MarkerTrailColorRamp
                    };
                    this.cboSpeedColorRamp.Add(item);
                }
                if (this.m_pSG != null)
                {
                    IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Color Ramps", "", "");
                    item2.Reset();
                    for (item = item2.Next(); item != null; item = item2.Next())
                    {
                        this.cboSpeedColorRamp.Add(item);
                    }
                    item2 = null;
                    GC.Collect();
                }
                if (this.cboSpeedColorRamp.Items.Count > 0)
                {
                    this.cboSpeedColorRamp.SelectedIndex = 0;
                }
                else
                {
                    this.cboSpeedColorRamp.Enabled = false;
                }
                this.m_CanDo = true;
            }
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void txtLineLength_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void txtPointNum_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void ValueChange()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int BaseClass.IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int BaseClass.IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

