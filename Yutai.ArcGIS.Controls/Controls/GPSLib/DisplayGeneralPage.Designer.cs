using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class DisplayGeneralPage
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.chkShowCurrentPosition = new CheckBox();
            this.chkShowEstimatedPosition = new CheckBox();
            this.chkUseMinimumDisplayRate = new CheckBox();
            this.btnBaseMarkerSymbol = new NewSymbolButton();
            this.btnEstimatedPositionSymbol = new NewSymbolButton();
            this.txtMinimumDisplayRate = new DomainUpDown();
            this.label4 = new Label();
            this.cboAltitudeUnits = new ComboBox();
            this.cboSpeedUnits = new ComboBox();
            this.cboLatLongDisplayFormat = new ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMinimumDisplayRate);
            this.groupBox1.Controls.Add(this.btnEstimatedPositionSymbol);
            this.groupBox1.Controls.Add(this.btnBaseMarkerSymbol);
            this.groupBox1.Controls.Add(this.chkUseMinimumDisplayRate);
            this.groupBox1.Controls.Add(this.chkShowEstimatedPosition);
            this.groupBox1.Controls.Add(this.chkShowCurrentPosition);
            this.groupBox1.Location = new Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(340, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前位置";
            this.groupBox2.Controls.Add(this.cboLatLongDisplayFormat);
            this.groupBox2.Controls.Add(this.cboSpeedUnits);
            this.groupBox2.Controls.Add(this.cboAltitudeUnits);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(15, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(340, 112);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单位";
            this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "高度单位";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "速度单位";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 77);
            this.label3.Name = "label3";
            this.label3.Size = new Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "经纬度格式";
            this.chkShowCurrentPosition.AutoSize = true;
            this.chkShowCurrentPosition.Location = new Point(10, 27);
            this.chkShowCurrentPosition.Name = "chkShowCurrentPosition";
            this.chkShowCurrentPosition.Size = new Size(168, 16);
            this.chkShowCurrentPosition.TabIndex = 0;
            this.chkShowCurrentPosition.Text = "使用以下符号显示当前位置";
            this.chkShowCurrentPosition.UseVisualStyleBackColor = true;
            this.chkShowCurrentPosition.CheckedChanged += new EventHandler(this.chkShowCurrentPosition_CheckedChanged);
            this.chkShowEstimatedPosition.AutoSize = true;
            this.chkShowEstimatedPosition.Location = new Point(10, 63);
            this.chkShowEstimatedPosition.Name = "chkShowEstimatedPosition";
            this.chkShowEstimatedPosition.Size = new Size(192, 16);
            this.chkShowEstimatedPosition.TabIndex = 1;
            this.chkShowEstimatedPosition.Text = "如果信息号丢失，显示估计位置";
            this.chkShowEstimatedPosition.UseVisualStyleBackColor = true;
            this.chkShowEstimatedPosition.CheckedChanged += new EventHandler(this.chkShowEstimatedPosition_CheckedChanged);
            this.chkUseMinimumDisplayRate.AutoSize = true;
            this.chkUseMinimumDisplayRate.Location = new Point(10, 108);
            this.chkUseMinimumDisplayRate.Name = "chkUseMinimumDisplayRate";
            this.chkUseMinimumDisplayRate.Size = new Size(96, 16);
            this.chkUseMinimumDisplayRate.TabIndex = 2;
            this.chkUseMinimumDisplayRate.Text = "最小显示比率";
            this.chkUseMinimumDisplayRate.UseVisualStyleBackColor = true;
            this.chkUseMinimumDisplayRate.CheckedChanged += new EventHandler(this.chkUseMinimumDisplayRate_CheckedChanged);
            this.btnBaseMarkerSymbol.Location = new Point(222, 19);
            this.btnBaseMarkerSymbol.Name = "btnBaseMarkerSymbol";
            this.btnBaseMarkerSymbol.Size = new Size(73, 31);
            this.btnBaseMarkerSymbol.TabIndex = 3;
            this.btnBaseMarkerSymbol.Click += new EventHandler(this.btnBaseMarkerSymbol_Click);
            this.btnEstimatedPositionSymbol.Location = new Point(222, 56);
            this.btnEstimatedPositionSymbol.Name = "btnEstimatedPositionSymbol";
            this.btnEstimatedPositionSymbol.Size = new Size(75, 29);
            this.btnEstimatedPositionSymbol.TabIndex = 4;
            this.btnEstimatedPositionSymbol.Click += new EventHandler(this.btnEstimatedPositionSymbol_Click);
            this.txtMinimumDisplayRate.Location = new Point(151, 103);
            this.txtMinimumDisplayRate.Name = "txtMinimumDisplayRate";
            this.txtMinimumDisplayRate.Size = new Size(116, 21);
            this.txtMinimumDisplayRate.TabIndex = 5;
            this.txtMinimumDisplayRate.Text = "1";
            this.txtMinimumDisplayRate.TextChanged += new EventHandler(this.txtMinimumDisplayRate_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(278, 109);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "秒";
            this.cboAltitudeUnits.FormattingEnabled = true;
            this.cboAltitudeUnits.Location = new Point(83, 20);
            this.cboAltitudeUnits.Name = "cboAltitudeUnits";
            this.cboAltitudeUnits.Size = new Size(214, 20);
            this.cboAltitudeUnits.TabIndex = 3;
            this.cboAltitudeUnits.Items.AddRange(new object[] { "未知单位", "英寸", "点", "英尺", "码", "英里", "海里", "毫米", "厘米", "米", "公里", "度", "分米" });
            this.cboAltitudeUnits.SelectedIndexChanged += new EventHandler(this.cboAltitudeUnits_SelectedIndexChanged);
            this.cboSpeedUnits.FormattingEnabled = true;
            this.cboSpeedUnits.Location = new Point(83, 48);
            this.cboSpeedUnits.Name = "cboSpeedUnits";
            this.cboSpeedUnits.Size = new Size(212, 20);
            this.cboSpeedUnits.TabIndex = 4;
            this.cboSpeedUnits.Items.AddRange(new object[] { "公里/小时", "英里/小时", "米/秒", "英尺/秒", "节" });
            this.cboSpeedUnits.SelectedIndexChanged += new EventHandler(this.cboSpeedUnits_SelectedIndexChanged);
            this.cboLatLongDisplayFormat.FormattingEnabled = true;
            this.cboLatLongDisplayFormat.Location = new Point(83, 77);
            this.cboLatLongDisplayFormat.Name = "cboLatLongDisplayFormat";
            this.cboLatLongDisplayFormat.Size = new Size(212, 20);
            this.cboLatLongDisplayFormat.TabIndex = 5;
            this.cboLatLongDisplayFormat.Items.AddRange(new object[] { "度分秒", "度分", "度" });
            this.cboLatLongDisplayFormat.SelectedIndexChanged += new EventHandler(this.cboLatLongDisplayFormat_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "DisplayGeneralPage";
            base.Size = new Size(381, 270);
            base.Load += new EventHandler(this.DisplayGeneralPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private IContainer components = null;
        private NewSymbolButton btnBaseMarkerSymbol;
        private NewSymbolButton btnEstimatedPositionSymbol;
        private ComboBox cboAltitudeUnits;
        private ComboBox cboLatLongDisplayFormat;
        private ComboBox cboSpeedUnits;
        private CheckBox chkShowCurrentPosition;
        private CheckBox chkShowEstimatedPosition;
        private CheckBox chkUseMinimumDisplayRate;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DomainUpDown txtMinimumDisplayRate;
    }
}