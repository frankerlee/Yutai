using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapGridTypeProperty
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
            this.groupBox2 = new GroupBox();
            this.txtMapGridName = new TextEdit();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioMapGridType = new RadioGroup();
            this.groupBox2.SuspendLayout();
            this.txtMapGridName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioMapGridType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.txtMapGridName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(216, 72);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.txtMapGridName.EditValue = "";
            this.txtMapGridName.Location = new System.Drawing.Point(72, 30);
            this.txtMapGridName.Name = "txtMapGridName";
            this.txtMapGridName.Size = new Size(128, 21);
            this.txtMapGridName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "格网名称";
            this.groupBox1.Controls.Add(this.radioMapGridType);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(216, 96);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "格网类型";
            this.radioMapGridType.Location = new System.Drawing.Point(16, 16);
            this.radioMapGridType.Name = "radioMapGridType";
            this.radioMapGridType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioMapGridType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "经纬网"), new RadioGroupItem(null, "方里网"), new RadioGroupItem(null, "参考格网") });
            this.radioMapGridType.Size = new Size(160, 72);
            this.radioMapGridType.TabIndex = 1;
            this.radioMapGridType.SelectedIndexChanged += new EventHandler(this.radioMapGridType_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridTypeProperty";
            base.Size = new Size(248, 200);
            this.groupBox2.ResumeLayout(false);
            this.txtMapGridName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioMapGridType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private RadioGroup radioMapGridType;
        private TextEdit txtMapGridName;
    }
}