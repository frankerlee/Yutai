
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
            this.components = new System.ComponentModel.Container();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboRoad1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboRoad2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(92, 64);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(227, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboRoad1
            // 
            this.comboRoad1.FormattingEnabled = true;
            this.comboRoad1.Location = new System.Drawing.Point(92, 12);
            this.comboRoad1.Name = "comboRoad1";
            this.comboRoad1.Size = new System.Drawing.Size(210, 20);
            this.comboRoad1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "所选道路：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "所选道路：";
            // 
            // comboRoad2
            // 
            this.comboRoad2.FormattingEnabled = true;
            this.comboRoad2.Location = new System.Drawing.Point(92, 38);
            this.comboRoad2.Name = "comboRoad2";
            this.comboRoad2.Size = new System.Drawing.Size(210, 20);
            this.comboRoad2.TabIndex = 5;
            // 
            // QueryIntersectionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 97);
            this.Controls.Add(this.comboRoad2);
            this.Controls.Add(this.comboRoad1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryIntersectionUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "道路交叉口查询";
            this.Activated += new System.EventHandler(this.QueryIntersectionUI_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryIntersectionUI_FormClosed);
            this.Load += new System.EventHandler(this.QueryIntersectionUI_Load);
            this.Enter += new System.EventHandler(this.QueryIntersectionUI_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

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