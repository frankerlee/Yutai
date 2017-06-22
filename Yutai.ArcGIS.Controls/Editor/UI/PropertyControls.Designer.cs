using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Controls.Controls.EditorUI;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class PropertyControls
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyControls));
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.textBox1 = new TextBox();
            this.dataGrid1 = new GridControl();
            this.gridView1 = new GridView();
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.dataGrid1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(372, 24);
            this.panel1.TabIndex = 0;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 265);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(372, 58);
            this.panel2.TabIndex = 1;
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Location = new Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(372, 58);
            this.textBox1.TabIndex = 0;
            this.dataGrid1.Dock = DockStyle.Fill;
            this.dataGrid1.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Append.Hint = "增加";
            this.dataGrid1.EmbeddedNavigator.Buttons.CancelEdit.Hint = "取消编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Edit.Hint = "编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.EndEdit.Hint = "结束编辑";
            this.dataGrid1.EmbeddedNavigator.Buttons.First.Hint = "第一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.Last.Hint = "上一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.Next.Hint = "下一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
            this.dataGrid1.EmbeddedNavigator.Buttons.Prev.Hint = "前一个";
            this.dataGrid1.EmbeddedNavigator.Buttons.PrevPage.Hint = "前一页";
            this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Hint = "删除";
            this.dataGrid1.EmbeddedNavigator.Name = "";
            this.dataGrid1.Location = new Point(0, 24);
            this.dataGrid1.MainView = this.gridView1;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new Size(372, 241);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.dataGrid1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SelectionChanged += new SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.EndSorting += new EventHandler(this.gridView1_EndSorting);
            this.radioButton1.Appearance = Appearance.Button;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.FlatAppearance.BorderSize = 0;
            this.radioButton1.FlatStyle = FlatStyle.Flat;
            this.radioButton1.Image = (Image) resources.GetObject("radioButton1.Image");
            this.radioButton1.Location = new Point(0, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(22, 22);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton2.Appearance = Appearance.Button;
            this.radioButton2.AutoSize = true;
            this.radioButton2.FlatAppearance.BorderSize = 0;
            this.radioButton2.FlatStyle = FlatStyle.Flat;
            this.radioButton2.Image = (Image) resources.GetObject("radioButton2.Image");
            this.radioButton2.Location = new Point(24, 0);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(22, 22);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "PropertyControls";
            base.Size = new Size(372, 323);
            base.Load += new EventHandler(this.PropertyControls_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.dataGrid1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private GridControl dataGrid1;
        private GridView gridView1;
        private Panel panel1;
        private Panel panel2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox1;
    }
}