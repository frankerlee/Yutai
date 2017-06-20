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
    public class frmConvertSymToRep : Form
    {
        private Button btnConvert;
        private Button button2;
        private CheckBox chkAddLayer;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0 = null;
        private IFeatureLayer ifeatureLayer_0 = null;
        private IMap imap_0 = null;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label lblFC;
        private RadioButton radioButton2;
        private RadioButton rdoAllFeature;
        private RadioButton rdoRequireShapeOverride;
        private RadioButton rdoSelectFeature;
        private RadioButton rdoVisibleFeature;
        private TextBox txtoverrideFldName;
        private TextBox txtRepresentationName;
        private TextBox txtruleIDFldName;

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
                    IRepresentationWorkspaceExtension repWSExtFromFClass = RepresentationAssist.GetRepWSExtFromFClass(featureClass);
                    if (repWSExtFromFClass != null)
                    {
                        IRepresentationRule repRule = RepresentationAssist.CreateRepresentationRule(featureClass);
                        IRepresentationRules rules = new RepresentationRulesClass();
                        rules.Add(repRule);
                        IRepresentationClass class3 = repWSExtFromFClass.CreateRepresentationClass(featureClass, this.txtRepresentationName.Text, this.txtruleIDFldName.Text, this.txtoverrideFldName.Text, this.rdoRequireShapeOverride.Checked, rules, null);
                        if ((this.imap_0 != null) && this.chkAddLayer.Checked)
                        {
                            IFeatureLayer layer = new FeatureLayerClass {
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmConvertSymToRep_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConvertSymToRep));
            this.label1 = new Label();
            this.txtRepresentationName = new TextBox();
            this.label2 = new Label();
            this.lblFC = new Label();
            this.txtruleIDFldName = new TextBox();
            this.label4 = new Label();
            this.txtoverrideFldName = new TextBox();
            this.label5 = new Label();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.rdoRequireShapeOverride = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.rdoVisibleFeature = new RadioButton();
            this.rdoAllFeature = new RadioButton();
            this.rdoSelectFeature = new RadioButton();
            this.chkAddLayer = new CheckBox();
            this.btnConvert = new Button();
            this.button2 = new Button();
            this.groupBox3 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "制图表现名称:";
            this.txtRepresentationName.Location = new Point(0x5d, 12);
            this.txtRepresentationName.Name = "txtRepresentationName";
            this.txtRepresentationName.Size = new Size(0xaf, 0x15);
            this.txtRepresentationName.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "要素类";
            this.lblFC.AutoSize = true;
            this.lblFC.Location = new Point(0x41, 0x11);
            this.lblFC.Name = "lblFC";
            this.lblFC.Size = new Size(0x29, 12);
            this.lblFC.TabIndex = 3;
            this.lblFC.Text = "label3";
            this.txtruleIDFldName.Location = new Point(0x43, 40);
            this.txtruleIDFldName.Name = "txtruleIDFldName";
            this.txtruleIDFldName.Size = new Size(0xa5, 0x15);
            this.txtruleIDFldName.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x2b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "规则字段";
            this.txtoverrideFldName.Location = new Point(0x43, 0x43);
            this.txtoverrideFldName.Name = "txtoverrideFldName";
            this.txtoverrideFldName.Size = new Size(0xa5, 0x15);
            this.txtoverrideFldName.TabIndex = 7;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 70);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "重载字段";
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.rdoRequireShapeOverride);
            this.groupBox1.Location = new Point(13, 0x93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xff, 70);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "制图几何对象编辑时的行为";
            this.groupBox2.Controls.Add(this.rdoSelectFeature);
            this.groupBox2.Controls.Add(this.rdoVisibleFeature);
            this.groupBox2.Controls.Add(this.rdoAllFeature);
            this.groupBox2.Location = new Point(13, 0xe4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xfe, 90);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "分配制图表现规则";
            this.rdoRequireShapeOverride.AutoSize = true;
            this.rdoRequireShapeOverride.Checked = true;
            this.rdoRequireShapeOverride.Location = new Point(12, 20);
            this.rdoRequireShapeOverride.Name = "rdoRequireShapeOverride";
            this.rdoRequireShapeOverride.Size = new Size(0xef, 0x10);
            this.rdoRequireShapeOverride.TabIndex = 0;
            this.rdoRequireShapeOverride.TabStop = true;
            this.rdoRequireShapeOverride.Text = "存储变化的几何对象作为制图表现的重载";
            this.rdoRequireShapeOverride.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(12, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x83, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "改变要素的几何对象";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.rdoVisibleFeature.AutoSize = true;
            this.rdoVisibleFeature.Location = new Point(0x11, 0x2a);
            this.rdoVisibleFeature.Name = "rdoVisibleFeature";
            this.rdoVisibleFeature.Size = new Size(0x77, 0x10);
            this.rdoVisibleFeature.TabIndex = 3;
            this.rdoVisibleFeature.Text = "可视区域内的要素";
            this.rdoVisibleFeature.UseVisualStyleBackColor = true;
            this.rdoAllFeature.AutoSize = true;
            this.rdoAllFeature.Checked = true;
            this.rdoAllFeature.Location = new Point(0x11, 20);
            this.rdoAllFeature.Name = "rdoAllFeature";
            this.rdoAllFeature.Size = new Size(0x47, 0x10);
            this.rdoAllFeature.TabIndex = 2;
            this.rdoAllFeature.TabStop = true;
            this.rdoAllFeature.Text = "所有要素";
            this.rdoAllFeature.UseVisualStyleBackColor = true;
            this.rdoSelectFeature.AutoSize = true;
            this.rdoSelectFeature.Location = new Point(0x11, 0x40);
            this.rdoSelectFeature.Name = "rdoSelectFeature";
            this.rdoSelectFeature.Size = new Size(0x47, 0x10);
            this.rdoSelectFeature.TabIndex = 4;
            this.rdoSelectFeature.Text = "选择要素";
            this.rdoSelectFeature.UseVisualStyleBackColor = true;
            this.chkAddLayer.AutoSize = true;
            this.chkAddLayer.Location = new Point(13, 0x148);
            this.chkAddLayer.Name = "chkAddLayer";
            this.chkAddLayer.Size = new Size(0xfc, 0x10);
            this.chkAddLayer.TabIndex = 10;
            this.chkAddLayer.Text = "添加使用制图表现符号化的新图层到地图中";
            this.chkAddLayer.UseVisualStyleBackColor = true;
            this.btnConvert.Location = new Point(0x91, 0x164);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new Size(0x37, 0x16);
            this.btnConvert.TabIndex = 11;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new EventHandler(this.btnConvert_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0xda, 0x165);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x35, 20);
            this.button2.TabIndex = 12;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblFC);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtruleIDFldName);
            this.groupBox3.Controls.Add(this.txtoverrideFldName);
            this.groupBox3.Location = new Point(12, 0x27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x100, 0x66);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(290, 0x182);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnConvert);
            base.Controls.Add(this.chkAddLayer);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtRepresentationName);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmConvertSymToRep";
            this.Text = "符号转换为制图表现";
            base.Load += new EventHandler(this.frmConvertSymToRep_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0()
        {
            IFeatureClass featureClass = this.ifeatureLayer_0.FeatureClass;
            IRepresentationWorkspaceExtension repWSExtFromFClass = RepresentationAssist.GetRepWSExtFromFClass(featureClass);
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
            set
            {
                this.ifeatureLayer_0 = value;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

