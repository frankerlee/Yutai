using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Raster;

namespace Yutai.Plugins.Catalog.Forms
{
	/// <summary>
	/// 导入栅格数据窗体
	/// </summary>
	public class frmRasterLoad : Form
	{
		private SimpleButton btnDelete;

		private ListView listView1;

		private Label label2;

		private SimpleButton btnSelectOutLocation;

		private TextEdit txtOutLocation;

		private SimpleButton btnSelectInputFeatures;

		private Label lblSelectObjects;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private IGxObject m_pOutGxObject = null;

		private IList m_pInNames = new ArrayList();

		private IName m_pInName = null;

		private SimpleButton btnCancel;

		private SimpleButton btnOK;

		private System.Windows.Forms.ProgressBar progressBar1;

		private IName m_pOutName = null;

		/// <summary>
		/// 设置输出对象
		/// </summary>
		public IGxObject OutGxObject
		{
			set
			{
				this.m_pOutGxObject = value;
			}
		}

		public frmRasterLoad()
		{
			this.InitializeComponent();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
			{
				if (this.listView1.Items[i].Selected)
				{
					this.m_pInNames.RemoveAt(i);
					this.listView1.Items.RemoveAt(i);
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.CanDo())
				{
					RasterUtil rasterUtil = new RasterUtil();
					if (!(this.m_pOutName is IRasterCatalogName))
					{
						IRasterWorkspaceEx rasterWorkspaceEx = this.m_pOutName.Open() as IRasterWorkspaceEx;
						for (int i = 0; i < this.m_pInNames.Count; i++)
						{
							IRasterDataset2 rasterDataset2 = (this.m_pInNames[i] as IName).Open() as IRasterDataset2;
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension((rasterDataset2 as IDataset).Name);
							if ((fileNameWithoutExtension[0] < '0' ? false : fileNameWithoutExtension[0] <= '9'))
							{
								fileNameWithoutExtension = string.Concat("A", fileNameWithoutExtension);
							}
							if (rasterWorkspaceEx is IWorkspace2)
							{
								int num = 1;
								string str = fileNameWithoutExtension;
								try
								{
									while ((rasterWorkspaceEx as IWorkspace2).NameExists[esriDatasetType.esriDTFeatureClass, str])
									{
										str = string.Concat(fileNameWithoutExtension, "_", num.ToString());
										num++;
									}
								}
								catch
								{
								}
								fileNameWithoutExtension = str;
							}
							((IGeometryDefEdit)(new GeometryDef())).SpatialReference_2 = (rasterDataset2 as IGeoDataset).SpatialReference;
							rasterWorkspaceEx.SaveAsRasterDataset(fileNameWithoutExtension, rasterDataset2.CreateFullRaster(), null, "", null, null);
						}
					}
					else
					{
						IFeatureClass featureClass = this.m_pOutName.Open() as IFeatureClass;
						rasterUtil.ToRasterCatalog(this.m_pInNames, featureClass);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CErrorLog.writeErrorLog(this, exception, "");
				MessageBox.Show(exception.Message);
			}
			this.progressBar1.Visible = false;
		}

		private void btnSelectInputFeatures_Click(object sender, EventArgs e)
		{
			frmOpenFile _frmOpenFile = new frmOpenFile()
			{
				Text = "添加数据"
			};
			_frmOpenFile.RemoveAllFilters();
			_frmOpenFile.AllowMultiSelect = true;
			_frmOpenFile.AddFilter(new MyGxFilterRasterDatasets(), true);
			if (_frmOpenFile.DoModalOpen() == System.Windows.Forms.DialogResult.OK)
			{
				IArray items = _frmOpenFile.Items;
				if (items.Count != 0)
				{
					for (int i = 0; i < items.Count; i++)
					{
						IGxObject element = items.Element[i] as IGxObject;
						this.m_pInNames.Add(element.InternalObjectName);
						this.listView1.Items.Add(element.FullName);
					}
				}
			}
		}

		private void btnSelectOutLocation_Click(object sender, EventArgs e)
		{
            frmOpenFile frmOpenFile = new frmOpenFile();
            frmOpenFile.Text = "保存位置";
            frmOpenFile.RemoveAllFilters();
            frmOpenFile.AddFilter(new MyGxFilterWorkspaces(), true);
            frmOpenFile.AddFilter(new MyGxFilterRasterCatalogDatasets(), false);
            if (frmOpenFile.DoModalSaveLocation() == DialogResult.OK)
            {
                IArray items = frmOpenFile.Items;
                if (items.Count != 0)
                {
                    this.m_pOutGxObject = (items.get_Element(0) as IGxObject);
                    if (this.m_pOutGxObject is IGxDatabase)
                    {
                        this.m_pOutName = this.m_pOutGxObject.InternalObjectName;
                    }
                    else
                    {
                        if (!(this.m_pOutGxObject is IGxDataset))
                        {
                            return;
                        }
                        if ((this.m_pOutGxObject as IGxDataset).Type != esriDatasetType.esriDTRasterCatalog)
                        {
                            return;
                        }
                        this.m_pOutName = this.m_pOutGxObject.InternalObjectName;
                    }
                    this.txtOutLocation.Text = this.m_pOutGxObject.FullName;
                }
            }
        }

		/// <summary>
		/// 是否可以执行导入栅格数据
		/// </summary>
		/// <returns></returns>
		public bool CanDo()
		{
			bool flag;
			if (this.m_pInNames.Count == 0)
			{
				MessageBox.Show("请输入需要转换的栅格数据");
				flag = false;
			}
			else if (this.m_pOutName != null)
			{
				flag = true;
			}
			else
			{
				MessageBox.Show("请选择输出位置");
				flag = false;
			}
			return flag;
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void frmRasterLoad_Load(object sender, EventArgs e)
		{
            if (this.m_pOutGxObject != null)
            {
                if (this.m_pOutGxObject is IGxDatabase)
                {
                    this.m_pOutName = this.m_pOutGxObject.InternalObjectName;
                }
                else
                {
                    if (!(this.m_pOutGxObject is IGxDataset))
                    {
                        return;
                    }
                    if ((this.m_pOutGxObject as IGxDataset).Type != esriDatasetType.esriDTRasterCatalog)
                    {
                        return;
                    }
                    this.m_pOutName = this.m_pOutGxObject.InternalObjectName;
                }
                this.txtOutLocation.Text = this.m_pOutGxObject.FullName;
            }
        }

		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRasterLoad));
			this.btnDelete = new SimpleButton();
			this.listView1 = new ListView();
			this.label2 = new Label();
			this.btnSelectOutLocation = new SimpleButton();
			this.txtOutLocation = new TextEdit();
			this.btnSelectInputFeatures = new SimpleButton();
			this.lblSelectObjects = new Label();
			this.btnCancel = new SimpleButton();
			this.btnOK = new SimpleButton();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			((ISupportInitialize)this.txtOutLocation.Properties).BeginInit();
			base.SuspendLayout();
			this.btnDelete.Enabled = false;
			this.btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
			this.btnDelete.Location = new Point(266, 76);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(24, 24);
			this.btnDelete.TabIndex = 14;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.listView1.GridLines = true;
			this.listView1.Location = new Point(2, 44);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(256, 152);
			this.listView1.TabIndex = 13;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.List;
			this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(2, 204);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "输出位置";
			this.btnSelectOutLocation.Image = (Image)resources.GetObject("btnSelectOutLocation.Image");
			this.btnSelectOutLocation.Location = new Point(272, 228);
			this.btnSelectOutLocation.Name = "btnSelectOutLocation";
			this.btnSelectOutLocation.Size = new System.Drawing.Size(24, 24);
			this.btnSelectOutLocation.TabIndex = 11;
			this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
			this.txtOutLocation.EditValue = "";
			this.txtOutLocation.Location = new Point(8, 228);
			this.txtOutLocation.Name = "txtOutLocation";
			this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
			this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
			this.txtOutLocation.Properties.ReadOnly = true;
			this.txtOutLocation.Size = new System.Drawing.Size(256, 21);
			this.txtOutLocation.TabIndex = 10;
			this.btnSelectInputFeatures.Image = (Image)resources.GetObject("btnSelectInputFeatures.Image");
			this.btnSelectInputFeatures.Location = new Point(266, 36);
			this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
			this.btnSelectInputFeatures.Size = new System.Drawing.Size(24, 24);
			this.btnSelectInputFeatures.TabIndex = 9;
			this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
			this.lblSelectObjects.AutoSize = true;
			this.lblSelectObjects.Location = new Point(2, 20);
			this.lblSelectObjects.Name = "lblSelectObjects";
			this.lblSelectObjects.Size = new System.Drawing.Size(53, 12);
			this.lblSelectObjects.TabIndex = 8;
			this.lblSelectObjects.Text = "输入栅格";
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new Point(232, 264);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(56, 24);
			this.btnCancel.TabIndex = 16;
			this.btnCancel.Text = "取消";
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new Point(168, 264);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 24);
			this.btnOK.TabIndex = 15;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.progressBar1.Location = new Point(8, 264);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(144, 16);
			this.progressBar1.TabIndex = 17;
			this.progressBar1.Visible = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(304, 293);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnDelete);
			base.Controls.Add(this.listView1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.lblSelectObjects);
			base.Controls.Add(this.btnSelectOutLocation);
			base.Controls.Add(this.txtOutLocation);
			base.Controls.Add(this.btnSelectInputFeatures);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmRasterLoad";
			this.Text = "导入栅格数据";
			base.Load += new EventHandler(this.frmRasterLoad_Load);
			((ISupportInitialize)this.txtOutLocation.Properties).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
		}
	}
}