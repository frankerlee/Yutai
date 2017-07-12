using Yutai.Pipeline.Editor.Controls;

namespace Yutai.Pipeline.Editor.Forms.Exchange
{
    partial class FrmImportAXFData
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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ckbAdd = new System.Windows.Forms.CheckBox();
            this.ckbModify = new System.Windows.Forms.CheckBox();
            this.ckbDelete = new System.Windows.Forms.CheckBox();
            this.ckbNoEdit = new System.Windows.Forms.CheckBox();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReadData = new System.Windows.Forms.Button();
            this.ucTargetFeatureClass = new UcSelectFeatureClass();
            this.ucAxfFolder = new UCSelectFolder();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(15, 409);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(98, 409);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Text = "清空";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(803, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(641, 409);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "AXF文件 (*.axf)|*.axf";
            this.openFileDialog1.Title = "选择AXF文件";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(845, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(10, 10);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.Visible = false;
            // 
            // ckbAdd
            // 
            this.ckbAdd.AutoSize = true;
            this.ckbAdd.Checked = true;
            this.ckbAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAdd.Location = new System.Drawing.Point(15, 380);
            this.ckbAdd.Name = "ckbAdd";
            this.ckbAdd.Size = new System.Drawing.Size(48, 16);
            this.ckbAdd.TabIndex = 11;
            this.ckbAdd.Text = "新增";
            this.ckbAdd.UseVisualStyleBackColor = true;
            // 
            // ckbModify
            // 
            this.ckbModify.AutoSize = true;
            this.ckbModify.Checked = true;
            this.ckbModify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbModify.Location = new System.Drawing.Point(98, 380);
            this.ckbModify.Name = "ckbModify";
            this.ckbModify.Size = new System.Drawing.Size(48, 16);
            this.ckbModify.TabIndex = 11;
            this.ckbModify.Text = "修改";
            this.ckbModify.UseVisualStyleBackColor = true;
            // 
            // ckbDelete
            // 
            this.ckbDelete.AutoSize = true;
            this.ckbDelete.Checked = true;
            this.ckbDelete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDelete.Location = new System.Drawing.Point(181, 380);
            this.ckbDelete.Name = "ckbDelete";
            this.ckbDelete.Size = new System.Drawing.Size(48, 16);
            this.ckbDelete.TabIndex = 11;
            this.ckbDelete.Text = "删除";
            this.ckbDelete.UseVisualStyleBackColor = true;
            // 
            // ckbNoEdit
            // 
            this.ckbNoEdit.AutoSize = true;
            this.ckbNoEdit.Checked = true;
            this.ckbNoEdit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNoEdit.Location = new System.Drawing.Point(264, 380);
            this.ckbNoEdit.Name = "ckbNoEdit";
            this.ckbNoEdit.Size = new System.Drawing.Size(48, 16);
            this.ckbNoEdit.TabIndex = 11;
            this.ckbNoEdit.Text = "不变";
            this.ckbNoEdit.UseVisualStyleBackColor = true;
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(722, 409);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(75, 23);
            this.btnCheckIn.TabIndex = 6;
            this.btnCheckIn.Text = "签入";
            this.btnCheckIn.UseVisualStyleBackColor = true;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column6,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column11,
            this.Column9,
            this.Column1});
            this.dataGridView2.Location = new System.Drawing.Point(15, 95);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(863, 279);
            this.dataGridView2.TabIndex = 12;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "IsSelect";
            this.Column10.FalseValue = "false";
            this.Column10.FillWeight = 48.7285F;
            this.Column10.HeaderText = "选择";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column10.TrueValue = "true";
            this.Column10.Width = 50;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "SourceLayerName";
            this.Column6.FillWeight = 125.8628F;
            this.Column6.HeaderText = "Axf图层";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "AddCount";
            this.Column2.FillWeight = 45.68528F;
            this.Column2.HeaderText = "新增";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ModifyCount";
            this.Column3.FillWeight = 103.3512F;
            this.Column3.HeaderText = "修改";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DeleteCount";
            this.Column4.FillWeight = 100.8075F;
            this.Column4.HeaderText = "删除";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 50;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "NoEditCount";
            this.Column5.FillWeight = 97.97643F;
            this.Column5.HeaderText = "不变";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "TargetLayerName";
            this.Column7.FillWeight = 125.8628F;
            this.Column7.HeaderText = "目标图层";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 150;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "AxfFilePath";
            this.Column11.FillWeight = 125.8628F;
            this.Column11.HeaderText = "Axf路径";
            this.Column11.Name = "Column11";
            this.Column11.Width = 300;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "TargetFeatureLayer";
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SourceFeatureClass";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(560, 409);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 13;
            this.btnExport.Text = "转出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "Axf文件夹：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "目标图层：";
            // 
            // btnReadData
            // 
            this.btnReadData.Location = new System.Drawing.Point(803, 66);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(75, 23);
            this.btnReadData.TabIndex = 18;
            this.btnReadData.Text = "读取数据";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // ucTargetFeatureClass
            // 
            this.ucTargetFeatureClass.Label = "";
            this.ucTargetFeatureClass.LabelWidth = 25;
            this.ucTargetFeatureClass.Location = new System.Drawing.Point(90, 38);
            this.ucTargetFeatureClass.Margin = new System.Windows.Forms.Padding(5);
            this.ucTargetFeatureClass.Name = "ucTargetFeatureClass";
            this.ucTargetFeatureClass.Size = new System.Drawing.Size(788, 20);
            this.ucTargetFeatureClass.TabIndex = 16;
            this.ucTargetFeatureClass.VisibleOpenButton = false;
            // 
            // ucAxfFolder
            // 
            this.ucAxfFolder.Label = "";
            this.ucAxfFolder.LabelWidth = 25;
            this.ucAxfFolder.Location = new System.Drawing.Point(90, 8);
            this.ucAxfFolder.Name = "ucAxfFolder";
            this.ucAxfFolder.Size = new System.Drawing.Size(788, 21);
            this.ucAxfFolder.TabIndex = 15;
            // 
            // frmImportAXFData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 441);
            this.Controls.Add(this.btnReadData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucTargetFeatureClass);
            this.Controls.Add(this.ucAxfFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.ckbNoEdit);
            this.Controls.Add(this.ckbDelete);
            this.Controls.Add(this.ckbModify);
            this.Controls.Add(this.ckbAdd);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnSelectAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportAXFData";
            this.Text = "导入AXF数据";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox ckbAdd;
        private System.Windows.Forms.CheckBox ckbModify;
        private System.Windows.Forms.CheckBox ckbDelete;
        private System.Windows.Forms.CheckBox ckbNoEdit;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private UCSelectFolder ucAxfFolder;
        private UcSelectFeatureClass ucTargetFeatureClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}