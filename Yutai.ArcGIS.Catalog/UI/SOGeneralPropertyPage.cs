using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal class SOGeneralPropertyPage : UserControl
    {
        private ComboBoxEdit cboSOType;
        private ComboBoxEdit cboStartupType;
        private Container container_0 = null;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IServerObject iserverObject_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string string_0 = "";
        private string string_1 = "";
        private MemoEdit txtDescription;
        private TextEdit txtSOName;

        public SOGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.iserverObjectConfiguration_0 == null)
            {
                this.iserverObjectConfiguration_0 = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.CreateConfiguration();
            }
            this.iserverObjectConfiguration_0.Name = this.txtSOName.Text;
            this.iserverObjectConfiguration_0.TypeName = this.cboSOType.Text;
            this.iserverObjectConfiguration_0.Description = this.txtDescription.Text;
            this.iserverObjectConfiguration_0.StartupType = (esriStartupType) this.cboStartupType.SelectedIndex;
        }

        public IServerObjectConfiguration CreateServerObjectConfig()
        {
            if (this.iserverObjectConfiguration_0 == null)
            {
                this.iserverObjectConfiguration_0 = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.CreateConfiguration();
            }
            this.iserverObjectConfiguration_0.Name = this.txtSOName.Text;
            this.iserverObjectConfiguration_0.TypeName = this.cboSOType.Text;
            this.iserverObjectConfiguration_0.Description = this.txtDescription.Text;
            this.iserverObjectConfiguration_0.StartupType = (esriStartupType) this.cboStartupType.SelectedIndex;
            return this.iserverObjectConfiguration_0;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtSOName = new TextEdit();
            this.cboSOType = new ComboBoxEdit();
            this.txtDescription = new MemoEdit();
            this.cboStartupType = new ComboBoxEdit();
            this.txtSOName.Properties.BeginInit();
            this.cboSOType.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            this.cboStartupType.Properties.BeginInit();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "名字:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "描述:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0xa3);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 5;
            this.label4.Text = "启动类型:";
            this.txtSOName.EditValue = "";
            this.txtSOName.Location = new Point(0x48, 8);
            this.txtSOName.Name = "txtSOName";
            this.txtSOName.Size = new Size(0x98, 0x17);
            this.txtSOName.TabIndex = 6;
            this.txtSOName.EditValueChanged += new EventHandler(this.txtSOName_EditValueChanged);
            this.cboSOType.EditValue = "MapServer";
            this.cboSOType.Location = new Point(0x48, 40);
            this.cboSOType.Name = "cboSOType";
            this.cboSOType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSOType.Properties.Items.AddRange(new object[] { "MapServer" });
            this.cboSOType.Size = new Size(0x90, 0x17);
            this.cboSOType.TabIndex = 7;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x48, 0x48);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x88, 80);
            this.txtDescription.TabIndex = 8;
            this.cboStartupType.EditValue = "自动";
            this.cboStartupType.Location = new Point(0x48, 160);
            this.cboStartupType.Name = "cboStartupType";
            this.cboStartupType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStartupType.Properties.Items.AddRange(new object[] { "自动", "手动" });
            this.cboStartupType.Size = new Size(0xa8, 0x17);
            this.cboStartupType.TabIndex = 9;
            base.Controls.Add(this.cboStartupType);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.cboSOType);
            base.Controls.Add(this.txtSOName);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Name = "SOGeneralPropertyPage";
            base.Size = new Size(0x130, 280);
            base.Load += new EventHandler(this.SOGeneralPropertyPage_Load);
            this.txtSOName.Properties.EndInit();
            this.cboSOType.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            this.cboStartupType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void SOGeneralPropertyPage_Load(object sender, EventArgs e)
        {
        }

        private void txtSOName_EditValueChanged(object sender, EventArgs e)
        {
            this.string_0 = this.txtSOName.Text;
        }

        public IAGSServerConnection AGSServerConnection
        {
            set
            {
                this.iagsserverConnection_0 = value;
            }
        }

        public IServerObject ServerObject
        {
            get
            {
                return this.iserverObject_0;
            }
            set
            {
                this.iserverObject_0 = value;
            }
        }

        public string SOName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string SOType
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }
    }
}

