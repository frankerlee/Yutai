using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmFeatureClassToCoverage
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFeatureClassToCoverage));
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
            this.btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(402, 73);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 10;
            this.btnSelectInputFeatures.Image = (Image)resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(402, 33);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 9;
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(12, 9);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(65, 12);
            this.lblSelectObjects.TabIndex = 8;
            this.lblSelectObjects.Text = "输入要素类";
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2 });
            this.dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new Point(12, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new Size(383, 150);
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
            this.label1.Location = new Point(12, 177);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "输出Coverage";
            this.btnSelectOutLocation.Image = (Image)resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(399, 192);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(24, 24);
            this.btnSelectOutLocation.TabIndex = 14;
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(12, 192);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(381, 21);
            this.txtOutLocation.TabIndex = 13;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(438, 273);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.dataGridView1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            
            base.Name = "frmFeatureClassToCoverage";
            this.Text = "要素类转为Coverage";
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.txtOutLocation.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Column2;
        private DataGridView dataGridView1;
        private Label label1;
        private Label lblSelectObjects;
        private TextEdit txtOutLocation;
    }
}