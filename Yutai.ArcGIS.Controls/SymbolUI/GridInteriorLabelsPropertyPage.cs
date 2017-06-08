using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class GridInteriorLabelsPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private CheckEdit chkShowInteriorLabels;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "内部标注";
        private TextEdit txtHatchIntervalXDegree;
        private TextEdit txtHatchIntervalXMinute;
        private TextEdit txtHatchIntervalXSecond;
        private TextEdit txtHatchIntervalYDegree;
        private TextEdit txtHatchIntervalYMinute;
        private TextEdit txtHatchIntervalYSecond;

        public event OnValueChangeEventHandler OnValueChange;

        public GridInteriorLabelsPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                IGridInteriorLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridInteriorLabels;
                pMapGrid.ShowInteriorLabels = this.chkShowInteriorLabels.Checked;
                double num = double.Parse(this.txtHatchIntervalXDegree.Text);
                double num2 = double.Parse(this.txtHatchIntervalXMinute.Text);
                double num3 = double.Parse(this.txtHatchIntervalXSecond.Text);
                pMapGrid.InteriorLabelIntervalX = (num + (num2 / 60.0)) + (num3 / 3600.0);
                num = double.Parse(this.txtHatchIntervalYDegree.Text);
                num2 = double.Parse(this.txtHatchIntervalYMinute.Text);
                num3 = double.Parse(this.txtHatchIntervalYSecond.Text);
                pMapGrid.InteriorLabelIntervalY = (num + (num2 / 60.0)) + (num3 / 3600.0);
            }
        }

        public void Cancel()
        {
        }

        private void chkShowInteriorLabels_CheckedChanged(object sender, EventArgs e)
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

        public void DegreeToDMS(double Degree, out double d, out double m, out double s)
        {
            int num = Math.Sign(Degree);
            Degree = Math.Abs(Degree);
            d = Math.Floor(Degree);
            Degree = (Degree - d) * 60.0;
            m = Math.Floor(Degree);
            s = (Degree - m) * 60.0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GridInteriorLabelsPropertyPage_Load(object sender, EventArgs e)
        {
            double num;
            double num2;
            double num3;
            IGridInteriorLabels pMapGrid = GridAxisPropertyPage.m_pMapGrid as IGridInteriorLabels;
            this.chkShowInteriorLabels.Checked = pMapGrid.ShowInteriorLabels;
            this.DegreeToDMS(pMapGrid.InteriorLabelIntervalX, out num, out num2, out num3);
            this.txtHatchIntervalXDegree.Text = num.ToString();
            this.txtHatchIntervalXMinute.Text = num2.ToString();
            this.txtHatchIntervalXSecond.Text = num3.ToString();
            this.DegreeToDMS(pMapGrid.InteriorLabelIntervalY, out num, out num2, out num3);
            this.txtHatchIntervalYDegree.Text = num.ToString();
            this.txtHatchIntervalYMinute.Text = num2.ToString();
            this.txtHatchIntervalYSecond.Text = num3.ToString();
            this.m_CanDo = true;
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.chkShowInteriorLabels = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.label9 = new Label();
            this.label8 = new Label();
            this.label7 = new Label();
            this.txtHatchIntervalYSecond = new TextEdit();
            this.txtHatchIntervalYMinute = new TextEdit();
            this.txtHatchIntervalXSecond = new TextEdit();
            this.txtHatchIntervalXMinute = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtHatchIntervalYDegree = new TextEdit();
            this.txtHatchIntervalXDegree = new TextEdit();
            this.label5 = new Label();
            this.label6 = new Label();
            this.chkShowInteriorLabels.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtHatchIntervalYSecond.Properties.BeginInit();
            this.txtHatchIntervalYMinute.Properties.BeginInit();
            this.txtHatchIntervalXSecond.Properties.BeginInit();
            this.txtHatchIntervalXMinute.Properties.BeginInit();
            this.txtHatchIntervalYDegree.Properties.BeginInit();
            this.txtHatchIntervalXDegree.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowInteriorLabels.Location = new Point(0x18, 0x10);
            this.chkShowInteriorLabels.Name = "chkShowInteriorLabels";
            this.chkShowInteriorLabels.Properties.Caption = "显示内部格网标注";
            this.chkShowInteriorLabels.Size = new Size(0x90, 0x13);
            this.chkShowInteriorLabels.TabIndex = 0;
            this.chkShowInteriorLabels.CheckedChanged += new EventHandler(this.chkShowInteriorLabels_CheckedChanged);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYMinute);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXSecond);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXMinute);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHatchIntervalYDegree);
            this.groupBox1.Controls.Add(this.txtHatchIntervalXDegree);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(0x18, 0x48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xf8, 0x80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "内部格网标注间隔";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0xb0, 0x18);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 0x11);
            this.label9.TabIndex = 0x19;
            this.label9.Text = "秒";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x88, 0x18);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 0x18;
            this.label8.Text = "分";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x60, 0x18);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 0x11);
            this.label7.TabIndex = 0x17;
            this.label7.Text = "度";
            this.txtHatchIntervalYSecond.EditValue = "";
            this.txtHatchIntervalYSecond.Location = new Point(160, 80);
            this.txtHatchIntervalYSecond.Name = "txtHatchIntervalYSecond";
            this.txtHatchIntervalYSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYSecond.TabIndex = 0x16;
            this.txtHatchIntervalYSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalYSecond_EditValueChanged);
            this.txtHatchIntervalYMinute.EditValue = "";
            this.txtHatchIntervalYMinute.Location = new Point(120, 80);
            this.txtHatchIntervalYMinute.Name = "txtHatchIntervalYMinute";
            this.txtHatchIntervalYMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalYMinute.TabIndex = 0x15;
            this.txtHatchIntervalYMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalYMinute_EditValueChanged);
            this.txtHatchIntervalXSecond.EditValue = "";
            this.txtHatchIntervalXSecond.Location = new Point(160, 0x30);
            this.txtHatchIntervalXSecond.Name = "txtHatchIntervalXSecond";
            this.txtHatchIntervalXSecond.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXSecond.TabIndex = 20;
            this.txtHatchIntervalXSecond.EditValueChanged += new EventHandler(this.txtHatchIntervalXSecond_EditValueChanged);
            this.txtHatchIntervalXMinute.EditValue = "";
            this.txtHatchIntervalXMinute.Location = new Point(120, 0x30);
            this.txtHatchIntervalXMinute.Name = "txtHatchIntervalXMinute";
            this.txtHatchIntervalXMinute.Size = new Size(0x20, 0x17);
            this.txtHatchIntervalXMinute.TabIndex = 0x13;
            this.txtHatchIntervalXMinute.EditValueChanged += new EventHandler(this.txtHatchIntervalXMinute_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(160, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x11);
            this.label3.TabIndex = 0x12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(160, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0, 0x11);
            this.label4.TabIndex = 0x11;
            this.txtHatchIntervalYDegree.EditValue = "";
            this.txtHatchIntervalYDegree.Location = new Point(0x40, 80);
            this.txtHatchIntervalYDegree.Name = "txtHatchIntervalYDegree";
            this.txtHatchIntervalYDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalYDegree.TabIndex = 0x10;
            this.txtHatchIntervalYDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalYDegree_EditValueChanged);
            this.txtHatchIntervalXDegree.EditValue = "";
            this.txtHatchIntervalXDegree.Location = new Point(0x40, 0x30);
            this.txtHatchIntervalXDegree.Name = "txtHatchIntervalXDegree";
            this.txtHatchIntervalXDegree.Size = new Size(0x30, 0x17);
            this.txtHatchIntervalXDegree.TabIndex = 15;
            this.txtHatchIntervalXDegree.EditValueChanged += new EventHandler(this.txtHatchIntervalXDegree_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 80);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 14;
            this.label5.Text = "经度";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x30);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 13;
            this.label6.Text = "纬度";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.chkShowInteriorLabels);
            base.Name = "GridInteriorLabelsPropertyPage";
            base.Size = new Size(0x148, 240);
            base.Load += new EventHandler(this.GridInteriorLabelsPropertyPage_Load);
            this.chkShowInteriorLabels.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtHatchIntervalYSecond.Properties.EndInit();
            this.txtHatchIntervalYMinute.Properties.EndInit();
            this.txtHatchIntervalXSecond.Properties.EndInit();
            this.txtHatchIntervalXMinute.Properties.EndInit();
            this.txtHatchIntervalYDegree.Properties.EndInit();
            this.txtHatchIntervalXDegree.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public void SetObjects(object @object)
        {
        }

        private void txtHatchIntervalXDegree_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalXMinute_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalXSecond_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYDegree_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYMinute_EditValueChanged(object sender, EventArgs e)
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

        private void txtHatchIntervalYSecond_EditValueChanged(object sender, EventArgs e)
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
                return this.m_Title;
            }
            set
            {
            }
        }
    }
}

