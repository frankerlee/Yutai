using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class BufferOutputSetCtrl : UserControl
    {
        private SimpleButton btnSelectInputFeatures;
        private ComboBoxEdit cboEditingPolygonLayer;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private RadioGroup rdoBufferType;
        private RadioButton rdoNewLayer;
        private RadioGroup rdoOutputType;
        private RadioButton rdoSaveToEditingLayer;
        private RadioButton rdoSaveToGraphicLayer;
        private SysGrants sysGrants_0 = new SysGrants();
        private TextEdit txtOutName;

        public BufferOutputSetCtrl()
        {
            this.InitializeComponent();
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterFeatureClasses(), true);
            if (file.DoModalSave() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        IWorkspaceName name = null;
                        IGxObject obj2 = items.get_Element(0) as IGxObject;
                        string str = "";
                        string fullName = "";
                        if (obj2 is IGxDatabase)
                        {
                            BufferHelper.m_BufferHelper.m_pOutFeatureWorksapce = (obj2 as IGxDatabase).Workspace as IFeatureWorkspace;
                            fullName = obj2.FullName;
                        }
                        else if (obj2 is IGxFolder)
                        {
                            name = new WorkspaceNameClass {
                                WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                                PathName = (obj2.InternalObjectName as IFileName).Path
                            };
                            BufferHelper.m_BufferHelper.m_pOutFeatureWorksapce = (name as IName).Open() as IFeatureWorkspace;
                            fullName = (obj2.InternalObjectName as IFileName).Path;
                            str = ".shp";
                        }
                        BufferHelper.m_BufferHelper.m_FeatClassName = file.SaveName;
                        this.txtOutName.Text = fullName + @"\" + file.SaveName + str;
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(null, exception, "");
                    }
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                }
            }
        }

        private void BufferOutputSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void cboEditingPolygonLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboEditingPolygonLayer.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pOutFC = ((this.cboEditingPolygonLayer.SelectedItem as ObjectWrap).Object as IFeatureLayer).FeatureClass;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Init()
        {
            UID uid = new UIDClass {
                Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"
            };
            IEnumLayer layer = BufferHelper.m_BufferHelper.m_pFocusMap.get_Layers(uid, true);
            this.cboEditingPolygonLayer.Properties.Items.Clear();
            layer.Reset();
            for (ILayer layer2 = layer.Next(); layer2 is IFeatureLayer; layer2 = layer.Next())
            {
                if ((((layer2 as IFeatureLayer).FeatureClass != null) && ((layer2 as IFeatureLayer).FeatureClass.FeatureType == esriFeatureType.esriFTSimple)) && this.method_0(layer2 as IFeatureLayer))
                {
                    this.cboEditingPolygonLayer.Properties.Items.Add(new ObjectWrap(layer2));
                }
            }
            this.cboEditingPolygonLayer.Enabled = false;
            if (this.cboEditingPolygonLayer.Properties.Items.Count > 0)
            {
                this.rdoSaveToEditingLayer.Enabled = true;
                this.cboEditingPolygonLayer.SelectedIndex = 0;
            }
            else
            {
                this.rdoSaveToEditingLayer.Enabled = false;
            }
            if (BufferHelper.m_BufferHelper.m_pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                this.rdoBufferType.Enabled = true;
            }
            else
            {
                this.rdoBufferType.Enabled = false;
            }
            this.txtOutName.Enabled = this.rdoNewLayer.Checked;
            this.btnSelectInputFeatures.Enabled = this.rdoNewLayer.Checked;
            this.cboEditingPolygonLayer.Enabled = this.rdoSaveToEditingLayer.Checked;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BufferOutputSetCtrl));
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.rdoOutputType = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.rdoBufferType = new RadioGroup();
            this.groupBox3 = new GroupBox();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutName = new TextEdit();
            this.cboEditingPolygonLayer = new ComboBoxEdit();
            this.rdoNewLayer = new RadioButton();
            this.rdoSaveToEditingLayer = new RadioButton();
            this.rdoSaveToGraphicLayer = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.rdoOutputType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.rdoBufferType.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtOutName.Properties.BeginInit();
            this.cboEditingPolygonLayer.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoOutputType);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1d0, 0x38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "缓冲输出类型";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x18, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa7, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "是否融合各缓冲间重合的部分?";
            this.rdoOutputType.Location = new System.Drawing.Point(0xe0, 0x10);
            this.rdoOutputType.Name = "rdoOutputType";
            this.rdoOutputType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoOutputType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoOutputType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoOutputType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "是"), new RadioGroupItem(null, "否") });
            this.rdoOutputType.Size = new Size(0xb8, 0x20);
            this.rdoOutputType.TabIndex = 0;
            this.rdoOutputType.SelectedIndexChanged += new EventHandler(this.rdoOutputType_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.rdoBufferType);
            this.groupBox2.Location = new System.Drawing.Point(8, 0x48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1d0, 0x68);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "创建缓冲，使其";
            this.rdoBufferType.Location = new System.Drawing.Point(8, 0x18);
            this.rdoBufferType.Name = "rdoBufferType";
            this.rdoBufferType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoBufferType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoBufferType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoBufferType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在多边形内部和外部"), new RadioGroupItem(null, "只在多边形外部"), new RadioGroupItem(null, "只在多边形内部"), new RadioGroupItem(null, "在多边形外部并包含多边形内部") });
            this.rdoBufferType.Size = new Size(0xe8, 0x48);
            this.rdoBufferType.TabIndex = 0;
            this.rdoBufferType.SelectedIndexChanged += new EventHandler(this.rdoBufferType_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.btnSelectInputFeatures);
            this.groupBox3.Controls.Add(this.txtOutName);
            this.groupBox3.Controls.Add(this.cboEditingPolygonLayer);
            this.groupBox3.Controls.Add(this.rdoNewLayer);
            this.groupBox3.Controls.Add(this.rdoSaveToEditingLayer);
            this.groupBox3.Controls.Add(this.rdoSaveToGraphicLayer);
            this.groupBox3.Location = new System.Drawing.Point(8, 0xc0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1d0, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "如何保存创建的缓冲";
            this.btnSelectInputFeatures.Image = (System.Drawing.Image)resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(0x1a0, 0x58);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 15;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutName.EditValue = "";
            this.txtOutName.Location = new System.Drawing.Point(0x20, 0x58);
            this.txtOutName.Name = "txtOutName";
            this.txtOutName.Size = new Size(0x178, 0x15);
            this.txtOutName.TabIndex = 4;
            this.cboEditingPolygonLayer.EditValue = "";
            this.cboEditingPolygonLayer.Location = new System.Drawing.Point(0xa8, 40);
            this.cboEditingPolygonLayer.Name = "cboEditingPolygonLayer";
            this.cboEditingPolygonLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboEditingPolygonLayer.Size = new Size(0xe8, 0x15);
            this.cboEditingPolygonLayer.TabIndex = 3;
            this.cboEditingPolygonLayer.SelectedIndexChanged += new EventHandler(this.cboEditingPolygonLayer_SelectedIndexChanged);
            this.rdoNewLayer.Location = new System.Drawing.Point(8, 0x40);
            this.rdoNewLayer.Name = "rdoNewLayer";
            this.rdoNewLayer.Size = new Size(320, 0x18);
            this.rdoNewLayer.TabIndex = 2;
            this.rdoNewLayer.Text = "创建一个新的图层，指定输出的shape文件或要素类";
            this.rdoNewLayer.Click += new EventHandler(this.rdoNewLayer_Click);
            this.rdoSaveToEditingLayer.Location = new System.Drawing.Point(8, 40);
            this.rdoSaveToEditingLayer.Name = "rdoSaveToEditingLayer";
            this.rdoSaveToEditingLayer.Size = new Size(160, 0x18);
            this.rdoSaveToEditingLayer.TabIndex = 1;
            this.rdoSaveToEditingLayer.Text = "添加到可编辑要素类中";
            this.rdoSaveToEditingLayer.Click += new EventHandler(this.rdoSaveToEditingLayer_Click);
            this.rdoSaveToGraphicLayer.Checked = true;
            this.rdoSaveToGraphicLayer.Location = new System.Drawing.Point(8, 0x10);
            this.rdoSaveToGraphicLayer.Name = "rdoSaveToGraphicLayer";
            this.rdoSaveToGraphicLayer.Size = new Size(0xb0, 0x18);
            this.rdoSaveToGraphicLayer.TabIndex = 0;
            this.rdoSaveToGraphicLayer.TabStop = true;
            this.rdoSaveToGraphicLayer.Text = "作为数据框中的图形图层";
            this.rdoSaveToGraphicLayer.Click += new EventHandler(this.rdoSaveToGraphicLayer_Click);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BufferOutputSetCtrl";
            base.Size = new Size(520, 0x150);
            base.Load += new EventHandler(this.BufferOutputSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rdoOutputType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.rdoBufferType.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtOutName.Properties.EndInit();
            this.cboEditingPolygonLayer.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private bool method_0(IFeatureLayer ifeatureLayer_0)
        {
            return this.method_1(ifeatureLayer_0.FeatureClass as IDataset);
        }

        private bool method_1(IDataset idataset_0)
        {
            IWorkspace workspace = idataset_0.Workspace;
            if (!(workspace as IWorkspaceEdit).IsBeingEdited())
            {
                return false;
            }
            if ((workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) && (workspace is IVersionedWorkspace))
            {
                IVersionedObject obj2 = idataset_0 as IVersionedObject;
                if (obj2.IsRegisteredAsVersioned)
                {
                    return (((AppConfigInfo.UserID.Length == 0) || (AppConfigInfo.UserID.ToLower() == "admin")) || this.sysGrants_0.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 2, idataset_0.Name));
                }
            }
            return true;
        }

        private void rdoBufferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.m_PolygonType = this.rdoOutputType.SelectedIndex;
        }

        private void rdoNewLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = false;
            BufferHelper.m_BufferHelper.m_OutputType = 2;
            this.txtOutName.Enabled = true;
            this.btnSelectInputFeatures.Enabled = true;
            this.cboEditingPolygonLayer.Enabled = false;
        }

        private void rdoOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BufferHelper.m_BufferHelper.bDissolve = this.rdoOutputType.SelectedIndex == 0;
        }

        private void rdoSaveToEditingLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = true;
            BufferHelper.m_BufferHelper.m_OutputType = 1;
            this.txtOutName.Enabled = false;
            this.btnSelectInputFeatures.Enabled = false;
            if (this.cboEditingPolygonLayer.SelectedIndex != -1)
            {
                BufferHelper.m_BufferHelper.m_pOutFC = ((this.cboEditingPolygonLayer.SelectedItem as ObjectWrap).Object as IFeatureLayer).FeatureClass;
            }
        }

        private void rdoSaveToGraphicLayer_Click(object sender, EventArgs e)
        {
            this.cboEditingPolygonLayer.Enabled = false;
            BufferHelper.m_BufferHelper.m_OutputType = 0;
            this.txtOutName.Enabled = false;
            this.btnSelectInputFeatures.Enabled = false;
            this.cboEditingPolygonLayer.Enabled = false;
        }
    }
}

