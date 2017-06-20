using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.Plugins.Catalog.Views
{
    partial class CatalogView
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
            this.components = new System.ComponentModel.Container();
            this.panelTreeView = new System.Windows.Forms.Panel();
            this.kTreeView1 = new Yutai.ArcGIS.Catalog.UI.KTreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.imageComboBoxEdit1 = new Yutai.ArcGIS.Common.ControlExtend.ImageComboBoxEditEx();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContentListView = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.qqqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelTreeView.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelContentListView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTreeView
            // 
            this.panelTreeView.Controls.Add(this.kTreeView1);
            this.panelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTreeView.Location = new System.Drawing.Point(0, 0);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Size = new System.Drawing.Size(255, 556);
            this.panelTreeView.TabIndex = 5;
            // 
            // kTreeView1
            // 
            this.kTreeView1.AllowDrop = true;
            this.kTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kTreeView1.GxCatalog = null;
            this.kTreeView1.HideSelection = false;
            this.kTreeView1.ImageIndex = 0;
            this.kTreeView1.LabelEdit = true;
            this.kTreeView1.Location = new System.Drawing.Point(0, 0);
            this.kTreeView1.Name = "kTreeView1";
            this.kTreeView1.SelectedImageIndex = 0;
            this.kTreeView1.Size = new System.Drawing.Size(255, 556);
            this.kTreeView1.TabIndex = 15;
            this.kTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.kTreeView1_AfterSelect);
            this.kTreeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kTreeView1_MouseUp);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 26);
            this.panel3.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.imageComboBoxEdit1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(60, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(359, 26);
            this.panel5.TabIndex = 1;
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(0, 0);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(359, 20);
            this.imageComboBoxEdit1.TabIndex = 0;
            this.imageComboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.imageComboBoxEditEx2_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(60, 26);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "位置";
            // 
            // panelContentListView
            // 
            this.panelContentListView.Controls.Add(this.listView1);
            this.panelContentListView.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelContentListView.Location = new System.Drawing.Point(258, 0);
            this.panelContentListView.Name = "panelContentListView";
            this.panelContentListView.Size = new System.Drawing.Size(161, 556);
            this.panelContentListView.TabIndex = 4;
            this.panelContentListView.Visible = false;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(161, 556);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名字";
            this.columnHeader1.Width = 143;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型";
            this.columnHeader2.Width = 207;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(255, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 556);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qqqToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(419, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // qqqToolStripMenuItem
            // 
            this.qqqToolStripMenuItem.Name = "qqqToolStripMenuItem";
            this.qqqToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.qqqToolStripMenuItem.Text = "切换";
            this.qqqToolStripMenuItem.Click += new System.EventHandler(this.qqqToolStripMenuItem_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panelTreeView);
            this.panel6.Controls.Add(this.splitter1);
            this.panel6.Controls.Add(this.panelContentListView);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 51);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(419, 556);
            this.panel6.TabIndex = 15;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // CatalogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.menuStrip1);
            this.Name = "CatalogView";
            this.Size = new System.Drawing.Size(419, 607);
            this.Load += new System.EventHandler(this.CatalogView_Load);
            this.SizeChanged += new System.EventHandler(this.CatalogView_SizeChanged);
            this.panelTreeView.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelContentListView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panelTreeView;

        private Panel panelContentListView;

        private MenuStrip menuStrip1;

        private ToolStripMenuItem qqqToolStripMenuItem;

        private Panel panel3;

        private Panel panel5;

        private Panel panel4;

        private Label label1;

        private KTreeView kTreeView1;

        private ImageComboBoxEditEx imageComboBoxEditEx1;

        private Splitter splitter1;

        public ListView listView1;

        private ColumnHeader columnHeader1;

        private ColumnHeader columnHeader2;

        private Panel panel6;

        private ImageList imageList1;

        private ImageComboBoxEditEx imageComboBoxEdit1;

       
        private PopupMenu popupMenu1;
    }
}
