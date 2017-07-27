namespace Yutai.Pipeline.Editor.Forms.Common
{
    partial class frmDeletePipeline
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
            this.btnCopyFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopySecond = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetele = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancle = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnFieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFeature1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnFeature2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMergeValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCopyFirst
            // 
            this.btnCopyFirst.Location = new System.Drawing.Point(89, 585);
            this.btnCopyFirst.Name = "btnCopyFirst";
            this.btnCopyFirst.Size = new System.Drawing.Size(75, 22);
            this.btnCopyFirst.TabIndex = 1;
            this.btnCopyFirst.Text = "复制线一";
            this.btnCopyFirst.Click += new System.EventHandler(this.btnCopyFirst_Click);
            // 
            // btnCopySecond
            // 
            this.btnCopySecond.Location = new System.Drawing.Point(182, 585);
            this.btnCopySecond.Name = "btnCopySecond";
            this.btnCopySecond.Size = new System.Drawing.Size(75, 22);
            this.btnCopySecond.TabIndex = 2;
            this.btnCopySecond.Text = "复制线二";
            this.btnCopySecond.Click += new System.EventHandler(this.btnCopySecond_Click);
            // 
            // btnDetele
            // 
            this.btnDetele.Location = new System.Drawing.Point(281, 585);
            this.btnDetele.Name = "btnDetele";
            this.btnDetele.Size = new System.Drawing.Size(75, 22);
            this.btnDetele.TabIndex = 3;
            this.btnDetele.Text = "删除";
            this.btnDetele.Click += new System.EventHandler(this.btnDetele_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(375, 584);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(484, 578);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnFieldName,
            this.gridColumnFeature1,
            this.gridColumnFeature2,
            this.gridColumnMergeValue});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEditForEditing);
            // 
            // gridColumnFieldName
            // 
            this.gridColumnFieldName.Caption = "属性名";
            this.gridColumnFieldName.FieldName = "FName";
            this.gridColumnFieldName.Name = "gridColumnFieldName";
            this.gridColumnFieldName.Visible = true;
            this.gridColumnFieldName.VisibleIndex = 0;
            // 
            // gridColumnFeature1
            // 
            this.gridColumnFeature1.Caption = "要素1属性值";
            this.gridColumnFeature1.FieldName = "FirstValue";
            this.gridColumnFeature1.Name = "gridColumnFeature1";
            this.gridColumnFeature1.Visible = true;
            this.gridColumnFeature1.VisibleIndex = 1;
            // 
            // gridColumnFeature2
            // 
            this.gridColumnFeature2.Caption = "要素2属性值";
            this.gridColumnFeature2.FieldName = "SecondValue";
            this.gridColumnFeature2.Name = "gridColumnFeature2";
            this.gridColumnFeature2.Visible = true;
            this.gridColumnFeature2.VisibleIndex = 2;
            // 
            // gridColumnMergeValue
            // 
            this.gridColumnMergeValue.Caption = "合并后属性值";
            this.gridColumnMergeValue.FieldName = "MergeValue";
            this.gridColumnMergeValue.Name = "gridColumnMergeValue";
            this.gridColumnMergeValue.Visible = true;
            this.gridColumnMergeValue.VisibleIndex = 3;
            // 
            // frmDeletePipeline
            // 
            this.AcceptButton = this.btnDetele;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(484, 619);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnDetele);
            this.Controls.Add(this.btnCopySecond);
            this.Controls.Add(this.btnCopyFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDeletePipeline";
            this.Text = "管线属性";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCopyFirst;
        private DevExpress.XtraEditors.SimpleButton btnCopySecond;
        private DevExpress.XtraEditors.SimpleButton btnDetele;
        private DevExpress.XtraEditors.SimpleButton btnCancle;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFieldName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFeature1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFeature2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMergeValue;
    }
}