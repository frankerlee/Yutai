using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class MeasureCoordinatePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnCoordinate;
        private Container components = null;
        private Label label1;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "坐标系统";
        private RadioGroup radioGroup;

        public event OnValueChangeEventHandler OnValueChange;

        public MeasureCoordinatePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
            }
        }

        private void btnCoordinate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("需要修改!");
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
            this.label1 = new Label();
            this.radioGroup = new RadioGroup();
            this.btnCoordinate = new SimpleButton();
            this.radioGroup.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x48, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用数据框的当前系统";
            this.radioGroup.Location = new Point(0x18, 40);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用数据框的当前系统"), new RadioGroupItem(null, "使用另一种坐标系统") });
            this.radioGroup.Size = new Size(0xb8, 0x38);
            this.radioGroup.TabIndex = 1;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.btnCoordinate.Location = new Point(0xb8, 0x48);
            this.btnCoordinate.Click += new EventHandler(this.btnCoordinate_Click);
            this.btnCoordinate.Name = "btnCoordinate";
            this.btnCoordinate.Size = new Size(0x40, 0x18);
            this.btnCoordinate.TabIndex = 2;
            this.btnCoordinate.Text = "属性";
            base.Controls.Add(this.btnCoordinate);
            base.Controls.Add(this.radioGroup);
            base.Controls.Add(this.label1);
            base.Name = "MeasureCoordinatePropertyPage";
            base.Size = new Size(0x100, 0xe8);
            base.Load += new EventHandler(this.MeasureCoordinatePropertyPage_Load);
            this.radioGroup.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MeasureCoordinatePropertyPage_Load(object sender, EventArgs e)
        {
            if ((GridAxisPropertyPage.m_pMapGrid as IProjectedGrid).SpatialReference != null)
            {
                this.radioGroup.SelectedIndex = 1;
            }
            else
            {
                this.radioGroup.SelectedIndex = 0;
            }
            this.m_CanDo = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.btnCoordinate.Enabled = this.radioGroup.SelectedIndex == 1;
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

