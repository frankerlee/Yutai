using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    internal class ServerObjectDocumentPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnOpenDocment;
        private ComboBoxEdit cboDirectory;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private string string_0 = "Started";
        private string string_1 = "";
        private TextEdit txtDataFrame;
        private MemoEdit txtDescription;
        private TextEdit txtDocument;
        private TextEdit txtPictureType;
        private TextEdit txtVirtualDirectory;

        public ServerObjectDocumentPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                this.iserverObjectConfiguration_0.Properties.SetProperty("FilePath", this.txtDocument.Text);
                this.bool_0 = false;
            }
        }

        private void btnOpenDocment_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "地图文档 (*.mxd;*.pmf)|*.mxd;*.pmf"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtDocument.Text = dialog.FileName;
                this.string_1 = this.txtDocument.Text;
                this.bool_0 = true;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerObjectDocumentPropertyPage));
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtDescription = new MemoEdit();
            this.cboDirectory = new ComboBoxEdit();
            this.txtPictureType = new TextEdit();
            this.txtVirtualDirectory = new TextEdit();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtDocument = new TextEdit();
            this.txtDataFrame = new TextEdit();
            this.btnOpenDocment = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtDescription.Properties.BeginInit();
            this.cboDirectory.Properties.BeginInit();
            this.txtPictureType.Properties.BeginInit();
            this.txtVirtualDirectory.Properties.BeginInit();
            this.txtDocument.Properties.BeginInit();
            this.txtDataFrame.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "地图文档:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据框:";
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.cboDirectory);
            this.groupBox1.Controls.Add(this.txtPictureType);
            this.groupBox1.Controls.Add(this.txtVirtualDirectory);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new Point(8, 0x58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 200);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择输出目录";
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(0x48, 0x60);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtDescription.Properties.Appearance.Options.UseBackColor = true;
            this.txtDescription.Properties.ReadOnly = true;
            this.txtDescription.Size = new Size(0xd0, 0x30);
            this.txtDescription.TabIndex = 8;
            this.cboDirectory.EditValue = "none";
            this.cboDirectory.Location = new Point(0x48, 0x18);
            this.cboDirectory.Name = "cboDirectory";
            this.cboDirectory.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDirectory.Properties.Items.AddRange(new object[] { "none" });
            this.cboDirectory.Size = new Size(0xd0, 0x17);
            this.cboDirectory.TabIndex = 7;
            this.txtPictureType.EditValue = "MIME Only";
            this.txtPictureType.Location = new Point(0x68, 0x98);
            this.txtPictureType.Name = "txtPictureType";
            this.txtPictureType.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtPictureType.Properties.Appearance.Options.UseBackColor = true;
            this.txtPictureType.Properties.ReadOnly = true;
            this.txtPictureType.Size = new Size(0xb0, 0x17);
            this.txtPictureType.TabIndex = 6;
            this.txtVirtualDirectory.EditValue = "";
            this.txtVirtualDirectory.Location = new Point(0x48, 0x38);
            this.txtVirtualDirectory.Name = "txtVirtualDirectory";
            this.txtVirtualDirectory.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtVirtualDirectory.Properties.Appearance.Options.UseBackColor = true;
            this.txtVirtualDirectory.Properties.ReadOnly = true;
            this.txtVirtualDirectory.Size = new Size(0xd0, 0x17);
            this.txtVirtualDirectory.TabIndex = 5;
            this.txtVirtualDirectory.EditValueChanged += new EventHandler(this.txtVirtualDirectory_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 160);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x61, 0x11);
            this.label6.TabIndex = 4;
            this.label6.Text = "支持的图片类型:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x51);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 3;
            this.label5.Text = "描述:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x38);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "虚拟目录:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "目录:";
            this.txtDocument.EditValue = "";
            this.txtDocument.Location = new Point(80, 0x10);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new Size(0xc0, 0x17);
            this.txtDocument.TabIndex = 3;
            this.txtDocument.EditValueChanged += new EventHandler(this.txtDocument_EditValueChanged);
            this.txtDataFrame.EditValue = "Active Data Frame";
            this.txtDataFrame.Location = new Point(80, 0x38);
            this.txtDataFrame.Name = "txtDataFrame";
            this.txtDataFrame.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtDataFrame.Properties.Appearance.Options.UseBackColor = true;
            this.txtDataFrame.Properties.ReadOnly = true;
            this.txtDataFrame.Size = new Size(0xc0, 0x17);
            this.txtDataFrame.TabIndex = 4;
            this.btnOpenDocment.Image = (Image) resources.GetObject("btnOpenDocment.Image");
            this.btnOpenDocment.Location = new Point(0x120, 0x10);
            this.btnOpenDocment.Name = "btnOpenDocment";
            this.btnOpenDocment.Size = new Size(0x20, 0x18);
            this.btnOpenDocment.TabIndex = 5;
            this.btnOpenDocment.Click += new EventHandler(this.btnOpenDocment_Click);
            base.Controls.Add(this.btnOpenDocment);
            base.Controls.Add(this.txtDataFrame);
            base.Controls.Add(this.txtDocument);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ServerObjectDocumentPropertyPage";
            base.Size = new Size(0x170, 0x130);
            base.Load += new EventHandler(this.ServerObjectDocumentPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtDescription.Properties.EndInit();
            this.cboDirectory.Properties.EndInit();
            this.txtPictureType.Properties.EndInit();
            this.txtVirtualDirectory.Properties.EndInit();
            this.txtDocument.Properties.EndInit();
            this.txtDataFrame.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            object obj2;
            object obj3;
            IPropertySet properties = this.iserverObjectConfiguration_0.Properties;
            properties.GetAllProperties(out obj2, out obj3);
            this.txtDocument.Text = properties.GetProperty("FilePath").ToString();
            if (this.string_0 == "Stoped")
            {
                this.txtDocument.Properties.ReadOnly = false;
                this.btnOpenDocment.Enabled = true;
            }
            else
            {
                this.txtDocument.Properties.ReadOnly = true;
                this.btnOpenDocment.Enabled = false;
            }
        }

        private void ServerObjectDocumentPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_1 = true;
        }

        private void txtDocument_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.string_1 = this.txtDocument.Text;
                this.bool_0 = true;
            }
        }

        private void txtVirtualDirectory_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IAGSServerConnectionAdmin AGSConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }

        public string Docunment
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

        public IServerObjectConfiguration ServerObjectConfiguration
        {
            get
            {
                return this.iserverObjectConfiguration_0;
            }
            set
            {
                this.iserverObjectConfiguration_0 = value;
            }
        }

        public string Status
        {
            set
            {
                this.string_0 = value;
            }
        }
    }
}

