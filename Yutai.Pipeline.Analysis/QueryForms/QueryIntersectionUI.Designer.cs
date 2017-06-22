
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	    partial class QueryIntersectionUI
    {
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
			this.components = new Container();
			this.btnQuery = new Button();
			this.btnCancel = new Button();
			this.timer1 = new Timer(this.components);
			this.comboRoad1 = new ComboBox();
			this.label3 = new Label();
			this.label1 = new Label();
			this.comboRoad2 = new ComboBox();
			base.SuspendLayout();
			this.btnQuery.Location = new System.Drawing.Point(136, 106);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new Size(75, 23);
			this.btnQuery.TabIndex = 0;
			this.btnQuery.Text = "查询";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new EventHandler(this.btnQuery_Click);
			this.btnCancel.Location = new System.Drawing.Point(217, 106);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "关闭";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.comboRoad1.FormattingEnabled = true;
			this.comboRoad1.Location = new System.Drawing.Point(92, 37);
			this.comboRoad1.Name = "comboRoad1";
			this.comboRoad1.Size = new Size(200, 20);
			this.comboRoad1.TabIndex = 5;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 40);
			this.label3.Name = "label3";
			this.label3.Size = new Size(65, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "所选道路：";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 66);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "所选道路：";
			this.comboRoad2.FormattingEnabled = true;
			this.comboRoad2.Location = new System.Drawing.Point(92, 63);
			this.comboRoad2.Name = "comboRoad2";
			this.comboRoad2.Size = new Size(200, 20);
			this.comboRoad2.TabIndex = 5;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(314, 169);
			base.Controls.Add(this.comboRoad2);
			base.Controls.Add(this.comboRoad1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnQuery);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "QueryIntersectionUI";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "道路交叉口查询";
			base.Load += new EventHandler(this.QueryIntersectionUI_Load);
			base.Activated += new EventHandler(this.QueryIntersectionUI_Activated);
			base.Enter += new EventHandler(this.QueryIntersectionUI_Enter);
			base.FormClosed += new FormClosedEventHandler(this.QueryIntersectionUI_FormClosed);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private IContainer components = null;
		private Button btnQuery;
		private Button btnCancel;
		private Timer timer1;
		private ComboBox comboRoad1;
		private Label label3;
		private Label label1;
		private ComboBox comboRoad2;
		private IFeatureLayer m_pFtLayer;
		private IGeometry m_pGeoFlash;
    }
}