using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    partial class CustomizationForm
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomizationForm));
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.label3 = new Label();
            this.cklsbtoolBar = new CheckedListBox();
            this.tabPage2 = new TabPage();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.label2 = new Label();
            this.label1 = new Label();
            this.lsbCatalog = new ListBox();
            this.chlsbCommands = new CheckedListBox();
            this.button1 = new Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(386, 261);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cklsbtoolBar);
            this.tabPage1.Location = new Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(378, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工具栏";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "工具栏";
            this.cklsbtoolBar.FormattingEnabled = true;
            this.cklsbtoolBar.Location = new Point(8, 28);
            this.cklsbtoolBar.Name = "cklsbtoolBar";
            this.cklsbtoolBar.Size = new Size(362, 196);
            this.cklsbtoolBar.TabIndex = 0;
            this.cklsbtoolBar.ItemCheck += new ItemCheckEventHandler(this.cklsbtoolBar_ItemCheck);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lsbCatalog);
            this.tabPage2.Controls.Add(this.chlsbCommands);
            this.tabPage2.Location = new Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(378, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "命令";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0 });
            this.listView1.HeaderStyle = ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(160, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(210, 214);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            this.listView1.MouseMove += new MouseEventHandler(this.listView1_MouseMove);
            this.columnHeader_0.Width = 184;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(158, 7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "命令";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "类别";
            this.lsbCatalog.FormattingEnabled = true;
            this.lsbCatalog.ItemHeight = 12;
            this.lsbCatalog.Location = new Point(8, 22);
            this.lsbCatalog.Name = "lsbCatalog";
            this.lsbCatalog.Size = new Size(146, 208);
            this.lsbCatalog.TabIndex = 2;
            this.lsbCatalog.SelectedIndexChanged += new EventHandler(this.lsbCatalog_SelectedIndexChanged);
            this.chlsbCommands.FormattingEnabled = true;
            this.chlsbCommands.Location = new Point(258, 22);
            this.chlsbCommands.Name = "chlsbCommands";
            this.chlsbCommands.Size = new Size(112, 212);
            this.chlsbCommands.TabIndex = 1;
            this.chlsbCommands.SelectedIndexChanged += new EventHandler(this.chlsbCommands_SelectedIndexChanged);
            this.chlsbCommands.ItemCheck += new ItemCheckEventHandler(this.chlsbCommands_ItemCheck);
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new Point(283, 267);
            this.button1.Name = "button1";
            this.button1.Size = new Size(61, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(386, 294);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.tabControl1);
            base.Name = "CustomizationForm";
            this.Text = "自定义";
            base.Load += new EventHandler(this.CustomizationForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private Button button1;
        private CheckedListBox chlsbCommands;
        private CheckedListBox cklsbtoolBar;
        private ColumnHeader columnHeader_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        private ListBox lsbCatalog;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}