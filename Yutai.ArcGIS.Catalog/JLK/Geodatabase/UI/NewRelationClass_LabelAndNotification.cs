namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class NewRelationClass_LabelAndNotification : UserControl
    {
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private RadioGroup rdoNotificationType;
        private TextEdit txtBackLabel;
        private TextEdit txtForwardLabel;

        public NewRelationClass_LabelAndNotification()
        {
            this.InitializeComponent();
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.rdoNotificationType = new RadioGroup();
            this.txtForwardLabel = new TextEdit();
            this.txtBackLabel = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.rdoNotificationType.Properties.BeginInit();
            this.txtForwardLabel.Properties.BeginInit();
            this.txtBackLabel.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "关系前向标注";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4f, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "关系后向标注";
            this.groupBox1.Controls.Add(this.rdoNotificationType);
            this.groupBox1.Location = new Point(0x10, 0x88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0x98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "消息传播方向";
            this.rdoNotificationType.Location = new Point(0x10, 0x20);
            this.rdoNotificationType.Name = "rdoNotificationType";
            this.rdoNotificationType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoNotificationType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoNotificationType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoNotificationType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "正向传播"), new RadioGroupItem(null, "反向传播"), new RadioGroupItem(null, "双向传播"), new RadioGroupItem(null, "无") });
            this.rdoNotificationType.Size = new Size(0xa8, 0x68);
            this.rdoNotificationType.TabIndex = 0;
            this.rdoNotificationType.SelectedIndexChanged += new EventHandler(this.rdoNotificationType_SelectedIndexChanged);
            this.txtForwardLabel.EditValue = "";
            this.txtForwardLabel.Location = new Point(0x10, 40);
            this.txtForwardLabel.Name = "txtForwardLabel";
            this.txtForwardLabel.Size = new Size(240, 0x17);
            this.txtForwardLabel.TabIndex = 3;
            this.txtForwardLabel.EditValueChanged += new EventHandler(this.txtForwardLabel_EditValueChanged);
            this.txtBackLabel.EditValue = "";
            this.txtBackLabel.Location = new Point(0x10, 0x60);
            this.txtBackLabel.Name = "txtBackLabel";
            this.txtBackLabel.Size = new Size(240, 0x17);
            this.txtBackLabel.TabIndex = 4;
            this.txtBackLabel.EditValueChanged += new EventHandler(this.txtBackLabel_EditValueChanged);
            base.Controls.Add(this.txtBackLabel);
            base.Controls.Add(this.txtForwardLabel);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "NewRelationClass_LabelAndNotification";
            base.Size = new Size(0x128, 0x138);
            base.Load += new EventHandler(this.NewRelationClass_LabelAndNotification_Load);
            this.groupBox1.ResumeLayout(false);
            this.rdoNotificationType.Properties.EndInit();
            this.txtForwardLabel.Properties.EndInit();
            this.txtBackLabel.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NewRelationClass_LabelAndNotification_Load(object sender, EventArgs e)
        {
            NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationNone;
            this.txtForwardLabel.Text = NewRelationClassHelper.forwardLabel;
            this.txtBackLabel.Text = NewRelationClassHelper.backwardLabel;
        }

        private void rdoNotificationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoNotificationType.SelectedIndex)
            {
                case 0:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationForward;
                    break;

                case 1:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationBackward;
                    break;

                case 2:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationBoth;
                    break;

                case 3:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationNone;
                    break;
            }
        }

        private void txtBackLabel_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.backwardLabel = this.txtBackLabel.Text;
        }

        private void txtForwardLabel_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.forwardLabel = this.txtForwardLabel.Text;
        }
    }
}

