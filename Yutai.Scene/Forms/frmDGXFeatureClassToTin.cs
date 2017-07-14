using System;
using System.ComponentModel;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Forms
{
	public partial class frmDGXFeatureClassToTin : System.Windows.Forms.Form
	{
       
        internal partial class TinSurfaceType
		{
            private int int_0;
            public esriTinSurfaceType SurfaceType
			{
				get
				{
					return (esriTinSurfaceType)this.int_0;
				}
			}

			public TinSurfaceType(int int_1)
			{
				this.int_0 = int_1;
			}

			public override string ToString()
			{
				string result;
				switch (this.int_0)
				{
				case 0:
					result = "轮廓线";
					break;
				case 1:
					result = "硬线条";
					break;
				case 2:
					result = "硬裁减";
					break;
				case 3:
					result = "硬删除";
					break;
				case 4:
					result = "硬替换";
					break;
				case 5:
					result = "硬值填充";
					break;
				case 6:
					result = "基准硬线条";
					break;
				case 7:
					result = "基准硬裁减";
					break;
				case 8:
					result = "基准硬删除";
					break;
				case 9:
					result = "软线条";
					break;
				case 10:
					result = "软裁减";
					break;
				case 11:
					result = "软删除";
					break;
				case 12:
					result = "软替换";
					break;
				case 13:
					result = "软值填充";
					break;
				case 14:
					result = "基准软线条";
					break;
				case 15:
					result = "基准轮廓线";
					break;
				case 16:
					result = "基准软裁减";
					break;
				case 17:
					result = "基准软删除";
					break;
				default:
					result = "散点";
					break;
				}
				return result;
			}
		}

		public IBasicMap m_pMap = null;

		private bool bool_0 = false;








		private Container container_0 = null;

		private frmDGXFeatureClassToTin.TinSurfaceType[] tinSurfaceType_0 = new frmDGXFeatureClassToTin.TinSurfaceType[]
		{
			new frmDGXFeatureClassToTin.TinSurfaceType(18)
		};

		private frmDGXFeatureClassToTin.TinSurfaceType[] tinSurfaceType_1 = new frmDGXFeatureClassToTin.TinSurfaceType[]
		{
			new frmDGXFeatureClassToTin.TinSurfaceType(18),
			new frmDGXFeatureClassToTin.TinSurfaceType(1),
			new frmDGXFeatureClassToTin.TinSurfaceType(9)
		};



		private frmDGXFeatureClassToTin.TinSurfaceType[] tinSurfaceType_2 = new frmDGXFeatureClassToTin.TinSurfaceType[]
		{
			new frmDGXFeatureClassToTin.TinSurfaceType(18),
			new frmDGXFeatureClassToTin.TinSurfaceType(1),
			new frmDGXFeatureClassToTin.TinSurfaceType(9),
			new frmDGXFeatureClassToTin.TinSurfaceType(2),
			new frmDGXFeatureClassToTin.TinSurfaceType(10),
			new frmDGXFeatureClassToTin.TinSurfaceType(3),
			new frmDGXFeatureClassToTin.TinSurfaceType(11),
			new frmDGXFeatureClassToTin.TinSurfaceType(4),
			new frmDGXFeatureClassToTin.TinSurfaceType(12),
			new frmDGXFeatureClassToTin.TinSurfaceType(5),
			new frmDGXFeatureClassToTin.TinSurfaceType(13)
		};

		private IFeatureClass ifeatureClass_0 = null;

		public IFeatureClass FeatureClass
		{
			set
			{
				this.ifeatureClass_0 = value;
			}
		}

		public IBasicMap Map
		{
			set
			{
				this.m_pMap = value;
			}
		}

		public frmDGXFeatureClassToTin()
		{
			this.InitializeComponent();
		}

	private void frmDGXFeatureClassToTin_Load(object sender, EventArgs e)
		{
			this.method_1();
		}

		private void method_0(ICompositeLayer icompositeLayer_0)
		{
			for (int i = 0; i < icompositeLayer_0.Count; i++)
			{
				ILayer layer = icompositeLayer_0.get_Layer(i);
				if (layer is IGroupLayer)
				{
					this.method_0(layer as ICompositeLayer);
				}
				else if (layer is IFeatureLayer)
				{
					IFeatureLayer object_ = layer as IFeatureLayer;
					new BulidTinFeatureClassInfo(object_);
				}
			}
		}

		private void method_1()
		{
			this.bool_0 = false;
			this.cboHeightField.Items.Clear();
			this.cboTagValueField.Items.Clear();
			this.cboTinSurfaceType.Items.Clear();
			if (this.ifeatureClass_0 != null)
			{
				IFeatureClass featureClass = this.ifeatureClass_0;
				switch (featureClass.ShapeType)
				{
				case esriGeometryType.esriGeometryPoint:
				case esriGeometryType.esriGeometryMultipoint:
					for (int i = 0; i < this.tinSurfaceType_0.Length; i++)
					{
						this.cboTinSurfaceType.Items.Add(this.tinSurfaceType_0[i]);
					}
					this.cboTagValueField.Enabled = true;
					break;
				case esriGeometryType.esriGeometryPolyline:
					for (int i = 0; i < this.tinSurfaceType_1.Length; i++)
					{
						this.cboTinSurfaceType.Items.Add(this.tinSurfaceType_1[i]);
					}
					this.cboTagValueField.Enabled = false;
					break;
				case esriGeometryType.esriGeometryPolygon:
					for (int i = 0; i < this.tinSurfaceType_2.Length; i++)
					{
						this.cboTinSurfaceType.Items.Add(this.tinSurfaceType_2[i]);
					}
					this.cboTagValueField.Enabled = true;
					break;
				}
				this.cboTinSurfaceType.SelectedIndex = 1;
				int index = featureClass.FindField(featureClass.ShapeFieldName);
				IField field = featureClass.Fields.get_Field(index);
				if (field.GeometryDef.HasZ)
				{
					this.cboHeightField.Items.Add("<要素Z值>");
				}
				for (int i = 0; i < featureClass.Fields.FieldCount; i++)
				{
					IField field2 = featureClass.Fields.get_Field(i);
					if (field2.Type == esriFieldType.esriFieldTypeDouble || field2.Type == esriFieldType.esriFieldTypeInteger || field2.Type == esriFieldType.esriFieldTypeSingle || field2.Type == esriFieldType.esriFieldTypeSmallInteger)
					{
						this.cboHeightField.Items.Add(field2.Name);
						this.cboTagValueField.Items.Add(field2.Name);
					}
				}
				this.cboHeightField.Items.Add("<无>");
				this.cboTagValueField.Items.Add("<无>");
				this.cboHeightField.SelectedIndex = 0;
				this.cboTagValueField.SelectedIndex = 0;
			}
		}

		private bool method_2()
		{
			bool result;
			try
			{
				IExtensionManagerAdmin extensionManagerAdmin = new ExtensionManager() as IExtensionManagerAdmin;
				UID uID = new UID();
				uID.Value = "esriGeoDatabase.DddServerEnvironment";
				object obj = null;
				extensionManagerAdmin.AddExtension(uID, ref obj);
				IExtensionManager extensionManager = extensionManagerAdmin as IExtensionManager;
				IExtensionConfig extensionConfig = extensionManager.FindExtension(uID) as IExtensionConfig;
				extensionConfig.State = esriExtensionState.esriESEnabled;
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
			result = false;
			return result;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (this.method_2())
			{
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
				try
				{
					IEnvelope extent = (this.ifeatureClass_0 as IGeoDataset).Extent;
					ITinEdit tinEdit = new Tin() as ITinEdit;
					tinEdit.InitNew(extent);
					tinEdit.StartEditing();
					IField pHeightField = null;
					bool flag = false;
					if (this.cboHeightField.Text != "")
					{
						if (this.cboHeightField.SelectedIndex == 0)
						{
							flag = true;
						}
						else
						{
							int num = this.ifeatureClass_0.FindField(this.cboHeightField.Text);
							if (num != -1)
							{
								pHeightField = this.ifeatureClass_0.Fields.get_Field(num);
							}
						}
					}
					IField pTagValueField = null;
					object obj = flag;
					tinEdit.AddFromFeatureClass(this.ifeatureClass_0, null, pHeightField, pTagValueField, (esriTinSurfaceType)this.cboTinSurfaceType.SelectedIndex, ref obj);
					object obj2 = true;
					string tempPath = System.IO.Path.GetTempPath();
					int num2 = 0;
					string text = tempPath + "tmptin";
					while (Directory.Exists(text))
					{
						text = tempPath + "tmptin" + num2++.ToString();
					}
					tinEdit.SaveAs(text, ref obj2);
					tinEdit.StopEditing(true);
					if (this.m_pMap != null)
					{
						ILayerFactoryHelper layerFactoryHelper = new LayerFactoryHelper();
						IEnumLayer enumLayer = layerFactoryHelper.CreateLayersFromName((tinEdit as IDataset).FullName);
						enumLayer.Reset();
						ILayer pLayer = enumLayer.Next();
						this.m_pMap.AddLayer(pLayer);
					}
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
				}
				catch (Exception exception_)
				{
					System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
					System.Windows.Forms.MessageBox.Show("无法创建TIN，请确保输入要素的是否合适!");
					CErrorLog.writeErrorLog(this, exception_, "");
				}
			}
		}
	}
}
