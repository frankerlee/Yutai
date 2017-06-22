using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class ToplogyLayerSymbolCtrl
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.chkListRender = new CheckedListBoxControl();
            this.label1 = new Label();
            this.rdoRenderType = new RadioGroup();
            this.btnSymbol = new StyleButton();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.imageList_0 = new ImageList(this.icontainer_0);
            ((ISupportInitialize) this.chkListRender).BeginInit();
            this.rdoRenderType.Properties.BeginInit();
            base.SuspendLayout();
            this.chkListRender.ItemHeight = 17;
            this.chkListRender.Location = new Point(8, 40);
            this.chkListRender.Name = "chkListRender";
            this.chkListRender.Size = new Size(112, 208);
            this.chkListRender.TabIndex = 0;
            this.chkListRender.SelectedIndexChanged += new EventHandler(this.chkListRender_SelectedIndexChanged);
            this.chkListRender.ItemCheck += new ItemCheckEventHandler(this.chkListRender_ItemCheck);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "显示";
            this.rdoRenderType.Location = new Point(128, -32);
            this.rdoRenderType.Name = "rdoRenderType";
            this.rdoRenderType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoRenderType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoRenderType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoRenderType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "单一符号"), new RadioGroupItem(null, "根据错误类型符号化") });
            this.rdoRenderType.Size = new Size(176, 184);
            this.rdoRenderType.TabIndex = 2;
            this.rdoRenderType.SelectedIndexChanged += new EventHandler(this.rdoRenderType_SelectedIndexChanged);
            this.btnSymbol.Location = new Point(144, 32);
            this.btnSymbol.Name = "btnSymbol";
            this.btnSymbol.Size = new Size(120, 48);
            this.btnSymbol.Style = null;
            this.btnSymbol.TabIndex = 3;
            this.btnSymbol.Click += new EventHandler(this.btnSymbol_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(136, 120);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(224, 128);
            this.listView1.SmallImageList = this.imageList_0;
            this.listView1.TabIndex = 4;
            this.listView1.View = View.List;
            this.columnHeader_0.Width = 360;
            this.imageList_0.ImageSize = new Size(50, 28);
            this.imageList_0.TransparentColor = Color.Transparent;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnSymbol);
            base.Controls.Add(this.rdoRenderType);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.chkListRender);
            base.Name = "ToplogyLayerSymbolCtrl";
            base.Size = new Size(424, 280);
            base.Load += new EventHandler(this.ToplogyLayerSymbolCtrl_Load);
            ((ISupportInitialize) this.chkListRender).EndInit();
            this.rdoRenderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnSymbol;
        private CheckedListBoxControl chkListRender;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private Label label1;
        private RenderInfoListView listView1;
        private RadioGroup rdoRenderType;
    }
}