using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmOpenFile
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2)
            {
                if (this.icontainer_0 != null)
                {
                    this.icontainer_0.Dispose();
                }
                this.igxCatalog_0 = null;
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOpenFile));
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.folderBrowserDialog_0 = new FolderBrowserDialog();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtName = new TextEdit();
            this.btnAdd = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnUpper = new SimpleButton();
            this.imageComboBoxEdit1 = new ImageComboBoxEditEx();
            this.cboShowType = new ComboBoxEdit();
            this.btnLargeIcon = new SimpleButton();
            this.btnList = new SimpleButton();
            this.btnDetial = new SimpleButton();
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.txtName.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.cboShowType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找";
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList_0;
            this.listView1.Location = new System.Drawing.Point(8, 40);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(472, 176);
            this.listView1.SmallImageList = this.imageList_0;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyUp += new KeyEventHandler(this.listView1_KeyUp);
            this.listView1.Click += new EventHandler(this.listView1_Click);
            this.columnHeader_0.Text = "名字";
            this.columnHeader_0.Width = 214;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 207;
            this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_0.ImageSize = new Size(16, 16);
            this.imageList_0.TransparentColor = Color.Magenta;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 232);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "名字";
            this.label3.Location = new System.Drawing.Point(8, 256);
            this.label3.Name = "label3";
            this.label3.Size = new Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "显示类型";
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(80, 224);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(288, 21);
            this.txtName.TabIndex = 6;
            this.btnAdd.Location = new System.Drawing.Point(392, 224);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(56, 24);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnUpper.Image = (Image) resources.GetObject("btnUpper.Image");
            this.btnUpper.Location = new System.Drawing.Point(360, 8);
            this.btnUpper.Name = "btnUpper";
            this.btnUpper.Size = new Size(24, 24);
            this.btnUpper.TabIndex = 10;
            this.btnUpper.ToolTip = "上一级";
            this.btnUpper.Click += new EventHandler(this.btnUpper_Click);
            this.imageComboBoxEdit1.EditValue = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(56, 8);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(296, 21);
            this.imageComboBoxEdit1.TabIndex = 11;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            this.cboShowType.EditValue = "";
            this.cboShowType.Location = new System.Drawing.Point(80, 256);
            this.cboShowType.Name = "cboShowType";
            this.cboShowType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboShowType.Size = new Size(288, 21);
            this.cboShowType.TabIndex = 12;
            this.cboShowType.SelectedIndexChanged += new EventHandler(this.cboShowType_SelectedIndexChanged);
            this.btnLargeIcon.Image = (Image) resources.GetObject("btnLargeIcon.Image");
            this.btnLargeIcon.Location = new System.Drawing.Point(400, 8);
            this.btnLargeIcon.Name = "btnLargeIcon";
            this.btnLargeIcon.Size = new Size(24, 24);
            this.btnLargeIcon.TabIndex = 13;
            this.btnLargeIcon.ToolTip = "大图标";
            this.btnLargeIcon.Click += new EventHandler(this.btnLargeIcon_Click);
            this.btnList.Image = (Image) resources.GetObject("btnList.Image");
            this.btnList.Location = new System.Drawing.Point(424, 8);
            this.btnList.Name = "btnList";
            this.btnList.Size = new Size(24, 24);
            this.btnList.TabIndex = 14;
            this.btnList.ToolTip = "列表";
            this.btnList.Click += new EventHandler(this.btnList_Click);
            this.btnDetial.Image = (Image) resources.GetObject("btnDetial.Image");
            this.btnDetial.Location = new System.Drawing.Point(448, 8);
            this.btnDetial.Name = "btnDetial";
            this.btnDetial.Size = new Size(24, 24);
            this.btnDetial.TabIndex = 15;
            this.btnDetial.ToolTip = "详细信息";
            this.btnDetial.Click += new EventHandler(this.btnDetial_Click);
            this.imageList_1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_1.ImageSize = new Size(16, 16);
            this.imageList_1.TransparentColor = Color.Transparent;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(490, 287);
            base.Controls.Add(this.btnDetial);
            base.Controls.Add(this.btnList);
            base.Controls.Add(this.btnLargeIcon);
            base.Controls.Add(this.cboShowType);
            base.Controls.Add(this.imageComboBoxEdit1);
            base.Controls.Add(this.btnUpper);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmOpenFile";
            base.ShowInTaskbar = false;
            base.Load += new EventHandler(this.frmOpenFile_Load);
            this.txtName.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            this.cboShowType.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnCancel;
        private SimpleButton btnDetial;
        private SimpleButton btnLargeIcon;
        private SimpleButton btnList;
        private SimpleButton btnUpper;
        private ComboBoxEdit cboShowType;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private FolderBrowserDialog folderBrowserDialog_0;
        private IContainer icontainer_0;
        private IGxCatalog igxCatalog_0;
        private ImageComboBoxEditEx imageComboBoxEdit1;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListView listView1;
        private TextEdit txtName;
    }
}