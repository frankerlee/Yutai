using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmGeoDBDataTransfer
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeoDBDataTransfer));
            this.panel1 = new Panel();
            this.simpleButton2 = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.txtObject = new TextEdit();
            this.txtObjectClass = new TextEdit();
            this.progressBarObject = new System.Windows.Forms.ProgressBar();
            this.progressBarObjectClass = new System.Windows.Forms.ProgressBar();
            this.label2 = new Label();
            this.listView1 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.lvcolumnHeader_2 = new LVColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.txtObject.Properties.BeginInit();
            this.txtObjectClass.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 267);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(416, 26);
            this.panel1.TabIndex = 2;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(288, 1);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "取消";
            this.btnOK.Location = new Point(216, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Controls.Add(this.txtObject);
            this.panel2.Controls.Add(this.txtObjectClass);
            this.panel2.Controls.Add(this.progressBarObject);
            this.panel2.Controls.Add(this.progressBarObjectClass);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(416, 97);
            this.panel2.TabIndex = 4;
            this.txtObject.EditValue = "";
            this.txtObject.Location = new Point(16, 80);
            this.txtObject.Name = "txtObject";
            this.txtObject.Properties.AllowFocused = false;
            this.txtObject.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtObject.Properties.Appearance.Options.UseBackColor = true;
            this.txtObject.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtObject.Properties.ReadOnly = true;
            this.txtObject.ShowToolTips = false;
            this.txtObject.Size = new Size(384, 19);
            this.txtObject.TabIndex = 11;
            this.txtObjectClass.EditValue = "";
            this.txtObjectClass.Location = new Point(288, 16);
            this.txtObjectClass.Name = "txtObjectClass";
            this.txtObjectClass.Properties.AllowFocused = false;
            this.txtObjectClass.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtObjectClass.Properties.Appearance.Options.UseBackColor = true;
            this.txtObjectClass.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtObjectClass.Properties.ReadOnly = true;
            this.txtObjectClass.Size = new Size(112, 19);
            this.txtObjectClass.TabIndex = 10;
            this.progressBarObject.Location = new Point(8, 104);
            this.progressBarObject.Name = "progressBarObject";
            this.progressBarObject.Size = new Size(392, 24);
            this.progressBarObject.TabIndex = 9;
            this.progressBarObjectClass.Location = new Point(16, 48);
            this.progressBarObjectClass.Name = "progressBarObjectClass";
            this.progressBarObjectClass.Size = new Size(384, 24);
            this.progressBarObjectClass.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new Size(143, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "正在传送数据，请稍候...";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1, this.lvcolumnHeader_2 });
            this.listView1.ComboBoxBgColor = Color.LightBlue;
            this.listView1.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.EditBgColor = Color.LightBlue;
            this.listView1.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(0, 0);
            this.listView1.LockRowCount = 0;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(416, 267);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.ValueChanged += new Common.ControlExtend.ValueChangedHandler(this.method_13);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "类型";
            this.lvcolumnHeader_0.Width = 98;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_1.Text = "源名";
            this.lvcolumnHeader_1.Width = 136;
            this.lvcolumnHeader_2.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_2.Text = "目标名";
            this.lvcolumnHeader_2.Width = 162;
            base.StartPosition = FormStartPosition.CenterParent;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(416, 293);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeoDBDataTransfer";
            this.Text = "数据传送";
            base.Load += new EventHandler(this.frmGeoDBDataTransfer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.txtObject.Properties.EndInit();
            this.txtObjectClass.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnOK;
        private Label label2;
        private EditListView listView1;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private LVColumnHeader lvcolumnHeader_2;
        private Panel panel1;
        private Panel panel2;
        private System.Windows.Forms.ProgressBar progressBarObject;
        private System.Windows.Forms.ProgressBar progressBarObjectClass;
        private SimpleButton simpleButton2;
        private Thread thread_0;
        private TextEdit txtObject;
        private TextEdit txtObjectClass;
    }
}