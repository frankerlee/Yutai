using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class MultiAttributeListControlExtend
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
            this.panel3 = new Panel();
            this.cboLayer = new ComboBoxEdit();
            this.label1 = new Label();
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.panel3.SuspendLayout();
            this.cboLayer.Properties.BeginInit();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.panel3.Controls.Add(this.cboLayer);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(374, 23);
            this.panel3.TabIndex = 1;
            this.panel3.Resize += new EventHandler(this.panel3_Resize);
            this.panel3.SizeChanged += new EventHandler(this.panel3_SizeChanged);
            this.cboLayer.EditValue = "";
            this.cboLayer.Location = new System.Drawing.Point(49, 1);
            this.cboLayer.Name = "cboLayer";
            this.cboLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLayer.Size = new Size(297, 21);
            this.cboLayer.TabIndex = 3;
            this.cboLayer.SelectedIndexChanged += new EventHandler(this.cboLayer_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层:";
            this.gridControl1.Dock = DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(374, 287);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            base.Controls.Add(this.gridControl1);
            base.Controls.Add(this.panel3);
            base.Name = "MultiAttributeListControlExtend";
            base.Size = new Size(374, 310);
            base.Load += new EventHandler(this.AttributeListControl_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.cboLayer.Properties.EndInit();
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboLayer;
        private GridControl gridControl1;
        private GridView gridView1;
        private Label label1;
        private int m_nX;
        private int m_nY;
        private Panel panel3;
    }
}