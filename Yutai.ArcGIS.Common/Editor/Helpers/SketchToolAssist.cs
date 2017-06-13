using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class SketchToolAssist
	{
		public static IGeometry m_LastPartGeometry;

		public static bool IsCreateParrel;

		public static double m_offset;

		public static int ConstructOffset;

		private static IDisplayFeedback m_Feedback;

		private static IPoint m_pUnDoPoint;

		private static IPoint m_pLastPoint1;

		private static IPoint m_pEndPoint1;

		private static bool m_bShowVlaue;

		private static double m_totalLength;

		public static IPointCollection m_pPointColn;

		internal static IAnchorPoint m_pAP;

		private static IPointCollection m_pPointCollection;

		private static int m_PointCount;

		private static bool m_bInUse;

		private static ISimpleMarkerSymbol m_pSym;

		private static IPoint m_pAnchorPoint;

		internal static IPointSnapper m_psnaper;

		public static IPoint AnchorPoint
		{
			get
			{
				return SketchToolAssist.m_pAnchorPoint;
			}
		}

		public static ITask CurrentTask
		{
			get;
			set;
		}

		public static IDisplayFeedback Feedback
		{
			get
			{
				return SketchToolAssist.m_Feedback;
			}
			set
			{
				SketchToolAssist.m_Feedback = value;
			}
		}

		public static double FixDirection
		{
			get;
			set;
		}

		public static double FixLength
		{
			get;
			set;
		}

		public static DrawTempGeometry IsDrawTempLine
		{
			get;
			set;
		}

		public static bool IsFixDirection
		{
			get;
			set;
		}

		public static bool IsFixLength
		{
			get;
			set;
		}

		public static IPoint LastPoint
		{
			get;
			set;
		}

		public static enumLineType LineType
		{
			get;
			set;
		}

		private static IMap Map
		{
			get;
			set;
		}

		public static IPoint StartPoint
		{
			get;
			set;
		}

		public static SysGrants SysGrants
		{
			get;
			protected set;
		}

		public static IGeometry TempLine
		{
			get;
			set;
		}

		static SketchToolAssist()
		{
			SketchToolAssist.old_acctor_mc();
		}

		public SketchToolAssist()
		{
		}

		internal static void AddNewAnchorPt(IPoint ipoint_0, esriSimpleMarkerStyle esriSimpleMarkerStyle_0, IActiveView pActiveView)
		{
			SketchToolAssist.m_pAP = new AnchorPoint()
			{
				Symbol = SketchToolAssist.m_pSym as ISymbol
			};
			SketchToolAssist.m_pAP.MoveTo(ipoint_0, pActiveView.ScreenDisplay);
		}

		public static void EndSketch(bool bool_0, IActiveView pActiveView, IFeatureLayer pFeatureLayer)
		{
			INewLineFeedback feedback;
			IPointCollection pointCollection;
			IGeometryCollection mLastPartGeometry;
			try
			{
				if (SketchToolAssist.m_pPointColn != null)
				{
					SketchToolAssist.m_pPointColn.RemovePoints(0, SketchToolAssist.m_pPointColn.PointCount);
				}
				IPoint mPAnchorPoint = null;
				if (SketchToolAssist.Feedback != null)
				{
					if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Line)
					{
						feedback = (INewLineFeedback)SketchToolAssist.Feedback;
						mPAnchorPoint = SketchToolAssist.m_pAnchorPoint;
						if (bool_0)
						{
							feedback.AddPoint(mPAnchorPoint);
						}
						SketchToolAssist.TempLine = feedback.Stop();
						SketchToolAssist.m_bInUse = false;
						if (SketchToolAssist.CurrentTask != null)
						{
							SketchToolAssist.CurrentTask.Excute();
						}
						SketchToolAssist.Feedback = null;
						return;
					}
					else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Fill)
					{
						INewPolygonFeedbackEx newPolygonFeedbackEx = (INewPolygonFeedbackEx)SketchToolAssist.Feedback;
						mPAnchorPoint = SketchToolAssist.m_pAnchorPoint;
						if (bool_0)
						{
							newPolygonFeedbackEx.AddPoint(mPAnchorPoint);
						}
						SketchToolAssist.TempLine = newPolygonFeedbackEx.Stop();
						SketchToolAssist.m_bInUse = false;
						if (SketchToolAssist.CurrentTask != null)
						{
							SketchToolAssist.CurrentTask.Excute();
						}
						SketchToolAssist.Feedback = null;
						return;
					}
					else if (SketchToolAssist.IsDrawTempLine != DrawTempGeometry.Point)
					{
						mPAnchorPoint = SketchToolAssist.m_pAnchorPoint;
						IGeometry mPPointCollection = null;
						if (SketchToolAssist.Feedback is INewMultiPointFeedback)
						{
							((INewMultiPointFeedback)SketchToolAssist.Feedback).Stop();
							mPPointCollection = (IGeometry)SketchToolAssist.m_pPointCollection;
						}
						else if (SketchToolAssist.Feedback is INewLineFeedback)
						{
							feedback = (INewLineFeedback)SketchToolAssist.Feedback;
							if (bool_0)
							{
								feedback.AddPoint(mPAnchorPoint);
							}
							IPolyline polyline = feedback.Stop();
							if (polyline != null)
							{
								pointCollection = (IPointCollection)polyline;
								if (pointCollection.PointCount >= 2)
								{
									mPPointCollection = (IGeometry)pointCollection;
									if (SketchToolAssist.m_LastPartGeometry != null && SketchToolAssist.m_LastPartGeometry is IPolyline)
									{
										mLastPartGeometry = SketchToolAssist.m_LastPartGeometry as IGeometryCollection;
										mLastPartGeometry.AddGeometryCollection(polyline as IGeometryCollection);
										mPPointCollection = mLastPartGeometry as IGeometry;
										SketchToolAssist.m_LastPartGeometry = null;
									}
								}
								SketchToolAssist.m_pLastPoint1 = null;
								SketchToolAssist.m_pEndPoint1 = null;
								SketchToolAssist.m_totalLength = 0;
								if ((mPPointCollection == null ? false : SketchToolAssist.IsCreateParrel))
								{
									object value = Missing.Value;
									object constructOffset = SketchToolAssist.ConstructOffset;
									IConstructCurve polylineClass = new Polyline() as IConstructCurve;
									polylineClass.ConstructOffset(mPPointCollection as IPolycurve, SketchToolAssist.m_offset, ref constructOffset, ref value);
									IGeometryCollection geometryCollection = new Polyline() as IGeometryCollection;
									geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
									polylineClass = new Polyline() as IConstructCurve;
									polylineClass.ConstructOffset(mPPointCollection as IPolycurve, -SketchToolAssist.m_offset, ref constructOffset, ref value);
									geometryCollection.AddGeometryCollection(polylineClass as IGeometryCollection);
									mPPointCollection = geometryCollection as IGeometry;
								}
							}
						}
						else if (SketchToolAssist.Feedback is INewPolygonFeedback)
						{
							INewPolygonFeedback newPolygonFeedback = (INewPolygonFeedback)SketchToolAssist.Feedback;
							if (bool_0)
							{
								newPolygonFeedback.AddPoint(mPAnchorPoint);
							}
							IPolygon polygon = newPolygonFeedback.Stop();
							if (polygon != null)
							{
								pointCollection = (IPointCollection)polygon;
								if (pointCollection.PointCount >= 3)
								{
									mPPointCollection = (IGeometry)pointCollection;
									if (!(mPPointCollection as ITopologicalOperator).IsSimple)
									{
										(mPPointCollection as ITopologicalOperator).Simplify();
									}
									if (SketchToolAssist.m_LastPartGeometry != null && SketchToolAssist.m_LastPartGeometry is IPolygon)
									{
										mLastPartGeometry = SketchToolAssist.m_LastPartGeometry as IGeometryCollection;
										mLastPartGeometry.AddGeometryCollection(polygon as IGeometryCollection);
										mPPointCollection = mLastPartGeometry as IGeometry;
										SketchToolAssist.m_LastPartGeometry = null;
									}
								}
								SketchToolAssist.m_pPointColn = null;
							}
						}
						CreateFeatureTool.CreateFeature(mPPointCollection, pActiveView, pFeatureLayer);
					}
					else
					{
						SketchToolAssist.TempLine = SketchToolAssist.m_pAnchorPoint;
						SketchToolAssist.m_bInUse = false;
						if (SketchToolAssist.CurrentTask != null)
						{
							SketchToolAssist.CurrentTask.Excute();
						}
						SketchToolAssist.Feedback = null;
						return;
					}
				}
			}
			catch (COMException cOMException1)
			{
				COMException cOMException = cOMException1;
				if (cOMException.ErrorCode == -2147220936)
				{
					MessageBox.Show("坐标值或量测值超出范围!", "创建要素", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else if (cOMException.ErrorCode != -2147220943)
				{
					Logger.Current.Error("",cOMException, "");
				}
				else
				{
					MessageBox.Show("面剪切操作无法将面的所有部分划分到剪切线的左侧或右侧!", "编辑", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pFeatureLayer, null);
				Logger.Current.Error("",exception, "");
			}
			SketchToolAssist.m_bInUse = false;
			SketchToolAssist.LastPoint = null;
			SketchToolAssist.Feedback = null;
			SketchToolAssist.m_PointCount = 0;
			SketchToolAssist.m_pAP = null;
		}

		public static void Init()
		{
			SketchToolAssist.SysGrants = new SysGrants(AppConfigInfo.UserID);
			SketchToolAssist.LineType = enumLineType.LTLine;
			SketchToolAssist.m_pSym.Style = esriSimpleMarkerStyle.esriSMSCircle;
			SketchToolAssist.m_pSym.Size = 8;
			SketchToolAssist.m_pSym.Outline = true;
			SketchToolAssist.m_pSym.Color = ColorManage.GetRGBColor(0, 255, 255);
		}

		private static void old_acctor_mc()
		{
			SketchToolAssist.m_LastPartGeometry = null;
			SketchToolAssist.IsCreateParrel = false;
			SketchToolAssist.m_offset = 1;
			SketchToolAssist.ConstructOffset = 5;
			SketchToolAssist.m_Feedback = null;
			SketchToolAssist.m_pUnDoPoint = null;
			SketchToolAssist.m_pLastPoint1 = null;
			SketchToolAssist.m_pEndPoint1 = null;
			SketchToolAssist.m_bShowVlaue = true;
			SketchToolAssist.m_totalLength = 0;
			SketchToolAssist.m_pPointColn = null;
			SketchToolAssist.m_pAP = null;
			SketchToolAssist.m_pPointCollection = null;
			SketchToolAssist.m_PointCount = 0;
			SketchToolAssist.m_bInUse = false;
			SketchToolAssist.m_pSym = new SimpleMarkerSymbol();
			SketchToolAssist.m_psnaper = new PointSnapper();
			SketchToolAssist.Init();
		}

		private static ISnappingResult OldSnap(IPoint ipoint_0, IActiveView pActiveView, IEngineSnapEnvironment iengineSnapEnvironment_0)
		{
			IHitTest mPPointColn;
			double num;
			int num1;
			int num2;
			bool flag;
			double mapUnits;
			IPoint pointClass;
			ISnappingResult snappingResult = null;
			if (iengineSnapEnvironment_0 is ISnapEnvironment)
			{
				ISnapEnvironment iengineSnapEnvironment0 = iengineSnapEnvironment_0 as ISnapEnvironment;
				if (!(iengineSnapEnvironment0 == null || !ApplicationRef.AppContext.Config.UseSnap ? true : !iengineSnapEnvironment0.SnapPoint(SketchToolAssist.LastPoint, ipoint_0)))
				{
					SnappingResult snappingResult1 = new SnappingResult()
					{
						X = ipoint_0.X,
						Y = ipoint_0.Y
					};
					snappingResult = snappingResult1;
				}
				else if (ApplicationRef.AppContext.Config.IsSnapSketch)
				{
					mPPointColn = SketchToolAssist.m_pPointColn as IHitTest;
					if (mPPointColn != null)
					{
						num = 0;
						num1 = 0;
						num2 = 0;
						flag = false;
						mapUnits = CommonHelper.ConvertPixelsToMapUnits(pActiveView, iengineSnapEnvironment0.SnapTolerance);
						if (mapUnits == 0)
						{
							mapUnits = 3;
						}
						pointClass = new ESRI.ArcGIS.Geometry.Point();
						if (mPPointColn.HitTest(ipoint_0, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num, ref num1, ref num2, ref flag))
						{
							SnappingResult snappingResult2 = new SnappingResult()
							{
								X = pointClass.X,
								Y = pointClass.Y
							};
							snappingResult = snappingResult2;
						}
					}
				}
			}
			else if (!(iengineSnapEnvironment_0 == null || !ApplicationRef.AppContext.Config.UseSnap ? true : !iengineSnapEnvironment_0.SnapPoint(ipoint_0)))
			{
				SnappingResult snappingResult3 = new SnappingResult()
				{
					X = ipoint_0.X,
					Y = ipoint_0.Y
				};
				snappingResult = snappingResult3;
			}
			else if (ApplicationRef.AppContext.Config.IsSnapSketch)
			{
				mPPointColn = SketchToolAssist.m_pPointColn as IHitTest;
				if (mPPointColn != null)
				{
					num = 0;
					num1 = 0;
					num2 = 0;
					flag = false;
					mapUnits = iengineSnapEnvironment_0.SnapTolerance;
					if (iengineSnapEnvironment_0.SnapToleranceUnits == esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
					{
						mapUnits = CommonHelper.ConvertPixelsToMapUnits(pActiveView, iengineSnapEnvironment_0.SnapTolerance);
					}
					if (mapUnits == 0)
					{
						mapUnits = 3;
					}
					pointClass = new ESRI.ArcGIS.Geometry.Point();
					if (mPPointColn.HitTest(ipoint_0, mapUnits, esriGeometryHitPartType.esriGeometryPartVertex, pointClass, ref num, ref num1, ref num2, ref flag))
					{
						SnappingResult snappingResult4 = new SnappingResult()
						{
							X = pointClass.X,
							Y = pointClass.Y
						};
						snappingResult = snappingResult4;
					}
				}
			}
			return snappingResult;
		}

		public static void ReSet()
		{
			SketchToolAssist.TempLine = null;
			SketchToolAssist.IsDrawTempLine = DrawTempGeometry.None;
			SketchToolAssist.CurrentTask = null;
			SketchToolAssist.m_pAnchorPoint = null;
		}

		public static void SetActiveMap(IMap imap_0)
		{
			SketchToolAssist.Map = imap_0;
			(SketchToolAssist.m_psnaper as PointSnapper).Map = imap_0;
		}

		public static string SketchMouseDown(IActiveView pActiveView, IFeatureLayer pFeatureLayer)
		{
			INewPolylineFeedback feedback;
			string str;
			INewPolygonFeedbackEx newPolygonFeedbackEx;
			object value;
			INewPolylineFeedback newPolylineFeedback;
			INewPolygonFeedbackEx feedback1;
			double num;
			double length;
			if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Line)
			{
				if (SketchToolAssist.Feedback != null)
				{
					feedback = (INewPolylineFeedback)SketchToolAssist.Feedback;
					feedback.AddPoint(SketchToolAssist.m_pAnchorPoint);
					object obj = Missing.Value;
					object value1 = Missing.Value;
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref obj, ref value1);
				}
				else
				{
					SketchToolAssist.m_bInUse = true;
					SketchToolAssist.Feedback = new NewPolylineFeedback();
					feedback = (INewPolylineFeedback)SketchToolAssist.Feedback;
					SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
					feedback.ChangeLineType(SketchToolAssist.LineType);
					feedback.Start(SketchToolAssist.m_pAnchorPoint);
					SketchToolAssist.m_pPointColn = new Polyline();
					object obj1 = Missing.Value;
					object value2 = Missing.Value;
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref obj1, ref value2);
				}
				str = "";
			}
			else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Fill)
			{
				if (SketchToolAssist.Feedback != null)
				{
					newPolygonFeedbackEx = (INewPolygonFeedbackEx)SketchToolAssist.Feedback;
					newPolygonFeedbackEx.AddPoint(SketchToolAssist.m_pAnchorPoint);
					object obj2 = Missing.Value;
					object value3 = Missing.Value;
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref obj2, ref value3);
				}
				else
				{
					SketchToolAssist.m_bInUse = true;
					SketchToolAssist.Feedback = new NewPolygonFeedbackEx();
					newPolygonFeedbackEx = (INewPolygonFeedbackEx)SketchToolAssist.Feedback;
					SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
					newPolygonFeedbackEx.ChangeLineType(SketchToolAssist.LineType);
					newPolygonFeedbackEx.Start(SketchToolAssist.m_pAnchorPoint);
					SketchToolAssist.m_pPointColn = new Polygon();
					object obj3 = Missing.Value;
					object value4 = Missing.Value;
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref obj3, ref value4);
				}
				str = "";
			}
			else if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.Point)
			{
				SketchToolAssist.TempLine = SketchToolAssist.m_pAnchorPoint;
				str = "";
			}
			else if (pFeatureLayer == null)
			{
				str = "";
			}
			else if (pFeatureLayer.FeatureClass != null)
			{
				string str1 = "";
				string unit = "";
				SketchToolAssist.IsFixDirection = false;
				SketchToolAssist.IsFixLength = false;
				if (SketchToolAssist.Feedback == null)
				{
					if (pFeatureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
					{
						try
						{
							Editor.EditWorkspace.StartEditOperation();
							IFeature feature = pFeatureLayer.FeatureClass.CreateFeature();
							ITextElement textElement = CreateFeatureTool.MakeTextElement("文本", 0, SketchToolAssist.m_pAnchorPoint);
							IAnnotationFeature2 annotationFeature2 = feature as IAnnotationFeature2;
							annotationFeature2.LinkedFeatureID = -1;
							annotationFeature2.AnnotationClassID = 0;
							annotationFeature2.Annotation = textElement as IElement;
							EditorEvent.NewRow(feature);
							feature.Store();
							Editor.EditWorkspace.StopEditOperation();
							EditorEvent.AfterNewRow(feature);
							pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
							pActiveView.FocusMap.ClearSelection();
							pActiveView.FocusMap.SelectFeature(pFeatureLayer, feature);
							pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
						}
						catch (Exception exception)
						{
							Logger.Current.Error("",exception, "");
						}
					}
					else if (pFeatureLayer.FeatureClass.FeatureType != esriFeatureType.esriFTDimension)
					{
						value = Missing.Value;
						switch (pFeatureLayer.FeatureClass.ShapeType)
						{
							case esriGeometryType.esriGeometryPoint:
							{
								CreateFeatureTool.CreateFeature(SketchToolAssist.m_pAnchorPoint, pActiveView, pFeatureLayer);
								break;
							}
							case esriGeometryType.esriGeometryMultipoint:
							{
								SketchToolAssist.m_bInUse = true;
								SketchToolAssist.Feedback = new NewMultiPointFeedback();
								INewMultiPointFeedback newMultiPointFeedback = (INewMultiPointFeedback)SketchToolAssist.Feedback;
								SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
								SketchToolAssist.m_pPointCollection = new Multipoint();
								newMultiPointFeedback.Start(SketchToolAssist.m_pPointCollection, SketchToolAssist.m_pAnchorPoint);
								break;
							}
							case esriGeometryType.esriGeometryPolyline:
							{
								SketchToolAssist.m_bInUse = true;
								SketchToolAssist.Feedback = new NewPolylineFeedback();
								newPolylineFeedback = (INewPolylineFeedback)SketchToolAssist.Feedback;
								newPolylineFeedback.ChangeLineType(SketchToolAssist.LineType);
								SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
								newPolylineFeedback.Start(SketchToolAssist.m_pAnchorPoint);
								SketchToolAssist.m_PointCount = 1;
								SketchToolAssist.StartPoint = SketchToolAssist.m_pAnchorPoint;
								SketchToolAssist.m_pPointColn = new Polyline();
								SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref value, ref value);
								unit = CommonHelper.GetUnit(pActiveView.FocusMap.MapUnits);
								break;
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								SketchToolAssist.m_bInUse = true;
								SketchToolAssist.Feedback = new NewPolygonFeedbackEx();
								feedback1 = (INewPolygonFeedbackEx)SketchToolAssist.Feedback;
								feedback1.ChangeLineType(SketchToolAssist.LineType);
								SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
								feedback1.Start(SketchToolAssist.m_pAnchorPoint);
								SketchToolAssist.m_PointCount = 0;
								SketchToolAssist.m_pPointColn = new Polygon();
								SketchToolAssist.StartPoint = SketchToolAssist.m_pAnchorPoint;
								unit = CommonHelper.GetUnit(pActiveView.FocusMap.MapUnits);
								num = CommonHelper.measureArea(SketchToolAssist.m_pAnchorPoint, 1, ref SketchToolAssist.m_pPointColn);
								length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
								if (num <= 0)
								{
									break;
								}
								str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num);
								break;
							}
						}
					}
					else
					{
						SketchToolAssist.Feedback = new NewDimensionFeedback();
						try
						{
							(SketchToolAssist.Feedback as INewDimensionFeedback).ReferenceScale = (pActiveView as IMap).ReferenceScale;
							(SketchToolAssist.Feedback as INewDimensionFeedback).ReferenceScaleUnits = (pActiveView as IMap).MapUnits;
						}
						catch
						{
						}
						SketchToolAssist.Feedback.Display = pActiveView.ScreenDisplay;
						(SketchToolAssist.Feedback as INewDimensionFeedback).Start(SketchToolAssist.m_pAnchorPoint);
						SketchToolAssist.m_PointCount = 1;
					}
				}
				else if (SketchToolAssist.Feedback is INewDimensionFeedback)
				{
					SketchToolAssist.m_PointCount = SketchToolAssist.m_PointCount + 1;
					(SketchToolAssist.Feedback as INewDimensionFeedback).AddPoint(SketchToolAssist.m_pAnchorPoint);
					if (SketchToolAssist.m_PointCount == 3)
					{
						IDimensionShape dimensionShape = (SketchToolAssist.Feedback as INewDimensionFeedback).Stop();
						CreateFeatureTool.CreateDimensionFeature(dimensionShape, (SketchToolAssist.Feedback as INewDimensionFeedback).DimensionType, pActiveView, pFeatureLayer);
					}
				}
				else if (SketchToolAssist.Feedback is INewMultiPointFeedback)
				{
					value = Missing.Value;
					SketchToolAssist.m_pPointCollection.AddPoint(SketchToolAssist.m_pAnchorPoint, ref value, ref value);
					(SketchToolAssist.Feedback as INewMultiPointFeedback).Start(SketchToolAssist.m_pPointCollection, SketchToolAssist.m_pAnchorPoint);
				}
				else if (SketchToolAssist.Feedback is INewLineFeedback)
				{
					newPolylineFeedback = (INewPolylineFeedback)SketchToolAssist.Feedback;
					value = Missing.Value;
					newPolylineFeedback.AddPoint(SketchToolAssist.m_pAnchorPoint);
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref value, ref value);
					SketchToolAssist.m_PointCount = SketchToolAssist.m_PointCount + 1;
					unit = CommonHelper.GetUnit(pActiveView.FocusMap.MapUnits);
				}
				else if (SketchToolAssist.Feedback is INewPolygonFeedback)
				{
					feedback1 = (INewPolygonFeedbackEx)SketchToolAssist.Feedback;
					feedback1.AddPoint(SketchToolAssist.m_pAnchorPoint);
					SketchToolAssist.m_PointCount = SketchToolAssist.m_PointCount + 1;
					unit = CommonHelper.GetUnit(pActiveView.FocusMap.MapUnits);
					num = CommonHelper.measureArea(SketchToolAssist.m_pAnchorPoint, 1, ref SketchToolAssist.m_pPointColn);
					length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
					if (num > 0)
					{
						str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num);
					}
					object value5 = Missing.Value;
					object obj5 = Missing.Value;
					SketchToolAssist.m_pPointColn.AddPoint(SketchToolAssist.m_pAnchorPoint, ref value5, ref obj5);
				}
				if (SketchToolAssist.LastPoint == null)
				{
					SketchToolAssist.LastPoint = new ESRI.ArcGIS.Geometry.Point();
				}
				SketchToolAssist.LastPoint.PutCoords(SketchToolAssist.m_pAnchorPoint.X, SketchToolAssist.m_pAnchorPoint.Y);
				str = str1;
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static string SketchMouseMove(IPoint ipoint_0)
		{
			string str;
			double x;
			double y;
			double num;
			ISnappingResult snappingResult;
			string unit;
			if (SketchToolAssist.Map != null)
			{
				IMap map = SketchToolAssist.Map;
				string str1 = "";
				bool flag = true;
				if (SketchToolAssist.LastPoint != null)
				{
					if (SketchToolAssist.IsFixDirection)
					{
						x = ipoint_0.X - SketchToolAssist.LastPoint.X;
						y = ipoint_0.Y - SketchToolAssist.LastPoint.Y;
						num = CommonHelper.azimuth(SketchToolAssist.LastPoint, ipoint_0);
						double num1 = Math.Sqrt(x * x + y * y);
						double num2 = num1 * Math.Cos(SketchToolAssist.FixDirection * 3.14159265358979 / 180);
						double num3 = num1 * Math.Sin(SketchToolAssist.FixDirection * 3.14159265358979 / 180);
						if (!(SketchToolAssist.FixDirection < 0 ? true : SketchToolAssist.FixDirection >= 90))
						{
							if ((num < 90 + SketchToolAssist.FixDirection ? false : num < 270 + SketchToolAssist.FixDirection))
							{
								num2 = -num2;
								num3 = -num3;
							}
						}
						else if (!(SketchToolAssist.FixDirection < 90 ? true : SketchToolAssist.FixDirection >= 270))
						{
							if ((num < SketchToolAssist.FixDirection - 90 ? true : num >= SketchToolAssist.FixDirection + 90))
							{
								num2 = -num2;
								num3 = -num3;
							}
						}
						else if ((num < SketchToolAssist.FixDirection - 270 ? false : num < SketchToolAssist.FixDirection - 90))
						{
							num2 = -num2;
							num3 = -num3;
						}
						flag = false;
						x = SketchToolAssist.LastPoint.X + num2;
						y = SketchToolAssist.LastPoint.Y + num3;
						ipoint_0.PutCoords(x, y);
					}
					else if (SketchToolAssist.IsFixLength)
					{
						num = CommonHelper.azimuth(SketchToolAssist.LastPoint, ipoint_0);
						x = SketchToolAssist.FixLength * Math.Cos(num * 3.14159265358979 / 180);
						y = SketchToolAssist.FixLength * Math.Sin(num * 3.14159265358979 / 180);
						x = SketchToolAssist.LastPoint.X + x;
						y = SketchToolAssist.LastPoint.Y + y;
						flag = false;
						ipoint_0.PutCoords(x, y);
					}
				}
				SketchToolAssist.m_pAnchorPoint = ipoint_0;
				if (flag)
				{
					if (!Editor.UseOldSnap)
					{
						snappingResult = SketchToolAssist.m_psnaper.Snap(SketchToolAssist.m_pAnchorPoint);
						if (snappingResult == null)
						{
							if (SketchToolAssist.m_pAP == null)
							{
								SketchToolAssist.m_pAP = new AnchorPoint()
								{
									Symbol = SketchToolAssist.m_pSym as ISymbol
								};
							}
							SketchToolAssist.m_pAP.MoveTo(SketchToolAssist.m_pAnchorPoint, (map as IActiveView).ScreenDisplay);
						}
						else
						{
							SketchToolAssist.m_pAnchorPoint = snappingResult.Location;
							if (SketchToolAssist.m_pAP == null)
							{
								SketchToolAssist.m_pAP = new AnchorPoint()
								{
									Symbol = SketchToolAssist.m_pSym as ISymbol
								};
							}
							SketchToolAssist.m_pAP.MoveTo(SketchToolAssist.m_pAnchorPoint, (map as IActiveView).ScreenDisplay);
						}
					}
					else
					{
						IAppContext application = ApplicationRef.AppContext;
						snappingResult = SketchToolAssist.OldSnap(SketchToolAssist.m_pAnchorPoint, map as IActiveView, application.Config.EngineSnapEnvironment);
						if (snappingResult == null)
						{
							if (SketchToolAssist.m_pAP == null)
							{
								SketchToolAssist.m_pAP = new AnchorPoint()
								{
									Symbol = SketchToolAssist.m_pSym as ISymbol
								};
							}
							SketchToolAssist.m_pAP.MoveTo(SketchToolAssist.m_pAnchorPoint, (map as IActiveView).ScreenDisplay);
						}
						else
						{
							SketchToolAssist.m_pAnchorPoint = snappingResult.Location;
							if (SketchToolAssist.m_pAP == null)
							{
								SketchToolAssist.m_pAP = new AnchorPoint()
								{
									Symbol = SketchToolAssist.m_pSym as ISymbol
								};
							}
							SketchToolAssist.m_pAP.MoveTo(SketchToolAssist.m_pAnchorPoint, (map as IActiveView).ScreenDisplay);
						}
					}
				}
				if (SketchToolAssist.Feedback != null)
				{
					SketchToolAssist.Feedback.MoveTo(SketchToolAssist.m_pAnchorPoint);
				}
				if (SketchToolAssist.IsDrawTempLine == DrawTempGeometry.None)
				{
					if (SketchToolAssist.m_bShowVlaue)
					{
						if (SketchToolAssist.Feedback is INewLineFeedback)
						{
							unit = CommonHelper.GetUnit(map.MapUnits);
							double num4 = CommonHelper.measureLength(ipoint_0, 2, ref SketchToolAssist.m_pLastPoint1, ref SketchToolAssist.m_pEndPoint1, ref SketchToolAssist.m_totalLength);
							string[] strArrays = new string[] { "距离 = ", num4.ToString("0.###"), unit, ", 总长度 = ", SketchToolAssist.m_totalLength.ToString("0.###"), unit };
							str1 = string.Concat(strArrays);
						}
						else if (SketchToolAssist.Feedback is INewPolygonFeedback)
						{
							unit = CommonHelper.GetUnit(map.MapUnits);
							double num5 = CommonHelper.measureArea(ipoint_0, 2, ref SketchToolAssist.m_pPointColn);
							double length = (SketchToolAssist.m_pPointColn as IPolygon).Length;
							if (num5 > 0)
							{
								str1 = string.Format("周长 = {0:0.###} {1} ，总面积 = {2:0.###} 平方{1}", length, unit, num5);
							}
						}
					}
					str = str1;
				}
				else
				{
					str = "";
				}
			}
			else
			{
				str = "";
			}
			return str;
		}
	}
}