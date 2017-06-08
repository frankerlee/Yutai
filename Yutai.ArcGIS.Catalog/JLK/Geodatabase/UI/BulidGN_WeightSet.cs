namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class BulidGN_WeightSet : UserControl
    {
        private ComboBoxEdit comboBoxEdit1;
        private Container container_0 = null;
        private Label label1;

        public BulidGN_WeightSet()
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
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.comboBoxEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x80, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "已有的网络权重";
            this.comboBoxEdit1.EditValue = "comboBoxEdit1";
            this.comboBoxEdit1.Location = new Point(0x10, 40);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit1.Size = new Size(240, 0x17);
            this.comboBoxEdit1.TabIndex = 1;
            base.Controls.Add(this.comboBoxEdit1);
            base.Controls.Add(this.label1);
            base.Name = "BulidGN_WeightSet";
            base.Size = new Size(0x120, 0x108);
            this.comboBoxEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }
    }
}

