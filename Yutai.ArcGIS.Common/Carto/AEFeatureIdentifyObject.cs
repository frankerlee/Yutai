using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Carto
{
	public class AEFeatureIdentifyObject : IIdentifyObj, IRowIdentifyObject, IBasicMapIdentifyObject, IFeatureIdentifyObj, IMapIdentifyObject
	{
		private IBasicMap ibasicMap_0 = null;

		private IFeature ifeature_0 = null;

		private ILayer ilayer_0 = null;

		public IBasicMap BasicMap
		{
			set
			{
				this.ibasicMap_0 = value;
			}
		}

		public IFeature Feature
		{
			set
			{
				this.ifeature_0 = value;
			}
		}

		public int hWnd
		{
			get
			{
				return 0;
			}
		}

		public ILayer Layer
		{
			get
			{
				ILayer layer;
				if (this.ilayer_0 == null)
				{
					if ((this.ifeature_0 == null ? false : this.ibasicMap_0 != null))
					{
						for (int i = 0; i < this.ibasicMap_0.LayerCount; i++)
						{
							ILayer layer1 = this.ibasicMap_0.Layer[i];
							if (layer1 is IFeatureLayer)
							{
								if ((layer1 as IFeatureLayer).FeatureClass == this.ifeature_0.Class)
								{
									layer = layer1;
									return layer;
								}
							}
							else if (layer1 is IGroupLayer)
							{
								ICompositeLayer compositeLayer = layer1 as ICompositeLayer;
								ILayer layer2 = null;
								int num = 0;
								while (num < compositeLayer.Count)
								{
									layer2 = this.method_0(this.ifeature_0.Class, compositeLayer.Layer[num]);
									if (layer2 != null)
									{
										layer = layer2;
										return layer;
									}
									else
									{
										num++;
									}
								}
							}
						}
					}
				}
				layer = null;
				return layer;
			}
		}

		public IMap Map
		{
			set
			{
				this.ibasicMap_0 = value as IBasicMap;
			}
		}

		public string Name
		{
			get
			{
				return "FeatureIdentifyObject";
			}
		}

		public IRow Row
		{
			get
			{
				return this.ifeature_0;
			}
			set
			{
				this.ifeature_0 = value as IFeature;
			}
		}

		public AEFeatureIdentifyObject()
		{
		}

		public bool CanIdentify(ILayer ilayer_1)
		{
			return ilayer_1 is IFeatureLayer;
		}

		public void Flash(IScreenDisplay iscreenDisplay_0)
		{
			if (this.ifeature_0 != null)
			{
				if (!(this.ibasicMap_0 is IScene))
				{
					Display.Flash.FlashFeature(iscreenDisplay_0, this.ifeature_0);
				}
				else
				{
					((this.ibasicMap_0 as IScene).SceneGraph as IDisplay3D).AddFlashFeature(this.ifeature_0.Shape);
					((this.ibasicMap_0 as IScene).SceneGraph as IDisplay3D).FlashFeatures();
				}
			}
		}

		private ILayer method_0(IClass iclass_0, ILayer ilayer_1)
		{
			ILayer ilayer1;
			if (!(ilayer_1 is ICompositeLayer))
			{
				if (!(ilayer_1 is IFeatureLayer) || (ilayer_1 as IFeatureLayer).FeatureClass != iclass_0)
				{
					ilayer1 = null;
					return ilayer1;
				}
				ilayer1 = ilayer_1;
				return ilayer1;
			}
			else
			{
				ICompositeLayer compositeLayer = ilayer_1 as ICompositeLayer;
				ILayer layer = null;
				int num = 0;
				while (num < compositeLayer.Count)
				{
					layer = this.method_0(iclass_0, compositeLayer.Layer[num]);
					if (layer != null)
					{
						ilayer1 = layer;
						return ilayer1;
					}
					else
					{
						num++;
					}
				}
			}
			ilayer1 = null;
			return ilayer1;
		}

		public void PopUpMenu(int int_0, int int_1)
		{
		}
	}
}