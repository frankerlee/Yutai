using System;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.Plugins.Editor.Forms
{
    internal class frmInputValue : XtraForm
	{
		private SimpleButton simpleButton_0;

		private TextEdit textEdit_0;

		private System.ComponentModel.Container container_0 = null;

		private double double_0 = 0;

		private esriUnits esriUnits_0 = esriUnits.esriUnknownUnits;

		public double InputValue
		{
			get
			{
				double num;
				num = (this.esriUnits_0 != esriUnits.esriDecimalDegrees ? this.double_0 : this.double_0 / 111194.874);
				return num;
			}
			set
			{
				this.double_0 = value;
			}
		}

		public esriUnits Unit
		{
			set
			{
				this.esriUnits_0 = value;
				if (this.esriUnits_0 != esriUnits.esriDecimalDegrees)
				{
					frmInputValue _frmInputValue = this;
					_frmInputValue.Text = string.Concat(_frmInputValue.Text, Common.GetUnit(this.esriUnits_0));
				}
				else
				{
					frmInputValue _frmInputValue1 = this;
					_frmInputValue1.Text = string.Concat(_frmInputValue1.Text, "米");
				}
			}
		}

		public frmInputValue()
		{
            InitializeComponent();
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void frmInputValue_Load(object sender, EventArgs e)
		{
			this.textEdit_0.Text = this.double_0.ToString();
		}

		private void InitializeComponent()
		{
            this.simpleButton_0 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit_0 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_0.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton_0
            // 
            this.simpleButton_0.Location = new System.Drawing.Point(59, -2);
            this.simpleButton_0.Name = "simpleButton_0";
            this.simpleButton_0.Size = new System.Drawing.Size(56, 24);
            this.simpleButton_0.TabIndex = 1;
            this.simpleButton_0.Text = "确认";
            this.simpleButton_0.Click += new System.EventHandler(this.simpleButton_0_Click);
            // 
            // textEdit_0
            // 
            this.textEdit_0.EditValue = "";
            this.textEdit_0.Location = new System.Drawing.Point(-1, 0);
            this.textEdit_0.Name = "textEdit_0";
            this.textEdit_0.Size = new System.Drawing.Size(236, 20);
            this.textEdit_0.TabIndex = 2;
            // 
            // frmInputValue
            // 
            this.AcceptButton = this.simpleButton_0;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(234, 22);
            this.Controls.Add(this.textEdit_0);
            this.Controls.Add(this.simpleButton_0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputValue";
            this.Load += new System.EventHandler(this.frmInputValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_0.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		private void simpleButton_0_Click(object sender, EventArgs e)
		{
			if (this.textEdit_0.Text.Trim().Length != 0)
			{
				try
				{
					this.double_0 = double.Parse(this.textEdit_0.Text);
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
				}
				catch
				{
					base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				}
			}
			else
			{
				base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			}
		}
	}
}