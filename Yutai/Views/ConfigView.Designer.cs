﻿using Yutai.Controls;
using Yutai.UI.Controls;

namespace Yutai.Views
{
    partial class ConfigView
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
            this._treeViewAdv1 = new Yutai.Controls.ConfigTreeView();
            this.btnCancel = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnOk = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnSave = new Syncfusion.Windows.Forms.ButtonAdv();
            this.configPageControl1 = new Yutai.UI.Controls.ConfigPageControl();
            this.toolStripEx1 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSetDefaults = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolRestorePlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRestoreToolbars = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this._treeViewAdv1)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _treeViewAdv1
            // 
            this._treeViewAdv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._treeViewAdv1.ApplyStyle = true;
            this._treeViewAdv1.BackColor = System.Drawing.Color.White;
            this._treeViewAdv1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.Gray);
            this._treeViewAdv1.BeforeTouchSize = new System.Drawing.Size(174, 437);
            this._treeViewAdv1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this._treeViewAdv1.BorderColor = System.Drawing.Color.DarkGray;
            this._treeViewAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._treeViewAdv1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._treeViewAdv1.ForeColor = System.Drawing.Color.Black;
            this._treeViewAdv1.GutterSpace = 15;
            // 
            // 
            // 
            this._treeViewAdv1.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._treeViewAdv1.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this._treeViewAdv1.HelpTextControl.Name = "helpText";
            this._treeViewAdv1.HelpTextControl.Size = new System.Drawing.Size(61, 14);
            this._treeViewAdv1.HelpTextControl.TabIndex = 0;
            this._treeViewAdv1.HelpTextControl.Text = "help text";
            this._treeViewAdv1.HideSelection = false;
            this._treeViewAdv1.InactiveSelectedNodeForeColor = System.Drawing.Color.Black;
            this._treeViewAdv1.ItemHeight = 30;
            this._treeViewAdv1.LineColor = System.Drawing.Color.DarkGray;
            this._treeViewAdv1.Location = new System.Drawing.Point(3, 11);
            this._treeViewAdv1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this._treeViewAdv1.Name = "_treeViewAdv1";
            this._treeViewAdv1.SelectedNodeBackground = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220))))));
            this._treeViewAdv1.SelectedNodeForeColor = System.Drawing.Color.Black;
            this._treeViewAdv1.ShowFocusRect = false;
            this._treeViewAdv1.ShowLines = false;
            this._treeViewAdv1.ShowSuperTooltip = false;
            this._treeViewAdv1.Size = new System.Drawing.Size(174, 437);
            this._treeViewAdv1.Style = Syncfusion.Windows.Forms.Tools.TreeStyle.Metro;
            this._treeViewAdv1.TabIndex = 0;
            this._treeViewAdv1.Text = "treeViewAdv1";
            this._treeViewAdv1.ThemesEnabled = false;
            // 
            // 
            // 
            this._treeViewAdv1.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this._treeViewAdv1.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._treeViewAdv1.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this._treeViewAdv1.ToolTipControl.Name = "toolTip";
            this._treeViewAdv1.ToolTipControl.Size = new System.Drawing.Size(49, 14);
            this._treeViewAdv1.ToolTipControl.TabIndex = 1;
            this._treeViewAdv1.ToolTipControl.Text = "toolTip";
            this._treeViewAdv1.ToolTipDuration = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.OfficeXP;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BeforeTouchSize = new System.Drawing.Size(85, 24);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.IsBackStageButton = false;
            this.btnCancel.Location = new System.Drawing.Point(619, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyle = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.OfficeXP;
            this.btnOk.BackColor = System.Drawing.Color.White;
            this.btnOk.BeforeTouchSize = new System.Drawing.Size(85, 24);
            this.btnOk.IsBackStageButton = false;
            this.btnOk.Location = new System.Drawing.Point(528, 453);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(85, 24);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyle = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.OfficeXP;
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.BeforeTouchSize = new System.Drawing.Size(92, 24);
            this.btnSave.IsBackStageButton = false;
            this.btnSave.Location = new System.Drawing.Point(430, 453);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 24);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "应用";
            this.btnSave.UseVisualStyle = false;
            // 
            // configPageControl1
            // 
            this.configPageControl1.ConfigPage = null;
            this.configPageControl1.Description = "详细说明文本";
            this.configPageControl1.Icon = null;
            this.configPageControl1.Location = new System.Drawing.Point(183, 11);
            this.configPageControl1.Name = "configPageControl1";
            this.configPageControl1.Size = new System.Drawing.Size(521, 437);
            this.configPageControl1.TabIndex = 12;
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Image = null;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOptions});
            this.toolStripEx1.Location = new System.Drawing.Point(3, 454);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Office12Mode = false;
            this.toolStripEx1.ShowCaption = false;
            this.toolStripEx1.Size = new System.Drawing.Size(64, 25);
            this.toolStripEx1.TabIndex = 40;
            this.toolStripEx1.Text = "Style";
            this.toolStripEx1.VisualStyle = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Metro;
            // 
            // toolOptions
            // 
            this.toolOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenFolder,
            this.toolStripSeparator1,
            this.toolSetDefaults,
            this.toolStripSeparator2,
            this.toolRestorePlugins,
            this.toolRestoreToolbars});
            this.toolOptions.ForeColor = System.Drawing.Color.Black;
            this.toolOptions.Image = global::Yutai.Properties.Resources.icon_settings;
            this.toolOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOptions.Name = "toolOptions";
            this.toolOptions.Size = new System.Drawing.Size(61, 22);
            this.toolOptions.Text = "选项";
            // 
            // toolOpenFolder
            // 
            this.toolOpenFolder.Image = global::Yutai.Properties.Resources.icon_folder;
            this.toolOpenFolder.Name = "toolOpenFolder";
            this.toolOpenFolder.Size = new System.Drawing.Size(148, 22);
            this.toolOpenFolder.Text = "打开文件夹";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // toolSetDefaults
            // 
            this.toolSetDefaults.Image = global::Yutai.Properties.Resources.img_default24;
            this.toolSetDefaults.Name = "toolSetDefaults";
            this.toolSetDefaults.Size = new System.Drawing.Size(148, 22);
            this.toolSetDefaults.Text = "使用默认配置";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // toolRestorePlugins
            // 
            this.toolRestorePlugins.Image = global::Yutai.Properties.Resources.img_plugin32;
            this.toolRestorePlugins.Name = "toolRestorePlugins";
            this.toolRestorePlugins.Size = new System.Drawing.Size(148, 22);
            this.toolRestorePlugins.Text = "恢复插件";
            // 
            // toolRestoreToolbars
            // 
            this.toolRestoreToolbars.Name = "toolRestoreToolbars";
            this.toolRestoreToolbars.Size = new System.Drawing.Size(148, 22);
            this.toolRestoreToolbars.Text = "恢复界面";
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientSize = new System.Drawing.Size(708, 483);
            this.Controls.Add(this._treeViewAdv1);
            this.Controls.Add(this.toolStripEx1);
            this.Controls.Add(this.configPageControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "ConfigView";
            this.Text = "配置";
            ((System.ComponentModel.ISupportInitialize)(this._treeViewAdv1)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConfigTreeView _treeViewAdv1;
        private Syncfusion.Windows.Forms.ButtonAdv btnCancel;
        private Syncfusion.Windows.Forms.ButtonAdv btnOk;
        private Syncfusion.Windows.Forms.ButtonAdv btnSave;
        private ConfigPageControl configPageControl1;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripDropDownButton toolOptions;
        private System.Windows.Forms.ToolStripMenuItem toolSetDefaults;
        private System.Windows.Forms.ToolStripMenuItem toolRestorePlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolRestoreToolbars;
    }
}