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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class FormMaiShenAnalysis : XtraForm
	{
		private IContainer icontainer_0 = null;
        


		public RuleMs m_RuleMs = new RuleMs();

		public int m_nTimerCount;


 public FormMaiShenAnalysis(IAppContext context)
		{
			this.InitializeComponent();
		    _context = context;
		}

		private void FormMaiShenAnalysis_Load(object obj, EventArgs eventArgs)
		{
			IFeature feature = ((IEnumFeature)_context.FocusMap.FeatureSelection).Next();
			if (feature == null || feature.Shape.GeometryType != (esriGeometryType) 4)
			{
				this.chkRegionAnalysis.Visible = false;
			}
			ArrayList arrayList = new ArrayList();
			CMapOperator.GetMapILayers(_context.FocusMap, null, arrayList);
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (arrayList[i] is IFeatureLayer)
				{
					IFeatureLayer featureLayer = (IFeatureLayer)arrayList[i];
					if (_context.PipeConfig.IsPipeLine(featureLayer.FeatureClass.AliasName))
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
			this.dataGridView1.Columns.Clear();
			this.dataGridView1.Rows.Clear();
			string text = _context.PipeConfig.GetLineTableFieldName("敷设方式");
			if (text == "")
			{
				text = "敷设方式";
			}
			string text2 = _context.PipeConfig.GetLineTableFieldName("所在位置");
			if (text2 == "")
			{
				text2 = "所在位置";
			}
			foreach (CheckListFeatureLayerItem pclass in this.checkedListBox1.CheckedItems)
			{
				IFeatureLayer ifeatureLayer = pclass.m_pFeatureLayer;
				this.progressBar1.Visible = true;
				this.progressBar1.Maximum = ifeatureLayer.FeatureClass.FeatureCount(null);
				this.progressBar1.Step = 1;
				this.progressBar1.Value = 0;
				this.Text = "覆土分析 - 正在处理：" + ifeatureLayer.Name+ "...";
				string lineConfig_Kind = _context.PipeConfig.getLineConfig_Kind(ifeatureLayer.FeatureClass.AliasName);
				string sDepthMethod = "";
				string sDepPosition = "";
				int num = ifeatureLayer.FeatureClass.Fields.FindField(text);
				int num2 = ifeatureLayer.FeatureClass.Fields.FindField(text2);
				IFeatureCursor featureCursor = ifeatureLayer.Search(null, false);
				IFeature feature = featureCursor.NextFeature();
				while (feature != null)
				{
					this.progressBar1.Value= this.progressBar1.Value+1;
					if (feature.Shape == null || feature.Shape.GeometryType != (esriGeometryType) 3)
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
						IPoint point = ((IPointCollection)feature.Shape).get_Point(0);
						IPoint point2 = ((IPointCollection)feature.Shape).get_Point(1);
						if (point.M < ruleMS || point2.M < ruleMS)
						{
							this.method_0(lineConfig_Kind, feature, ruleMS, point.M, point2.M);
						}
						feature = featureCursor.NextFeature();
					}
				}
				Marshal.ReleaseComObject(featureCursor);
				this.progressBar1.Visible = false;
			}
			this.Text = "覆土分析--记录数:" + this.dataGridView1.Rows.Count.ToString();
		}

		private void method_0(string value, IFeature feature, double num, double num2, double num3)
		{
			if (!this.dataGridView1.Columns.Contains("*规范埋深"))
			{
				this.dataGridView1.Columns.Add("*规范埋深", "*规范埋深");
			}
			if (!this.dataGridView1.Columns.Contains("*起点埋深"))
			{
				this.dataGridView1.Columns.Add("*起点埋深", "*起点埋深");
			}
			if (!this.dataGridView1.Columns.Contains("*终点埋深"))
			{
				this.dataGridView1.Columns.Add("*终点埋深", "*终点埋深");
			}
			if (!this.dataGridView1.Columns.Contains("*管线类别"))
			{
				this.dataGridView1.Columns.Add("*管线类别", "*管线类别");
			}
			string text = _context.PipeConfig.GetLineTableFieldName("管线性质");
			if (text == "")
			{
				text = "管线性质";
			}
			if (!this.dataGridView1.Columns.Contains(text))
			{
				this.dataGridView1.Columns.Add(text, text);
			}
			string text2 = _context.PipeConfig.GetLineTableFieldName("埋设方式");
			if (text2 == "")
			{
				text2 = "埋设方式";
			}
			if (!this.dataGridView1.Columns.Contains(text2))
			{
				this.dataGridView1.Columns.Add(text2, text2);
			}
			Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$");
			for (int i = 0; i < feature.Fields.FieldCount; i++)
			{
				string name = feature.Fields.get_Field(i).Name;
				if (!this.dataGridView1.Columns.Contains(name) && regex.IsMatch(name))
				{
					this.dataGridView1.Columns.Add(name, name);
				}
			}
			int index = this.dataGridView1.Rows.Add();
			this.dataGridView1.Rows[index].Cells["*规范埋深"].Value = string.Format("{0:f2}", num);
			this.dataGridView1.Rows[index].Cells["*起点埋深"].Value = string.Format("{0:f2}", num2);
			this.dataGridView1.Rows[index].Cells["*终点埋深"].Value = string.Format("{0:f2}", num3);
			this.dataGridView1.Rows[index].Cells["*管线类别"].Value = value;
			this.dataGridView1.Rows[index].Tag = feature;
			if (num2 < num)
			{
				this.dataGridView1.Rows[index].Cells["*起点埋深"].Style.BackColor = Color.Red;
			}
			if (num3 < num)
			{
				this.dataGridView1.Rows[index].Cells["*终点埋深"].Style.BackColor = Color.Red;
			}
			for (int j = 0; j < feature.Fields.FieldCount; j++)
			{
				string name2 = feature.Fields.get_Field(j).Name;
				if (regex.IsMatch(name2))
				{
					this.dataGridView1.Rows[index].Cells[name2].Value = feature.get_Value(j).ToString();
				}
			}
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
			if (this.saveFileDialog_0.ShowDialog() == DialogResult.OK)
			{
				string fileName = this.saveFileDialog_0.FileName;
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
				OdbcCommand odbcCommand = new OdbcCommand();
				OdbcConnection odbcConnection = new OdbcConnection();
				string connectionString = "DRIVER=MICROSOFT EXCEL DRIVER (*.xls);FIRSTROWHASNAMES=1;READONLY=FALSE;CREATE_DB=\"" + fileName + "\\;DBQ=" + fileName;
				odbcConnection.ConnectionString = connectionString;
				odbcConnection.Open();
				odbcCommand.Connection = odbcConnection;
				odbcCommand.CommandType = CommandType.Text;
				string text = "(";
				for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
				{
					if (i == 0)
					{
						text = text + this.dataGridView1.Columns[i].Name + " varchar(20) ";
					}
					else
					{
						text = text + " , " + this.dataGridView1.Columns[i].Name + " varchar(20) ";
					}
				}
				text += ")";
				text = text.Replace("*", "Custom");
				string commandText = "CREATE TABLE Result " + text;
				odbcCommand.CommandText = commandText;
				odbcCommand.ExecuteNonQuery();
				for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
				{
					OdbcCommand odbcCommand2 = new OdbcCommand();
					odbcCommand2.Connection = odbcConnection;
					odbcCommand2.CommandType = CommandType.Text;
					string text2 = "INSERT INTO Result ";
					string text3 = " VALUES(";
					for (int k = 0; k < this.dataGridView1.ColumnCount; k++)
					{
						if (k != this.dataGridView1.ColumnCount - 1)
						{
							text3 = text3 + "'" + this.dataGridView1[k, j].Value.ToString() + "', ";
						}
						else
						{
							text3 = text3 + "'" + this.dataGridView1[k, j].Value.ToString() + "')";
						}
					}
					text2 += text3;
					odbcCommand2.CommandText = text2;
					odbcCommand2.ExecuteNonQuery();
				}
				odbcConnection.Close();
			}
		}

		private void dataGridView1_CellClick(object obj, DataGridViewCellEventArgs dataGridViewCellEventArgs)
		{
			if (dataGridViewCellEventArgs.RowIndex >= 0)
			{
				IFeature feature = (IFeature)this.dataGridView1.Rows[dataGridViewCellEventArgs.RowIndex].Tag;
				if (feature != null)
				{
					IEnvelope extent = _context.ActiveView.Extent;
					IEnvelope arg_B5_0 = extent;
					IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
					pointClass.X=((feature.Shape.Envelope.XMin + feature.Shape.Envelope.XMax) / 2.0);
					pointClass.Y=((feature.Shape.Envelope.YMin + feature.Shape.Envelope.YMax) / 2.0);
					arg_B5_0.CenterAt(pointClass);
					_context.MapControl.Extent=(extent);
					this.ifeature_0 = feature;
					this.timer_0.Start();
					this.timer_0.Interval = 100;
					_context.ActiveView.Refresh();
					this.m_nTimerCount = 0;
				}
			}
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
			CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, this.ifeature_0.Shape);
			this.m_nTimerCount++;
		}
	}
}
