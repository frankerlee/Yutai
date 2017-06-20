using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmExpressionBuild : Form
    {
        private bool bool_0 = false;
        private SimpleButton btnAnd;
        private SimpleButton btnApply;
        private SimpleButton btnBracket;
        private SimpleButton btnClear;
        private SimpleButton btnClose;
        private SimpleButton btnEqual;
        private SimpleButton btnGreat;
        private SimpleButton btnGreatEqual;
        private SimpleButton btnIs;
        private SimpleButton btnLike;
        private SimpleButton btnLittle;
        private SimpleButton btnLittleEqual;
        private SimpleButton btnNot;
        private SimpleButton btnNotEqual;
        private SimpleButton btnOr;
        private SimpleButton btnValidate;
        private Container container_0 = null;
        private ListBoxControl Fieldlist;
        private ILayer ilayer_0 = null;
        private ITable itable_0 = null;
        private Label label1;
        private Label label2;
        private Label lbl;
        private ListBoxControl lstFunction;
        private MemoEdit memEditWhereCaluse;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton4;
        private string string_0 = "";
        private string string_1 = "";

        public frmExpressionBuild()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.ilayer_0 != null)
            {
                this.string_0 = this.memEditWhereCaluse.Text;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.string_0 = this.memEditWhereCaluse.Text;
            base.DialogResult = DialogResult.OK;
        }

        public void ClearWhereCaluse()
        {
            this.memEditWhereCaluse.Text = "";
        }

        public string ConvertFieldValueToString(esriFieldType esriFieldType_0, object object_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeDouble:
                case esriFieldType.esriFieldTypeOID:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeString:
                    return ("'" + object_0.ToString() + "'");

                case esriFieldType.esriFieldTypeDate:
                    return object_0.ToString();

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何数据";

                case esriFieldType.esriFieldTypeBlob:
                    return "长二进制串";
            }
            return "";
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void Fieldlist_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.Fieldlist.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            try
            {
                this.memEditWhereCaluse.Focus();
            }
            catch
            {
            }
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void Fieldlist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void frmExpressionBuild_Load(object sender, EventArgs e)
        {
            this.method_17();
            this.memEditWhereCaluse.Text = this.string_0;
            this.bool_0 = true;
        }

        public void GetUniqueValues(ITable itable_1, string string_2, ListBoxItemCollection listBoxItemCollection_0)
        {
            try
            {
                string str;
                if ((string_2[0] == '\'') || (string_2[0] == '['))
                {
                    str = string_2.Substring(1, string_2.Length - 2);
                }
                else
                {
                    str = string_2;
                }
                IQueryFilter queryFilter = new QueryFilterClass {
                    WhereClause = "1=1"
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + str;
                ICursor cursor = itable_1.Search(queryFilter, false);
                IDataStatistics statistics = new DataStatisticsClass {
                    Field = str,
                    Cursor = cursor
                };
                IEnumerator uniqueValues = statistics.UniqueValues;
                uniqueValues.Reset();
                int index = cursor.Fields.FindField(str);
                esriFieldType type = cursor.Fields.get_Field(index).Type;
                while (uniqueValues.MoveNext())
                {
                    listBoxItemCollection_0.Add(this.ConvertFieldValueToString(type, uniqueValues.Current));
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpressionBuild));
            this.label2 = new Label();
            this.btnIs = new SimpleButton();
            this.btnNot = new SimpleButton();
            this.btnBracket = new SimpleButton();
            this.btnOr = new SimpleButton();
            this.btnLittleEqual = new SimpleButton();
            this.btnLittle = new SimpleButton();
            this.btnAnd = new SimpleButton();
            this.btnLike = new SimpleButton();
            this.btnGreat = new SimpleButton();
            this.btnNotEqual = new SimpleButton();
            this.btnGreatEqual = new SimpleButton();
            this.btnEqual = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.memEditWhereCaluse = new MemoEdit();
            this.Fieldlist = new ListBoxControl();
            this.lbl = new Label();
            this.lstFunction = new ListBoxControl();
            this.btnClear = new SimpleButton();
            this.btnValidate = new SimpleButton();
            this.label1 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.simpleButton4 = new SimpleButton();
            this.memEditWhereCaluse.Properties.BeginInit();
            ((ISupportInitialize) this.Fieldlist).BeginInit();
            ((ISupportInitialize) this.lstFunction).BeginInit();
            base.SuspendLayout();
            this.label2.Location = new Point(0x10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x10);
            this.label2.TabIndex = 0x2b;
            this.label2.Text = "字段";
            this.btnIs.Location = new Point(0xe9, 0x127);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new Size(0x20, 0x18);
            this.btnIs.TabIndex = 0x29;
            this.btnIs.Text = "0";
            this.btnIs.Click += new EventHandler(this.simpleButton4_Click);
            this.btnNot.Location = new Point(0x113, 0x127);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new Size(0x20, 0x18);
            this.btnNot.TabIndex = 40;
            this.btnNot.Text = ".";
            this.btnNot.Click += new EventHandler(this.simpleButton4_Click);
            this.btnBracket.Location = new Point(0xc3, 0x127);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new Size(0x20, 0x18);
            this.btnBracket.TabIndex = 0x27;
            this.btnBracket.Text = "(  )";
            this.btnBracket.Click += new EventHandler(this.simpleButton4_Click);
            this.btnOr.Location = new Point(0x113, 0x109);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new Size(0x20, 0x18);
            this.btnOr.TabIndex = 0x25;
            this.btnOr.Text = "3";
            this.btnOr.Click += new EventHandler(this.simpleButton4_Click);
            this.btnLittleEqual.Location = new Point(0xeb, 0x109);
            this.btnLittleEqual.Name = "btnLittleEqual";
            this.btnLittleEqual.Size = new Size(0x20, 0x18);
            this.btnLittleEqual.TabIndex = 0x24;
            this.btnLittleEqual.Text = "2";
            this.btnLittleEqual.Click += new EventHandler(this.simpleButton4_Click);
            this.btnLittle.Location = new Point(0xc3, 0x109);
            this.btnLittle.Name = "btnLittle";
            this.btnLittle.Size = new Size(0x20, 0x18);
            this.btnLittle.TabIndex = 0x23;
            this.btnLittle.Text = "1";
            this.btnLittle.Click += new EventHandler(this.simpleButton4_Click);
            this.btnAnd.Location = new Point(0x113, 0xe9);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new Size(0x20, 0x18);
            this.btnAnd.TabIndex = 0x22;
            this.btnAnd.Text = "6";
            this.btnAnd.Click += new EventHandler(this.simpleButton4_Click);
            this.btnLike.Location = new Point(0x113, 0xc9);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new Size(0x20, 0x18);
            this.btnLike.TabIndex = 0x21;
            this.btnLike.Text = "9";
            this.btnLike.Click += new EventHandler(this.simpleButton4_Click);
            this.btnGreat.Location = new Point(0xc3, 0xe9);
            this.btnGreat.Name = "btnGreat";
            this.btnGreat.Size = new Size(0x20, 0x18);
            this.btnGreat.TabIndex = 0x20;
            this.btnGreat.Text = "4";
            this.btnGreat.Click += new EventHandler(this.simpleButton4_Click);
            this.btnNotEqual.Location = new Point(0xeb, 0xc9);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new Size(0x20, 0x18);
            this.btnNotEqual.TabIndex = 0x1f;
            this.btnNotEqual.Text = "8";
            this.btnNotEqual.Click += new EventHandler(this.simpleButton4_Click);
            this.btnGreatEqual.Location = new Point(0xeb, 0xe9);
            this.btnGreatEqual.Name = "btnGreatEqual";
            this.btnGreatEqual.Size = new Size(0x20, 0x18);
            this.btnGreatEqual.TabIndex = 30;
            this.btnGreatEqual.Text = "5";
            this.btnGreatEqual.Click += new EventHandler(this.simpleButton4_Click);
            this.btnEqual.Location = new Point(0xc3, 0xc9);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new Size(0x20, 0x18);
            this.btnEqual.TabIndex = 0x1d;
            this.btnEqual.Text = "7";
            this.btnEqual.Click += new EventHandler(this.simpleButton4_Click);
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x121, 0x151);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x38, 0x18);
            this.btnClose.TabIndex = 0x1c;
            this.btnClose.Text = "取消";
            this.btnApply.Location = new Point(0xd1, 0x151);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 0x1b;
            this.btnApply.Text = "确定";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.memEditWhereCaluse.EditValue = "";
            this.memEditWhereCaluse.Location = new Point(0x12, 0xc9);
            this.memEditWhereCaluse.Name = "memEditWhereCaluse";
            this.memEditWhereCaluse.Size = new Size(0x9f, 0x76);
            this.memEditWhereCaluse.TabIndex = 0x1a;
            this.Fieldlist.ItemHeight = 0x11;
            this.Fieldlist.Location = new Point(0x10, 0x18);
            this.Fieldlist.Name = "Fieldlist";
            this.Fieldlist.Size = new Size(0xa1, 0x93);
            this.Fieldlist.TabIndex = 0x18;
            this.Fieldlist.DoubleClick += new EventHandler(this.Fieldlist_DoubleClick);
            this.Fieldlist.SelectedIndexChanged += new EventHandler(this.Fieldlist_SelectedIndexChanged);
            this.lbl.AutoSize = true;
            this.lbl.Location = new Point(0xb5, 8);
            this.lbl.Name = "lbl";
            this.lbl.Size = new Size(0x1d, 12);
            this.lbl.TabIndex = 0x30;
            this.lbl.Text = "函数";
            this.lstFunction.Enabled = false;
            this.lstFunction.ItemHeight = 0x11;
            this.lstFunction.Items.AddRange(new object[] { "Abs (  )", "Atn (  )", "Cos (  )", "Exp (  )", " Fix (  )", "Int (  )", "Log (  ) ", "Sin (  )", "Sqr (  )", "Tan (  )" });
            this.lstFunction.Location = new Point(0xb7, 0x18);
            this.lstFunction.Name = "lstFunction";
            this.lstFunction.Size = new Size(0xa2, 0x93);
            this.lstFunction.TabIndex = 0x2f;
            this.lstFunction.DoubleClick += new EventHandler(this.lstFunction_DoubleClick);
            this.btnClear.Location = new Point(0x12, 0x15b);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x38, 0x18);
            this.btnClear.TabIndex = 50;
            this.btnClear.Text = "清除";
            this.btnClear.Visible = false;
            this.btnValidate.Location = new Point(0x52, 0x15b);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new Size(0x38, 0x18);
            this.btnValidate.TabIndex = 0x33;
            this.btnValidate.Text = "验证";
            this.btnValidate.Visible = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x12, 0xb6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x34;
            this.label1.Text = "表达式";
            this.simpleButton1.Location = new Point(0x139, 0x127);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x20, 0x18);
            this.simpleButton1.TabIndex = 0x38;
            this.simpleButton1.Text = "+";
            this.simpleButton1.Click += new EventHandler(this.simpleButton4_Click);
            this.simpleButton2.Location = new Point(0x139, 0x109);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x20, 0x18);
            this.simpleButton2.TabIndex = 0x37;
            this.simpleButton2.Text = "-";
            this.simpleButton2.Click += new EventHandler(this.simpleButton4_Click);
            this.simpleButton3.Location = new Point(0x139, 0xe9);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x20, 0x18);
            this.simpleButton3.TabIndex = 0x36;
            this.simpleButton3.Text = "/";
            this.simpleButton3.Click += new EventHandler(this.simpleButton4_Click);
            this.simpleButton4.Location = new Point(0x139, 0xc9);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new Size(0x20, 0x18);
            this.simpleButton4.TabIndex = 0x35;
            this.simpleButton4.Text = "*";
            this.simpleButton4.Click += new EventHandler(this.simpleButton4_Click);
            base.ClientSize = new Size(0x163, 0x178);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton3);
            base.Controls.Add(this.simpleButton4);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnValidate);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.lbl);
            base.Controls.Add(this.lstFunction);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnIs);
            base.Controls.Add(this.btnNot);
            base.Controls.Add(this.btnBracket);
            base.Controls.Add(this.btnOr);
            base.Controls.Add(this.btnLittleEqual);
            base.Controls.Add(this.btnLittle);
            base.Controls.Add(this.btnAnd);
            base.Controls.Add(this.btnLike);
            base.Controls.Add(this.btnGreat);
            base.Controls.Add(this.btnNotEqual);
            base.Controls.Add(this.btnGreatEqual);
            base.Controls.Add(this.btnEqual);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.memEditWhereCaluse);
            base.Controls.Add(this.Fieldlist);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExpressionBuild";
            this.Text = "表达式构造器";
            base.Load += new EventHandler(this.frmExpressionBuild_Load);
            this.memEditWhereCaluse.Properties.EndInit();
            ((ISupportInitialize) this.Fieldlist).EndInit();
            ((ISupportInitialize) this.lstFunction).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lstFunction_DoubleClick(object sender, EventArgs e)
        {
            string selectedItem = this.lstFunction.SelectedItem as string;
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + selectedItem + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + selectedItem.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_0(object sender, EventArgs e)
        {
            string str = "";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, str);
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 1;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private ITable method_1()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_10(object sender, EventArgs e)
        {
            string str = "OR";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_11(object sender, EventArgs e)
        {
            string str = "";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            this.memEditWhereCaluse.Focus();
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_12(object sender, EventArgs e)
        {
            string text = this.btnBracket.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_13(object sender, EventArgs e)
        {
            string str = "NOT";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_14(object sender, EventArgs e)
        {
            string str = "IS";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_15(object sender, EventArgs e)
        {
            if ((this.itable_0 != null) && (this.Fieldlist.SelectedItem != null))
            {
                this.lstFunction.Enabled = true;
                this.lstFunction.Items.Clear();
                this.Fieldlist.SelectedItem.ToString();
                this.GetUniqueValues(this.itable_0, this.Fieldlist.SelectedItem.ToString(), this.lstFunction.Items);
            }
        }

        private void method_16(object sender, EventArgs e)
        {
            this.memEditWhereCaluse.Text = "";
        }

        private void method_17()
        {
            if (this.itable_0 != null)
            {
                this.method_18(this.itable_0, this.Fieldlist.Items);
            }
        }

        private void method_18(ITable itable_1, ListBoxItemCollection listBoxItemCollection_0)
        {
            listBoxItemCollection_0.Clear();
            string str = "";
            string str2 = "";
            try
            {
                if ((itable_1 as IDataset).Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    str = "[";
                    str2 = "]";
                }
                IFields fields = itable_1.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if ((((field.Type == esriFieldType.esriFieldTypeInteger) || (field.Type == esriFieldType.esriFieldTypeSingle)) || (field.Type == esriFieldType.esriFieldTypeDouble)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                    {
                        listBoxItemCollection_0.Add(str + field.Name + str2);
                    }
                }
            }
            catch
            {
            }
        }

        private ILayer method_19(IBasicMap ibasicMap_0, string string_2)
        {
            ILayer layer = null;
            for (int i = 0; i < ibasicMap_0.LayerCount; i++)
            {
                layer = ibasicMap_0.get_Layer(i);
                if (layer.Name == string_2)
                {
                    return layer;
                }
            }
            return null;
        }

        private void method_2(object sender, EventArgs e)
        {
            string text = this.btnEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_3(object sender, EventArgs e)
        {
            string text = this.btnNotEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_4(object sender, EventArgs e)
        {
            string str = "LIKE";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_5(object sender, EventArgs e)
        {
            string text = this.btnGreat.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_6(object sender, EventArgs e)
        {
            string text = this.btnGreatEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_7(object sender, EventArgs e)
        {
            string str = "AND";
            string text = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                text = text.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = text.Insert(selectionStart, " " + str + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + str.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_8(object sender, EventArgs e)
        {
            string text = this.btnLittle.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void method_9(object sender, EventArgs e)
        {
            string text = this.btnLittleEqual.Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string text = (sender as SimpleButton).Text;
            string str2 = this.memEditWhereCaluse.Text;
            int selectionStart = this.memEditWhereCaluse.SelectionStart;
            if (this.memEditWhereCaluse.SelectionLength > 0)
            {
                str2 = str2.Remove(selectionStart, this.memEditWhereCaluse.SelectionLength);
            }
            this.memEditWhereCaluse.Text = str2.Insert(selectionStart, " " + text + " ");
            this.memEditWhereCaluse.Focus();
            this.memEditWhereCaluse.SelectionStart = (selectionStart + text.Length) + 2;
            this.memEditWhereCaluse.SelectionLength = 0;
        }

        public ILayer CurrentLayer
        {
            set
            {
                this.ilayer_0 = value;
                this.itable_0 = this.method_1();
                this.string_1 = this.ilayer_0.Name;
                if (this.bool_0)
                {
                    this.method_17();
                }
            }
        }

        public string Expression
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public ITable Table
        {
            set
            {
                this.itable_0 = value;
                this.string_1 = (this.itable_0 as IDataset).Name;
            }
        }
    }
}

