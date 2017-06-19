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
	public class EditTools
	{
		private static HitType m_HitType;

		internal static IPoint m_CurrentPoint;

		private System.Windows.Forms.Cursor m_Cursor = null;

		internal static int m_PartIndex;

		internal static int m_PointIndex;

		internal static IFeature m_pEditFeature;

		private IActiveViewEvents_Event m_ActiveViewEvents;

		private bool bool_0 = true;

		private IList ilist_0 = new ArrayList();

		private IGeometry m_Geometry = null;

		private string string_0 = "";

		private bool bool_1 = true;

		private IPoint ipoint_0 = null;

		private ISet iset_0;

		private IAppContext  _appContext ;

		private IPoint ipoint_1;

		private IDisplayFeedback m_DisplayFeedback;

		private bool bool_2 = false;

		private bool bool_3 = false;

		private IPoint ipoint_2;

		private IPoint ipoint_3;

		private ISymbol m_Symbol;

		private ISymbol m_Symbol1 = null;

		private bool bool_4;

		private IAnchorPoint m_AnchorPoint;

		private ISimpleMarkerSymbol m_SimpleMarkerSymbol = new SimpleMarkerSymbol();

		private INewEnvelopeFeedback m_NewEnvelopeFeedback;

		private IEnvelope m_Envelope;

		private bool bool_5;

		private bool bool_6 = false;

		private bool bool_7 = false;

		private bool bool_8 = false;

		private bool bool_9 = false;

		private int int_0 = -1;

		private IEnumFeature m_EnumFeature = null;

		private int int_1 = 0;

		private bool bool_10 = false;

		private bool bool_11 = false;

		private IPoint ipoint_4;

		private IPopuMenuWrap m_PopuMenuWrap = null;

		private HitType p_HitType = HitType.HitNode;

		private bool bool_12 = false;

		private static IPoint m_SelectionSetAnchorPoint;

	    private string[] _contextMenuKeys;
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
				return this.m_Cursor;
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
			    if (_appContext == null) return false;
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
				this.m_PopuMenuWrap = value;
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

		public EditTools(IAppContext context)
		{
		    _appContext = context;
			this.iset_0 = new Set();
			this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
			m_Symbol1 = new SimpleMarkerSymbol() as ISymbol;
			(m_Symbol1 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSX;
			(m_Symbol1 as ISimpleMarkerSymbol).Size = 4;
			this.m_Symbol = new SimpleMarkerSymbol() as ISymbol;
            (this.m_Symbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
			(this.m_Symbol as ISimpleMarkerSymbol).Size = 4;
			IRgbColor rgbColorClass = new RgbColor()
			{
				Red = 183,
				Green = 120,
				Blue = 245
			};
			(this.m_Symbol as ISimpleMarkerSymbol).Color = rgbColorClass;
			this.m_SimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
			this.m_SimpleMarkerSymbol.Size = 8;
			this.m_SimpleMarkerSymbol.Outline = true;
			this.m_SimpleMarkerSymbol.Color = ColorManage.GetRGBColor(0, 255, 255);
		}

        public string[] ContextMenuKeys { get { return _contextMenuKeys; } }
		public void Init()
		{
			string[] strArrays;
			string[] strArrays1;
			if (!_appContext.RibbonMenu.GetContextMenuVisible())
			{
				if ((!this.bool_12 ? true : this.p_HitType != EditTools.m_HitType))
				{
					this.bool_12 = true;
					this.p_HitType = EditTools.m_HitType;
					//this.m_PopuMenuWrap.Clear();
					if (EditTools.m_HitType != HitType.None)
					{
						strArrays = new string[] { "Edit_MoveVertexToPoint", "Edit_MoveVertex", "-", "Edit_MoveFeatureToPoint", "Edit_MoveFeatures", "-", "Edit_AddPointInGeometry", "-", "ViewGeometryInfo" };
						strArrays1 = strArrays;
					    _contextMenuKeys = strArrays;
					}
					else
					{
						strArrays = new string[] { "Edit_Copy", "Edit_Paste", "Edit_CopyFeatureTools", "-", "Edit_Zoom2Selection", "Edit_DeleteSelection", "Edit_ClearSelection", "-", "Edit_Attribute" };
						strArrays1 = strArrays;
					}
                    _contextMenuKeys = strArrays;
                    bool flag = false;
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						flag = (i + 1 == (int)strArrays1.Length ? false : string.Compare(strArrays1[i + 1], "-") == 0);
						//this.m_PopuMenuWrap.AddItem(strArrays1[i], flag);
					}
				    _appContext.UpdateContextMenu();
				}
				else
				{
                    //this.m_PopuMenuWrap.UpdateUI();
                    _appContext.RefreshContextMenu();
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
			this.m_Geometry = null;
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
									this.m_Geometry = feature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.m_Geometry as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
										pointCount = 0;
										for (i = 0; i < num2; i++)
										{
											geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.m_DisplayFeedback = new LineMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((ILineMovePointFeedback)this.m_DisplayFeedback).Start((IPolyline)shape, num, ipoint_5);
										break;
									}
									else
									{
										if (Editor.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
											if (flag1)
											{
												(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_3(searchTolerance, ipoint_5, this.m_Geometry, out point, ref num1, ref num2, ref num, out flag2);
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
									this.m_Geometry = feature.ShapeCopy;
									double num3 = Math.Abs((this.m_Geometry as IArea).Area);
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
										this.m_DisplayFeedback = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										IPolygonMovePointFeedback idisplayFeedback0 = (PolygonMovePointFeedback)this.m_DisplayFeedback;
										IPolygon polygonClass = new Polygon() as IPolygon;
										object missing = Type.Missing;
										(polygonClass as IGeometryCollection).AddGeometry(((IGeometryCollection)this.m_Geometry).Geometry[num2], ref missing, ref missing);
										idisplayFeedback0.Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										if (Editor.DrawNode)
										{
											geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[num2];
											value = Missing.Value;
											obj = num;
											((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
											flag1 = false;
											Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
											if (flag1)
											{
												(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
											}
											this.method_3(searchTolerance, ipoint_5, this.m_Geometry, out point, ref num1, ref num2, ref num, out flag2);
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
			this.m_Geometry = null;
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
									this.m_Geometry = feature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.m_Geometry as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (flag2)
									{
										EditTools.m_PartIndex = num2;
										EditTools.m_PointIndex = num;
										pointCount = 0;
										for (i = 0; i < num2; i++)
										{
											geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.m_DisplayFeedback = new LineMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((ILineMovePointFeedback)this.m_DisplayFeedback).Start((IPolyline)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[num2];
										value = Missing.Value;
										obj = num;
										((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, ipoint_5, this.m_Geometry, out point, ref num1, ref num2, ref num, out flag2);
										this.bool_3 = false;
										this.m_Cursor.Dispose();
										this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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
									this.m_Geometry = feature.ShapeCopy;
									double num3 = Math.Abs((this.m_Geometry as IArea).Area);
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
										this.m_DisplayFeedback = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((PolygonMovePointFeedback)this.m_DisplayFeedback).Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[num2];
										value = Missing.Value;
										obj = num;
										((IPointCollection)geometry).AddPoint(point, ref value, ref obj);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, ipoint_5, this.m_Geometry, out point, ref num1, ref num2, ref num, out flag2);
										this.bool_3 = false;
										this.m_Cursor.Dispose();
										this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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

		private void ClearLayerSelection(ICompositeLayer pCompLayer)
		{
			for (int i = 0; i < pCompLayer.Count; i++)
			{
				ILayer layer = pCompLayer.Layer[i];
				if (layer is IGroupLayer)
				{
					this.ClearLayerSelection(layer as ICompositeLayer);
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

		private IFeatureLayer GetLayerByFeature(ICompositeLayer pCompLayer, IFeature pFeature)
		{
			IFeatureLayer featureLayer;
			int num = 0;
			while (true)
			{
				if (num < pCompLayer.Count)
				{
					ILayer layer = pCompLayer.Layer[num];
					if (layer is IGroupLayer)
					{
						IFeatureLayer featureLayer1 = this.GetLayerByFeature(layer as ICompositeLayer, pFeature);
						if (featureLayer1 != null)
						{
							featureLayer = featureLayer1;
							break;
						}
					}
					else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class)
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

		private IFeatureLayer GetLayerByFeature(IMap pMap, IFeature pFeature)
		{
			IFeatureLayer featureLayer;
			int num = 0;
			while (true)
			{
				if (num < pMap.LayerCount)
				{
					ILayer layer = pMap.Layer[num];
					if (layer is IGroupLayer)
					{
						IFeatureLayer featureLayer1 = this.GetLayerByFeature(layer as ICompositeLayer, pFeature);
						if (featureLayer1 != null)
						{
							featureLayer = featureLayer1;
							break;
						}
					}
					else if (layer is IFeatureLayer && (layer as IFeatureLayer).FeatureClass == pFeature.Class)
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

		private double GetSearchTolerance()
		{
			double num;
			num = (!(this._appContext is IAppContext) ? 8 : (double)(this._appContext as IAppContext).Config.SelectionEnvironment.SearchTolerance);
			return num;
		}

		private bool IsNetworkFeature()
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

		private ISnappingResult SnapPoint(IPoint pPoint, IActiveView pActiveView)
		{
			IEngineSnapEnvironment engineSnapEnvironment = ApplicationRef.AppContext.Config.EngineSnapEnvironment;
			ISnappingResult snappingResult = null;
			if (engineSnapEnvironment is ISnapEnvironment)
			{
				ISnapEnvironment snapEnvironment = engineSnapEnvironment as ISnapEnvironment;
				if ((snapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap ? false : snapEnvironment.SnapPoint(this.ipoint_3, pPoint)))
				{
					SnappingResult snappingResult1 = new SnappingResult()
					{
						X = pPoint.X,
						Y = pPoint.Y
					};
					snappingResult = snappingResult1;
				}
			}
			else if ((engineSnapEnvironment == null || !ApplicationRef.AppContext.Config.UseSnap ? false : engineSnapEnvironment.SnapPoint(pPoint)))
			{
				SnappingResult snappingResult2 = new SnappingResult()
				{
					X = pPoint.X,
					Y = pPoint.Y
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
					if (this.m_AnchorPoint == null)
					{
						this.m_AnchorPoint = new AnchorPoint()
						{
							Symbol = this.m_SimpleMarkerSymbol as ISymbol
						};
					}
					this.m_AnchorPoint.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
				else
				{
					this.ipoint_2 = snappingResult.Location;
					if (this.m_AnchorPoint == null)
					{
						this.m_AnchorPoint = new AnchorPoint()
						{
							Symbol = this.m_SimpleMarkerSymbol as ISymbol
						};
					}
					this.m_AnchorPoint.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
			else
			{
				IAppContext application = ApplicationRef.AppContext;
				snappingResult = this.SnapPoint(this.ipoint_2, this._appContext.MapControl.Map as IActiveView);
				if (snappingResult == null)
				{
					if (this.m_AnchorPoint == null)
					{
						this.m_AnchorPoint = new AnchorPoint()
						{
							Symbol = this.m_SimpleMarkerSymbol as ISymbol
						};
					}
					this.m_AnchorPoint.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
				else
				{
					this.ipoint_2 = snappingResult.Location;
					if (this.m_AnchorPoint == null)
					{
						this.m_AnchorPoint = new AnchorPoint()
						{
							Symbol = this.m_SimpleMarkerSymbol as ISymbol
						};
					}
					this.m_AnchorPoint.MoveTo(this.ipoint_2, (this._appContext.MapControl.Map as IActiveView).ScreenDisplay);
				}
			}
		}

		private void method_17(IPoint ipoint_5, IPoint ipoint_6, esriSimpleMarkerStyle esriSimpleMarkerStyle_0)
		{
			ISimpleMarkerSymbol isimpleMarkerSymbol0 = this.m_SimpleMarkerSymbol;
			isimpleMarkerSymbol0.Style = esriSimpleMarkerStyle_0;
			IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
			if (this._appContext.Config.EngineSnapEnvironment != null)
			{
				IEngineSnapEnvironment snapEnvironment = this._appContext.Config.EngineSnapEnvironment;
				if (snapEnvironment is ISnapEnvironment)
				{
					if (!(snapEnvironment as ISnapEnvironment).SnapPoint(this.ipoint_3, ipoint_5))
					{
						this.bool_4 = false;
						if (this.m_AnchorPoint != null)
						{
							this.m_AnchorPoint.Symbol = (ISymbol)isimpleMarkerSymbol0;
							this.m_AnchorPoint.MoveTo(ipoint_5, focusMap.ScreenDisplay);
						}
						else
						{
							this.method_18(ipoint_5, esriSimpleMarkerStyle_0);
						}
					}
					else
					{
						this.bool_4 = true;
						if (this.m_AnchorPoint != null)
						{
							this.m_AnchorPoint.Symbol = (ISymbol)isimpleMarkerSymbol0;
							this.m_AnchorPoint.MoveTo(ipoint_6, focusMap.ScreenDisplay);
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
					if (this.m_AnchorPoint != null)
					{
						this.m_AnchorPoint.Symbol = (ISymbol)isimpleMarkerSymbol0;
						this.m_AnchorPoint.MoveTo(ipoint_5, focusMap.ScreenDisplay);
					}
					else
					{
						this.method_18(ipoint_5, esriSimpleMarkerStyle_0);
					}
				}
				else
				{
					this.bool_4 = true;
					if (this.m_AnchorPoint != null)
					{
						this.m_AnchorPoint.Symbol = (ISymbol)isimpleMarkerSymbol0;
						this.m_AnchorPoint.MoveTo(ipoint_6, focusMap.ScreenDisplay);
					}
					else
					{
						this.method_18(ipoint_6, esriSimpleMarkerStyle_0);
					}
				}
			}
		}

		private void method_18(IPoint ipoint_5, esriSimpleMarkerStyle pSimpleMarkerStyle)
		{
			IActiveView focusMap = (IActiveView)this._appContext.MapControl.Map;
			this.m_AnchorPoint = new AnchorPoint()
			{
				Symbol = this.m_SimpleMarkerSymbol as ISymbol
			};
			this.m_AnchorPoint.MoveTo(ipoint_5, focusMap.ScreenDisplay);
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
									this.m_Geometry = mPEditFeature.ShapeCopy;
									unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
									double length = (this.m_Geometry as IPolyline).Length;
									this.string_0 = string.Concat("原线长度: ", length.ToString("0.###"), unit);
									if (EditTools.m_HitType != HitType.HitSegment)
									{
										num = EditTools.m_PointIndex;
										pointCount = 0;
										for (i = 0; i < EditTools.m_PartIndex; i++)
										{
											geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[i];
											pointCount = pointCount + (geometry as IPointCollection).PointCount;
										}
										num = num + pointCount;
										this.m_DisplayFeedback = new LineMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((ILineMovePointFeedback)this.m_DisplayFeedback).Start((IPolyline)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[EditTools.m_PartIndex];
										value = Missing.Value;
										mPointIndex = EditTools.m_PointIndex;
										((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, this.ipoint_4, this.m_Geometry, out this.ipoint_4, ref num1, ref EditTools.m_PartIndex, ref EditTools.m_PointIndex, out flag2);
										this.bool_3 = false;
										this.m_Cursor.Dispose();
										this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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
									this.m_Geometry = mPEditFeature.ShapeCopy;
									double num2 = Math.Abs((this.m_Geometry as IArea).Area);
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
										this.m_DisplayFeedback = new PolygonMovePointFeedback()
										{
											Display = focusMap.ScreenDisplay
										};
										((PolygonMovePointFeedback)this.m_DisplayFeedback).Start((IPolygon)shape, num, ipoint_5);
										break;
									}
									else
									{
										geometry = (IPath)((IGeometryCollection)this.m_Geometry).Geometry[EditTools.m_PartIndex];
										value = Missing.Value;
										mPointIndex = EditTools.m_PointIndex;
										((IPointCollection)geometry).AddPoint(this.ipoint_4, ref value, ref mPointIndex);
										flag1 = false;
										Editor.UpdateFeature(EditTools.m_pEditFeature, this.m_Geometry, ref flag1);
										if (flag1)
										{
											(this._appContext.MapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
										}
										this.method_3(searchTolerance, this.ipoint_4, this.m_Geometry, out this.ipoint_4, ref num1, ref EditTools.m_PartIndex, ref EditTools.m_PointIndex, out flag2);
										this.bool_3 = false;
										this.m_Cursor.Dispose();
										this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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

		private void View_AfterDraw(IDisplay pDisplay, esriViewDrawPhase pViewDrawPhase)
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
							pDisplay.StartDrawing(0, -1);
							pDisplay.SetSymbol(m_Symbol);
							IPointCollection multipointClass = new Multipoint();
							multipointClass.AddPointCollection(shape);
							pDisplay.DrawMultipoint(multipointClass as IGeometry);
							pDisplay.FinishDrawing();
							IColor color = (m_Symbol as IMarkerSymbol).Color;
							IColor rgbColorClass = new RgbColor()
							{
								RGB = 255
							};
							(m_Symbol as IMarkerSymbol).Color = rgbColorClass;
							pDisplay.StartDrawing(0, -1);
							pDisplay.SetSymbol(m_Symbol);
							pDisplay.DrawPoint(multipointClass.Point[multipointClass.PointCount - 1]);
							pDisplay.FinishDrawing();
							(m_Symbol as IMarkerSymbol).Color = color;
						}
					}
				}
				
			}
		}

		private void method_21(ISet iset_1)
		{
			iset_1.Reset();
		}

		private void View_ActiveHookChanged(object object_0)
		{
			if (this.m_ActiveViewEvents != null)
			{
				try
				{
					this.m_ActiveViewEvents.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.View_AfterDraw);
					this.m_ActiveViewEvents.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.View_SelectionChanged);
				}
				catch
				{
				}
			}
			this.m_ActiveViewEvents = this._appContext.MapControl.ActiveView as IActiveViewEvents_Event;
			if (this.m_ActiveViewEvents != null)
			{
				try
				{
					this.m_ActiveViewEvents.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.View_AfterDraw);
					this.m_ActiveViewEvents.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.View_SelectionChanged);
				}
				catch
				{
				}
			}
		}

		private void View_SelectionChanged()
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
								this.m_Envelope.Height = 2;
								this.m_Envelope.Width = 2;
								this.m_Envelope.CenterAt(shape);
							}
							this.m_Envelope = feature.Extent;
							Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.m_Envelope, esriViewDrawPhase.esriViewGeoSelection);
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
				if (this.m_DisplayFeedback != null)
				{
					Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.m_Envelope, esriViewDrawPhase.esriViewGeography);
					if (this.m_DisplayFeedback is IMoveGeometryFeedback)
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
								if (this.m_Envelope == null)
								{
									this.m_Envelope = extent;
								}
								else
								{
									this.m_Envelope.Union(extent);
								}
							}
						}
					}
					else if (this.m_DisplayFeedback is IPolygonMovePointFeedback)
					{
						geometry = ((IPolygonMovePointFeedback)this.m_DisplayFeedback).Stop();
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
							if (this.m_Envelope == null)
							{
								this.m_Envelope = extent;
							}
							else
							{
								this.m_Envelope.Union(extent);
							}
						}
					}
					else if (this.m_DisplayFeedback is ILineMovePointFeedback)
					{
						geometry = ((ILineMovePointFeedback)this.m_DisplayFeedback).Stop();
						Editor.UpdateFeature(EditTools.m_pEditFeature, geometry, ref flag);
						extent = EditTools.m_pEditFeature.Extent;
						if (!extent.IsEmpty)
						{
							extent.Expand(1.4, 1.4, false);
							if (this.m_Envelope == null)
							{
								this.m_Envelope = extent;
							}
							else
							{
								this.m_Envelope.Union(extent);
							}
						}
					}
					else if (this.m_DisplayFeedback is IMoveGeometryFeedbackEx)
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
								if (this.m_Envelope == null)
								{
									this.m_Envelope = extent;
								}
								else
								{
									this.m_Envelope.Union(extent);
								}
							}
							i = setClass.Next() as IFeature;
						}
						if (this.m_DisplayFeedback is IMoveGeometryFeedbackEx)
						{
							(this._appContext.MapControl.Map as IActiveView).Refresh();
						}
					}
					Editor.RefreshLayerWithSelection(this._appContext.MapControl.Map, this.m_Envelope, esriViewDrawPhase.esriViewGeography);
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
					ILayer layer = this.GetLayerByFeature(this._appContext.MapControl.Map, feature);
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

		public void OnCreate(object context)
		{
			this._appContext = context as IAppContext;
			if (this._appContext is IAppContextEvents)
			{
				(this._appContext as IAppContextEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.View_ActiveHookChanged);
			}
			if (this.m_ActiveViewEvents != null)
			{
				try
				{
					this.m_ActiveViewEvents.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.View_AfterDraw);
					this.m_ActiveViewEvents.SelectionChanged -= new IActiveViewEvents_SelectionChangedEventHandler(this.View_SelectionChanged);
				}
				catch
				{
				}
			}
			if (this._appContext.MapControl.Map != null)
			{
				m_ActiveViewEvents = this._appContext.MapControl.ActiveView as IActiveViewEvents_Event;
				try
				{
					m_ActiveViewEvents.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.View_AfterDraw);
					m_ActiveViewEvents.SelectionChanged += new IActiveViewEvents_SelectionChangedEventHandler(this.View_SelectionChanged);
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
								ILayer layer = this.GetLayerByFeature(this._appContext.MapControl.Map, item);
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
						this.m_NewEnvelopeFeedback = new NewEnvelopeFeedback();
						this.int_0 = int_4;
						this.m_NewEnvelopeFeedback.Display = focusMap.ScreenDisplay;
						this.m_NewEnvelopeFeedback.Start(mapPoint);
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
							this.m_NewEnvelopeFeedback = new NewEnvelopeFeedback();
							this.int_0 = int_4;
							this.m_NewEnvelopeFeedback.Display = focusMap.ScreenDisplay;
							this.m_NewEnvelopeFeedback.Start(mapPoint);
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
				//this.m_PopuMenuWrap.Visible = false;
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
						if (int_2 == 1 && this.m_NewEnvelopeFeedback != null)
						{
							this.m_NewEnvelopeFeedback.MoveTo(mapPoint);
						}
					}
					else if (this.bool_2)
					{
						this.Init();
						if (int_2 == 1)
						{
							if (this.m_DisplayFeedback != null)
							{
								this.ipoint_2 = mapPoint;
								this.method_16(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								this.m_DisplayFeedback.MoveTo(this.ipoint_2);
								if ((!this.bool_1 || this.m_Geometry == null || EditTools.m_PartIndex == -1 ? false : EditTools.m_PointIndex != -1))
								{
									IPointCollection geometry = (this.m_Geometry as IGeometryCollection).Geometry[EditTools.m_PartIndex] as IPointCollection;
									geometry.UpdatePoint(EditTools.m_PointIndex, this.ipoint_2);
									(this.m_Geometry as IGeometryCollection).GeometriesChanged();
									string string0 = this.string_0;
									if (this.m_Geometry is IPolyline)
									{
										unit = CommonHelper.GetUnit(this._appContext.MapControl.Map.MapUnits);
										double length = (this.m_Geometry as IPolyline).Length;
										string0 = string.Concat(string0, ", 调整后的线长度: ", length.ToString("0.###"), unit);
									}
									else if (this.m_Geometry is IPolygon)
									{
										double num3 = Math.Abs((this.m_Geometry as IArea).Area);
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
								if (!this.IsNetworkFeature())
								{
									this.m_DisplayFeedback = new MoveGeometryFeedback()
									{
										Display = focusMap.ScreenDisplay
									};
								}
								else
								{
									this.m_DisplayFeedback = new MoveGeometryFeedbackEx();
									(this.m_DisplayFeedback as IMoveGeometryFeedbackEx).Display = this._appContext.MapControl.ActiveView.ScreenDisplay;
								}
								featureSelection = (IEnumFeature)this._appContext.MapControl.Map.FeatureSelection;
								featureSelection.Reset();
								for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
								{
									if (this.m_DisplayFeedback is IMoveGeometryFeedback)
									{
										IGeometry shapeCopy = i.ShapeCopy;
										(this.m_DisplayFeedback as IMoveGeometryFeedback).AddGeometry(shapeCopy);
									}
									else if (i is INetworkFeature)
									{
										(this.m_DisplayFeedback as IMoveGeometryFeedbackEx).Add(i);
									}
								}
								this.ipoint_2 = mapPoint;
								this.method_16(mapPoint, this.ipoint_2, esriSimpleMarkerStyle.esriSMSDiamond);
								if (this.m_DisplayFeedback is IMoveGeometryFeedback)
								{
									(this.m_DisplayFeedback as IMoveGeometryFeedback).Start(mapPoint);
								}
								else if (this.m_DisplayFeedback is IMoveGeometryFeedbackEx)
								{
									(this.m_DisplayFeedback as IMoveGeometryFeedbackEx).Start(mapPoint);
								}
								this.m_Cursor.Dispose();
								this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
							}
						}
					}
					else if ((this._appContext.MapControl.Map.SelectionCount <= 0 ? true : int_2 != 0))
					{
						this.m_Cursor.Dispose();
						this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
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
								this.m_Cursor.Dispose();
								this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.VertexEdit.cur"));
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
								this.m_Cursor.Dispose();
								this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Digitise.cur"));
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
								this.m_Cursor.Dispose();
								this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
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
								this.m_Cursor.Dispose();
								this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.EditMove.cur"));
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
							this.m_Cursor.Dispose();
							this.bool_3 = false;
							this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
						}
						this.Init();
					}
				}
				else
				{
					this.m_Cursor.Dispose();
					this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
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
						this.m_Cursor.Dispose();
						this.m_Cursor = new System.Windows.Forms.Cursor(this.GetType().Assembly.GetManifestResourceStream("Yutai.ArcGIS.Common.Cursor.Edit.cur"));
					}
					else
					{
						bool flag = false;
						IGeometry spatialReference = null;
						if (this.m_NewEnvelopeFeedback != null)
						{
							spatialReference = this.m_NewEnvelopeFeedback.Stop();
						}
						if (spatialReference != null)
						{
							if (spatialReference.IsEmpty)
							{
								envelope = this.ipoint_1.Envelope;
								mapUnits = CommonHelper.ConvertPixelsToMapUnits(this._appContext.MapControl.Map as IActiveView, this.GetSearchTolerance());
								envelope.Height = mapUnits;
								envelope.Width = mapUnits;
								envelope.CenterAt(this.ipoint_1);
								spatialReference = envelope;
								flag = true;
							}
							else if ((spatialReference.Envelope.Width == 0 ? true : spatialReference.Envelope.Height == 0))
							{
								envelope = this.ipoint_1.Envelope;
								mapUnits = CommonHelper.ConvertPixelsToMapUnits(this._appContext.MapControl.Map as IActiveView, this.GetSearchTolerance());
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
											this.ClearLayerSelection(layer as ICompositeLayer);
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
									this.m_EnumFeature = null;
								}
								else if (this._appContext.MapControl.Map.SelectionCount <= 1)
								{
									this.m_EnumFeature = null;
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
									layer = this.GetLayerByFeature(this._appContext.MapControl.Map, item);
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
							this.m_NewEnvelopeFeedback = null;
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
				this.m_DisplayFeedback = null;
			}
		}
	}
}