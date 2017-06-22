using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewVersionControl
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
            this.label1 = new Label();
            this.comboParentVersion = new ComboBoxEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtName = new TextEdit();
            this.txtDescription = new MemoEdit();
            this.groupBox1 = new GroupBox();
            this.radioAccessType = new RadioGroup();
            this.comboParentVersion.Properties.BeginInit();
            this.txtName.Properties.BeginInit();
            this.txtDescription.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioAccessType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "父版本:";
            this.comboParentVersion.Location = new Point(16, 27);
            this.comboParentVersion.Name = "comboParentVersion";
            this.comboParentVersion.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboParentVersion.Size = new Size(192, 21);
            this.comboParentVersion.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 103);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "说明:";
            this.txtName.EditValue = "";
            this.txtName.Location = new Point(16, 75);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(192, 21);
            this.txtName.TabIndex = 4;
            this.txtDescription.EditValue = "";
            this.txtDescription.Location = new Point(16, 123);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(192, 56);
            this.txtDescription.TabIndex = 5;
            this.groupBox1.Controls.Add(this.radioAccessType);
            this.groupBox1.Location = new Point(16, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(192, 104);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "访问权限";
            this.radioAccessType.Location = new Point(16, 16);
            this.radioAccessType.Name = "radioAccessType";
            this.radioAccessType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioAccessType.Properties.Columns = 1;
            this.radioAccessType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "私有"), new RadioGroupItem(null, "公有"), new RadioGroupItem(null, "保护") });
            this.radioAccessType.Size = new Size(136, 80);
            this.radioAccessType.TabIndex = 7;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtDescription);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboParentVersion);
            base.Controls.Add(this.label1);
            base.Name = "NewVersionControl";
            base.Size = new Size(224, 304);
            base.Load += new EventHandler(this.NewVersionControl_Load);
            this.comboParentVersion.Properties.EndInit();
            this.txtName.Properties.EndInit();
            this.txtDescription.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioAccessType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private ComboBoxEdit comboParentVersion;
        private GroupBox groupBox1;
        private IArray iarray_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioAccessType;
        private MemoEdit txtDescription;
        private TextEdit txtName;
    }
}