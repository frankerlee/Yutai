using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;
using BaseView = DevExpress.XtraBars.Docking2010.Views.BaseView;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class FieldMatchCtrl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new Point(8, 8);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(296, 224);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.gridControl1);
            base.Name = "FieldMatchCtrl";
            base.Size = new Size(325, 257);
            base.Load += new EventHandler(this.FieldMatchCtrl_Load);
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
        }

       
        private GridControl gridControl1;
        private GridView gridView1;
        private IFields ifields_0;
        private IFields ifields_1;
    }
}