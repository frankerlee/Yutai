namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class NewRelationClass_RelationType : UserControl
    {
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private RadioGroup rdoRelationType;

        public NewRelationClass_RelationType()
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
            this.rdoRelationType = new RadioGroup();
            this.label1 = new Label();
            this.label2 = new Label();
            this.rdoRelationType.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoRelationType.Location = new Point(8, -8);
            this.rdoRelationType.Name = "rdoRelationType";
            this.rdoRelationType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoRelationType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRelationType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRelationType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "简单关系"), new RadioGroupItem(null, "复杂关系") });
            this.rdoRelationType.Size = new Size(240, 0xa8);
            this.rdoRelationType.TabIndex = 0;
            this.rdoRelationType.SelectedIndexChanged += new EventHandler(this.rdoRelationType_SelectedIndexChanged);
            this.label1.Location = new Point(0x18, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xf8, 0x38);
            this.label1.TabIndex = 1;
            this.label1.Text = "简单的对等关系是数据库中可以独立存在的两个或多个对象间的关系。在这种关系中源表/要素类的对象被删除了，目标表/要素类中的相关对象缺省不被删除。";
            this.label2.Location = new Point(0x18, 0x88);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xf8, 0x40);
            this.label2.TabIndex = 2;
            this.label2.Text = "在组合关系中，目标表/要素类中的对象的生命周期取决于源表/要素类中相关对象的生命周期。当源表/要素类中的对象被删除，目标表/要素类中的相关对象也被删除。";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoRelationType);
            base.Name = "NewRelationClass_RelationType";
            base.Size = new Size(0x130, 280);
            base.Load += new EventHandler(this.NewRelationClass_RelationType_Load);
            this.rdoRelationType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NewRelationClass_RelationType_Load(object sender, EventArgs e)
        {
            NewRelationClassHelper.IsComposite = false;
        }

        private void rdoRelationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoRelationType.SelectedIndex == 0)
            {
                NewRelationClassHelper.IsComposite = false;
            }
            else
            {
                NewRelationClassHelper.IsComposite = true;
            }
        }
    }
}

