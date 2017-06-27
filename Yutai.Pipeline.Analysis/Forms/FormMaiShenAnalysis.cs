using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class FormMaiShenAnalysis : XtraForm
	{
		private IContainer icontainer_0 = null;

	    private IPipelineConfig _config;

        public RuleMs m_RuleMs ;//= new RuleMs();

		public int m_nTimerCount;

	    private DataTable _table;
	    private IGeometry _curShape;


	    public FormMaiShenAnalysis(IAppContext context,IPipelineConfig config)
		{
			this.InitializeComponent();
		    _context = context;
		    _config = config;
            m_RuleMs=new RuleMs(config);

        }

		private void FormMaiShenAnalysis_Load(object obj, EventArgs eventArgs)
		{
			IFeature feature = ((IEnumFeature)_context.FocusMap.FeatureSelection).Next();
			//if (feature == null || feature.Shape.GeometryType != (esriGeometryType) 4)
			//{
			//	this.chkRegionAnalysis.Visible = false;
			//}
			ArrayList arrayList = new ArrayList();
			CMapOperator.GetMapILayers(_context.FocusMap, null, arrayList);
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (arrayList[i] is IFeatureLayer)
				{
					IFeatureLayer featureLayer = (IFeatureLayer)arrayList[i];
					if (_config.IsPipelineLayer(featureLayer.Name,enumPipelineDataType.Line))
					{
                        CheckListFeatureLayerItem class1 = new CheckListFeatureLayerItem();
						class1.m_pFeatureLayer = featureLayer;
						this.checkedListBox1.Items.Add(class1, true);
					}
				}
			}
		}

		private void btnAnalysis_Click(object obj, EventArgs eventArgs)
		{
		    if (cmbDepthType.SelectedIndex < 0)
		    {
		        MessageService.Current.Warn("请先选择埋深数据类型再进行分析!");
		        return;
		    }
			this.gridView1.Columns.Clear();
			//this.gridControl1.DataRowCount=0;//.Rows.Clear();
		    IQueryFilter queryFilter = null;
		    bool isM = cmbDepthType.SelectedIndex > 0 ? true : false;
		    if (chkRegionAnalysis.Checked)
		    {
		        ISpatialFilter spatialFilter=new SpatialFilterClass();
		        spatialFilter.Geometry = _context.ActiveView.Extent;
                spatialFilter.SpatialRel= esriSpatialRelEnum.esriSpatialRelIntersects;
                queryFilter=spatialFilter as IQueryFilter;
		    }
		    _table = null;
			foreach (CheckListFeatureLayerItem pclass in this.checkedListBox1.CheckedItems)
			{
				IFeatureLayer ifeatureLayer = pclass.m_pFeatureLayer;
				this.progressBar1.Visible = true;
				this.progressBar1.Maximum = ifeatureLayer.FeatureClass.FeatureCount(queryFilter);
				this.progressBar1.Step = 1;
				this.progressBar1.Value = 0;
				this.Text = "覆土分析 - 正在处理：" + ifeatureLayer.Name+ "...";
			    IPipelineLayer pipelineLayer = _config.GetPipelineLayer(ifeatureLayer.FeatureClass);
			    IBasicLayerInfo pipeLine = _config.GetBasicLayerInfo(ifeatureLayer.FeatureClass) as IBasicLayerInfo;


                string lineConfig_Kind = pipelineLayer.Code;
                
                string sDepthMethod = "";
				string sDepPosition = "";
				int num = ifeatureLayer.FeatureClass.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.MSFS));
                //基本上没有使用，因为所在位置没有数据
				int num2 = ifeatureLayer.FeatureClass.Fields.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.SZWZ));
				IFeatureCursor featureCursor = ifeatureLayer.Search(queryFilter, false);
			    int qdmsIdx = featureCursor.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.QDMS));
                int zdmsIdx = featureCursor.FindField(pipeLine.GetFieldName(PipeConfigWordHelper.LineWords.ZDMS));

			    BuildTable(featureCursor);
                IFeature feature = featureCursor.NextFeature();
				while (feature != null)
				{
					this.progressBar1.Value= this.progressBar1.Value+1;
					if (feature.Shape == null || feature.Shape.GeometryType !=esriGeometryType.esriGeometryPolyline )
					{
						feature = featureCursor.NextFeature();
					}
					else
					{
						if (num != -1)
						{
							sDepthMethod = feature.get_Value(num).ToString();
						}
						if (num2 != -1)
						{
							sDepPosition = feature.get_Value(num2).ToString();
						}
						double ruleMS = this.m_RuleMs.GetRuleMS(lineConfig_Kind, sDepthMethod, sDepPosition);
					    if (isM)
					    {
					        IPoint point = ((IPointCollection) feature.Shape).get_Point(0);
					        IPoint point2 = ((IPointCollection) feature.Shape).get_Point(1);
					        if (point.M < ruleMS || point2.M < ruleMS)
					        {
					            this.FillFeatureValue(pipeLine, lineConfig_Kind, feature, ruleMS, point.M, point2.M);
					        }
					    }
					    else
					    {
					        double qdms = qdmsIdx >= 0 ? Convert.ToDouble(feature.Value[qdmsIdx]) : 0;
                            double zdms = qdmsIdx >= 0 ? Convert.ToDouble(feature.Value[zdmsIdx]) : 0;
                            if (qdms < ruleMS || zdms < ruleMS)
                            {
                                this.FillFeatureValue(pipeLine, lineConfig_Kind, feature, ruleMS, qdms, zdms);
                            }
                        }
					    feature = featureCursor.NextFeature();
					}
				}
				Marshal.ReleaseComObject(featureCursor);
				this.progressBar1.Visible = false;
			    this.gridControl1.DataSource = _table;
			}
			this.Text = "覆土分析--记录数:" + _table.Rows.Count.ToString();
		}

	    private void BuildTable(IFeatureCursor pCursor)
	    {
	        if (_table == null)
	        {
	            _table=new DataTable("分析结果");
	            _table.Columns.Add("规范埋深");
                _table.Columns.Add("起点埋深");
                _table.Columns.Add("终点埋深");
                _table.Columns.Add("管线类别", typeof(string));
                _table.Columns.Add("管线名称", typeof(string));
                _table.Columns.Add("对象");
            }

            Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
            for (int i = 0; i < pCursor.Fields.FieldCount; i++)
            {
                string name = pCursor.Fields.get_Field(i).Name;
                if (_table.Columns[name] == null && regex.IsMatch(name))
                {
                    _table.Columns.Add(name);
                }

            }
        }
		private void FillFeatureValue(IBasicLayerInfo pipeLine,string value, IFeature feature, double num, double num2, double num3)
		{
            Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");

		    DataRow pRow = _table.NewRow();
		    pRow["管线名称"] = pipeLine.Name;
            pRow["规范埋深"] = string.Format("{0:f2}", num);
            pRow["起点埋深"] = string.Format("{0:f2}", num2);
            pRow["终点埋深"] = string.Format("{0:f2}", num3);
            pRow["管线类别"] = value;
            pRow["对象"] = feature.Shape;
         
            for (int j = 0; j < feature.Fields.FieldCount; j++)
			{
                
				string name2 = feature.Fields.get_Field(j).Name;
				if (regex.IsMatch(name2))
				{
				    pRow[name2]= feature.get_Value(j);
				}
			}
		    _table.Rows.Add(pRow);
		    // this.gridView1.Columns["ObjShape"].Visible = false;
		}

		private void btnExit_Click(object obj, EventArgs eventArgs)
		{
			base.Close();
		}

		private void btnSelctAll_Click(object obj, EventArgs eventArgs)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, true);
			}
		}

		private void btnSelectReverse_Click(object obj, EventArgs eventArgs)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, !this.checkedListBox1.GetItemChecked(i));
			}
		}

		private void btnSelectNon_Click(object obj, EventArgs eventArgs)
		{
			for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
			{
				this.checkedListBox1.SetItemChecked(i, false);
			}
		}

		private void btnExport_Click(object obj, EventArgs eventArgs)
		{
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            gridControl1.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            gridControl1.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            gridControl1.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            gridControl1.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            gridControl1.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            gridControl1.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

		private void dataGridView1_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArgs)
		{
			//if (dataGridViewCellEventArgs.RowIndex >= 0)
			//{
			//	IFeature feature = (IFeature)this.gridView1.Rows[dataGridViewCellEventArgs.RowIndex].Tag;
			//	if (feature != null)
			//	{
			//		IEnvelope extent = _context.ActiveView.Extent;
			//		IEnvelope arg_B5_0 = extent;
			//		IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
			//		pointClass.X=((feature.Shape.Envelope.XMin + feature.Shape.Envelope.XMax) / 2.0);
			//		pointClass.Y=((feature.Shape.Envelope.YMin + feature.Shape.Envelope.YMax) / 2.0);
			//		arg_B5_0.CenterAt(pointClass);
			//		_context.MapControl.Extent=(extent);
			//		this.ifeature_0 = feature;
			//		this.timer_0.Start();
			//		this.timer_0.Interval = 100;
			//		_context.ActiveView.Refresh();
			//		this.m_nTimerCount = 0;
			//	}
			//}
		}

		private void FormMaiShenAnalysis_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "覆土分析";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}

		private void timer_0_Tick(object obj, EventArgs eventArgs)
		{
			if (!base.Visible || this.m_nTimerCount > 1)
			{
				this.m_nTimerCount = 0;
				this.timer_0.Stop();
				IActiveView activeView = _context.ActiveView;
				activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			}
			CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, this._curShape);
			this.m_nTimerCount++;
		}

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            IGeometry feature = this.gridView1.GetRowCellValue(e.RowHandle, "对象") as IGeometry;
            if (feature != null)
            {
                IEnvelope extent = _context.ActiveView.Extent;
                IEnvelope arg_B5_0 = extent;
                IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                pointClass.X = ((feature.Envelope.XMin + feature.Envelope.XMax) / 2.0);
                pointClass.Y = ((feature.Envelope.YMin + feature.Envelope.YMax) / 2.0);
                arg_B5_0.CenterAt(pointClass);
                _context.MapControl.Extent = (extent);
                this._curShape = feature;
                this.timer_0.Start();
                this.timer_0.Interval = 100;
                _context.ActiveView.Refresh();
                this.m_nTimerCount = 0;
            }
        }
    }
}
