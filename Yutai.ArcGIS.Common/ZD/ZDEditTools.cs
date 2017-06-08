using System.Collections.Generic;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDEditTools
	{
		public static System.Collections.Generic.List<int> Oids;

		private static ITable _ZDChangeHisInfoTable;

		public static event OnAfterCreateZDHandler OnAfterCreateZD;

		public static event OnStartZDEditHandler OnStartZDEdit;

		public static event OnEndZDEditHandler OnEndZDEdit;

		public static System.DateTime StartEditDateTime
		{
			get;
			set;
		}

		public static IFeatureLayer ZDFeatureLayer
		{
			get;
			set;
		}

		private static IFeatureClass ZDFeatureClass
		{
			get;
			set;
		}

		public static IFeatureClass ZDHisFeatureClass
		{
			get;
			set;
		}

		public static ITable ZDChangeHisInfoTable
		{
			get
			{
				if (ZDEditTools._ZDChangeHisInfoTable == null)
				{
					ZDEditTools._ZDChangeHisInfoTable = AppConfigInfo.OpenTable("ZDHistory");
				}
				return ZDEditTools._ZDChangeHisInfoTable;
			}
		}

		static ZDEditTools()
		{
			// 注意: 此类型已标记为 'beforefieldinit'.
			ZDEditTools.old_acctor_mc();
		}

		public static void FireAfterCreateZD(IRow irow_0)
		{
			if (ZDEditTools.OnAfterCreateZD != null)
			{
				ZDEditTools.OnAfterCreateZD(irow_0);
			}
		}

		public static void FireStartZDEdit()
		{
			if (ZDEditTools.OnStartZDEdit != null)
			{
				ZDEditTools.OnStartZDEdit();
			}
		}

		public static void FireEndZDEdit()
		{
			if (ZDEditTools.OnEndZDEdit != null)
			{
				ZDEditTools.OnEndZDEdit();
			}
		}

		private static void CheckGroupLayer(System.Collections.Generic.List<IFeatureLayer> list_0, ICompositeLayer icompositeLayer_0)
		{
			for (int i = 0; i < icompositeLayer_0.Count; i++)
			{
				ILayer layer = icompositeLayer_0.get_Layer(i);
				if (layer is IGroupLayer)
				{
					ZDEditTools.CheckGroupLayer(list_0, layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer item = layer as IFeatureLayer;
					if (ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
					{
						list_0.Add(item);
					}
				}
			}
		}

		private static void CheckGroupLayerEdit(System.Collections.Generic.List<IFeatureLayer> list_0, ICompositeLayer icompositeLayer_0)
		{
			for (int i = 0; i < icompositeLayer_0.Count; i++)
			{
				ILayer layer = icompositeLayer_0.get_Layer(i);
				if (layer is IGroupLayer)
				{
					ZDEditTools.CheckGroupLayerEdit(list_0, layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = layer as IFeatureLayer;
					if (ZDEditTools.CheckLayerIsCanEditZD(featureLayer))
					{
						list_0.Add(featureLayer);
					}
				}
			}
		}

		private static bool LayerCanEdit(IFeatureLayer ifeatureLayer_0)
		{
			bool result;
			if (Editor.Editor.EditWorkspace == null)
			{
				result = false;
			}
			else
			{
				IDataset dataset = ifeatureLayer_0.FeatureClass as IDataset;
				if (dataset == null)
				{
					result = false;
				}
				else if (dataset is ICoverageFeatureClass)
				{
					result = false;
				}
				else
				{
					IWorkspace workspace = dataset.Workspace;
					IPropertySet connectionProperties = workspace.ConnectionProperties;
					IWorkspace workspace2 = Editor.Editor.EditWorkspace as IWorkspace;
					if (connectionProperties.IsEqual(workspace2.ConnectionProperties))
					{
						if (workspace2.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
						{
							if (workspace2 is IVersionedWorkspace)
							{
								IVersionedObject versionedObject = dataset as IVersionedObject;
								if (versionedObject.IsRegisteredAsVersioned)
								{
									if (AppConfigInfo.UserID.Length == 0 || AppConfigInfo.UserID.ToLower() == "admin")
									{
										result = true;
										return result;
									}
									if (Editor.Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, 2, dataset.Name))
									{
										result = true;
										return result;
									}
								}
							}
							else
							{
								if (AppConfigInfo.UserID.Length == 0 || AppConfigInfo.UserID.ToLower() == "admin")
								{
									result = true;
									return result;
								}
								if (Editor.Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
								{
									result = true;
									return result;
								}
							}
						}
						else
						{
							if (AppConfigInfo.UserID.Length == 0 || AppConfigInfo.UserID.ToLower() == "admin")
							{
								result = true;
								return result;
							}
							if (Editor.Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
							{
								result = true;
								return result;
							}
						}
					}
					result = false;
				}
			}
			return result;
		}

		private static bool CheckLayerIsCanEditZD(IFeatureLayer ifeatureLayer_0)
		{
			bool result;
			if (result = ZDEditTools.LayerCanEdit(ifeatureLayer_0))
			{
				result = ZDRegister.IsZDFeatureClass(ifeatureLayer_0.FeatureClass);
			}
			return result;
		}

		public static bool HasZDLayer(IMap imap_0)
		{
			bool result = false;
			IEnumLayer enumLayer = imap_0.get_Layers(null, true);
			enumLayer.Reset();
			ILayer layer = enumLayer.Next();
			while (layer != null && (!(layer is IFeatureLayer) || !(result = ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))))
			{
				layer = enumLayer.Next();
			}
			return result;
		}

		public static IFeatureLayer GetFirstZDLayer(IMap imap_0)
		{
			IEnumLayer enumLayer = imap_0.get_Layers(null, true);
			enumLayer.Reset();
			IFeatureLayer result;
			for (ILayer layer = enumLayer.Next(); layer != null; layer = enumLayer.Next())
			{
				if (layer is IFeatureLayer && ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
				{
					result = (layer as IFeatureLayer);
					return result;
				}
			}
			result = null;
			return result;
		}

		public static System.Collections.Generic.List<IFeatureLayer> GetZDLayers(IMap imap_0)
		{
			System.Collections.Generic.List<IFeatureLayer> list = new System.Collections.Generic.List<IFeatureLayer>();
			for (int i = 0; i < imap_0.LayerCount; i++)
			{
				ILayer layer = imap_0.get_Layer(i);
				if (layer is IGroupLayer)
				{
					ZDEditTools.CheckGroupLayer(list, layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer item = layer as IFeatureLayer;
					if (ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		public static void StartZDEdit()
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			IMap editMap = Editor.Editor.EditMap;
			System.Collections.Generic.List<IFeatureLayer> list = new System.Collections.Generic.List<IFeatureLayer>();
			for (int i = 0; i < editMap.LayerCount; i++)
			{
				ILayer layer = editMap.get_Layer(i);
				if (layer is IGroupLayer)
				{
					ZDEditTools.CheckGroupLayerEdit(list, layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer featureLayer = layer as IFeatureLayer;
					if (ZDEditTools.CheckLayerIsCanEditZD(featureLayer))
					{
						list.Add(featureLayer);
					}
				}
			}
			if (list.Count == 0)
			{
				System.Windows.Forms.MessageBox.Show("当前没有可编辑的宗地图层!");
			}
			else if (list.Count == 1)
			{
				ZDEditTools.ZDFeatureLayer = list[0];
				ZDEditTools.ZDFeatureClass = ZDEditTools.ZDFeatureLayer.FeatureClass;
				ZDEditTools.ZDHisFeatureClass = ZDRegister.GetHistoryFeatureClass(ZDEditTools.ZDFeatureClass);
				ZDEditTools.FireStartZDEdit();
			}
			else
			{
				frmSelectEditZD frmSelectEditZD = new frmSelectEditZD();
				frmSelectEditZD.FeatureLayers = list;
				if (frmSelectEditZD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					ZDEditTools.ZDFeatureLayer = frmSelectEditZD.SelectFeatureLayer;
					ZDEditTools.ZDFeatureClass = ZDEditTools.ZDFeatureLayer.FeatureClass;
					ZDEditTools.ZDHisFeatureClass = ZDRegister.GetHistoryFeatureClass(ZDEditTools.ZDFeatureClass);
					ZDEditTools.FireStartZDEdit();
				}
			}
		}

		public static bool UpdateZDAttribute(IFeature ifeature_0, string string_0, object object_0, out IFeature ifeature_1)
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
            Editor.Editor.StartEditOperation();
			IWorkspace workspace = AppConfigInfo.GetWorkspace();
			if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.Editor.EditWorkspace as IWorkspace))
			{
				(workspace as IWorkspaceEdit).StartEditOperation();
			}
			ifeature_1 = null;
			bool flag = ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1;
			IFeature feature = null;
			if (flag)
			{
				feature = ZDEditTools.WriteHistory(ifeature_0);
			}
			bool result = true;
			try
			{
				int index = ifeature_0.Fields.FindField(string_0);
				ifeature_0.set_Value(index, object_0);
				ifeature_0.Store();
				if (flag)
				{
					IFeature feature2 = ZDEditTools.ZDFeatureClass.CreateFeature();
					RowOperator.CopyFeatureToFeature(ifeature_0, feature2);
					ZDEditTools.Oids.Add(feature2.OID);
					ifeature_1 = feature2;
					ZDEditTools.WriteHistoryLine(ifeature_0, feature2, feature, 5, ZDEditTools.StartEditDateTime);
					ifeature_0.Delete();
				}
			}
			catch (System.Exception exception_)
			{
				if (feature != null)
				{
					feature.Delete();
				}
				result = false;
				System.Windows.Forms.MessageBox.Show("输入数据格式错误");
				//Logger.Current.Error("", exception_, "");
			}
			if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.Editor.EditWorkspace as IWorkspace))
			{
				(workspace as IWorkspaceEdit).StopEditOperation();
			}
            Editor.Editor.StopEditOperation();
			return result;
		}

		public static void StopZDEdit()
		{
			ZDEditTools.Oids.Clear();
            Editor.Editor.SaveEditing();
			ZDEditTools.ZDFeatureLayer = null;
			ZDEditTools.ZDFeatureClass = null;
			ZDEditTools.ZDHisFeatureClass = null;
			ZDEditTools.FireEndZDEdit();
		}

		public static IFeature Union(IFeatureSelection ifeatureSelection_0)
		{
			ICursor cursor = null;
			ifeatureSelection_0.SelectionSet.Search(null, false, out cursor);
			IFeature result = ZDEditTools.Union(cursor);
			ComReleaser.ReleaseCOMObject(cursor);
			cursor = null;
			return result;
		}

		public static IFeature Union(ICursor icursor_0)
		{
			System.Collections.Generic.List<IFeature> list = new System.Collections.Generic.List<IFeature>();
			for (IRow row = icursor_0.NextRow(); row != null; row = icursor_0.NextRow())
			{
				list.Add(row as IFeature);
			}
			return ZDEditTools.Union(list);
		}

		public static IFeature Union(System.Collections.Generic.List<IFeature> list_0)
		{
			ITopologicalOperator4 topologicalOperator = null;
			IGeometry geometry = null;
			System.Collections.Generic.List<IFeature> list = new System.Collections.Generic.List<IFeature>();
			foreach (IFeature current in list_0)
			{
				list.Add(ZDEditTools.WriteHistory(current));
				if (topologicalOperator == null)
				{
					topologicalOperator = (current.ShapeCopy as ITopologicalOperator4);
				}
				else
				{
					geometry = topologicalOperator.Union(current.ShapeCopy);
					topologicalOperator = (geometry as ITopologicalOperator4);
				}
			}
			IFeature feature = ZDEditTools.ZDFeatureClass.CreateFeature();
			feature.Shape = geometry;
			RowOperator.CopyFeatureAttributeToFeature(list_0[0], feature);
			feature.Store();
			(ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[]
			{
				'.'
			});
			new ZDHistoryTable();
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			int num = 0;
			foreach (IFeature current in list_0)
			{
				ZDEditTools.WriteHistoryLine(current, feature, list[num++], 1, ZDEditTools.StartEditDateTime);
				current.Delete();
			}
			ZDEditTools.Oids.Add(feature.OID);
			return feature;
		}

		public static IFeature CreateZD(IGeometry igeometry_0)
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			IFeature feature = ZDEditTools.ZDFeatureClass.CreateFeature();
			feature.Shape = igeometry_0;
			feature.Store();
			ZDEditTools.WriteHistoryLine(feature, null, 0, ZDEditTools.StartEditDateTime);
			ZDEditTools.Oids.Add(feature.OID);
			return feature;
		}

		public static void UpdateFeatureGeometry(IFeature ifeature_0, IGeometry igeometry_0)
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			IFeature feature = null;
			bool flag;
			if (flag = (ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1))
			{
				feature = ZDEditTools.WriteHistory(ifeature_0);
			}
			try
			{
				ifeature_0.Shape = igeometry_0;
				ifeature_0.Store();
				if (flag)
				{
					IFeature feature2 = ZDEditTools.ZDFeatureClass.CreateFeature();
					RowOperator.CopyFeatureToFeature(ifeature_0, feature2);
					ZDEditTools.Oids.Add(feature2.OID);
					ZDEditTools.WriteHistoryLine(ifeature_0, feature2, feature, 3, ZDEditTools.StartEditDateTime);
					ifeature_0.Delete();
				}
			}
			catch
			{
				if (feature != null)
				{
					feature.Delete();
				}
			}
		}

		public static IFeature WriteHistory(IFeature ifeature_0)
		{
			IFeature feature = ZDEditTools.ZDHisFeatureClass.CreateFeature();
			RowOperator.SetFieldValue(feature, "OriginOID_", ifeature_0.OID);
			feature.Shape = ifeature_0.ShapeCopy;
			RowOperator.CopyRowToRow(ifeature_0, feature);
			return feature;
		}

		private static void DeleteZD(IFeature ifeature_0)
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			IFeature ifeature_ = ZDEditTools.WriteHistory(ifeature_0);
			ZDEditTools.WriteDelHistoryLine(ifeature_0, ifeature_, 4, ZDEditTools.StartEditDateTime);
			ifeature_0.Delete();
		}

		private static void WriteHistoryLine(IFeature ifeature_0, IFeature ifeature_1, int int_0, System.DateTime dateTime_0)
		{
			string[] array = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[]
			{
				'.'
			});
			ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
			IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
			RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, -1);
			RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, ifeature_0.OID);
			if (ifeature_1 != null)
			{
				RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_1.OID);
			}
			else
			{
				RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, -1);
			}
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, array[array.Length - 1]);
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName, ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
			row.Store();
		}

		private static void WriteDelHistoryLine(IFeature ifeature_0, IFeature ifeature_1, int int_0, System.DateTime dateTime_0)
		{
			string[] array = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[]
			{
				'.'
			});
			ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
			IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
			RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, ifeature_0.OID);
			RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, -1);
			if (ifeature_1 != null)
			{
				RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_1.OID);
			}
			else
			{
				RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, -1);
			}
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, array[array.Length - 1]);
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName, ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
			row.Store();
		}

		private static void WriteHistoryLine(IFeature ifeature_0, IFeature ifeature_1, IFeature ifeature_2, int int_0, System.DateTime dateTime_0)
		{
			string[] array = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[]
			{
				'.'
			});
			ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
			IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
			RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
			RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, ifeature_0.OID);
			RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, ifeature_1.OID);
			if (ifeature_2 != null)
			{
				RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_2.OID);
			}
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, array[array.Length - 1]);
			RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName, ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
			row.Store();
		}

		private static void WriteHistoryLine(System.Collections.Generic.List<IFeature> list_0, IFeature ifeature_0, System.Collections.Generic.List<IFeature> list_1, int int_0, System.DateTime dateTime_0)
		{
			int num = 0;
			foreach (IFeature current in list_0)
			{
				ZDEditTools.WriteHistoryLine(current, ifeature_0, list_1[num++], int_0, dateTime_0);
			}
		}

		private static void WriteHistoryLine(IFeature ifeature_0, System.Collections.Generic.List<IFeature> list_0, IFeature ifeature_1, int int_0, System.DateTime dateTime_0)
		{
			foreach (IFeature current in list_0)
			{
				ZDEditTools.WriteHistoryLine(ifeature_0, current, ifeature_1, int_0, dateTime_0);
			}
		}

		public static System.Collections.Generic.List<IFeature> Split(IFeatureClass ifeatureClass_0, IPolyline ipolyline_0)
		{
			IFeatureCursor featureCursor = ifeatureClass_0.Search(new SpatialFilter
			{
				Geometry = ipolyline_0,
				SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
			}, false);
			IFeature feature = featureCursor.NextFeature();
			System.Collections.Generic.List<IFeature> list = new System.Collections.Generic.List<IFeature>();
			while (feature != null)
			{
				System.Collections.Generic.List<IFeature> list2 = ZDEditTools.Split(feature, ipolyline_0);
				if (list2.Count > 0)
				{
					list.AddRange(list2);
				}
				feature = featureCursor.NextFeature();
			}
			ComReleaser.ReleaseCOMObject(featureCursor);
			return list;
		}

		public static System.Collections.Generic.List<IFeature> Split(IFeatureSelection ifeatureSelection_0, IPolyline ipolyline_0)
		{
			ICursor cursor;
			ifeatureSelection_0.SelectionSet.Search(null, false, out cursor);
			IFeature feature = cursor.NextRow() as IFeature;
			ComReleaser.ReleaseCOMObject(cursor);
			System.Collections.Generic.List<IFeature> result = new System.Collections.Generic.List<IFeature>();
			if (feature != null)
			{
				result = ZDEditTools.Split(feature, ipolyline_0);
			}
			return result;
		}

		public static System.Collections.Generic.List<IFeature> Split(IFeature ifeature_0, IPolyline ipolyline_0)
		{
			System.Collections.Generic.List<IFeature> list = new System.Collections.Generic.List<IFeature>();
			ITopologicalOperator4 topologicalOperator = ifeature_0.ShapeCopy as ITopologicalOperator4;
			IGeometry geometry = topologicalOperator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension);
			System.Collections.Generic.List<IFeature> list2 = new System.Collections.Generic.List<IFeature>();
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			if (geometry.IsEmpty)
			{
				IGeometry geometry2;
				IGeometry geometry3;
				topologicalOperator.Cut(ipolyline_0, out geometry2, out geometry3);
				if (geometry2 != null && geometry3 != null)
				{
					IFeature ifeature_ = ZDEditTools.WriteHistory(ifeature_0);
					IFeature feature = ZDEditTools.ZDFeatureClass.CreateFeature();
					list.Add(feature);
					feature.Shape = geometry2;
					RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature);
					feature.Store();
					ZDEditTools.Oids.Add(feature.OID);
					ZDEditTools.WriteHistoryLine(ifeature_0, feature, ifeature_, 2, ZDEditTools.StartEditDateTime);
					feature = ZDEditTools.ZDFeatureClass.CreateFeature();
					list.Add(feature);
					feature.Shape = geometry3;
					RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature);
					feature.Store();
					ZDEditTools.Oids.Add(feature.OID);
					ZDEditTools.WriteHistoryLine(ifeature_0, feature, ifeature_, 2, ZDEditTools.StartEditDateTime);
					ifeature_0.Delete();
				}
			}
			else if (geometry is IMultipoint && (geometry as IPointCollection).PointCount > 1)
			{
				ISpatialFilter spatialFilter = new SpatialFilter();
				spatialFilter.Geometry = ipolyline_0;
				spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
				IFeatureCursor featureCursor = (ifeature_0.Class as IFeatureClass).Search(spatialFilter, false);
				for (IFeature feature2 = featureCursor.NextFeature(); feature2 != null; feature2 = featureCursor.NextFeature())
				{
					if (feature2.OID != ifeature_0.OID)
					{
						list2.Add(feature2);
					}
				}
				ComReleaser.ReleaseCOMObject(featureCursor);
				int pointCount = (geometry as IPointCollection).PointCount;
				IPoint ipoint_ = (geometry as IPointCollection).get_Point(0);
				IPoint ipoint_2 = (geometry as IPointCollection).get_Point(pointCount - 1);
				foreach (IFeature current in list2)
				{
					ZDEditTools.HitTest(current, ipoint_);
					ZDEditTools.HitTest(current, ipoint_2);
				}
				IGeometryCollection geometryCollection = topologicalOperator.Cut2(ipolyline_0);
				System.DateTime startEditDateTime = ZDEditTools.StartEditDateTime;
				IFeature ifeature_ = ZDEditTools.WriteHistory(ifeature_0);
				for (int i = 0; i < geometryCollection.GeometryCount; i++)
				{
					IGeometry shape = geometryCollection.get_Geometry(i);
					IFeature feature = ZDEditTools.ZDFeatureClass.CreateFeature();
					list.Add(feature);
					feature.Shape = shape;
					RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature);
					feature.Store();
					ZDEditTools.Oids.Add(feature.OID);
					ZDEditTools.WriteHistoryLine(ifeature_0, feature, ifeature_, 2, startEditDateTime);
				}
				ifeature_0.Delete();
			}
			return list;
		}

		private static void HitTest(IFeature ifeature_0, IPoint ipoint_0)
		{
			IGeometry shapeCopy = ifeature_0.ShapeCopy;
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			double num = 0.0;
			int index = 0;
			int num2 = 0;
			bool flag = false;
			if ((!(shapeCopy as IHitTest).HitTest(ipoint_0, 0.0001, esriGeometryHitPartType.esriGeometryPartVertex, point, ref num, ref index, ref num2, ref flag) || num >= 0.0001) && (shapeCopy as IHitTest).HitTest(ipoint_0, 0.0001, esriGeometryHitPartType.esriGeometryPartBoundary, point, ref num, ref index, ref num2, ref flag) && num < 0.0001)
			{
				ISegmentCollection segmentCollection = (shapeCopy as IGeometryCollection).get_Geometry(index) as ISegmentCollection;
				ISegment segment = segmentCollection.get_Segment(num2);
				if (segment is ILine)
				{
					ILine line = new Line();
					line.PutCoords((segment as ILine).FromPoint, point);
					ILine line2 = new Line();
					line2.PutCoords(point, (segment as ILine).ToPoint);
					ISegmentCollection segmentCollection2 = new Polyline() as ISegmentCollection;
					object value = System.Reflection.Missing.Value;
					object value2 = System.Reflection.Missing.Value;
					segmentCollection2.AddSegment(line as ISegment, ref value, ref value2);
					segmentCollection.ReplaceSegmentCollection(num2, 1, segmentCollection2);
					segmentCollection2 = new Polyline() as ISegmentCollection;
					object value3 = System.Reflection.Missing.Value;
					object value4 = System.Reflection.Missing.Value;
					segmentCollection2.AddSegment(line2 as ISegment, ref value3, ref value4);
					segmentCollection.InsertSegmentCollection(num2 + 1, segmentCollection2);
					segmentCollection.SegmentsChanged();
					(shapeCopy as IGeometryCollection).GeometriesChanged();
					ifeature_0.Shape = shapeCopy;
					ifeature_0.Store();
				}
			}
		}

		public static System.Collections.Generic.List<HitFeatureInfo> GetHitInfo(IFeature ifeature_0, IPoint ipoint_0)
		{
			System.Collections.Generic.List<HitFeatureInfo> list = new System.Collections.Generic.List<HitFeatureInfo>();
			System.Collections.Generic.List<IFeature> list2 = new System.Collections.Generic.List<IFeature>();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Expand(0.1, 0.1, false);
			ISpatialFilter spatialFilter = new SpatialFilter();
			spatialFilter.Geometry = envelope;
			spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
			IFeatureCursor featureCursor = (ifeature_0.Class as IFeatureClass).Search(spatialFilter, false);
			for (IFeature feature = featureCursor.NextFeature(); feature != null; feature = featureCursor.NextFeature())
			{
				if (feature.OID != ifeature_0.OID)
				{
					list2.Add(feature);
				}
			}
			ComReleaser.ReleaseCOMObject(featureCursor);
			foreach (IFeature current in list2)
			{
				IGeometry shapeCopy = current.ShapeCopy;
				IPoint hitPoint = new Point();
				double num = 0.0;
				int partIndex = 0;
				int num2 = 0;
				bool flag = false;
				if ((shapeCopy as IHitTest).HitTest(ipoint_0, 1E-05, esriGeometryHitPartType.esriGeometryPartVertex, hitPoint, ref num, ref partIndex, ref num2, ref flag))
				{
					list.Add(new HitFeatureInfo
					{
						Feature = current,
						PartIndex = partIndex,
						VertIndex = num2
					});
				}
				else if ((shapeCopy as IHitTest).HitTest(ipoint_0, 1E-05, esriGeometryHitPartType.esriGeometryPartBoundary, hitPoint, ref num, ref partIndex, ref num2, ref flag) && num < 0.0001)
				{
					list.Add(new HitFeatureInfo
					{
						Feature = current,
						PartIndex = partIndex,
						SegmentIndex = num2
					});
				}
			}
			return list;
		}

		private static void GetHitZD(IFeatureClass ifeatureClass_0, IPolyline ipolyline_0)
		{
			IFeatureCursor featureCursor = ifeatureClass_0.Search(new SpatialFilter
			{
				Geometry = ipolyline_0,
				SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
			}, false);
			for (IFeature feature = featureCursor.NextFeature(); feature != null; feature = featureCursor.NextFeature())
			{
			}
		}

		public static void Delete(IFeature ifeature_0)
		{
			ZDEditTools.DeleteZD(ifeature_0);
		}

		public static void DeletedSelectedZD(IFeatureSelection ifeatureSelection_0)
		{
			IEnumIDs iDs = ifeatureSelection_0.SelectionSet.IDs;
			iDs.Reset();
			int i = iDs.Next();
			System.Collections.Generic.List<IFeature> list = new System.Collections.Generic.List<IFeature>();
			IFeatureClass featureClass = (ifeatureSelection_0 as IFeatureLayer).FeatureClass;
			while (i > 0)
			{
				IFeature feature = featureClass.GetFeature(i);
				list.Add(feature);
				i = iDs.Next();
			}
			foreach (IFeature current in list)
			{
				ZDEditTools.DeleteZD(current);
			}
		}

		public static void DeletedSelectedZD(ICursor icursor_0)
		{
			new System.Collections.Generic.List<IFeature>();
			for (IRow row = icursor_0.NextRow(); row != null; row = icursor_0.NextRow())
			{
				ZDEditTools.DeleteZD(row as IFeature);
			}
		}

		public static void DeletedSelectedZD(IMap imap_0, IFeatureLayer ifeatureLayer_0)
		{
			Editor.Editor.StartEditOperation();
			IWorkspace workspace = AppConfigInfo.GetWorkspace();
			if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.Editor.EditWorkspace as IWorkspace))
			{
				(workspace as IWorkspaceEdit).StartEditOperation();
			}
			ZDEditTools.DeletedSelectedZD(ifeatureLayer_0 as IFeatureSelection);
			if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.Editor.EditWorkspace as IWorkspace))
			{
				(workspace as IWorkspaceEdit).StopEditOperation();
			}
            Editor.Editor.StopEditOperation();
			imap_0.ClearSelection();
			(imap_0 as IActiveView).Refresh();
		}

		public static void ChangeAttribute(IFeature ifeature_0, SortedList<string, object> sortedList_0)
		{
			ZDEditTools.StartEditDateTime = System.DateTime.Now;
			if (ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1)
			{
				ZDEditTools.Oids.Add(ifeature_0.OID);
				IFeature ifeature_ = ZDEditTools.WriteHistory(ifeature_0);
				ZDEditTools.WriteHistoryLine(ifeature_0, ifeature_, 5, ZDEditTools.StartEditDateTime);
			}
			foreach (System.Collections.Generic.KeyValuePair<string, object> current in sortedList_0)
			{
				RowOperator.SetFieldValue(ifeature_0, current.Key, current.Value);
			}
			ifeature_0.Store();
		}

		private static void old_acctor_mc()
		{
			ZDEditTools.Oids = new System.Collections.Generic.List<int>();
			ZDEditTools._ZDChangeHisInfoTable = null;
		}
	}
}
