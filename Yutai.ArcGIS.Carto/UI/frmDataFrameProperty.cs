using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmDataFrameProperty : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IMapFrame imapFrame_0 = null;
        private MapCoordinateCtrl mapCoordinateCtrl_0 = new MapCoordinateCtrl();
        private MapDataFramePage mapDataFramePage_0 = new MapDataFramePage();
        private MapGeneralInfoCtrl mapGeneralInfoCtrl_0 = new MapGeneralInfoCtrl();
        private TabControl tabControl1;
        private TabPage tabPageCoordinate;
        private TabPage tabPageGeneral;
        private TabPage tabPageMapDataFrame;

        public frmDataFrameProperty()
        {
            this.InitializeComponent();
            this.mapGeneralInfoCtrl_0.Dock = DockStyle.Fill;
            this.tabPageGeneral.Controls.Add(this.mapGeneralInfoCtrl_0);
            this.mapCoordinateCtrl_0.Dock = DockStyle.Fill;
            this.tabPageCoordinate.Controls.Add(this.mapCoordinateCtrl_0);
            this.mapDataFramePage_0.Dock = DockStyle.Fill;
            this.tabPageMapDataFrame.Controls.Add(this.mapDataFramePage_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.mapGeneralInfoCtrl_0.Apply();
            this.mapCoordinateCtrl_0.Apply();
            this.mapDataFramePage_0.Apply();
            base.Close();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmDataFrameProperty_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataFrameProperty));
            this.tabControl1 = new TabControl();
            this.tabPageGeneral = new TabPage();
            this.tabPageCoordinate = new TabPage();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.tabPageMapDataFrame = new TabPage();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageCoordinate);
            this.tabControl1.Controls.Add(this.tabPageMapDataFrame);
            this.tabControl1.Location = new Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(360, 0x198);
            this.tabControl1.TabIndex = 0;
            this.tabPageGeneral.Location = new Point(4, 0x16);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new Size(0x160, 0x17e);
            this.tabPageGeneral.TabIndex = 1;
            this.tabPageGeneral.Text = "常规";
            this.tabPageCoordinate.Location = new Point(4, 0x16);
            this.tabPageCoordinate.Name = "tabPageCoordinate";
            this.tabPageCoordinate.Size = new Size(0x160, 0x17e);
            this.tabPageCoordinate.TabIndex = 0;
            this.tabPageCoordinate.Text = "坐标系统";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xd0, 0x1a8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 0x1a8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.tabPageMapDataFrame.Location = new Point(4, 0x16);
            this.tabPageMapDataFrame.Name = "tabPageMapDataFrame";
            this.tabPageMapDataFrame.Size = new Size(0x160, 0x17e);
            this.tabPageMapDataFrame.TabIndex = 2;
            this.tabPageMapDataFrame.Text = "数据框";
            this.tabPageMapDataFrame.UseVisualStyleBackColor = true;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x180, 0x1c5);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.Name = "frmDataFrameProperty";
            this.Text = "数据框属性";
            base.Load += new EventHandler(this.frmDataFrameProperty_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.mapGeneralInfoCtrl_0.FocusMap = value;
                this.mapCoordinateCtrl_0.FocusMap = value;
                this.mapDataFramePage_0.Map = value;
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
                this.mapGeneralInfoCtrl_0.FocusMap = this.imapFrame_0.Map as IBasicMap;
                this.mapCoordinateCtrl_0.FocusMap = this.imapFrame_0.Map as IBasicMap;
                this.mapDataFramePage_0.MapFrame = value;
            }
        }
    }
}

