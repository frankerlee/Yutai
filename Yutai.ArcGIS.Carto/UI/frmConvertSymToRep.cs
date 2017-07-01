using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmConvertSymToRep : Form
    {
        private IContainer icontainer_0 = null;
        private IFeatureLayer ifeatureLayer_0 = null;
        private IMap imap_0 = null;

        public frmConvertSymToRep()
        {
            this.InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (this.txtRepresentationName.Text.Trim().Length == 0)
            {
                MessageBox.Show("名字不能为空!");
            }
            else if (this.txtruleIDFldName.Text.Trim().Length == 0)
            {
                MessageBox.Show("RuleID字段不能为空!");
            }
            else if (this.txtoverrideFldName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Override字段不能为空!");
            }
            else
            {
                try
                {
                    IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
                    IRepresentationWorkspaceExtension repWSExtFromFClass =
                        RepresentationAssist.GetRepWSExtFromFClass(featureClass);
                    if (repWSExtFromFClass != null)
                    {
                        IRepresentationRule repRule = RepresentationAssist.CreateRepresentationRule(featureClass);
                        IRepresentationRules rules = new RepresentationRulesClass();
                        rules.Add(repRule);
                        IRepresentationClass class3 = repWSExtFromFClass.CreateRepresentationClass(featureClass,
                            this.txtRepresentationName.Text, this.txtruleIDFldName.Text, this.txtoverrideFldName.Text,
                            this.rdoRequireShapeOverride.Checked, rules, null);
                        if ((this.imap_0 != null) && this.chkAddLayer.Checked)
                        {
                            IFeatureLayer layer = new FeatureLayerClass
                            {
                                FeatureClass = featureClass
                            };
                            IFeatureRenderer renderer = new RepresentationRendererClass();
                            (renderer as IRepresentationRenderer).RepresentationClass = class3;
                            (layer as IGeoFeatureLayer).Renderer = renderer;
                            this.imap_0.AddLayer(layer);
                            (this.imap_0 as IActiveView).Refresh();
                        }
                        base.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                }
                catch (COMException exception)
                {
                    switch (exception.ErrorCode)
                    {
                        case -2147218682:
                            MessageBox.Show(exception.Message);
                            return;

                        case -2147218675:
                            MessageBox.Show("该要素类中已存在同名的制图表现");
                            break;

                        case -2147218674:
                            MessageBox.Show("该要素类中已存在同名RuleID字段或Override字段");
                            break;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void frmConvertSymToRep_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
            IRepresentationWorkspaceExtension repWSExtFromFClass =
                RepresentationAssist.GetRepWSExtFromFClass(featureClass);
            string str = this.ifeatureLayer_0.FeatureClass.AliasName + "_Rep";
            string str2 = "RuleID";
            string str3 = "Override";
            if (repWSExtFromFClass.get_FeatureClassHasRepresentations(featureClass))
            {
                IList<string> list = new List<string>();
                IEnumDatasetName name = repWSExtFromFClass.get_FeatureClassRepresentationNames(featureClass);
                for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                {
                    list.Add(name2.Name);
                }
                int num = 1;
                string item = str;
                while (list.IndexOf(item) != -1)
                {
                    item = str + num.ToString();
                    num++;
                }
                str = item;
                num = 1;
                item = str2;
                while (featureClass.FindField(item) != -1)
                {
                    item = str2 + "_" + num.ToString();
                    num++;
                }
                str2 = item;
                num = 1;
                item = str3;
                while (featureClass.FindField(item) != -1)
                {
                    item = str3 + "_" + num.ToString();
                    num++;
                }
                str3 = item;
            }
            this.txtruleIDFldName.Text = str2;
            this.txtoverrideFldName.Text = str3;
            this.txtRepresentationName.Text = str;
            this.lblFC.Text = this.ifeatureLayer_0.FeatureClass.AliasName;
            this.rdoSelectFeature.Enabled = (this.ifeatureLayer_0 as IFeatureSelection).SelectionSet.Count > 0;
        }

        public IFeatureLayer FeatureLayer
        {
            set { this.ifeatureLayer_0 = value; }
        }

        public IMap FocusMap
        {
            set { this.imap_0 = value; }
        }
    }
}