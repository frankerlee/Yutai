namespace Yutai.Pipeline.Editor.Controls
{
    partial class UcExtentSetting
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
            this.groupBoxExtentType = new System.Windows.Forms.GroupBox();
            this.radioGroupExtentType = new DevExpress.XtraEditors.RadioGroup();
            this.groupBoxIndexLayer = new System.Windows.Forms.GroupBox();
            this.checkedListBoxIndexes = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonSelectNull = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonByAll = new System.Windows.Forms.Button();
            this.buttonBySelection = new System.Windows.Forms.Button();
            this.buttonByView = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucSelectFeatureClass = new Yutai.Pipeline.Editor.Controls.UcSelectFeatureClass();
            this.buttonRefersh = new System.Windows.Forms.Button();
            this.groupBoxExtentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupExtentType.Properties)).BeginInit();
            this.groupBoxIndexLayer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxExtentType
            // 
            this.groupBoxExtentType.Controls.Add(this.radioGroupExtentType);
            this.groupBoxExtentType.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxExtentType.Location = new System.Drawing.Point(0, 0);
            this.groupBoxExtentType.Name = "groupBoxExtentType";
            this.groupBoxExtentType.Size = new System.Drawing.Size(252, 65);
            this.groupBoxExtentType.TabIndex = 0;
            this.groupBoxExtentType.TabStop = false;
            this.groupBoxExtentType.Text = "范围类型";
            // 
            // radioGroupExtentType
            // 
            this.radioGroupExtentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroupExtentType.EditValue = 0;
            this.radioGroupExtentType.Location = new System.Drawing.Point(3, 17);
            this.radioGroupExtentType.Name = "radioGroupExtentType";
            this.radioGroupExtentType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroupExtentType.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupExtentType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupExtentType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "全部范围"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "当前视图"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "依据索引"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "鼠标选择")});
            this.radioGroupExtentType.Size = new System.Drawing.Size(246, 45);
            this.radioGroupExtentType.TabIndex = 0;
            this.radioGroupExtentType.SelectedIndexChanged += new System.EventHandler(this.radioGroupExtentType_SelectedIndexChanged);
            // 
            // groupBoxIndexLayer
            // 
            this.groupBoxIndexLayer.Controls.Add(this.checkedListBoxIndexes);
            this.groupBoxIndexLayer.Controls.Add(this.panel2);
            this.groupBoxIndexLayer.Controls.Add(this.panel1);
            this.groupBoxIndexLayer.Controls.Add(this.splitContainer1);
            this.groupBoxIndexLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxIndexLayer.Enabled = false;
            this.groupBoxIndexLayer.Location = new System.Drawing.Point(0, 65);
            this.groupBoxIndexLayer.Name = "groupBoxIndexLayer";
            this.groupBoxIndexLayer.Size = new System.Drawing.Size(252, 327);
            this.groupBoxIndexLayer.TabIndex = 1;
            this.groupBoxIndexLayer.TabStop = false;
            this.groupBoxIndexLayer.Text = "索引图层";
            // 
            // checkedListBoxIndexes
            // 
            this.checkedListBoxIndexes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxIndexes.FormattingEnabled = true;
            this.checkedListBoxIndexes.Location = new System.Drawing.Point(3, 68);
            this.checkedListBoxIndexes.Name = "checkedListBoxIndexes";
            this.checkedListBoxIndexes.Size = new System.Drawing.Size(246, 225);
            this.checkedListBoxIndexes.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonSelectAll);
            this.panel2.Controls.Add(this.buttonSelectNull);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 293);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 31);
            this.panel2.TabIndex = 2;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(84, 3);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAll.TabIndex = 0;
            this.buttonSelectAll.Text = "全选";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // buttonSelectNull
            // 
            this.buttonSelectNull.Location = new System.Drawing.Point(165, 3);
            this.buttonSelectNull.Name = "buttonSelectNull";
            this.buttonSelectNull.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectNull.TabIndex = 0;
            this.buttonSelectNull.Text = "清空选择";
            this.buttonSelectNull.UseVisualStyleBackColor = true;
            this.buttonSelectNull.Click += new System.EventHandler(this.buttonSelectNull_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonByAll);
            this.panel1.Controls.Add(this.buttonBySelection);
            this.panel1.Controls.Add(this.buttonByView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 31);
            this.panel1.TabIndex = 1;
            // 
            // buttonByAll
            // 
            this.buttonByAll.Location = new System.Drawing.Point(165, 6);
            this.buttonByAll.Name = "buttonByAll";
            this.buttonByAll.Size = new System.Drawing.Size(75, 23);
            this.buttonByAll.TabIndex = 0;
            this.buttonByAll.Text = "全部索引";
            this.buttonByAll.UseVisualStyleBackColor = true;
            this.buttonByAll.Click += new System.EventHandler(this.buttonByAll_Click);
            // 
            // buttonBySelection
            // 
            this.buttonBySelection.Location = new System.Drawing.Point(84, 6);
            this.buttonBySelection.Name = "buttonBySelection";
            this.buttonBySelection.Size = new System.Drawing.Size(75, 23);
            this.buttonBySelection.TabIndex = 0;
            this.buttonBySelection.Text = "选择的索引";
            this.buttonBySelection.UseVisualStyleBackColor = true;
            this.buttonBySelection.Click += new System.EventHandler(this.buttonBySelection_Click);
            // 
            // buttonByView
            // 
            this.buttonByView.Location = new System.Drawing.Point(3, 6);
            this.buttonByView.Name = "buttonByView";
            this.buttonByView.Size = new System.Drawing.Size(75, 23);
            this.buttonByView.TabIndex = 0;
            this.buttonByView.Text = "视图内索引";
            this.buttonByView.UseVisualStyleBackColor = true;
            this.buttonByView.Click += new System.EventHandler(this.buttonByView_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucSelectFeatureClass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonRefersh);
            this.splitContainer1.Size = new System.Drawing.Size(246, 20);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 0;
            // 
            // ucSelectFeatureClass
            // 
            this.ucSelectFeatureClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSelectFeatureClass.Label = "";
            this.ucSelectFeatureClass.LabelWidth = 25;
            this.ucSelectFeatureClass.Location = new System.Drawing.Point(0, 0);
            this.ucSelectFeatureClass.Margin = new System.Windows.Forms.Padding(5);
            this.ucSelectFeatureClass.Name = "ucSelectFeatureClass";
            this.ucSelectFeatureClass.Size = new System.Drawing.Size(192, 20);
            this.ucSelectFeatureClass.TabIndex = 0;
            this.ucSelectFeatureClass.VisibleOpenButton = false;
            // 
            // buttonRefersh
            // 
            this.buttonRefersh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRefersh.Location = new System.Drawing.Point(0, 0);
            this.buttonRefersh.Name = "buttonRefersh";
            this.buttonRefersh.Size = new System.Drawing.Size(50, 20);
            this.buttonRefersh.TabIndex = 0;
            this.buttonRefersh.Text = "刷新";
            this.buttonRefersh.UseVisualStyleBackColor = true;
            this.buttonRefersh.Click += new System.EventHandler(this.buttonRefersh_Click);
            // 
            // UcExtentSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxIndexLayer);
            this.Controls.Add(this.groupBoxExtentType);
            this.Name = "UcExtentSetting";
            this.Size = new System.Drawing.Size(252, 392);
            this.groupBoxExtentType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupExtentType.Properties)).EndInit();
            this.groupBoxIndexLayer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxExtentType;
        private DevExpress.XtraEditors.RadioGroup radioGroupExtentType;
        private System.Windows.Forms.GroupBox groupBoxIndexLayer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonRefersh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonByAll;
        private System.Windows.Forms.Button buttonBySelection;
        private System.Windows.Forms.Button buttonByView;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonSelectNull;
        private System.Windows.Forms.CheckedListBox checkedListBoxIndexes;
        private UcSelectFeatureClass ucSelectFeatureClass;
    }
}
