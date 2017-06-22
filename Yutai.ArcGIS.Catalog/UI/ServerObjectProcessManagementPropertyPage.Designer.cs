using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class ServerObjectProcessManagementPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtRecycleInterval = new TextEdit();
            this.timeEdit1 = new TimeEdit();
            this.cboIsolationLevel = new ComboBoxEdit();
            this.txtRecycleInterval.Properties.BeginInit();
            this.timeEdit1.Properties.BeginInit();
            this.cboIsolationLevel.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置实例数目";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new Size(456, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "回收进程每隔一段时间将停止所有的进程并且把它们重新启动,以提高性能和稳定性";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(24, 112);
            this.label3.Name = "label3";
            this.label3.Size = new Size(110, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "回收进程时间间隔:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(24, 152);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "开始:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(256, 112);
            this.label5.Name = "label5";
            this.label5.Size = new Size(29, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "小时";
            this.txtRecycleInterval.EditValue = "10";
            this.txtRecycleInterval.Location = new Point(136, 104);
            this.txtRecycleInterval.Name = "txtRecycleInterval";
            this.txtRecycleInterval.Size = new Size(96, 23);
            this.txtRecycleInterval.TabIndex = 6;
            this.txtRecycleInterval.EditValueChanged += new EventHandler(this.txtRecycleInterval_EditValueChanged);
            this.timeEdit1.EditValue = new DateTime(2006, 1, 27, 23, 5, 0, 0);
            this.timeEdit1.Location = new Point(136, 152);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.timeEdit1.Properties.DisplayFormat.FormatString = "t";
            this.timeEdit1.Properties.DisplayFormat.FormatType = FormatType.Custom;
            this.timeEdit1.Properties.EditFormat.FormatString = "h.m";
            this.timeEdit1.Size = new Size(120, 23);
            this.timeEdit1.TabIndex = 7;
            this.timeEdit1.EditValueChanged += new EventHandler(this.timeEdit1_EditValueChanged);
            this.cboIsolationLevel.EditValue = "每一个实例一个进程(高孤立性)";
            this.cboIsolationLevel.Location = new Point(16, 32);
            this.cboIsolationLevel.Name = "cboIsolationLevel";
            this.cboIsolationLevel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboIsolationLevel.Properties.Items.AddRange(new object[] { "每一个实例一个进程(高孤立性)", "仅一个进程(低孤立性)" });
            this.cboIsolationLevel.Size = new Size(264, 23);
            this.cboIsolationLevel.TabIndex = 8;
            this.cboIsolationLevel.SelectedIndexChanged += new EventHandler(this.cboIsolationLevel_SelectedIndexChanged);
            base.Controls.Add(this.cboIsolationLevel);
            base.Controls.Add(this.timeEdit1);
            base.Controls.Add(this.txtRecycleInterval);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ServerObjectProcessManagementPropertyPage";
            base.Size = new Size(496, 256);
            base.Load += new EventHandler(this.ServerObjectProcessManagementPropertyPage_Load);
            this.txtRecycleInterval.Properties.EndInit();
            this.timeEdit1.Properties.EndInit();
            this.cboIsolationLevel.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit cboIsolationLevel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TimeEdit timeEdit1;
        private TextEdit txtRecycleInterval;
    }
}