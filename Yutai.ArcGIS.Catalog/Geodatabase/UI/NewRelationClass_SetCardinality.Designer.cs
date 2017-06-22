using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewRelationClass_SetCardinality
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
            this.rdoOnr2One = new RadioButton();
            this.rdoOne2More = new RadioButton();
            this.rdoMore2More = new RadioButton();
            this.chkAttribute = new CheckEdit();
            this.chkAttribute.Properties.BeginInit();
            base.SuspendLayout();
            this.rdoOnr2One.Checked = true;
            this.rdoOnr2One.Location = new Point(24, 16);
            this.rdoOnr2One.Name = "rdoOnr2One";
            this.rdoOnr2One.TabIndex = 0;
            this.rdoOnr2One.TabStop = true;
            this.rdoOnr2One.Text = "1-1(一对一)";
            this.rdoOnr2One.CheckedChanged += new EventHandler(this.rdoOnr2One_CheckedChanged);
            this.rdoOne2More.Location = new Point(24, 56);
            this.rdoOne2More.Name = "rdoOne2More";
            this.rdoOne2More.TabIndex = 1;
            this.rdoOne2More.Text = "1-M(一对多)";
            this.rdoOne2More.CheckedChanged += new EventHandler(this.rdoOne2More_CheckedChanged);
            this.rdoMore2More.Location = new Point(24, 96);
            this.rdoMore2More.Name = "rdoMore2More";
            this.rdoMore2More.TabIndex = 2;
            this.rdoMore2More.Text = "M-N(多对多)";
            this.rdoMore2More.CheckedChanged += new EventHandler(this.rdoMore2More_CheckedChanged);
            this.chkAttribute.Location = new Point(24, 152);
            this.chkAttribute.Name = "chkAttribute";
            this.chkAttribute.Properties.Caption = "关系类中包含属性";
            this.chkAttribute.Size = new Size(144, 19);
            this.chkAttribute.TabIndex = 3;
            this.chkAttribute.CheckedChanged += new EventHandler(this.chkAttribute_CheckedChanged);
            base.Controls.Add(this.chkAttribute);
            base.Controls.Add(this.rdoMore2More);
            base.Controls.Add(this.rdoOne2More);
            base.Controls.Add(this.rdoOnr2One);
            base.Name = "NewRelationClass_SetCardinality";
            base.Size = new Size(264, 320);
            base.Load += new EventHandler(this.NewRelationClass_SetCardinality_Load);
            this.chkAttribute.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private CheckEdit chkAttribute;
        private RadioButton rdoMore2More;
        private RadioButton rdoOne2More;
        private RadioButton rdoOnr2One;
    }
}