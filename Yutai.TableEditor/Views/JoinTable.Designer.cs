using Yutai.Plugins.TableEditor.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    partial class JoinTable
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
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupKeys = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDatasource = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.selectFields1 = new Yutai.Plugins.TableEditor.Controls.SelectFields();
            this.cboCurrent = new Yutai.Plugins.TableEditor.Controls.UCSelectField();
            this.cboExternal = new Yutai.Plugins.TableEditor.Controls.UCSelectField();
            this.groupKeys.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(25, 408);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(90, 16);
            this.chkAll.TabIndex = 6;
            this.chkAll.Text = "选择所有/无";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(315, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(224, 403);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "连接";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupKeys
            // 
            this.groupKeys.Controls.Add(this.label5);
            this.groupKeys.Controls.Add(this.label4);
            this.groupKeys.Controls.Add(this.cboCurrent);
            this.groupKeys.Controls.Add(this.cboExternal);
            this.groupKeys.Controls.Add(this.label3);
            this.groupKeys.Location = new System.Drawing.Point(12, 96);
            this.groupKeys.Name = "groupKeys";
            this.groupKeys.Size = new System.Drawing.Size(392, 85);
            this.groupKeys.TabIndex = 2;
            this.groupKeys.TabStop = false;
            this.groupKeys.Text = "选择键列";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(16, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "当前";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(217, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "外部";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(183, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "____";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDatasource);
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据源";
            // 
            // txtDatasource
            // 
            this.txtDatasource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDatasource.Location = new System.Drawing.Point(101, 30);
            this.txtDatasource.Name = "txtDatasource";
            this.txtDatasource.ReadOnly = true;
            this.txtDatasource.Size = new System.Drawing.Size(218, 21);
            this.txtDatasource.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(325, 28);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(53, 21);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "打开";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "数据源";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectFields1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 211);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "要加入的字段";
            // 
            // selectFields1
            // 
            this.selectFields1.Fields = null;
            this.selectFields1.FormattingEnabled = true;
            this.selectFields1.Location = new System.Drawing.Point(3, 26);
            this.selectFields1.Name = "selectFields1";
            this.selectFields1.Size = new System.Drawing.Size(385, 180);
            this.selectFields1.TabIndex = 29;
            // 
            // cboCurrent
            // 
            this.cboCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboCurrent.Label = "";
            this.cboCurrent.LabelWidth = 25;
            this.cboCurrent.Location = new System.Drawing.Point(19, 39);
            this.cboCurrent.Name = "cboCurrent";
            this.cboCurrent.Size = new System.Drawing.Size(158, 21);
            this.cboCurrent.TabIndex = 1;
            // 
            // cboExternal
            // 
            this.cboExternal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboExternal.Label = "";
            this.cboExternal.LabelWidth = 25;
            this.cboExternal.Location = new System.Drawing.Point(220, 39);
            this.cboExternal.Name = "cboExternal";
            this.cboExternal.Size = new System.Drawing.Size(158, 21);
            this.cboExternal.TabIndex = 2;
            // 
            // JoinTable
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 434);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupKeys);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "JoinTable";
            this.Text = "连接表";
            this.groupKeys.ResumeLayout(false);
            this.groupKeys.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupKeys;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private UCSelectField cboCurrent;
        private UCSelectField cboExternal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDatasource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private SelectFields selectFields1;
    }
}