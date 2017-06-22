using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class AttributeListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeListControl));
            this.toolStrip1 = new ToolStrip();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.toolAttachmentLabel = new ToolStripLabel();
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.toolStrip1.SuspendLayout();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripDropDownButton1, this.toolAttachmentLabel });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(296, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = (Image) resources.GetObject("toolStripDropDownButton1.Image");
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolAttachmentLabel.Name = "toolAttachmentLabel";
            this.toolAttachmentLabel.Size = new Size(47, 22);
            this.toolAttachmentLabel.Text = "附件(0)";
            this.gridControl1.Dock = DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(0, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(296, 199);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor;
            base.Controls.Add(this.gridControl1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "AttributeListControl";
            base.Size = new Size(296, 224);
            base.Load += new EventHandler(this.AttributeListControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private GridControl gridControl1;
        private GridView gridView1;
        private int m_nX;
        private int m_nY;
        private ToolStripLabel toolAttachmentLabel;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
    }
}