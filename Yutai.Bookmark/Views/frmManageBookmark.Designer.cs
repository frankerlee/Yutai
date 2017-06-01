namespace Yutai.Plugins.Bookmark.Views
{
    partial class frmManageBookmark
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
            this.lstBookmarks = new System.Windows.Forms.ListView();
            this.btnZoomTo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBookmarks
            // 
            this.lstBookmarks.Location = new System.Drawing.Point(12, 12);
            this.lstBookmarks.Name = "lstBookmarks";
            this.lstBookmarks.Size = new System.Drawing.Size(312, 237);
            this.lstBookmarks.TabIndex = 0;
            this.lstBookmarks.UseCompatibleStateImageBehavior = false;
            this.lstBookmarks.SelectedIndexChanged += new System.EventHandler(this.lstBookmarks_SelectedIndexChanged);
            // 
            // btnZoomTo
            // 
            this.btnZoomTo.Location = new System.Drawing.Point(330, 12);
            this.btnZoomTo.Name = "btnZoomTo";
            this.btnZoomTo.Size = new System.Drawing.Size(75, 23);
            this.btnZoomTo.TabIndex = 1;
            this.btnZoomTo.Text = "缩放到";
            this.btnZoomTo.UseVisualStyleBackColor = true;
            this.btnZoomTo.Click += new System.EventHandler(this.btnZoomTo_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(330, 41);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(330, 70);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 1;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(330, 226);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmManageBookmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(417, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnZoomTo);
            this.Controls.Add(this.lstBookmarks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageBookmark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理书签";
            this.Load += new System.EventHandler(this.frmManageBookmark_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstBookmarks;
        private System.Windows.Forms.Button btnZoomTo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnClose;
    }
}