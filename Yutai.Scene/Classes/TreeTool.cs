using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.Scene.Classes
{
	internal class TreeTool
	{
		private static ISurface m_pSurface;

		private static ILayer m_pTargetLayer;

		private static List<Tree> m_trees;

		private static double m_TreeHeight;

		private static int m_TreeSelectIndex;

		public static ISurface Surface
		{
			get
			{
				return TreeTool.m_pSurface;
			}
			set
			{
				TreeTool.m_pSurface = null;
			}
		}

		public static ILayer TargetLayer
		{
			get
			{
				return TreeTool.m_pTargetLayer;
			}
			set
			{
				TreeTool.m_pTargetLayer = value;
			}
		}

		public static double TreeHeight
		{
			get
			{
				return TreeTool.m_TreeHeight;
			}
			set
			{
				TreeTool.m_TreeHeight = value;
			}
		}

		public static int TreeSelectIndex
		{
			get
			{
				return TreeTool.m_TreeSelectIndex;
			}
			set
			{
				TreeTool.m_TreeSelectIndex = value;
			}
		}

		public static int TreeCount
		{
			get
			{
				return TreeTool.m_trees.Count;
			}
		}

		static TreeTool()
		{
			// 注意: 此类型已标记为 'beforefieldinit'.
			TreeTool.old_acctor_mc();
		}

		public static bool LayerIsExist(IScene iscene_0, ILayer ilayer_0)
		{
			bool result;
			for (int i = 0; i < iscene_0.LayerCount; i++)
			{
				if (iscene_0.get_Layer(i) == ilayer_0)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static void LoadTree()
		{
			try
			{
				string path = System.Windows.Forms.Application.StartupPath + "\\TreeImages";
				string[] files = Directory.GetFiles(path, "*.bmp");
				for (int i = 0; i < files.Length; i++)
				{
					TreeTool.m_trees.Add(new Tree(files[i]));
				}
			}
			catch
			{
			}
		}

		public static Tree Item(int int_0)
		{
			return TreeTool.m_trees[int_0];
		}

		public static void Add(Tree tree_0)
		{
			TreeTool.m_trees.Add(tree_0);
		}

		public static void Clear()
		{
			TreeTool.m_trees.Clear();
		}

		public static void Remove(Tree tree_0)
		{
			TreeTool.m_trees.Remove(tree_0);
		}

		public static void RemoveAt(int int_0)
		{
			TreeTool.m_trees.RemoveAt(int_0);
		}

		private static void old_acctor_mc()
		{
			TreeTool.m_pSurface = null;
			TreeTool.m_pTargetLayer = null;
			TreeTool.m_trees = new List<Tree>();
			TreeTool.m_TreeHeight = 12.0;
			TreeTool.m_TreeSelectIndex = 0;
		}
	}
}
