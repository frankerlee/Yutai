using System;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Carto
{
	public class IdentifyHelper
	{
		private IdentifyTypeEnum identifyTypeEnum_0 = IdentifyTypeEnum.enumITAllLayer;

		private ILayer ilayer_0 = null;

		private static IList<string> m_pIdentifyLayers;

		public ILayer CurrentLayer
		{
			get
			{
				return this.ilayer_0;
			}
			set
			{
				this.ilayer_0 = value;
			}
		}

		public static IList<string> IdentifyLayers
		{
			get
			{
				return IdentifyHelper.m_pIdentifyLayers;
			}
		}

		public IdentifyTypeEnum IdentifyType
		{
			get
			{
				return this.identifyTypeEnum_0;
			}
			set
			{
				this.identifyTypeEnum_0 = value;
			}
		}

		static IdentifyHelper()
		{
			IdentifyHelper.old_acctor_mc();
		}

		public IdentifyHelper()
		{
		}

		public static bool EditIdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, out IArray iarray_0)
		{
			bool flag;
			iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (layer is IFeatureLayer)
					{
						IFeatureLayer featureLayer = layer as IFeatureLayer;
						if (!(featureLayer.FeatureClass == null ? false : featureLayer.Visible))
						{
							layer = layers.Next();
						}
						else if (((featureLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
						{
							if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
							{
								if (!(layer as IFeatureLayer).Selectable)
								{
									layer = layers.Next();
									continue;
								}
							}
							else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
							{
								flag1 = true;
							}
							try
							{
								IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, iarray_0);
							}
							catch (Exception exception)
							{
								Logger.Current.Error("", exception, "");
							}
							if (flag1)
							{
								break;
							}
							layer = layers.Next();
						}
						else
						{
							layer = layers.Next();
						}
					}
					else
					{
						layer = layers.Next();
					}
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, iarray_0);
			}
			return flag;
		}

		public static bool EditIdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, out IList<object> ilist_0)
		{
			bool flag;
			ilist_0 = new List<object>();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (layer is IFeatureLayer)
					{
						IFeatureLayer featureLayer = layer as IFeatureLayer;
						if (!(featureLayer.FeatureClass == null ? false : featureLayer.Visible))
						{
							layer = layers.Next();
						}
						else if (((featureLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
						{
							if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
							{
								if (!(layer as IFeatureLayer).Selectable)
								{
									layer = layers.Next();
									continue;
								}
							}
							else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
							{
								flag1 = true;
							}
							try
							{
								IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, ilist_0);
							}
							catch (Exception exception)
							{
								Logger.Current.Error("", exception, "");
							}
							if (flag1)
							{
								break;
							}
							layer = layers.Next();
						}
						else
						{
							layer = layers.Next();
						}
					}
					else
					{
						layer = layers.Next();
					}
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, ilist_0);
			}
			return flag;
		}

		private static bool FeatureLayerIdentify(IBasicMap ibasicMap_0, IFeatureLayer ifeatureLayer_0, IGeometry igeometry_0, IArray iarray_0)
		{
			bool flag;
			ISpatialFilter spatialFilterClass = new SpatialFilter()
			{
				Geometry = igeometry_0,
				SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
			};
			try
			{
				IFeatureCursor featureCursor = ifeatureLayer_0.FeatureClass.Search(spatialFilterClass, false);
				for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
				{
					AEFeatureIdentifyObject aEFeatureIdentifyObject = new AEFeatureIdentifyObject()
					{
						BasicMap = ibasicMap_0,
						Feature = i
					};
					iarray_0.Add(aEFeatureIdentifyObject);
				}
				ComReleaser.ReleaseCOMObject(featureCursor);
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
				Logger.Current.Error("", exception, "");
			}
			flag = false;
			return flag;
		}

		private static bool FeatureLayerIdentify(IBasicMap ibasicMap_0, IFeatureLayer ifeatureLayer_0, IGeometry igeometry_0, IList<object> ilist_0)
		{
			bool flag;
			ISpatialFilter spatialFilterClass = new SpatialFilter()
			{
				Geometry = igeometry_0,
				SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
			};
			ISpatialFilter spatialFilter = spatialFilterClass;
			try
			{
				IFeatureCursor featureCursor = ifeatureLayer_0.FeatureClass.Search(spatialFilter, false);
				for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
				{
					AEFeatureIdentifyObject aEFeatureIdentifyObject = new AEFeatureIdentifyObject()
					{
						BasicMap = ibasicMap_0,
						Feature = i
					};
					ilist_0.Add(aEFeatureIdentifyObject);
				}
				flag = true;
				return flag;
			}
			catch (Exception exception)
			{
				Logger.Current.Error("", exception, "");
			}
			flag = false;
			return flag;
		}

		public static bool IdentifyFeatureLayers(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, List<IFeatureLayer> list_0, out IList<object> ilist_0)
		{
			ilist_0 = new List<object>();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			foreach (IFeatureLayer list0 in list_0)
			{
				IdentifyHelper.FeatureLayerIdentify(ibasicMap_0, list0, envelope, ilist_0);
			}
			return true;
		}

		public static bool IdentifyLayer(IBasicMap ibasicMap_0, ILayer ilayer_1, IGeometry igeometry_0, IArray iarray_0)
		{
			if (ilayer_1 is IFeatureLayer)
			{
				IdentifyHelper.FeatureLayerIdentify(ibasicMap_0, ilayer_1 as IFeatureLayer, igeometry_0, iarray_0);
			}
			else if ((ilayer_1 is IRasterLayer || ilayer_1 is ITinLayer || ilayer_1 is IMapServerLayer ? true : ilayer_1 is ICadLayer))
			{
				IArray array = (ilayer_1 as IIdentify).Identify(igeometry_0);
				if (array != null)
				{
					for (int i = 0; i < array.Count; i++)
					{
						iarray_0.Add(array.Element[i]);
					}
				}
			}
			return true;
		}

		public static bool IdentifyLayer(IBasicMap ibasicMap_0, ILayer ilayer_1, IGeometry igeometry_0, IList<object> ilist_0)
		{
			if (ilayer_1 is IFeatureLayer)
			{
				IdentifyHelper.FeatureLayerIdentify(ibasicMap_0, ilayer_1 as IFeatureLayer, igeometry_0, ilist_0);
			}
			else if ((ilayer_1 is IRasterLayer || ilayer_1 is ITinLayer || ilayer_1 is IMapServerLayer ? true : ilayer_1 is ICadLayer))
			{
				IArray array = (ilayer_1 as IIdentify).Identify(igeometry_0);
				if (array != null)
				{
					for (int i = 0; i < array.Count; i++)
					{
						ilist_0.Add(array.Element[i]);
					}
				}
			}
			return true;
		}

		public void IdentifyMap(IBasicMap ibasicMap_0, double double_0, IPoint ipoint_0, IFeatureLayer ifeatureLayer_0, IArray iarray_0)
		{
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if (ifeatureLayer_0 == null)
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag = false;
				while (layer != null)
				{
					if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITSelectableLayer)
					{
						if (!(layer is IFeatureLayer))
						{
							layer = layers.Next();
							continue;
						}
						else if (!(layer as IFeatureLayer).Selectable)
						{
							layer = layers.Next();
							continue;
						}
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITTopLayer)
					{
						flag = true;
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITVisibleLayer)
					{
						if (!layer.Visible)
						{
							layer = layers.Next();
							continue;
						}
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITCurrentLayer)
					{
						if (this.ilayer_0 != layer)
						{
							layer = layers.Next();
							continue;
						}
						else
						{
							flag = true;
						}
					}
					try
					{
						if (layer is IFeatureLayer)
						{
							IdentifyHelper.FeatureLayerIdentify(ibasicMap_0, layer as IFeatureLayer, envelope, iarray_0);
						}
						else if ((layer is IRasterLayer || layer is ITinLayer || layer is IMapServerLayer ? true : layer is ICadLayer))
						{
							IArray array = (layer as IIdentify).Identify(envelope);
							if (array != null)
							{
								for (int i = 0; i < array.Count; i++)
								{
									iarray_0.Add(array.Element[i]);
								}
							}
						}
					}
					catch (Exception exception)
					{
						Logger.Current.Error("", exception, "");
					}
					if (flag)
					{
						break;
					}
					layer = layers.Next();
				}
			}
		}

		public void IdentifyMap(IBasicMap ibasicMap_0, double double_0, IPoint ipoint_0, IFeatureLayer ifeatureLayer_0, IList<object> ilist_0)
		{
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if (ifeatureLayer_0 == null)
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag = false;
				while (layer != null)
				{
					if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITSelectableLayer)
					{
						if (!(layer is IFeatureLayer))
						{
							layer = layers.Next();
							continue;
						}
						else if (!(layer as IFeatureLayer).Selectable)
						{
							layer = layers.Next();
							continue;
						}
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITTopLayer)
					{
						flag = true;
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITVisibleLayer)
					{
						if (!layer.Visible)
						{
							layer = layers.Next();
							continue;
						}
					}
					else if (this.identifyTypeEnum_0 == IdentifyTypeEnum.enumITCurrentLayer)
					{
						if (this.ilayer_0 != layer)
						{
							layer = layers.Next();
							continue;
						}
						else
						{
							flag = true;
						}
					}
					try
					{
						if (layer is IFeatureLayer)
						{
							IdentifyHelper.FeatureLayerIdentify(ibasicMap_0, layer as IFeatureLayer, envelope, ilist_0);
						}
						else if ((layer is IRasterLayer || layer is ITinLayer || layer is IMapServerLayer ? true : layer is ICadLayer))
						{
							IArray array = (layer as IIdentify).Identify(envelope);
							if (array != null)
							{
								for (int i = 0; i < array.Count; i++)
								{
									ilist_0.Add(array.Element[i]);
								}
							}
						}
					}
					catch (Exception exception)
					{
						Logger.Current.Error("", exception, "");
					}
					if (flag)
					{
						break;
					}
					layer = layers.Next();
				}
			}
		}

		public static bool IdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, out IArray iarray_0)
		{
			bool flag;
			iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (!IdentifyHelper.IsNotIdentyfy(layer))
					{
						if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
						{
							if (!(layer is IFeatureLayer))
							{
								layer = layers.Next();
								continue;
							}
							else if (!(layer as IFeatureLayer).Selectable)
							{
								layer = layers.Next();
								continue;
							}
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
						{
							flag1 = true;
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITVisibleLayer)
						{
							if (!layer.Visible)
							{
								layer = layers.Next();
								continue;
							}
						}
						try
						{
							IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, iarray_0);
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
						if (flag1)
						{
							break;
						}
						layer = layers.Next();
					}
					else
					{
						layer = layers.Next();
					}
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, iarray_0);
			}
			return flag;
		}

		public static bool IdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, out IList<object> ilist_0)
		{
			bool flag;
			ilist_0 = new List<object>();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
					{
						if (!(layer is IFeatureLayer))
						{
							layer = layers.Next();
							continue;
						}
						else if (!(layer as IFeatureLayer).Selectable)
						{
							layer = layers.Next();
							continue;
						}
					}
					else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
					{
						flag1 = true;
					}
					else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITVisibleLayer)
					{
						if (!layer.Visible)
						{
							layer = layers.Next();
							continue;
						}
					}
					try
					{
						IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, ilist_0);
					}
					catch (Exception exception)
					{
						Logger.Current.Error("", exception, "");
					}
					if (flag1)
					{
						break;
					}
					layer = layers.Next();
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, ilist_0);
			}
			return flag;
		}

		public static bool IdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, IList<string> ilist_0, out IArray iarray_0)
		{
			bool flag;
			iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (ilist_0.IndexOf(layer.Name) != -1)
					{
						if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
						{
							if (!(layer is IFeatureLayer))
							{
								layer = layers.Next();
								continue;
							}
							else if (!(layer as IFeatureLayer).Selectable)
							{
								layer = layers.Next();
								continue;
							}
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
						{
							flag1 = true;
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITVisibleLayer)
						{
							if (!layer.Visible)
							{
								layer = layers.Next();
								continue;
							}
						}
						try
						{
							IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, iarray_0);
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
						if (flag1)
						{
							break;
						}
						layer = layers.Next();
					}
					else
					{
						layers.Next();
					}
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, iarray_0);
			}
			return flag;
		}

		public static bool IdentifyMap(IBasicMap ibasicMap_0, IPoint ipoint_0, double double_0, ILayer ilayer_1, IdentifyTypeEnum identifyTypeEnum_1, IList<string> ilist_0, out IList<object> ilist_1)
		{
			bool flag;
			ilist_1 = new List<object>();
			IEnvelope envelope = ipoint_0.Envelope;
			envelope.Width = double_0;
			envelope.Height = double_0;
			envelope.CenterAt(ipoint_0);
			if ((ilayer_1 == null ? true : identifyTypeEnum_1 != IdentifyTypeEnum.enumITCurrentLayer))
			{
				UID uIDClass = new UID()
				{
					Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
				};
				IEnumLayer layers = ibasicMap_0.Layers[uIDClass, true];
				ISpatialFilter spatialFilterClass = new SpatialFilter()
				{
					Geometry = envelope,
					SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
				};
				layers.Reset();
				ILayer layer = layers.Next();
				bool flag1 = false;
				while (layer != null)
				{
					if (ilist_0.IndexOf(layer.Name) != -1)
					{
						if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITSelectableLayer)
						{
							if (!(layer is IFeatureLayer))
							{
								layer = layers.Next();
								continue;
							}
							else if (!(layer as IFeatureLayer).Selectable)
							{
								layer = layers.Next();
								continue;
							}
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITTopLayer)
						{
							flag1 = true;
						}
						else if (identifyTypeEnum_1 == IdentifyTypeEnum.enumITVisibleLayer)
						{
							if (!layer.Visible)
							{
								layer = layers.Next();
								continue;
							}
						}
						try
						{
							IdentifyHelper.IdentifyLayer(ibasicMap_0, layer, envelope, ilist_1);
						}
						catch (Exception exception)
						{
							Logger.Current.Error("", exception, "");
						}
						if (flag1)
						{
							break;
						}
						layer = layers.Next();
					}
					else
					{
						layers.Next();
					}
				}
				flag = true;
			}
			else
			{
				flag = IdentifyHelper.IdentifyLayer(ibasicMap_0, ilayer_1, envelope, ilist_1);
			}
			return flag;
		}

		private static bool IsNotIdentyfy(ILayer ilayer_1)
		{
			bool flag;
			flag = (IdentifyHelper.m_pIdentifyLayers.IndexOf(ilayer_1.Name) != -1 ? true : false);
			return flag;
		}

		private static void old_acctor_mc()
		{
			IdentifyHelper.m_pIdentifyLayers = new List<string>();
		}
	}
}