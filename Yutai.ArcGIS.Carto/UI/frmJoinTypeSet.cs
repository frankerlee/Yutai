using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class frmJoinTypeSet : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private esriJoinType esriJoinType_0 = esriJoinType.esriLeftOuterJoin;
        private PictureEdit pictureEdit1;
        private RadioGroup radioGroup1;

        public frmJoinTypeSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.esriJoinType_0 = esriJoinType.esriLeftOuterJoin;
            }
            else
            {
                this.esriJoinType_0 = esriJoinType.esriLeftInnerJoin;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmJoinTypeSet_Load(object sender, EventArgs e)
        {
            if (this.esriJoinType_0 == esriJoinType.esriLeftOuterJoin)
            {
                this.radioGroup1.SelectedIndex = 0;
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJoinTypeSet));
            this.pictureEdit1 = new PictureEdit();
            this.radioGroup1 = new RadioGroup();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.pictureEdit1.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0xa8, 8);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.ForeColor = SystemColors.Info;
            this.pictureEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Size = new Size(0xf8, 0xd8);
            this.pictureEdit1.TabIndex = 0;
            this.radioGroup1.Location = new Point(8, 8);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "保留所有记录                                                         "), new RadioGroupItem(null, "只保留匹配的记录") });
            this.radioGroup1.Size = new Size(160, 0xb8);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(240, 0x100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x138, 0x100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1b0, 290);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.pictureEdit1);
            
            base.Name = "frmJoinTypeSet";
            this.Text = "高级连接选项";
            base.Load += new EventHandler(this.frmJoinTypeSet_Load);
            this.pictureEdit1.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public esriJoinType JoinType
        {
            get
            {
                return this.esriJoinType_0;
            }
            set
            {
                this.esriJoinType_0 = value;
            }
        }
    }
}

