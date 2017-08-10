using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Check.Classes;
using Yutai.Check.Controls;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Check.Views
{
    public partial class CheckResultDockPanel : DockPanelControlBase, ICheckResultView
    {
        private readonly IAppContext _context;
        private List<FeatureItem> _featureItems;
        private IFeatureLayer _featureLayer;
        private IGridView _gridView;

        public CheckResultDockPanel(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public override string Caption
        {
            get { return "检查结果"; }
            set { Caption = value; }
        }
        public override DockPanelState DefaultDock => DockPanelState.Bottom;

        public override string DockName => DefaultDockName;

        public override string DefaultNestDockName => "";

        public const string DefaultDockName = "Check_ResultView";

        public List<FeatureItem> FeatureItems
        {
            get { return _featureItems; }
            set { _featureItems = value; }
        }

        public IFeatureLayer FeatureLayer
        {
            set { _featureLayer = value; }
        }

        public void Initialize(IAppContext context)
        {
        }

        public void ReloadData()
        {
            mainPanel.Controls.Clear();
            _gridView = new GridControlView();
            _gridView.Dock = DockStyle.Fill;
            _gridView.Map = _context.FocusMap;
            _gridView.FeatureLayer = _featureLayer;
            _gridView.Grid.DataSource = _featureItems;

            mainPanel.Controls.Add(_gridView as Control);
        }
        
        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter =
                    @"Excel 2003(.xls)|*.xls|Excel 2010(.xlsx)|*.xlsx|RichText(.rtf)|*.rtf|Pdf File(.pdf)|*.pdf|Html File(.html)|*.html|Mht File(.mht)|*.mht";
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveFileDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            _gridView.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            _gridView.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            _gridView.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            _gridView.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            _gridView.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            _gridView.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            if (DialogResult.Yes == MessageBox.Show(@"文件已成功导出，是否打开文件?", @"提示", MessageBoxButtons.YesNo))
                            {
                                System.Diagnostics.Process.Start(exportFilePath);
                            }
                        }
                        catch
                        {
                            String msg = "文件打开失败。" + Environment.NewLine + Environment.NewLine + "路径: " + exportFilePath;
                            MessageBox.Show(msg, @"错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "文件保存失败。" + Environment.NewLine + Environment.NewLine + "路径: " + exportFilePath;
                        MessageBox.Show(msg, @"错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
