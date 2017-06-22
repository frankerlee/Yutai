using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class MultiAttributeListControl
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
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.gridControl1.Dock = DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(296, 224);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor;
            base.Controls.Add(this.gridControl1);
            base.Name = "MultiAttributeListControl";
            base.Size = new Size(296, 224);
            base.Load += new EventHandler(this.AttributeListControl_Load);
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private GridControl gridControl1;
        private GridView gridView1;
        private int m_nX;
        private int m_nY;
    }
}