using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Framework.Docking;
using Yutai.Plugins.Events;
using IDockContent = Yutai.ArcGIS.Framework.Docking.IDockContent;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class SnapConfigControl
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
            this.groupBox4 = new GroupBox();
            this.cboSnapUnits = new ComboBoxEdit();
            this.txtRadio = new TextEdit();
            this.dataGrid1 = new DataGrid();
            this.btnApply = new SimpleButton();
            this.chkStartSnap = new CheckEdit();
            this.groupBox4.SuspendLayout();
            this.cboSnapUnits.Properties.BeginInit();
            this.txtRadio.Properties.BeginInit();
            this.dataGrid1.BeginInit();
            this.chkStartSnap.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox4.Controls.Add(this.cboSnapUnits);
            this.groupBox4.Controls.Add(this.txtRadio);
            this.groupBox4.Location = new System.Drawing.Point(8, 176);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(264, 48);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "捕捉半径";
            this.cboSnapUnits.EditValue = "像素";
            this.cboSnapUnits.Location = new System.Drawing.Point(144, 17);
            this.cboSnapUnits.Name = "cboSnapUnits";
            this.cboSnapUnits.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSnapUnits.Properties.Items.AddRange(new object[] { "像素", "地图单位" });
            this.cboSnapUnits.Size = new Size(104, 21);
            this.cboSnapUnits.TabIndex = 2;
            this.cboSnapUnits.SelectedIndexChanged += new EventHandler(this.cboSnapUnits_SelectedIndexChanged);
            this.txtRadio.EditValue = "1";
            this.txtRadio.Location = new System.Drawing.Point(16, 17);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.RightToLeft = RightToLeft.Yes;
            this.txtRadio.Size = new Size(112, 21);
            this.txtRadio.TabIndex = 0;
            this.txtRadio.EditValueChanged += new EventHandler(this.txtRadio_EditValueChanged);
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.Dock = DockStyle.Top;
            this.dataGrid1.GridLineStyle = DataGridLineStyle.None;
            this.dataGrid1.HeaderForeColor = SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.RowHeaderWidth = 0;
            this.dataGrid1.Size = new Size(312, 168);
            this.dataGrid1.TabIndex = 2;
            this.btnApply.Location = new System.Drawing.Point(8, 262);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(88, 24);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用设置";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.chkStartSnap.EditValue = true;
            this.chkStartSnap.Location = new System.Drawing.Point(8, 237);
            this.chkStartSnap.Name = "chkStartSnap";
            this.chkStartSnap.Properties.Caption = "启用捕捉";
            this.chkStartSnap.Size = new Size(128, 19);
            this.chkStartSnap.TabIndex = 6;
            base.Controls.Add(this.chkStartSnap);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.dataGrid1);
            base.Controls.Add(this.groupBox4);
            base.Name = "SnapConfigControl";
            base.Size = new Size(312, 302);
            base.Load += new EventHandler(this.SnapConfigControl_Load);
            this.groupBox4.ResumeLayout(false);
            this.cboSnapUnits.Properties.EndInit();
            this.txtRadio.Properties.EndInit();
            this.dataGrid1.EndInit();
            this.chkStartSnap.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnApply;
        private ComboBoxEdit cboSnapUnits;
        private CheckEdit chkStartSnap;
        private DataGrid dataGrid1;
        private GroupBox groupBox4;
        private IApplication m_pApp;
        private IEngineSnapEnvironment m_pEngineSnapEnvironment;
        private IMap m_pMap;
        private ISnapEnvironment m_pSnapEnvironment;
        private TextEdit txtRadio;
    }
}