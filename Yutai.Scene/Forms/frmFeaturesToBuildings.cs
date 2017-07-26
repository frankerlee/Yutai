using System;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Wrapper;
using Yutai.Plugins.Scene.Classes;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmFeaturesToBuildings : System.Windows.Forms.Form
	{
		private IBasicMap ibasicMap_0 = null;
	    private IScenePlugin _plugin;

		public IBasicMap Map
		{
			set
			{
				this.ibasicMap_0 = value;
			}
		}

		private void cmbBuildingLayer_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cmbBuildingLayer.SelectedItem is LayerObject)
				{
					IFeatureSelection featureSelection = (this.cmbBuildingLayer.SelectedItem as LayerObject).Layer as IFeatureSelection;
					if (featureSelection == null)
					{
						this.chkSelFeatures.Enabled = false;
						this.chkSelFeatures.Checked = false;
					}
					else if (featureSelection.SelectionSet.Count == 0)
					{
						this.chkSelFeatures.Enabled = false;
						this.chkSelFeatures.Checked = false;
					}
					else
					{
						this.chkSelFeatures.Enabled = true;
						this.chkSelFeatures.Checked = true;
					}
					IFeatureLayer featureLayer = featureSelection as IFeatureLayer;
					if (featureLayer != null)
					{
						IFields fields = featureLayer.FeatureClass.Fields;
						this.cmbHeight.Items.Clear();
						short num = 0;
						while ((int)num <= fields.FieldCount - 1)
						{
							IField field = fields.get_Field((int)num);
							int type = (int)field.Type;
							if (type > -1 && type < 4)
							{
								this.cmbHeight.Items.Add(field.Name);
							}
							num += 1;
						}
						if (this.cmbHeight.Items.Count > 0)
						{
							this.cmbHeight.SelectedIndex = 0;
						}
					}
				}
				else
				{
					this.chkSelFeatures.Enabled = false;
					this.chkSelFeatures.Checked = false;
				}
			}
			catch
			{
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Hide();
		}
        

		private void cmdLayerBrowse_Click(object sender, EventArgs e)
		{
			frmOpenFile frmOpenFile = new frmOpenFile();
			frmOpenFile.AddFilter(new MyGxFilterPolygonFeatureClasses(), true);
			frmOpenFile.Text = "选择要素";
			if (frmOpenFile.DoModalOpen() == System.Windows.Forms.DialogResult.OK)
			{
				IArray items = frmOpenFile.Items;
				for (int i = 0; i < items.Count; i++)
				{
					IGxDataset gxDataset = items.get_Element(i) as IGxDataset;
					this.cmbBuildingLayer.Items.Add(new ObjectWrap(gxDataset.Dataset));
				}
				this.cmbBuildingLayer.SelectedIndex = this.cmbBuildingLayer.Items.Count - 1;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
		    if (ibasicMap_0 == null)
		    {
		        ibasicMap_0 = _plugin.Scene as IBasicMap;
		    }
			this.method_2();
			if (this.ValidateDialog())
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				this.Refresh();
				this.RunCommand();
				this.Cursor = System.Windows.Forms.Cursors.Default;
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		private void method_1(object sender, EventArgs e)
		{
			this.method_2();
			if (this.ValidateDialog())
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				this.RunCommand();
				this.Cursor = System.Windows.Forms.Cursors.Default;
			}
		}

		private void cmdTexturesBrowse_Click(object sender, EventArgs e)
		{
			frmTexturePallette frmTexturePallette = new frmTexturePallette();
			frmTexturePallette.ShowDialog();
		}

		private void frmFeaturesToBuildings_Load(object sender, EventArgs e)
		{
			this.SetToDefaultState();
			IEnumLayer enumLayer = this.ibasicMap_0.get_Layers(null, true);
			this.txtDigitizeHeight.Text = BuildingProperty.BuildingHieght;
			this.cmbBuildingLayer.Items.Clear();
			if (enumLayer != null)
			{
				for (ILayer layer = enumLayer.Next(); layer != null; layer = enumLayer.Next())
				{
					if (layer is IFeatureLayer)
					{
						IFeatureLayer featureLayer = layer as IFeatureLayer;
						if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
						{
							this.cmbBuildingLayer.Items.Add(new LayerObject(layer));
						}
					}
				}
			}
			if (this.cmbBuildingLayer.Items.Count > 0)
			{
				this.cmbBuildingLayer.SelectedIndex = 0;
			}
		}


		public static I3DProperties Get3DPropsFromLayer(ILayer ilayer_0)
		{
			I3DProperties result;
			try
			{
				ILayerExtensions layerExtensions = ilayer_0 as ILayerExtensions;
				for (int i = 0; i <= layerExtensions.ExtensionCount - 1; i++)
				{
					I3DProperties i3DProperties = layerExtensions.get_Extension(i) as I3DProperties;
					if (i3DProperties != null)
					{
						result = i3DProperties;
						return result;
					}
				}
			}
			catch
			{
			}
			result = null;
			return result;
		}

		public void RunCommand()
		{
			try
			{
				IFeatureCursor ifeatureCursor_ = null;
				I3DProperties i3DProperties = null;
				if (this.cmbBuildingLayer.SelectedItem is LayerObject)
				{
					IFeatureLayer featureLayer = (this.cmbBuildingLayer.SelectedItem as LayerObject).Layer as IFeatureLayer;
					if (this.chkSelFeatures.Checked)
					{
						IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
						ICursor cursor;
						featureSelection.SelectionSet.Search(null, true, out cursor);
						ifeatureCursor_ = (cursor as IFeatureCursor);
					}
					else
					{
						ifeatureCursor_ = featureLayer.Search(null, true);
					}
					i3DProperties = frmFeaturesToBuildings.Get3DPropsFromLayer(featureLayer);
				}
				else if (this.cmbBuildingLayer.SelectedItem is ObjectWrap)
				{
					IFeatureClass featureClass = (this.cmbBuildingLayer.SelectedItem as ObjectWrap).Object as IFeatureClass;
					ifeatureCursor_ = featureClass.Search(null, true);
				}
				int int_ = -1;
				if (this.chkConst.Checked)
				{
					if (i3DProperties != null && i3DProperties.ExtrusionExpressionString.Length == 0)
					{
						int_ = int.Parse(BuildingProperty.BuildingHieght);
					}
					else
					{
						int_ = -1;
					}
					modFacades.CreateFacades(BuildingProperty.TargetLayer, ifeatureCursor_, i3DProperties as IFeature3DProperties, "", int_, null, BuildingProperty.TextureGroups, true);
				}
				else
				{
					modFacades.CreateFacades(BuildingProperty.TargetLayer, ifeatureCursor_, i3DProperties as IFeature3DProperties, this.cmbHeight.Text, int_, null, BuildingProperty.TextureGroups, true);
				}
			}
			catch
			{
			}
		}

		private void method_2()
		{
			if (BuildingProperty.TargetLayer == null)
			{
			    BuildingProperty.Scene = _plugin.Scene;

                BuildingProperty.TargetLayer = ((this.ibasicMap_0 as IScene).BasicGraphicsLayer as IGraphicsContainer3D);
				if (!TreeTool.LayerIsExist(this.ibasicMap_0 as IScene, BuildingProperty.TargetLayer as ILayer))
				{
					(this.ibasicMap_0 as IScene).AddLayer(BuildingProperty.TargetLayer as ILayer, false);
				}
			}
		}

		public void SetToDefaultState()
		{
			this.chkSelFeatures.CheckState = System.Windows.Forms.CheckState.Unchecked;
		}

		private void lstTextureGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.lstTextureGroups.SelectedIndex < 0)
				{
				}
			}
			catch
			{
			}
		}

		public void mnuApplyTextureGroup_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch
			{
			}
		}

		public void FormatMe(bool bool_0, bool bool_1)
		{
			try
			{
				if (bool_0)
				{
					this.SetToDefaultState();
				}
			}
			catch
			{
			}
		}

		public bool ValidateDialog()
		{
			bool flag = false;
			bool result;
			try
			{
				if (this.cmbBuildingLayer.Text.Length < 1)
				{
					System.Windows.Forms.MessageBox.Show("请选择一个图层:");
					result = flag;
					return result;
				}
				flag = true;
				result = true;
				return result;
			}
			catch
			{
			}
			result = flag;
			return result;
		}

		public void mnuBuildingDeselectAll_Click(object sender, EventArgs e)
		{
		}

		private void method_3()
		{
		}

		public void mnuBuildingsDeleteSelected_Click(object sender, EventArgs e)
		{
		}

		public void mnuBuildingsSelectAll_Click(object sender, EventArgs e)
		{
		}

		public void mnuCreateBuildingFromFootprint_Click(object sender, EventArgs e)
		{
		}

		private void method_4()
		{
		}

		private void method_5()
		{
			frmProperties frmProperties = new frmProperties();
			frmProperties.ShowDialog();
		}

		public void mnuRoofColor_Click(object sender, EventArgs e)
		{
			frmTexturePallette frmTexturePallette = new frmTexturePallette();
			frmTexturePallette.ShowDialog();
		}

		private void chkConst_CheckedChanged(object sender, EventArgs e)
		{
			this.txtDigitizeHeight.Enabled = this.chkConst.Checked;
			this.cmbHeight.Enabled = !this.chkConst.Checked;
		}

		public frmFeaturesToBuildings()
		{
			this.InitializeComponent();
		}

        public frmFeaturesToBuildings(IScenePlugin plugin)
        {
            this.InitializeComponent();
            _plugin = plugin;
        }


    }
}