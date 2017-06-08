namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility.Wrap;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class NewObjectClassGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private ComboBoxEdit cboFeatureType;
        private ComboBoxEdit cboRelateFC;
        private CheckEdit chkHasM;
        private CheckEdit chkHasZ;
        private CheckEdit chkRelateFeatureClass;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private string string_0 = "";
        private TextEdit txtAliasName;
        private TextEdit txtName;

        public event ValueChangedHandler ValueChanged;

        public NewObjectClassGeneralPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入要素类名称!");
                return false;
            }
            NewObjectClassHelper.m_pObjectClassHelper.Name = this.txtName.Text.Trim();
            NewObjectClassHelper.m_pObjectClassHelper.AliasName = this.txtAliasName.Text.Trim();
            if (NewObjectClassHelper.m_pObjectClassHelper.AliasName.Length == 0)
            {
                NewObjectClassHelper.m_pObjectClassHelper.AliasName = NewObjectClassHelper.m_pObjectClassHelper.Name;
            }
            switch (this.cboFeatureType.SelectedIndex)
            {
                case 0:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPoint;
                    break;

                case 1:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryMultipoint;
                    break;

                case 2:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolyline;
                    break;

                case 3:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    break;

                case 4:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTSimple;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryMultiPatch;
                    break;

                case 5:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTAnnotation;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature = this.chkRelateFeatureClass.Checked;
                    if (!this.chkRelateFeatureClass.Checked || (this.cboRelateFC.SelectedIndex == -1))
                    {
                        NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass = null;
                        break;
                    }
                    NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass = (this.cboRelateFC.SelectedItem as ObjectWrap).Object as IFeatureClass;
                    break;

                case 6:
                    NewObjectClassHelper.m_pObjectClassHelper.FeatureType = esriFeatureType.esriFTDimension;
                    NewObjectClassHelper.m_pObjectClassHelper.ShapeType = esriGeometryType.esriGeometryPolygon;
                    break;
            }
            NewObjectClassHelper.m_pObjectClassHelper.HasM = this.chkHasM.Checked;
            NewObjectClassHelper.m_pObjectClassHelper.HasZ = this.chkHasZ.Checked;
            return true;
        }

        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.cboFeatureType.SelectedIndex == 5;
            this.cboRelateFC.Visible = flag;
            this.chkRelateFeatureClass.Visible = flag;
            if ((this.cboFeatureType.SelectedIndex == 5) || (this.cboFeatureType.SelectedIndex == 6))
            {
                this.chkHasM.Enabled = false;
                this.chkHasZ.Enabled = false;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = false;
            }
            else if (this.cboFeatureType.SelectedIndex == 4)
            {
                this.chkHasM.Enabled = true;
                this.chkHasZ.Enabled = false;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = true;
            }
            else
            {
                this.chkHasM.Enabled = true;
                this.chkHasZ.Enabled = true;
                this.chkHasM.Checked = false;
                this.chkHasZ.Checked = false;
            }
        }

        private void chkRelateFeatureClass_CheckedChanged(object sender, EventArgs e)
        {
            this.cboRelateFC.Enabled = (this.cboRelateFC.Properties.Items.Count > 0) && this.chkRelateFeatureClass.Checked;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.txtAliasName = new TextEdit();
            this.txtName = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.cboRelateFC = new ComboBoxEdit();
            this.cboFeatureType = new ComboBoxEdit();
            this.chkRelateFeatureClass = new CheckEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.chkHasZ = new CheckEdit();
            this.chkHasM = new CheckEdit();
            this.txtAliasName.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboRelateFC.Properties.BeginInit();
            this.cboFeatureType.Properties.BeginInit();
            this.chkRelateFeatureClass.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.chkHasZ.Properties.BeginInit();
            this.chkHasM.Properties.BeginInit();
            base.SuspendLayout();
            this.txtAliasName.EditValue = "";
            this.txtAliasName.Location = new System.Drawing.Point(0x30, 40);
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.Size = new Size(240, 0x15);
            this.txtAliasName.TabIndex = 9;
            this.txtAliasName.EditValueChanged += new EventHandler(this.txtAliasName_EditValueChanged);
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(0x30, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(240, 0x15);
            this.txtName.TabIndex = 8;
            this.groupBox1.Controls.Add(this.cboRelateFC);
            this.groupBox1.Controls.Add(this.cboFeatureType);
            this.groupBox1.Controls.Add(this.chkRelateFeatureClass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 0x4d);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x130, 0x9e);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类型";
            this.cboRelateFC.Location = new System.Drawing.Point(0x10, 0x5e);
            this.cboRelateFC.Name = "cboRelateFC";
            this.cboRelateFC.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboRelateFC.Size = new Size(0xcb, 0x15);
            this.cboRelateFC.TabIndex = 6;
            this.cboRelateFC.Visible = false;
            this.cboFeatureType.EditValue = "点要素";
            this.cboFeatureType.Location = new System.Drawing.Point(0x10, 0x29);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFeatureType.Properties.Items.AddRange(new object[] { "点要素", "多点要素", "线要素", "多边形要素", "面片要素" });
            this.cboFeatureType.Size = new Size(0xcb, 0x15);
            this.cboFeatureType.TabIndex = 5;
            this.cboFeatureType.SelectedIndexChanged += new EventHandler(this.cboFeatureType_SelectedIndexChanged);
            this.chkRelateFeatureClass.Location = new System.Drawing.Point(0x10, 0x44);
            this.chkRelateFeatureClass.Name = "chkRelateFeatureClass";
            this.chkRelateFeatureClass.Properties.Caption = "把注记连接到下面要素";
            this.chkRelateFeatureClass.Size = new Size(190, 0x13);
            this.chkRelateFeatureClass.TabIndex = 3;
            this.chkRelateFeatureClass.Visible = false;
            this.chkRelateFeatureClass.CheckedChanged += new EventHandler(this.chkRelateFeatureClass_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 0x1a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xdd, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "选择要保存在该要素类中的定制对象类型";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x27);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "别名";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "名称";
            this.groupBox2.Controls.Add(this.chkHasZ);
            this.groupBox2.Controls.Add(this.chkHasM);
            this.groupBox2.Location = new System.Drawing.Point(8, 0xef);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x130, 0x58);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "几何属性";
            this.chkHasZ.Location = new System.Drawing.Point(6, 0x2d);
            this.chkHasZ.Name = "chkHasZ";
            this.chkHasZ.Properties.Caption = "包含Z值。用来存放三维值";
            this.chkHasZ.Size = new Size(190, 0x13);
            this.chkHasZ.TabIndex = 5;
            this.chkHasM.Location = new System.Drawing.Point(6, 20);
            this.chkHasM.Name = "chkHasM";
            this.chkHasM.Properties.Caption = "包含M值。用来存放路径值";
            this.chkHasM.Size = new Size(190, 0x13);
            this.chkHasM.TabIndex = 4;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtAliasName);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "NewObjectClassGeneralPage";
            base.Size = new Size(0x158, 0x173);
            base.Load += new EventHandler(this.NewObjectClassGeneralPage_Load);
            this.txtAliasName.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboRelateFC.Properties.EndInit();
            this.cboFeatureType.Properties.EndInit();
            this.chkRelateFeatureClass.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.chkHasZ.Properties.EndInit();
            this.chkHasM.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(IList ilist_0, IWorkspace iworkspace_0)
        {
            IEnumDataset dataset = iworkspace_0.get_Datasets(esriDatasetType.esriDTAny);
            dataset.Reset();
            for (IDataset dataset2 = dataset.Next(); dataset2 != null; dataset2 = dataset.Next())
            {
                if (dataset2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    if ((dataset2 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple)
                    {
                        ilist_0.Add(new ObjectWrap(dataset2));
                    }
                }
                else if (dataset2.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    IEnumDataset subsets = dataset2.Subsets;
                    subsets.Reset();
                    for (IDataset dataset4 = subsets.Next(); dataset4 != null; dataset4 = subsets.Next())
                    {
                        if ((dataset4.Type == esriDatasetType.esriDTFeatureClass) && ((dataset4 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple))
                        {
                            ilist_0.Add(new ObjectWrap(dataset4));
                        }
                    }
                }
            }
        }

        private void method_1(IList ilist_0, IWorkspace iworkspace_0)
        {
            IEnumDatasetName name = iworkspace_0.get_DatasetNames(esriDatasetType.esriDTAny);
            name.Reset();
            for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
            {
                if (name2.Type == esriDatasetType.esriDTFeatureClass)
                {
                    if ((name2 as IFeatureClassName).FeatureType == esriFeatureType.esriFTSimple)
                    {
                        ilist_0.Add(new ObjectWrap(name2));
                    }
                }
                else if (name2.Type == esriDatasetType.esriDTFeatureDataset)
                {
                    IEnumDatasetName subsetNames = name2.SubsetNames;
                    subsetNames.Reset();
                    for (IDatasetName name4 = subsetNames.Next(); name4 != null; name4 = subsetNames.Next())
                    {
                        if ((name4.Type == esriDatasetType.esriDTFeatureClass) && ((name4 as IFeatureClassName).FeatureType == esriFeatureType.esriFTSimple))
                        {
                            ilist_0.Add(new ObjectWrap(name4));
                        }
                    }
                }
            }
        }

        private void NewObjectClassGeneralPage_Load(object sender, EventArgs e)
        {
            if (!NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
            {
                this.cboFeatureType.SelectedIndex = 3;
                this.method_0(this.cboRelateFC.Properties.Items, NewObjectClassHelper.m_pObjectClassHelper.Workspace);
                if (this.cboRelateFC.Properties.Items.Count == 0)
                {
                    this.cboRelateFC.SelectedIndex = 0;
                }
                this.cboRelateFC.Enabled = (this.cboRelateFC.Properties.Items.Count > 0) && this.chkRelateFeatureClass.Checked;
                this.chkRelateFeatureClass.Enabled = this.cboRelateFC.Properties.Items.Count > 0;
            }
            else
            {
                this.txtName.Text = NewObjectClassHelper.m_pObjectClassHelper.Name;
                this.txtName.Properties.ReadOnly = true;
                this.txtAliasName.Text = NewObjectClassHelper.m_pObjectClassHelper.AliasName;
                this.cboFeatureType.Enabled = false;
                if (!NewObjectClassHelper.m_pObjectClassHelper.IsFeatureClass)
                {
                    this.groupBox1.Visible = false;
                    this.groupBox1.Visible = false;
                }
                else
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTSimple)
                    {
                        switch (NewObjectClassHelper.m_pObjectClassHelper.ShapeType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                this.cboFeatureType.SelectedIndex = 0;
                                break;

                            case esriGeometryType.esriGeometryMultipoint:
                                this.cboFeatureType.SelectedIndex = 1;
                                break;

                            case esriGeometryType.esriGeometryPolyline:
                                this.cboFeatureType.SelectedIndex = 2;
                                break;

                            case esriGeometryType.esriGeometryPolygon:
                                this.cboFeatureType.SelectedIndex = 3;
                                break;

                            case esriGeometryType.esriGeometryAny:
                                this.cboFeatureType.SelectedIndex = -1;
                                break;

                            case esriGeometryType.esriGeometryMultiPatch:
                                this.cboFeatureType.SelectedIndex = 4;
                                break;
                        }
                    }
                    else if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        this.cboFeatureType.SelectedIndex = 5;
                    }
                    else
                    {
                        this.cboFeatureType.SelectedIndex = 6;
                    }
                    this.chkHasZ.Checked = NewObjectClassHelper.m_pObjectClassHelper.HasZ;
                    this.chkHasZ.Enabled = false;
                    this.chkHasM.Checked = NewObjectClassHelper.m_pObjectClassHelper.HasM;
                    this.chkHasM.Enabled = false;
                }
            }
            this.bool_0 = true;
        }

        private void txtAliasName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && (this.valueChangedHandler_0 != null))
            {
                this.valueChangedHandler_0(this, new EventArgs());
            }
        }

        public string AliasName
        {
            get
            {
                this.string_0 = this.txtAliasName.Text;
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

