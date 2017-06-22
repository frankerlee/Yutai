using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ControlExtend;
using FileStream = System.IO.FileStream;

namespace Yutai.ArcGIS.Common.Raster.UI
{
  
    partial class RasterAdjustPointsTablePage
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
            System.ComponentModel.ComponentResourceManager resources= new  System.ComponentModel.ComponentResourceManager(typeof(RasterAdjustPointsTablePage));
            this.LinkPointlist = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.lvcolumnHeader_3 = new LVColumnHeader();
            this.lvcolumnHeader_4 = new LVColumnHeader();
            this.checkEditAutoAdjust = new CheckEdit();
            this.btnDelete = new SimpleButton();
            this.btnLoadLinkPoint = new SimpleButton();
            this.button1 = new SimpleButton();
            this.btnSave = new SimpleButton();
            this.checkEditAutoAdjust.Properties.BeginInit();
            base.SuspendLayout();
            this.LinkPointlist.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2, this.lvcolumnHeader_3, this.lvcolumnHeader_4 });
            this.LinkPointlist.ComboBoxBgColor = Color.LightBlue;
            this.LinkPointlist.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.LinkPointlist.EditBgColor = Color.White;
            this.LinkPointlist.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.LinkPointlist.FullRowSelect = true;
            this.LinkPointlist.GridLines = true;
            this.LinkPointlist.Location = new System.Drawing.Point(8, 8);
            this.LinkPointlist.LockRowCount = 0;
            this.LinkPointlist.Name = "LinkPointlist";
            this.LinkPointlist.Size = new Size(424, 136);
            this.LinkPointlist.TabIndex = 5;
            this.LinkPointlist.View = View.Details;
            this.LinkPointlist.KeyPress += new KeyPressEventHandler(this.LinkPointlist_KeyPress);
            this.LinkPointlist.ValueChanged += new ValueChangedHandler(this.method_3);
            this.LinkPointlist.SelectedIndexChanged += new EventHandler(this.LinkPointlist_SelectedIndexChanged);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "链接";
            this.lvcolumnHeader_0.Width = 39;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "影像源X";
            this.lvcolumnHeader_1.Width = 82;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_2.Text = "影像源Y";
            this.lvcolumnHeader_2.Width = 86;
            this.lvcolumnHeader_3.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_3.Text = "地图X";
            this.lvcolumnHeader_3.Width = 106;
            this.lvcolumnHeader_4.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_4.Text = "地图Y";
            this.lvcolumnHeader_4.Width = 105;
            this.checkEditAutoAdjust.Location = new System.Drawing.Point(8, 152);
            this.checkEditAutoAdjust.Name = "checkEditAutoAdjust";
            this.checkEditAutoAdjust.Properties.Caption = "自动调整";
            this.checkEditAutoAdjust.Size = new Size(88, 19);
            this.checkEditAutoAdjust.TabIndex = 7;
            this.checkEditAutoAdjust.Click += new EventHandler(this.checkEditAutoAdjust_Click);
            this.checkEditAutoAdjust.CheckedChanged += new EventHandler(this.checkEditAutoAdjust_CheckedChanged);
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new System.Drawing.Point(440, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnLoadLinkPoint.Location = new System.Drawing.Point(112, 152);
            this.btnLoadLinkPoint.Name = "btnLoadLinkPoint";
            this.btnLoadLinkPoint.Size = new Size(48, 24);
            this.btnLoadLinkPoint.TabIndex = 10;
            this.btnLoadLinkPoint.Text = "装入";
            this.btnLoadLinkPoint.Click += new EventHandler(this.btnLoadLinkPoint_Click);
            this.button1.Location = new System.Drawing.Point(176, 152);
            this.button1.Name = "button1";
            this.button1.Size = new Size(48, 24);
            this.button1.TabIndex = 11;
            this.button1.Text = "配准";
            this.button1.Click += new EventHandler(this.button1_Click);
            this.btnSave.Location = new System.Drawing.Point(240, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(56, 24);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存变化";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            base.Controls.Add(this.btnSave);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnLoadLinkPoint);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.checkEditAutoAdjust);
            base.Controls.Add(this.LinkPointlist);
            base.Name = "RasterAdjustPointsTablePage";
            base.Size = new Size(472, 192);
            base.Load += new EventHandler(this.RasterAdjustPointsTablePage_Load);
            this.checkEditAutoAdjust.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnDelete;
        private SimpleButton btnLoadLinkPoint;
        private SimpleButton btnSave;
        private SimpleButton button1;
        private CheckEdit checkEditAutoAdjust;
        private EditListView LinkPointlist;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private LVColumnHeader lvcolumnHeader_3;
        private LVColumnHeader lvcolumnHeader_4;
    }
}