using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    partial class frmSetSelectableLayer
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.checkedListBoxControl = new CheckedListBox();
            this.btnSelectAll = new Button();
            this.btnClear = new Button();
            this.btnClose = new Button();
            this.btnSwitchSelect = new Button();
          
            base.SuspendLayout();
            this.checkedListBoxControl.CheckOnClick = true;
            this.checkedListBoxControl.ItemHeight = 17;
            this.checkedListBoxControl.Location = new Point(8, 8);
            this.checkedListBoxControl.Name = "checkedListBoxControl";
            this.checkedListBoxControl.Size = new Size(192, 248);
            this.checkedListBoxControl.TabIndex = 0;
            this.checkedListBoxControl.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxControl_ItemCheck);
            this.btnSelectAll.Location = new Point(216, 8);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(48, 24);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.btnClear.Location = new Point(216, 68);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(48, 24);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnClose.Location = new Point(216, 232);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(48, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnSwitchSelect.Location = new Point(216, 38);
            this.btnSwitchSelect.Name = "btnSwitchSelect";
            this.btnSwitchSelect.Size = new Size(48, 24);
            this.btnSwitchSelect.TabIndex = 4;
            this.btnSwitchSelect.Text = "反选";
            this.btnSwitchSelect.Click += new EventHandler(this.btnSwitchSelect_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(266, 276);
            base.Controls.Add(this.btnSwitchSelect);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.checkedListBoxControl);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSetSelectableLayer";
            this.Text = "设置可选的图层";
            base.Load += new EventHandler(this.frmSetSelectableLayer_Load);
            base.ResumeLayout(false);
            
        }

        #endregion

        private CheckedListBox checkedListBoxControl;

        private Button btnSelectAll;

        private Button btnClear;

        private Button btnClose;

        private Button btnSwitchSelect;
    }
}