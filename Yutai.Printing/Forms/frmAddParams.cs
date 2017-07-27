
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace CartoTemplateApp
{
	public class frmAddParams : Form
	{
		private IContainer components = null;

		private Button button1;

		private Button button2;

		private Label label1;

		private Label label2;

		private TextBox textBox1;

		private CheckBox checkBox1;

		private ComboBox comboBox1;

		public MapTemplateParam MapTemplateParam
		{
			get;
			set;
		}

		public frmAddParams()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.MapTemplateParam != null)
			{
				this.MapTemplateParam.AllowNull = this.checkBox1.Checked;
				this.MapTemplateParam.Name = this.textBox1.Text;
				this.MapTemplateParam.ParamDataType = (DataType)this.comboBox1.SelectedIndex;
			}
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmAddParams_Load(object sender, EventArgs e)
		{
			if (this.MapTemplateParam != null)
			{
				this.checkBox1.Checked = this.MapTemplateParam.AllowNull;
				this.textBox1.Text = this.MapTemplateParam.Name;
				this.comboBox1.SelectedIndex = (int)this.MapTemplateParam.ParamDataType;
			}
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddParams));
			this.button1 = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.textBox1 = new TextBox();
			this.checkBox1 = new CheckBox();
			this.comboBox1 = new ComboBox();
			base.SuspendLayout();
			this.button1.Location = new Point(81, 126);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new Point(186, 126);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "参数名称";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(13, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "数据类型";
			this.textBox1.Location = new Point(72, 10);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(184, 21);
			this.textBox1.TabIndex = 4;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new Point(15, 80);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 16);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "允许空值";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] { "Boolean", "DateTime", "Interger", "Float", "String" });
			this.comboBox1.Location = new Point(72, 44);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(184, 20);
			this.comboBox1.TabIndex = 6;
			this.comboBox1.Text = "String";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(279, 156);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmAddParams";
			this.Text = "参数";
			base.Load += new EventHandler(this.frmAddParams_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}