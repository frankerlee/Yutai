using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class frmMapGridType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapGridType));
            this.radioGroup1 = new RadioGroup();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(8, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "MGRS格网"), new RadioGroupItem(null, "经纬网"), new RadioGroupItem(null, "方格网"), new RadioGroupItem(null, "索引格网") });
            this.radioGroup1.Size = new Size(144, 128);
            this.radioGroup1.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(8, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(72, 152);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(168, 197);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.radioGroup1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMapGridType";
            this.Text = "格网类型";
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnOK;
        private RadioGroup radioGroup1;
        private SimpleButton simpleButton2;
    }
}