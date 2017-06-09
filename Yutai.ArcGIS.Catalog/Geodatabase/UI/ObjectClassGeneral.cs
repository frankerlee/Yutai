using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class ObjectClassGeneral : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBoxEdit cboFeatureType;
        private ComboBoxEdit cboRelateFC;
        private CheckEdit chkRelateFeatureClass;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup;
        private string string_0 = "";
        private string string_1 = "";
        private TextEdit txtAliasName;
        private TextEdit txtName;

        public event ValueChangedHandler ValueChanged;

        public ObjectClassGeneral()
        {
            this.InitializeComponent();
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboFeatureType.Enabled)
            {
                if (this.cboFeatureType.SelectedIndex == 0)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTAnnotation;
                    }
                }
                else if (this.cboFeatureType.SelectedIndex == 1)
                {
                    this.chkRelateFeatureClass.Visible = false;
                    this.cboRelateFC.Visible = false;
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTDimension;
                    }
                }
            }
        }

        private void cboRelateFC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboRelateFC.SelectedIndex != -1)
            {
                ObjectClassHelper.m_pObjectClassHelper.m_RelatedFeatureClass = (this.cboRelateFC.SelectedItem as DatasetWrap).Dataset as IFeatureClass;
            }
        }

        private void chkRelateFeatureClass_CheckedChanged(object sender, EventArgs e)
        {
            this.cboRelateFC.Visible = this.chkRelateFeatureClass.Checked;
            this.cboRelateFC.Enabled = true;
            ObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature = this.chkRelateFeatureClass.Checked;
            if (this.chkRelateFeatureClass.Checked && (this.cboRelateFC.Properties.Items.Count == 0))
            {
                IEnumDataset subsets = (ObjectClassHelper.m_pObjectClassHelper.Workspace as IDataset).Subsets;
                subsets.Reset();
                for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                {
                    if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        this.cboRelateFC.Properties.Items.Add(new DatasetWrap(dataset2));
                    }
                    else if (dataset2.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset dataset3 = dataset2.Subsets;
                        dataset3.Reset();
                        for (IDataset dataset4 = dataset3.Next(); dataset4 != null; dataset4 = dataset3.Next())
                        {
                            this.cboRelateFC.Properties.Items.Add(new DatasetWrap(dataset4));
                        }
                    }
                }
                if (this.cboRelateFC.Properties.Items.Count > 0)
                {
                    this.cboRelateFC.SelectedIndex = 0;
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboRelateFC = new ComboBoxEdit();
            this.chkRelateFeatureClass = new CheckEdit();
            this.cboFeatureType = new ComboBoxEdit();
            this.label3 = new Label();
            this.radioGroup = new RadioGroup();
            this.txtName = new TextEdit();
            this.txtAliasName = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.cboRelateFC.Properties.BeginInit();
            this.chkRelateFeatureClass.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            this.radioGroup.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.txtAliasName.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x2f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "别名";
            this.groupBox1.Controls.Add(this.cboRelateFC);
            this.groupBox1.Controls.Add(this.chkRelateFeatureClass);
            this.groupBox1.Controls.Add(this.cboFeatureType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioGroup);
            this.groupBox1.Location = new Point(0x18, 0x58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x130, 200);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类型";
            this.cboRelateFC.EditValue = "";
            this.cboRelateFC.Location = new Point(40, 0xa8);
            this.cboRelateFC.Name = "cboRelateFC";
            this.cboRelateFC.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelateFC.Size = new Size(0xd8, 0x15);
            this.cboRelateFC.TabIndex = 4;
            this.cboRelateFC.Visible = false;
            this.cboRelateFC.SelectedIndexChanged += new EventHandler(this.cboRelateFC_SelectedIndexChanged);
            this.chkRelateFeatureClass.Location = new Point(0x10, 0x90);
            this.chkRelateFeatureClass.Name = "chkRelateFeatureClass";
            this.chkRelateFeatureClass.Properties.Caption = "把注记连接到下面要素";
            this.chkRelateFeatureClass.Size = new Size(0x80, 0x13);
            this.chkRelateFeatureClass.TabIndex = 3;
            this.chkRelateFeatureClass.Visible = false;
            this.chkRelateFeatureClass.CheckedChanged += new EventHandler(this.chkRelateFeatureClass_CheckedChanged);
            this.cboFeatureType.EditValue = "ESRI注记要素";
            this.cboFeatureType.Location = new Point(0x20, 0x70);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "ESRI注记要素", "ESRI尺寸标注要素" });
            this.cboFeatureType.Size = new Size(0xd8, 0x15);
            this.cboFeatureType.TabIndex = 2;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            this.label3.Location = new Point(0x18, 0x58);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xf8, 0x18);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择要保存在该要素类中的定制对象类型";
            this.radioGroup.Location = new Point(8, 0x18);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "简单要素类型"), new RadioGroupItem(null, "该要素类保存注记要素,网络要素,维要素等对象") });
            this.radioGroup.Size = new Size(280, 0x40);
            this.radioGroup.TabIndex = 0;
            this.radioGroup.SelectedIndexChanged += new EventHandler(this.radioGroup_SelectedIndexChanged);
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(0x38, 0x10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 0x15);
            this.txtName.TabIndex = 3;
            this.txtName.EditValueChanged += new EventHandler(this.txtName_EditValueChanged);
            this.txtAliasName.EditValue = "";
            this.txtAliasName.Location = new Point(0x38, 0x30);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new Size(240, 0x15);
            this.txtAliasName.TabIndex = 4;
            this.txtAliasName.EditValueChanged += new EventHandler(this.txtAliasName_EditValueChanged);
            base.Controls.Add(this.txtAliasName);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ObjectClassGeneral";
            base.Size = new Size(0x17d, 0x139);
            base.Load += new EventHandler(this.ObjectClassGeneral_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboRelateFC.Properties.EndInit();
            this.chkRelateFeatureClass.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            this.radioGroup.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.txtAliasName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void ObjectClassGeneral_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.txtName.Text = this.string_0;
                this.txtAliasName.Text = this.string_1;
                this.txtName.Enabled = false;
                this.radioGroup.Enabled = false;
                try
                {
                    if (!(!ObjectClassShareData.m_IsShapeFile && ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass))
                    {
                        this.groupBox1.Visible = false;
                    }
                }
                catch
                {
                }
            }
            else if (!(!ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass || ObjectClassShareData.m_IsShapeFile))
            {
                this.radioGroup.Enabled = true;
            }
            else
            {
                this.radioGroup.Enabled = false;
                this.groupBox1.Visible = false;
                this.label2.Visible = false;
                this.txtAliasName.Visible = false;
            }
            if (ObjectClassShareData.m_IsShapeFile)
            {
                this.txtAliasName.Enabled = false;
            }
            this.bool_1 = true;
        }

        private void radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.cboFeatureType.Enabled = this.radioGroup.SelectedIndex == 1;
                if (this.radioGroup.SelectedIndex == 0)
                {
                    if (ObjectClassHelper.m_pObjectClassHelper != null)
                    {
                        ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTSimple;
                    }
                    this.cboRelateFC.Visible = false;
                    this.chkRelateFeatureClass.Visible = false;
                    this.cboFeatureType.Enabled = false;
                }
                else
                {
                    this.cboFeatureType.Enabled = true;
                    if (this.cboFeatureType.SelectedIndex == 0)
                    {
                        this.chkRelateFeatureClass.Visible = true;
                        this.cboRelateFC.Visible = this.chkRelateFeatureClass.Checked;
                        if (ObjectClassHelper.m_pObjectClassHelper != null)
                        {
                            ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTAnnotation;
                        }
                    }
                    else if (this.cboFeatureType.SelectedIndex == 1)
                    {
                        this.chkRelateFeatureClass.Visible = false;
                        this.cboRelateFC.Visible = false;
                        if (ObjectClassHelper.m_pObjectClassHelper != null)
                        {
                            ObjectClassHelper.m_pObjectClassHelper.m_FeatreType = esriFeatureType.esriFTDimension;
                        }
                    }
                }
            }
        }

        private void txtAliasName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.string_1 = this.txtAliasName.Text;
                if (ObjectClassHelper.m_pObjectClassHelper != null)
                {
                    ObjectClassHelper.m_pObjectClassHelper.AliasName = this.string_1;
                }
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
            }
        }

        private void txtName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1 && (ObjectClassHelper.m_pObjectClassHelper != null))
            {
                ObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text;
            }
        }

        public string AliasName
        {
            get
            {
                this.string_1 = this.txtAliasName.Text;
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string ClassName
        {
            get
            {
                this.string_0 = this.txtName.Text;
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_0 = value;
            }
        }

        internal class DatasetWrap
        {
            public IDataset Dataset = null;

            public DatasetWrap(IDataset idataset_0)
            {
                this.Dataset = idataset_0;
            }

            public override string ToString()
            {
                return this.Dataset.Name;
            }
        }
    }
}

