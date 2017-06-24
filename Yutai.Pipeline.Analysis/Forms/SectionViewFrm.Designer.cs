using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class SectionViewFrm
    {
		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.icontainer_0 != null))
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SectionViewFrm));
			this.menuStrip1 = new MenuStrip();
			this.文件FToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripMenuItem();
			this.toolStripMenuItem3 = new ToolStripMenuItem();
			this.页面设置PToolStripMenuItem = new ToolStripMenuItem();
			this.打印预览VToolStripMenuItem = new ToolStripMenuItem();
			this.打印PToolStripMenuItem = new ToolStripMenuItem();
			this.退出XToolStripMenuItem = new ToolStripMenuItem();
			this.游览ToolStripMenuItem = new ToolStripMenuItem();
			this.选择SToolStripMenuItem = new ToolStripMenuItem();
			this.放大ZToolStripMenuItem = new ToolStripMenuItem();
			this.缩小XToolStripMenuItem = new ToolStripMenuItem();
			this.全图VToolStripMenuItem = new ToolStripMenuItem();
			this.漫游PToolStripMenuItem = new ToolStripMenuItem();
			this.MagnifierItem = new ToolStripMenuItem();
			this.设置SToolStripMenuItem = new ToolStripMenuItem();
			this.选项OToolStripMenuItem = new ToolStripMenuItem();
			this.printDocument_0 = new PrintDocument();
			this.pageSetupDialog_0 = new PageSetupDialog();
			this.printPreviewDialog1 = new PrintPreviewDialog();
			this.printDialog_0 = new PrintDialog();
			this.openFileDialog_0 = new OpenFileDialog();
			this.saveFileDialog_0 = new SaveFileDialog();
			this.OutPicMenuItem = new ToolStripMenuItem();
			this.OutPicItem = new ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.AutoSize = false;
			this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.文件FToolStripMenuItem, this.游览ToolStripMenuItem, this.设置SToolStripMenuItem, this.OutPicItem });
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(731, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.文件FToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.页面设置PToolStripMenuItem, this.OutPicMenuItem, this.打印预览VToolStripMenuItem, this.打印PToolStripMenuItem, this.退出XToolStripMenuItem });
			
			this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
			this.文件FToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.文件FToolStripMenuItem.Text = "文件(&F)";
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem1.Text = "打开断面图(&O)";
			this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem2.Text = "保存断面图(&A)";
			this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem3.Text = "比例尺设置(&C)";
			this.toolStripMenuItem3.Click += new EventHandler(this.toolStripMenuItem3_Click);
			this.页面设置PToolStripMenuItem.Name = "页面设置PToolStripMenuItem";
			this.页面设置PToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.页面设置PToolStripMenuItem.Text = "页面设置(&S)";
			this.页面设置PToolStripMenuItem.Click += new EventHandler(this.页面设置PToolStripMenuItem_Click);
			this.打印预览VToolStripMenuItem.Name = "打印预览VToolStripMenuItem";
			this.打印预览VToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.打印预览VToolStripMenuItem.Text = "打印预览(&V)";
			this.打印预览VToolStripMenuItem.Click += new EventHandler(this.打印预览VToolStripMenuItem_Click);
			this.打印PToolStripMenuItem.Name = "打印PToolStripMenuItem";
			this.打印PToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.打印PToolStripMenuItem.Text = "打印(&P)";
			this.打印PToolStripMenuItem.Click += new EventHandler(this.打印PToolStripMenuItem_Click);
			this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
			this.退出XToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.退出XToolStripMenuItem.Text = "退出(&X)";
			this.退出XToolStripMenuItem.Click += new EventHandler(this.退出XToolStripMenuItem_Click);
			this.游览ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.选择SToolStripMenuItem, this.放大ZToolStripMenuItem, this.缩小XToolStripMenuItem, this.全图VToolStripMenuItem, this.漫游PToolStripMenuItem, this.MagnifierItem });
			
			this.游览ToolStripMenuItem.Name = "游览ToolStripMenuItem";
			this.游览ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.游览ToolStripMenuItem.Text = "浏览(&V)";
			this.选择SToolStripMenuItem.Name = "选择SToolStripMenuItem";
			this.选择SToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.选择SToolStripMenuItem.Text = "选择(&S)";
			this.选择SToolStripMenuItem.Click += new EventHandler(this.选择SToolStripMenuItem_Click);
			this.放大ZToolStripMenuItem.Name = "放大ZToolStripMenuItem";
			this.放大ZToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.放大ZToolStripMenuItem.Text = "放大(&Z)";
			this.放大ZToolStripMenuItem.Click += new EventHandler(this.放大ZToolStripMenuItem_Click);
			this.缩小XToolStripMenuItem.Name = "缩小XToolStripMenuItem";
			this.缩小XToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.缩小XToolStripMenuItem.Text = "缩小(&X)";
			this.缩小XToolStripMenuItem.Click += new EventHandler(this.缩小XToolStripMenuItem_Click);
			this.全图VToolStripMenuItem.Name = "全图VToolStripMenuItem";
			this.全图VToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.全图VToolStripMenuItem.Text = "全图(&V)";
			this.全图VToolStripMenuItem.Click += new EventHandler(this.全图VToolStripMenuItem_Click);
			this.漫游PToolStripMenuItem.Name = "漫游PToolStripMenuItem";
			this.漫游PToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.漫游PToolStripMenuItem.Text = "漫游(&P)";
			this.漫游PToolStripMenuItem.Click += new EventHandler(this.漫游PToolStripMenuItem_Click);
			this.MagnifierItem.Name = "MagnifierItem";
			this.MagnifierItem.Size = new System.Drawing.Size(112, 22);
			this.MagnifierItem.Text = "放大镜";
			this.MagnifierItem.Click += new EventHandler(this.MagnifierItem_Click);
			this.设置SToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.选项OToolStripMenuItem });
			this.设置SToolStripMenuItem.Name = "设置SToolStripMenuItem";
			this.设置SToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.设置SToolStripMenuItem.Text = "设置(&S)";
			this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
			this.选项OToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.选项OToolStripMenuItem.Text = "选项(&O)";
			this.选项OToolStripMenuItem.Click += new EventHandler(this.选项OToolStripMenuItem_Click);
			this.printDocument_0.PrintPage += new PrintPageEventHandler(this.printDocument_0_PrintPage);
			this.pageSetupDialog_0.Document = this.printDocument_0;
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Document = this.printDocument_0;
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = (System.Drawing.Icon)resources.GetObject("printPreviewDialog1.Icon");
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.UseAntiAlias = true;
			this.printPreviewDialog1.Visible = false;
			this.printPreviewDialog1.Load += new EventHandler(this.printPreviewDialog1_Load);
			this.printPreviewDialog1.FormClosed += new FormClosedEventHandler(this.printPreviewDialog1_FormClosed);
			this.printDialog_0.AllowCurrentPage = true;
			this.printDialog_0.Document = this.printDocument_0;
			this.printDialog_0.UseEXDialog = true;
			this.openFileDialog_0.FileName = "openFileDialog1";
			this.openFileDialog_0.Filter = "断面文件(*.xml)|*.xml";
			this.saveFileDialog_0.Filter = "断面文件(*.xml)|*.xml";
			this.OutPicMenuItem.Name = "OutPicMenuItem";
			this.OutPicMenuItem.Size = new System.Drawing.Size(152, 22);
			this.OutPicMenuItem.Text = "输出图片";
			this.OutPicMenuItem.Click += new EventHandler(this.OutPicMenuItem_Click);
			this.OutPicItem.Name = "OutPicItem";
			this.OutPicItem.Size = new System.Drawing.Size(65, 20);
			this.OutPicItem.Text = "输出图片";
			this.OutPicItem.Click += new EventHandler(this.OutPicItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(731, 514);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$Icon");
			base.Location = new System.Drawing.Point(100, 100);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "SectionViewFrm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "SectionViewFrm";
			base.Load += new EventHandler(this.SectionViewFrm_Load);
			base.MouseUp += new MouseEventHandler(this.SectionViewFrm_MouseUp);
			base.ClientSizeChanged += new EventHandler(this.SectionViewFrm_ClientSizeChanged);
			base.Paint += new PaintEventHandler(this.SectionViewFrm_Paint);
			base.FormClosed += new FormClosedEventHandler(this.SectionViewFrm_FormClosed);
			base.MouseDown += new MouseEventHandler(this.SectionViewFrm_MouseDown);
			base.KeyPress += new KeyPressEventHandler(this.SectionViewFrm_KeyPress);
			base.Resize += new EventHandler(this.SectionViewFrm_Resize);
			base.KeyDown += new KeyEventHandler(this.SectionViewFrm_KeyDown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
		}

	
		private PrintDocument printDocument_0;
		private PageSetupDialog pageSetupDialog_0;
		private PrintPreviewDialog printPreviewDialog1;
		private PrintDialog printDialog_0;
		private OpenFileDialog openFileDialog_0;
		
		private SectionInfo sectionInfo_0;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem 文件FToolStripMenuItem;
		private ToolStripMenuItem 退出XToolStripMenuItem;
		private ToolStripMenuItem 游览ToolStripMenuItem;
		private ToolStripMenuItem 选择SToolStripMenuItem;
		private ToolStripMenuItem 放大ZToolStripMenuItem;
		private ToolStripMenuItem 缩小XToolStripMenuItem;
		private ToolStripMenuItem 全图VToolStripMenuItem;
		private ToolStripMenuItem 漫游PToolStripMenuItem;
		private ToolStripMenuItem 页面设置PToolStripMenuItem;
		private ToolStripMenuItem 打印预览VToolStripMenuItem;
		private ToolStripMenuItem 打印PToolStripMenuItem;
		private ToolStripMenuItem 设置SToolStripMenuItem;
		private ToolStripMenuItem 选项OToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem toolStripMenuItem2;
		private SaveFileDialog saveFileDialog_0;
		private ToolStripMenuItem toolStripMenuItem3;
		private ToolStripMenuItem MagnifierItem;
		private ToolStripMenuItem OutPicMenuItem;
		private ToolStripMenuItem OutPicItem;
    }
}