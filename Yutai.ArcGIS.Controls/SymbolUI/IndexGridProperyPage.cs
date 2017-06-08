using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class IndexGridProperyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private string m_Title = "索引";
        private SpinEdit txtColumnCount;
        private SpinEdit txtRowCount;

        public event OnValueChangeEventHandler OnValueChange;

        public IndexGridProperyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).ColumnCount = (int) this.txtColumnCount.Value;
                (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).RowCount = (int) this.txtRowCount.Value;
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

        private void IndexGridProperyPage_Load(object sender, EventArgs e)
        {
            this.txtColumnCount.Value = (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).ColumnCount;
            this.txtRowCount.Value = (GridAxisPropertyPage.m_pMapGrid as IIndexGrid).RowCount;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtColumnCount = new SpinEdit();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.txtRowCount = new SpinEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtColumnCount.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtRowCount.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtColumnCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xc0, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列";
            int[] bits = new int[4];
            this.txtColumnCount.EditValue = new decimal(bits);
            this.txtColumnCount.Location = new Point(0x38, 0x18);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtColumnCount.Size = new Size(80, 0x17);
            this.txtColumnCount.TabIndex = 1;
            this.txtColumnCount.EditValueChanged += new EventHandler(this.txtColumnCount_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "列数";
            this.groupBox2.Controls.Add(this.txtRowCount);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(0x10, 0x68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc0, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "行";
            bits = new int[4];
            this.txtRowCount.EditValue = new decimal(bits);
            this.txtRowCount.Location = new Point(0x38, 0x18);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtRowCount.Properties.UseCtrlIncrement = false;
            this.txtRowCount.Size = new Size(80, 0x17);
            this.txtRowCount.TabIndex = 1;
            this.txtRowCount.EditValueChanged += new EventHandler(this.txtRowCount_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "行数";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IndexGridProperyPage";
            base.Size = new Size(0x138, 0x100);
            base.Load += new EventHandler(this.IndexGridProperyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtColumnCount.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtRowCount.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public void SetObjects(object @object)
        {
        }

        private void txtColumnCount_EditValueChanged(object sender, EventArgs e)
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

        private void txtRowCount_EditValueChanged(object sender, EventArgs e)
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

