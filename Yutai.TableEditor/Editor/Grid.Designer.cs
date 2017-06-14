namespace Yutai.Plugins.TableEditor.Editor
{
    partial class Grid
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._navigationBar = new Yutai.Plugins.TableEditor.Editor.NavigationBar();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.AllowUserToResizeRows = false;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 0);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.RowTemplate.Height = 23;
            this._dataGridView.Size = new System.Drawing.Size(706, 463);
            this._dataGridView.TabIndex = 1;
            this._dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dataGridView_ColumnHeaderMouseClick);
            this._dataGridView.CurrentCellChanged += new System.EventHandler(this._dataGridView_CurrentCellChanged);
            // 
            // _navigationBar
            // 
            this._navigationBar.AutoSize = true;
            this._navigationBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._navigationBar.Location = new System.Drawing.Point(0, 463);
            this._navigationBar.Name = "_navigationBar";
            this._navigationBar.Size = new System.Drawing.Size(706, 31);
            this._navigationBar.TabIndex = 0;
            this._navigationBar.SwitchTableEventHandler += new Yutai.Plugins.TableEditor.Editor.TableEditEventHandler(this._navigationBar_SwitchTableEventHandler);
            // 
            // Grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this._navigationBar);
            this.Name = "Grid";
            this.Size = new System.Drawing.Size(706, 494);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NavigationBar _navigationBar;
        private System.Windows.Forms.DataGridView _dataGridView;
    }
}
