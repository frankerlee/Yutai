using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGeometryNetworkType
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.rdoCreateType = new RadioGroup();
            this.rdoCreateType.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoCreateType.Location = new Point(24, 32);
            this.rdoCreateType.Name = "rdoCreateType";
            this.rdoCreateType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoCreateType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoCreateType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoCreateType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "利用已有几何要素创建几何网络"), new RadioGroupItem(null, "创建空几何网络") });
            this.rdoCreateType.Size = new Size(200, 112);
            this.rdoCreateType.TabIndex = 0;
            this.rdoCreateType.SelectedIndexChanged += new EventHandler(this.rdoCreateType_SelectedIndexChanged);
            base.Controls.Add(this.rdoCreateType);
            base.Name = "BulidGeometryNetworkType";
            base.Size = new Size(272, 200);
            base.Load += new EventHandler(this.BulidGeometryNetworkType_Load);
            this.rdoCreateType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private RadioGroup rdoCreateType;
    }
}