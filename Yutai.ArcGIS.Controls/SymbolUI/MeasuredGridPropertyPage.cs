using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class MeasuredGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private ComboBoxEdit cboMapUnit;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "间隔";
        private RadioGroup rdoOriginType;
        private TextEdit txtOriginX;
        private TextEdit txtOriginY;
        private TextEdit txtXSpace;
        private TextEdit txtYSpace;

        public event OnValueChangeEventHandler OnValueChange;

        public MeasuredGridPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).Units = (esriUnits) this.cboMapUnit.SelectedIndex;
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XIntervalSize = double.Parse(this.txtXSpace.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YIntervalSize = double.Parse(this.txtYSpace.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XOrigin = double.Parse(this.txtOriginX.Text);
                }
                catch
                {
                }
                try
                {
                    (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YOrigin = double.Parse(this.txtOriginY.Text);
                }
                catch
                {
                }
                (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).FixedOrigin = this.rdoOriginType.SelectedIndex == 1;
            }
        }

        public void Cancel()
        {
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
            this.cboMapUnit = new ComboBoxEdit();
            this.txtYSpace = new TextEdit();
            this.txtXSpace = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.txtOriginY = new TextEdit();
            this.txtOriginX = new TextEdit();
            this.label4 = new Label();
            this.label5 = new Label();
            this.rdoOriginType = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.cboMapUnit.Properties.BeginInit();
            this.txtYSpace.Properties.BeginInit();
            this.txtXSpace.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtOriginY.Properties.BeginInit();
            this.txtOriginX.Properties.BeginInit();
            this.rdoOriginType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboMapUnit);
            this.groupBox1.Controls.Add(this.txtYSpace);
            this.groupBox1.Controls.Add(this.txtXSpace);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x100, 0x80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "间隔";
            this.cboMapUnit.EditValue = "未知单位";
            this.cboMapUnit.Location = new Point(0x58, 0x18);
            this.cboMapUnit.Name = "cboMapUnit";
            this.cboMapUnit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboMapUnit.Properties.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "度", "分米" });
            this.cboMapUnit.Size = new Size(0x90, 0x17);
            this.cboMapUnit.TabIndex = 6;
            this.txtYSpace.EditValue = "";
            this.txtYSpace.Location = new Point(0x58, 0x58);
            this.txtYSpace.Name = "txtYSpace";
            this.txtYSpace.Size = new Size(0x88, 0x17);
            this.txtYSpace.TabIndex = 5;
            this.txtYSpace.EditValueChanged += new EventHandler(this.txtYSpace_EditValueChanged);
            this.txtXSpace.EditValue = "";
            this.txtXSpace.Location = new Point(0x58, 0x38);
            this.txtXSpace.Name = "txtXSpace";
            this.txtXSpace.Size = new Size(0x88, 0x17);
            this.txtXSpace.TabIndex = 4;
            this.txtXSpace.EditValueChanged += new EventHandler(this.txtXSpace_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x60);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 0x11);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y轴间隔:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "X轴间隔:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "单位:";
            this.groupBox2.Controls.Add(this.txtOriginY);
            this.groupBox2.Controls.Add(this.txtOriginX);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rdoOriginType);
            this.groupBox2.Location = new Point(0x10, 0x98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x100, 0x90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "原点";
            this.txtOriginY.EditValue = "";
            this.txtOriginY.Location = new Point(0x48, 0x68);
            this.txtOriginY.Name = "txtOriginY";
            this.txtOriginY.Size = new Size(0x88, 0x17);
            this.txtOriginY.TabIndex = 9;
            this.txtOriginY.EditValueChanged += new EventHandler(this.txtOriginY_EditValueChanged);
            this.txtOriginX.EditValue = "";
            this.txtOriginX.Location = new Point(0x48, 0x48);
            this.txtOriginX.Name = "txtOriginX";
            this.txtOriginX.Size = new Size(0x88, 0x17);
            this.txtOriginX.TabIndex = 8;
            this.txtOriginX.EditValueChanged += new EventHandler(this.txtOriginX_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x68);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2a, 0x11);
            this.label4.TabIndex = 7;
            this.label4.Text = "Y原点:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2a, 0x11);
            this.label5.TabIndex = 6;
            this.label5.Text = "X原点:";
            this.rdoOriginType.Location = new Point(0x10, 0x11);
            this.rdoOriginType.Name = "rdoOriginType";
            this.rdoOriginType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoOriginType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoOriginType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoOriginType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用当前坐标系的原点"), new RadioGroupItem(null, "定义自己的原点") });
            this.rdoOriginType.Size = new Size(0xb0, 0x2f);
            this.rdoOriginType.TabIndex = 0;
            this.rdoOriginType.SelectedIndexChanged += new EventHandler(this.rdoOriginType_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MeasuredGridPropertyPage";
            base.Size = new Size(0x128, 0x138);
            base.Load += new EventHandler(this.MeasuredGridPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboMapUnit.Properties.EndInit();
            this.txtYSpace.Properties.EndInit();
            this.txtXSpace.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtOriginY.Properties.EndInit();
            this.txtOriginX.Properties.EndInit();
            this.rdoOriginType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MeasuredGridPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid is IMeasuredGrid)
            {
                this.txtXSpace.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XIntervalSize.ToString();
                this.txtYSpace.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YIntervalSize.ToString();
                this.cboMapUnit.SelectedIndex = (int) (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).Units;
                this.txtOriginX.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).XOrigin.ToString();
                this.txtOriginY.Text = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).YOrigin.ToString();
                this.rdoOriginType.SelectedIndex = (GridAxisPropertyPage.m_pMapGrid as IMeasuredGrid).FixedOrigin ? 1 : 0;
                this.m_CanDo = true;
            }
        }

        private void rdoOriginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtOriginX.Enabled = this.rdoOriginType.SelectedIndex == 1;
            this.txtOriginY.Enabled = this.rdoOriginType.SelectedIndex == 1;
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
        }

        private void txtOriginX_EditValueChanged(object sender, EventArgs e)
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

        private void txtOriginY_EditValueChanged(object sender, EventArgs e)
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

        private void txtXSpace_EditValueChanged(object sender, EventArgs e)
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

        private void txtYSpace_EditValueChanged(object sender, EventArgs e)
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
                this.m_Title = value;
            }
        }
    }
}

