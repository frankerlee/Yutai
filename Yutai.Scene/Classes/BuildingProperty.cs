using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Classes
{
	internal class BuildingProperty
	{
		private static string m_pBuildingHieght;

		private static clsTextureGroup m_pTextureGroup;

		private static bool m_CreateNewLayer;

		private static IScene m_pScene;

		private static IGraphicsContainer3D m_pTargetLayer;

		private static List<clsTextureGroup> m_groups;

		private static List<IGraphicsContainer3D> m_pLayers;

		private static Dictionary<int, List<IElement>> m_ElemntMap;

		public static string BuildingHieght
		{
			get
			{
				return BuildingProperty.m_pBuildingHieght;
			}
			set
			{
				BuildingProperty.m_pBuildingHieght = value;
			}
		}

		public static clsTextureGroup TextureGroup
		{
			get
			{
				if (BuildingProperty.m_pTextureGroup == null)
				{
					BuildingProperty.PreloadTextureGroups();
				}
				return BuildingProperty.m_pTextureGroup;
			}
			set
			{
				BuildingProperty.m_pTextureGroup = value;
			}
		}

		public static IScene Scene
		{
			get
			{
				return BuildingProperty.m_pScene;
			}
			set
			{
				BuildingProperty.m_pScene = value;
			}
		}

		public static bool CreateNewLayer
		{
			get
			{
				return BuildingProperty.m_CreateNewLayer;
			}
			set
			{
				BuildingProperty.m_CreateNewLayer = value;
				IGraphicsContainer3D graphicsContainer3D = BuildingProperty.GetTempleGraphicsLayer(BuildingProperty.m_pScene);
				if (graphicsContainer3D == null)
				{
					graphicsContainer3D = new GraphicsLayer3DClass();
					(graphicsContainer3D as ILayer).Name = "BuildGraphicLayer";
					BuildingProperty.m_pScene.AddLayer(graphicsContainer3D as ILayer, false);
				}
				BuildingProperty.m_pTargetLayer = graphicsContainer3D;
			}
		}

		public static IGraphicsContainer3D TargetLayer
		{
			get
			{
				return BuildingProperty.m_pTargetLayer;
			}
			set
			{
				BuildingProperty.m_pTargetLayer = value;
				BuildingProperty.m_CreateNewLayer = false;
				if (!TreeTool.LayerIsExist(BuildingProperty.m_pScene, BuildingProperty.TargetLayer as ILayer))
				{
					BuildingProperty.m_pScene.AddLayer(BuildingProperty.TargetLayer as ILayer, false);
				}
			}
		}

		public static List<clsTextureGroup> TextureGroups
		{
			get
			{
				if (BuildingProperty.m_groups == null)
				{
					BuildingProperty.PreloadTextureGroups();
				}
				return BuildingProperty.m_groups;
			}
		}

		static BuildingProperty()
		{
			// 注意: 此类型已标记为 'beforefieldinit'.
			BuildingProperty.old_acctor_mc();
		}

		private static IGraphicsContainer3D GetTempleGraphicsLayer(IScene iscene_0)
		{
			IGraphicsContainer3D result;
			for (int i = 0; i < iscene_0.LayerCount; i++)
			{
				ILayer layer = iscene_0.get_Layer(i);
				if (layer is IGraphicsContainer3D && layer.Name == "BuildGraphicLayer")
				{
					result = (layer as IGraphicsContainer3D);
					return result;
				}
			}
			result = null;
			return result;
		}

		public static List<clsTextureGroup> PreloadTextureGroups(bool bool_0)
		{
			List<clsTextureGroup> result;
			try
			{
				List<clsTextureGroup> list = new List<clsTextureGroup>();
				clsTextureGroup clsTextureGroup = new clsTextureGroup();
				list.Add(new clsTextureGroup
				{
					name = "默认",
					RoofColorRGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown),
					TexturePaths = 
					{
						"[facade01]",
						"[facade02]",
						"[facade03]",
						"[facade04]",
						"[facade05]"
					}
				});
				bool flag = false;
				string path = System.Windows.Forms.Application.StartupPath + "\\TextureGroups.ini";
				if (File.Exists(path))
				{
					flag = true;
				}
				if (!flag)
				{
				}
				if (bool_0)
				{
					modFacades.InitTextures(list, false);
				}
				result = list;
				return result;
			}
			catch
			{
			}
			result = null;
			return result;
		}

		public static void PreloadTextureGroups()
		{
			try
			{
				if (BuildingProperty.m_groups == null)
				{
					BuildingProperty.m_groups = new List<clsTextureGroup>();
					clsTextureGroup clsTextureGroup = new clsTextureGroup();
					clsTextureGroup.name = "默认";
					clsTextureGroup.RoofColorRGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
					clsTextureGroup.TexturePaths.Add("[facade01]");
					clsTextureGroup.TexturePaths.Add("[facade02]");
					clsTextureGroup.TexturePaths.Add("[facade03]");
					clsTextureGroup.TexturePaths.Add("[facade04]");
					clsTextureGroup.TexturePaths.Add("[facade05]");
					BuildingProperty.m_groups.Add(clsTextureGroup);
					bool flag = false;
					string path = System.Windows.Forms.Application.StartupPath + "\\TextureGroups.ini";
					if (File.Exists(path))
					{
						flag = true;
					}
					if (!flag)
					{
					}
					modFacades.InitTextures(BuildingProperty.m_groups, false);
				}
			}
			catch
			{
			}
		}

		public static void Clear()
		{
			for (int i = 0; i < BuildingProperty.m_pLayers.Count; i++)
			{
				try
				{
					List<IElement> list = BuildingProperty.m_ElemntMap[i];
					if (BuildingProperty.m_pScene.BasicGraphicsLayer == BuildingProperty.m_pLayers[i])
					{
						for (int j = 0; j < list.Count; j++)
						{
							BuildingProperty.m_pLayers[i].DeleteElement(list[j]);
						}
					}
					else
					{
						BuildingProperty.m_pLayers[i].DeleteAllElements();
					}
				}
				catch
				{
				}
				BuildingProperty.m_pLayers.Clear();
				BuildingProperty.m_ElemntMap.Clear();
			}
		}

		public static int GetGraphicsLayerIndex(IGraphicsContainer3D igraphicsContainer3D_0)
		{
			int result;
			for (int i = 0; i < BuildingProperty.m_pLayers.Count; i++)
			{
				if (BuildingProperty.m_pLayers[i] == igraphicsContainer3D_0)
				{
					result = i;
					return result;
				}
			}
			BuildingProperty.m_pLayers.Add(igraphicsContainer3D_0);
			result = BuildingProperty.m_pLayers.Count - 1;
			return result;
		}

		public static void AddElement(int int_0, IElement ielement_0)
		{
			List<IElement> list = null;
			try
			{
				list = BuildingProperty.m_ElemntMap[int_0];
			}
			catch
			{
			}
			if (list == null)
			{
				list = new List<IElement>();
				BuildingProperty.m_ElemntMap.Add(int_0, list);
			}
			list.Add(ielement_0);
		}

		private static void old_acctor_mc()
		{
			BuildingProperty.m_pBuildingHieght = "30";
			BuildingProperty.m_pTextureGroup = null;
			BuildingProperty.m_CreateNewLayer = true;
			BuildingProperty.m_pScene = null;
			BuildingProperty.m_pTargetLayer = null;
			BuildingProperty.m_groups = null;
			BuildingProperty.m_pLayers = new List<IGraphicsContainer3D>();
			BuildingProperty.m_ElemntMap = new Dictionary<int, List<IElement>>();
		}
	}
}
