using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Cursor = System.Windows.Forms.Cursor;
using Point = System.Drawing.Point;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public class frmLabelToAnnoSetup : Form
    {
        private double double_0 = 0;

        private esriLabelWhichFeatures esriLabelWhichFeatures_0 = esriLabelWhichFeatures.esriAllFeatures;

        private GroupBox groupBox1;

        private GroupBox groupBox2;

        private Label lblRefrencesScale;

        private RadioGroup rdoSaveType;

        private GroupBox groupBox3;

        private RadioGroup radioGroup2;

        private Label label1;

        private Label label2;

        private Label label3;

        private TextEdit txtFeatLayer;

        private CheckEdit chkFeatureLinked;

        private TextEdit txtAnnoName;

        private TextEdit txtSavePos;

        private SimpleButton btnConvert;

        private SimpleButton btnCancel;

        private Container container_0 = null;

        private IWorkspace iworkspace_0 = null;

        private IMap imap_0 = null;

        private IFeatureLayer ifeatureLayer_0 = null;

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

        public frmLabelToAnnoSetup()
        {
            if (CartoLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.ConvertAnnotation(this.imap_0, this.ifeatureLayer_0, this.txtAnnoName.Text, this.double_0, this.iworkspace_0, true, this.chkFeatureLinked.Checked, this.esriLabelWhichFeatures_0);
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
            Cursor.Current = Cursors.Default;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && this.container_0 != null)
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoSaveType = new DevExpress.XtraEditors.RadioGroup();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRefrencesScale = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFeatLayer = new DevExpress.XtraEditors.TextEdit();
            this.chkFeatureLinked = new DevExpress.XtraEditors.CheckEdit();
            this.txtAnnoName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSavePos = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConvert = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSaveType.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeatLayer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFeatureLinked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnnoName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSavePos.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoSaveType);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "保存注记";
            // 
            // rdoSaveType
            // 
            this.rdoSaveType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdoSaveType.Location = new System.Drawing.Point(3, 17);
            this.rdoSaveType.Name = "rdoSaveType";
            this.rdoSaveType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.rdoSaveType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoSaveType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.rdoSaveType.Properties.Columns = 2;
            this.rdoSaveType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "在数据库中"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "在地图中")});
            this.rdoSaveType.Size = new System.Drawing.Size(186, 36);
            this.rdoSaveType.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRefrencesScale);
            this.groupBox2.Location = new System.Drawing.Point(224, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参考比例";
            // 
            // lblRefrencesScale
            // 
            this.lblRefrencesScale.Location = new System.Drawing.Point(8, 16);
            this.lblRefrencesScale.Name = "lblRefrencesScale";
            this.lblRefrencesScale.Size = new System.Drawing.Size(160, 24);
            this.lblRefrencesScale.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioGroup2);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(8, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(408, 49);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "创建注记要素";
            // 
            // radioGroup2
            // 
            this.radioGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup2.Location = new System.Drawing.Point(3, 17);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.radioGroup2.Properties.Columns = 3;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "所有要素"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "当前范围内的要素"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "选中的要素")});
            this.radioGroup2.Size = new System.Drawing.Size(402, 29);
            this.radioGroup2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "要 素 图 层";
            // 
            // txtFeatLayer
            // 
            this.txtFeatLayer.EditValue = "";
            this.txtFeatLayer.Location = new System.Drawing.Point(96, 152);
            this.txtFeatLayer.Name = "txtFeatLayer";
            this.txtFeatLayer.Properties.ReadOnly = true;
            this.txtFeatLayer.Size = new System.Drawing.Size(192, 20);
            this.txtFeatLayer.TabIndex = 4;
            // 
            // chkFeatureLinked
            // 
            this.chkFeatureLinked.Location = new System.Drawing.Point(320, 152);
            this.chkFeatureLinked.Name = "chkFeatureLinked";
            this.chkFeatureLinked.Properties.Caption = "与要素关联";
            this.chkFeatureLinked.Size = new System.Drawing.Size(96, 19);
            this.chkFeatureLinked.TabIndex = 5;
            // 
            // txtAnnoName
            // 
            this.txtAnnoName.EditValue = "";
            this.txtAnnoName.Location = new System.Drawing.Point(96, 184);
            this.txtAnnoName.Name = "txtAnnoName";
            this.txtAnnoName.Size = new System.Drawing.Size(192, 20);
            this.txtAnnoName.TabIndex = 7;
            this.txtAnnoName.TextChanged += new System.EventHandler(this.txtAnnoName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "注记要素类名";
            // 
            // txtSavePos
            // 
            this.txtSavePos.EditValue = "";
            this.txtSavePos.Location = new System.Drawing.Point(96, 216);
            this.txtSavePos.Name = "txtSavePos";
            this.txtSavePos.Properties.ReadOnly = true;
            this.txtSavePos.Size = new System.Drawing.Size(192, 20);
            this.txtSavePos.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "保 存 位 置";
            // 
            // btnConvert
            // 
            this.btnConvert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConvert.Location = new System.Drawing.Point(216, 248);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(64, 24);
            this.btnConvert.TabIndex = 10;
            this.btnConvert.Text = "转换";
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            // 
            // frmLabelToAnnoSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(434, 284);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtSavePos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAnnoName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkFeatureLinked);
            this.Controls.Add(this.txtFeatLayer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLabelToAnnoSetup";
            this.Text = "标注转换为注记";
            this.Load += new System.EventHandler(this.frmLabelToAnnoSetup_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoSaveType.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFeatLayer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFeatureLinked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnnoName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSavePos.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void method_0()
        {
            if (this.ifeatureLayer_0 != null)
            {
                this.txtFeatLayer.Text = this.ifeatureLayer_0.Name;
                this.iworkspace_0 = (this.ifeatureLayer_0.FeatureClass as IDataset).Workspace;
                string str = string.Concat(this.txtFeatLayer.Text, "_Anno");
                this.txtAnnoName.Text = WorkspaceOperator.GetFinalName(this.iworkspace_0, esriDatasetType.esriDTFeatureClass, str);
                this.txtSavePos.Text = string.Concat(this.iworkspace_0.PathName, "\\", this.txtAnnoName.Text);
                this.double_0 = this.method_1();
                this.lblRefrencesScale.Text = string.Concat("1:", this.double_0.ToString());
            }
            else
            {
                this.btnConvert.Enabled = false;
            }
        }

        private double method_1()
        {
            double referenceScale;
            if (this.imap_0 != null)
            {
                try
                {
                    if (this.imap_0.ReferenceScale != 0)
                    {
                        referenceScale = this.imap_0.ReferenceScale;
                        return referenceScale;
                    }
                    else
                    {
                        referenceScale = this.imap_0.MapScale;
                        return referenceScale;
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(this, exception, null);
                }
                referenceScale = 0;
            }
            else
            {
                referenceScale = 0;
            }
            return referenceScale;
        }

        private IFeatureClass CreateAnnoFeatureClass(IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0, IFeatureDataset ifeatureDataset_0, IFeatureClass ifeatureClass_0, double double_1, esriUnits esriUnits_0, IAnnotateLayerPropertiesCollection iannotateLayerPropertiesCollection_0, ISymbolCollection isymbolCollection_0, string string_0)
        {
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescriptionClass();
            IFields requiredFields = annotationFeatureClassDescriptionClass.RequiredFields;
            int num = requiredFields.FindField((annotationFeatureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
            if (num != -1)
            {
                IField field = requiredFields.Field[num];
                IGeometryDef geometryDef = field.GeometryDef;
                ISpatialReference spatialReference = geometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease, spatialReference, false);
                (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                (field as IFieldEdit).GeometryDef_2 = geometryDef;
            }
            IFeatureClassDescription featureClassDescription = annotationFeatureClassDescriptionClass as IFeatureClassDescription;
            IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScaleClass()
            {
                ReferenceScale = double_1,
                Units = esriUnits_0
            };
            UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
            UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
            IFeatureClass featureClass = ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, requiredFields, instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_0, ifeatureClass_0, iannotateLayerPropertiesCollection_0, graphicsLayerScaleClass, isymbolCollection_0, true);
            return featureClass;
        }

        private void ConvertAnnotation(IMap imap_1, ILayer ilayer_0, string string_0, double double_1, IWorkspace iworkspace_1, bool bool_0, bool bool_1, esriLabelWhichFeatures esriLabelWhichFeatures_1)
        {
            int i;
            IAnnotateLayerProperties bool0;
            IElementCollection elementCollection;
            IElementCollection elementCollection1;
            ILabelEngineLayerProperties2 d;
            ISymbolIdentifier2 symbolIdentifier2;
            IAnnotationLayer annotationLayer;
            if (iworkspace_1.Type != esriWorkspaceType.esriFileSystemWorkspace && ilayer_0 is IGeoFeatureLayer)
            {
                IGeoFeatureLayer ilayer0 = ilayer_0 as IGeoFeatureLayer;
                IFeatureClass featureClass = ilayer0.FeatureClass;
                IAnnotationLayerFactory fDOGraphicsLayerFactoryClass = new FDOGraphicsLayerFactoryClass();
                ISymbolCollection2 symbolCollectionClass = new SymbolCollectionClass();
                IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass = new AnnotateLayerPropertiesCollectionClass();
                IAnnotateLayerPropertiesCollection annotationProperties = ilayer0.AnnotationProperties;
                for (i = 0; i < annotationProperties.Count; i++)
                {
                    annotationProperties.QueryItem(i, out bool0, out elementCollection, out elementCollection1);
                    if (bool0 != null)
                    {
                        annotateLayerPropertiesCollectionClass.Add(bool0);
                        d = bool0 as ILabelEngineLayerProperties2;
                        IClone symbol = d.Symbol as IClone;
                        symbolCollectionClass.AddSymbol(symbol.Clone() as ISymbol, string.Concat(bool0.Class, " ", i.ToString()), out symbolIdentifier2);
                        d.SymbolID = symbolIdentifier2.ID;
                    }
                }
                bool0 = null;
                d = null;
                IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScaleClass()
                {
                    ReferenceScale = double_1,
                    Units = imap_1.MapUnits
                };
                IFeatureClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescriptionClass();
                IFields requiredFields = (annotationFeatureClassDescriptionClass as IObjectClassDescription).RequiredFields;
                IField field = requiredFields.Field[requiredFields.FindField(annotationFeatureClassDescriptionClass.ShapeFieldName)];
                (field.GeometryDef as IGeometryDefEdit).SpatialReference_2 = (featureClass as IGeoDataset).SpatialReference;
                IOverposterProperties overposterProperties = (imap_1 as IMapOverposter).OverposterProperties;
                if (!bool_1)
                {
                    this.CreateAnnoFeatureClass(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, null, graphicsLayerScaleClass.ReferenceScale, graphicsLayerScaleClass.Units, annotateLayerPropertiesCollectionClass, symbolCollectionClass as ISymbolCollection, string_0);
                    annotationLayer = fDOGraphicsLayerFactoryClass.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                else
                {
                    this.CreateAnnoFeatureClass(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, featureClass, graphicsLayerScaleClass.ReferenceScale, graphicsLayerScaleClass.Units, annotateLayerPropertiesCollectionClass, symbolCollectionClass as ISymbolCollection, string_0);
                    annotationLayer = fDOGraphicsLayerFactoryClass.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                IActiveView imap1 = imap_1 as IActiveView;
                (annotationLayer as IGraphicsLayer).Activate(imap1.ScreenDisplay);
                for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                {
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out bool0, out elementCollection, out elementCollection1);
                    if (bool0 != null)
                    {
                        bool0.FeatureLayer = ilayer0;
                        bool0.GraphicsContainer = annotationLayer as IGraphicsContainer;
                        bool0.AddUnplacedToGraphicsContainer = bool_0;
                        bool0.CreateUnplacedElements = true;
                        bool0.DisplayAnnotation = true;
                        bool0.FeatureLinked = bool_1;
                        bool0.LabelWhichFeatures = esriLabelWhichFeatures_1;
                        bool0.UseOutput = true;
                        d = bool0 as ILabelEngineLayerProperties2;
                        d.SymbolID = i;
                        d.AnnotationClassID = i;
                        (d.OverposterLayerProperties as IOverposterLayerProperties2).TagUnplaced = true;
                    }
                }
                annotateLayerPropertiesCollectionClass.Sort();
                IAnnotateMapProperties annotateMapPropertiesClass = new AnnotateMapPropertiesClass()
                {
                    AnnotateLayerPropertiesCollection = annotateLayerPropertiesCollectionClass
                };
                ITrackCancel cancelTrackerClass = new CancelTrackerClass();
                (imap_1.AnnotationEngine as IAnnotateMap2).Label(overposterProperties, annotateMapPropertiesClass, imap_1, cancelTrackerClass);
                for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                {
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out bool0, out elementCollection, out elementCollection1);
                    if (bool0 != null)
                    {
                        bool0.FeatureLayer = null;
                    }
                }
                imap_1.AddLayer(annotationLayer as ILayer);
                ilayer0.DisplayAnnotation = false;
                imap1.Refresh();
            }
        }

        private void txtAnnoName_TextChanged(object sender, EventArgs e)
        {
            this.txtSavePos.Text = string.Concat(this.iworkspace_0.PathName, "\\", this.txtAnnoName.Text);
        }
    }
}
