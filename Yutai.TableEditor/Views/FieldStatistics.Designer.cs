using Yutai.Plugins.TableEditor.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    partial class FieldStatistics
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
            this.cboField = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this._fieldStatsGrid = new Yutai.Plugins.TableEditor.Controls.FieldStatsGrid();
            this.SuspendLayout();
            // 
            // cboField
            // 
            this.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboField.Location = new System.Drawing.Point(58, 21);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(223, 21);
            this.cboField.TabIndex = 0;
            this.cboField.SelectedIndexChanged += new System.EventHandler(this.cboField_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "字段";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(206, 216);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(125, 216);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 26);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "拷贝";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // _fieldStatsGrid
            // 
            this._fieldStatsGrid.FeatureLayer = null;
            this._fieldStatsGrid.FieldName = null;
            this._fieldStatsGrid.Location = new System.Drawing.Point(14, 48);
            this._fieldStatsGrid.Name = "_fieldStatsGrid";
            this._fieldStatsGrid.Size = new System.Drawing.Size(267, 162);
            this._fieldStatsGrid.TabIndex = 5;
            // 
            // FieldStatistics
            // 
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(293, 250);
            this.Controls.Add(this._fieldStatsGrid);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboField);
            this.Name = "FieldStatistics";
            this.Text = "字段统计";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private FieldStatsGrid _fieldStatsGrid;
    }
}