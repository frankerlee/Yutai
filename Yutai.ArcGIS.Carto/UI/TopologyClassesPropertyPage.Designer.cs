using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;


namespace Yutai.ArcGIS.Carto.UI
{
    partial class TopologyClassesPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.gridControl1 = new GridControl();
            this.gridView1 = new GridView();
            this.txtValue = new SpinEdit();
            this.gridControl1.BeginInit();
            this.gridView1.BeginInit();
            this.txtValue.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "优先级数值:";
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new Point(16, 40);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new Size(312, 200);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            int[] bits = new int[4];
            bits[0] = 1;
            this.txtValue.EditValue = new decimal(bits);
            this.txtValue.Location = new Point(96, 8);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtValue.Properties.Enabled = false;
            int[] bits2 = new int[4];
            bits2[0] = 50;
            this.txtValue.Properties.MaxValue = new decimal(bits2);
            int[] bits3 = new int[4];
            bits3[0] = 1;
            this.txtValue.Properties.MinValue = new decimal(bits3);
            this.txtValue.Properties.UseCtrlIncrement = false;
            this.txtValue.Size = new Size(96, 23);
            this.txtValue.TabIndex = 6;
            base.Controls.Add(this.txtValue);
            base.Controls.Add(this.gridControl1);
            base.Controls.Add(this.label1);
            base.Name = "TopologyClassesPropertyPage";
            base.Size = new Size(408, 288);
            base.Load += new EventHandler(this.TopologyClassesPropertyPage_Load);
            this.gridControl1.EndInit();
            this.gridView1.EndInit();
            this.txtValue.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GridControl gridControl1;
        private GridView gridView1;
        private Label label1;
        private SpinEdit txtValue;
        private VertXtraGrid vertXtraGrid_0;
    }
}