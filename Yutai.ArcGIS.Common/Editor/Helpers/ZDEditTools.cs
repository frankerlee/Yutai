using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;
using WorkspaceOperator = Yutai.Shared.WorkspaceOperator;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class ZDEditTools
	{
		private static HitType m_HitType;

		internal static IPoint m_CurrentPoint;

		private System.Windows.Forms.Cursor cursor_0 = null;

		internal static int m_PartIndex;

		internal static int m_PointIndex;

		internal static IFeature m_pEditFeature;

		private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;

		private bool bool_0 = true;

		private IList ilist_0 = new ArrayList();

		private IGeometry igeometry_0 = null;

		private string string_0 = "";

		private bool bool_1 = true;

		private IPoint ipoint_0 = null;

		private ISet iset_0;

	    private IAppContext ikhookHelper_0;

		private IPoint ipoint_1;

		private IDisplayFeedback idisplayFeedback_0;

		private bool bool_2 = false;

		private bool bool_3 = false;

		private IPoint ipoint_2;

		private IPoint ipoint_3;

		private ISymbol isymbol_0;

		private ISymbol isymbol_1 = null;

		private bool bool_4;

		private IAnchorPoint ianchorPoint_0;

		private ISimpleMarkerSymbol isimpleMarkerSymbol_0 = new SimpleMarkerSymbol();

		private INewEnvelopeFeedback inewEnvelopeFeedback_0;

		private IEnvelope ienvelope_0;

		private bool bool_5;

		private bool bool_6 = false;

		private bool bool_7 = false;

		private List<ZD.HitFeatureInfo> list_0 = null;

		private static bool _DrawNode;

		private bool bool_8 = false;

		private bool bool_9 = false;

		private int int_0 = -1;

		private IEnumFeature ienumFeature_0 = null;

		private int int_1 = 0;

		private bool bool_10 = false;

		private bool bool_11 = false;

		private IPoint ipoint_4;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		private HitType hitType_0 = HitType.HitNode;

		private bool bool_12 = false;

		private static IPoint m_SelectionSetAnchorPoint;

		public static IPoint CurrentPosition
		{
			get
			{
				return ZDEditTools.m_CurrentPoint;
			}
		}

		public System.Windows.Forms.Cursor Cursor
		{
			get
			{
				return this.cursor_0;
			}
		}

		public static bool DrawNode
		{
			get
			{
				return ZDEditTools._DrawNode;
			}
			set
			{
				ZDEditTools._DrawNode = value;
			}
		}

		public static IFeature EditFeature
		{
			get
			{
				return ZDEditTools.m_pEditFeature;
			}
			set
			{
				ZDEditTools.m_pEditFeature = value;
			}
		}

		public bool Enabled
		{
			get
			{
				bool flag;
				if (this.ikhookHelper_0.MapControl.Map == null)
				{
					flag = false;
				}
				else if (!(this.ikhookHelper_0.MapControl.Map.LayerCount == 0 || Editor.EditWorkspace == null ? false : Editor.EditMap != null))
				{
					ZDEditTools.DrawNode = false;
					flag = false;
				}
				else if (Editor.EditMap != this.ikhookHelper_0.MapControl.Map)
				{
					ZDEditTools.DrawNode = false;
					flag = false;
				}
				else if (ZD.ZDEditTools.ZDFeatureLayer != null)
				{
					flag = true;
				}
				else
				{
					ZDEditTools.DrawNode = false;
					flag = false;
				}
				return flag;
			}
		}

		public static HitType HitType
		{
			get
			{
				return ZDEditTools.m_HitType;
			}
			set
			{
				ZDEditTools.m_HitType = value;
			}
		}

		public static bool IsAutoShowAttribute
		{
			get;
			set;
		}

		public static int PartIndex
		{
			get
			{
				return ZDEditTools.m_PartIndex;
			}
			set
			{
				ZDEditTools.m_PartIndex = value;
			}
		}

		public static int PointIndex
		{
			get
			{
				return ZDEditTools.m_PointIndex;
			}
			set
			{
				ZDEditTools.m_PointIndex = value;
			}
		}

		public IPopuMenuWrap PopuMenu
		{
			set
			{
				this.ipopuMenuWrap_0 = value;
			}
		}

		public static IPoint SelectionSetAnchorPoint
		{
			get
			{
				return ZDEditTools.m_SelectionSetAnchorPoint;
			}
			set
			{
				ZDEditTools.m_SelectionSetAnchorPoint = value;
			}
		}

		static ZDEditTools()
		{
			ZDEditTools.old_acctor_mc();
		}

		public ZDEditTools()
		{
			this.iset_0 = new Set();
			this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Edit.cur"));
			this.isymbol_1 = new SimpleMarkerSymbol() as ISymbol;
			(this.isymbol_1 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSX;
			(this.isymbol_1 as ISimpleMarkerSymbol).Size = 4;
			this.isymbol_0 = new SimpleMarkerSymbol() as ISymbol;
			(this.isymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
			(this.isymbol_0 as ISimpleMarkerSymbol).Size = 4;
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = 183,
				Green = 120,
				Blue = 245
			};
			(this.isymbol_0 as ISimpleMarkerSymbol).Color = rgbColorClass;
			this.isimpleMarkerSymbol_0.Style = esriSimpleMarkerStyle.esriSMSCircle;
			this.isimpleMarkerSymbol_0.Size = 8;
			this.isimpleMarkerSymbol_0.Outline = true;
			this.isimpleMarkerSymbol_0.Color = ColorManage.GetRGBColor(0, 255, 255);
		}

		public void Init()
		{
			string[] strArrays;
			string[] strArrays1;
			if (!this.ipopuMenuWrap_0.Visible)
			{
				if ((!this.bool_12 ? true : this.hitType_0 != ZDEditTools.m_HitType))
				{
					this.bool_12 = true;
					this.hitType_0 = ZDEditTools.m_HitType;
					this.ipopuMenuWrap_0.Clear();
					if (ZDEditTools.m_HitType != HitType.None)
					{
						strArrays = new string[] { "ViewGeometryInfo" };
						strArrays1 = strArrays;
					}
					else
					{
						strArrays = new string[] { "Zoom2SelectedFeature", "ClearSelection", "-", "AttributeEdit" };
						strArrays1 = strArrays;
					}
					bool flag = false;
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						flag = (i + 1 == (int)strArrays1.Length ? false : string.Compare(strArrays1[i + 1], "-") == 0);
						this.ipopuMenuWrap_0.AddItem(strArrays1[i], flag);
					}
				}
				else
				{
					this.ipopuMenuWrap_0.UpdateUI();
				}
			}
		}

		private bool method_0(IPoint ipoint_5, out bool bool_13)
		{
			bool flag;
			IFeature feature;
			IPoint point;
			string unit;
			IPath geometry;
			object value;
			object obj;
			bool flag1;
			int pointCount;
			int i;
			bool_13 = false;
			this.igeometry_0 = null;
			ZDEditTools.m_PartIndex = -1;
			ZDEditTools.m_PointIndex = -1;
			this.string_0 = "";
			int num = -1;
			double num1 = 0;
			int num2 = -1;
			bool flag2 = false;
			if (this.ikhookHelper_0.MapControl.Map.SelectionCount >= 1)
			{
				double searchTolerance = 4;
				if (this.ikhookHelper_0 is IAppContext)
				{
					searchTolerance = (double)(this.ikhookHelper_0 as IAppContext).Config.SelectionEnvironment.SearchTolerance;
				}
				searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this.ikhookHelper_0.MapControl.Map, searchTolerance);
				Editor.GetClosestSelectedFeature(this.ikhookHelper_0.MapControl.Map, ipoint_5, out feature);
				if (feature != null)
				{
					IGeometry shape = feature.Shape;
					IObjectClass @class = feature.Class;
					if ((shape.GeometryType == esriGeometryType.esriGeometryPoint ? false : shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
					{
						ZDEditTools.m_pEditFeature = feature;
						IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
						switch (shape.GeometryType)
						{
							case esriGeometryType.esriGeometryPolyline:
							{
								if (!this.method_5(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									unit = CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits);
									double length = (this.igeometry_0 as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (flag2)
									{
										ZDEditTools.m_PartIndex = num2;
										ZDEditTools.m_PointIndex = num;
										pointCount = 0;
										for (i = 0; i < num2; i++)
										{
											geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.idisplayFeedback_0 = new LineMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((ILineMovePointFeedback)this.idisplayFeedback_0).Start((IPolyline)shape, num, ipoint_5);
										break;
									}
									else
									{
										if (ZDEditTools.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(ZDEditTools.m_pEditFeature, this.igeometry_0, ref flag1);
											if (flag1)
											{
												(this.ikhookHelper_0.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_5(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
											bool_13 = true;
										}
										flag = true;
										return flag;
									}
								}
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								if (!this.method_5(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									double num3 = Math.Abs((this.igeometry_0 as IArea).Area);
									unit = string.Concat(" 平方", CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits));
									this.string_0 = string.Concat("原多边形面积: ", num3.ToString("0.###"), unit);
									if (flag2)
									{
										ZDEditTools.m_PartIndex = num2;
										ZDEditTools.m_PointIndex = num;
										pointCount = 0;
										for (i = 0; i < num2; i++)
										{
											geometry = (IPath)((IGeometryCollection)shape).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.idisplayFeedback_0 = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										IPolygonMovePointFeedback idisplayFeedback0 = (PolygonMovePointFeedback)this.idisplayFeedback_0;
										IPolygon polygonClass = new Polygon() as IPolygon;
										object missing = Type.Missing;
										(polygonClass as IGeometryCollection).AddGeometry(((IGeometryCollection)this.igeometry_0).Geometry[num2], ref missing, ref missing);
										idisplayFeedback0.Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										if (ZDEditTools.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(ZDEditTools.m_pEditFeature, this.igeometry_0, ref flag1);
											if (flag1)
											{
												(this.ikhookHelper_0.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_5(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
											bool_13 = true;
										}
										flag = true;
										return flag;
									}
								}
							}
						}
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private ISegment method_1(IFeature ifeature_0, int int_2, int int_3)
		{
			ISegment segment;
			if (int_3 != -1)
			{
				IGeometryCollection shape = ifeature_0.Shape as IGeometryCollection;
				segment = (shape.Geometry[int_2] as ISegmentCollection).Segment[int_3];
			}
			else
			{
				segment = null;
			}
			return segment;
		}

		private void method_10()
		{
			IEnumFeature featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
			if (featureSelection != null)
			{
				if (this.iset_0 == null)
				{
					this.iset_0 = new Set();
				}
				if (this.iset_0 != null)
				{
					this.iset_0.RemoveAll();
					featureSelection.Reset();
					for (IFeature i = featureSelection.Next(); i != null; i = featureSelection.Next())
					{
						this.iset_0.Add(i);
					}
				}
			}
		}

		private void method_11(bool bool_13)
		{
			if (bool_13)
			{
				this.bool_8 = bool_13;
				if (this.ikhookHelper_0.MapControl.Map.SelectionCount != 1)
				{
					this.ikhookHelper_0.SetStatus("");
				}
				else
				{
					IEnumFeature featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
					featureSelection.Reset();
					IFeature feature = featureSelection.Next();
					ILayer layer = this.method_14(this.ikhookHelper_0.MapControl.Map, feature);
					this.ikhookHelper_0.SetStatus(string.Concat("当前图层:", layer.Name));
				}
			}
			else
			{
				if (this.bool_8 != bool_13)
				{
					this.ikhookHelper_0.SetStatus("");
				}
				this.bool_8 = bool_13;
			}
		}

		private void method_12(ICompositeLayer icompositeLayer_0)
		{
			for (int i = 0; i < icompositeLayer_0.Count; i++)
			{
				ILayer layer = icompositeLayer_0.Layer[i];
				if (layer is IGroupLayer)
				{
					this.method_12(layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = layer as IFeatureLayer;
					if (featureLayer !=ZD.ZDEditTools.ZDFeatureLayer)
					{
						(featureLayer as IFeatureSelection).Clear();
					}
				}
			}
		}

		private IFeatureLayer method_13(ICompositeLayer icompositeLayer_0, IFeature ifeature_0)
		{
			IFeatureLayer featureLayer;
			int num = 0;
			while (true)
			{
				if (num < icompositeLayer_0.Count)
				{
					ILayer layer = icompositeLayer_0.Layer[num];
					if (layer is IGroupLayer)
					{
						IFeatureLayer featureLayer1 = this.method_13(layer as ICompositeLayer, ifeature_0);
						if (featureLayer1 != null)
						{
							featureLayer = featureLayer1;
							break;
						}
					}
					else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == ifeature_0.Class)
					{
						featureLayer = layer as IFeatureLayer;
						break;
					}
					num++;
				}
				else
				{
					featureLayer = null;
					break;
				}
			}
			return featureLayer;
		}

		private IFeatureLayer method_14(IMap imap_0, IFeature ifeature_0)
		{
			IFeatureLayer featureLayer;
			int num = 0;
			while (true)
			{
				if (num < imap_0.LayerCount)
				{
					ILayer layer = imap_0.Layer[num];
					if (layer is IGroupLayer)
					{
						IFeatureLayer featureLayer1 = this.method_13(layer as ICompositeLayer, ifeature_0);
						if (featureLayer1 != null)
						{
							featureLayer = featureLayer1;
							break;
						}
					}
					else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == ifeature_0.Class)
					{
						featureLayer = layer as IFeatureLayer;
						break;
					}
					num++;
				}
				else
				{
					featureLayer = null;
					break;
				}
			}
			return featureLayer;
		}

		private double method_15()
		{
			double num;
			num = (!(this.ikhookHelper_0 is IAppContext) ? 8 : (double)(this.ikhookHelper_0 as IAppContext).Config.SelectionEnvironment.SearchTolerance);
			return num;
		}

		private void method_16(int int_2, int int_3, int int_4, int int_5)
		{
			IEnvelope envelope;
			double mapUnits;
			IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
			IGeometry spatialReference = null;
			if (this.inewEnvelopeFeedback_0 != null)
			{
				spatialReference = this.inewEnvelopeFeedback_0.Stop();
			}
			if (spatialReference != null)
			{
				if (spatialReference.IsEmpty)
				{
					envelope = this.ipoint_1.Envelope;
					mapUnits = CommonHelper.ConvertPixelsToMapUnits(this.ikhookHelper_0.MapControl.Map as IActiveView, this.method_15());
					envelope.Height = mapUnits;
					envelope.Width = mapUnits;
					envelope.CenterAt(this.ipoint_1);
					spatialReference = envelope;
				}
				else if ((spatialReference.Envelope.Width == 0 ? true : spatialReference.Envelope.Height == 0))
				{
					envelope = this.ipoint_1.Envelope;
					mapUnits = CommonHelper.ConvertPixelsToMapUnits(this.ikhookHelper_0.MapControl.Map as IActiveView, this.method_15());
					envelope.Height = mapUnits;
					envelope.Width = mapUnits;
					envelope.CenterAt(this.ipoint_1);
					spatialReference = envelope;
				}
				spatialReference.SpatialReference = this.ikhookHelper_0.MapControl.Map.SpatialReference;
				ISelectionEnvironment selectionEnvironmentClass = null;
				if (!(this.ikhookHelper_0 is IAppContext))
				{
					selectionEnvironmentClass = new SelectionEnvironment();
				}
				else
				{
					selectionEnvironmentClass = (this.ikhookHelper_0 as IAppContext).Config.SelectionEnvironment;
				}
				esriSpatialRelEnum linearSelectionMethod = selectionEnvironmentClass.LinearSelectionMethod;
				esriSpatialRelEnum areaSelectionMethod = selectionEnvironmentClass.AreaSelectionMethod;
				if (this.int_0 != -1)
				{
					if (this.int_0 <= int_4)
					{
						selectionEnvironmentClass.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
						selectionEnvironmentClass.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
					}
					else
					{
						selectionEnvironmentClass.LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
						selectionEnvironmentClass.AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
					}
				}
				esriSelectionResultEnum combinationMethod = selectionEnvironmentClass.CombinationMethod;
				if (int_3 == 1)
				{
					selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
				}
				else if (int_3 != 2)
				{
					selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
				}
				else
				{
					selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultSubtract;
				}
				IEnvelope envelope1 = null;
				if (!ZDEditTools.DrawNode)
				{
					focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
				}
				else
				{
					ZDEditTools.DrawNode = false;
					IEnumFeature featureSelection = this.ikhookHelper_0.MapControl.Map.FeatureSelection as IEnumFeature;
					featureSelection.Reset();
					IFeature feature = featureSelection.Next();
					if (feature != null)
					{
						envelope1 = feature.Shape.Envelope;
						envelope1.Expand(10, 10, false);
						focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope1);
					}
				}
				try
				{
					this.bool_0 = false;
					ISpatialFilter spatialFilterClass = new SpatialFilter()
					{
						Geometry = spatialReference
					};
					if (selectionEnvironmentClass.CombinationMethod == esriSelectionResultEnum.esriSelectionResultNew)
					{
						this.ikhookHelper_0.MapControl.Map.ClearSelection();
					}
					spatialFilterClass.SpatialRel = selectionEnvironmentClass.AreaSelectionMethod;
					IFeatureCursor featureCursor = ZD.ZDEditTools.ZDFeatureLayer.Search(spatialFilterClass, false);
					for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
					{
						this.ikhookHelper_0.MapControl.Map.SelectFeature(ZD.ZDEditTools.ZDFeatureLayer, i);
					}
					ComReleaser.ReleaseCOMObject(featureCursor);
					this.method_11(true);
				}
				catch (COMException cOMException1)
				{
					COMException cOMException = cOMException1;
					if (cOMException.ErrorCode != -2147467259)
					{
						Logger.Current.Error("", cOMException, "");
					}
					else
					{
						MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
					}
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
				if (this.ikhookHelper_0.MapControl.Map.SelectionCount != 0)
				{
					focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
				}
				else
				{
					focusMap.Refresh();
				}
				if (this.ikhookHelper_0.MapControl.Map.SelectionCount > 1)
				{
					ZDEditTools.DrawNode = false;
				}
				this.bool_2 = false;
				this.bool_5 = false;
				selectionEnvironmentClass.CombinationMethod = combinationMethod;
				selectionEnvironmentClass.LinearSelectionMethod = linearSelectionMethod;
				selectionEnvironmentClass.AreaSelectionMethod = areaSelectionMethod;
				this.inewEnvelopeFeedback_0 = null;
				this.ikhookHelper_0.UpdateUI();
			}
		}

		private ISnappingResult method_17(IPoint ipoint_5, IActiveView iactiveView_0)
		{
			IEngineSnapEnvironment engineSnapEnvironment = ApplicationRef.AppContext.Config.EngineSnapEnvironment;
			ISnappingResult snappingResult = null;
			if (engineSnapEnvironment is ISnapEnvironment)
			{
				ISnapEnvironment snapEnvironment = engineSnapEnvironment as ISnapEnvironment;
				if ((snapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap ? false : snapEnvironment.SnapPoint(this.ipoint_3, ipoint_5)))
				{
					SnappingResult snappingResult1 = new SnappingResult()
					{
						X = ipoint_5.X,
						Y = ipoint_5.Y
					};
					snappingResult = snappingResult1;
				}
			}
			else if ((engineSnapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap ? false : engineSnapEnvironment.SnapPoint(ipoint_5)))
			{
				SnappingResult snappingResult2 = new SnappingResult()
				{
					X = ipoint_5.X,
					Y = ipoint_5.Y
				};
				snappingResult = snappingResult2;
			}
			return snappingResult;
		}

		private void method_18(IPoint ipoint_5, IPoint ipoint_6, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			ISnappingResult snappingResult;
			if (!Editor.UseOldSnap)
			{
				snappingResult = SketchToolAssist.m_psnaper.Snap(this.ipoint_2);
				if (snappingResult == null)
				{
					if (this.ianchorPoint_0 == null)
					{
						this.ianchorPoint_0 = new AnchorPoint()
						{
							Symbol = this.isimpleMarkerSymbol_0 as ISymbol
						};
					}
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this.ikhookHelper_0.MapControl.Map as IActiveView).ScreenDisplay);
				}
				else
				{
					this.ipoint_2 = snappingResult.Location;
					if (this.ianchorPoint_0 == null)
					{
						this.ianchorPoint_0 = new AnchorPoint()
						{
							Symbol = this.isimpleMarkerSymbol_0 as ISymbol
						};
					}
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this.ikhookHelper_0.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
			else
			{
				IAppContext application = ApplicationRef.AppContext;
				snappingResult = this.method_17(this.ipoint_2, this.ikhookHelper_0.MapControl.Map as IActiveView);
				if (snappingResult == null)
				{
					if (this.ianchorPoint_0 == null)
					{
						this.ianchorPoint_0 = new AnchorPoint()
						{
							Symbol = this.isimpleMarkerSymbol_0 as ISymbol
						};
					}
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this.ikhookHelper_0.MapControl.Map as IActiveView).ScreenDisplay);
				}
				else
				{
					this.ipoint_2 = snappingResult.Location;
					if (this.ianchorPoint_0 == null)
					{
						this.ianchorPoint_0 = new AnchorPoint()
						{
							Symbol = this.isimpleMarkerSymbol_0 as ISymbol
						};
					}
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this.ikhookHelper_0.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
		}

		private void method_19(IPoint ipoint_5, IPoint ipoint_6, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			ISimpleMarkerSymbol isimpleMarkerSymbol0 = this.isimpleMarkerSymbol_0;
			isimpleMarkerSymbol0.Style = esriSimpleMarkerStyle_0;
			IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
			if (this.ikhookHelper_0.Config.EngineSnapEnvironment != null)
			{
				IEngineSnapEnvironment snapEnvironment = this.ikhookHelper_0.Config.EngineSnapEnvironment;
				if (snapEnvironment is ISnapEnvironment)
				{
					if (!(snapEnvironment as ISnapEnvironment).SnapPoint(this.ipoint_3, ipoint_5))
					{
						this.bool_4 = false;
						if (this.ianchorPoint_0 != null)
						{
							this.ianchorPoint_0.Symbol = (ISymbol)isimpleMarkerSymbol0;
							this.ianchorPoint_0.MoveTo(ipoint_5, focusMap.ScreenDisplay);
						}
						else
						{
							this.method_20(ipoint_5, esriSimpleMarkerStyle_0);
						}
					}
					else
					{
						this.bool_4 = true;
						if (this.ianchorPoint_0 != null)
						{
							this.ianchorPoint_0.Symbol = (ISymbol)isimpleMarkerSymbol0;
							this.ianchorPoint_0.MoveTo(ipoint_6, focusMap.ScreenDisplay);
						}
						else
						{
							this.method_20(ipoint_6, esriSimpleMarkerStyle_0);
						}
					}
				}
				else if (!snapEnvironment.SnapPoint(ipoint_5))
				{
					this.bool_4 = false;
					if (this.ianchorPoint_0 != null)
					{
						this.ianchorPoint_0.Symbol = (ISymbol)isimpleMarkerSymbol0;
						this.ianchorPoint_0.MoveTo(ipoint_5, focusMap.ScreenDisplay);
					}
					else
					{
						this.method_20(ipoint_5, esriSimpleMarkerStyle_0);
					}
				}
				else
				{
					this.bool_4 = true;
					if (this.ianchorPoint_0 != null)
					{
						this.ianchorPoint_0.Symbol = (ISymbol)isimpleMarkerSymbol0;
						this.ianchorPoint_0.MoveTo(ipoint_6, focusMap.ScreenDisplay);
					}
					else
					{
						this.method_20(ipoint_6, esriSimpleMarkerStyle_0);
					}
				}
			}
		}

		private ISegment method_2(IFeature ifeature_0, int int_2)
		{
			ISegmentCollection geometry = (ifeature_0.Shape as IGeometryCollection).Geometry[int_2] as ISegmentCollection;
			return geometry.Segment[geometry.SegmentCount - 1];
		}

		private void method_20(IPoint ipoint_5, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
			this.ianchorPoint_0 = new AnchorPoint()
			{
				Symbol = this.isimpleMarkerSymbol_0 as ISymbol
			};
			this.ianchorPoint_0.MoveTo(ipoint_5, focusMap.ScreenDisplay);
		}

		private void method_21()
		{
		}

		private void method_22(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
		{
			if (this.Enabled)
			{
				if ((!ZDEditTools.DrawNode ? false : this.ikhookHelper_0.MapControl.Map.SelectionCount == 1))
				{
					IEnumFeature featureSelection = this.ikhookHelper_0.MapControl.Map.FeatureSelection as IEnumFeature;
					featureSelection.Reset();
					IFeature feature = featureSelection.Next();
					if (feature != null)
					{
						IPointCollection shape = (feature.Class as IFeatureClass).GetFeature(feature.OID).Shape as IPointCollection;
						if (shape != null)
						{
							idisplay_0.StartDrawing(0, -1);
							idisplay_0.SetSymbol(this.isymbol_0);
							IPointCollection multipointClass = new Multipoint();
							multipointClass.AddPointCollection(shape);
							idisplay_0.DrawMultipoint(multipointClass as IGeometry);
							idisplay_0.FinishDrawing();
							IColor color = (this.isymbol_0 as IMarkerSymbol).Color;
							IColor rgbColorClass = new RgbColor()
							{
								RGB = 255
							};
							(this.isymbol_0 as IMarkerSymbol).Color = rgbColorClass;
							idisplay_0.StartDrawing(0, -1);
							idisplay_0.SetSymbol(this.isymbol_0);
							idisplay_0.DrawPoint(multipointClass.Point[multipointClass.PointCount - 1]);
							idisplay_0.FinishDrawing();
							(this.isymbol_0 as IMarkerSymbol).Color = color;
						}
					}
				}
				//(this.ikhookHelper_0.MapControl.Map.SelectionCount <= 0 ? false : ZDEditTools.m_SelectionSetAnchorPoint != null);
			}
		}

		private void method_23(ISet iset_1)
		{
			iset_1.Reset();
		}

		private void method_24(object object_0)
		{
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.method_22);
					this.iactiveViewEvents_Event_0.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.method_25);
				}
				catch
				{
				}
			}
			this.iactiveViewEvents_Event_0 = this.ikhookHelper_0.MapControl.Map as IActiveViewEvents_Event;
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_22);
					this.iactiveViewEvents_Event_0.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.method_25);
				}
				catch
				{
				}
			}
		}

		private void method_25()
		{
			if (this.Enabled && this.bool_0)
			{
				this.method_11(true);
				this.ilist_0.Clear();
				this.int_1 = 0;
			}
		}

		private bool method_3(List<ISegment> list_1, ISegment isegment_0)
		{
			bool flag;
			foreach (ISegment list1 in list_1)
			{
				if (list1.GeometryType != isegment_0.GeometryType || Math.Abs(list1.Length - isegment_0.Length) >= 1E-05)
				{
					continue;
				}
				if (CommonHelper.distance(list1.FromPoint, isegment_0.FromPoint) >= 0.001 || CommonHelper.distance(list1.ToPoint, isegment_0.ToPoint) >= 0.001)
				{
					if (CommonHelper.distance(list1.FromPoint, isegment_0.ToPoint) >= 0.001 || CommonHelper.distance(list1.ToPoint, isegment_0.FromPoint) >= 0.001)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				else
				{
					flag = true;
					return flag;
				}
			}
			flag = false;
			return flag;
		}

		private bool method_4(IPoint ipoint_5)
		{
			bool flag;
			string unit;
			IPath geometry;
			object value;
			object mPointIndex;
			bool flag1;
			int num;
			this.string_0 = "";
			double num1 = 0;
			IFeature mPEditFeature = ZDEditTools.m_pEditFeature;
			bool flag2 = false;
			if (this.ikhookHelper_0.MapControl.Map.SelectionCount >= 1)
			{
				double searchTolerance = 4;
				if (this.ikhookHelper_0 is IAppContext)
				{
					searchTolerance = (double)(this.ikhookHelper_0 as IAppContext).Config.SelectionEnvironment.SearchTolerance;
				}
				searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this.ikhookHelper_0.MapControl.Map, searchTolerance);
				IGeometry shape = ZDEditTools.m_pEditFeature.Shape;
				IObjectClass @class = mPEditFeature.Class;
				if ((shape.GeometryType == esriGeometryType.esriGeometryPoint ? false : shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
				{
					IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
					switch (shape.GeometryType)
					{
						case esriGeometryType.esriGeometryPolyline:
						{
							if (ZDEditTools.m_HitType == HitType.None)
							{
								flag = false;
								return flag;
							}
							else
							{
								this.igeometry_0 = mPEditFeature.ShapeCopy;
								unit = CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits);
								double length = (this.igeometry_0 as IPolyline).Length;
								this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
								if (ZDEditTools.m_HitType != HitType.HitSegment)
								{
									num = ZDEditTools.m_PointIndex;
									int pointCount = 0;
									for (int i = 0; i < ZDEditTools.m_PartIndex; i++)
									{
										geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[i];
										pointCount = pointCount + (geometry as IPointCollection).PointCount;
									}
									num = num + pointCount;
									this.idisplayFeedback_0 = new LineMovePointFeedback()
									{
										Display = focusMap.ScreenDisplay
									};
									((ILineMovePointFeedback)this.idisplayFeedback_0).Start((IPolyline)shape, num, ipoint_5);
									break;
								}
								else
								{
									geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[ZDEditTools.m_PartIndex];
									value = Missing.Value;
									mPointIndex = ZDEditTools.m_PointIndex;
									((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
									flag1 = false;
									Editor.UpdateFeature(ZDEditTools.m_pEditFeature, this.igeometry_0, ref flag1);
									if (flag1)
									{
										(this.ikhookHelper_0.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
									}
									this.method_5(searchTolerance, this.ipoint_4, this.igeometry_0, out this.ipoint_4, ref num1, ref ZDEditTools.m_PartIndex, ref ZDEditTools.m_PointIndex, out flag2);
									this.bool_3 = false;
									this.cursor_0.Dispose();
									this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.VertexEdit.cur"));
									flag = true;
									return flag;
								}
							}
						}
						case esriGeometryType.esriGeometryPolygon:
						{
							if (ZDEditTools.m_HitType == HitType.None)
							{
								flag = false;
								return flag;
							}
							else
							{
								this.igeometry_0 = mPEditFeature.ShapeCopy;
								double num2 = Math.Abs((this.igeometry_0 as IArea).Area);
								unit = string.Concat(" 平方", CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits));
								this.string_0 = string.Concat("原多边形面积: ", num2.ToString("0.###"), unit);
								if (ZDEditTools.m_HitType == HitType.HitSegment)
								{
									geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[ZDEditTools.m_PartIndex];
									value = Missing.Value;
									mPointIndex = ZDEditTools.m_PointIndex;
									((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
									flag1 = false;
									Editor.UpdateFeature(ZDEditTools.m_pEditFeature, this.igeometry_0, ref flag1);
									if (flag1)
									{
										(this.ikhookHelper_0.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
									}
									this.method_5(searchTolerance, this.ipoint_4, this.igeometry_0, out this.ipoint_4, ref num1, ref ZDEditTools.m_PartIndex, ref ZDEditTools.m_PointIndex, out flag2);
									this.bool_3 = false;
									this.cursor_0.Dispose();
									this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.VertexEdit.cur"));
									flag = true;
									return flag;
								}
								else if (ZDEditTools.m_PartIndex != -1)
								{
									num = ZDEditTools.m_PointIndex;
									geometry = (IPath)((IGeometryCollection)shape).Geometry[ZDEditTools.m_PartIndex];
									IPoint point = (geometry as IPointCollection).Point[num];
									List<ZD.HitFeatureInfo> hitInfo = ZD.ZDEditTools.GetHitInfo(mPEditFeature, point);
									this.list_0 = hitInfo;
									if (hitInfo.Count != 0)
									{
										List<ISegment> segments = new List<ISegment>();
										this.idisplayFeedback_0 = new VertexFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										ISegment segment = this.method_1(mPEditFeature, ZDEditTools.m_PartIndex, num);
										if (segment != null)
										{
											segments.Add(segment);
											(this.idisplayFeedback_0 as IVertexFeedback).AddSegment(segment, false);
										}
										if (num != 0)
										{
											segment = this.method_1(mPEditFeature, ZDEditTools.m_PartIndex, num - 1);
											if (segment != null)
											{
												segments.Add(segment);
												(this.idisplayFeedback_0 as IVertexFeedback).AddSegment(segment, true);
											}
										}
										else
										{
											segment = this.method_2(mPEditFeature, ZDEditTools.m_PartIndex);
											segments.Add(segment);
											(this.idisplayFeedback_0 as IVertexFeedback).AddSegment(segment, true);
										}
										foreach (ZD.HitFeatureInfo hitFeatureInfo in hitInfo)
										{
											segment = hitFeatureInfo.GetSegment();
											if (segment != null && !this.method_3(segments, segment))
											{
												segments.Add(segment);
												(this.idisplayFeedback_0 as IVertexFeedback).AddSegment(segment, false);
											}
											segment = hitFeatureInfo.GetSegment2();
											if (segment == null || this.method_3(segments, segment))
											{
												continue;
											}
											segments.Add(segment);
											(this.idisplayFeedback_0 as IVertexFeedback).AddSegment(segment, true);
										}
										(this.idisplayFeedback_0 as IVertexFeedback).Refresh(focusMap.ScreenDisplay.hDC);
										(this.idisplayFeedback_0 as IVertexFeedback).MoveTo(ipoint_5);
										break;
									}
									else
									{
										this.idisplayFeedback_0 = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((PolygonMovePointFeedback)this.idisplayFeedback_0).Start((IPolygon)shape, num, ipoint_5);
										break;
									}
								}
								else
								{
									flag = false;
									return flag;
								}
							}
						}
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private bool method_5(double double_0, IPoint ipoint_5, IGeometry igeometry_1, out IPoint ipoint_6, ref double double_1, ref int int_2, ref int int_3, out bool bool_13)
		{
			bool flag;
			bool flag1 = false;
			IHitTest igeometry1 = (IHitTest)igeometry_1;
			ipoint_6 = new ESRI.ArcGIS.Geometry.Point();
			bool_13 = false;
			bool flag2 = false;
			if (!(igeometry_1.GeometryType == esriGeometryType.esriGeometryPoint ? false : igeometry_1.GeometryType != esriGeometryType.esriGeometryMultipoint))
			{
				if (!igeometry1.HitTest(ipoint_5, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_6, ref double_1, ref int_2, ref int_3, ref flag2))
				{
					bool_13 = false;
				}
				else
				{
					flag1 = true;
					bool_13 = true;
				}
				flag = flag1;
			}
			else if (igeometry1.HitTest(ipoint_5, double_0, esriGeometryHitPartType.esriGeometryPartVertex, ipoint_6, ref double_1, ref int_2, ref int_3, ref flag2))
			{
				bool_13 = true;
				flag = true;
			}
			else if (igeometry1.HitTest(ipoint_5, double_0, esriGeometryHitPartType.esriGeometryPartBoundary, ipoint_6, ref double_1, ref int_2, ref int_3, ref flag2))
			{
				bool_13 = false;
				flag = true;
			}
			else if ((igeometry_1.GeometryType == esriGeometryType.esriGeometryEnvelope ? false : igeometry_1.GeometryType != esriGeometryType.esriGeometryPolygon) || !((IRelationalOperator)igeometry_1).Contains(ipoint_5))
			{
				flag = false;
			}
			else
			{
				int_2 = -1;
				bool_13 = false;
				flag = true;
			}
			return flag;
		}

		private bool method_6(IPoint ipoint_5, int int_2)
		{
			bool flag;
			int i;
			IFeatureLayer layer;
			double searchTolerance = 16;
			if (this.ikhookHelper_0 is IAppContext)
			{
				searchTolerance = (double)(this.ikhookHelper_0 as IAppContext).Config.SelectionEnvironment.SearchTolerance;
			}
			searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this.ikhookHelper_0.MapControl.Map, searchTolerance);
			IEnvelope envelope = ipoint_5.Envelope;
			envelope.Height = searchTolerance;
			envelope.Width = searchTolerance;
			envelope.CenterAt(ipoint_5);
			ISelectionEnvironment selectionEnvironmentClass = new SelectionEnvironment();
			if (int_2 == 1)
			{
				selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultAdd;
			}
			else if (int_2 != 2)
			{
				selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
			}
			else
			{
				selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultXOR;
			}
			IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
			try
			{
				this.bool_0 = false;
				this.ikhookHelper_0.MapControl.Map.SelectByShape(envelope, selectionEnvironmentClass, true);
				this.method_11(true);
				this.bool_0 = true;
			}
			catch (COMException cOMException1)
			{
				COMException cOMException = cOMException1;
				if (cOMException.ErrorCode != -2147467259)
				{
					Logger.Current.Error("", cOMException, "");
				}
				else
				{
					MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
				}
			}
			catch (Exception exception)
			{
				Logger.Current.Error("", exception, "");
			}
			if (this.ikhookHelper_0.MapControl.Map.SelectionCount >= 1)
			{
				for (i = 0; i < this.ikhookHelper_0.MapControl.Map.LayerCount; i++)
				{
					layer = this.ikhookHelper_0.MapControl.Map.Layer[i] as IFeatureLayer;
					if (layer != null && !Editor.CheckIsEdit(layer))
					{
						(layer as IFeatureSelection).Clear();
					}
				}
				int selectionCount = this.ikhookHelper_0.MapControl.Map.SelectionCount;
				if (this.ikhookHelper_0.MapControl.Map.SelectionCount != 1)
				{
					if ((this.ikhookHelper_0.MapControl.Map.SelectionCount <= 1 ? false : int_2 == 0))
					{
						layer = null;
						IEnumFeature featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
						if (featureSelection != null)
						{
							featureSelection.Reset();
							IFeature feature = featureSelection.Next();
							while (feature != null && !Editor.CheckIsEdit(feature.Class as IDataset))
							{
								feature = featureSelection.Next();
							}
							this.ikhookHelper_0.MapControl.Map.ClearSelection();
							if (feature == null)
							{
								goto Label1;
							}
							i = 0;
							while (true)
							{
								if (i < this.ikhookHelper_0.MapControl.Map.LayerCount)
								{
									layer = this.ikhookHelper_0.MapControl.Map.Layer[i] as IFeatureLayer;
									if (layer == null || !(layer.FeatureClass.AliasName == feature.Class.AliasName))
									{
										i++;
									}
									else
									{
										this.ikhookHelper_0.MapControl.Map.SelectFeature(layer, feature);
										break;
									}
								}
								else
								{
									break;
								}
							}
							if (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
							{
								IPoint shape = (IPoint)feature.Shape;
								this.ienvelope_0.Height = 2;
								this.ienvelope_0.Width = 2;
								this.ienvelope_0.CenterAt(shape);
							}
							this.ienvelope_0 = feature.Extent;
							Editor.RefreshLayerWithSelection(this.ikhookHelper_0.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeoSelection);
							flag = true;
							return flag;
						}
						else
						{
							flag = false;
							return flag;
						}
					}
				Label1:
					flag = false;
				}
				else
				{
					this.ikhookHelper_0.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private void method_7(IPoint ipoint_5)
		{
			IGeometry shapeCopy;
			IInvalidArea3 invalidAreaClass;
			IWorkspace workspace;
			IFeature i;
			ITransform2D annotation;
			try
			{
				bool flag = false;
				if (this.idisplayFeedback_0 != null)
				{
					Editor.RefreshLayerWithSelection(this.ikhookHelper_0.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeography);
					if (this.idisplayFeedback_0 is IPolygonMovePointFeedback)
					{
						shapeCopy = ((IPolygonMovePointFeedback)this.idisplayFeedback_0).Stop();
						if (!(shapeCopy as ITopologicalOperator).IsSimple)
						{
							(shapeCopy as ITopologicalOperator).Simplify();
						}
					    invalidAreaClass = new InvalidArea() as IInvalidArea3;
                        
					        invalidAreaClass.Display = this.ikhookHelper_0.MapControl.ActiveView.ScreenDisplay;
					    
						invalidAreaClass.Add(ZDEditTools.m_pEditFeature);
						Editor.StartEditOperation();
						workspace = AppConfigInfo.GetWorkspace();
						if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
						{
							(workspace as IWorkspaceEdit).StartEditOperation();
						}
						ZD.ZDEditTools.UpdateFeatureGeometry(ZDEditTools.m_pEditFeature, shapeCopy);
						if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
						{
							(workspace as IWorkspaceEdit).StopEditOperation();
						}
						Editor.StopEditOperation();
						if (this.bool_1)
						{
							IArea shape = ZDEditTools.m_pEditFeature.Shape as IArea;
							double num = Math.Abs(shape.Area);
							string str = string.Concat(" 平方", CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits));
							string str1 = string.Concat(this.string_0, ", 最后多边形面积: ", num.ToString("0.###"), str);
							this.ikhookHelper_0.SetStatus(str1);
						}
						invalidAreaClass.Invalidate(-2);
					}
					else if (this.idisplayFeedback_0 is IVertexFeedback)
					{
						Editor.StartEditOperation();
						workspace = AppConfigInfo.GetWorkspace();
						if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
						{
							(workspace as IWorkspaceEdit).StartEditOperation();
						}
						shapeCopy = ZDEditTools.m_pEditFeature.ShapeCopy;
						GeometryOperator.RelpacePoint(ipoint_5, shapeCopy, ZDEditTools.m_PartIndex, ZDEditTools.m_PointIndex);
						ZD.ZDEditTools.UpdateFeatureGeometry(ZDEditTools.m_pEditFeature, shapeCopy);
                        invalidAreaClass = new InvalidArea() as IInvalidArea3;

                        invalidAreaClass.Display = this.ikhookHelper_0.MapControl.ActiveView.ScreenDisplay;
                        invalidAreaClass.Add(ZDEditTools.m_pEditFeature);
						if (this.list_0 != null)
						{
							foreach (ZD.HitFeatureInfo list0 in this.list_0)
							{
								shapeCopy = list0.Feature.ShapeCopy;
								GeometryOperator.RelpacePoint(ipoint_5, shapeCopy, list0.PartIndex, list0.VertIndex);
								list0.Feature.Shape = shapeCopy;
								ZD.ZDEditTools.UpdateFeatureGeometry(list0.Feature, shapeCopy);
								invalidAreaClass.Add(list0.Feature);
							}
							this.list_0 = null;
						}
						if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
						{
							(workspace as IWorkspaceEdit).StopEditOperation();
						}
						Editor.StopEditOperation();
						invalidAreaClass.Invalidate(-2);
					}
					else if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
					{
						double x = ipoint_5.X - this.ipoint_3.X;
						double y = ipoint_5.Y - this.ipoint_3.Y;
						ISet setClass = new Set();
						ISet set = new Set();
						IEnumFeature featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
						featureSelection.Reset();
						for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
						{
							if (Editor.CheckIsEdit(i.Class as IDataset))
							{
								setClass.Add(i);
								set.Add(i.ShapeCopy);
							}
						}
						setClass.Reset();
						i = setClass.Next() as IFeature;
						set.Reset();
						while (i != null)
						{
							if (!(i is IAnnotationFeature2))
							{
								annotation = (ITransform2D)set.Next();
								annotation.Move(x, y);
								Editor.UpdateFeature(i, (IGeometry)annotation, ref flag);
							}
							else
							{
								annotation = (ITransform2D)((IAnnotationFeature2)i).Annotation;
								annotation.Move(x, y);
								Editor.UpdateFeature(this.ikhookHelper_0.MapControl.Map, i, annotation, ref flag);
							}
							IEnvelope extent = i.Extent;
							extent.Expand(1.4, 1.4, false);
							if (this.ienvelope_0 == null)
							{
								this.ienvelope_0 = extent;
							}
							else
							{
								this.ienvelope_0.Union(extent);
							}
							i = setClass.Next() as IFeature;
						}
						if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
						{
							(this.ikhookHelper_0.MapControl.Map as IActiveView).Refresh();
						}
					}
					Editor.RefreshLayerWithSelection(this.ikhookHelper_0.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeography);
				}
			}
			catch (Exception exception)
			{
				Logger.Current.Error("",exception, "");
			}
		}

		private void method_8(IEdgeFeature iedgeFeature_0, ISet iset_1)
		{
			int edgeFeatureCount;
			int i;
			IJunctionFeature fromJunctionFeature = iedgeFeature_0.FromJunctionFeature;
			iset_1.Add(fromJunctionFeature);
			if (fromJunctionFeature is ISimpleJunctionFeature)
			{
				edgeFeatureCount = (fromJunctionFeature as ISimpleJunctionFeature).EdgeFeatureCount;
				for (i = 0; i < edgeFeatureCount; i++)
				{
					iset_1.Add((fromJunctionFeature as ISimpleJunctionFeature).EdgeFeature[i]);
				}
			}
			IJunctionFeature toJunctionFeature = iedgeFeature_0.ToJunctionFeature;
			iset_1.Add(toJunctionFeature);
			if (toJunctionFeature is ISimpleJunctionFeature)
			{
				edgeFeatureCount = (toJunctionFeature as ISimpleJunctionFeature).EdgeFeatureCount;
				for (i = 0; i < edgeFeatureCount; i++)
				{
					iset_1.Add((toJunctionFeature as ISimpleJunctionFeature).EdgeFeature[i]);
				}
			}
		}

		private void method_9(IJunctionFeature ijunctionFeature_0, ISet iset_1)
		{
			if (ijunctionFeature_0 is ISimpleJunctionFeature)
			{
				int edgeFeatureCount = (ijunctionFeature_0 as ISimpleJunctionFeature).EdgeFeatureCount;
				for (int i = 0; i < edgeFeatureCount; i++)
				{
					iset_1.Add((ijunctionFeature_0 as ISimpleJunctionFeature).EdgeFeature[i]);
				}
			}
		}

		private static void old_acctor_mc()
		{
			ZDEditTools.m_HitType = HitType.None;
			ZDEditTools.m_CurrentPoint = new ESRI.ArcGIS.Geometry.Point();
			ZDEditTools.m_PartIndex = -1;
			ZDEditTools.m_PointIndex = -1;
			ZDEditTools._DrawNode = false;
			ZDEditTools.m_SelectionSetAnchorPoint = null;
		}

		public void OnCreate(object object_0)
		{
			this.ikhookHelper_0 = object_0 as IAppContext;
			if (this.ikhookHelper_0 is IAppContextEvents)
			{
				(this.ikhookHelper_0 as IAppContextEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_24);
			}
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.method_22);
					this.iactiveViewEvents_Event_0.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.method_25);
				}
				catch
				{
				}
			}
			if (this.ikhookHelper_0.MapControl.Map != null)
			{
				this.iactiveViewEvents_Event_0 = this.ikhookHelper_0.MapControl.Map as IActiveViewEvents_Event;
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_22);
					this.iactiveViewEvents_Event_0.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.method_25);
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
			}
		}

		public void OnDblClick()
		{
			if (this.ikhookHelper_0.MapControl.Map.SelectionCount == 1)
			{
				ZDEditTools.DrawNode = !ZDEditTools.DrawNode;
				if (this.ikhookHelper_0.MapControl.Map.SelectionCount == 1)
				{
					try
					{
						IEnumFeature featureSelection = this.ikhookHelper_0.MapControl.Map.FeatureSelection as IEnumFeature;
						featureSelection.Reset();
						IEnvelope envelope = featureSelection.Next().Shape.Envelope;
						envelope.Expand(10, 10, false);
						this.ikhookHelper_0.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope);
					}
					catch
					{
					}
				}
			}
			else
			{
				ZDEditTools.DrawNode = false;
			}
			this.ikhookHelper_0.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
		}

		public void OnKeyDown(int int_2, int int_3)
		{
			if (int_2 == 229)
			{
				this.bool_1 = !this.bool_1;
				this.ikhookHelper_0.SetStatus("");
			}
		}

		public void OnKeyUp(int int_2, int int_3)
		{
			CommonHelper.ConvertPixelsToMapUnits(this.ikhookHelper_0.MapControl.Map as IActiveView, 5);
			if (int_2 == 46)
			{
				ZD.ZDEditTools.DeletedSelectedZD(this.ikhookHelper_0.MapControl.Map, ZD.ZDEditTools.ZDFeatureLayer);
			}
		}

		public void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
		{
			if (int_2 != 2)
			{
				this.bool_9 = false;
				IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
				IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
				if ((ZD.ZDEditTools.ZDFeatureLayer as IFeatureSelection).SelectionSet.Count != 1 || this.bool_3 || !ZDEditTools.DrawNode || !this.method_4(mapPoint))
				{
					this.bool_2 = true;
					if (this.bool_3)
					{
						this.bool_2 = true;
						this.bool_5 = false;
					}
					else if (!this.bool_11)
					{
						this.bool_2 = false;
						this.bool_5 = true;
						this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
						this.int_0 = int_4;
						this.inewEnvelopeFeedback_0.Display = focusMap.ScreenDisplay;
						this.inewEnvelopeFeedback_0.Start(mapPoint);
					}
					else
					{
						bool drawNode = ZDEditTools.DrawNode;
						ZDEditTools.DrawNode = false;
						IActiveView activeView = (IActiveView)this.ikhookHelper_0.MapControl.Map;
						activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
						ZDEditTools.DrawNode = drawNode;
						if (!this.method_6(mapPoint, int_3))
						{
							this.bool_2 = false;
							this.bool_5 = true;
							this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback();
							this.int_0 = int_4;
							this.inewEnvelopeFeedback_0.Display = focusMap.ScreenDisplay;
							this.inewEnvelopeFeedback_0.Start(mapPoint);
						}
						activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
					}
					this.ipoint_1 = mapPoint;
					this.ipoint_3 = new ESRI.ArcGIS.Geometry.Point();
					this.ipoint_3.PutCoords(mapPoint.X, mapPoint.Y);
				}
				else
				{
					this.ipoint_1 = this.ipoint_4;
					this.bool_2 = true;
					this.ipoint_3 = new ESRI.ArcGIS.Geometry.Point();
					this.ipoint_3.PutCoords(mapPoint.X, mapPoint.Y);
				}
			}
			else
			{
				this.ipopuMenuWrap_0.Visible = false;
				this.OnMouseMove(int_2, int_3, int_4, int_5);
				this.bool_9 = !this.bool_9;
			}
		}

		public void OnMouseMove(int int_2, int int_3, int int_4, int int_5)
		{
			IEnumFeature featureSelection;
			IFeature i;
			string unit;
			if (int_2 != 2)
			{
				if (int_2 != 1)
				{
					this.ipoint_4 = null;
					ZDEditTools.m_HitType = HitType.None;
				}
				if ((int_2 != 0 ? true : int_3 != 1))
				{
					double num = 0;
					bool flag = false;
					int num1 = -1;
					int num2 = -1;
					this.bool_11 = false;
					IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
					IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
					ZDEditTools.m_CurrentPoint.PutCoords(mapPoint.X, mapPoint.Y);
					if (this.bool_5)
					{
						if (int_2 == 1 && this.inewEnvelopeFeedback_0 != null)
						{
							this.inewEnvelopeFeedback_0.MoveTo(mapPoint);
						}
					}
					else if (this.bool_2)
					{
						this.Init();
						if (int_2 == 1)
						{
							if (this.idisplayFeedback_0 != null)
							{
								this.ipoint_2 = mapPoint;
								this.method_18(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								this.idisplayFeedback_0.MoveTo(this.ipoint_2);
								if ((!this.bool_1 || this.igeometry_0 == null || ZDEditTools.m_PartIndex == -1 ? false : ZDEditTools.m_PointIndex != -1))
								{
									IPointCollection geometry = (this.igeometry_0 as IGeometryCollection).Geometry[ZDEditTools.m_PartIndex] as IPointCollection;
									geometry.UpdatePoint(ZDEditTools.m_PointIndex, this.ipoint_2);
									(this.igeometry_0 as IGeometryCollection).GeometriesChanged();
									string string0 = this.string_0;
									if (this.igeometry_0 is IPolyline)
									{
										unit = CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits);
										double length = (this.igeometry_0 as IPolyline).Length;
										string0 = string.Concat(string0, ", 调整后的线长度: ", length.ToString("0.###"), unit);
									}
									else if (this.igeometry_0 is IPolygon)
									{
										double num3 = Math.Abs((this.igeometry_0 as IArea).Area);
										unit = string.Concat(" 平方", CommonHelper.GetUnit(this.ikhookHelper_0.MapControl.Map.MapUnits));
										string0 = string.Concat(string0, ", 调整后的多边形面积: ", num3.ToString("0.###"), unit);
									}
									this.ikhookHelper_0.SetStatus(string0);
								}
								this.ipoint_1 = new Point()
								{
									X = mapPoint.X,
									Y = mapPoint.Y
								};
							}
							else
							{
								this.idisplayFeedback_0 = new MoveGeometryFeedback()
								{
									Display = focusMap.ScreenDisplay
								};
								featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
								featureSelection.Reset();
								for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
								{
									if (this.idisplayFeedback_0 is IMoveGeometryFeedback)
									{
										IGeometry shapeCopy = i.ShapeCopy;
										(this.idisplayFeedback_0 as IMoveGeometryFeedback).AddGeometry(shapeCopy);
									}
								}
								this.ipoint_2 = mapPoint;
								this.method_18(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								if (this.idisplayFeedback_0 is IMoveGeometryFeedback)
								{
									(this.idisplayFeedback_0 as IMoveGeometryFeedback).Start(mapPoint);
								}
								else if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
								{
									(this.idisplayFeedback_0 as IMoveGeometryFeedbackEx).Start(mapPoint);
								}
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.EditMove.cur"));
							}
						}
					}
					else if ((this.ikhookHelper_0.MapControl.Map.SelectionCount <= 0 ? true : int_2 != 0))
					{
						this.cursor_0.Dispose();
						this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Edit.cur"));
						this.bool_3 = false;
						this.Init();
					}
					else
					{
						this.ipoint_0 = mapPoint;
						featureSelection = (IEnumFeature)this.ikhookHelper_0.MapControl.Map.FeatureSelection;
						featureSelection.Reset();
						i = featureSelection.Next();
						double mapUnits = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this.ikhookHelper_0.MapControl.Map, 4);
						while (true)
						{
							if (i == null)
							{
								break;
							}
							else if (!Editor.CheckIsEdit(i.Class as IDataset) || !this.method_5(mapUnits, mapPoint, i.Shape, out this.ipoint_4, ref num, ref num2, ref num1, out flag))
							{
								i = featureSelection.Next();
							}
							else if (!(!flag ? true : !ZDEditTools.DrawNode))
							{
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.VertexEdit.cur"));
								this.bool_11 = true;
								ZDEditTools.m_pEditFeature = i;
								ZDEditTools.m_PartIndex = num2;
								ZDEditTools.m_PointIndex = num1;
								ZDEditTools.m_HitType = HitType.HitNode;
								break;
							}
							else if (!(num2 < 0 ? true : !ZDEditTools.DrawNode))
							{
								this.bool_3 = false;
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Digitise.cur"));
								this.bool_11 = true;
								ZDEditTools.m_HitType = HitType.HitSegment;
								ZDEditTools.m_pEditFeature = i;
								ZDEditTools.m_PartIndex = num2;
								ZDEditTools.m_PointIndex = num1;
								break;
							}
							else if (num2 != -1)
							{
								ZDEditTools.m_pEditFeature = i;
								ZDEditTools.m_PartIndex = num2;
								ZDEditTools.m_PointIndex = num1;
								ZDEditTools.m_HitType = HitType.HitInner;
								break;
							}
							else
							{
								ZDEditTools.m_pEditFeature = i;
								ZDEditTools.m_PartIndex = num2;
								ZDEditTools.m_PointIndex = num1;
								ZDEditTools.m_HitType = HitType.HitInner;
								break;
							}
						}
						if (!this.bool_11)
						{
							this.cursor_0.Dispose();
							this.bool_3 = false;
							this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Edit.cur"));
						}
						this.Init();
					}
				}
				else
				{
					this.cursor_0.Dispose();
					this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Edit.cur"));
					this.Init();
					this.bool_3 = false;
				}
			}
		}

		public void OnMouseUp(int int_2, int int_3, int int_4, int int_5)
		{
			if (int_2 != 2)
			{
				IActiveView focusMap = (IActiveView)this.ikhookHelper_0.MapControl.Map;
				if (!this.bool_5)
				{
					focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
					try
					{
						this.method_7(this.ipoint_2);
					}
					catch (Exception exception)
					{
						Logger.Current.Error("", exception, "");
					}
					this.cursor_0.Dispose();
					this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("JLK.Utility.Cursor.Edit.cur"));
					this.bool_3 = false;
					this.bool_2 = false;
					this.idisplayFeedback_0 = null;
				}
				else
				{
					try
					{
						this.method_16(int_2, int_3, int_4, int_5);
					}
					catch (Exception exception1)
					{
						Logger.Current.Error("", exception1, "");
					}
				}
			}
		}
	}
}