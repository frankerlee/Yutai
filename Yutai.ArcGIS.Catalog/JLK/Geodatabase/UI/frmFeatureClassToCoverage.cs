namespace JLK.Geodatabase.UI
{
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class frmFeatureClassToCoverage : Form
    {
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridView dataGridView1;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label lblSelectObjects;
        private TextEdit txtOutLocation;

        public frmFeatureClassToCoverage()
        {
            this.InitializeComponent();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmFeatureClassToCoverage));
            this.btnDelete = new SimpleButton();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewComboBoxColumn();
            this.label1 = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.txtOutLocation.Properties.BeginInit();
            base.SuspendLayout();
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x192, 0x49);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 10;
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x192, 0x21);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 9;
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(12, 9);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(0x41, 12);
            this.lblSelectObjects.TabIndex = 8;
            this.lblSelectObjects.Text = "输入要素类";
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2 });
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new Point(12, 0x18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x17f, 150);
            this.dataGridView1.TabIndex = 11;
            this.Column1.HeaderText = "要素类";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 250;
            this.Column2.HeaderText = "类型";
            this.Column2.Items.AddRange(new object[] { "POINT", "LABEL", "NODE", "ARC", "ROUTE", "POLYGON", "ANNO" });
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0xb1);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "输出Coverage";
            this.btnSelectOutLocation.Image = (Image) manager.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(0x18f, 0xc0);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 14;
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(12, 0xc0);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0x17d, 0x15);
            this.txtOutLocation.TabIndex = 13;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b6, 0x111);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmFeatureClassToCoverage";
            this.Text = "要素类转为Coverage";
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.txtOutLocation.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

