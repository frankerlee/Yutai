using System.Drawing;

namespace Yutai.Plugins.Identifer.Query
{
    partial class frmSpatialAndAttributeQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          

            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
           
            base.ClientSize = new Size(376, 388);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSpatialAndAttributeQuery";
            this.Text = "空间属性联合查询";
            base.ResumeLayout(false);
        }

        #endregion
    }
}