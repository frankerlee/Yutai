using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class SearchAffixAnalyDlg : Form
	{
		public IGeometry m_ipGeo;

		public IAppContext m_iApp;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;

        

	    private IBasicLayerInfo _basicLayerInfo;


		public object mainform;

		public int m_nTimerCounter;

		private IGeometryCollection igeometryCollection_0 = new Multipoint() as IGeometryCollection;

		private List<string> list_0 = new List<string>();

		private IContainer icontainer_0 = null;
        

		public SearchAffixAnalyDlg()
		{
			this.InitializeComponent();
		}

		private void SearchAffixAnalyDlg_Load(object obj, EventArgs eventArgs)
		{
			this.AutoFlash();
		}

		public void AddName(ILayer pLayer)
		{
			try
			{
				if (pLayer != null)
				{
					string name = pLayer.Name;
					IFeatureLayer featureLayer = pLayer as IFeatureLayer;
					if (this.pPipeCfg.IsPipelineLayer(featureLayer.Name,enumPipelineDataType.Point))
					{
						this.LayerBox.Items.Add(name);
					}
				}
			}
			catch
			{
			}
		}

		public void AutoFlash()
		{
			this.FindLayers();
			if (!this.FindPipePointLayer())
			{
				MessageBox.Show("配置文件有误，请检查！");
			}
			
		}

		private void FindLayers()
		{
			this.LayerBox.Items.Clear();

			CommonUtils.ThrougAllLayer(this.m_iApp.FocusMap, new CommonUtils.DealLayer(this.AddName));
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private bool FindPipePointLayer()
		{
			int selectedIndex = this.LayerBox.SelectedIndex;
			bool result;
			if (selectedIndex < 0)
			{
				result = false;
			}
			else
			{
				this.ifeatureLayer_0 = null;
				if (this.MapControl == null)
				{
					result = false;
				}
				else
				{
					string strDstName = this.LayerBox.SelectedItem.ToString();
					this.ifeatureLayer_0 = (IFeatureLayer)CommonUtils.GetLayerByName(this.m_iApp.FocusMap, strDstName);
					if (this.ifeatureLayer_0 == null)
					{
						result = false;
					}
					else
					{
					    IBasicLayerInfo basicLayerInfo = pPipeCfg.GetBasicLayerInfo(ifeatureLayer_0.FeatureClass);
					   if(basicLayerInfo.DataType!= enumPipelineDataType.Point)
						{
							this.btnAnalyse.Enabled = false;
							result = false;
						}
						else
						{
							this.btnAnalyse.Enabled = true;
						    _basicLayerInfo = basicLayerInfo;
						    //string values=basicLayerInfo.GetField(PipeConfigWordHelper.PointWords.FSW).DomainValues;
                            FillValues();
							result = true;
						}
					}
				}
			}
			return result;
		}

		public void InitAppearance()
		{
			this.txBoxX.Text = "";
			this.txBoxY.Text = "";
			this.txBoxRadius.Text = "50";
			this.LayerBox.Text = "";
			this.chkBoxSet.Checked = false;
			this.list_0.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, false);
			}
		}

		public void OnMouseMove(double x, double y)
		{
			if (this.chkBoxSet.Checked)
			{
				this.txBoxX.Text = x.ToString("f3");
				this.txBoxY.Text = y.ToString("f3");
			}
		}

		public void OnMouseDown(double x, double y)
		{
			if (this.chkBoxSet.Checked)
			{
				this.txBoxX.Text = x.ToString("f3");
				this.txBoxY.Text = y.ToString("f3");
				SearchAffixAnalyDlg.DeleteAllElements(this.m_iApp.ActiveView);
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				point.X=(x);
				point.Y=(y);
				SearchAffixAnalyDlg.NewBasePointElement(this.m_iApp.ActiveView, point);
				this.chkBoxSet.Checked = false;
			}
		}

		private void FillValues()
		{

            List<string> listValues =new List<string>();
                CommonHelper.GetUniqueValues(ifeatureLayer_0, _basicLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.FSW),listValues);
		    this.string_0 = _basicLayerInfo.GetFieldName(PipeConfigWordHelper.PointWords.FSW);

                    this.ValueBox.Items.Clear();
		    foreach (string value in listValues)
		    {
		        ValueBox.Items.Add(value);
		    }
				
			
		}

		private void LayerBox_SelectedIndexChanged(object obj, EventArgs eventArgs)
		{
			this.list_0.Clear();
			if (!this.FindPipePointLayer())
			{
				MessageBox.Show("配置文件有误，点性字段不匹配,请检查！");
			}
		}

		private void AllBut_Click(object obj, EventArgs eventArgs)
		{
			this.list_0.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.list_0.Add(this.ValueBox.Items[i].ToString());
				this.ValueBox.SetItemChecked(i, true);
			}
			string str = "";
			foreach (string current in this.list_0)
			{
				str += current;
				str += " ";
			}
		}

		private void NoneBut_Click(object obj, EventArgs eventArgs)
		{
			this.list_0.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				this.ValueBox.SetItemChecked(i, false);
			}
		}

		private void RevBut_Click(object obj, EventArgs eventArgs)
		{
			this.list_0.Clear();
			for (int i = 0; i < this.ValueBox.Items.Count; i++)
			{
				if (this.ValueBox.GetItemChecked(i))
				{
					this.ValueBox.SetItemChecked(i, false);
				}
				else
				{
					this.list_0.Add(this.ValueBox.Items[i].ToString());
					this.ValueBox.SetItemChecked(i, true);
				}
			}
			string str = "";
			foreach (string current in this.list_0)
			{
				str += current;
				str += " ";
			}
		}

		private void CloseBut_Click(object obj, EventArgs eventArgs)
		{
			base.Visible = false;
			if (this.checkReView.Checked)
			{
				SearchAffixAnalyDlg.DeleteAllElements(this.m_iApp.ActiveView);
			}
		}

		private void ValueBox_SelectedIndexChanged(object obj, EventArgs eventArgs)
		{
			int selectedIndex = this.ValueBox.SelectedIndex;
			string str = "";
			string item = this.ValueBox.SelectedItem.ToString();
			if (this.ValueBox.GetItemChecked(selectedIndex))
			{
				if (!this.list_0.Contains(item))
				{
					this.list_0.Add(item);
				}
			}
			else if (this.list_0.Contains(item))
			{
				this.list_0.Remove(item);
			}
			foreach (string current in this.list_0)
			{
				str += current;
				str += " ";
			}
		}

		private void method_3(object obj, EventArgs eventArgs)
		{
			IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
			ISpatialFilter spatialFilter = new SpatialFilter();
			string text = "";
			int count = this.list_0.Count;
			int num = 1;
			foreach (string current in this.list_0)
			{
				text += this.string_0;
				text += " = \"";
				text += current;
				text += "\"";
				if (num < count)
				{
					text += " OR ";
				}
				num++;
			}
			spatialFilter.WhereClause=(text);
			IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
			for (IFeature feature = featureCursor.NextFeature(); feature != null; feature = featureCursor.NextFeature())
			{
				ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
                ISimpleMarkerSymbol arg_ED_0 = simpleMarkerSymbolClass;
				IRgbColor rgbColorClass = new RgbColor();
				rgbColorClass.Red=(255);
				rgbColorClass.Green=(0);
				rgbColorClass.Blue=(0);
				arg_ED_0.Color=(rgbColorClass);
				object obj2 = simpleMarkerSymbolClass;
				this.MapControl.DrawShape(feature.Shape, ref obj2);
			}
		}

		private void txBoxY_TextChanged(object obj, EventArgs eventArgs)
		{
		}

		private void label4_Click(object obj, EventArgs eventArgs)
		{
		}

		private void method_4(object obj, EventArgs eventArgs)
		{
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			if (this.txBoxX.Text == "")
			{
				point.X=(0.0);
			}
			else
			{
				point.X=(Convert.ToDouble(this.txBoxX.Text.Trim()));
			}
			if (this.txBoxY.Text == "")
			{
				point.Y=(0.0);
			}
			else
			{
				point.Y=(Convert.ToDouble(this.txBoxY.Text.Trim()));
			}
			SearchAffixAnalyDlg.NewBasePointElement(this.m_iApp.ActiveView, point);
		}

		private void txBoxX_KeyPress(object obj, KeyPressEventArgs keyPressEventArgs)
		{
			TextBox textBox = (TextBox)obj;
			char keyChar = keyPressEventArgs.KeyChar;
			char c = keyChar;
			if (c != '\b')
			{
				switch (c)
				{
				case '.':
				{
					int num = textBox.Text.IndexOf('.');
					if (num != -1 || textBox.SelectionStart == 0)
					{
						keyPressEventArgs.KeyChar = '\0';
						return;
					}
					return;
				}
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return;
				}
				keyPressEventArgs.KeyChar = '\0';
			}
		}

		private void txBoxY_KeyPress(object sender, KeyPressEventArgs e)
		{
			CommonUtils.NumberText_KeyPress(sender, e);
		}

		private void txBoxRadius_KeyPress(object sender, KeyPressEventArgs e)
		{
            CommonUtils.NumberText_KeyPress(sender, e);
		}

		private void btnAnalyse_Click(object obj, EventArgs eventArgs)
		{
			if (this.MapControl != null)
			{
				this.igeometryCollection_0.RemoveGeometries(0, this.igeometryCollection_0.GeometryCount);
				SearchAffixAnalyDlg.DeleteAllElements(this.m_iApp.ActiveView);
				((IFeatureSelection)this.ifeatureLayer_0).Clear();
				IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
				ISpatialFilter spatialFilter = new SpatialFilter();
				string text = "";
				int count = this.list_0.Count;
				int num = 1;
				foreach (string current in this.list_0)
				{
					text += this.string_0;
					text += " = '";
					text += current;
					text += "'";
					if (num < count)
					{
						text += " OR ";
					}
					num++;
				}
				if (this.txBoxX.Text == "" || this.txBoxY.Text == "")
				{
					MessageBox.Show("请设置搜索中心点坐标！");
				}
				else if (this.txBoxRadius.Text == "")
				{
					MessageBox.Show("请设置查找半径！");
				}
				else
				{
					if (this.list_0.Count != 0)
					{
					}
					this.m_iApp.ActiveView.Refresh();
					IPoint point = new ESRI.ArcGIS.Geometry.Point();
					double num2 = 0.0;
					if (this.txBoxX.Text == "")
					{
						point.X=(0.0);
					}
					else
					{
						point.X=(Convert.ToDouble(this.txBoxX.Text.Trim()));
					}
					if (this.txBoxY.Text == "")
					{
						point.Y=(0.0);
					}
					else
					{
						point.Y=(Convert.ToDouble(this.txBoxY.Text.Trim()));
					}
					SearchAffixAnalyDlg.NewBasePointElement(this.m_iApp.ActiveView, point);
					if (this.txBoxRadius.Text != "")
					{
						num2 = Convert.ToDouble(this.txBoxRadius.Text.Trim());
					}
					ITopologicalOperator topologicalOperator = (ITopologicalOperator)point;
					IGeometry geometry = topologicalOperator.Buffer(num2);
					spatialFilter.Geometry=(geometry);
					spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
					spatialFilter.WhereClause=(text);
					if (this.list_0.Count > 0)
					{
						IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);
						for (IFeature feature = featureCursor.NextFeature(); feature != null; feature = featureCursor.NextFeature())
						{
							object missing = Type.Missing;
							this.igeometryCollection_0.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
							SearchAffixAnalyDlg.NewPointElement(this.m_iApp.ActiveView, feature.ShapeCopy as IPoint);
							this.method_7(this.m_iApp.ActiveView, point, feature.Shape as IPoint);
						}
						IRgbColor rgbColor = new RgbColor();
						rgbColor.Red=(255);
						rgbColor.Green=(0);
						rgbColor.Blue=(0);
						IRgbColor rgbColor2 = new RgbColor();
						rgbColor2.Red=(0);
						rgbColor2.Green=(255);
						rgbColor2.Blue=(0);
						this.method_9(this.m_iApp.ActiveView, point, num2, false, rgbColor);
						this.method_9(this.m_iApp.ActiveView, point, num2 / 2.0, false, rgbColor2);
						this.FlashDstItem();
						//featureCursor = featureClass.Search(spatialFilter, false);
                        ((IFeatureSelection)this.ifeatureLayer_0).SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew,false);
						//this.m_iApp.SetResult(featureCursor, (IFeatureSelection)this.ifeatureLayer_0);
					}
					if (this.checAnaPipeline.Checked)
					{
						ILayer layer = (IFeatureLayer)CommonUtils.GetLayerByName(this.m_iApp.FocusMap, this.LayerBox.Text.Replace("点", "线"));
						if (layer != null)
						{
							spatialFilter = new SpatialFilter();
							topologicalOperator = (ITopologicalOperator)point;
							geometry = topologicalOperator.Buffer(num2);
							spatialFilter.Geometry=(geometry);
							spatialFilter.SpatialRel=(esriSpatialRelEnum) (1);
							IFeatureCursor featureCursor2 = (layer as IFeatureLayer).FeatureClass.Search(spatialFilter, false);
							for (IFeature feature = featureCursor2.NextFeature(); feature != null; feature = featureCursor2.NextFeature())
							{
								object missing = Type.Missing;
								this.method_5(point, feature);
							}
							featureCursor2 = (layer as IFeatureLayer).FeatureClass.Search(spatialFilter, false);
                            ((IFeatureSelection)layer).SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                           // this.m_iApp.SetResult(featureCursor2, (IFeatureSelection)layer);
						}
					}
				}
			}
		}

		private void method_5(IPoint point, IFeature feature)
		{
			IPoint point2;
			IPoint point3;
			double num;
			this.method_6(point, feature, out point2, out point3, out num);
			IPoint point4 = point2;
			IPoint point5 = point3;
			object missing = Type.Missing;
			IPointCollection pointCollection = new Polyline();
			pointCollection.AddPoint(point4, ref missing, ref missing);
			pointCollection.AddPoint(point5, ref missing, ref missing);
			BaseFun baseFun = new BaseFun(pPipeCfg);
			IElement lineElement = baseFun.GetLineElement((IPolyline)pointCollection);
			((IGraphicsContainer)this.m_iApp.ActiveView).AddElement(lineElement, 0);
			string sNoteText = string.Format("L = {0:f2}m", ((IPolyline)pointCollection).Length);
			baseFun.NoteOrientationCenterText(this.m_iApp.ActiveView, (IPolyline)pointCollection, sNoteText);
		}

		private void method_6(IPoint point, IFeature feature, out IPoint ipoint_1, out IPoint ipoint_2, out double double_0)
		{
			ipoint_1 = null;
			ipoint_2 = null;
			double_0 = 384000000.0;
			try
			{
				object missing = Type.Missing;
				IPointCollection pointCollection = new Polyline();
				pointCollection.AddPoint(point, ref missing, ref missing);
				for (int i = 0; i < pointCollection.PointCount; i++)
				{
					IPoint point2 = pointCollection.get_Point(i);
					IProximityOperator proximityOperator = (IProximityOperator)feature.Shape;
					IPoint point3 = proximityOperator.ReturnNearestPoint(point2, 0);
					double num = Math.Sqrt(Math.Pow(point3.X - point2.X, 2.0) + Math.Pow(point3.Y - point2.Y, 2.0));
					if (double_0 > num)
					{
						double_0 = num;
						ipoint_1 = point2;
						ipoint_2 = point3;
					}
				}
			}
			catch
			{
				MessageBox.Show("所选的实体不能量算");
				double_0 = 384000000.0;
			}
		}

		private void method_7(IActiveView pView, IPoint point, IPoint point2)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			double num = Math.Sqrt(Math.Pow(point2.X - point.X, 2.0) + Math.Pow(point2.Y - point.Y, 2.0));
			num = Math.Round(num, 3);
			IPoint point3 = new ESRI.ArcGIS.Geometry.Point();
			point3.X=(point.X + (point2.X - point.X) / 2.0);
			point3.Y=(point.Y + (point2.Y - point.Y) / 2.0);
			IElement textElement = this.GetTextElement(string.Concat(num), point3);
			graphicsContainer.AddElement(textElement, 0);
			IPolyline polyline = new Polyline() as IPolyline;
			polyline.FromPoint=(point);
			polyline.ToPoint=(point2);
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			ISimpleLineSymbol arg_FF_0 = simpleLineSymbol;
			IRgbColor rgbColorClass = new RgbColor();
			rgbColorClass.Red=(255);
			rgbColorClass.Green=(0);
			rgbColorClass.Blue=(0);
			arg_FF_0.Color=(rgbColorClass);
			simpleLineSymbol.Width=(8.0);
			simpleLineSymbol.Style=(esriSimpleLineStyle) (2);
			IGraphicsContainer arg_130_0 = graphicsContainer;
			ILineElement lineElementClass = new LineElement() as ILineElement;
			((IElement)lineElementClass).Geometry=(polyline);
			arg_130_0.AddElement(lineElementClass as IElement, 0);
		}

		public IElement GetTextElement(string sNoteText, IPoint PosPt)
		{
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=(255);
			rgbColor.Green=(0);
			rgbColor.Blue=(0);
			IFont font = new SystemFont() as IFont;
			font.Name = "宋体";
			font.Size = 8m;
			ITextSymbol textSymbol = new TextSymbol();
			textSymbol.Color=(rgbColor);
			textSymbol.Font=((IFontDisp)font);
			textSymbol.Angle=(0.0);
			textSymbol.RightToLeft=(false);
			textSymbol.VerticalAlignment=(0);
			textSymbol.HorizontalAlignment=(0);
			ITextElement textElement = new TextElement() as ITextElement;
			textElement.Symbol=(textSymbol);
			textElement.Text=(sNoteText);
			textElement.ScaleText=(true);
			((IElement)textElement).Geometry=(PosPt);
			((IElementProperties)textElement).Name=("AffixAnalyDistance");
			return (IElement)textElement;
		}

		public static void NewPointElement(IActiveView pView, IPoint pPoint)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			new RubberPoint();
			ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=(255);
			rgbColor.Green=(0);
			rgbColor.Blue=(0);
			IRgbColor rgbColor2 = new RgbColor();
			rgbColor2.Red=(0);
			rgbColor2.Green=(0);
			rgbColor2.Blue=(0);
			simpleMarkerSymbol.Style=(0);
			simpleMarkerSymbol.Color=(rgbColor);
			simpleMarkerSymbol.Outline=(true);
			simpleMarkerSymbol.OutlineSize=(2.0);
			simpleMarkerSymbol.OutlineColor=(rgbColor2);
			simpleMarkerSymbol.Size=(9.0);
			IElement element = new MarkerElement();
			IMarkerElement markerElement = element as IMarkerElement;
			markerElement.Symbol=(simpleMarkerSymbol);
			element.Geometry=(pPoint);
			graphicsContainer.AddElement(element, 0);
		}

		public static void NewBasePointElement(IActiveView pView, IPoint pPoint)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			new RubberPoint();
			ISimpleMarkerSymbol simpleMarkerSymbol = new SimpleMarkerSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=(255);
			rgbColor.Green=(0);
			rgbColor.Blue=(0);
			IRgbColor rgbColor2 = new RgbColor();
			rgbColor2.Red=(0);
			rgbColor2.Green=(0);
			rgbColor2.Blue=(0);
			simpleMarkerSymbol.Style=(esriSimpleMarkerStyle) (3);
			simpleMarkerSymbol.Color=(rgbColor);
			simpleMarkerSymbol.Outline=(true);
			simpleMarkerSymbol.OutlineSize=(1.0);
			simpleMarkerSymbol.OutlineColor=(rgbColor2);
			simpleMarkerSymbol.Size=(12.0);
			IElement element = new MarkerElement();
			IMarkerElement markerElement = element as IMarkerElement;
			markerElement.Symbol=(simpleMarkerSymbol);
			element.Geometry=(pPoint);
			graphicsContainer.AddElement(element, 0);
		}

		private ICircularArc method_8(IPoint point, double num, bool flag)
		{
			IConstructCircularArc constructCircularArc = new CircularArc() as IConstructCircularArc;
			constructCircularArc.ConstructCircle(point, num, flag);
			return (ICircularArc)constructCircularArc;
		}

		private void method_9(IActiveView pView, IPoint point, double num, bool flag, IRgbColor color)
		{
			this.method_8(point, num, flag);
			ISegmentCollection segmentCollection = new Polygon() as ISegmentCollection;
			ICircularArc circularArc = this.method_8(point, num, flag);
			object missing = Type.Missing;
			segmentCollection.AddSegment(circularArc as ISegment, ref missing, ref missing);
			ICircleElement circleElement = new CircleElement() as ICircleElement;
			IElement element = (IElement)circleElement;
			element.Geometry=(segmentCollection as IGeometry);
			IFillShapeElement fillShapeElement = (IFillShapeElement)circleElement;
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
			simpleLineSymbol.Color=(color);
			simpleLineSymbol.Width=(2.0);
			simpleLineSymbol.Style=(0);
			simpleFillSymbol.Color=(color);
			simpleFillSymbol.Style=(esriSimpleFillStyle) (1);
			simpleFillSymbol.Outline=(simpleLineSymbol);
			fillShapeElement.Symbol=(simpleFillSymbol);
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			graphicsContainer.AddElement((IElement)circleElement, 0);
		}

		public static void DeleteAllElements(IActiveView pView)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) pView;
			graphicsContainer.DeleteAllElements();
		    IActiveView activeView = pView;
			activeView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			activeView.Refresh();
			//ICommand command = new ControlsClearSelectionCommand();
			//command.OnCreate(Se.m_iApp.MapControl);
			//command.OnClick();
		}

		private void SearchAffixAnalyDlg_FormClosing(object obj, FormClosingEventArgs formClosingEventArgs)
		{
			formClosingEventArgs.Cancel = true;
			base.Visible = false;
			if (this.checkReView.Checked)
			{
				SearchAffixAnalyDlg.DeleteAllElements(this.m_iApp.ActiveView);
			}
		}

		private void groupBox2_Enter(object obj, EventArgs eventArgs)
		{
		}

		public void FlashDstItem()
		{
			IMapControl3 mapControl = this.m_iApp.MapControl as IMapControl3;
			CRandomColor cRandomColor = new CRandomColor();
			Color randColor = cRandomColor.GetRandColor();
			ISymbol symbol = null;
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=((int)randColor.R);
			rgbColor.Green=((int)randColor.G);
			rgbColor.Blue=((int)randColor.B);
			try
			{
				esriGeometryType geometryType = this.igeometryCollection_0.get_Geometry(0).GeometryType;
				if (geometryType == (esriGeometryType) 1)
				{
					ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
					simpleMarkerSymbolClass.Color=(rgbColor);
					symbol = simpleMarkerSymbolClass as ISymbol;
				}
				if (geometryType == (esriGeometryType) 3)
				{
					ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
					simpleLineSymbolClass.Color=(rgbColor);
					simpleLineSymbolClass.Width=(6.0);
					symbol = simpleLineSymbolClass as ISymbol;
				}
				symbol.ROP2=(esriRasterOpCode) (10);
				mapControl.FlashShape((IGeometry)this.igeometryCollection_0, 6, 150, symbol);
			}
			catch
			{
			}
		}

		private void SearchAffixAnalyDlg_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "设施搜索";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}


    }
}