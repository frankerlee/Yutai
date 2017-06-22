using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;


namespace Yutai.Pipeline.Analysis.Forms
{

	public partial class BufferAnalyseDlg : XtraForm
	{

		public IAppContext m_app;

		public ArrayList m_alCheckedLayerNames;

		public ArrayList m_alCheckedLayer;

		public IGeometry m_pDrawGeo;

		public IGeometry m_pBufferGeo;


		public bool bDrawRed;

		private IContainer icontainer_0 = null;















		public double Radius
		{
			get
			{
				return this.double_0;
			}
			set
			{
				this.double_0 = value;
			}
		}

		public bool SelectGeometry
		{
			get
			{
				return this.bGeo.Checked;
			}
		}

		public BufferAnalyseDlg()
		{
			this.InitializeComponent();
		}

		public void AddName(ILayer pLayer)
		{
			try
			{
				if (pLayer != null)
				{
					IFeatureLayer featureLayer = pLayer as IFeatureLayer;
					CheckListFeatureLayerItem checkListFeatureLayerItem = new CheckListFeatureLayerItem()
					{
						m_pFeatureLayer = featureLayer
					};
					IFeatureClass featureClass = featureLayer.FeatureClass;
					if (featureLayer.FeatureClass.FeatureType != (esriFeatureType) 11)
					{
						if ((!this.radBtnPt.Checked ? false : this.m_app.PipeConfig.IsPipePoint(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
						if ((!this.radBtnLn.Checked ? false : this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
						if ((!this.radBtnOther.Checked || this.m_app.PipeConfig.IsPipeLine(featureClass.AliasName) ? false : !this.m_app.PipeConfig.IsPipePoint(featureClass.AliasName)))
						{
							this.chkLstLayers.Items.Add(checkListFeatureLayerItem);
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void bGeo_CheckedChanged(object obj, EventArgs eventArg)
		{
			if (!this.bGeo.Checked)
			{
				this.m_pDrawGeo = null;
				this.m_app.ActiveView.PartialRefresh((esriViewDrawPhase)32, null, null);
			}
		}

		private void bGeo_Click(object obj, EventArgs eventArg)
		{
		}

		private void btnAnalyse_Click(object obj, EventArgs eventArg)
		{
			double num = Convert.ToDouble(this.txBoxRadius.Text.Trim());
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			this.method_0(num);
			this.bDrawRed = false;
		}

		private void btnClose_Click(object obj, EventArgs eventArg)
		{
			base.Visible = false;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			base.Close();
		}

		private void btnConvertSelect_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				bool itemChecked = this.chkLstLayers.GetItemChecked(i);
				this.chkLstLayers.SetItemChecked(i, !itemChecked);
			}
		}

		private void btnSelectAll_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.chkLstLayers.SetItemChecked(i, true);
			}
		}

		private void btnSelectNone_Click(object obj, EventArgs eventArg)
		{
			int count = this.chkLstLayers.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.chkLstLayers.SetItemChecked(i, false);
			}
		}

		private void BufferAnalyseDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArg)
		{
			base.Visible = false;
			formClosingEventArg.Cancel = true;
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
		}

		private void BufferAnalyseDlg_Load(object obj, EventArgs eventArg)
		{
			this.method_1();
		}

		private void BufferAnalyseDlg_VisibleChanged(object obj, EventArgs eventArg)
		{
			if (!base.Visible)
			{
				IMapControlEvents2_Event _axMapControl = this.m_app.MapControl as IMapControlEvents2_Event;
				_axMapControl.OnAfterDraw-= AxMapControlOnOnAfterDraw;
            }
			else
			{
                IMapControlEvents2_Event axMapControl = this.m_app.MapControl as IMapControlEvents2_Event;
				axMapControl.OnAfterDraw+= AxMapControlOnOnAfterDraw;
			}
		}

	    private void AxMapControlOnOnAfterDraw(object display, int viewDrawPhase)
	    {
            if ((viewDrawPhase != 32 ? false : this.bDrawRed))
            {
                this.DrawSelGeometry();
            }
        }

	    private void button1_Click(object obj, EventArgs eventArg)
		{
			this.method_3();
			double num = Convert.ToDouble(this.txBoxRadius.Text.Trim());
			CommonUtils.DeleteAllElements(this.m_app.FocusMap);
			this.method_0(num);
			this.XbwqoohXht();
		}

		private void chkLstLayers_Click(object obj, EventArgs eventArg)
		{
		}

		private void chkLstLayers_ItemCheck(object obj, ItemCheckEventArgs itemCheckEventArg)
		{
			if (!this.radBtnPt.Checked && !this.radBtnLn.Checked && this.radBtnOther.Checked && itemCheckEventArg.CurrentValue != CheckState.Checked)
			{
				for (int i = 0; i < ((CheckedListBox)obj).Items.Count; i++)
				{
					((CheckedListBox)obj).SetItemChecked(i, false);
				}
				itemCheckEventArg.NewValue = CheckState.Checked;
			}
		}

	public void DrawSelGeometry()
		{
            if (this.m_pDrawGeo != null)
            {
                IRgbColor rgbColor = new RgbColor();
                IRgbColor selectionCorlor = this.m_app.Config.SelectionEnvironment.DefaultColor as IRgbColor;
                rgbColor.Blue=((int)selectionCorlor.Blue);
                rgbColor.Green=((int)selectionCorlor.Green);
                rgbColor.Red=((int)selectionCorlor.Red);
                rgbColor.Transparency=(selectionCorlor.Transparency);
                object obj = null;
                int selectionBufferInPixels = this.m_app.Config.SelectionEnvironment.SearchTolerance;
                ISymbol symbol = null;
                switch ((int)this.m_pDrawGeo.GeometryType)
                {
                    case 1:
                        {
                            ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
                            symbol = (ISymbol)simpleMarkerSymbol;
                            symbol.ROP2=(esriRasterOpCode)  (10);
                            simpleMarkerSymbol.Color=(rgbColor);
                            simpleMarkerSymbol.Size=((double)(selectionBufferInPixels + selectionBufferInPixels + selectionBufferInPixels));
                            break;
                        }
                    case 3:
                        {
                            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
                            symbol = (ISymbol)simpleLineSymbol;
                            symbol.ROP2=(esriRasterOpCode) (10);
                            simpleLineSymbol.Color=(rgbColor);
                            simpleLineSymbol.Color.Transparency=(1);
                            simpleLineSymbol.Width=((double)selectionBufferInPixels);
                            break;
                        }
                    case 4:
                    case 5:
                        {
                            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
                            symbol = (ISymbol)simpleFillSymbol;
                            symbol.ROP2=(esriRasterOpCode) (10);
                            simpleFillSymbol.Color=(rgbColor);
                            simpleFillSymbol.Color.Transparency=(1);
                            break;
                        }
                }
                obj = symbol;
                this.m_app.MapControl.DrawShape(this.m_pDrawGeo, ref obj);
            }
        }

		private void EoQqirFnxu(IGeometry geometry, double num)
		{
			this.m_pBufferGeo = ((ITopologicalOperator)geometry).Buffer(num);
			CommonUtils.NewPolygonElementTran(this.m_app.FocusMap, this.m_pBufferGeo as IPolygon, false);
		    IActiveView activeView = this.m_app.ActiveView;
			activeView.PartialRefresh((esriViewDrawPhase)8, null, null);
		}

		private void hbAqYjDqsa(object obj, HelpEventArgs helpEventArg)
		{
			string str = string.Concat(Application.StartupPath, "\\帮助.chm");
			Help.ShowHelp(this, str, HelpNavigator.KeywordIndex, "缓冲分析");
		}

	private void method_0(double num)
		{
			if (!this.bGeo.Checked)
			{
				IMap map = this.m_app.FocusMap;
				IEnumFeature featureSelection = (IEnumFeature)map.FeatureSelection;
				IFeature feature = featureSelection.Next();
				if (feature != null)
				{
					while (feature != null)
					{
						this.EoQqirFnxu(feature.Shape, num);
						feature = featureSelection.Next();
					}
				}
				else
				{
					MessageBox.Show("请确定范围！");
				}
			}
			else if (this.m_pDrawGeo != null)
			{
				this.EoQqirFnxu(this.m_pDrawGeo, num);
			}
			else
			{
				MessageBox.Show("请确定范围！");
			}
		}

		private void method_1()
		{
			this.chkLstLayers.Items.Clear();
			CommonUtils.ThrougAllLayer(this.m_app.FocusMap, new CommonUtils.DealLayer(this.AddName));
		}

		private bool method_2(string str)
		{
			bool flag;
			int count = this.m_alCheckedLayerNames.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					string item = this.m_alCheckedLayerNames[num] as string;
					if (str.Trim().ToUpper() == item.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					else
					{
						num++;
					}
				}
				else
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		private void method_3()
		{
			this.m_alCheckedLayer = new ArrayList();
			int count = this.chkLstLayers.CheckedItems.Count;
			this.m_alCheckedLayer.Clear();
			for (int i = 0; i < count; i++)
			{
				CheckListFeatureLayerItem item = this.chkLstLayers.CheckedItems[i] as CheckListFeatureLayerItem;
				this.m_alCheckedLayer.Add(item.m_pFeatureLayer);
			}
		}

		

		private void radBtnLn_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = true;
			this.btnConvertSelect.Enabled = true;
			this.btnSelectNone.Enabled = true;
			this.method_1();
		}

		private void radBtnOther_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = false;
			this.btnConvertSelect.Enabled = false;
			this.btnSelectNone.Enabled = false;
			this.method_1();
		}

		private void radBtnPt_CheckedChanged(object obj, EventArgs eventArg)
		{
			this.btnSelectAll.Enabled = true;
			this.btnConvertSelect.Enabled = true;
			this.btnSelectNone.Enabled = true;
			this.method_1();
		}

		private void txBoxRadius_KeyPress(object obj, KeyPressEventArgs keyPressEventArg)
		{
			//ApplicationData.CommonUtils.NumberText_KeyPress(obj, keyPressEventArg);
		}

		private void XbwqoohXht()
		{
			if (this.resultDialog_0 == null)
			{
				this.resultDialog_0 = new ResultDialog()
				{
					App = this.m_app,
					m_pBufferGeo = this.m_pBufferGeo,
					m_alLayers = this.m_alCheckedLayer
				};
				this.resultDialog_0.Show();
			}
			else if (!this.resultDialog_0.Visible)
			{
				this.resultDialog_0.m_pBufferGeo = this.m_pBufferGeo;
				this.resultDialog_0.m_alLayers = this.m_alCheckedLayer;
				this.resultDialog_0.ThrougAllLayer();
				this.resultDialog_0.Visible = true;
			}
			else
			{
				this.resultDialog_0.m_pBufferGeo = this.m_pBufferGeo;
				this.resultDialog_0.m_alLayers = this.m_alCheckedLayer;
				this.resultDialog_0.ThrougAllLayer();
			}
		}
	}
}