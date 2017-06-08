using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class CustomOverlayGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnSelect;
        private ComboBoxEdit cboLabelField;
        private Container components = null;
        private Label label1;
        private Label label2;
        private bool m_IsPageDirty = false;
        private TextEdit txtFC;

        public event OnValueChangeEventHandler OnValueChange;

        public CustomOverlayGridPropertyPage()
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

        public void Cancel()
        {
        }

        private void CustomOverlayGridPropertyPage_Load(object sender, EventArgs e)
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
            this.label2 = new Label();
            this.btnSelect = new SimpleButton();
            this.txtFC = new TextEdit();
            this.cboLabelField = new ComboBoxEdit();
            this.txtFC.Properties.BeginInit();
            this.cboLabelField.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据源:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "标注字段:";
            this.btnSelect.Location = new Point(0xd8, 8);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x40, 0x18);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "选择";
            this.txtFC.EditValue = "";
            this.txtFC.Location = new Point(80, 8);
            this.txtFC.Name = "txtFC";
            this.txtFC.Size = new Size(0x70, 0x17);
            this.txtFC.TabIndex = 3;
            this.cboLabelField.EditValue = "";
            this.cboLabelField.Location = new Point(80, 0x30);
            this.cboLabelField.Name = "cboLabelField";
            this.cboLabelField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelField.Size = new Size(0x98, 0x17);
            this.cboLabelField.TabIndex = 4;
            base.Controls.Add(this.cboLabelField);
            base.Controls.Add(this.txtFC);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "CustomOverlayGridPropertyPage";
            base.Size = new Size(0x158, 0x120);
            base.Load += new EventHandler(this.CustomOverlayGridPropertyPage_Load);
            this.txtFC.Properties.EndInit();
            this.cboLabelField.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public void SetObjects(object @object)
        {
        }

        public int Height
        {
            get
            {
                return 0;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        public string Title
        {
            get
            {
                return "定制覆盖";
            }
            set
            {
            }
        }

        public int Width
        {
            get
            {
                return 0;
            }
        }
    }
}

