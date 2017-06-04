using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.ZD
{
	public class frmSelectEditZD : System.Windows.Forms.Form
	{
		private IContainer icontainer_0 = null;

		private System.Windows.Forms.Label label1;

		private System.Windows.Forms.ComboBox comboBox1;

		private System.Windows.Forms.Button btnOK;

		private System.Windows.Forms.Button button2;

		[System.Runtime.CompilerServices.CompilerGenerated]
		private System.Collections.Generic.List<IFeatureLayer> list_0;

		[System.Runtime.CompilerServices.CompilerGenerated]
		private IFeatureLayer ifeatureLayer_0;

		public System.Collections.Generic.List<IFeatureLayer> FeatureLayers
		{
			get;
			set;
		}

		public IFeatureLayer SelectFeatureLayer
		{
			get;
			protected set;
		}

		public frmSelectEditZD()
		{
			this.InitializeComponent();
		}

		private void frmSelectEditZD_Load(object sender, System.EventArgs e)
		{
			foreach (IFeatureLayer current in this.FeatureLayers)
			{
				this.comboBox1.Items.Add(new LayerObject(current));
			}
			this.comboBox1.SelectedIndex = 0;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if (this.comboBox1.SelectedIndex == -1)
			{
				System.Windows.Forms.MessageBox.Show("请选择要编辑的宗地图层!");
			}
			else
			{
				this.SelectFeatureLayer = ((this.comboBox1.SelectedItem as LayerObject).Layer as IFeatureLayer);
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 22);
			this.label1.Name = "label1";
			this.label1.Size = new Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "宗地图层:";
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new Point(78, 19);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new Size(160, 20);
			this.comboBox1.TabIndex = 1;
			this.btnOK.Location = new Point(78, 45);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new Point(163, 45);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(249, 71);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.comboBox1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSelectEditZD";
			this.Text = "选择编辑宗地图层";
			base.Load += new System.EventHandler(this.frmSelectEditZD_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
