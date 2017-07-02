
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace CartoTemplateApp
{
	public class frmNewMapTemplateClass : Form
	{
		private IContainer components = null;

		private Button btnOK;

		private Button button2;

		private Label label1;

		private Label label2;

		private TextBox txtName;

		private TextBox txtDescription;

		public string Description
		{
			get
			{
				return this.txtDescription.Text;
			}
			set
			{
				this.txtDescription.Text = value;
			}
		}

		public string MapTemplateClassName
		{
			get
			{
				return this.txtName.Text;
			}
			set
			{
				this.txtName.Text = value;
			}
		}

		public MapTemplateGallery MapTemplateGallery
		{
			get;
			set;
		}

		public frmNewMapTemplateClass()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.txtName.Text.Trim().Length == 0)
			{
				MessageBox.Show("请输入模板类别名称!");
			}
			else if (!this.MapTemplateGallery.MapTemplateClassIsExist(this.txtName.Text))
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else
			{
				MessageBox.Show("已存在该模板类别名称，请输入其他模板类别名称!");
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewMapTemplateClass));
			this.btnOK = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.txtName = new TextBox();
			this.txtDescription = new TextBox();
			base.SuspendLayout();
			this.btnOK.Location = new Point(94, 201);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new Point(198, 200);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "名称";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(15, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "描述";
			this.txtName.Location = new Point(75, 13);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(189, 21);
			this.txtName.TabIndex = 4;
			this.txtDescription.Location = new Point(75, 43);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(189, 143);
			this.txtDescription.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 239);
			base.Controls.Add(this.txtDescription);
			base.Controls.Add(this.txtName);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmNewMapTemplateClass";
			this.Text = "地图模板类别";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}