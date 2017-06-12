namespace Yutai.Plugins.TableEditor.Views
{
    partial class AttributeExplorer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkZoomToFeature = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtStrFilter = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtField = new Yutai.Plugins.TableEditor.Controls.UCSelectField();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(393, 397);
            this.panel1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.dataGridView);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(8, 133);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(377, 226);
            this.panel8.TabIndex = 6;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOID,
            this.colValue});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(377, 226);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // colOID
            // 
            this.colOID.HeaderText = "序号";
            this.colOID.Name = "colOID";
            this.colOID.ReadOnly = true;
            this.colOID.Width = 80;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "值";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnClose);
            this.panel7.Controls.Add(this.chkZoomToFeature);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(8, 359);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(377, 30);
            this.panel7.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(295, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkZoomToFeature
            // 
            this.chkZoomToFeature.AutoSize = true;
            this.chkZoomToFeature.Location = new System.Drawing.Point(3, 8);
            this.chkZoomToFeature.Name = "chkZoomToFeature";
            this.chkZoomToFeature.Size = new System.Drawing.Size(108, 16);
            this.chkZoomToFeature.TabIndex = 0;
            this.chkZoomToFeature.Text = "缩放到选择要素";
            this.chkZoomToFeature.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(8, 108);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(377, 25);
            this.panel6.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label3.Size = new System.Drawing.Size(46, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "属性值";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtStrFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(8, 83);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(377, 25);
            this.panel5.TabIndex = 3;
            // 
            // txtStrFilter
            // 
            this.txtStrFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtStrFilter.Location = new System.Drawing.Point(0, 0);
            this.txtStrFilter.Name = "txtStrFilter";
            this.txtStrFilter.Size = new System.Drawing.Size(377, 21);
            this.txtStrFilter.TabIndex = 0;
            this.txtStrFilter.TextChanged += new System.EventHandler(this.txtStrFilter_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(8, 58);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(377, 25);
            this.panel4.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label2.Size = new System.Drawing.Size(34, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "搜索";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtField);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(8, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(377, 25);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(8, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 25);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.label1.Size = new System.Drawing.Size(34, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段";
            // 
            // txtField
            // 
            this.txtField.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtField.Label = "";
            this.txtField.LabelWidth = 25;
            this.txtField.Location = new System.Drawing.Point(0, 0);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(377, 20);
            this.txtField.TabIndex = 0;
            this.txtField.SelectComlateEvent += new System.EventHandler(this.txtField_SelectComlateEvent);
            // 
            // AttributeExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 397);
            this.Controls.Add(this.panel1);
            this.Name = "AttributeExplorer";
            this.Text = "属性资源管理器";
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private Controls.UCSelectField txtField;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtStrFilter;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkZoomToFeature;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}