using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    partial class frmNewVerticalSectionSet
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewVerticalSectionSet));
            this.label1 = new Label();
            this.cboPipleLineLayers = new ComboBox();
            this.label2 = new Label();
            this.cboPiplePointLayers = new ComboBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cboSURFHField = new ComboBox();
            this.cboBOTTOMHField = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线层";
            this.cboPipleLineLayers.Location = new System.Drawing.Point(72, 16);
            this.cboPipleLineLayers.Name = "cboPipleLineLayers";
            this.cboPipleLineLayers.Size = new Size(152, 20);
            this.cboPipleLineLayers.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "点层";
            this.cboPiplePointLayers.Location = new System.Drawing.Point(72, 56);
            this.cboPiplePointLayers.Name = "cboPiplePointLayers";
            this.cboPiplePointLayers.Size = new Size(152, 20);
            this.cboPiplePointLayers.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 104);
            this.label3.Name = "label3";
            this.label3.Size = new Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "地面点高程字段";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 144);
            this.label4.Name = "label4";
            this.label4.Size = new Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "管底高程字段";
            this.cboSURFHField.Location = new System.Drawing.Point(112, 104);
            this.cboSURFHField.Name = "cboSURFHField";
            this.cboSURFHField.Size = new Size(112, 20);
            this.cboSURFHField.TabIndex = 6;
            this.cboBOTTOMHField.Location = new System.Drawing.Point(112, 136);
            this.cboBOTTOMHField.Name = "cboBOTTOMHField";
            this.cboBOTTOMHField.Size = new Size(112, 20);
            this.cboBOTTOMHField.TabIndex = 7;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(256, 258);
            base.Controls.Add(this.cboBOTTOMHField);
            base.Controls.Add(this.cboSURFHField);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.cboPiplePointLayers);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboPipleLineLayers);
            base.Controls.Add(this.label1);
            
            base.Name = "frmNewVerticalSectionSet";
            this.Text = "纵断面设置";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private ComboBox cboBOTTOMHField;
        private ComboBox cboPipleLineLayers;
        private ComboBox cboPiplePointLayers;
        private ComboBox cboSURFHField;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}