namespace Yutai.Plugins.TableEditor.Editor
{
    partial class NavigationBar
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuFirst = new System.Windows.Forms.ToolStripButton();
            this.mnuPrevious = new System.Windows.Forms.ToolStripButton();
            this.mnuCurrentRecord = new System.Windows.Forms.ToolStripTextBox();
            this.mnuNext = new System.Windows.Forms.ToolStripButton();
            this.mnuLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAllRecords = new System.Windows.Forms.ToolStripButton();
            this.mnuSelectedRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecordInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFirst,
            this.mnuPrevious,
            this.mnuCurrentRecord,
            this.mnuNext,
            this.mnuLast,
            this.toolStripSeparator1,
            this.mnuAllRecords,
            this.mnuSelectedRecords,
            this.toolStripSeparator2,
            this.mnuRecordInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(627, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuFirst
            // 
            this.mnuFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuFirst.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_data_first;
            this.mnuFirst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFirst.Name = "mnuFirst";
            this.mnuFirst.Size = new System.Drawing.Size(28, 28);
            this.mnuFirst.Text = "移动到表开始处";
            this.mnuFirst.Click += new System.EventHandler(this.mnuFirst_Click);
            // 
            // mnuPrevious
            // 
            this.mnuPrevious.BackColor = System.Drawing.SystemColors.Control;
            this.mnuPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuPrevious.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_data_previous;
            this.mnuPrevious.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuPrevious.Name = "mnuPrevious";
            this.mnuPrevious.Size = new System.Drawing.Size(28, 28);
            this.mnuPrevious.Text = "移动到前一条记录";
            this.mnuPrevious.Click += new System.EventHandler(this.mnuPrevious_Click);
            // 
            // mnuCurrentRecord
            // 
            this.mnuCurrentRecord.Name = "mnuCurrentRecord";
            this.mnuCurrentRecord.Size = new System.Drawing.Size(100, 31);
            this.mnuCurrentRecord.ToolTipText = "转到特定记录";
            this.mnuCurrentRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mnuCurrentRecord_KeyDown);
            // 
            // mnuNext
            // 
            this.mnuNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuNext.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_data_next;
            this.mnuNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuNext.Name = "mnuNext";
            this.mnuNext.Size = new System.Drawing.Size(28, 28);
            this.mnuNext.Text = "移动到下一条记录";
            this.mnuNext.Click += new System.EventHandler(this.mnuNext_Click);
            // 
            // mnuLast
            // 
            this.mnuLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuLast.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_data_last;
            this.mnuLast.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuLast.Name = "mnuLast";
            this.mnuLast.Size = new System.Drawing.Size(28, 28);
            this.mnuLast.Text = "移动到表结束处";
            this.mnuLast.Click += new System.EventHandler(this.mnuLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // mnuAllRecords
            // 
            this.mnuAllRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuAllRecords.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_all_records;
            this.mnuAllRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAllRecords.Name = "mnuAllRecords";
            this.mnuAllRecords.Size = new System.Drawing.Size(23, 28);
            this.mnuAllRecords.Text = "显示所有记录";
            this.mnuAllRecords.Click += new System.EventHandler(this.mnuAllRecords_Click);
            // 
            // mnuSelectedRecords
            // 
            this.mnuSelectedRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSelectedRecords.Image = global::Yutai.Plugins.TableEditor.Properties.Resources.icon_selected_records;
            this.mnuSelectedRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSelectedRecords.Name = "mnuSelectedRecords";
            this.mnuSelectedRecords.Size = new System.Drawing.Size(23, 28);
            this.mnuSelectedRecords.Text = "显示所选记录";
            this.mnuSelectedRecords.Click += new System.EventHandler(this.mnuSelectedRecords_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // mnuRecordInfo
            // 
            this.mnuRecordInfo.Name = "mnuRecordInfo";
            this.mnuRecordInfo.Size = new System.Drawing.Size(0, 28);
            this.mnuRecordInfo.ToolTipText = "记录条数";
            // 
            // NavigationBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.toolStrip1);
            this.Name = "NavigationBar";
            this.Size = new System.Drawing.Size(627, 31);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuFirst;
        private System.Windows.Forms.ToolStripButton mnuPrevious;
        private System.Windows.Forms.ToolStripTextBox mnuCurrentRecord;
        private System.Windows.Forms.ToolStripButton mnuNext;
        private System.Windows.Forms.ToolStripButton mnuLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuAllRecords;
        private System.Windows.Forms.ToolStripButton mnuSelectedRecords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel mnuRecordInfo;
    }
}
