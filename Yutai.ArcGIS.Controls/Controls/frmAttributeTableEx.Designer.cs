using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmAttributeTableEx
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
            this.gridView2 = new GridView();
            this.dataGrid1 = new GridControl();
            this.gridView1 = new GridView();
            this.panel1 = new Panel();
            this.rdoSelect = new RadioButton();
            this.rdoAll = new RadioButton();
            this.label1 = new Label();
            this.gridView2.BeginInit();
            this.dataGrid1.BeginInit();
            this.gridView1.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.gridView2.GridControl = this.dataGrid1;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
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
            this.dataGrid1.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(this.dataGrid1_EmbeddedNavigator_ButtonClick);
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.MainView = this.gridView1;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new Size(614, 230);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.UseEmbeddedNavigator = true;
            this.dataGrid1.ViewCollection.AddRange(new BaseView[] { this.gridView1, this.gridView2 });
            this.gridView1.Appearance.SelectedRow.BackColor = Color.FromArgb(176, 255, 255);
            this.gridView1.Appearance.SelectedRow.BackColor2 = Color.FromArgb(176, 255, 255);
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.dataGrid1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.panel1.Controls.Add(this.rdoSelect);
            this.panel1.Controls.Add(this.rdoAll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 230);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(614, 32);
            this.panel1.TabIndex = 2;
            this.rdoSelect.AutoSize = true;
            this.rdoSelect.Location = new System.Drawing.Point(126, 7);
            this.rdoSelect.Name = "rdoSelect";
            this.rdoSelect.Size = new Size(71, 16);
            this.rdoSelect.TabIndex = 2;
            this.rdoSelect.Text = "选中对象";
            this.rdoSelect.UseVisualStyleBackColor = true;
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Location = new System.Drawing.Point(49, 6);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new Size(71, 16);
            this.rdoAll.TabIndex = 1;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "所有对象";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.CheckedChanged += new EventHandler(this.rdoAll_CheckedChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "显示";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(614, 262);
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel1);
            base.Name = "frmAttributeTableEx";
            this.Text = "属性表";
            base.Load += new EventHandler(this.frmAttributeTableEx_Load);
            base.VisibleChanged += new EventHandler(this.frmAttributeTableEx_VisibleChanged);
            this.gridView2.EndInit();
            this.dataGrid1.EndInit();
            this.gridView1.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private GridControl dataGrid1;
        private GridView gridView1;
        private GridView gridView2;
        private Label label1;
        private ICursor m_pCursor;
        private ICursor m_pSelectedCursor;
        private int m_RecordNum;
        private int m_SelectedRecordNum;
        private Panel panel1;
        private RadioButton rdoAll;
        private RadioButton rdoSelect;
    }
}