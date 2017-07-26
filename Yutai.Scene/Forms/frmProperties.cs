using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Wrapper;
using Yutai.Plugins.Scene.Classes;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmProperties : System.Windows.Forms.Form
	{
        private short short_0;

        private IBasicMap ibasicMap_0 = null;


		private List<clsTextureGroup> list_0 = null;

		public IBasicMap Map
		{
			set
			{
				this.ibasicMap_0 = value;
			}
		}

		public List<clsTextureGroup> TextureGroups
		{
			get
			{
				return this.list_0;
			}
			set
			{
				this.list_0 = value;
			}
		}

		public frmProperties()
		{
			this.InitializeComponent();
		}

	private void cmdCancel_Click(object sender, EventArgs e)
		{
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				BuildingProperty.BuildingHieght = this.txtDigitizeHeight.Text;
				if (this.cmbTextureGroup.SelectedIndex >= 0)
				{
					BuildingProperty.TextureGroup = this.list_0[this.cmbTextureGroup.SelectedIndex];
				}
				this.method_1();
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			catch
			{
			}
		}

		private void cmdTextureBrowse_Click(object sender, EventArgs e)
		{
			if (new frmTexturePallette
			{
				TextureGroups = this.list_0
			}.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.cmbTextureGroup.Items.Clear();
				for (int i = 0; i < this.list_0.Count; i++)
				{
					this.cmbTextureGroup.Items.Add(this.list_0[i]);
				}
				this.cmbTextureGroup.SelectedIndex = this.cmbTextureGroup.Items.Count - 1;
			}
		}

		private void method_0()
		{
			try
			{
				this.cmbTargetLayer.Items.Clear();
				this.cmbTargetLayer.Items.Add("<自动创建新层>");
				this.cmbTargetLayer.Items.Add("<默认的ArcScene图形层>");
				this.cmbTargetLayer.SelectedIndex = 0;
				for (int i = 0; i <= this.ibasicMap_0.LayerCount - 1; i++)
				{
					ILayer layer = this.ibasicMap_0.get_Layer(i);
					if (layer is IGraphicsLayer && layer.Name != "MyTempleGraphics" && layer.Name != "SketchTempleGraphics" && layer.Name != "EditFeatureNodeDisplayHelper")
					{
						this.cmbTargetLayer.Items.Add(new LayerObject(layer));
					}
				}
				this.txtDigitizeHeight.Text = BuildingProperty.BuildingHieght;
				this.cmbTextureGroup.Items.Clear();
				for (int i = 0; i < this.list_0.Count; i++)
				{
					this.cmbTextureGroup.Items.Add(this.list_0[i]);
				}
				if (this.cmbTextureGroup.Items.Count > 0)
				{
					this.cmbTextureGroup.SelectedIndex = 0;
				}
			}
			catch
			{
			}
		}

		private void method_1()
		{
			try
			{
				if (this.cmbTargetLayer.SelectedIndex == 0)
				{
					BuildingProperty.CreateNewLayer = true;
				}
				else if (this.cmbTargetLayer.SelectedIndex == 1)
				{
					BuildingProperty.TargetLayer = ((this.ibasicMap_0 as IScene).BasicGraphicsLayer as IGraphicsContainer3D);
				}
				else if (this.cmbTargetLayer.SelectedIndex > 1)
				{
					BuildingProperty.TargetLayer = ((this.cmbTargetLayer.SelectedItem as LayerObject).Layer as IGraphicsContainer3D);
				}
			}
			catch
			{
			}
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

		private void frmProperties_Load(object sender, EventArgs e)
		{
			this.method_0();
		}

		private void cmbTargetLayer_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
	}
}
