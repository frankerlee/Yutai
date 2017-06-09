using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class VersionPropertyCtrl : UserControl
    {
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IVersion iversion_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblCreatTime;
        private Label lblLastModifyTime;
        private PictureEdit pictureEdit1;
        private RadioGroup radioAccessType;
        private MemoEdit txtDescription;
        private TextEdit txtName;
        private TextEdit txtOwner;
        private TextEdit txtParentVersion;

        public VersionPropertyCtrl()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                this.iversion_0.Description = this.txtDescription.Text;
                this.iversion_0.Access = (esriVersionAccess) this.radioAccessType.SelectedIndex;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionPropertyCtrl));
            this.pictureEdit1 = new PictureEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioAccessType = new RadioGroup();
            this.txtName = new TextEdit();
            this.txtOwner = new TextEdit();
            this.txtParentVersion = new TextEdit();
            this.lblCreatTime = new Label();
            this.lblLastModifyTime = new Label();
            this.txtDescription = new MemoEdit();
            this.pictureEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioAccessType.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.txtOwner.Properties.BeginInit();
            this.txtParentVersion.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0x10, 8);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Size = new Size(0x12, 0x12);
            this.pictureEdit1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "所 有 者";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x36, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "父 版 本";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x62);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "创建日期";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x36, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "修改日期";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 160);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 5;
            this.label5.Text = "描述";
            this.groupBox1.Controls.Add(this.radioAccessType);
            this.groupBox1.Location = new Point(8, 0x108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0x58);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "访问权限";
            this.radioAccessType.Location = new Point(0x10, 0x10);
            this.radioAccessType.Name = "radioAccessType";
            this.radioAccessType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioAccessType.Properties.Columns = 1;
            this.radioAccessType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "私有"), new RadioGroupItem(null, "公有"), new RadioGroupItem(null, "保护") });
            this.radioAccessType.Size = new Size(0x48, 0x40);
            this.radioAccessType.TabIndex = 7;
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x48, 8);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new Size(0x98, 0x15);
            this.txtName.TabIndex = 9;
            this.txtOwner.EditValue = "";
            this.txtOwner.Location = new Point(0x48, 0x26);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Properties.ReadOnly = true;
            this.txtOwner.Size = new Size(0x98, 0x15);
            this.txtOwner.TabIndex = 10;
            this.txtParentVersion.EditValue = "";
            this.txtParentVersion.Location = new Point(0x48, 0x44);
            this.txtParentVersion.Name = "txtParentVersion";
            this.txtParentVersion.Properties.ReadOnly = true;
            this.txtParentVersion.Size = new Size(0x98, 0x15);
            this.txtParentVersion.TabIndex = 11;
            this.lblCreatTime.Location = new Point(0x48, 0x62);
            this.lblCreatTime.Name = "lblCreatTime";
            this.lblCreatTime.Size = new Size(0x98, 0x15);
            this.lblCreatTime.TabIndex = 12;
            this.lblLastModifyTime.Location = new Point(0x48, 0x80);
            this.lblLastModifyTime.Name = "lblLastModifyTime";
            this.lblLastModifyTime.Size = new Size(0x98, 0x15);
            this.lblLastModifyTime.TabIndex = 13;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(8, 0xb8);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0xd0, 0x40);
            this.txtDescription.TabIndex = 14;
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.lblLastModifyTime);
            base.Controls.Add(this.lblCreatTime);
            base.Controls.Add(this.txtParentVersion);
            base.Controls.Add(this.txtOwner);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pictureEdit1);
            base.Name = "VersionPropertyCtrl";
            base.Size = new Size(0xe8, 360);
            base.Load += new EventHandler(this.VersionPropertyCtrl_Load);
            this.pictureEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioAccessType.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.txtOwner.Properties.EndInit();
            this.txtParentVersion.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            string[] strArray = this.iversion_0.VersionName.Split(new char[] { '.' });
            this.txtName.Text = strArray[1];
            this.txtOwner.Text = strArray[0];
            IVersionInfo parent = null;
            try
            {
                parent = this.iversion_0.VersionInfo.Parent;
            }
            catch
            {
            }
            if (parent != null)
            {
                this.txtParentVersion.Text = parent.VersionName;
            }
            else
            {
                this.txtParentVersion.Text = "";
            }
            this.lblCreatTime.Text = this.iversion_0.VersionInfo.Created.ToString();
            this.lblLastModifyTime.Text = this.iversion_0.VersionInfo.Modified.ToString();
            this.txtDescription.Text = this.iversion_0.Description;
            this.radioAccessType.SelectedIndex = (int) this.iversion_0.Access;
        }

        private void VersionPropertyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IVersion Version
        {
            set
            {
                this.iversion_0 = value;
            }
        }
    }
}

