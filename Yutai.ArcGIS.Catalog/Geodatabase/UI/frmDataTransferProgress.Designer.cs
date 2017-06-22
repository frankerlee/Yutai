using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmDataTransferProgress
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataTransferProgress));
            this.lblObjectClass = new Label();
            this.label2 = new Label();
            this.progressBarObjectClass = new ProgressBar();
            this.lblObject = new Label();
            this.progressBarObject = new ProgressBar();
            base.SuspendLayout();
            this.lblObjectClass.Location = new Point(80, 8);
            this.lblObjectClass.Name = "lblObjectClass";
            this.lblObjectClass.Size = new Size(328, 16);
            this.lblObjectClass.TabIndex = 0;
            this.label2.Location = new Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "总进程";
            this.progressBarObjectClass.Location = new Point(80, 48);
            this.progressBarObjectClass.Name = "progressBarObjectClass";
            this.progressBarObjectClass.Size = new Size(344, 24);
            this.progressBarObjectClass.TabIndex = 2;
            this.lblObject.Location = new Point(24, 80);
            this.lblObject.Name = "lblObject";
            this.lblObject.Size = new Size(408, 16);
            this.lblObject.TabIndex = 3;
            this.progressBarObject.Location = new Point(24, 112);
            this.progressBarObject.Name = "progressBarObject";
            this.progressBarObject.Size = new Size(408, 24);
            this.progressBarObject.TabIndex = 4;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(448, 157);
            base.Controls.Add(this.progressBarObject);
            base.Controls.Add(this.lblObject);
            base.Controls.Add(this.progressBarObjectClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblObjectClass);
            
            base.Name = "frmDataTransferProgress";
            this.Text = "数据传送进程";
            base.Load += new EventHandler(this.frmDataTransferProgress_Load);
            base.ResumeLayout(false);
        }

       
        private Label label2;
        private Label lblObject;
        private Label lblObjectClass;
        private ProgressBar progressBarObject;
        private ProgressBar progressBarObjectClass;
    }
}