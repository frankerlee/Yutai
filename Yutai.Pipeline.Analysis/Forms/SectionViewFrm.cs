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
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class SectionViewFrm : XtraForm
	{
        

		public IAppContext _context;

		public SectionViewFrm.SectionType m_sectionType;

		public Section m_pSection;

		public int m_nLeftMargin;

		public int m_nTopMargin;

		public int m_nRightMargin;

		public int m_nBottomMargin;
        private IPipelineConfig m_PipeConfig;


        private int int_0 = 739;

		private int int_1 = 541;

		private float float_0 = 1f;

		private float float_1 = 1f;

		private IContainer icontainer_0 = null;

        



		public IPipelineConfig PipeConfig
		{
			get
			{
				return this.m_PipeConfig;
			}
			set
			{
				this.m_PipeConfig = value;
			}
		}

		public SectionViewFrm(SectionViewFrm.SectionType stVal, IAppContext pApp,IPipelineConfig config)
		{
			this.m_sectionType = stVal;
			this._context = pApp;
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeTransect)
			{
				this.m_pSection = new TranSection(this, _context,config);
			}
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeVersect)
			{
				this.m_pSection = new VerSection(this, _context,config);
			}
			this.m_pSection.PipeConfig = config;
			this.InitializeComponent();
		}

	public void GetSelectedData()
		{
			this.m_pSection.GetSelectedData();
		}

	private void MagnifierItem_Click(object obj, EventArgs eventArg)
		{
			(new MagnifierMainForm()).Show();
		}

		private void method_0(object obj, EventArgs eventArg)
		{
		}

		private void method_1(object obj, EventArgs eventArg)
		{
		}

		private void method_2(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			string str1 = "";
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeTransect)
			{
				str1 = "横断面分析";
			}
			else if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeVersect)
			{
				str1 = "纵断面分析";
			}
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, str1);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
		}

		private void OutPicItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.method_0();
		}

		private void OutPicMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.method_0();
		}

		private void printDocument_0_PrintPage(object obj, PrintPageEventArgs printPageEventArg)
		{
			this.m_pSection.PrintPage(printPageEventArg);
		}

		private void printPreviewDialog1_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
			base.Visible = true;
			base.Invalidate();
		}

		private void printPreviewDialog1_Load(object obj, EventArgs eventArg)
		{
		}

		public void PutBaseLine(IPolyline pVal)
		{
			this.m_pSection.BaseLine = pVal;
		}

		private void SectionViewFrm_ClientSizeChanged(object obj, EventArgs eventArg)
		{
		}

		private void SectionViewFrm_FormClosed(object obj, FormClosedEventArgs formClosedEventArg)
		{
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeTransect)
			{
                //_context.FocusMap.MousePointer=(0);
			}
		}

		private void SectionViewFrm_KeyDown(object obj, KeyEventArgs keyEventArg)
		{
		}

		private void SectionViewFrm_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
		}

		private void SectionViewFrm_Load(object obj, EventArgs eventArg)
		{
			int height = this.menuStrip1.Height;
			this.m_pSection.OnResize(height, 0, 1f, 1f);
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeTransect)
			{
				this.Text = "横断面分析";
			}
			if (this.m_sectionType == SectionViewFrm.SectionType.SectionTypeVersect)
			{
				this.Text = "纵断面分析";
			}
		}

		private void SectionViewFrm_MouseDown(object obj, MouseEventArgs mouseEventArg)
		{
			this.m_pSection.MouseDown(mouseEventArg.Location);
			if (mouseEventArg.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.Cursor = Cursors.Default;
				this.m_pSection.Select();
			}
		}

		private void SectionViewFrm_MouseUp(object obj, MouseEventArgs mouseEventArg)
		{
			ArrayList arrayLists;
			this.m_pSection.MouseUp(mouseEventArg.Location);
			if (this.m_pSection.ActionType == Section.Section_Action.Section_Select)
			{
				this.m_pSection.SectionInfo(out arrayLists);
				if (arrayLists.Count == 0)
				{
					if (this.sectionInfo_0 != null)
					{
						this.sectionInfo_0.Visible = false;
					}
				}
				else if (this.sectionInfo_0 != null)
				{
					this.sectionInfo_0.FillData = arrayLists;
					this.sectionInfo_0.RefreshDialog();
					this.sectionInfo_0.Visible = true;
				}
				else
				{
					this.sectionInfo_0 = new SectionInfo()
					{
						FillData = arrayLists
					};
					this.sectionInfo_0.RefreshDialog();
					this.sectionInfo_0.Show(this);
				}
			}
		}

		private void SectionViewFrm_Paint(object obj, PaintEventArgs paintEventArg)
		{
			if (base.WindowState != FormWindowState.Minimized)
			{
				this.m_pSection.Paint();
			}
		}

		private void SectionViewFrm_Resize(object obj, EventArgs eventArg)
		{
			this.float_0 = (float)(this.int_0 / base.Width);
			this.float_1 = (float)(this.int_1 / base.Height);
			int height = this.menuStrip1.Height;
			this.m_pSection.OnResize(height, 0, this.float_0, this.float_1);
		}

		private void toolStripMenuItem1_Click(object obj, EventArgs eventArg)
		{
			if (this.openFileDialog_0.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				string fileName = this.openFileDialog_0.FileName;
				this.m_pSection.OpenSectionFile(fileName);
			}
		}

		private void toolStripMenuItem2_Click(object obj, EventArgs eventArg)
		{
			if (this.saveFileDialog_0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string fileName = this.saveFileDialog_0.FileName;
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
				this.m_pSection.SaveSectionFile(fileName);
			}
		}

		private void toolStripMenuItem3_Click(object obj, EventArgs eventArg)
		{
			if (PrinterSettings.InstalledPrinters.Count != 0)
			{
				SetScaleDlg setScaleDlg = new SetScaleDlg()
				{
					ScaleX = this.m_pSection.m_pSectionDisp.PrintScaleX,
					ScaleY = this.m_pSection.m_pSectionDisp.PrintScaleY
				};
				if (System.Windows.Forms.DialogResult.OK == setScaleDlg.ShowDialog(this))
				{
					this.m_pSection.m_pSectionDisp.PrintScaleX = setScaleDlg.ScaleX;
					this.m_pSection.m_pSectionDisp.PrintScaleY = setScaleDlg.ScaleY;
					this.m_pSection.m_pSectionDisp.CustomScale = true;
					base.Visible = false;
					this.printPreviewDialog1.ShowDialog();
				}
			}
			else
			{
				MessageBox.Show("您没有安装打印机！");
			}
		}

		private void 打印PToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			if (PrinterSettings.InstalledPrinters.Count == 0)
			{
				MessageBox.Show("您没有安装打印机！");
			}
			else if (this.printDialog_0.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.printDocument_0.Print();
			}
		}

		private void 打印预览VToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			if (PrinterSettings.InstalledPrinters.Count != 0)
			{
				base.Visible = false;
				PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
				this.printPreviewDialog1.ShowDialog();
			}
			else
			{
				MessageBox.Show("您没有安装打印机！");
			}
		}

		private void 放大ZToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.ZoomIn();
		}

		private void 漫游PToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.Pan();
		}

		private void 全图VToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.ViewEntire();
		}

		private void 缩小XToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.m_pSection.ZoomOut();
		}

		private void 退出XToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			base.Close();
		}

		private void 选项OToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			SectionOption sectionOption = new SectionOption()
			{
				Title = this.m_pSection.m_pSectionDisp.Title,
				RoadName = this.m_pSection.m_pSectionDisp.RoadName,
				SectionNo = this.m_pSection.m_pSectionDisp.SectionNo,
				bRotate = this.m_pSection.m_pSectionDisp.bRotate,
				bFalse = this.m_pSection.m_pSectionDisp.bFalse
			};
			if (sectionOption.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.m_pSection.m_pSectionDisp.Title = sectionOption.Title;
				this.m_pSection.m_pSectionDisp.RoadName = sectionOption.RoadName;
				this.m_pSection.m_pSectionDisp.SectionNo = sectionOption.SectionNo;
				this.m_pSection.m_pSectionDisp.bRotate = sectionOption.bRotate;
				this.m_pSection.m_pSectionDisp.bFalse = sectionOption.bFalse;
				base.Invalidate();
			}
		}

		private void 选择SToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			this.Cursor = Cursors.Default;
			this.m_pSection.Select();
		}

		private void 页面设置PToolStripMenuItem_Click(object obj, EventArgs eventArg)
		{
			if (PrinterSettings.InstalledPrinters.Count == 0)
			{
				MessageBox.Show("您没有安装打印机！");
			}
			else if (this.pageSetupDialog_0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.printPreviewDialog1.ShowDialog();
			}
		}

		public enum SectionType
		{
			SectionTypeTransect,
			SectionTypeVersect
		}
	}
}