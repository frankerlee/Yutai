using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.AELicenseProvider;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	[LicenseProvider(typeof(AELicenseProviderEx))]
	public class EditTools
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

		private IAppContext  _appContext ;

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
				return EditTools.m_CurrentPoint;
			}
		}

		public System.Windows.Forms.Cursor Cursor
		{
			get
			{
				return this.cursor_0;
			}
		}

		public static IFeature EditFeature
		{
			get
			{
				return EditTools.m_pEditFeature;
			}
			set
			{
				EditTools.m_pEditFeature = value;
			}
		}

		public bool Enabled
		{
			get
			{
				bool flag;
				if (this._appContext.MapControl.Map == null)
				{
					flag = false;
				}
				else if (!(this._appContext.MapControl.Map.LayerCount == 0 || Editor.EditWorkspace == null ? false : Editor.EditMap != null))
				{
					Editor.DrawNode = false;
					this.method_9(false);
					flag = false;
				}
				else if (Editor.EditMap == this._appContext.MapControl.Map)
				{
					this.method_9(true);
					flag = true;
				}
				else
				{
					Editor.DrawNode = false;
					this.method_9(false);
					flag = false;
				}
				return flag;
			}
		}

		public static HitType HitType
		{
			get
			{
				return EditTools.m_HitType;
			}
			set
			{
				EditTools.m_HitType = value;
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
				return EditTools.m_PartIndex;
			}
			set
			{
				EditTools.m_PartIndex = value;
			}
		}

		public static int PointIndex
		{
			get
			{
				return EditTools.m_PointIndex;
			}
			set
			{
				EditTools.m_PointIndex = value;
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
				return EditTools.m_SelectionSetAnchorPoint;
			}
			set
			{
				EditTools.m_SelectionSetAnchorPoint = value;
			}
		}

		static EditTools()
		{
			EditTools.old_acctor_mc();
		}

		public EditTools()
		{
			this.iset_0 = new Set();
			this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
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
				if ((!this.bool_12 ? true : this.hitType_0 != EditTools.m_HitType))
				{
					this.bool_12 = true;
					this.hitType_0 = EditTools.m_HitType;
					this.ipopuMenuWrap_0.Clear();
					if (EditTools.m_HitType != HitType.None)
					{
						strArrays = new string[] { "MoveVertToPoint", "MoveVert", "-", "MoveFeaturesToPoint", "MoveFeatures", "-", "AddPointInGeometry", "-", "ViewGeometryInfo" };
						strArrays1 = strArrays;
					}
					else
					{
						strArrays = new string[] { "CopyCommand", "PasteCommand", "CopyFeaturesTool", "-", "Zoom2SelectedFeature", "DeleteSelectFeature", "ClearSelection", "-", "AttributeEdit" };
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
			EditTools.m_PartIndex = -1;
			EditTools.m_PointIndex = -1;
			this.string_0 = "";
			int num = -1;
			double num1 = 0;
			int num2 = -1;
			bool flag2 = false;
			if (this._appContext.MapControl.Map.SelectionCount >= 1)
			{
				double searchTolerance = 4;
				if (this._appContext is IAppContext)
				{
					searchTolerance = (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance;
				}
				searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this._appContext.MapControl.Map, searchTolerance);
				Editor.GetClosestSelectedFeature(this._appContext.MapControl.Map, ipoint_5, out feature);
				if (feature != null)
				{
					IGeometry shape = feature.Shape;
					IObjectClass @class = feature.Class;
					if ((shape.GeometryType == esriGeometryType.esriGeometryPoint ? false : shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
					{
						EditTools.m_pEditFeature = feature;
						IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
						switch (shape.GeometryType)
						{
							case esriGeometryType.esriGeometryPolyline:
							{
								if (!this.method_3(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.igeometry_0 as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
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
										if (Editor.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
											if (flag1)
											{
												(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_3(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
											bool_13 = true;
										}
										flag = true;
										return flag;
									}
								}
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								if (!this.method_3(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									double num3 = Math.Abs((this.igeometry_0 as IArea).Area);
									unit = string.Concat(" 平方", CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits));
									this.string_0 = string.Concat("原多边形面积: ", num3.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
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
										if (Editor.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
											if (flag1)
											{
												(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_3(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
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

		private bool method_1(IPoint ipoint_5)
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
			this.igeometry_0 = null;
			EditTools.m_PartIndex = -1;
			EditTools.m_PointIndex = -1;
			this.string_0 = "";
			int num = -1;
			double num1 = 0;
			int num2 = -1;
			bool flag2 = false;
			if (this._appContext.MapControl.Map.SelectionCount >= 1)
			{
				double searchTolerance = 4;
				if (this._appContext is IAppContext)
				{
					searchTolerance = (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance;
				}
				searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this._appContext.MapControl.Map, searchTolerance);
				Editor.GetClosestSelectedFeature(this._appContext.MapControl.Map, ipoint_5, out feature);
				if (feature != null)
				{
					IGeometry shape = feature.Shape;
					IObjectClass @class = feature.Class;
					if ((shape.GeometryType == esriGeometryType.esriGeometryPoint ? false : shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
					{
						EditTools.m_pEditFeature = feature;
						IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
						switch (shape.GeometryType)
						{
							case esriGeometryType.esriGeometryPolyline:
							{
								if (!this.method_3(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.igeometry_0 as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
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
										geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
										value = Missing.Value;
										obj = num;
										((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
										this.bool_3 = false;
										this.cursor_0.Dispose();
										this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
										flag = true;
										return flag;
									}
								}
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								if (!this.method_3(searchTolerance, ipoint_5, shape, out point, ref num1, ref num2, ref num, out flag2))
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = feature.ShapeCopy;
									double num3 = Math.Abs((this.igeometry_0 as IArea).Area);
									unit = string.Concat(" 平方", CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits));
									this.string_0 = string.Concat("原多边形面积: ", num3.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
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
										((PolygonMovePointFeedback)this.idisplayFeedback_0).Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[num2];
										value = Missing.Value;
										obj = num;
										((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, ipoint_5, this.igeometry_0, out point, ref num1, ref num2, ref num, out flag2);
										this.bool_3 = false;
										this.cursor_0.Dispose();
										this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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

		private void method_10(ICompositeLayer icompositeLayer_0)
		{
			for (int i = 0; i < icompositeLayer_0.Count; i++)
			{
				ILayer layer = icompositeLayer_0.Layer[i];
				if (layer is IGroupLayer)
				{
					this.method_10(layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = layer as IFeatureLayer;
					if (!Editor.CheckIsEdit(featureLayer))
					{
						(featureLayer as IFeatureSelection).Clear();
					}
				}
			}
		}

		private IFeatureLayer method_11(ICompositeLayer icompositeLayer_0, IFeature ifeature_0)
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
						IFeatureLayer featureLayer1 = this.method_11(layer as ICompositeLayer, ifeature_0);
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

		private IFeatureLayer method_12(IMap imap_0, IFeature ifeature_0)
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
						IFeatureLayer featureLayer1 = this.method_11(layer as ICompositeLayer, ifeature_0);
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

		private double method_13()
		{
			double num;
			num = (!(this._appContext is IAppContext) ? 8 : (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance);
			return num;
		}

		private bool method_14()
		{
			bool flag;
			IEnumFeature featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
			featureSelection.Reset();
			IFeature feature = featureSelection.Next();
			while (true)
			{
				if (feature == null)
				{
					flag = false;
					break;
				}
				else if (feature is INetworkFeature)
				{
					flag = true;
					break;
				}
				else
				{
					feature = featureSelection.Next();
				}
			}
			return flag;
		}

		private ISnappingResult method_15(IPoint ipoint_5, IActiveView iactiveView_0)
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

		private void method_16(IPoint ipoint_5, IPoint ipoint_6, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
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
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
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
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
			else
			{
				IAppContext application = ApplicationRef.AppContext;
				snappingResult = this.method_15(this.ipoint_2, this._appContext.MapControl.Map as IActiveView);
				if (snappingResult == null)
				{
					if (this.ianchorPoint_0 == null)
					{
						this.ianchorPoint_0 = new AnchorPoint()
						{
							Symbol = this.isimpleMarkerSymbol_0 as ISymbol
						};
					}
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
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
					this.ianchorPoint_0.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
		}

		private void method_17(IPoint ipoint_5, IPoint ipoint_6, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			ISimpleMarkerSymbol isimpleMarkerSymbol0 = this.isimpleMarkerSymbol_0;
			isimpleMarkerSymbol0.Style = esriSimpleMarkerStyle_0;
			IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
			if (this._appContext.Config.SnapEnvironment != null)
			{
				IEngineSnapEnvironment snapEnvironment = this._appContext.Config.SnapEnvironment;
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
							this.method_18(ipoint_5, esriSimpleMarkerStyle_0);
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
							this.method_18(ipoint_6, esriSimpleMarkerStyle_0);
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
						this.method_18(ipoint_5, esriSimpleMarkerStyle_0);
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
						this.method_18(ipoint_6, esriSimpleMarkerStyle_0);
					}
				}
			}
		}

		private void method_18(IPoint ipoint_5, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
			this.ianchorPoint_0 = new AnchorPoint()
			{
				Symbol = this.isimpleMarkerSymbol_0 as ISymbol
			};
			this.ianchorPoint_0.MoveTo(ipoint_5, focusMap.ScreenDisplay);
		}

		private void method_19()
		{
			if (this._appContext.MapControl.Map.SelectionCount != 0)
			{
				IEnumFeature featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
				featureSelection.Reset();
				IFeature feature = featureSelection.Next();
				double x = 0;
				double y = 0;
				int num = 0;
				bool flag = false;
				while (feature != null)
				{
					IGeometry shape = (feature.Class as IFeatureClass).GetFeature(feature.OID).Shape;
					if (shape is IPoint)
					{
						if (flag)
						{
							x = x + (shape as IPoint).X;
							y = y + (shape as IPoint).Y;
							num++;
						}
						else
						{
							flag = true;
							x = (shape as IPoint).X;
							y = (shape as IPoint).Y;
							num++;
						}
					}
					else if (shape is IPointCollection)
					{
						IPointCollection pointCollection = shape as IPointCollection;
						int pointCount = pointCollection.PointCount;
						
						for (int i = 0; i < pointCount; i++)
						{
							if (flag)
							{
								x = x + pointCollection.Point[i].X;
								y = y + pointCollection.Point[i].Y;
								num++;
							}
							else
							{
								flag = true;
								x = pointCollection.Point[i].X;
								y = pointCollection.Point[i].Y;
								num++;
							}
						}
					}
					feature = featureSelection.Next();
				}
				if (EditTools.m_SelectionSetAnchorPoint != null)
				{
					IEnvelope envelope = EditTools.m_SelectionSetAnchorPoint.Envelope;
					envelope.Expand(2, 2, false);
					this._appContext.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope);
				}
				EditTools.m_SelectionSetAnchorPoint = new ESRI.ArcGIS.Geometry.Point();
				EditTools.m_SelectionSetAnchorPoint.PutCoords(x / (double)num, y / (double)num);
			}
			else
			{
				EditTools.m_SelectionSetAnchorPoint = null;
			}
		}

		private bool method_2(IPoint ipoint_5)
		{
			bool flag;
			string unit;
			IPath geometry;
			object value;
			object mPointIndex;
			bool flag1;
			int num;
			int pointCount;
			int i;
			this.string_0 = "";
			double num1 = 0;
			IFeature mPEditFeature = EditTools.m_pEditFeature;
			bool flag2 = false;
			if (this._appContext.MapControl.Map.SelectionCount >= 1)
			{
				double searchTolerance = 4;
				if (this._appContext is IAppContext)
				{
					searchTolerance = (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance;
				}
				searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this._appContext.MapControl.Map, searchTolerance);
				if (EditTools.m_pEditFeature != null)
				{
					IGeometry shape = EditTools.m_pEditFeature.Shape;
					IObjectClass @class = mPEditFeature.Class;
					if ((shape.GeometryType == esriGeometryType.esriGeometryPoint ? false : shape.GeometryType != esriGeometryType.esriGeometryMultipoint))
					{
						
						IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
						switch (shape.GeometryType)
						{
							case esriGeometryType.esriGeometryPolyline:
							{
								if (EditTools.m_HitType == HitType.None)
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = mPEditFeature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.igeometry_0 as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (EditTools.m_HitType != HitType.HitSegment)
									{
										num = EditTools.m_PointIndex;
										pointCount = 0;
										for (i = 0; i < EditTools.m_PartIndex; i++)
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
										geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[EditTools.m_PartIndex];
										value = Missing.Value;
										mPointIndex = EditTools.m_PointIndex;
										((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, this.ipoint_4, this.igeometry_0, out this.ipoint_4, ref num1, ref EditTools.m_PartIndex, ref EditTools.m_PointIndex, out flag2);
										this.bool_3 = false;
										this.cursor_0.Dispose();
										this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
										flag = true;
										return flag;
									}
								}
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								if (EditTools.m_HitType == HitType.None)
								{
									flag = false;
									return flag;
								}
								else
								{
									this.igeometry_0 = mPEditFeature.ShapeCopy;
									double num2 = Math.Abs((this.igeometry_0 as IArea).Area);
									unit = string.Concat(" 平方", CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits));
									this.string_0 = string.Concat("原多边形面积: ", num2.ToString("0.###"), unit);
									if (EditTools.m_HitType != HitType.HitSegment)
									{
										num = EditTools.m_PointIndex;
										pointCount = 0;
										for (i = 0; i < EditTools.m_PartIndex; i++)
										{
											geometry = (IPath)((IGeometryCollection)shape).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.idisplayFeedback_0 = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((PolygonMovePointFeedback)this.idisplayFeedback_0).Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.igeometry_0).Geometry[EditTools.m_PartIndex];
										value = Missing.Value;
										mPointIndex = EditTools.m_PointIndex;
										((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.igeometry_0, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, this.ipoint_4, this.igeometry_0, out this.ipoint_4, ref num1, ref EditTools.m_PartIndex, ref EditTools.m_PointIndex, out flag2);
										this.bool_3 = false;
										this.cursor_0.Dispose();
										this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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

		private void method_20(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
		{
			if (this.Enabled)
			{
				if ((!Editor.DrawNode ? false : this._appContext.MapControl.Map.SelectionCount == 1))
				{
					IEnumFeature featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
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
				
			}
		}

		private void method_21(ISet iset_1)
		{
			iset_1.Reset();
		}

		private void method_22(object object_0)
		{
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.method_20);
					this.iactiveViewEvents_Event_0.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.method_23);
				}
				catch
				{
				}
			}
			this.iactiveViewEvents_Event_0 = this._appContext.MapControl.ActiveView as IActiveViewEvents_Event;
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_20);
					this.iactiveViewEvents_Event_0.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.method_23);
				}
				catch
				{
				}
			}
		}

		private void method_23()
		{
			if (this.Enabled)
			{
				this.method_19();
				if (this.bool_0)
				{
					this.method_9(true);
					this.ilist_0.Clear();
					this.int_1 = 0;
				}
			}
		}

		private bool method_3(double double_0, IPoint ipoint_5, IGeometry igeometry_1, out IPoint ipoint_6, ref double double_1, ref int int_2, ref int int_3, out bool bool_13)
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

		private bool method_4(IPoint ipoint_5, int int_2)
		{
			bool flag;
			int i;
			IFeatureLayer layer;
			double searchTolerance = 16;
			if (this._appContext is IAppContext)
			{
				searchTolerance = (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance;
			}
			searchTolerance = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this._appContext.MapControl.Map, searchTolerance);
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
			IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
			try
			{
				this.bool_0 = false;
				this._appContext.MapControl.Map.SelectByShape(envelope, selectionEnvironmentClass, true);
				this.method_9(true);
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
			if (this._appContext.MapControl.Map.SelectionCount >= 1)
			{
				for (i = 0; i < this._appContext.MapControl.Map.LayerCount; i++)
				{
					layer = this._appContext.MapControl.Map.Layer[i] as IFeatureLayer;
					if (layer != null && !Editor.CheckIsEdit(layer))
					{
						(layer as IFeatureSelection).Clear();
					}
				}
				int selectionCount = this._appContext.MapControl.Map.SelectionCount;
				if (this._appContext.MapControl.Map.SelectionCount != 1)
				{
					if ((this._appContext.MapControl.Map.SelectionCount <= 1 ? false : int_2 == 0))
					{
						layer = null;
						IEnumFeature featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
						if (featureSelection != null)
						{
							featureSelection.Reset();
							IFeature feature = featureSelection.Next();
							while (feature != null && !Editor.CheckIsEdit(feature.Class as IDataset))
							{
								feature = featureSelection.Next();
							}
							this._appContext.MapControl.Map.ClearSelection();
							if (feature == null)
							{
								goto Label1;
							}
							i = 0;
							while (true)
							{
								if (i < this._appContext.MapControl.Map.LayerCount)
								{
									layer = this._appContext.MapControl.Map.Layer[i] as IFeatureLayer;
									if (layer == null || !(layer.FeatureClass.AliasName == feature.Class.AliasName))
									{
										i++;
									}
									else
									{
										this._appContext.MapControl.Map.SelectFeature(layer, feature);
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
							Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeoSelection);
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
					this._appContext.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private void method_5(IPoint ipoint_5)
		{
			double x;
			double y;
			IEnumFeature featureSelection;
			IFeature i;
			ITransform2D shapeCopy;
			IEnvelope extent;
			IGeometry geometry;
			try
			{
				bool flag = false;
				if (this.idisplayFeedback_0 != null)
				{
					Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeography);
					if (this.idisplayFeedback_0 is IMoveGeometryFeedback)
					{
						x = ipoint_5.X - this.ipoint_3.X;
						y = ipoint_5.Y - this.ipoint_3.Y;
						featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
						featureSelection.Reset();
						for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
						{
							if (Editor.CheckIsEdit(i.Class as IDataset))
							{
								if (!(i is IAnnotationFeature2))
								{
									shapeCopy = (ITransform2D)i.ShapeCopy;
									shapeCopy.Move(x, y);
									Editor.UpdateFeature(this._appContext.MapControl.Map, i, shapeCopy, ref flag);
								}
								else
								{
									shapeCopy = (ITransform2D)((IAnnotationFeature2)i).Annotation;
									shapeCopy.Move(x, y);
									Editor.UpdateFeature(this._appContext.MapControl.Map, i, shapeCopy, ref flag);
								}
								extent = i.Extent;
								extent.Expand(3, 3, false);
								if (this.ienvelope_0 == null)
								{
									this.ienvelope_0 = extent;
								}
								else
								{
									this.ienvelope_0.Union(extent);
								}
							}
						}
					}
					else if (this.idisplayFeedback_0 is IPolygonMovePointFeedback)
					{
						geometry = ((IPolygonMovePointFeedback)this.idisplayFeedback_0).Stop();
						if (!(geometry as ITopologicalOperator).IsSimple)
						{
							(geometry as ITopologicalOperator).Simplify();
						}
						Editor.UpdateFeature(EditTools.m_pEditFeature, geometry, ref flag);
						if (this.bool_1)
						{
							IArea shape = EditTools.m_pEditFeature.Shape as IArea;
							double num = Math.Abs(shape.Area);
							string str = string.Concat(" 平方", CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits));
							string str1 = string.Concat(this.string_0, ", 最后多边形面积: ", num.ToString("0.###"), str);
							this._appContext.SetStatus(str1);
						}
						extent = EditTools.m_pEditFeature.Extent;
						if (!extent.IsEmpty)
						{
							extent.Expand(1.4, 1.4, false);
							if (this.ienvelope_0 == null)
							{
								this.ienvelope_0 = extent;
							}
							else
							{
								this.ienvelope_0.Union(extent);
							}
						}
					}
					else if (this.idisplayFeedback_0 is ILineMovePointFeedback)
					{
						geometry = ((ILineMovePointFeedback)this.idisplayFeedback_0).Stop();
						Editor.UpdateFeature(EditTools.m_pEditFeature, geometry, ref flag);
						extent = EditTools.m_pEditFeature.Extent;
						if (!extent.IsEmpty)
						{
							extent.Expand(1.4, 1.4, false);
							if (this.ienvelope_0 == null)
							{
								this.ienvelope_0 = extent;
							}
							else
							{
								this.ienvelope_0.Union(extent);
							}
						}
					}
					else if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
					{
						x = ipoint_5.X - this.ipoint_3.X;
						y = ipoint_5.Y - this.ipoint_3.Y;
						ISet setClass = new Set();
						ISet set = new Set();
						featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
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
								shapeCopy = (ITransform2D)set.Next();
								shapeCopy.Move(x, y);
								Editor.UpdateFeature(i, (IGeometry)shapeCopy, ref flag);
							}
							else
							{
								shapeCopy = (ITransform2D)((IAnnotationFeature2)i).Annotation;
								shapeCopy.Move(x, y);
								Editor.UpdateFeature(this._appContext.MapControl.Map, i, shapeCopy, ref flag);
							}
							extent = i.Extent;
							if (!extent.IsEmpty)
							{
								extent.Expand(1.4, 1.4, false);
								if (this.ienvelope_0 == null)
								{
									this.ienvelope_0 = extent;
								}
								else
								{
									this.ienvelope_0.Union(extent);
								}
							}
							i = setClass.Next() as IFeature;
						}
						if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
						{
							(this._appContext.MapControl.Map as IActiveView).Refresh();
						}
					}
					Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.ienvelope_0, esriViewDrawPhase.esriViewGeography);
				}
			}
			catch (Exception exception)
			{
				Logger.Current.Error("",exception, "");
			}
		}

		private void method_6(IEdgeFeature iedgeFeature_0, ISet iset_1)
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

		private void method_7(IJunctionFeature ijunctionFeature_0, ISet iset_1)
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

		private void method_8()
		{
			IEnumFeature featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
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

		private void method_9(bool bool_13)
		{
			if (bool_13)
			{
				this.bool_8 = bool_13;
				if (this._appContext.MapControl.Map.SelectionCount != 1)
				{
					this._appContext.SetStatus("");
				}
				else
				{
					IEnumFeature featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
					featureSelection.Reset();
					IFeature feature = featureSelection.Next();
					ILayer layer = this.method_12(this._appContext.MapControl.Map, feature);
					this._appContext.SetStatus(string.Concat("当前图层:", layer.Name));
				}
			}
			else
			{
				if (this.bool_8 != bool_13)
				{
					this._appContext.SetStatus("");
				}
				this.bool_8 = bool_13;
			}
		}

		private static void old_acctor_mc()
		{
			EditTools.m_HitType = HitType.None;
			EditTools.m_CurrentPoint = new ESRI.ArcGIS.Geometry.Point();
			EditTools.m_PartIndex = -1;
			EditTools.m_PointIndex = -1;
			EditTools.m_SelectionSetAnchorPoint = null;
		}

		public void OnCreate(object object_0)
		{
			this._appContext = object_0 as IAppContext;
			if (this._appContext is IAppContextEvents)
			{
				(this._appContext as IAppContextEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_22);
			}
			if (this.iactiveViewEvents_Event_0 != null)
			{
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.method_20);
					this.iactiveViewEvents_Event_0.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.method_23);
				}
				catch
				{
				}
			}
			if (this._appContext.MapControl.Map != null)
			{
				this.iactiveViewEvents_Event_0 = this._appContext.MapControl.ActiveView as IActiveViewEvents_Event;
				try
				{
					this.iactiveViewEvents_Event_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_20);
					this.iactiveViewEvents_Event_0.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.method_23);
				}
				catch (Exception exception)
				{
					Logger.Current.Error("", exception, "");
				}
			}
		}

		public void OnDblClick()
		{
			if (this._appContext.MapControl.Map.SelectionCount == 1)
			{
				Editor.DrawNode = !Editor.DrawNode;
				if (this._appContext.MapControl.Map.SelectionCount == 1)
				{
					try
					{
						IEnumFeature featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
						featureSelection.Reset();
						IEnvelope envelope = featureSelection.Next().Shape.Envelope;
						envelope.Expand(10, 10, false);
						this._appContext.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope);
					}
					catch
					{
					}
				}
			}
			else
			{
				Editor.DrawNode = false;
			}
			this._appContext.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
		}

		public void OnKeyDown(int int_2, int int_3)
		{
			if (int_2 == 229)
			{
				this.bool_1 = !this.bool_1;
				this._appContext.SetStatus("");
			}
		}

		public void OnKeyUp(int int_2, int int_3)
		{
			double mapUnits = CommonHelper.ConvertPixelsToMapUnits(this._appContext.MapControl.Map as IActiveView, 5);
			int int2 = int_2;
			switch (int2)
			{
				case 37:
				{
					if (int_3 != 1)
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, -mapUnits, 0);
						break;
					}
					else
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, -mapUnits / 2, 0);
						break;
					}
				}
				case 38:
				{
					if (int_3 != 1)
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, 0, mapUnits);
						break;
					}
					else
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, 0, mapUnits / 2);
						break;
					}
				}
				case 39:
				{
					if (int_3 != 1)
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, mapUnits, 0);
						break;
					}
					else
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, mapUnits / 2, 0);
						break;
					}
				}
				case 40:
				{
					if (int_3 != 1)
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, 0, -mapUnits);
						break;
					}
					else
					{
						Editor.MoveSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace, 0, -mapUnits / 2);
						break;
					}
				}
				default:
				{
					if (int2 == 46)
					{
						Editor.DeletedSelectedFeatures(this._appContext.MapControl.Map, Editor.EditWorkspace as IWorkspace);
						break;
					}
					else
					{
						switch (int2)
						{
							case 77:
							{
								this.bool_10 = !this.bool_10;
								break;
							}
							case 78:
							{
								if (this.ilist_0.Count <= 0)
								{
									return;
								}
								if (this.int_1 == this.ilist_0.Count)
								{
									this.int_1 = 0;
								}
								IFeature item = this.ilist_0[this.int_1] as IFeature;
								EditTools int1 = this;
								int1.int_1 = int1.int_1 + 1;
								ILayer layer = this.method_12(this._appContext.MapControl.Map, item);
								this.bool_0 = false;
								(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
								this._appContext.MapControl.Map.ClearSelection();
								this._appContext.MapControl.Map.SelectFeature(layer, item);
								(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
								this.bool_0 = true;
								this._appContext.SetStatus(layer.Name);
								break;
							}
						}
					}
					break;
				}
			}
		}

		public void OnMouseDown(int int_2, int int_3, int int_4, int int_5)
		{
			if (int_2 != 2)
			{
				this.bool_9 = false;
				IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
				IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
				if (this._appContext.MapControl.Map.SelectionCount != 1 || this.bool_3 || !Editor.DrawNode || !this.method_2(mapPoint))
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
						bool drawNode = Editor.DrawNode;
						Editor.DrawNode = false;
						IActiveView activeView = (IActiveView)this._appContext.MapControl.Map;
						activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
						Editor.DrawNode = drawNode;
						if (!this.method_4(mapPoint, int_3))
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
					EditTools.m_HitType = HitType.None;
				}
				if ((int_2 != 0 ? true : int_3 != 1))
				{
					double num = 0;
					bool flag = false;
					int num1 = -1;
					int num2 = -1;
					this.bool_11 = false;
					IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
					IPoint mapPoint = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
					EditTools.m_CurrentPoint.PutCoords(mapPoint.X, mapPoint.Y);
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
								this.method_16(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								this.idisplayFeedback_0.MoveTo(this.ipoint_2);
								if ((!this.bool_1 || this.igeometry_0 == null || EditTools.m_PartIndex == -1 ? false : EditTools.m_PointIndex != -1))
								{
									IPointCollection geometry = (this.igeometry_0 as IGeometryCollection).Geometry[EditTools.m_PartIndex] as IPointCollection;
									geometry.UpdatePoint(EditTools.m_PointIndex, this.ipoint_2);
									(this.igeometry_0 as IGeometryCollection).GeometriesChanged();
									string string0 = this.string_0;
									if (this.igeometry_0 is IPolyline)
									{
										unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
										double length = (this.igeometry_0 as IPolyline).Length;
										string0 = string.Concat(string0, ", 调整后的线长度: ", length.ToString("0.###"), unit);
									}
									else if (this.igeometry_0 is IPolygon)
									{
										double num3 = Math.Abs((this.igeometry_0 as IArea).Area);
										unit = string.Concat(" 平方", CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits));
										string0 = string.Concat(string0, ", 调整后的多边形面积: ", num3.ToString("0.###"), unit);
									}
									this._appContext.SetStatus(string0);
								}
								this.ipoint_1 = new Point()
								{
									X = mapPoint.X,
									Y = mapPoint.Y
								};
							}
							else
							{
								if (!this.method_14())
								{
									this.idisplayFeedback_0 = new MoveGeometryFeedback()
									{
										Display = focusMap.ScreenDisplay
									};
								}
								else
								{
									this.idisplayFeedback_0 = new MoveGeometryFeedbackEx();
									(this.idisplayFeedback_0 as IMoveGeometryFeedbackEx).Display = this._appContext.MapControl.ActiveView.ScreenDisplay;
								}
								featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
								featureSelection.Reset();
								for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
								{
									if (this.idisplayFeedback_0 is IMoveGeometryFeedback)
									{
										IGeometry shapeCopy = i.ShapeCopy;
										(this.idisplayFeedback_0 as IMoveGeometryFeedback).AddGeometry(shapeCopy);
									}
									else if (i is INetworkFeature)
									{
										(this.idisplayFeedback_0 as IMoveGeometryFeedbackEx).Add(i);
									}
								}
								this.ipoint_2 = mapPoint;
								this.method_16(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								if (this.idisplayFeedback_0 is IMoveGeometryFeedback)
								{
									(this.idisplayFeedback_0 as IMoveGeometryFeedback).Start(mapPoint);
								}
								else if (this.idisplayFeedback_0 is IMoveGeometryFeedbackEx)
								{
									(this.idisplayFeedback_0 as IMoveGeometryFeedbackEx).Start(mapPoint);
								}
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
							}
						}
					}
					else if ((this._appContext.MapControl.Map.SelectionCount <= 0 ? true : int_2 != 0))
					{
						this.cursor_0.Dispose();
						this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
						this.bool_3 = false;
						this.Init();
					}
					else
					{
						this.ipoint_0 = mapPoint;
						featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
						featureSelection.Reset();
						i = featureSelection.Next();
						double mapUnits = CommonHelper.ConvertPixelsToMapUnits((IActiveView)this._appContext.MapControl.Map, 4);
						while (true)
						{
							if (i == null)
							{
								break;
							}
							else if (!Editor.CheckIsEdit(i.Class as IDataset) || !this.method_3(mapUnits, mapPoint, i.Shape, out this.ipoint_4, ref num, ref num2, ref num1, out flag))
							{
								i = featureSelection.Next();
							}
							else if (!(!flag ? true : !Editor.DrawNode))
							{
								if (!this.bool_10)
								{
									this.bool_3 = false;
								}
								else
								{
									this.bool_3 = true;
								}
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
								this.bool_11 = true;
								EditTools.m_pEditFeature = i;
								EditTools.m_PartIndex = num2;
								EditTools.m_PointIndex = num1;
								EditTools.m_HitType = HitType.HitNode;
								break;
							}
							else if (!(num2 < 0 ? true : !Editor.DrawNode))
							{
								this.bool_3 = false;
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Digitise.cur"));
								this.bool_11 = true;
								EditTools.m_HitType = HitType.HitSegment;
								EditTools.m_pEditFeature = i;
								EditTools.m_PartIndex = num2;
								EditTools.m_PointIndex = num1;
								break;
							}
							else if (num2 != -1)
							{
								this.bool_3 = true;
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
								this.bool_11 = true;
								EditTools.m_pEditFeature = i;
								EditTools.m_PartIndex = num2;
								EditTools.m_PointIndex = num1;
								EditTools.m_HitType = HitType.HitInner;
								break;
							}
							else
							{
								this.bool_3 = true;
								this.cursor_0.Dispose();
								this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
								this.bool_11 = true;
								EditTools.m_pEditFeature = i;
								EditTools.m_PartIndex = num2;
								EditTools.m_PointIndex = num1;
								EditTools.m_HitType = HitType.HitInner;
								break;
							}
						}
						if (!this.bool_11)
						{
							this.cursor_0.Dispose();
							this.bool_3 = false;
							this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
						}
						this.Init();
					}
				}
				else
				{
					this.cursor_0.Dispose();
					this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
					this.Init();
					this.bool_3 = false;
				}
			}
		}

		public void OnMouseUp(int int_2, int int_3, int int_4, int int_5)
		{
			IEnvelope envelope;
			double mapUnits;
			IEnumFeature featureSelection;
			IFeature item;
			ILayer layer;
			if (int_2 != 2)
			{
				try
				{
					IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
					if (!this.bool_5)
					{
						focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_4, int_5);
						this.method_5(this.ipoint_2);
						this.cursor_0.Dispose();
						this.cursor_0 = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
					}
					else
					{
						bool flag = false;
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
								mapUnits = CommonHelper.ConvertPixelsToMapUnits(this._appContext.MapControl.Map as IActiveView, this.method_13());
								envelope.Height = mapUnits;
								envelope.Width = mapUnits;
								envelope.CenterAt(this.ipoint_1);
								spatialReference = envelope;
								flag = true;
							}
							else if ((spatialReference.Envelope.Width == 0 ? true : spatialReference.Envelope.Height == 0))
							{
								envelope = this.ipoint_1.Envelope;
								mapUnits = CommonHelper.ConvertPixelsToMapUnits(this._appContext.MapControl.Map as IActiveView, this.method_13());
								envelope.Height = mapUnits;
								envelope.Width = mapUnits;
								envelope.CenterAt(this.ipoint_1);
								spatialReference = envelope;
								flag = true;
							}
							spatialReference.SpatialReference = this._appContext.MapControl.Map.SpatialReference;
							ISelectionEnvironment selectionEnvironmentClass = null;
							if (!(this._appContext is IAppContext))
							{
								selectionEnvironmentClass = new SelectionEnvironment();
							}
							else
							{
								selectionEnvironmentClass = (this._appContext as IAppContext).Config.SelectionEnvironment;
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
								flag = false;
							}
							else if (int_3 != 2)
							{
								selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
							}
							else
							{
								selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultXOR;
								flag = false;
							}
							IEnvelope envelope1 = null;
							if (!Editor.DrawNode)
							{
								focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
							}
							else
							{
								Editor.DrawNode = false;
								featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
								double num = CommonHelper.ConvertPixelsToMapUnits(focusMap, 5);
								featureSelection.Reset();
								item = featureSelection.Next();
								if (item != null)
								{
									envelope1 = item.Shape.Envelope;
									envelope1.Expand(num, num, false);
									focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope1);
								}
							}
							try
							{
								this.bool_0 = false;
								this._appContext.MapControl.Map.SelectByShape(spatialReference, selectionEnvironmentClass, false);
								if (Editor.IsRemoveUnEditLayerSelect && this._appContext.MapControl.Map.SelectionCount > 0)
								{
									for (int i = 0; i < this._appContext.MapControl.Map.LayerCount; i++)
									{
										layer = this._appContext.MapControl.Map.Layer[i];
										if (layer is IGroupLayer)
										{
											this.method_10(layer as ICompositeLayer);
										}
										else if (layer is IFeatureLayer)
										{
											IFeatureLayer featureLayer = layer as IFeatureLayer;
											if (!Editor.LayerCanEdit(featureLayer))
											{
												(featureLayer as IFeatureSelection).Clear();
											}
										}
									}
								}
								this.bool_0 = true;
								this.ilist_0.Clear();
								if (!flag)
								{
									this.ienumFeature_0 = null;
								}
								else if (this._appContext.MapControl.Map.SelectionCount <= 1)
								{
									this.ienumFeature_0 = null;
								}
								else
								{
									this.bool_0 = false;
									featureSelection = this._appContext.MapControl.Map.FeatureSelection as IEnumFeature;
									featureSelection.Reset();
									item = featureSelection.Next();
									this.int_1 = 0;
									while (item != null)
									{
										this.ilist_0.Add(item);
										item = featureSelection.Next();
									}
									this._appContext.MapControl.Map.ClearSelection();
									item = this.ilist_0[0] as IFeature;
									this.int_1 = 1;
									layer = this.method_12(this._appContext.MapControl.Map, item);
									this._appContext.MapControl.Map.SelectFeature(layer, item);
									this.method_9(true);
									this.bool_0 = true;
								}
								this.method_9(true);
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
							if (this._appContext.MapControl.Map.SelectionCount != 0)
							{
								focusMap.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
							}
							else
							{
								focusMap.Refresh();
							}
							if (this._appContext.MapControl.Map.SelectionCount > 1)
							{
								Editor.DrawNode = false;
							}
							this.bool_2 = false;
							this.bool_5 = false;
							selectionEnvironmentClass.CombinationMethod = combinationMethod;
							selectionEnvironmentClass.LinearSelectionMethod = linearSelectionMethod;
							selectionEnvironmentClass.AreaSelectionMethod = areaSelectionMethod;
							this.inewEnvelopeFeedback_0 = null;
							this._appContext.UpdateUI();
							return;
						}
						else
						{
							return;
						}
					}
				}
				catch (Exception exception1)
				{
					Logger.Current.Error("", exception1, "");
				}
				this.bool_3 = false;
				this.bool_2 = false;
				this.idisplayFeedback_0 = null;
			}
		}
	}
}