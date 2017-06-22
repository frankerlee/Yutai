using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Controls.ControlExtend
{
    partial class RenderInfoListView
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
            this.components = new Container();
            this.sImageList = new ImageList(this.components);
            this.lImageList = new ImageList(this.components);
            this.textBox = new TextBox();
            base.SuspendLayout();
            this.sImageList.ColorDepth = ColorDepth.Depth8Bit;
            this.sImageList.ImageSize = new Size(16, 16);
            this.sImageList.TransparentColor = Color.Transparent;
            this.lImageList.ColorDepth = ColorDepth.Depth8Bit;
            this.lImageList.ImageSize = new Size(48, 48);
            this.lImageList.TransparentColor = Color.Transparent;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Location = new System.Drawing.Point(243, 17);
            this.textBox.Name = "textBox";
            this.textBox.Size = new Size(100, 21);
            this.textBox.TabIndex = 0;
            this.textBox.Visible = false;
            this.textBox.Leave += new EventHandler(this.textBox_Leave);
            this.textBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
            base.FullRowSelect = true;
            base.LargeImageList = this.lImageList;
            base.SmallImageList = this.sImageList;
            base.DoubleClick += new EventHandler(this.RenderInfoListView_DoubleClick);
            base.SelectedIndexChanged += new EventHandler(this.SymbolListViewEx_SelectedIndexChanged);
            base.MouseDown += new MouseEventHandler(this.RenderInfoListView_MouseDown);
            base.ResumeLayout(false);
        }

       
        private IContainer components;
        private ImageList lImageList;
        private int m_nX;
        private int m_nY;
        private ListViewItem m_preListViewItem;
        private ImageList sImageList;
        private TextBox textBox;
    }
}