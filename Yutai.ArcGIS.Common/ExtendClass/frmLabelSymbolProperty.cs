using System.ComponentModel;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	public class frmLabelSymbolProperty : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		public frmLabelSymbolProperty()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Text = "frmLabelSymbolProperty";
		}
	}
}