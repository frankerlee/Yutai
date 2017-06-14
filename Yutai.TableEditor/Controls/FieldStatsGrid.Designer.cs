namespace Yutai.Plugins.TableEditor.Controls
{
    partial class FieldStatsGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(313, 281);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "计数:\t1330\r\n最小值:\t34.57202\r\n最大值:\t34.72199\r\n总和:\t46091.7034230001\r\n平均值:\t34.65541610751" +
    "88\r\n标准差:\t0.0271714163386424\r\n空:\t0";
            // 
            // FieldStatsGrid
            // 
            this.Controls.Add(this.txtResult);
            this.Name = "FieldStatsGrid";
            this.Size = new System.Drawing.Size(313, 281);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult;
    }
}
