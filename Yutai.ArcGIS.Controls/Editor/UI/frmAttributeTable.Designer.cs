using System;
using System.Collections;
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
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmAttributeTable
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
            if (this.m_pCursor != null)
            {
                ComReleaser.ReleaseCOMObject(this.m_pCursor);
            }
            this.m_pDataTable.ColumnChanged -= new DataColumnChangeEventHandler(this.m_pDataTable_ColumnChanged);
            EditorEvent.OnStopEditing -= new EditorEvent.OnStopEditingHandler(this.EditorEvent_OnStopEditing);
            EditorEvent.OnStartEditing -= new EditorEvent.OnStartEditingHandler(this.EditorEvent_OnStartEditing);
            EditorEvent.OnAddFeature -= new EditorEvent.OnAddFeatureHandler(this.EditorEvent_OnAddFeature);
            EditorEvent.OnDeleteFeature -= new EditorEvent.OnDeleteFeatureHandler(this.EditorEvent_OnDeleteFeature);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeTable));
            this.panel1 = new Panel();
            this.dataGrid1 = new GridControl();
            this.gridView1 = new GridView();
            this.dataGrid1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(464, 8);
            this.panel1.TabIndex = 1;
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
            this.dataGrid1.Size = new Size(464, 232);
            this.dataGrid1.TabIndex = 3;
            this.dataGrid1.UseEmbeddedNavigator = true;
            this.dataGrid1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.dataGrid1.LocationChanged += new EventHandler(this.dataGrid1_LocationChanged);
            this.dataGrid1.Click += new EventHandler(this.dataGrid1_Click);
            this.gridView1.GridControl = this.dataGrid1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(464, 240);
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "frmAttributeTable";
            base.Load += new EventHandler(this.TableControl_Load);
            base.SizeChanged += new EventHandler(this.TableControl_SizeChanged);
            this.dataGrid1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private GridControl dataGrid1;
        private GridView gridView1;
        private ICursor m_pCursor;
        private int m_RecordNum;
        private Panel panel1;
    }
}