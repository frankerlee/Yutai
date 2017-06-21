using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public class SearchAffixAnalyDlg : Form
	{
		public IGeometry m_ipGeo;

		public IAppContext m_iApp;

		public IMapControl3 MapControl;

		public IPipeConfig pPipeCfg;

		private IFeatureLayer ifeatureLayer_0;

		private IFields ifields_0;

		private string string_0;

		private IField ifield_0;

		public object mainform;

		public int m_nTimerCounter;

		private IGeometryCollection igeometryCollection_0 = new Multipoint() as IGeometryCollection;

		private List<string> list_0 = new List<string>();

		private IContainer icontainer_0 = null;

		private Button RevBut;

		private Button NoneBut;

		private GroupBox groupBox1;

		private ComboBox LayerBox;

		private Label lable;

		private Button AllBut;

		private Button CloseBut;

		private GroupBox groupBox2;

		private CheckedListBox ValueBox;

		private TextBox txBoxY;

		private Label label2;

		private TextBox txBoxX;

		private Label label1;

		private Label label3;

		private TextBox txBoxRadius;

		private Label label4;

		private Button btnAnalyse;

		private CheckBox chkBoxSet;

		private CheckBox checkReView;

		private CheckBox checAnaPipeline;

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
					if (this.m_iApp.PipeConfig.IsPipePoint(featureLayer.FeatureClass.AliasName))
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
			this.method_0();
			if (!this.method_1())
			{
				MessageBox.Show("配置文件有误，请检查！");
			}
			else
			{
				this.method_2();
			}
		}

		private void method_0()
		{
			this.LayerBox.Items.Clear();
			CommonUtils.ThrougAllLayer(this.m_iApp.FocusMap, new CommonUtils.DealLayer(this.AddName));
			if (this.LayerBox.Items.Count > 0)
			{
				this.LayerBox.SelectedIndex = 0;
			}
		}

		private bool method_1()
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
						this.ifields_0 = this.ifeatureLayer_0.FeatureClass.Fields;
						this.string_0 = this.pPipeCfg.GetPointTableFieldName("节点性质");
						this.string_0 = "节点性质";
						if (this.ifields_0.FindField(this.string_0) < 0)
						{
							this.btnAnalyse.Enabled = false;
							result = false;
						}
						else
						{
							this.btnAnalyse.Enabled = true;
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

		private void method_2()
		{
			this.string_0 = "节点性质";
			if (this.ifields_0 != null)
			{
				int num = this.ifields_0.FindField(this.string_0);
				if (num >= 0)
				{
					this.ifield_0 = this.ifields_0.get_Field(num);
					IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
					IQueryFilter queryFilter = new QueryFilter();
					IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
					IFeature feature = featureCursor.NextFeature();
					this.ValueBox.Items.Clear();
					while (feature != null)
					{
						object obj = feature.get_Value(num);
						if (obj is DBNull)
						{
							feature = featureCursor.NextFeature();
						}
						else
						{
							string text = obj.ToString();
							if (text.Length == 0)
							{
								feature = featureCursor.NextFeature();
							}
							else
							{
								if (!this.ValueBox.Items.Contains(text))
								{
									this.ValueBox.Items.Add(text);
								}
								feature = featureCursor.NextFeature();
							}
						}
					}
				}
			}
		}

		private void LayerBox_SelectedIndexChanged(object obj, EventArgs eventArgs)
		{
			this.list_0.Clear();
			if (!this.method_1())
			{
				MessageBox.Show("配置文件有误，点性字段不匹配,请检查！");
			}
			else
			{
				this.method_2();
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
			BaseFun baseFun = new BaseFun();
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.RevBut = new Button();
			this.NoneBut = new Button();
			this.groupBox1 = new GroupBox();
			this.chkBoxSet = new CheckBox();
			this.txBoxY = new TextBox();
			this.label4 = new Label();
			this.label2 = new Label();
			this.txBoxX = new TextBox();
			this.label1 = new Label();
			this.LayerBox = new ComboBox();
			this.lable = new Label();
			this.AllBut = new Button();
			this.CloseBut = new Button();
			this.groupBox2 = new GroupBox();
			this.checAnaPipeline = new CheckBox();
			this.ValueBox = new CheckedListBox();
			this.label3 = new Label();
			this.txBoxRadius = new TextBox();
			this.btnAnalyse = new Button();
			this.checkReView = new CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.RevBut.Location = new System.Drawing.Point(200, 163);
			this.RevBut.Name = "RevBut";
			this.RevBut.Size = new Size(76, 23);
			this.RevBut.TabIndex = 3;
			this.RevBut.Text = "反选(&I)";
			this.RevBut.UseVisualStyleBackColor = true;
			this.RevBut.Click += new EventHandler(this.RevBut_Click);
			this.NoneBut.Location = new System.Drawing.Point(200, 118);
			this.NoneBut.Name = "NoneBut";
			this.NoneBut.Size = new Size(76, 23);
			this.NoneBut.TabIndex = 2;
			this.NoneBut.Text = "全不选(&N)";
			this.NoneBut.UseVisualStyleBackColor = true;
			this.NoneBut.Click += new EventHandler(this.NoneBut_Click);
			this.groupBox1.Controls.Add(this.chkBoxSet);
			this.groupBox1.Controls.Add(this.txBoxY);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txBoxX);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 233);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(298, 51);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "查找设置";
			this.chkBoxSet.Appearance = Appearance.Button;
			this.chkBoxSet.AutoSize = true;
			this.chkBoxSet.Location = new System.Drawing.Point(237, 16);
			this.chkBoxSet.Name = "chkBoxSet";
			this.chkBoxSet.Size = new Size(39, 22);
			this.chkBoxSet.TabIndex = 4;
			this.chkBoxSet.Text = "设置";
			this.chkBoxSet.UseVisualStyleBackColor = true;
			this.txBoxY.Location = new System.Drawing.Point(129, 16);
			this.txBoxY.Name = "txBoxY";
			this.txBoxY.Size = new Size(72, 21);
			this.txBoxY.TabIndex = 3;
			this.txBoxY.TextChanged += new EventHandler(this.txBoxY_TextChanged);
			this.txBoxY.KeyPress += new KeyPressEventHandler(this.txBoxY_KeyPress);
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(128, 60);
			this.label4.Name = "label4";
			this.label4.Size = new Size(17, 12);
			this.label4.TabIndex = 20;
			this.label4.Text = "米";
			this.label4.Click += new EventHandler(this.label4_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(112, 20);
			this.label2.Name = "label2";
			this.label2.Size = new Size(17, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y:";
			this.txBoxX.Location = new System.Drawing.Point(32, 16);
			this.txBoxX.Name = "txBoxX";
			this.txBoxX.Size = new Size(74, 21);
			this.txBoxX.TabIndex = 1;
			this.txBoxX.KeyPress += new KeyPressEventHandler(this.txBoxX_KeyPress);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 20);
			this.label1.Name = "label1";
			this.label1.Size = new Size(17, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "X:";
			this.LayerBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.LayerBox.FormattingEnabled = true;
			this.LayerBox.Location = new System.Drawing.Point(56, 18);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new Size(148, 20);
			this.LayerBox.TabIndex = 17;
			this.LayerBox.SelectedIndexChanged += new EventHandler(this.LayerBox_SelectedIndexChanged);
			this.lable.AutoSize = true;
			this.lable.Location = new System.Drawing.Point(6, 22);
			this.lable.Name = "lable";
			this.lable.Size = new Size(41, 12);
			this.lable.TabIndex = 16;
			this.lable.Text = "管点层";
			this.AllBut.Location = new System.Drawing.Point(200, 73);
			this.AllBut.Name = "AllBut";
			this.AllBut.Size = new Size(76, 23);
			this.AllBut.TabIndex = 1;
			this.AllBut.Text = "全选(&A)";
			this.AllBut.UseVisualStyleBackColor = true;
			this.AllBut.Click += new EventHandler(this.AllBut_Click);
			this.CloseBut.DialogResult = DialogResult.Cancel;
			this.CloseBut.Location = new System.Drawing.Point(224, 292);
			this.CloseBut.Name = "CloseBut";
			this.CloseBut.Size = new Size(60, 23);
			this.CloseBut.TabIndex = 15;
			this.CloseBut.Text = "关闭(&C)";
			this.CloseBut.UseVisualStyleBackColor = true;
			this.CloseBut.Click += new EventHandler(this.CloseBut_Click);
			this.groupBox2.Controls.Add(this.checAnaPipeline);
			this.groupBox2.Controls.Add(this.RevBut);
			this.groupBox2.Controls.Add(this.NoneBut);
			this.groupBox2.Controls.Add(this.AllBut);
			this.groupBox2.Controls.Add(this.ValueBox);
			this.groupBox2.Controls.Add(this.LayerBox);
			this.groupBox2.Controls.Add(this.lable);
			this.groupBox2.Location = new System.Drawing.Point(8, 1);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(298, 226);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择查找对象";
			this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
			this.checAnaPipeline.AutoSize = true;
			this.checAnaPipeline.Checked = true;
			this.checAnaPipeline.CheckState = CheckState.Checked;
			this.checAnaPipeline.Location = new System.Drawing.Point(216, 17);
			this.checAnaPipeline.Name = "checAnaPipeline";
			this.checAnaPipeline.Size = new Size(72, 16);
			this.checAnaPipeline.TabIndex = 18;
			this.checAnaPipeline.Text = "分析管线";
			this.checAnaPipeline.UseVisualStyleBackColor = true;
			this.ValueBox.CheckOnClick = true;
			this.ValueBox.FormattingEnabled = true;
			this.ValueBox.Items.AddRange(new object[]
			{
				"sdfsfsfs",
				"sdsdf",
				"sfdsdf"
			});
			this.ValueBox.Location = new System.Drawing.Point(13, 53);
			this.ValueBox.Name = "ValueBox";
			this.ValueBox.Size = new Size(166, 164);
			this.ValueBox.Sorted = true;
			this.ValueBox.TabIndex = 0;
			this.ValueBox.SelectedIndexChanged += new EventHandler(this.ValueBox_SelectedIndexChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 296);
			this.label3.Name = "label3";
			this.label3.Size = new Size(65, 12);
			this.label3.TabIndex = 18;
			this.label3.Text = "查找半径：";
			this.txBoxRadius.Location = new System.Drawing.Point(86, 293);
			this.txBoxRadius.Name = "txBoxRadius";
			this.txBoxRadius.Size = new Size(123, 21);
			this.txBoxRadius.TabIndex = 19;
			this.txBoxRadius.Text = "50";
			this.txBoxRadius.KeyPress += new KeyPressEventHandler(this.txBoxRadius_KeyPress);
			this.btnAnalyse.Location = new System.Drawing.Point(224, 320);
			this.btnAnalyse.Name = "btnAnalyse";
			this.btnAnalyse.Size = new Size(60, 23);
			this.btnAnalyse.TabIndex = 22;
			this.btnAnalyse.Text = "分析(&A)";
			this.btnAnalyse.UseVisualStyleBackColor = true;
			this.btnAnalyse.Click += new EventHandler(this.btnAnalyse_Click);
			this.checkReView.AutoSize = true;
			this.checkReView.Checked = true;
			this.checkReView.CheckState = CheckState.Checked;
			this.checkReView.Location = new System.Drawing.Point(21, 323);
			this.checkReView.Name = "checkReView";
			this.checkReView.Size = new Size(120, 16);
			this.checkReView.TabIndex = 24;
			this.checkReView.Text = "关闭清除分析结果";
			this.checkReView.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(309, 347);
			base.Controls.Add(this.checkReView);
			base.Controls.Add(this.btnAnalyse);
			base.Controls.Add(this.txBoxRadius);
			base.Controls.Add(this.CloseBut);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SearchAffixAnalyDlg";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "设施搜索";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.SearchAffixAnalyDlg_FormClosing);
			base.Load += new EventHandler(this.SearchAffixAnalyDlg_Load);
			base.HelpRequested += new HelpEventHandler(this.SearchAffixAnalyDlg_HelpRequested);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
