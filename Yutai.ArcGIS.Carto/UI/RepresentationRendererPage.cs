using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Controls.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class RepresentationRendererPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IRepresentationClass irepresentationClass_0 = null;
        private IRepresentationRenderer irepresentationRenderer_0 = null;
        private IRepresentationWorkspaceExtension irepresentationWorkspaceExtension_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private RepresationRuleCtrl represationRuleCtrl_0 = new RepresationRuleCtrl();

        public RepresentationRendererPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            IObjectCopy copy = new ObjectCopyClass();
            IRepresentationRenderer renderer = copy.Copy(this.irepresentationRenderer_0) as IRepresentationRenderer;
            this.igeoFeatureLayer_0.Renderer = renderer as IFeatureRenderer;
        }

 private void method_0()
        {
        }

        private void RepresentationRendererPage_Load(object sender, EventArgs e)
        {
            this.represationRuleCtrl_0.CanEdit = false;
            this.represationRuleCtrl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.represationRuleCtrl_0);
            this.bool_0 = true;
        }

        private void representationruleListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RepresentationruleListBox.RepresentationRuleWrap selectedItem = this.representationruleListBox1.SelectedItem as RepresentationruleListBox.RepresentationRuleWrap;
            if (selectedItem != null)
            {
                this.represationRuleCtrl_0.RepresentationRule = selectedItem.RepresentationRule;
                this.represationRuleCtrl_0.Init();
            }
            else
            {
                this.represationRuleCtrl_0.RepresentationRule = null;
                this.represationRuleCtrl_0.Init();
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.igeoFeatureLayer_0 == null)
                {
                    this.irepresentationRenderer_0 = null;
                }
                else
                {
                    if (this.igeoFeatureLayer_0.FeatureClass != null)
                    {
                        this.representationruleListBox1.GeometryType = this.igeoFeatureLayer_0.FeatureClass.ShapeType;
                        this.represationRuleCtrl_0.GeometryType = this.igeoFeatureLayer_0.FeatureClass.ShapeType;
                    }
                    IRepresentationRenderer pInObject = this.igeoFeatureLayer_0.Renderer as IRepresentationRenderer;
                    if (pInObject == null)
                    {
                        if (this.irepresentationRenderer_0 == null)
                        {
                            this.irepresentationRenderer_0 = new RepresentationRendererClass();
                            if (this.irepresentationClass_0 == null)
                            {
                                IFeatureClass featureClass = this.igeoFeatureLayer_0.FeatureClass;
                                IRepresentationWorkspaceExtension repWSExtFromFClass = RepresentationAssist.GetRepWSExtFromFClass(featureClass);
                                IDatasetName name2 = repWSExtFromFClass.get_FeatureClassRepresentationNames(featureClass).Next();
                                this.irepresentationClass_0 = repWSExtFromFClass.OpenRepresentationClass(name2.Name);
                            }
                            this.irepresentationRenderer_0.RepresentationClass = this.irepresentationClass_0;
                            this.representationruleListBox1.Init(this.irepresentationClass_0.RepresentationRules);
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.irepresentationRenderer_0 = copy.Copy(pInObject) as IRepresentationRenderer;
                        this.irepresentationClass_0 = this.irepresentationRenderer_0.RepresentationClass;
                        this.representationruleListBox1.Init(this.irepresentationRenderer_0.RepresentationClass.RepresentationRules);
                    }
                    if (this.bool_0)
                    {
                        this.method_0();
                    }
                }
            }
        }

        bool IUserControl.Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        public IRepresentationClass RepresentationClass
        {
            get
            {
                return this.irepresentationClass_0;
            }
            set
            {
                this.irepresentationClass_0 = value;
            }
        }

        public string RepresentationClassName
        {
            set
            {
                this.irepresentationClass_0 = this.irepresentationWorkspaceExtension_0.OpenRepresentationClass(value);
                this.irepresentationRenderer_0.RepresentationClass = this.irepresentationClass_0;
                this.representationruleListBox1.Init(this.irepresentationClass_0.RepresentationRules);
            }
        }

        public IRepresentationWorkspaceExtension RepresentationWorkspaceExtension
        {
            set
            {
                this.irepresentationWorkspaceExtension_0 = value;
            }
        }
    }
}

