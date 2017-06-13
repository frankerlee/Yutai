namespace Yutai.Plugins.Editor.Forms
{
    partial class frmLinePointProperties
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
            this.chkStart = new DevExpress.XtraEditors.CheckEdit();
            this.chkVertex = new DevExpress.XtraEditors.CheckEdit();
            this.chkEnd = new DevExpress.XtraEditors.CheckEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVertex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkStart
            // 
            this.chkStart.Location = new System.Drawing.Point(26, 24);
            this.chkStart.Name = "chkStart";
            this.chkStart.Properties.Caption = "起点";
            this.chkStart.Size = new System.Drawing.Size(75, 19);
            this.chkStart.TabIndex = 0;
            // 
            // chkVertex
            // 
            this.chkVertex.Location = new System.Drawing.Point(132, 24);
            this.chkVertex.Name = "chkVertex";
            this.chkVertex.Properties.Caption = "端点";
            this.chkVertex.Size = new System.Drawing.Size(75, 19);
            this.chkVertex.TabIndex = 1;
            // 
            // chkEnd
            // 
            this.chkEnd.Location = new System.Drawing.Point(80, 24);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Properties.Caption = "终点";
            this.chkEnd.Size = new System.Drawing.Size(46, 19);
            this.chkEnd.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(47, 78);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(132, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            // 
            // frmLinePointProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 116);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkEnd);
            this.Controls.Add(this.chkVertex);
            this.Controls.Add(this.chkStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLinePointProperties";
            this.Text = "端点选项";
            this.Load += new System.EventHandler(this.frmLinePointProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVertex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkStart;
        private DevExpress.XtraEditors.CheckEdit chkVertex;
        private DevExpress.XtraEditors.CheckEdit chkEnd;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}