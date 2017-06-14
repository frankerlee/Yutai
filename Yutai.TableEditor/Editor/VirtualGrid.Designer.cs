namespace Yutai.Plugins.TableEditor.Editor
{
    partial class VirtualGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewAll = new System.Windows.Forms.DataGridView();
            this.dataGridViewSelected = new System.Windows.Forms.DataGridView();
            this.navigationBar = new Yutai.Plugins.TableEditor.Editor.NavigationBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAll
            // 
            this.dataGridViewAll.AllowUserToAddRows = false;
            this.dataGridViewAll.AllowUserToResizeRows = false;
            this.dataGridViewAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAll.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewAll.Name = "dataGridViewAll";
            this.dataGridViewAll.RowTemplate.Height = 23;
            this.dataGridViewAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAll.ShowEditingIcon = false;
            this.dataGridViewAll.Size = new System.Drawing.Size(631, 318);
            this.dataGridViewAll.TabIndex = 0;
            this.dataGridViewAll.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAll_ColumnHeaderMouseClick);
            this.dataGridViewAll.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridViewAll.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewAll_DataBindingComplete);
            // 
            // dataGridViewSelected
            // 
            this.dataGridViewSelected.AllowUserToAddRows = false;
            this.dataGridViewSelected.AllowUserToResizeRows = false;
            this.dataGridViewSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSelected.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSelected.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSelected.Name = "dataGridViewSelected";
            this.dataGridViewSelected.RowTemplate.Height = 23;
            this.dataGridViewSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSelected.ShowEditingIcon = false;
            this.dataGridViewSelected.Size = new System.Drawing.Size(631, 318);
            this.dataGridViewSelected.TabIndex = 0;
            this.dataGridViewSelected.CurrentCellChanged += new System.EventHandler(this.dataGridViewSelected_CurrentCellChanged);
            this.dataGridViewSelected.SelectionChanged += new System.EventHandler(this.dataGridViewSelected_SelectionChanged);
            // 
            // navigationBar
            // 
            this.navigationBar.AutoSize = true;
            this.navigationBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navigationBar.Location = new System.Drawing.Point(0, 318);
            this.navigationBar.Name = "navigationBar";
            this.navigationBar.Size = new System.Drawing.Size(631, 31);
            this.navigationBar.TabIndex = 1;
            this.navigationBar.SwitchTableEventHandler += new Yutai.Plugins.TableEditor.Editor.TableEditEventHandler(this.navigationBar_SwitchTableEventHandler);
            // 
            // VirtualGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewAll);
            this.Controls.Add(this.dataGridViewSelected);
            this.Controls.Add(this.navigationBar);
            this.Name = "VirtualGrid";
            this.Size = new System.Drawing.Size(631, 349);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAll;
        private System.Windows.Forms.DataGridView dataGridViewSelected;
        private NavigationBar navigationBar;
    }
}
