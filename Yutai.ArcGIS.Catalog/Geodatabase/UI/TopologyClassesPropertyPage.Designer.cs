using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
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
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
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
            this.gridView1.SelectionChanged += new SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.btnAdd.Location = new Point(336, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(64, 24);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "增加类";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Location = new Point(336, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(64, 24);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnDeleteAll.Location = new Point(336, 120);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(64, 24);
            this.btnDeleteAll.TabIndex = 5;
            this.btnDeleteAll.Text = "删除全部";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            int[] bits = new int[4];
            bits[0] = 1;
            this.txtValue.EditValue = new decimal(bits);
            this.txtValue.Location = new Point(96, 8);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 50;
            this.txtValue.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.txtValue.Properties.MinValue = new decimal(bits);
            this.txtValue.Size = new Size(96, 23);
            this.txtValue.TabIndex = 6;
            this.txtValue.EditValueChanged += new EventHandler(this.txtValue_EditValueChanged);
            base.Controls.Add(this.txtValue);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
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

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnDeleteAll;
        private GridControl gridControl1;
        private GridView gridView1;
        private Label label1;
        private SpinEdit txtValue;
        private VertXtraGrid vertXtraGrid_0;
    }
}