using System.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class UcAttributeQueryBuilder
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.btnMatchString = new System.Windows.Forms.Button();
            this.btnIs = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnBracket = new System.Windows.Forms.Button();
            this.btnMatchOneChar = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnLittleEqual = new System.Windows.Forms.Button();
            this.btnLittle = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnGreat = new System.Windows.Forms.Button();
            this.btnNotEqual = new System.Windows.Forms.Button();
            this.btnGreatEqual = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.memEditWhereCaluse = new System.Windows.Forms.TextBox();
            this.textEdit1 = new System.Windows.Forms.TextBox();
            this.Fieldlist = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UniqueValuelist = new System.Windows.Forms.ListBox();
            this.btnGetUniqueValue = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "字段";
            // 
            // btnMatchString
            // 
            this.btnMatchString.Location = new System.Drawing.Point(144, 120);
            this.btnMatchString.Name = "btnMatchString";
            this.btnMatchString.Size = new System.Drawing.Size(16, 24);
            this.btnMatchString.TabIndex = 42;
            this.btnMatchString.Text = "%";
            this.btnMatchString.Click += new System.EventHandler(this.btnMatchString_Click);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(128, 152);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new System.Drawing.Size(32, 24);
            this.btnIs.TabIndex = 41;
            this.btnIs.Text = "&Is";
            this.btnIs.Click += new System.EventHandler(this.btnIs_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(208, 120);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(32, 24);
            this.btnNot.TabIndex = 40;
            this.btnNot.Text = "&Not";
            this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(168, 120);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(32, 24);
            this.btnBracket.TabIndex = 39;
            this.btnBracket.Text = "()";
            this.btnBracket.Click += new System.EventHandler(this.btnBracket_Click);
            // 
            // btnMatchOneChar
            // 
            this.btnMatchOneChar.Location = new System.Drawing.Point(128, 120);
            this.btnMatchOneChar.Name = "btnMatchOneChar";
            this.btnMatchOneChar.Size = new System.Drawing.Size(16, 24);
            this.btnMatchOneChar.TabIndex = 38;
            this.btnMatchOneChar.Text = "_";
            this.btnMatchOneChar.Click += new System.EventHandler(this.btnMatchOneChar_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(208, 88);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(32, 24);
            this.btnOr.TabIndex = 37;
            this.btnOr.Text = "&Or";
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnLittleEqual
            // 
            this.btnLittleEqual.Location = new System.Drawing.Point(168, 88);
            this.btnLittleEqual.Name = "btnLittleEqual";
            this.btnLittleEqual.Size = new System.Drawing.Size(32, 24);
            this.btnLittleEqual.TabIndex = 36;
            this.btnLittleEqual.Text = "<=";
            this.btnLittleEqual.Click += new System.EventHandler(this.btnLittleEqual_Click);
            // 
            // btnLittle
            // 
            this.btnLittle.Location = new System.Drawing.Point(128, 88);
            this.btnLittle.Name = "btnLittle";
            this.btnLittle.Size = new System.Drawing.Size(32, 24);
            this.btnLittle.TabIndex = 35;
            this.btnLittle.Text = "<";
            this.btnLittle.Click += new System.EventHandler(this.btnLittle_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(208, 56);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(32, 24);
            this.btnAnd.TabIndex = 34;
            this.btnAnd.Text = "&And";
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(208, 24);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(32, 24);
            this.btnLike.TabIndex = 33;
            this.btnLike.Text = "Li&ke";
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnGreat
            // 
            this.btnGreat.Location = new System.Drawing.Point(128, 56);
            this.btnGreat.Name = "btnGreat";
            this.btnGreat.Size = new System.Drawing.Size(32, 24);
            this.btnGreat.TabIndex = 32;
            this.btnGreat.Text = ">";
            this.btnGreat.Click += new System.EventHandler(this.btnGreat_Click);
            // 
            // btnNotEqual
            // 
            this.btnNotEqual.Location = new System.Drawing.Point(168, 24);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new System.Drawing.Size(32, 24);
            this.btnNotEqual.TabIndex = 31;
            this.btnNotEqual.Text = "<>";
            this.btnNotEqual.Click += new System.EventHandler(this.btnNotEqual_Click);
            // 
            // btnGreatEqual
            // 
            this.btnGreatEqual.Location = new System.Drawing.Point(168, 56);
            this.btnGreatEqual.Name = "btnGreatEqual";
            this.btnGreatEqual.Size = new System.Drawing.Size(32, 24);
            this.btnGreatEqual.TabIndex = 30;
            this.btnGreatEqual.Text = ">=";
            this.btnGreatEqual.Click += new System.EventHandler(this.btnGreatEqual_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(128, 24);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(32, 24);
            this.btnEqual.TabIndex = 29;
            this.btnEqual.Text = "=";
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(208, 198);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 24);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "关闭";
            this.btnClose.Visible = false;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(144, 198);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(56, 24);
            this.btnApply.TabIndex = 27;
            this.btnApply.Text = "应用";
            this.btnApply.Visible = false;
            // 
            // memEditWhereCaluse
            // 
            this.memEditWhereCaluse.Location = new System.Drawing.Point(16, 232);
            this.memEditWhereCaluse.Multiline = true;
            this.memEditWhereCaluse.Name = "memEditWhereCaluse";
            this.memEditWhereCaluse.Size = new System.Drawing.Size(352, 104);
            this.memEditWhereCaluse.TabIndex = 26;
            // 
            // textEdit1
            // 
            this.textEdit1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textEdit1.Enabled = false;
            this.textEdit1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEdit1.Location = new System.Drawing.Point(16, 200);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(352, 19);
            this.textEdit1.TabIndex = 25;
            // 
            // Fieldlist
            // 
            this.Fieldlist.ItemHeight = 12;
            this.Fieldlist.Location = new System.Drawing.Point(16, 24);
            this.Fieldlist.Name = "Fieldlist";
            this.Fieldlist.Size = new System.Drawing.Size(104, 160);
            this.Fieldlist.TabIndex = 24;
            this.Fieldlist.SelectedIndexChanged += new System.EventHandler(this.Fieldlist_SelectedIndexChanged);
            this.Fieldlist.DoubleClick += new System.EventHandler(this.Fieldlist_DoubleClick);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(248, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 48;
            this.label4.Text = "唯一值";
            // 
            // UniqueValuelist
            // 
            this.UniqueValuelist.Enabled = false;
            this.UniqueValuelist.ItemHeight = 12;
            this.UniqueValuelist.Location = new System.Drawing.Point(248, 24);
            this.UniqueValuelist.Name = "UniqueValuelist";
            this.UniqueValuelist.Size = new System.Drawing.Size(120, 136);
            this.UniqueValuelist.TabIndex = 47;
            this.UniqueValuelist.DoubleClick += new System.EventHandler(this.UniqueValuelist_DoubleClick);
            // 
            // btnGetUniqueValue
            // 
            this.btnGetUniqueValue.Location = new System.Drawing.Point(248, 168);
            this.btnGetUniqueValue.Name = "btnGetUniqueValue";
            this.btnGetUniqueValue.Size = new System.Drawing.Size(120, 24);
            this.btnGetUniqueValue.TabIndex = 49;
            this.btnGetUniqueValue.Text = "获取唯一值";
            this.btnGetUniqueValue.Click += new System.EventHandler(this.btnGetUniqueValue_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 198);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 24);
            this.btnClear.TabIndex = 50;
            this.btnClear.Text = "清除";
            this.btnClear.Visible = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(80, 198);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(56, 24);
            this.btnValidate.TabIndex = 51;
            this.btnValidate.Text = "验证";
            this.btnValidate.Visible = false;
            // 
            // UcAttributeQueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGetUniqueValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UniqueValuelist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnMatchString);
            this.Controls.Add(this.btnIs);
            this.Controls.Add(this.btnNot);
            this.Controls.Add(this.btnBracket);
            this.Controls.Add(this.btnMatchOneChar);
            this.Controls.Add(this.btnOr);
            this.Controls.Add(this.btnLittleEqual);
            this.Controls.Add(this.btnLittle);
            this.Controls.Add(this.btnAnd);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.btnGreat);
            this.Controls.Add(this.btnNotEqual);
            this.Controls.Add(this.btnGreatEqual);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.memEditWhereCaluse);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.Fieldlist);
            this.Name = "UcAttributeQueryBuilder";
            this.Size = new System.Drawing.Size(384, 350);
            this.Load += new System.EventHandler(this.UcAttributeQueryBulider_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;

        private Button btnOr;

        private Button btnLittleEqual;

        private Button btnLittle;

        private Button btnAnd;

        private Button btnLike;

        private Button btnGreat;

        private Button btnNotEqual;

        private Button btnGreatEqual;

        private Button btnEqual;

        private Button btnClose;

        private Button btnApply;

        private TextBox memEditWhereCaluse;

        private TextBox textEdit1;

        private ListBox Fieldlist;

        private Label label4;

        private Button btnGetUniqueValue;

        private Button btnMatchOneChar;

        private Button btnMatchString;

        private Button btnIs;

        private Button btnNot;

        private Button btnBracket;

        private ListBox UniqueValuelist;

        private Button btnClear;

        private Button btnValidate;
    }
}
