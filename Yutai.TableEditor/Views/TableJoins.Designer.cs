using Yutai.Plugins.TableEditor.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    partial class TableJoins
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this._joinsGrid1 = new Yutai.Plugins.TableEditor.Controls.JoinsGrid();
            ((System.ComponentModel.ISupportInitialize)(this._joinsGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(553, 285);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 24);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnStopAll
            // 
            this.btnStopAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopAll.Location = new System.Drawing.Point(553, 71);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(81, 24);
            this.btnStopAll.TabIndex = 8;
            this.btnStopAll.Text = "停止所有";
            this.btnStopAll.UseVisualStyleBackColor = true;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(553, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(81, 24);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "停止连接";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJoin.Location = new System.Drawing.Point(553, 11);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(81, 24);
            this.btnJoin.TabIndex = 6;
            this.btnJoin.Text = "添加";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // _joinsGrid1
            // 
            this._joinsGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._joinsGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._joinsGrid1.BackgroundColor = System.Drawing.SystemColors.Window;
            this._joinsGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._joinsGrid1.Location = new System.Drawing.Point(12, 12);
            this._joinsGrid1.MultiSelect = false;
            this._joinsGrid1.Name = "_joinsGrid1";
            this._joinsGrid1.ReadOnly = true;
            this._joinsGrid1.RowTemplate.Height = 23;
            this._joinsGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._joinsGrid1.Size = new System.Drawing.Size(535, 297);
            this._joinsGrid1.TabIndex = 11;
            // 
            // TableJoins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(637, 320);
            this.Controls.Add(this._joinsGrid1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStopAll);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnJoin);
            this.Name = "TableJoins";
            this.Text = "连接表";
            ((System.ComponentModel.ISupportInitialize)(this._joinsGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStopAll;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnJoin;
        public JoinsGrid _joinsGrid1;
    }
}