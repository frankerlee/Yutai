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
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public class RasterClassifiedRenderPage : UserControl, IUserControl
    {
        private bool bool_0 = false;
        private ComboBoxEdit cboClassifyMethod;
        private ComboBoxEdit cboClassifyNum;
        private StyleComboBox cboColorRamp;
        private ComboBoxEdit cboNormFields;
        private ComboBoxEdit cboValueFields;
        private CheckBox checkBox1;
        private GroupBox Classifygroup;
        private ColorEdit colorEdit1;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private GroupBox groupBox1;
        private IColorRamp icolorRamp_0 = null;
        private IContainer icontainer_0 = null;
        private IEnumColors ienumColors_0 = null;
        private IRasterClassifyColorRampRenderer irasterClassifyColorRampRenderer_0 = null;
        private IRasterLayer irasterLayer_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblZFactor;
        private RenderInfoListView listView1;
        private TextBox txtZFactor;

        public RasterClassifiedRenderPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                IObjectCopy copy = new ObjectCopyClass();
                IRasterClassifyColorRampRenderer renderer1 = copy.Copy(this.irasterClassifyColorRampRenderer_0) as IRasterClassifyColorRampRenderer;
                this.irasterLayer_0.Renderer = this.irasterClassifyColorRampRenderer_0 as IRasterRenderer;
            }
            catch (Exception)
            {
            }
        }

        private void cboClassifyMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            IClassify classify;
            if (this.bool_0)
            {
                classify = null;
                switch (this.cboClassifyMethod.SelectedIndex)
                {
                    case 0:
                        classify = new EqualIntervalClass();
                        goto Label_0043;

                    case 1:
                        classify = new QuantileClass();
                        goto Label_0043;

                    case 2:
                        classify = new NaturalBreaksClass();
                        goto Label_0043;
                }
            }
            return;
        Label_0043:
            (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ClassificationMethod = classify.ClassID;
            if (this.cboClassifyNum.SelectedIndex >= 0)
            {
                this.method_3();
            }
            else
            {
                this.cboClassifyNum.SelectedIndex = 4;
            }
        }

        private void cboClassifyNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_3();
            }
        }

        private void cboColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp = this.cboColorRamp.Text;
                if (this.irasterClassifyColorRampRenderer_0.ClassCount > 0)
                {
                    bool flag;
                    this.icolorRamp_0.Size = this.irasterClassifyColorRampRenderer_0.ClassCount;
                    this.icolorRamp_0.CreateRamp(out flag);
                    IEnumColors colors = this.icolorRamp_0.Colors;
                    colors.Reset();
                    this.listView1.BeginUpdate();
                    for (int i = 0; i < this.irasterClassifyColorRampRenderer_0.ClassCount; i++)
                    {
                        ISymbol symbol = this.irasterClassifyColorRampRenderer_0.get_Symbol(i);
                        if (symbol is IMarkerSymbol)
                        {
                            (symbol as IMarkerSymbol).Color = colors.Next();
                        }
                        else if (symbol is ILineSymbol)
                        {
                            (symbol as ILineSymbol).Color = colors.Next();
                        }
                        else if (symbol is IFillSymbol)
                        {
                            (symbol as IFillSymbol).Color = colors.Next();
                        }
                        this.irasterClassifyColorRampRenderer_0.set_Symbol(i, symbol);
                        if (this.listView1.Items[i] is ListViewItemEx)
                        {
                            (this.listView1.Items[i] as ListViewItemEx).Style = symbol;
                        }
                        this.listView1.Items[i].Tag = symbol;
                    }
                    this.listView1.EndUpdate();
                    this.listView1.Invalidate();
                }
            }
        }

        private void cboNormFields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboValueFields_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).UseHillShade = this.checkBox1.Checked;
                this.lblZFactor.Enabled = this.checkBox1.Checked;
                this.txtZFactor.Enabled = this.checkBox1.Checked;
            }
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IColor noDataColor = (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
                if (noDataColor != null)
                {
                    this.method_8(this.colorEdit1, noDataColor);
                    (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor = noDataColor;
                }
            }
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
            this.icontainer_0 = new Container();
            this.cboColorRamp = new StyleComboBox(this.icontainer_0);
            this.label2 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.Classifygroup = new GroupBox();
            this.cboClassifyNum = new ComboBoxEdit();
            this.cboClassifyMethod = new ComboBoxEdit();
            this.label4 = new Label();
            this.label5 = new Label();
            this.listView1 = new RenderInfoListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.cboNormFields = new ComboBoxEdit();
            this.cboValueFields = new ComboBoxEdit();
            this.label3 = new Label();
            this.label6 = new Label();
            this.checkBox1 = new CheckBox();
            this.txtZFactor = new TextBox();
            this.lblZFactor = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.Classifygroup.SuspendLayout();
            this.cboClassifyNum.Properties.BeginInit();
            this.cboClassifyMethod.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboNormFields.Properties.BeginInit();
            this.cboValueFields.Properties.BeginInit();
            base.SuspendLayout();
            this.cboColorRamp.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboColorRamp.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboColorRamp.DropDownWidth = 160;
            this.cboColorRamp.Location = new Point(0x62, 0x65);
            this.cboColorRamp.Name = "cboColorRamp";
            this.cboColorRamp.Size = new Size(0x90, 0x16);
            this.cboColorRamp.TabIndex = 7;
            this.cboColorRamp.SelectedIndexChanged += new EventHandler(this.cboColorRamp_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 0x69);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "颜色方案";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(0x149, 0xf9);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x15);
            this.colorEdit1.TabIndex = 0x2e;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xf6, 0xfe);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0x2d;
            this.label1.Text = "空值显示颜色";
            this.Classifygroup.Controls.Add(this.cboClassifyNum);
            this.Classifygroup.Controls.Add(this.cboClassifyMethod);
            this.Classifygroup.Controls.Add(this.label4);
            this.Classifygroup.Controls.Add(this.label5);
            this.Classifygroup.Location = new Point(210, 5);
            this.Classifygroup.Name = "Classifygroup";
            this.Classifygroup.Size = new Size(0xa7, 0x58);
            this.Classifygroup.TabIndex = 0x31;
            this.Classifygroup.TabStop = false;
            this.Classifygroup.Text = "分类";
            this.cboClassifyNum.EditValue = "";
            this.cboClassifyNum.Location = new Point(0x43, 0x38);
            this.cboClassifyNum.Name = "cboClassifyNum";
            this.cboClassifyNum.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyNum.Properties.Items.AddRange(new object[] { 
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", 
                "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"
             });
            this.cboClassifyNum.Size = new Size(0x5c, 0x15);
            this.cboClassifyNum.TabIndex = 0x31;
            this.cboClassifyNum.SelectedIndexChanged += new EventHandler(this.cboClassifyNum_SelectedIndexChanged);
            this.cboClassifyMethod.EditValue = "";
            this.cboClassifyMethod.Location = new Point(0x43, 0x18);
            this.cboClassifyMethod.Name = "cboClassifyMethod";
            this.cboClassifyMethod.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClassifyMethod.Properties.Items.AddRange(new object[] { "等间隔", "分位数", "自然间隔" });
            this.cboClassifyMethod.Size = new Size(0x5c, 0x15);
            this.cboClassifyMethod.TabIndex = 0x30;
            this.cboClassifyMethod.SelectedIndexChanged += new EventHandler(this.cboClassifyMethod_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 0x38);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 0x27;
            this.label4.Text = "类";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 0x18);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 0x26;
            this.label5.Text = "分类方法";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(0x10, 130);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x169, 0x6d);
            this.listView1.TabIndex = 0x30;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "符号";
            this.columnHeader_1.Text = "范围";
            this.columnHeader_1.Width = 0x61;
            this.columnHeader_2.Text = "标注";
            this.columnHeader_2.Width = 150;
            this.groupBox1.Controls.Add(this.cboNormFields);
            this.groupBox1.Controls.Add(this.cboValueFields);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new Point(10, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xc2, 0x58);
            this.groupBox1.TabIndex = 0x2f;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            this.cboNormFields.EditValue = "";
            this.cboNormFields.Location = new Point(0x4e, 0x38);
            this.cboNormFields.Name = "cboNormFields";
            this.cboNormFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboNormFields.Size = new Size(0x68, 0x15);
            this.cboNormFields.TabIndex = 0x2e;
            this.cboNormFields.SelectedIndexChanged += new EventHandler(this.cboNormFields_SelectedIndexChanged);
            this.cboValueFields.EditValue = "";
            this.cboValueFields.Location = new Point(0x4e, 0x18);
            this.cboValueFields.Name = "cboValueFields";
            this.cboValueFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboValueFields.Size = new Size(0x68, 0x15);
            this.cboValueFields.TabIndex = 0x2d;
            this.cboValueFields.SelectedIndexChanged += new EventHandler(this.cboValueFields_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 0x27;
            this.label3.Text = "正则化字段";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0x18);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x29, 12);
            this.label6.TabIndex = 0x26;
            this.label6.Text = "值字段";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(20, 250);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x48, 0x10);
            this.checkBox1.TabIndex = 50;
            this.checkBox1.Text = "使用山影";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.txtZFactor.Location = new Point(0x9d, 0xf8);
            this.txtZFactor.Name = "txtZFactor";
            this.txtZFactor.Size = new Size(0x4e, 0x15);
            this.txtZFactor.TabIndex = 0x33;
            this.txtZFactor.Text = "1";
            this.txtZFactor.TextChanged += new EventHandler(this.txtZFactor_TextChanged);
            this.lblZFactor.AutoSize = true;
            this.lblZFactor.Location = new Point(0x74, 0xfc);
            this.lblZFactor.Name = "lblZFactor";
            this.lblZFactor.Size = new Size(0x23, 12);
            this.lblZFactor.TabIndex = 0x34;
            this.lblZFactor.Text = "Z因子";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.lblZFactor);
            base.Controls.Add(this.txtZFactor);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.Classifygroup);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboColorRamp);
            base.Controls.Add(this.label2);
            base.Name = "RasterClassifiedRenderPage";
            base.Size = new Size(0x19d, 0x131);
            base.Load += new EventHandler(this.RasterClassifiedRenderPage_Load);
            this.colorEdit1.Properties.EndInit();
            this.Classifygroup.ResumeLayout(false);
            this.Classifygroup.PerformLayout();
            this.cboClassifyNum.Properties.EndInit();
            this.cboClassifyMethod.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cboNormFields.Properties.EndInit();
            this.cboValueFields.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private object method_0(UID uid_0)
        {
            if (uid_0 == null)
            {
                return null;
            }
            try
            {
                Guid clsid = new Guid(uid_0.Value.ToString());
                return Activator.CreateInstance(System.Type.GetTypeFromCLSID(clsid));
            }
            catch
            {
                return null;
            }
        }

        private void method_1()
        {
            int num;
            this.cboValueFields.Properties.Items.Clear();
            this.cboValueFields.Properties.Items.Add("<VALUE>");
            this.cboNormFields.Properties.Items.Clear();
            this.cboNormFields.Properties.Items.Add("<无>");
            this.cboNormFields.Properties.Items.Add("<百分比>");
            this.cboNormFields.Properties.Items.Add("<LOG>");
            IFields fields = null;
            ITable displayTable = (this.irasterLayer_0 as IDisplayTable).DisplayTable;
            if (displayTable != null)
            {
                fields = displayTable.Fields;
            }
            if (fields != null)
            {
                for (num = 0; num < fields.FieldCount; num++)
                {
                    IField field = fields.get_Field(num);
                    switch (field.Type)
                    {
                        case esriFieldType.esriFieldTypeDouble:
                        case esriFieldType.esriFieldTypeInteger:
                        case esriFieldType.esriFieldTypeSingle:
                        case esriFieldType.esriFieldTypeSmallInteger:
                            this.cboValueFields.Properties.Items.Add(new FieldWrap(field));
                            this.cboNormFields.Properties.Items.Add(new FieldWrap(field));
                            break;
                    }
                }
            }
            if ((this.irasterClassifyColorRampRenderer_0.ClassField == "<VALUE>") || (this.irasterClassifyColorRampRenderer_0.ClassField == ""))
            {
                this.cboValueFields.SelectedIndex = 0;
            }
            else
            {
                num = 1;
                while (num < this.cboValueFields.Properties.Items.Count)
                {
                    if ((this.cboValueFields.Properties.Items[num] as FieldWrap).Name == this.irasterClassifyColorRampRenderer_0.ClassField)
                    {
                        this.cboValueFields.SelectedIndex = num;
                        break;
                    }
                    num++;
                }
            }
            this.cboNormFields.Text = this.irasterClassifyColorRampRenderer_0.NormField;
            if (this.cboValueFields.Properties.Items.Count == 1)
            {
                this.cboValueFields.Enabled = false;
                this.cboNormFields.Enabled = false;
            }
            object[] objArray = new object[3];
            this.listView1.Items.Clear();
            for (num = 0; num < this.irasterClassifyColorRampRenderer_0.ClassCount; num++)
            {
                objArray[0] = this.irasterClassifyColorRampRenderer_0.get_Symbol(num);
                if (num == 0)
                {
                    objArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(num).ToString("0.####");
                }
                else
                {
                    objArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(num - 1).ToString("0.####") + " - " + this.irasterClassifyColorRampRenderer_0.get_Break(num).ToString("0.####");
                }
                objArray[2] = this.irasterClassifyColorRampRenderer_0.get_Label(num);
                this.listView1.Add(objArray);
            }
            this.cboClassifyNum.SelectedIndex = this.irasterClassifyColorRampRenderer_0.ClassCount - 1;
            IClassify classify = this.method_0((this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ClassificationMethod) as IClassify;
            if (classify != null)
            {
                this.Classifygroup.Enabled = true;
                switch (classify.MethodName)
                {
                    case "Equal Interval":
                    case "等间隔":
                        this.cboClassifyMethod.SelectedIndex = 0;
                        break;

                    case "Quantile":
                    case "分位数":
                        this.cboClassifyMethod.SelectedIndex = 1;
                        break;

                    case "Natural Breaks (Jenks)":
                    case "自然间隔(Jenks)":
                        this.cboClassifyMethod.SelectedIndex = 2;
                        break;
                }
            }
            else
            {
                this.Classifygroup.Enabled = false;
            }
            IColor noDataColor = (this.irasterClassifyColorRampRenderer_0 as IRasterDisplayProps).NoDataColor;
            if (noDataColor != null)
            {
                this.method_5(this.colorEdit1, noDataColor);
            }
            this.checkBox1.Checked = (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).UseHillShade;
            this.txtZFactor.Text = (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).ZScale.ToString();
            this.lblZFactor.Enabled = this.checkBox1.Checked;
            this.txtZFactor.Enabled = this.checkBox1.Checked;
        }

        private ITable method_2()
        {
            if (this.irasterLayer_0 == null)
            {
                return null;
            }
            if (this.irasterLayer_0 is IDisplayTable)
            {
                return (this.irasterLayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.irasterLayer_0 is IAttributeTable)
            {
                return (this.irasterLayer_0 as IAttributeTable).AttributeTable;
            }
            return (this.irasterLayer_0 as ITable);
        }

        private void method_3()
        {
            string text = this.cboNormFields.Text;
            string name = "";
            if (this.cboValueFields.SelectedIndex > 0)
            {
                name = (this.cboValueFields.SelectedItem as FieldWrap).Name;
            }
            else
            {
                name = "";
            }
            this.method_4(this.icolorRamp_0, this.irasterClassifyColorRampRenderer_0, this.cboClassifyNum.SelectedIndex + 1, this.method_2(), name, text);
            this.listView1.Items.Clear();
            string[] strArray = new string[3];
            int index = 0;
            while (true)
            {
                if (index >= this.irasterClassifyColorRampRenderer_0.ClassCount)
                {
                    break;
                }
                strArray[0] = "";
                if (index == 0)
                {
                    strArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(index).ToString();
                }
                else
                {
                    strArray[1] = this.irasterClassifyColorRampRenderer_0.get_Break(index - 1).ToString() + " - " + this.irasterClassifyColorRampRenderer_0.get_Break(index).ToString();
                }
                strArray[2] = this.irasterClassifyColorRampRenderer_0.get_Label(index);
                ListViewItemEx ex = new ListViewItemEx(strArray);
                try
                {
                    ex.Style = this.irasterClassifyColorRampRenderer_0.get_Symbol(index);
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, "");
                }
                this.listView1.Add(ex);
                index++;
            }
            this.cboClassifyNum.SelectedIndex = this.irasterClassifyColorRampRenderer_0.ClassCount - 1;
        }

        private void method_4(IColorRamp icolorRamp_1, IRasterClassifyColorRampRenderer irasterClassifyColorRampRenderer_1, int int_0, ITable itable_0, string string_0, string string_1)
        {
            try
            {
                bool flag;
                if (string_0.Length > 0)
                {
                    irasterClassifyColorRampRenderer_1.ClassField = string_0;
                }
                if (string_1.Length > 0)
                {
                    irasterClassifyColorRampRenderer_1.NormField = string_1;
                }
                IRasterRenderer renderer = (IRasterRenderer) irasterClassifyColorRampRenderer_1;
                renderer.Raster = this.irasterLayer_0.Raster;
                irasterClassifyColorRampRenderer_1.ClassCount = int_0;
                renderer.Update();
                this.bool_0 = false;
                this.cboClassifyNum.SelectedIndex = irasterClassifyColorRampRenderer_1.ClassCount - 1;
                this.bool_0 = true;
                icolorRamp_1.Size = irasterClassifyColorRampRenderer_1.ClassCount;
                icolorRamp_1.CreateRamp(out flag);
                IEnumColors colors = icolorRamp_1.Colors;
                ISymbol symbol = null;
                for (int i = 0; i < irasterClassifyColorRampRenderer_1.ClassCount; i++)
                {
                    string str;
                    IColor color = colors.Next();
                    ISimpleFillSymbol symbol2 = new SimpleFillSymbolClass {
                        Color = color,
                        Style = esriSimpleFillStyle.esriSFSSolid
                    };
                    symbol = symbol2 as ISymbol;
                    irasterClassifyColorRampRenderer_1.set_Symbol(i, symbol);
                    if (i == (irasterClassifyColorRampRenderer_1.ClassCount - 1))
                    {
                        str = irasterClassifyColorRampRenderer_1.get_Break(i).ToString("0.####");
                    }
                    else
                    {
                        str = irasterClassifyColorRampRenderer_1.get_Break(i).ToString("0.####") + " - " + irasterClassifyColorRampRenderer_1.get_Break(i + 1).ToString("0.####");
                    }
                    irasterClassifyColorRampRenderer_1.set_Label(i, str);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void method_5(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
                this.method_6(rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_6(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
             int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_7(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |=(uint) int_1;
            num = num << 8;
            return (int) (num | int_0);
        }

        private void method_8(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_7(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void RasterClassifiedRenderPage_Load(object sender, EventArgs e)
        {
            int num = -1;
            if (this.irasterClassifyColorRampRenderer_0 != null)
            {
                string colorRamp = (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp;
                if (this.istyleGallery_0 != null)
                {
                    IEnumStyleGalleryItem item = this.istyleGallery_0.get_Items("Color Ramps", "", "");
                    item.Reset();
                    for (IStyleGalleryItem item2 = item.Next(); item2 != null; item2 = item.Next())
                    {
                        this.cboColorRamp.Add(item2);
                        if (item2.Name == colorRamp)
                        {
                            num = this.cboColorRamp.Items.Count - 1;
                        }
                    }
                    item = null;
                    GC.Collect();
                }
                if (this.cboColorRamp.Items.Count == 0)
                {
                    this.cboColorRamp.Enabled = false;
                    IRandomColorRamp ramp = new RandomColorRampClass {
                        StartHue = 40,
                        EndHue = 120,
                        MinValue = 0x41,
                        MaxValue = 90,
                        MinSaturation = 0x19,
                        MaxSaturation = 0x2d,
                        Size = 5,
                        Seed = 0x17
                    };
                    this.icolorRamp_0 = ramp;
                }
                else
                {
                    this.cboColorRamp.SelectedIndex = num;
                    if (this.cboColorRamp.SelectedIndex == -1)
                    {
                        this.cboColorRamp.SelectedIndex = 0;
                    }
                    this.icolorRamp_0 = this.cboColorRamp.GetSelectStyleGalleryItem().Item as IColorRamp;
                    (this.irasterClassifyColorRampRenderer_0 as IRasterClassifyUIProperties).ColorRamp = this.cboColorRamp.Text;
                }
                if (this.irasterLayer_0 != null)
                {
                    this.method_1();
                }
                this.bool_0 = true;
            }
        }

        private void txtZFactor_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    (this.irasterClassifyColorRampRenderer_0 as IHillShadeInfo).ZScale = double.Parse(this.txtZFactor.Text);
                }
                catch
                {
                }
            }
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.irasterLayer_0 = value as IRasterLayer;
                if (this.irasterLayer_0 == null)
                {
                    this.irasterClassifyColorRampRenderer_0 = null;
                }
                else
                {
                    IRasterClassifyColorRampRenderer pInObject = this.irasterLayer_0.Renderer as IRasterClassifyColorRampRenderer;
                    if (pInObject == null)
                    {
                        if (this.irasterClassifyColorRampRenderer_0 == null)
                        {
                            this.irasterClassifyColorRampRenderer_0 = RenderHelper.RasterClassifyRenderer(this.irasterLayer_0);
                        }
                    }
                    else
                    {
                        IObjectCopy copy = new ObjectCopyClass();
                        this.irasterClassifyColorRampRenderer_0 = copy.Copy(pInObject) as IRasterClassifyColorRampRenderer;
                    }
                    if (this.bool_0)
                    {
                        this.bool_0 = false;
                        this.method_1();
                        this.bool_0 = true;
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

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
                this.listView1.StyleGallery = this.istyleGallery_0;
            }
        }
    }
}

