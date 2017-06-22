using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LayerDisplaySetCtrl
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
            this.chkShowMapTip = new CheckEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtPercent = new TextEdit();
            this.chkScaleSymbols = new CheckEdit();
            this.HyperLinkGroup = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.rdoHyperLinkeType = new RadioGroup();
            this.chkHyperline = new CheckEdit();
            this.chkShowMapTip.Properties.BeginInit();
            this.txtPercent.Properties.BeginInit();
            this.chkScaleSymbols.Properties.BeginInit();
            this.HyperLinkGroup.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.rdoHyperLinkeType.Properties.BeginInit();
            this.chkHyperline.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowMapTip.Location = new Point(12, 12);
            this.chkShowMapTip.Name = "chkShowMapTip";
            this.chkShowMapTip.Properties.Caption = "显示地图提示(用主显示字段)";
            this.chkShowMapTip.Size = new Size(200, 19);
            this.chkShowMapTip.TabIndex = 2;
            this.chkShowMapTip.CheckedChanged += new EventHandler(this.chkShowMapTip_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "透明度";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(138, 62);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "%";
            this.txtPercent.EditValue = "0";
            this.txtPercent.Location = new Point(82, 60);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new Size(48, 21);
            this.txtPercent.TabIndex = 5;
            this.txtPercent.EditValueChanged += new EventHandler(this.txtPercent_EditValueChanged);
            this.chkScaleSymbols.Location = new Point(12, 35);
            this.chkScaleSymbols.Name = "chkScaleSymbols";
            this.chkScaleSymbols.Properties.Caption = "设置参考比例后是否缩放符号";
            this.chkScaleSymbols.Size = new Size(200, 19);
            this.chkScaleSymbols.TabIndex = 6;
            this.chkScaleSymbols.CheckedChanged += new EventHandler(this.chkScaleSymbols_CheckedChanged);
            this.HyperLinkGroup.Controls.Add(this.cboFields);
            this.HyperLinkGroup.Controls.Add(this.rdoHyperLinkeType);
            this.HyperLinkGroup.Controls.Add(this.chkHyperline);
            this.HyperLinkGroup.Location = new Point(12, 87);
            this.HyperLinkGroup.Name = "HyperLinkGroup";
            this.HyperLinkGroup.Size = new Size(313, 128);
            this.HyperLinkGroup.TabIndex = 7;
            this.HyperLinkGroup.TabStop = false;
            this.HyperLinkGroup.Text = "超链接";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(144, 18);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(144, 21);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.rdoHyperLinkeType.Location = new Point(6, 45);
            this.rdoHyperLinkeType.Name = "rdoHyperLinkeType";
            this.rdoHyperLinkeType.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoHyperLinkeType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoHyperLinkeType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoHyperLinkeType.Properties.Columns = 3;
            this.rdoHyperLinkeType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "文档"), new RadioGroupItem(null, "URL"), new RadioGroupItem(null, "宏") });
            this.rdoHyperLinkeType.Size = new Size(166, 31);
            this.rdoHyperLinkeType.TabIndex = 4;
            this.rdoHyperLinkeType.SelectedIndexChanged += new EventHandler(this.rdoHyperLinkeType_SelectedIndexChanged);
            this.chkHyperline.Location = new Point(6, 20);
            this.chkHyperline.Name = "chkHyperline";
            this.chkHyperline.Properties.Caption = "使用支持设置超链接";
            this.chkHyperline.Size = new Size(145, 19);
            this.chkHyperline.TabIndex = 3;
            base.Controls.Add(this.HyperLinkGroup);
            base.Controls.Add(this.chkScaleSymbols);
            base.Controls.Add(this.txtPercent);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkShowMapTip);
            base.Name = "LayerDisplaySetCtrl";
            base.Size = new Size(336, 232);
            base.Load += new EventHandler(this.LayerDisplaySetCtrl_Load);
            this.chkShowMapTip.Properties.EndInit();
            this.txtPercent.Properties.EndInit();
            this.chkScaleSymbols.Properties.EndInit();
            this.HyperLinkGroup.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.rdoHyperLinkeType.Properties.EndInit();
            this.chkHyperline.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ComboBoxEdit cboFields;
        private CheckEdit chkHyperline;
        private CheckEdit chkScaleSymbols;
        private CheckEdit chkShowMapTip;
        private GroupBox HyperLinkGroup;
        private ILayer ilayer_0;
        private Label label2;
        private Label label3;
        private RadioGroup rdoHyperLinkeType;
        private TextEdit txtPercent;
    }
}