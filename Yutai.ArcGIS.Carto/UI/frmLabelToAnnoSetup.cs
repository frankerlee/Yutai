using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmLabelToAnnoSetup : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnConvert;
        private CheckEdit chkFeatureLinked;
        private Container container_0 = null;
        private double double_0 = 0.0;
        private esriLabelWhichFeatures esriLabelWhichFeatures_0 = esriLabelWhichFeatures.esriAllFeatures;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IFeatureLayer ifeatureLayer_0 = null;
        private IMap imap_0 = null;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblRefrencesScale;
        private RadioGroup radioGroup2;
        private RadioGroup rdoSaveType;
        private TextEdit txtAnnoName;
        private TextEdit txtFeatLayer;
        private TextEdit txtSavePos;

        public frmLabelToAnnoSetup()
        {
            if (CartoLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.method_3(this.imap_0, this.ifeatureLayer_0, this.txtAnnoName.Text, this.double_0, this.iworkspace_0, true, this.chkFeatureLinked.Checked, this.esriLabelWhichFeatures_0);
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmLabelToAnnoSetup_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelToAnnoSetup));
            this.groupBox1 = new GroupBox();
            this.rdoSaveType = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.lblRefrencesScale = new Label();
            this.groupBox3 = new GroupBox();
            this.radioGroup2 = new RadioGroup();
            this.label1 = new Label();
            this.txtFeatLayer = new TextEdit();
            this.chkFeatureLinked = new CheckEdit();
            this.txtAnnoName = new TextEdit();
            this.label2 = new Label();
            this.txtSavePos = new TextEdit();
            this.label3 = new Label();
            this.btnConvert = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.rdoSaveType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.radioGroup2.Properties.BeginInit();
            this.txtFeatLayer.Properties.BeginInit();
            this.chkFeatureLinked.Properties.BeginInit();
            this.txtAnnoName.Properties.BeginInit();
            this.txtSavePos.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoSaveType);
            this.groupBox1.Location = new System.Drawing.Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xc0, 0x38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "保存注记";
            this.rdoSaveType.Location = new System.Drawing.Point(8, 0x18);
            this.rdoSaveType.Name = "rdoSaveType";
            this.rdoSaveType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoSaveType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoSaveType.Properties.BorderStyle = BorderStyles.Office2003;
            this.rdoSaveType.Properties.Columns = 2;
            this.rdoSaveType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在数据库中"), new RadioGroupItem(null, "在地图中") });
            this.rdoSaveType.Size = new Size(0xb0, 0x18);
            this.rdoSaveType.TabIndex = 0;
            this.groupBox2.Controls.Add(this.lblRefrencesScale);
            this.groupBox2.Location = new System.Drawing.Point(0xe0, 0x10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc0, 0x38);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参考比例";
            this.lblRefrencesScale.Location = new System.Drawing.Point(8, 0x10);
            this.lblRefrencesScale.Name = "lblRefrencesScale";
            this.lblRefrencesScale.Size = new Size(160, 0x18);
            this.lblRefrencesScale.TabIndex = 0;
            this.groupBox3.Controls.Add(this.radioGroup2);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(8, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x198, 0x40);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "创建注记要素";
            this.radioGroup2.Location = new System.Drawing.Point(8, 0x18);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = BorderStyles.Office2003;
            this.radioGroup2.Properties.Columns = 3;
            this.radioGroup2.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "所有要素"), new RadioGroupItem(null, "当前范围内的要素"), new RadioGroupItem(null, "选中的要素") });
            this.radioGroup2.Size = new Size(0x180, 0x18);
            this.radioGroup2.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 160);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "要 素 图 层";
            this.txtFeatLayer.EditValue = "";
            this.txtFeatLayer.Location = new System.Drawing.Point(0x60, 0x98);
            this.txtFeatLayer.Name = "txtFeatLayer";
            this.txtFeatLayer.Properties.ReadOnly = true;
            this.txtFeatLayer.Size = new Size(0xc0, 0x15);
            this.txtFeatLayer.TabIndex = 4;
            this.chkFeatureLinked.Location = new System.Drawing.Point(320, 0x98);
            this.chkFeatureLinked.Name = "chkFeatureLinked";
            this.chkFeatureLinked.Properties.Caption = "与要素关联";
            this.chkFeatureLinked.Size = new Size(0x60, 0x13);
            this.chkFeatureLinked.TabIndex = 5;
            this.txtAnnoName.EditValue = "";
            this.txtAnnoName.Location = new System.Drawing.Point(0x60, 0xb8);
            this.txtAnnoName.Name = "txtAnnoName";
            this.txtAnnoName.Size = new Size(0xc0, 0x15);
            this.txtAnnoName.TabIndex = 7;
            this.txtAnnoName.TextChanged += new EventHandler(this.txtAnnoName_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0xb8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "注记要素类名";
            this.txtSavePos.EditValue = "";
            this.txtSavePos.Location = new System.Drawing.Point(0x60, 0xd8);
            this.txtSavePos.Name = "txtSavePos";
            this.txtSavePos.Properties.ReadOnly = true;
            this.txtSavePos.Size = new Size(0xc0, 0x15);
            this.txtSavePos.TabIndex = 9;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0xe0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x47, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "保 存 位 置";
            this.btnConvert.DialogResult = DialogResult.OK;
            this.btnConvert.Location = new System.Drawing.Point(0xd8, 0xf8);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(0x40, 0x18);
            this.btnConvert.TabIndex = 10;
            this.btnConvert.Text = "转换";
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x130, 0xf8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1b2, 0x11c);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.txtSavePos);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtAnnoName);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkFeatureLinked);
            base.Controls.Add(this.txtFeatLayer);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLabelToAnnoSetup";
            this.Text = "标注转换为注记";
            base.Load += new EventHandler(this.frmLabelToAnnoSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.rdoSaveType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.radioGroup2.Properties.EndInit();
            this.txtFeatLayer.Properties.EndInit();
            this.chkFeatureLinked.Properties.EndInit();
            this.txtAnnoName.Properties.EndInit();
            this.txtSavePos.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            if (this.ifeatureLayer_0 == null)
            {
                this.btnConvert.Enabled = false;
            }
            else
            {
                this.txtFeatLayer.Text = this.ifeatureLayer_0.Name;
                this.iworkspace_0 = (this.ifeatureLayer_0.FeatureClass as IDataset).Workspace;
                string str = this.txtFeatLayer.Text + "_Anno";
                this.txtAnnoName.Text = WorkspaceOperator.GetFinalName(this.iworkspace_0, esriDatasetType.esriDTFeatureClass, str);
                this.txtSavePos.Text = this.iworkspace_0.PathName + @"\" + this.txtAnnoName.Text;
                this.double_0 = this.method_1();
                this.lblRefrencesScale.Text = "1:" + this.double_0.ToString();
            }
        }

        private double method_1()
        {
            if (this.imap_0 != null)
            {
                try
                {
                    if (this.imap_0.ReferenceScale == 0.0)
                    {
                        return this.imap_0.MapScale;
                    }
                    return this.imap_0.ReferenceScale;
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, null);
                }
            }
            return 0.0;
        }

        private IFeatureClass method_2(IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0, IFeatureDataset ifeatureDataset_0, IFeatureClass ifeatureClass_0, double double_1, esriUnits esriUnits_0, IAnnotateLayerPropertiesCollection iannotateLayerPropertiesCollection_0, ISymbolCollection isymbolCollection_0, string string_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescriptionClass();
            IFields requiredFields = description.RequiredFields;
            int index = requiredFields.FindField((description as IFeatureClassDescription).ShapeFieldName);
            if (index != -1)
            {
                IField field = requiredFields.get_Field(index);
                IGeometryDef geometryDef = field.GeometryDef;
                ISpatialReference spatialReference = geometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease, spatialReference, false);
                (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                (field as IFieldEdit).GeometryDef_2 = geometryDef;
            }
            IFeatureClassDescription description2 = description as IFeatureClassDescription;
            IGraphicsLayerScale referenceScale = new GraphicsLayerScaleClass {
                ReferenceScale = double_1,
                Units = esriUnits_0
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            return ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, requiredFields, instanceCLSID, classExtensionCLSID, description2.ShapeFieldName, "", ifeatureDataset_0, ifeatureClass_0, iannotateLayerPropertiesCollection_0, referenceScale, isymbolCollection_0, true);
        }

        private void method_3(IMap imap_1, ILayer ilayer_0, string string_0, double double_1, IWorkspace iworkspace_1, bool bool_0, bool bool_1, esriLabelWhichFeatures esriLabelWhichFeatures_1)
        {
            if ((iworkspace_1.Type != esriWorkspaceType.esriFileSystemWorkspace) && (ilayer_0 is IGeoFeatureLayer))
            {
                int num;
                IAnnotateLayerProperties properties;
                IElementCollection elements;
                IElementCollection elements2;
                ILabelEngineLayerProperties2 properties2;
                IAnnotationLayer layer2;
                IGeoFeatureLayer layer = ilayer_0 as IGeoFeatureLayer;
                IFeatureClass featureClass = layer.FeatureClass;
                IAnnotationLayerFactory factory = new FDOGraphicsLayerFactoryClass();
                ISymbolCollection2 lcs = new SymbolCollectionClass();
                IAnnotateLayerPropertiesCollection propertiess = new AnnotateLayerPropertiesCollectionClass();
                IAnnotateLayerPropertiesCollection annotationProperties = layer.AnnotationProperties;
                for (num = 0; num < annotationProperties.Count; num++)
                {
                    annotationProperties.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        ISymbolIdentifier2 identifier;
                        propertiess.Add(properties);
                        properties2 = properties as ILabelEngineLayerProperties2;
                        IClone symbol = properties2.Symbol as IClone;
                        lcs.AddSymbol(symbol.Clone() as ISymbol, properties.Class + " " + num.ToString(), out identifier);
                        properties2.SymbolID = identifier.ID;
                    }
                }
                properties = null;
                properties2 = null;
                IGraphicsLayerScale scale = new GraphicsLayerScaleClass {
                    ReferenceScale = double_1,
                    Units = imap_1.MapUnits
                };
                IFeatureClassDescription description = new AnnotationFeatureClassDescriptionClass();
                IObjectClassDescription description2 = description as IObjectClassDescription;
                IFields requiredFields = description2.RequiredFields;
                IGeometryDefEdit geometryDef = requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName)).GeometryDef as IGeometryDefEdit;
                IGeoDataset dataset = featureClass as IGeoDataset;
                geometryDef.SpatialReference_2 = dataset.SpatialReference;
                IMapOverposter overposter = imap_1 as IMapOverposter;
                IOverposterProperties overposterProperties = overposter.OverposterProperties;
                if (bool_1)
                {
                    this.method_2(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, featureClass, scale.ReferenceScale, scale.Units, propertiess, lcs as ISymbolCollection, string_0);
                    layer2 = factory.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                else
                {
                    this.method_2(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, null, scale.ReferenceScale, scale.Units, propertiess, lcs as ISymbolCollection, string_0);
                    layer2 = factory.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                IActiveView view = imap_1 as IActiveView;
                IScreenDisplay screenDisplay = view.ScreenDisplay;
                (layer2 as IGraphicsLayer).Activate(screenDisplay);
                for (num = 0; num < propertiess.Count; num++)
                {
                    propertiess.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        properties.FeatureLayer = layer;
                        properties.GraphicsContainer = layer2 as IGraphicsContainer;
                        properties.AddUnplacedToGraphicsContainer = bool_0;
                        properties.CreateUnplacedElements = true;
                        properties.DisplayAnnotation = true;
                        properties.FeatureLinked = bool_1;
                        properties.LabelWhichFeatures = esriLabelWhichFeatures_1;
                        properties.UseOutput = true;
                        properties2 = properties as ILabelEngineLayerProperties2;
                        properties2.SymbolID = num;
                        properties2.AnnotationClassID = num;
                        IOverposterLayerProperties2 overposterLayerProperties = properties2.OverposterLayerProperties as IOverposterLayerProperties2;
                        overposterLayerProperties.TagUnplaced = true;
                    }
                }
                propertiess.Sort();
                IAnnotateMapProperties annoMapCmdProps = new AnnotateMapPropertiesClass {
                    AnnotateLayerPropertiesCollection = propertiess
                };
                ITrackCancel trackCancel = new CancelTrackerClass();
                (imap_1.AnnotationEngine as IAnnotateMap2).Label(overposterProperties, annoMapCmdProps, imap_1, trackCancel);
                for (num = 0; num < propertiess.Count; num++)
                {
                    propertiess.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        properties.FeatureLayer = null;
                    }
                }
                imap_1.AddLayer(layer2 as ILayer);
                layer.DisplayAnnotation = false;
                view.Refresh();
            }
        }

        private void txtAnnoName_TextChanged(object sender, EventArgs e)
        {
            this.txtSavePos.Text = this.iworkspace_0.PathName + @"\" + this.txtAnnoName.Text;
        }

        public ILayer Layer
        {
            set
            {
                this.ifeatureLayer_0 = value as IFeatureLayer;
            }
        }

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

