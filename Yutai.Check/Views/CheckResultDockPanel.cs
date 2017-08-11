using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Check.Classes;
using Yutai.Check.Controls;
using Yutai.Pipeline.Editor.Helper;
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
        private bool _displayName;
        private bool _displayRemarks;

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

        public bool DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public bool DisplayRemarks
        {
            get { return _displayRemarks; }
            set { _displayRemarks = value; }
        }

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
            _gridView.DisplayRemarks = _displayRemarks;
            _gridView.IsSelect = toolSelected.Checked;
            _gridView.IsPanTo = toolPanTo.Checked;
            _gridView.IsZoomTo = toolZoomTo.Checked;
            _gridView.Dock = DockStyle.Fill;
            _gridView.Map = _context.FocusMap;
            _gridView.FeatureLayer = _featureLayer;
            _gridView.Grid.DataSource = ConvertToExpandoObjectList(_featureItems);
            _gridView.BestFitColumns();
            mainPanel.Controls.Add(_gridView as Control);
        }

        public List<ExpandoObject> ConvertToExpandoObjectList(List<FeatureItem> featureItems)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            foreach (FeatureItem featureItem in featureItems)
            {
                var expandoObject = new ExpandoObject() as IDictionary<string, System.Object>;
                expandoObject.Add("[编号]", featureItem.OID);
                if (_displayName)
                    expandoObject.Add("[信息]", featureItem.Name);

                IFields fields = featureItem.MainFeature.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.Field[i];
                    string strGeometry = FeatureClassUtil.GetShapeString(featureItem.MainFeature);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        expandoObject.Add(field.AliasName, strGeometry);
                    }
                    else if (field.Type != esriFieldType.esriFieldTypeBlob)
                    {
                        expandoObject.Add(field.AliasName, featureItem.MainFeature.Value[i]);
                    }
                    else
                    {
                        expandoObject.Add(field.AliasName, "二进制数据");
                    }
                }
                if (_displayRemarks)
                    expandoObject.Add("[备注]", featureItem.Remarks);

                list.Add(expandoObject as ExpandoObject);
            }

            return list;
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
        
        private void toolSelected_Click(object sender, EventArgs e)
        {
            if (_gridView == null)
                return;
            _gridView.IsSelect = toolSelected.Checked;
        }

        private void toolPanTo_Click(object sender, EventArgs e)
        {
            if (_gridView == null)
                return;
            _gridView.IsPanTo = toolPanTo.Checked;
        }

        private void toolZoomTo_Click(object sender, EventArgs e)
        {
            if (_gridView == null)
                return;
            _gridView.IsZoomTo = toolZoomTo.Checked;
        }
    }
}
