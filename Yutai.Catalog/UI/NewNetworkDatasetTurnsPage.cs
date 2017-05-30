namespace Yutai.Catalog.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewNetworkDatasetTurnsPage : UserControl
    {
        private CheckedListBox checkedListBox1;
        private IContainer icontainer_0 = null;
        private Label label1;
        private RadioButton rdoFalse;
        private RadioButton rdoTrue;

        public NewNetworkDatasetTurnsPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            NewNetworkDatasetHelper.NewNetworkDataset.SupportsTurns = this.rdoTrue.Checked;
            return true;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.rdoFalse = new RadioButton();
            this.rdoTrue = new RadioButton();
            this.checkedListBox1 = new CheckedListBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x95, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "是否为网络要素集建立转向";
            this.rdoFalse.AutoSize = true;
            this.rdoFalse.Checked = true;
            this.rdoFalse.Location = new Point(14, 40);
            this.rdoFalse.Name = "rdoFalse";
            this.rdoFalse.Size = new Size(0x23, 0x10);
            this.rdoFalse.TabIndex = 4;
            this.rdoFalse.TabStop = true;
            this.rdoFalse.Text = "否";
            this.rdoFalse.UseVisualStyleBackColor = true;
            this.rdoFalse.CheckedChanged += new EventHandler(this.rdoFalse_CheckedChanged);
            this.rdoTrue.AutoSize = true;
            this.rdoTrue.Location = new Point(14, 0x3e);
            this.rdoTrue.Name = "rdoTrue";
            this.rdoTrue.Size = new Size(0x23, 0x10);
            this.rdoTrue.TabIndex = 3;
            this.rdoTrue.Text = "是";
            this.rdoTrue.UseVisualStyleBackColor = true;
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] { "<Global Turns>" });
            this.checkedListBox1.Location = new Point(14, 0x60);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(0xde, 0x74);
            this.checkedListBox1.TabIndex = 6;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.checkedListBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoFalse);
            base.Controls.Add(this.rdoTrue);
            base.Name = "NewNetworkDatasetTurnsPage";
            base.Size = new Size(0x128, 0xf5);
            base.Load += new EventHandler(this.NewNetworkDatasetTurnsPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void NewNetworkDatasetTurnsPage_Load(object sender, EventArgs e)
        {
            this.checkedListBox1.SetItemChecked(0, true);
        }

        private void rdoFalse_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}

