namespace Yutai.Plugins.TableEditor.Editor
{
    partial class TableEditorGrid
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
            this.virtualGrid = new Yutai.Plugins.TableEditor.Editor.VirtualGrid();
            this.header = new Yutai.Plugins.TableEditor.Editor.Header();
            this.SuspendLayout();
            // 
            // virtualGrid
            // 
            this.virtualGrid.AppContext = null;
            this.virtualGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.virtualGrid.FeatureLayer = null;
            this.virtualGrid.Location = new System.Drawing.Point(0, 20);
            this.virtualGrid.Name = "virtualGrid";
            this.virtualGrid.RecordNum = 0;
            this.virtualGrid.Size = new System.Drawing.Size(771, 343);
            this.virtualGrid.TabIndex = 1;
            this.virtualGrid.View = null;
            this.virtualGrid.SelectFeaturesHandler += new System.EventHandler(this.virtualGrid_SelectFeaturesHandler);
            // 
            // header
            // 
            this.header.AppContext = null;
            this.header.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.ForeColor = System.Drawing.SystemColors.ControlText;
            this.header.Handler = 0;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(771, 20);
            this.header.TabIndex = 0;
            this.header.Value = "";
            this.header.View = null;
            // 
            // TableEditorGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.virtualGrid);
            this.Controls.Add(this.header);
            this.Name = "TableEditorGrid";
            this.Size = new System.Drawing.Size(771, 363);
            this.ResumeLayout(false);

        }

        #endregion

        private Header header;
        private VirtualGrid virtualGrid;
    }
}
